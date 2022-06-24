using System.Collections.Generic;

namespace DunkSearch.Domain.Models.ServiceModels.UnsupportedVideo
{
    public class UnsupportedVideoListResponse
    {
        public List<Entities.UnsupportedVideo> UnsupportedVideos { get; set; }

        public bool IsSuccessful { get; set; }

        public string Message { get; set; }
    }
}
