using System;
using System.Collections.Generic;

namespace DunkSearch.Domain.Models.ServiceModels.AutomatedVideoProcessor
{
    public class GetVideoCaptionsAndroidResponse
    {
        public List<AndroidAction> actions { get; set; }
        public bool IsSuccessful { get; set; }
        public String Message { get; set; }

        /// <summary>
        /// Helper to get the name of the selected language used for this transcript.
        /// For now we'll always assume captions will be English auto-generated,
        /// because that's historically the case and this response doesn't include the actual value.
        /// </summary>
        public String SelectedLanguage
        {
            get
            {
                return "English (auto-generated)";
            }
        }

        /// <summary>
        /// Helper that determines if there are no captions present for this video
        /// TODO won't find out if this works until a new video gets released with no captions.
        /// </summary>
        public bool HasNoCaptions
        {
            get
            {
                return (actions == null ||
                    actions.Count == 0 ||
                    actions[0].elementsCommand?.transformEntityCommand?.arguments?.transformTranscriptSegmentListArguments?.overwrite?.initialSegments == null ||
                    actions[0].elementsCommand?.transformEntityCommand?.arguments?.transformTranscriptSegmentListArguments?.overwrite?.initialSegments.Length == 0
                );
            }
        }
    }


    public class AndroidAction
    {
        public ElementsCommand elementsCommand { get; set; }
    }

    public class ElementsCommand
    {
        public TransformEntityCommand transformEntityCommand { get; set; }
    }

    public class TransformEntityCommand
    {
        public Arguments arguments { get; set; }
    }

    public class Arguments
    {
        public TransformTranscriptSegmentListArguments transformTranscriptSegmentListArguments { get; set; }
    }

    public class TransformTranscriptSegmentListArguments
    {
        public Overwrite overwrite { get; set; }

    }

    public class Overwrite
    {
        public InitialSegments[] initialSegments { get; set; }
    }

    public class InitialSegments
    {
        public AndroidTranscriptSegmentRenderer transcriptSegmentRenderer { get; set; }
    }

    public class AndroidTranscriptSegmentRenderer
    {
        public string startMs { get; set; }
        public AndroidSnippet snippet { get; set; }
    }

    public class AndroidSnippet
    {
        public ElementsAttributedString elementsAttributedString { get; set; }
    }

    public class ElementsAttributedString
    {
        public string content { get; set; }
    }

}
