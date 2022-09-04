using DunkSearch.Domain.Models.ServiceModels.AppEventLogService;

namespace DunkSearch.Domain.Services
{
    public interface IAppEventLogService
    {
        AppEventLogCreateResponse CreateLog(AppEventLogCreateRequest request);
    }
}
