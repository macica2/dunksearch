﻿using DunkSearch.Domain.Models.DataModels;
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

        [Display(Name = "Channel")]
        public int? ChannelId { get; set; }

        [Display(Name = "Video")]
        public int? VideoId { get; set; }

        [Display(Name = "Caption Language")]
        public int? CaptionTypeId { get; set; }

        [Display(Name = "Upload Date Range - Start Date")]
        public DateTime? UploadDateFrom { get; set; }

        [Display(Name = "Upload Date Range - End Date")]
        public DateTime? UploadDateTo { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        //public int SearchMode { get; set; }

        public List<SelectListItem> ChannelOptions { get; set; }

        public List<SelectListItem> CaptionTypeOptions { get; set; }
    }
}