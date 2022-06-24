using DunkSearch.Domain.Models.DataModels;
using DunkSearch.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DunkSearch.WebApp.Controllers
{
    public class VideoController : Controller
    {
        private readonly IVideoService _VideoService;

        public VideoController(IVideoService VideoService)
        {
            _VideoService = VideoService;
        }

        [HttpGet]
        public IActionResult GetSelectItems(string searchTerm)
        {
            var response = _VideoService.ListVideos(new Domain.Models.ServiceModels.Video.VideoListRequest()
            {
                Title = searchTerm
            });

            var items = new List<SelectListItem>();
            foreach (var Video in response.Videos)
            {
                items.Add(new SelectListItem()
                {
                    id = Video.VideoId.ToString(),
                    text = Video.Title
                });
            }

            return Json(new { results = items });
        }
    }
}
