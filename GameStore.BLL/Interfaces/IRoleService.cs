using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Role;

namespace GameStore.BLL.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<GetRoleDTO>> GetAllAsync();

        Task<PaginationResult<GetRoleDTO>> GetAllWithPaginationAsync(PaginationDTO paginationDTO);

        Task<GetRoleDTO> GetByIdAsync(int id);

        Task CreateAsync(CreateRoleDTO createRoleDTO);

        Task UpdateAsync(int id, UpdateRoleDTO updateRoleDTO);

        Task DeleteAsync(int id);
    }
}
