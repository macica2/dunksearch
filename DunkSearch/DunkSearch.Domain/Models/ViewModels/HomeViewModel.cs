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

        /// <summary>
        /// I want the URL to just show the value, not the enum name.
        /// And if I use enum instead of int, then it doesn't bind properly to radio button.
        /// </summary>
        [Display(Name = "Search Mode")]
        public int SearchMode { get; set; }
    }
}
