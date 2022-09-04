using System.Net;

namespace DunkSearch.Domain.Models.ServiceModels.AppEventLogService
{
    public class AppEventLogCreateRequest
    {
        public string EventType { get; set; }

        public IPAddress IPAddress { get; set; }

        public string EventDetails { get; set; }
    }
}
