using DunkSearch.Domain.Models.ServiceModels.UnsupportedVideo;
using DunkSearch.Domain.Models.ViewModels;
using DunkSearch.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace DunkSearch.WebApp.Controllers
{
    public class FAQController : Controller
    {
        private readonly IUnsupportedVideoService _unsupportedVideoService;

        public FAQController(IUnsupportedVideoService unsupportedVideoService)
        {
            _unsupportedVideoService = unsupportedVideoService;
        }

        public IActionResult Index(FAQViewModel model)
        {
            var resp = _unsupportedVideoService.ListUnsupportedVideos(new UnsupportedVideoListRequest());
            model.UnsupportedVideos = resp.UnsupportedVideos;
            return View(model);
        }
    }
}
