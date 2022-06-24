using DunkSearch.Domain;
using DunkSearch.Domain.Models.DataModels;
using DunkSearch.Domain.Models.ServiceModels.CaptionType;
using DunkSearch.Domain.Models.ServiceModels.Channel;
using DunkSearch.Domain.Models.ViewModels;
using DunkSearch.Domain.Services;
using DunkSearch.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;

namespace DunkSearch.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChannelService _channelService;
        private readonly ICaptionService _captionService;
        private readonly ICaptionTypeService _captionTypeService;

        public HomeController(ILogger<HomeController> logger, 
            IChannelService channelService, 
            ICaptionService captionService, 
            ICaptionTypeService captionTypeService)
        {
            _logger = logger;
            _channelService = channelService;
            _captionService = captionService;
            _captionTypeService = captionTypeService;
        }

        public IActionResult Index(HomeViewModel model)
        {
            // On load, get values from database for the smaller dropdowns
            // Channel Dropdown
            var listChannelsResponse = _channelService.ListChannels(new ChannelListRequest());
            model.ChannelOptions = new List<SelectListItem>();
            foreach (var channel in listChannelsResponse.Channels)
            {
                model.ChannelOptions.Add(new SelectListItem()
                {
                    id = channel.ChannelId.ToString(),
                    text = channel.Name
                });
            };
            // Caption Type Dropdown
            var listCaptionTypesResponse = _captionTypeService.ListCaptionTypes(new CaptionTypeListRequest());
            model.CaptionTypeOptions = new List<SelectListItem>();
            foreach (var captionType in listCaptionTypesResponse.CaptionTypes)
            {
                model.CaptionTypeOptions.Add(new SelectListItem()
                {
                    id = captionType.CaptionTypeId.ToString(),
                    text = captionType.Name
                });
            }

            model.PageSize = 24;
            model.PageNumber = 1;
            return View(model);
        }

        [HttpPost]
        public PartialViewResult Search(HomeViewModel model)
        {
            var response = _captionService.ListCaptions(new Domain.Models.ServiceModels.Caption.CaptionListRequest()
            {
                SearchTerm = model.SearchTerm,
                CaptionTypeId = model.CaptionTypeId,
                ChannelId = model.ChannelId,
                VideoId = model.VideoId,
                UploadDateFrom = model.UploadDateFrom,
                UploadDateTo = model.UploadDateTo,
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
                IPAddress = Request.HttpContext.Connection.RemoteIpAddress
            });

            return PartialView("_SearchResults", response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
