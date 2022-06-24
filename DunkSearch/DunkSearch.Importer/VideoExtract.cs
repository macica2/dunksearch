using System;
using System.Collections.Generic;

namespace DunkSearch.Importer
{
    class VideoExtract
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string UploadDate { get; set; }
        public string CaptionLanguage { get; set; }
        public List<VideoExtractCaption> Transcript { get; set; }

        /// <summary>
        /// In the JSON, dates are formatted like 'Apr 4, 2022',
        /// so we need to convert this string to a DateTime.
        /// </summary>
        public DateTime UploadDateAsDate
        {
            get
            {
                return DateTime.Parse(UploadDate);
            }
        }
    }
}
