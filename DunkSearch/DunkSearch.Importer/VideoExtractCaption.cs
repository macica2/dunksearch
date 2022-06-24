using System;
using System.Linq;

namespace DunkSearch.Importer
{
    class VideoExtractCaption
    {
        public string Timestamp { get; set; }

        public string Caption { get; set; }

        /// <summary>
        /// Converts a timestamp into the number of seconds after the video start
        /// </summary>
        public int StartSeconds 
        { 
            get
            {
                var seconds = 0;
                var timestampSplit = Timestamp.Split(':').Select(p => Int32.Parse(p)).Reverse().ToArray();
                // If timestamp format was like hh:mm:ss, our array is [ss, mm, hh]
                var multiplier = 1; // we start with seconds (1 * ss), then minutes (60 * mm), then hour (60 * 60 * hh)
                for (int i = 0; i < timestampSplit.Length; i++)
                {
                    seconds += timestampSplit[i] * multiplier;
                    multiplier *= 60;
                }
                return seconds;
            }
        }
    }
}
