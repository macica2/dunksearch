using DunkSearch.Domain.Models.ServiceModels.CaptionType;

namespace DunkSearch.Domain.Services
{
    public interface ICaptionTypeService
    {
        CaptionTypeListResponse ListCaptionTypes(CaptionTypeListRequest request);
    }
}
