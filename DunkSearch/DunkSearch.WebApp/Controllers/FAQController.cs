using DunkSearch.Domain.Models.ServiceModels.AppEventLogService;
using DunkSearch.Domain.Models.ServiceModels.UnsupportedVideo;
using DunkSearch.Domain.Models.ViewModels;
using DunkSearch.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace DunkSearch.WebApp.Controllers
{
    public class FAQController : Controller
    {
        private readonly IUnsupportedVideoService _unsupportedVideoService;
        private readonly IAppEventLogService _appEventLogService;

        public FAQController(IUnsupportedVideoService unsupportedVideoService, IAppEventLogService appEventLogService)
        {
            _unsupportedVideoService = unsupportedVideoService;
            _appEventLogService = appEventLogService;
        }

        public IActionResult Index(FAQViewModel model)
        {
            _appEventLogService.CreateLog(new AppEventLogCreateRequest()
            {
                EventType = "FAQ",
                EventDetails = "Get Index",
                IPAddress = Request.HttpContext.Connection.RemoteIpAddress
            });

            var resp = _unsupportedVideoService.ListUnsupportedVideos(new UnsupportedVideoListRequest());
            model.UnsupportedVideos = resp.UnsupportedVideos;
            return View(model);
        }
    }
}
