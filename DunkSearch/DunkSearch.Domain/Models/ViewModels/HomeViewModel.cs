using DunkSearch.Domain.Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DunkSearch.Domain.Models.ViewModels
{
    public class HomeViewModel
    {
        [Required]
        [Display(Name = "Search Term")]
        public string SearchTerm { get; set; }

        [Display(Name = "Video")]
        public int? VideoId { get; set; }

        public string DefaultVideoName { get; set; }

        [Display(Name = "Caption Language")]
        public int? CaptionTypeId { get; set; }

        [Display(Name = "Upload Date Range - Start Date")]
        public DateTime? UploadDateFrom { get; set; }

        [Display(Name = "Upload Date Range - End Date")]
        public DateTime? UploadDateTo { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        //public int SearchMode { get; set; }

        [Display(Name = "Channels")]
        public List<ChannelChoiceModel> ChannelOptions { get; set; }
        
        public List<int> ChannelIds { get; set; }

        public List<SelectListItem> CaptionTypeOptions { get; set; }

        [Display(Name = "Search Mode")]
        public string SearchMode { get; set; }
    }
}
