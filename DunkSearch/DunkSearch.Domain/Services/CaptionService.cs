using DunkSearch.Domain.Entities;
using DunkSearch.Domain.Models.ServiceModels.Caption;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DunkSearch.Domain.Services
{
    public class CaptionService : ICaptionService
    {
        private DataContext _dataContext;
        public CaptionService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public CaptionListResponse ListCaptions(CaptionListRequest request)
        {
            // Log this search attempt
            try
            {
                var searchLog = new AppEventLog()
                {
                    CreateDate = DateTime.Now,
                    EventType = "Search",
                    IPAddress = request.IPAddress,
                    EventDetails = request.SearchTerm + " | " + request.PageNumber
                };
                _dataContext.Add(searchLog);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // do nothing for now
            }

            //var pageSize = request.PageSize pass this in? and page number.
            var response = new CaptionListResponse()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            if (string.IsNullOrEmpty(request.SearchTerm))
            {
                response.IsSuccessful = false;
                response.Message = "Search term cannot be empty";
            }

            //
            // Build query starting with filters
            //
            var query = _dataContext.Captions.Include("Video");

            // some people leave spaces in the beginning of their search somehow, so trim both sides to be safe
            request.SearchTerm = request.SearchTerm.Trim();

            // If the seach term was wrapped in quotes like "This", then serach for the exact string,
            // otherwise use the FTS Vector for a faster search
            if (request.SearchTerm.StartsWith('\"') && request.SearchTerm.EndsWith('\"'))
            {
                var sanitizedSearchTerm = request.SearchTerm.Substring(1, request.SearchTerm.Length - 2); // remove the beginning and end quotes
                sanitizedSearchTerm = sanitizedSearchTerm.ToLower();
                sanitizedSearchTerm = sanitizedSearchTerm.Replace("[ __ ]", "[ __ ]"); // Censored swears are actually stored with ascii char 160, not space which is char 32
                query = query.Where(p => p.CaptionText.ToLower().Contains(sanitizedSearchTerm));

            }
            else
            {
                if (request.SearchMode == "Fuzzy")
                {
                    query = query.Where(p => p.CaptionTextVector.Matches(EF.Functions.PhraseToTsQuery("english", request.SearchTerm)));
                }
                else
                {
                    query = query.Where(p => p.CaptionTextSimpleVector.Matches(EF.Functions.PlainToTsQuery("simple", request.SearchTerm)));
                }
            }
            if (request.ChannelIds != null && request.ChannelIds.Count > 0)
            {
                query = query.Where(p => request.ChannelIds.Contains(p.Video.ChannelId));
            }
            if (request.CaptionTypeId != null)
            {
                query = query.Where(p => p.CaptionTypeId == request.CaptionTypeId);
            }
            if (request.VideoId != null)
            {
                query = query.Where(p => p.VideoId == request.VideoId);
            }
            if (request.UploadDateFrom != null)
            {
                query = query.Where(p => p.Video.UploadDate >= request.UploadDateFrom);
            }
            if (request.UploadDateTo != null)
            {
                query = query.Where(p => p.Video.UploadDate <= request.UploadDateTo);
            }

            //
            // Apply sorting and paging
            //     
            response.Captions = query.OrderBy(p => p.Video.UploadDate)
                                .ThenBy(p => p.Video.Title)
                                .ThenBy(p => p.StartSeconds)                                
                                .Skip((request.PageNumber - 1) * request.PageSize)
                                .Take(request.PageSize)
                                .ToList();
            response.IsSuccessful = true;
            return response;
        }
    }
}
