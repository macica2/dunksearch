using DunkSearch.Domain;
using DunkSearch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DunkSearch.Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Importer starting...");

            // Initialize config file and DB context
            var configuration = GetConfiguration();
            var dataContext = GetDataContext(configuration);
            
            // Load channel and file paths based on config file
            var channel = GetChannel(configuration, dataContext);
            var filePaths = GetFilePaths(configuration);

            // Load CaptionTypes up front for reuse
            var captionTypes = dataContext.CaptionTypes.ToList();

            // Loop through each input file
            foreach (var filePath in filePaths)
            {
                ProcessFile(filePath, dataContext, captionTypes, channel);
            }

            // If anything fails, log it
            // TODO

            // After it finishes, print out if it's done and how many were successful TODO
            Console.WriteLine("Done! Press enter to close.");
            Console.ReadLine();
        }

        /// <summary>
        /// Initializes and returns configration based on appsettings
        /// </summary>
        /// <returns></returns>
        private static IConfigurationRoot GetConfiguration()
        {
            var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            // Initialize configurations and data context
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");

            if (environmentName == "Development")
            {
                builder.AddJsonFile("appsettings.Development.json");
            }

            var configuration = builder.Build();
            return configuration;
        }

        /// <summary>
        /// Initializes and returns data context based on the DefaultConnection string
        /// in the configuration file
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static DataContext GetDataContext(IConfigurationRoot configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            var dataContext = new DataContext(optionsBuilder.Options);
            return dataContext;
        }

        /// <summary>
        /// Gets the channel from the DB using the name.
        /// If channel is not found, don't automatically create it, since
        /// this helps prevent typos/bad data. Assume admin creates channel manually.
        /// </summary>
        /// <returns></returns>
        private static Channel GetChannel(IConfigurationRoot configuration, DataContext dataContext)
        {
            var channelName = configuration["ChannelName"];
            // Load the channel ID from DB
            var channel = dataContext.Channels.Where(p => p.Name == channelName).FirstOrDefault();
            if (channel == null)
            {
                Console.WriteLine("Error: channel " + channelName + " not found.");
                Console.ReadLine();
            }
            return channel;
        }

        /// <summary>
        /// Gets the array of file paths based on the folder in the configuration file
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static string[] GetFilePaths(IConfigurationRoot configuration)
        {
            // Read the input variables
            var inputFolderPath = configuration["InputFolderPath"];

            // Load the input files
            if (!Directory.Exists(inputFolderPath))
            {
                Console.WriteLine("Input path not found");
                Console.ReadLine();
            }
            var filePaths = Directory.GetFiles(inputFolderPath);
            return filePaths;
        }

        /// <summary>
        /// Reads a video extract input file and inputs the data into the database
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dataContext"></param>
        /// <param name="captionTypes"></param>
        /// <param name="channel"></param>
        private static void ProcessFile(string filePath, DataContext dataContext, List<CaptionType> captionTypes, Channel channel)
        {
            Console.WriteLine("Processing file " + filePath);
            string jsonFileContent = File.ReadAllText(filePath);
            var videoExtract = JsonSerializer.Deserialize<VideoExtract>(jsonFileContent);

            // Grab the caption type for this extract's language
            var captionTypeId = captionTypes.Where(p => p.Name == videoExtract.CaptionLanguage).Select(p => p.CaptionTypeId).FirstOrDefault();
            if (captionTypeId == 0)
            {
                Console.WriteLine("Error: CaptionType for language " + videoExtract.CaptionLanguage + " not found.");
                Console.ReadLine();
            }

            var video = GetVideo(dataContext, videoExtract, channel, captionTypeId);
            SaveCaptions(dataContext, video, videoExtract, captionTypeId);
        }

        /// <summary>
        /// Finds the existing video or creates one if it doesn't exist.
        /// If an existing video was found, it also clears existing captions for the video.
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="videoExtract"></param>
        /// <param name="channel"></param>
        /// <param name="captionTypeId"></param>
        /// <returns></returns>
        private static Video GetVideo(DataContext dataContext, VideoExtract videoExtract, Channel channel, int captionTypeId)
        {
            // check if a video with this title and date already exists.
            var video = dataContext.Videos.Where(p => p.Title == videoExtract.Title && p.UploadDate == videoExtract.UploadDateAsDate).FirstOrDefault();
            if (video == null)
            {
                // create video
                video = new Video()
                {
                    ChannelId = channel.ChannelId,
                    Title = videoExtract.Title,
                    UploadDate = videoExtract.UploadDateAsDate,
                    YTVideoId = videoExtract.Id
                };
                dataContext.Add(video);
                dataContext.SaveChanges();
            }
            else
            {
                // find existing captions for this video and caption language,
                // then and delete them so we can use the latest captions instead
                var oldCaptions = dataContext.Captions.Where(p => p.VideoId == video.VideoId && p.CaptionTypeId == captionTypeId);
                dataContext.RemoveRange(oldCaptions);
                dataContext.SaveChanges();
            }

            return video;
        }

        /// <summary>
        /// Creates caption records based on the provided video extract.
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="video"></param>
        /// <param name="videoExtract"></param>
        /// <param name="captionTypeId"></param>
        private static void SaveCaptions(DataContext dataContext, Video video, VideoExtract videoExtract, int captionTypeId)
        {
            // add the caption records for each line in the input
            var captionsToAdd = new List<Caption>();
            foreach (var videoExtractCaption in videoExtract.Transcript)
            {
                captionsToAdd.Add(new Caption()
                {
                    VideoId = video.VideoId,
                    CaptionTypeId = captionTypeId,
                    StartSeconds = videoExtractCaption.StartSeconds,
                    CaptionText = videoExtractCaption.Caption
                });
            }
            dataContext.AddRange(captionsToAdd);
            dataContext.SaveChanges();
        }
    }
}
