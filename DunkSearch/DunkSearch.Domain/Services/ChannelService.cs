using DunkSearch.Domain.Models.ServiceModels.Channel;
using System;
using System.Linq;

namespace DunkSearch.Domain.Services
{
    public class ChannelService : IChannelService
    {
        private DataContext _dataContext;
        public ChannelService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ChannelListResponse ListChannels(ChannelListRequest request)
        {
            var response = new ChannelListResponse();
            response.IsSuccessful = false;

            try
            {
                response.Channels = _dataContext.Channels.ToList();
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
