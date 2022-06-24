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
            var query = _dataContext.Captions.Include("Video")
                            .Where(p => p.CaptionTextVector.Matches(EF.Functions.PlainToTsQuery("english", request.SearchTerm)));
            if (request.ChannelId != null)
            {
                query = query.Where(p => p.Video.ChannelId == request.ChannelId);
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
                                .ThenBy(p => p.StartSeconds)
                                .Skip((request.PageNumber - 1) * request.PageSize)
                                .Take(request.PageSize)
                                .ToList();
            response.IsSuccessful = true;
            return response;
        }
    }
}
