using DunkSearch.Domain.Models.DataModels;
using System.Collections.Generic;

namespace DunkSearch.Domain.Models.ServiceModels.Video
{
    public class GetRandomVideoRequest
    {
        public int TotalVideoCount { get; set; }
        public bool IncludeLeagueVideos { get; set; }
        public int NumVideosToShow { get; set; }
        public int NumCaptionsToShow { get; set; }
        public List<ChannelChoiceModel> ChannelOptions { get; set; }
        public List<int> ChoiceVideoIds { get; set; }
    }
}
