using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Genre;

namespace GameStore.BLL.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GetGenreDTO>> GetAllAsync();

        Task<PaginationResult<GetGenreDTO>> GetAllWithPaginationAsync(PaginationDTO paginationDTO);

        Task CreateAsync(CreateGenreDTO createGenreDTO);

        Task UpdateAsync(int id, UpdateGenreDTO updateGenreDTO);

        Task DeleteAsync(int id);
    }
}
