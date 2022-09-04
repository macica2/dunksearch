using System;
using System.Collections.Generic;
using System.Net;

namespace DunkSearch.Domain.Models.ServiceModels.Caption
{
    public class CaptionListRequest
    {
        public CaptionListRequest()
        {
            PageSize = 20;
            PageNumber = 1;
        }

        public string SearchTerm { get; set; }

        public List<int> ChannelIds { get; set; }

        public int? VideoId { get; set; }

        public int? CaptionTypeId { get; set; }

        public DateTime? UploadDateFrom { get; set; }

        public DateTime? UploadDateTo { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public IPAddress IPAddress { get; set; }

        public Enums.SearchMode SearchMode { get; set; }
    }
}
