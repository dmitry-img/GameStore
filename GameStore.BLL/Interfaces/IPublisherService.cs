using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.Publisher;

namespace GameStore.BLL.Interfaces
{
    public interface IPublisherService
    {
        Task<GetPublisherDTO> GetByCompanyNameAsync(string companyName);

        Task<IEnumerable<GetPublisherBriefDTO>> GetAllBriefAsync();

        Task CreateAsync(CreatePublisherDTO publisherDTO);
    }
}
