using System.Collections.Generic;

namespace DunkSearch.Domain.Models.ServiceModels.Channel
{
    public class ChannelListResponse
    {
        public List<Entities.Channel> Channels { get; set; }

        public bool IsSuccessful { get; set; }

        public string Message { get; set; }
    }
}
