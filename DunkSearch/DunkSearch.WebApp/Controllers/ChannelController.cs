using DunkSearch.Domain.Models.DataModels;
using DunkSearch.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DunkSearch.WebApp.Controllers
{
    public class ChannelController : Controller
    {
        private readonly IChannelService _channelService;

        public ChannelController(IChannelService channelService)
        {
            _channelService = channelService;
        }

        [HttpGet]
        public IActionResult GetSelectItems()
        {
            var response = _channelService.ListChannels(new Domain.Models.ServiceModels.Channel.ChannelListRequest());

            var items = new List<SelectListItem>();
            foreach (var channel in response.Channels)
            {
                items.Add(new SelectListItem()
                {
                    id = channel.ChannelId.ToString(),
                    text = channel.Name
                });
            }

            return Json(new { results = items });
        }
    }
}
