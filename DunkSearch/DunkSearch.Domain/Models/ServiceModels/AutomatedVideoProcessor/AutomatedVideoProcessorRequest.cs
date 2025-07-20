using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DunkSearch.Domain.Models.ServiceModels.AutomatedVideoProcessor
{
    public class AutomatedVideoProcessorRequest
    {
        public string GmailAddress { get; set; }
        public string YouTubeAPIKey { get; set; }
        public string SmtpAppPassword { get; set; }
    }
}
