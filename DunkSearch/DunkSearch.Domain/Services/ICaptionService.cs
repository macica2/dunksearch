using DunkSearch.Domain.Models.ServiceModels.Caption;
using System.Collections.Generic;

namespace DunkSearch.Domain.Services
{
    public interface ICaptionService
    {
        public CaptionListResponse ListCaptions(CaptionListRequest request);
        List<Entities.Caption> GetRandomCaptions(int videoId, int captionsToRetrieve);
    }
}
