using DunkSearch.Domain.Models.ServiceModels.CaptionType;
using System;
using System.Linq;

namespace DunkSearch.Domain.Services
{
    public class CaptionTypeService : ICaptionTypeService
    {
        private DataContext _dataContext;
        public CaptionTypeService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public CaptionTypeListResponse ListCaptionTypes(CaptionTypeListRequest request)
        {
            var response = new CaptionTypeListResponse();
            response.IsSuccessful = false;

            try
            {
                response.CaptionTypes = _dataContext.CaptionTypes.ToList();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
