using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.PlatformType;

namespace GameStore.BLL.Interfaces
{
    public interface IPlatformTypeService
    {
        Task<IEnumerable<GetPlatformTypeDTO>> GetAllAsync();

        Task CreateAsync(CreatePlatformTypeDTO createPlatformTypeDTO);

        Task UpdateAsync(int id, UpdatePlatformTypeDTO updatePlatformTypeDTO);

        Task DeleteAsync(int id);
    }
}
