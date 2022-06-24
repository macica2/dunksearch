using DunkSearch.Domain.Models.DataModels;
using DunkSearch.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DunkSearch.WebApp.Controllers
{
    public class CaptionTypeController : Controller
    {
        private readonly ICaptionTypeService _CaptionTypeService;

        public CaptionTypeController(ICaptionTypeService CaptionTypeService)
        {
            _CaptionTypeService = CaptionTypeService;
        }

        [HttpGet]
        public IActionResult GetSelectItems()
        {
            var response = _CaptionTypeService.ListCaptionTypes(new Domain.Models.ServiceModels.CaptionType.CaptionTypeListRequest());

            var items = new List<SelectListItem>();
            foreach (var CaptionType in response.CaptionTypes)
            {
                items.Add(new SelectListItem()
                {
                    id = CaptionType.CaptionTypeId.ToString(),
                    text = CaptionType.Name
                });
            }

            return Json(new { results = items });
        }
    }
}
