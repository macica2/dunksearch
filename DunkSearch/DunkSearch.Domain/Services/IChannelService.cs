using DunkSearch.Domain.Models.ServiceModels.Channel;

namespace DunkSearch.Domain.Services
{
    public interface IChannelService
    {
        ChannelListResponse ListChannels(ChannelListRequest request);
    }
}
