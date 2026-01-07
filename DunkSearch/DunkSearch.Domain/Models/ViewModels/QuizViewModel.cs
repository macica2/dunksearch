using DunkSearch.Domain.Models.DataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DunkSearch.Domain.Models.ViewModels
{
    public class QuizViewModel
    {
        [Display(Name = "Channels to Pull From")]
        public List<ChannelChoiceModel> ChannelOptions { get; set; }

        [Display(Name = "Include League of Legends Videos?")]
        public bool IncludeLeagueVideos { get; set; }
        
        [Display(Name = "Number of Video Options")]
        public int NumVideosToShow { get; set; }

        [Display(Name = "Number of Caption Hints")]
        public int NumCaptionsToShow { get; set; }
        public List<SelectListItem> NumCaptionsOptions { get; set; }
        public List<SelectListItem> NumVideosOptions { get; set; }
    }
}
