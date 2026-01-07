using DunkSearch.Domain.Models.ServiceModels.Video;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DunkSearch.Domain.Services
{
    public class VideoService : IVideoService
    {
        private DataContext _dataContext;
        public VideoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public VideoListResponse ListVideos(VideoListRequest request)
        {
            var response = new VideoListResponse();
            response.IsSuccessful = false;

            try
            {
                var query = _dataContext.Videos.AsQueryable();
                if (!string.IsNullOrEmpty(request.Title))
                {
                    query = query.Where(p => p.Title.ToLower().Contains(request.Title.ToLower()));
                }
                if (request.VideoId != null)
                {
                    query = query.Where(p => p.VideoId == request.VideoId);    
                }
                response.Videos = query.ToList();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public GetVideoCountResponse GetVideoCount(GetRandomVideoRequest request)
        {
            var query = BuildRandomVideoQuery(request);
            return new GetVideoCountResponse()
            {
                TotalVideoCount = query.Count()
            };
        }

        public Entities.Video GetRandomVideo(GetRandomVideoRequest request)
        {
            var query = BuildRandomVideoQuery(request);
            Random rand = new Random();
            var vidCount = request.TotalVideoCount;
            // If we selected any video options so far, don't randomly select them again
            if (request.ChoiceVideoIds?.Any() == true)
            {
                foreach (var vidId in request.ChoiceVideoIds)
                {
                    query = query.Where(p => p.VideoId != vidId);
                    vidCount -= 1;
                }
            }
            var skip = rand.Next(0, vidCount);
            return query.OrderBy(p => p.VideoId).Skip(skip).Take(1).First();
        }

        private IQueryable<Entities.Video> BuildRandomVideoQuery(GetRandomVideoRequest request)
        {
            var query = _dataContext.Videos.AsQueryable();
            var selectedChannelIds = request.ChannelOptions.Where(p => p.Selected).Select(p => p.ChannelId).ToList();
            if (selectedChannelIds.Any())
            {
                // if user deselected all channels, just select all, else filter to the selected ones
                query = query.Where(p => selectedChannelIds.Contains(p.ChannelId));
            }
            if (!request.IncludeLeagueVideos)
            {
                query = query.Where(p => !p.Title.StartsWith("League of Legends : ") && !p.Title.StartsWith("LoL : "));
            }
            return query;
        }
    }
}
