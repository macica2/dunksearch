using DunkSearch.Domain.Models.ServiceModels.UnsupportedVideo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DunkSearch.Domain.Services
{
    public class UnsupportedVideoService : IUnsupportedVideoService
    {
        private DataContext _dataContext;
        public UnsupportedVideoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public UnsupportedVideoListResponse ListUnsupportedVideos(UnsupportedVideoListRequest request)
        {
            var response = new UnsupportedVideoListResponse();
            response.IsSuccessful = false;

            try
            {
                response.UnsupportedVideos = _dataContext.UnsupportedVideos.Include("Channel")
                    .OrderBy(p => p.ChannelId)
                    .ThenBy(p => p.Title)
                    .ToList();
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
