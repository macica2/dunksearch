using DunkSearch.Domain.Models.ServiceModels.Video;

namespace DunkSearch.Domain.Services
{
    public interface IVideoService
    {
        VideoListResponse ListVideos(VideoListRequest request);
    }
}
