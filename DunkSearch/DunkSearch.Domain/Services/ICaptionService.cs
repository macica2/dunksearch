using DunkSearch.Domain.Models.ServiceModels.Caption;

namespace DunkSearch.Domain.Services
{
    public interface ICaptionService
    {
        public CaptionListResponse ListCaptions(CaptionListRequest request);
    }
}
