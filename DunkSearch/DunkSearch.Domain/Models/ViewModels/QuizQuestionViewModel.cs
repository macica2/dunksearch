using System.Collections.Generic;

namespace DunkSearch.Domain.Models.ViewModels
{
    public class QuizQuestionViewModel
    {
        public Entities.Video CorrectVideo { get; set; }

        public List<string> Captions { get; set; }

        /// <summary>
        /// All options, including the correct and incorrect ones
        /// </summary>
        public List<Entities.Video> Videos { get; set; }

        public int CaptionStartSeconds { get; set; }
    }
}
