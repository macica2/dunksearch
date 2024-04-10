using System;
using System.Collections.Generic;
using System.Linq;

namespace DunkSearch.Domain.Models.ServiceModels.AutomatedVideoProcessor
{
    /// <summary>
    /// Class used to parse the JSON response from the YouTube UI API.
    /// Irrelevant properties and classes have been omitted.
    /// </summary>
    public class GetVideoCaptionsResponse // Root object
    {
        public List<Action> actions { get; set; }
        public bool IsSuccessful { get; set; }
        public String Message { get; set; }

        /// <summary>
        /// Helper to get the name of the selected language used for this transcript.
        /// </summary>
        public String SelectedLanguage
        {
            get
            {
                return actions[0].updateEngagementPanelAction.content.
                    transcriptRenderer.content.transcriptSearchPanelRenderer.
                    footer.transcriptFooterRenderer.languageMenu.
                    sortFilterSubMenuRenderer.subMenuItems.
                    Where(p => p.selected == true).Select(p => p.title).FirstOrDefault();
            }
        }

        /// <summary>
        /// Helper that determines if there are no captions present for this video
        /// </summary>
        public bool HasNoCaptions
        {
            get 
            {
                return (actions == null ||
                    actions.Count == 0 ||
                    actions[0].updateEngagementPanelAction?.content?.transcriptRenderer?.content?.transcriptSearchPanelRenderer?.body?.transcriptSegmentListRenderer?.initialSegments == null ||
                    actions[0].updateEngagementPanelAction?.content?.transcriptRenderer?.content?.transcriptSearchPanelRenderer?.body?.transcriptSegmentListRenderer?.initialSegments.Count == 0 ||
                    actions[0].updateEngagementPanelAction.targetId != "engagement-panel-searchable-transcript");
            }
        }
    }

    /// <summary>
    /// Represents inputs in the UI for opening more options like Show Transcript
    /// </summary>
    public class Action
    {
        public UpdateEngagementPanelAction updateEngagementPanelAction { get; set; }
    }

    /// <summary>
    /// Contains the entire transcript body that you'd see in the UI, 
    /// excluding the language selector
    /// </summary>
    public class Body
    {
        public TranscriptSegmentListRenderer transcriptSegmentListRenderer { get; set; }
    }

    /// <summary>
    /// Not sure the exact purpose, but it houses the transcript.
    /// The targetId is "engagement-panel-transcript".
    /// </summary>
    public class UpdateEngagementPanelAction
    {
        public string targetId { get; set; }
        public Content content { get; set; }
    }

    /// <summary>
    /// Simply a parent object to house the transcript UI renderer
    /// </summary>
    public class Content
    {
        public TranscriptRenderer transcriptRenderer { get; set; }

        public TranscriptSearchPanelRenderer transcriptSearchPanelRenderer { get; set; }
    }

    /// <summary>
    /// Parent object containing the entire transcript box in the UI
    /// </summary>
    public class TranscriptRenderer
    {
        public Content content { get; set; }
    }

    /// <summary>
    /// Represents the entire transcript box in the UI
    /// </summary>
    public class TranscriptSearchPanelRenderer
    {
        public Body body { get; set; }
        public Footer footer { get; set; }
    }

    /// <summary>
    /// Represents the box that contains all the clickable lines of the transcript in the UI
    /// </summary>
    public class TranscriptSegmentListRenderer
    {
        public List<InitialSegment> initialSegments { get; set; }
    }

    /// <summary>
    /// Represents a segment of the video which gets rendered with a caption and timestamp
    /// </summary>
    public class InitialSegment
    {
        public TranscriptSegmentRenderer transcriptSegmentRenderer { get; set; }
    }

    /// <summary>
    /// Represents one line in the transcript, including the caption and the time
    /// </summary>
    public class TranscriptSegmentRenderer
    {
        public string startMs { get; set; }

        public CaptionSnippet snippet { get; set; }
    }

    /// <summary>
    /// Simple parent object to hold multiple runs.
    /// In reality it looks like only 1 run is ever really in this list.
    /// </summary>
    public class CaptionSnippet
    {
        public List<Run> runs { get; set; }
    }

    /// <summary>
    /// Represents the text that is displayed for the caption
    /// </summary>
    public class Run
    {
        public string text { get; set; }
    }

    /// <summary>
    /// Renderer of the transcript footer with the language picker
    /// </summary>
    public class TranscriptFooterRenderer
    {
        public LanguageMenu languageMenu { get; set; }
    }

    /// <summary>
    /// Represents the bottom of the UI where you can select the language
    /// </summary>
    public class Footer
    {
        public TranscriptFooterRenderer transcriptFooterRenderer { get; set; }
    }

    /// <summary>
    /// Represents the dropdown where users can pick the transcript language
    /// </summary>
    public class LanguageMenu
    {
        public SortFilterSubMenuRenderer sortFilterSubMenuRenderer { get; set; }
    }
    
    /// <summary>
    /// The language dropdown renderer
    /// </summary>
    public class SortFilterSubMenuRenderer
    {
        public List<SubMenuItem> subMenuItems { get; set; }
    }

    /// <summary>
    /// The language dropdown options
    /// </summary>
    public class SubMenuItem
    {
        /// <summary>
        /// The language, such as "English" or "English (auto-generated)"
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// If this is true, then this is the language currently used to render the captions.
        /// If this is not present, it just means the option is not selected. It doesn't appear as false.
        /// </summary>
        public bool? selected { get; set; }
    }
}
