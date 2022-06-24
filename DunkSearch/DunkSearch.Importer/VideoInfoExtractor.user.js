// ==UserScript==
// @name         DunkSearch Video Extractor
// @namespace    http://tampermonkey.net/
// @version      0.1
// @description  Extracts the video information, including transcripts, when a video is opened
// @author       https://github.com/macica2
// @match        https://www.youtube.com/watch?v=*
// @icon         https://www.google.com/s2/favicons?sz=64&domain=youtube.com
// @grant        none
// ==/UserScript==

(function ()
{
    /* To run this manually, open up Google Chrome or Firefox and open up a video.
     * For example, https://www.youtube.com/watch?v=kUKinGYoeDA
     * Then click F12 to open the dev console. Switch to the Console tab.
     * Copy everything below inside the first setTimeout block of code
     * and paste it into the console, then hit enter.
     * The code should run, print out the JSON to the console, and save a file to the downloads folder.
     */
    'use strict';
    console.log('going to sleep while page loads');
    setTimeout(function ()
    {
        console.log('Page loaded');
        // opens the menu which includes the Show Transcripts button.
        // If YouTube doesn't show the clip button, then the position changes
        if (document.getElementById("info-contents").querySelectorAll("yt-icon-button").length >= 6)
        {
            document.getElementById("info-contents").querySelectorAll("yt-icon-button")[5].click();
        }
        else
        {
            document.getElementById("info-contents").querySelectorAll("yt-icon-button")[4].click();
        }
        // sleep for some time while we wait for the menu to show
        setTimeout(function ()
        {
            // clicks the Show transcript button
            document.getElementsByClassName("style-scope ytd-menu-popup-renderer")[2].click();
            // sleep again while we wait for the 2nd menu to show up
            setTimeout(function ()
            {
                // Now that the transcript is showing, we can get the timestamps if transcript is showing
                var timestamps = document.getElementsByClassName("segment-timestamp style-scope ytd-transcript-segment-renderer");
                var captions = document.getElementsByClassName("segment-text style-scope ytd-transcript-segment-renderer");
                var captionLanguage = document.getElementById("label-text").innerText;

                // Grab other general information about the video
                var vidId = window.location.href.split("?v=")[1].split("&")[0];
                var vidTitle = document.getElementsByClassName("title style-scope ytd-video-primary-info-renderer")[0].innerText;
                var vidDate = document.getElementById("info-strings").children[1].innerText;

                //var captionsOutput = "";
                var transcript = [];
                for (var i = 0; i < captions.length; i++)
                {
                    var curTimestamp = timestamps[i].innerText;
                    var curCaption = captions[i].innerText;
                    transcript.push({ 'Timestamp': curTimestamp, 'Caption': curCaption });
                    //captionsOutput += curTimestamp + "\n" + curCaption;
                    //if (i != (captions.length - 1))
                    //{
                    //    // if this isn't the last caption, add a new line to separate this caption from the next timestamp
                    //    captionsOutput += "\n";
                    //}
                }
                var jsonInfo = {
                    Id: vidId,
                    Title: vidTitle,
                    UploadDate: vidDate,
                    CaptionLanguage: captionLanguage,
                    Transcript: transcript
                    //Transcript: captionsOutput
                };
                var jsonStr = JSON.stringify(jsonInfo, null, 2);
                console.log(jsonStr);

                // Download the file
                var fileName = vidTitle.replace(':', '_') // remove invalid file name characters
                    .replace('/', '_').replace('\\', '_').replace('*', '_')
                    .replace('?', '_').replace('|', '_').replace('>', '_')
                    .replace('<', '_').replace('"', '_');
                var a = document.createElement("a");
                var file = new Blob([jsonStr], { type: 'txt/json' });
                a.href = URL.createObjectURL(file);
                a.download = fileName + '.json';
                a.click();
            }, 1000);
        }, 1000);
    }, 3000);
})();