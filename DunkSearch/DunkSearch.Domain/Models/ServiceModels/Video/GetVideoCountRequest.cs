using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DunkSearch.Domain.Models.ServiceModels.Video
{
    public class GetVideoCountRequest
    {
        public List<int> SelectedChannelIds { get; set; }
    }
}
