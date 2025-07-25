﻿using DunkSearch.Domain.Entities;
using DunkSearch.Domain.Models.ServiceModels.AutomatedVideoProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Google.Protobuf;
using System.Net.Mail;

namespace DunkSearch.Domain.Services
{
    // TODO eventually update this to use dependency injection
    public class AutomatedVideoProcessorService
    {
        private DataContext _dataContext;
        private List<Channel> _channels;
        private List<CaptionType> _captionTypes;
        private static HttpClient httpClient = new HttpClient();
        private List<String> _logs = new List<String>();

        public AutomatedVideoProcessorService(DataContext dataContext)
        {
            _dataContext = dataContext;

            // Get channels and caption types
            _channels = dataContext.Channels.ToList();
            _captionTypes = dataContext.CaptionTypes.ToList();
        }

        /// <summary>
        /// Kicks off the whole process to look for new videos and add their captions to the DB.
        /// </summary>
        /// <param name="request"></param>
        public void StartProcess(AutomatedVideoProcessorRequest request)
        {
            try
            {
                foreach (var channel in _channels)
                {
                    ProcessChannel(channel, request.YouTubeAPIKey);
                }
            }
            catch (Exception ex)
            {
                // If processing failed, add exception to the email logs
                _logs.Add(ex.Message + " | " + ex.StackTrace);
            }

            try
            {
                SendSummaryEmail(request.GmailAddress, request.SmtpAppPassword);
            }
            catch (Exception ex)
            {
                // If email fails too, try logging to the database
                _dataContext.Add(new AppEventLog()
                {
                    CreateDate = DateTime.Now,
                    EventType = "Automated Video Processing Error",
                    EventDetails = ex.Message + " | " + ex.StackTrace
                });
                _dataContext.Add(new AppEventLog()
                {
                    CreateDate = DateTime.Now,
                    EventType = "Automated Video Processing Error",
                    EventDetails = string.Join(" ||| ", _logs) // arbitrary separator to make reading easier
                });
                _dataContext.SaveChanges();
            }
        }

        private void ProcessChannel(Channel channel, string ytApiKey)
        {
            _logs.Add("Starting processing of channel " + channel.Name + "");

            // Get the latest videos from this channel
            var resp = GetLatestVideos(ytApiKey, channel);
            if (resp == null)
            {
                return;
            }

            // Grab just the YouTube IDs of the latest videos
            var vidIds = resp.items.Select(p => p.snippet.resourceId.videoId).ToList();

            // Determine which videos were already processed before
            var existingVideoIds = new List<String>();
            existingVideoIds.AddRange(_dataContext.Videos.Where(p => p.ChannelId == channel.ChannelId && vidIds.Contains(p.YTVideoId)).Select(p => p.YTVideoId).ToList());
            existingVideoIds.AddRange(_dataContext.UnsupportedVideos.Where(p => p.ChannelId == channel.ChannelId && vidIds.Contains(p.YTVideoId)).Select(p => p.YTVideoId).ToList());

            foreach (var item in resp.items)
            {
                // If this video wasn't already in our DB, then we can process it
                if (!existingVideoIds.Contains(item.snippet.resourceId.videoId))
                {
                    // get the captions
                    var getCaptionsResponse = GetCaptionsForVideo(item.snippet.resourceId.videoId);

                    // Check for errors, log them, and add records for unsupported videos
                    if (!getCaptionsResponse.IsSuccessful)
                    {
                        _logs.Add($"<b>ERROR:</b> Failed to get captions for video <i>{item.snippet.title}</i>. Please manually fix this. Error: {getCaptionsResponse.Message}");
                        continue;
                    }
                    if (getCaptionsResponse.HasNoCaptions)
                    {
                        _logs.Add($"<b>ERROR:</b> Video <i>{item.snippet.title}</i> had no captions to process. Adding to unsupported videos.");
                        AddUnsupportedVideo(channel, item.snippet.resourceId.videoId, item.snippet.title, "No captions");
                        continue;
                    }

                    if (getCaptionsResponse.SelectedLanguage != "English" && getCaptionsResponse.SelectedLanguage != "English (auto-generated)")
                    {
                        _logs.Add($"<b>ERROR:</b> Video <i>{item.snippet.title}</i> had captions, but not English. Adding to unsupported videos.");
                        AddUnsupportedVideo(channel, item.snippet.resourceId.videoId, item.snippet.title, "No English captions");
                        continue;
                    }

                    // If we got to this point, we should be able to process this new video
                    var video = new Video()
                    {
                        ChannelId = channel.ChannelId,
                        YTVideoId = item.snippet.resourceId.videoId,
                        Title = item.snippet.title,
                        UploadDate = item.snippet.publishedAt.Date
                    };
                    _dataContext.Add(video);
                    _dataContext.SaveChanges();
                    _logs.Add($"Added <i>{video.Title}</i> to the DB.");

                    var captionTypeId = _captionTypes.Where(p => p.Name == getCaptionsResponse.SelectedLanguage).Select(p => p.CaptionTypeId).First();
                    var captionsToAdd = ParseCaptions(video.VideoId, captionTypeId, getCaptionsResponse);
                    _dataContext.AddRange(captionsToAdd);
                    _dataContext.SaveChanges();
                    _logs.Add($"Added {captionsToAdd.Count} new caption(s) for video.");
                }
                else
                {
                    // Leave this commented out during the nightly job because it's too much info
                    //_logs.Add($"Video {item.snippet.title} was already processed before.");
                }
            }
        }

        /// <summary>
        /// Uses the YouTube API to list the most recent videos from
        /// a channel's Uploads playlist. Defaults to 5 videos.
        /// </summary>
        /// <param name="ytApiKey"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        private GetLatestVideosResponse GetLatestVideos(string ytApiKey, Channel channel)
        {
            // Get the 5 latest vids for the channel
            var url = $"https://youtube.googleapis.com/youtube/v3/playlistItems?part=snippet&playlistId={channel.UploadsPlaylistId}&key={ytApiKey}";
            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            // TODO NEED TO ADD RETRY LOGIC? SOMETIMES YOU GET INTERNAL SERVER ERROR
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<GetLatestVideosResponse>(jsonString);
            }
            else
            {
                _logs.Add($"<b>ERROR:</b> Failed to get latest videos for {channel.Name}. Status Code: {response.StatusCode}. Reason: {response.ReasonPhrase}");
                return null;
            }
        }

        /// <summary>
        /// Makes a call to YouTube to get get the raw caption data for a video.
        /// Uses a hacky method that uses the YouTube UI key to query data used
        /// to render the transcript in the UI. More info at https://stackoverflow.com/a/70013529.
        /// We do this because YT gives us no option to download other people's captions.
        /// This code could break if YouTube ever changes this logic.
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        private GetVideoCaptionsResponse GetCaptionsForVideo(String videoId)
        {
            GetVideoCaptionsResponse getCaptionsResponse = new GetVideoCaptionsResponse();
            var url = "https://www.youtube.com/youtubei/v1/get_transcript?key=AIzaSyAO_FJ2SlqU8Q4STEHLGCilw_Y9_11qcW8"; // YT UI Key
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), url))
            {
                // We must encode the video ID and the language of the captions we're trying to grab.
                // It must be in a specific format or the request will fail.
                var desiredLanguage = new Models.ProtoModels.DesiredLanguage()
                {
                    PrimaryLanguage = "asr",
                    SecondaryLanguage = "en"
                };
                var captionRequest = new Models.ProtoModels.CaptionRequest()
                {
                    VideoId = videoId,
                    DesiredLanguageStr = desiredLanguage.ToByteString().ToBase64() // You must encode the language here even though we encode the whole request after
                };
                var encodedParams = captionRequest.ToByteString().ToBase64();
                request.Content = new StringContent("{\"context\":{\"client\":{\"clientName\":\"WEB\",\"clientVersion\":\"2.2021111\"}},\"params\":\"" + encodedParams + "\"}");
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                var response = httpClient.SendAsync(request).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    getCaptionsResponse = JsonSerializer.Deserialize<GetVideoCaptionsResponse>(jsonString);
                    getCaptionsResponse.IsSuccessful = true;
                }
                else
                {
                    getCaptionsResponse.IsSuccessful = false;
                    getCaptionsResponse.Message = response.StatusCode + " " + response.ReasonPhrase;
                }
            }

            return getCaptionsResponse;
        }

        /// <summary>
        /// Processes the JSON structure used for the YouTube UI
        /// and initializes Caption entities from the data.
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="captionTypeId"></param>
        /// <param name="getCaptionsResponse"></param>
        /// <returns></returns>
        private List<Caption> ParseCaptions(int videoId, int captionTypeId, GetVideoCaptionsResponse getCaptionsResponse)
        {
            // add the caption records for each line in the input
            var captionsToAdd = new List<Caption>();
            foreach (var initialSegment in getCaptionsResponse.actions[0].updateEngagementPanelAction?.content.transcriptRenderer.content.transcriptSearchPanelRenderer.body.transcriptSegmentListRenderer.initialSegments)
            {
                // There can technically be multiple runs per snippet/segment, but in reality it's always 1
                foreach (var run in initialSegment.transcriptSegmentRenderer.snippet.runs)
                {
                    if (run.text != null)
                    {
                        var startSeconds = (Int32.Parse(initialSegment.transcriptSegmentRenderer.startMs) / 1000);
                        captionsToAdd.Add(new Caption()
                        {
                            VideoId = videoId,
                            CaptionTypeId = captionTypeId,
                            StartSeconds = startSeconds,
                            CaptionText = run.text
                        });
                    }
                    else
                    {
                        _logs.Add($"<b>ERROR:</b> Caption text was blank at {initialSegment.transcriptSegmentRenderer.startMs}ms. Skipping caption.");
                    }
                }
            }

            return captionsToAdd;
        }

        /// <summary>
        /// Adds an UnsupportedVideo record for a video.
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="videoId"></param>
        /// <param name="title"></param>
        /// <param name="reason"></param>
        private void AddUnsupportedVideo(Channel channel, string videoId, string title, string reason)
        {
            var unsupportedVid = new UnsupportedVideo()
            {
                ChannelId = channel.ChannelId,
                Title = title,
                YTVideoId = videoId,
                Reason = reason
            };
            _dataContext.Add(unsupportedVid);
            _dataContext.SaveChanges();
        }

        /// <summary>
        /// Uses Gmail SMTP to send an email summarizing this run.
        /// </summary>
        private void SendSummaryEmail(string gmailAddress, string smtpAppPassword)
        {
            var emailBody = string.Join("<br>", _logs);

            try
            {
                SmtpClient smtpClient = new SmtpClient()
                {
                    Credentials = new System.Net.NetworkCredential(gmailAddress, smtpAppPassword),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true
                };
                
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(gmailAddress, "DunkSearch Bot"), // even if you set a different from address, it just uses the gmail address
                    Subject = "DunkSearch Finished Processing Videos",
                    Body = emailBody,
                    IsBodyHtml = true,
                    BodyEncoding = System.Text.Encoding.UTF8
                };
                mail.To.Add(new MailAddress(gmailAddress));
                
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                // If somehow the email failed to send, create an app event log record
                // with all the logs so someone can manually check what happened.
                _dataContext.Add(new AppEventLog()
                {
                    CreateDate = DateTime.Now,
                    EventType = "Automated Video Processing Error",
                    EventDetails = emailBody + "<br>" + ex.Message + ". " + ex.StackTrace
                });
            }
        }
    }
}
