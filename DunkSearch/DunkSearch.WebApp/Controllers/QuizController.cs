using DunkSearch.Domain.Models.DataModels;
using DunkSearch.Domain.Models.ServiceModels.AppEventLogService;
using DunkSearch.Domain.Models.ServiceModels.Channel;
using DunkSearch.Domain.Models.ServiceModels.Video;
using DunkSearch.Domain.Models.ViewModels;
using DunkSearch.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DunkSearch.WebApp.Controllers
{
    public class QuizController : Controller
    {
        private readonly ILogger<QuizController> _logger;
        private readonly IChannelService _channelService;
        private readonly ICaptionService _captionService;
        private readonly ICaptionTypeService _captionTypeService;
        private readonly IVideoService _videoService;
        private readonly IAppEventLogService _appEventLogService;

        public QuizController(ILogger<QuizController> logger,
            IChannelService channelService,
            ICaptionService captionService,
            ICaptionTypeService captionTypeService,
            IVideoService videoService,
            IAppEventLogService appEventLogService)
        {
            _logger = logger;
            _channelService = channelService;
            _captionService = captionService;
            _captionTypeService = captionTypeService;
            _videoService = videoService;
            _appEventLogService = appEventLogService;
        }


        public IActionResult Index(QuizViewModel model)
        {
            _appEventLogService.CreateLog(new AppEventLogCreateRequest()
            {
                EventType = "Quiz",
                EventDetails = "Get Index",
                IPAddress = Request.HttpContext.Connection.RemoteIpAddress
            });

            // Load default settings
            var listChannelsResponse = _channelService.ListChannels(new ChannelListRequest());
            model.ChannelOptions = new List<ChannelChoiceModel>();
            foreach (var channel in listChannelsResponse.Channels)
            {
                model.ChannelOptions.Add(new ChannelChoiceModel()
                {
                    ChannelId = channel.ChannelId,
                    ChannelName = channel.Name,
                    Selected = (channel.Name == "videogamedunkey") // by default only check dunkey
                });
            };
            model.IncludeLeagueVideos = false;
            model.NumCaptionsToShow = 3;
            model.NumVideosToShow = 4;
            model.NumCaptionsOptions = new List<SelectListItem>();
            for (int i = 1; i < 4; i++)
            {
                model.NumCaptionsOptions.Add(new SelectListItem()
                {
                    id = i.ToString(),
                    text = i.ToString()
                });
            }
            model.NumVideosOptions = GetNumVideosOptions();
            
            return View(model);
        }

        [HttpPost]
        public int GetVideoCount(GetRandomVideoRequest request)
        {
            return _videoService.GetVideoCount(request).TotalVideoCount;
        }

        [HttpPost]
        public PartialViewResult GetQuizQuestion(GetRandomVideoRequest getRandomVideoRequest)
        {
            // Validation - in case user tried modifying the request on the front end
            getRandomVideoRequest.NumVideosToShow = Math.Clamp(getRandomVideoRequest.NumVideosToShow, 0, 6);
            getRandomVideoRequest.NumCaptionsToShow = Math.Clamp(getRandomVideoRequest.NumCaptionsToShow, 1, 3);

            var video = _videoService.GetRandomVideo(getRandomVideoRequest);
            var captions = _captionService.GetRandomCaptions(video.VideoId, getRandomVideoRequest.NumCaptionsToShow);
            var model = new QuizQuestionViewModel();
            model.CorrectVideo = video;
            model.Captions = new List<string>();
            foreach (var caption in captions)
            {
                model.Captions.Add(caption.CaptionText);
            }
            model.CaptionStartSeconds = captions.FirstOrDefault()?.StartSeconds ?? 0;

            
            model.Videos = new List<Domain.Entities.Video>();
            model.Videos.Add(video);
            getRandomVideoRequest.ChoiceVideoIds = new List<int>() { video.VideoId };
            // get the incorrect videos now
            for (int i = 0; i < (getRandomVideoRequest.NumVideosToShow - 1); i++)
            {
                var incorrectVideo = _videoService.GetRandomVideo(getRandomVideoRequest);
                model.Videos.Add(incorrectVideo);
                getRandomVideoRequest.ChoiceVideoIds.Add(incorrectVideo.VideoId);
            }
            model.Videos = model.Videos.OrderBy(p => p.Title).ToList();
            
            return PartialView("_QuizQuestion", model);
        }

        private List<SelectListItem> GetNumVideosOptions()
        {
            var options = new List<SelectListItem>();
            for (int i = 2; i < 7; i++)
            {
                options.Add(new SelectListItem()
                {
                    id = i.ToString(),
                    text = i.ToString()
                });
            }
            options.Add(new SelectListItem()
                {
                    id = "0",
                    text = "All"
                }
            );
            return options;
        }
    }
}
