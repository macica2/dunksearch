using DunkSearch.Domain.Models.ServiceModels.UnsupportedVideo;

namespace DunkSearch.Domain.Services
{
    public interface IUnsupportedVideoService
    {
        UnsupportedVideoListResponse ListUnsupportedVideos(UnsupportedVideoListRequest request);
    }
}
