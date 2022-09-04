using DunkSearch.Domain.Entities;
using DunkSearch.Domain.Models.ServiceModels.AppEventLogService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DunkSearch.Domain.Services
{
    public class AppEventLogService : IAppEventLogService
    {
        private DataContext _dataContext;
        public AppEventLogService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public AppEventLogCreateResponse CreateLog(AppEventLogCreateRequest request)
        {
            var response = new AppEventLogCreateResponse();
            try
            {
                var newLog = new AppEventLog()
                {
                    CreateDate = DateTime.Now,
                    EventType = request.EventType,
                    IPAddress = request.IPAddress,
                    EventDetails = request.EventDetails
                };
                _dataContext.Add(newLog);
                _dataContext.SaveChanges();
                response.IsSuccessful = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
