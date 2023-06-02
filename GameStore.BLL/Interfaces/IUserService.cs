using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.User;

namespace GameStore.BLL.Interfaces
{
    public interface IUserService
    {
        Task<PaginationResult<GetUserDTO>> GetAllWithPaginationAsync(PaginationDTO paginationDTO);

        Task<GetUserDTO> GetByIdAsync(string id);

        Task CreateAsync(CreateUserDTO createUserDTO);

        Task UpdateAsync(string id, UpdateUserDTO updateUserDTO);

        Task DeleteAsync(string id);
    }
}
