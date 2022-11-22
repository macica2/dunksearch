using System;
using System.Collections.Generic;

namespace DunkSearch.Domain.Models.ServiceModels.AutomatedVideoProcessor
{
    /// <summary>
    /// Class used to parse the JSON response from the YouTube API.
    /// Irrelevant properties have been omitted.
    /// </summary>
    public class GetLatestVideosResponse
    {
        public List<Item> items { get; set; }
    }

    public class Item
    {
        public Snippet snippet { get; set; }
    }

    public class Snippet
    {
        public DateTime publishedAt { get; set; }
        public string channelId { get; set; }
        public string title { get; set; }
        public string channelTitle { get; set; }
        public ResourceId resourceId { get; set; }
        public string videoOwnerChannelTitle { get; set; }
        public string videoOwnerChannelId { get; set; }
    }

    public class ResourceId
    {
        public string videoId { get; set; }
    }
}
