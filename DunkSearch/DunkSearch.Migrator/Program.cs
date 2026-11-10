using DunkSearch.Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace DunkSearch.Migrator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize config file and DB context and local DB path
            var configuration = GetConfiguration();
            var dataContext = GetDataContext(configuration);
            var sqliteDbPath = configuration["SQLiteDbPath"];

            MigrateChannels(sqliteDbPath, dataContext);
            MigrateCaptionTypes(sqliteDbPath, dataContext);
            MigrateVideos(sqliteDbPath, dataContext);
            MigrateCaptions(sqliteDbPath, dataContext);
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

        private static void CreateTables(String sqliteDbPath)
        {
            // TODO idk if it's worth doing it here, or just keep it in DB Browser.
        }

        private static void MigrateChannels(String sqliteDbPath, DataContext dataContext)
        {
            var channels = dataContext.Channels.ToList();

            using var sqliteConnection = new SqliteConnection("Data Source=" + sqliteDbPath);
            sqliteConnection.Open();

            using (var transaction = sqliteConnection.BeginTransaction())
            {
                var insert = sqliteConnection.CreateCommand();
                insert.CommandText = "INSERT INTO channel (channel_id, name) VALUES ($channel_id, $name)";
                var channel_id = insert.Parameters.Add("$channel_id", SqliteType.Integer);
                var name = insert.Parameters.Add("$name", SqliteType.Text);
                foreach (var channel in channels)
                {
                    channel_id.Value = channel.ChannelId; 
                    name.Value = channel.Name; 
                    insert.ExecuteNonQuery();
                }
                transaction.Commit();
            }

            sqliteConnection.Close();
        }

        private static void MigrateCaptionTypes(String sqliteDbPath, DataContext dataContext)
        {
            var captionTypes = dataContext.CaptionTypes.ToList();

            using var sqliteConnection = new SqliteConnection("Data Source=" + sqliteDbPath);
            sqliteConnection.Open();

            using (var transaction = sqliteConnection.BeginTransaction())
            {
                var insert = sqliteConnection.CreateCommand();
                insert.CommandText = "INSERT INTO caption_type (caption_type_id, name) VALUES ($caption_type_id, $name)";
                var caption_type_id = insert.Parameters.Add("caption_type_id", SqliteType.Integer);
                var name = insert.Parameters.Add("$name", SqliteType.Text);
                foreach (var captionType in captionTypes)
                {
                    caption_type_id.Value = captionType.CaptionTypeId;
                    name.Value = captionType.Name;
                    insert.ExecuteNonQuery();
                }
                transaction.Commit();
            }

            sqliteConnection.Close();
        }

        private static void MigrateVideos(String sqliteDbPath, DataContext dataContext)
        {
            var videos = dataContext.Videos.ToList();

            using var sqliteConnection = new SqliteConnection("Data Source=" + sqliteDbPath);
            sqliteConnection.Open();

            using (var transaction = sqliteConnection.BeginTransaction())
            {
                var insert = sqliteConnection.CreateCommand();
                insert.CommandText = "INSERT INTO video (video_id, channel_id, title, yt_video_id, upload_date) VALUES ($video_id, $channel_id, $title, $yt_video_id, $upload_date)";
                var video_id = insert.Parameters.Add("video_id", SqliteType.Integer);
                var channel_id = insert.Parameters.Add("channel_id", SqliteType.Integer);
                var title = insert.Parameters.Add("title", SqliteType.Text);
                var yt_video_id = insert.Parameters.Add("yt_video_id", SqliteType.Text);
                var upload_date = insert.Parameters.Add("upload_date", SqliteType.Integer);
                foreach (var video in videos)
                {
                    video_id.Value = video.VideoId;
                    channel_id.Value = video.ChannelId;
                    title.Value = video.Title;
                    yt_video_id.Value = video.YTVideoId;
                    upload_date.Value = ((DateTimeOffset)video.UploadDate).ToUnixTimeSeconds();
                    insert.ExecuteNonQuery();
                }
                transaction.Commit();
            }

            sqliteConnection.Close();
        }

        private static void MigrateCaptions(String sqliteDbPath, DataContext dataContext)
        {
            var captions = dataContext.Captions.ToList();
            using var sqliteConnection = new SqliteConnection("Data Source=" + sqliteDbPath);
            sqliteConnection.Open();

            // we'll do batches of 1000 since there are hundreds of thousands of captions
            int index = 0;
            int batchSize = 1000;
            while (index < captions.Count)
            {
                using (var transaction = sqliteConnection.BeginTransaction())
                {
                    var insert = sqliteConnection.CreateCommand();
                    insert.CommandText = "INSERT INTO caption (caption_id, video_id, caption_type_id, start_seconds, caption_text) VALUES ($caption_id, $video_id, $caption_type_id, $start_seconds, $caption_text)";
                    var caption_id = insert.Parameters.Add("caption_id", SqliteType.Integer);
                    var video_id = insert.Parameters.Add("video_id", SqliteType.Integer);
                    var caption_type_id = insert.Parameters.Add("caption_type_id", SqliteType.Integer);
                    var start_seconds = insert.Parameters.Add("start_seconds", SqliteType.Integer);
                    var caption_text = insert.Parameters.Add("caption_text", SqliteType.Text);
                    for (int i = index; i < captions.Count && i < index + batchSize; i++)
                    {
                        var caption = captions[i];
                        caption_id.Value = caption.CaptionId;
                        video_id.Value = caption.VideoId;
                        caption_type_id.Value = caption.CaptionTypeId;
                        start_seconds.Value = caption.StartSeconds;
                        caption_text.Value = caption.CaptionText;
                        insert.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    index += batchSize;
                    Console.WriteLine("At index " + index + " / " + captions.Count);
                }
            }

            sqliteConnection.Close();
        }
    }
}
