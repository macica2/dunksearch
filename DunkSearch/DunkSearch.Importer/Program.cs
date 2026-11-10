using DunkSearch.Domain;
using DunkSearch.Domain.Entities;
using Microsoft.Data.Sqlite;
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
            var sqliteDbPath = configuration["SQLiteDbPath"];
            using var sqliteConnection = new SqliteConnection("Data Source=" + sqliteDbPath);
            sqliteConnection.Open();

            //var dataContext = GetDataContext(configuration);

            // Load channel and file paths based on config file
            var channels = GetChannels(configuration, sqliteConnection);
            var filePaths = GetFilePaths(configuration);

            // Load CaptionTypes up front for reuse
            var captionTypes = GetCaptionTypes(sqliteConnection);

            // Loop through each input file
            foreach (var filePath in filePaths)
            {
                ProcessFile(filePath, sqliteConnection, captionTypes, channels);
            }

            // If anything fails, log it
            // TODO

            // After it finishes, print out if it's done and how many were successful TODO
            sqliteConnection.Close();
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
        /// Gets the channels from the DB, and optionally gets just one based on input file.
        /// If channel is not found, don't automatically create it, since
        /// this helps prevent typos/bad data. Assume admin creates channel manually.
        /// </summary>
        /// <returns></returns>
        private static List<Channel> GetChannels(IConfigurationRoot configuration, SqliteConnection connection)
        {
            var select = connection.CreateCommand();
            select.CommandText = "SELECT channel_id, name FROM channel";
            // If the app settings config file provided a ChannelName, then load that channel instead.
            // This supports the legacy JSON files that don't include the channel name,
            // in case we ever need to import those again.
            var channelName = configuration["ChannelName"];
            if (!string.IsNullOrEmpty(channelName))
            {
                select.CommandText += " WHERE name = $channelName";
                select.Parameters.AddWithValue("$channelName", channelName);
            }

            var channels = new List<Channel>();
            using (var reader = select.ExecuteReader())
            {
                while (reader.Read())
                {
                    channels.Add(new Channel() { ChannelId = reader.GetInt32(0), Name = reader.GetString(1) });
                }
            }

            return channels;
        }

        /// <summary>
        /// Gets the channels from the DB, and optionally gets just one based on input file.
        /// If channel is not found, don't automatically create it, since
        /// this helps prevent typos/bad data. Assume admin creates channel manually.
        /// </summary>
        /// <returns></returns>
        private static List<CaptionType> GetCaptionTypes(SqliteConnection connection)
        {
            var select = connection.CreateCommand();
            select.CommandText = "SELECT caption_type_id, name FROM caption_type";

            var captionTypes = new List<CaptionType>();
            using (var reader = select.ExecuteReader())
            {
                while (reader.Read())
                {
                    captionTypes.Add(new CaptionType() { CaptionTypeId = reader.GetInt32(0), Name = reader.GetString(1) });
                }
            }

            return captionTypes;
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
        private static void ProcessFile(string filePath, SqliteConnection connection, List<CaptionType> captionTypes, List<Channel> channels)
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

            // Grab the channel based on the name in the JSON file.
            // If no channel was specified, use the first channel in the passed in list, meaning
            // we are using the one in the app settings file.
            Channel channel = null;
            if (!string.IsNullOrEmpty(videoExtract.Channel))
            {
                channel = channels.Where(p => p.Name == videoExtract.Channel).First();
            }
            else
            {
                channel = channels[0];
            }

            var video = GetVideo(connection, videoExtract, channel, captionTypeId);
            SaveCaptions(connection, video, videoExtract, captionTypeId);
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
        private static Video GetVideo(SqliteConnection connection, VideoExtract videoExtract, Channel channel, int captionTypeId)
        {
            // TODO might be a slight issue here with daylight savings time if you run it for a video created a while back...
            // but odds of it being an issue are very low so ignore until it happens.
            var unixSeconds = ((DateTimeOffset)videoExtract.UploadDateAsDate).ToUnixTimeSeconds();

            // check if a video with this title and date already exists.
            var select = connection.CreateCommand();
            select.CommandText = "SELECT video_id, title FROM video WHERE title = $title AND upload_date = $uploadDate";
            select.Parameters.AddWithValue("$title", videoExtract.Title);
            select.Parameters.AddWithValue("$uploadDate", unixSeconds);
            var videos = new List<Video>();
            using (var reader = select.ExecuteReader())
            {
                while (reader.Read())
                {
                    videos.Add(new Video() { VideoId = reader.GetInt32(0), Title = reader.GetString(1) });
                }
            }

            if (!videos.Any())
            {
                var insert = connection.CreateCommand();
                insert.CommandText = "INSERT INTO video (channel_id, title, yt_video_id, upload_date) VALUES ($channel_id, $title, $yt_video_id, $upload_date)";
                var channel_id = insert.Parameters.Add("channel_id", SqliteType.Integer);
                channel_id.Value = channel.ChannelId;
                var title = insert.Parameters.Add("title", SqliteType.Text);
                title.Value = videoExtract.Title;
                var yt_video_id = insert.Parameters.Add("yt_video_id", SqliteType.Text);
                yt_video_id.Value = videoExtract.Id;
                var upload_date = insert.Parameters.Add("upload_date", SqliteType.Integer);
                upload_date.Value = unixSeconds;
                insert.ExecuteNonQuery();

                var lastId = connection.CreateCommand();
                lastId.CommandText = "SELECT last_insert_rowid()";

                return new Video()
                {
                    VideoId = Convert.ToInt32(lastId.ExecuteScalar()),
                    ChannelId = channel.ChannelId,
                    Title = videoExtract.Title,
                    UploadDate = videoExtract.UploadDateAsDate,
                    YTVideoId = videoExtract.Id
                };
            }
            else
            {
                // find existing captions for this video and caption language,
                // then and delete them so we can use the latest captions instead
                var delete = connection.CreateCommand();
                delete.CommandText = "DELETE FROM caption WHERE video_id = $videoId AND caption_type_id = $captionTypeId";
                delete.Parameters.AddWithValue("$videoId", videos.First().VideoId);
                delete.Parameters.AddWithValue("$captionTypeId", captionTypeId);
                delete.ExecuteNonQuery();

                return videos.First();
            }
        }

        /// <summary>
        /// Creates caption records based on the provided video extract.
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="video"></param>
        /// <param name="videoExtract"></param>
        /// <param name="captionTypeId"></param>
        private static void SaveCaptions(SqliteConnection connection, Video video, VideoExtract videoExtract, int captionTypeId)
        {
            // add the caption records for each line in the input
            using (var transaction = connection.BeginTransaction())
            {
                var insert = connection.CreateCommand();
                insert.CommandText = "INSERT INTO caption (video_id, caption_type_id, start_seconds, caption_text) VALUES ($video_id, $caption_type_id, $start_seconds, $caption_text)";
                var caption_id = insert.Parameters.Add("caption_id", SqliteType.Integer);
                var video_id = insert.Parameters.Add("video_id", SqliteType.Integer);
                var caption_type_id = insert.Parameters.Add("caption_type_id", SqliteType.Integer);
                var start_seconds = insert.Parameters.Add("start_seconds", SqliteType.Integer);
                var caption_text = insert.Parameters.Add("caption_text", SqliteType.Text);
                foreach (var videoExtractCaption in videoExtract.Transcript)
                {
                    video_id.Value = video.VideoId;
                    caption_type_id.Value = captionTypeId;
                    start_seconds.Value = videoExtractCaption.StartSeconds;
                    caption_text.Value = videoExtractCaption.Caption;
                    insert.ExecuteNonQuery();
                }
                transaction.Commit();
            }
        }
    }
}
