using DunkSearch.Domain;
using DunkSearch.Domain.Models.DataModels;
using DunkSearch.Domain.Models.ServiceModels.CaptionType;
using DunkSearch.Domain.Models.ServiceModels.Channel;
using DunkSearch.Domain.Models.ServiceModels.Video;
using DunkSearch.Domain.Models.ViewModels;
using DunkSearch.Domain.Services;
using DunkSearch.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DunkSearch.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChannelService _channelService;
        private readonly ICaptionService _captionService;
        private readonly ICaptionTypeService _captionTypeService;
        private readonly IVideoService _videoService;

        public HomeController(ILogger<HomeController> logger, 
            IChannelService channelService, 
            ICaptionService captionService, 
            ICaptionTypeService captionTypeService,
            IVideoService videoService)
        {
            _logger = logger;
            _channelService = channelService;
            _captionService = captionService;
            _captionTypeService = captionTypeService;
            _videoService = videoService;
        }

        public IActionResult Index(HomeViewModel model)
        {
            // On load, get values from database for the smaller dropdowns

            // Channel Dropdown
            var listChannelsResponse = _channelService.ListChannels(new ChannelListRequest());
            model.ChannelOptions = new List<ChannelChoiceModel>();
            foreach (var channel in listChannelsResponse.Channels)
            {
                model.ChannelOptions.Add(new ChannelChoiceModel()
                {
                    ChannelId = channel.ChannelId,
                    ChannelName= channel.Name,
                    Selected = (channel.Name == "videogamedunkey") // by default only check dunkey
                });
            };
            // If user passed in Channel ID parameters, check/uncheck options
            if (model.ChannelIds != null)
            {
                foreach (var channel in model.ChannelOptions)
                {
                    channel.Selected = model.ChannelIds.Contains(channel.ChannelId);
                }
            }

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
            if (model.CaptionTypeId != null) // sanity check to make sure the passed in id is valid
            {
                if (!listCaptionTypesResponse.CaptionTypes.Where(p => p.CaptionTypeId == model.CaptionTypeId).Any())
                {
                    model.CaptionTypeId = null;
                }
            }

            // If the VideoId URL parameter was specified, try to find
            // the matching video to pre-populate the search form
            if (model.VideoId != null)
            {
                var videoListResponse = _videoService.ListVideos(new VideoListRequest()
                {
                    VideoId = model.VideoId
                });
                var foundVideo = videoListResponse.Videos.FirstOrDefault();
                if (foundVideo != null)
                {
                    model.DefaultVideoName = foundVideo.Title;
                }
                else
                {
                    model.VideoId = null;
                }
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
                ChannelIds = model.ChannelOptions.Where(p => p.Selected).Select(p => p.ChannelId).ToList(),
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
