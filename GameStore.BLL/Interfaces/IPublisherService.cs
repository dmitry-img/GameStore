using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Publisher;

namespace GameStore.BLL.Interfaces
{
    public interface IPublisherService
    {
        Task<GetPublisherDTO> GetByCompanyNameAsync(string companyName);

        Task<PaginationResult<GetPublisherBriefDTO>> GetAllBriefWithPaginationAsync(PaginationDTO paginationDTO);

        Task<IEnumerable<GetPublisherBriefDTO>> GetAllBriefAsync();

        Task CreateAsync(CreatePublisherDTO createPublisherDTO);

        Task UpdateAsync(string companyName, UpdatePublisherDTO updatePublisherDTO);

        Task DeleteAsync(int id);

        Task<bool> IsUserAssociatedWithPublisherAsync(string userObjectId, string companyName);

        Task<bool> IsGameAssociatedWithPublisherAsync(string userObjectId, string gameKey);

        Task<string> GetCurrentCompanyNameAsync(string userObjectId);

        Task<IEnumerable<string>> GetFreePublisherUsernamesAsync();
    }
}
