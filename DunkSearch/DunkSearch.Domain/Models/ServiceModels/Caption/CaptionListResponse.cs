using System.Collections.Generic;

namespace DunkSearch.Domain.Models.ServiceModels.Caption
{
    public class CaptionListResponse
    {
        public List<Entities.Caption> Captions { get; set; }

        public bool IsSuccessful { get; set; }

        public string Message { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
