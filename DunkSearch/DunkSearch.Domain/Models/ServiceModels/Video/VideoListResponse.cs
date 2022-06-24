using System.Collections.Generic;

namespace DunkSearch.Domain.Models.ServiceModels.Video
{
    public class VideoListResponse
    {
        public List<Entities.Video> Videos { get; set; }

        public bool IsSuccessful { get; set; }

        public string Message { get; set; }
    }
}
