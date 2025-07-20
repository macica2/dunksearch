using DunkSearch.Domain;
using DunkSearch.Domain.Entities;
using DunkSearch.Domain.Models.DataModels;
using DunkSearch.Domain.Models.ServiceModels.AutomatedVideoProcessor;
using DunkSearch.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DunkSearch.AutomatedVideoProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get config and DB context
            var configuration = GetConfiguration();
            var dataContext = GetDataContext(configuration);

            // Call the service which does all the work to check for
            // new videos and process their captions.
            var service = new AutomatedVideoProcessorService(dataContext);
            service.StartProcess(new AutomatedVideoProcessorRequest()
            {
                GmailAddress = configuration["GmailAddress"].ToString(),
                YouTubeAPIKey = configuration["YouTubeAPIKey"].ToString(),
                SmtpAppPassword = configuration["SmtpAppPassword"].ToString()
            });            
        }

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
    }
}
