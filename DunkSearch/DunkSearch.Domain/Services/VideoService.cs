using DunkSearch.Domain.Models.ServiceModels.Video;
using System;
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
    }
}
