using System.Collections.Generic;

namespace DunkSearch.Domain.Models.ServiceModels.CaptionType
{
    public class CaptionTypeListResponse
    {
        public List<Entities.CaptionType> CaptionTypes { get; set; }

        public bool IsSuccessful { get; set; }

        public string Message { get; set; }
    }
}
