using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DunkSearch.Domain.Models.ServiceModels.AutomatedVideoProcessor
{
    public class AutomatedVideoProcessorRequest
    {
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string YouTubeAPIKey { get; set; }
        public string SendGridAPIKey { get; set; }
    }
}
