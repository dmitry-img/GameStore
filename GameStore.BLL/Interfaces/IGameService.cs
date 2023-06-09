using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Game;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface IGameService
    {
        Task CreateAsync(CreateGameDTO gameDTO);

        Task UpdateAsync(string key, UpdateGameDTO gameDTO);

        Task DeleteAsync(string key);

        Task<GetGameDTO> GetByKeyAsync(string key);

        int GetCount();

        Task<IEnumerable<GetGameBriefDTO>> GetAllAsync();

        Task<PaginationResult<GetGameBriefDTO>> GetAllWithPaginationAsync(PaginationDTO paginationDTO);

        Task<PaginationResult<GetGameBriefDTO>> GetFilteredAsync(FilterGameDTO filter);

        Task<IEnumerable<GetGameBriefDTO>> GetAllByGenreAsync(int genreId);

        Task<IEnumerable<GetGameBriefDTO>> GetAllByPlatformTypeAsync(int platformTypeId);

        Task<MemoryStream> GetGameFileAsync(string gameKey);

        Task<PaginationResult<GetGameBriefDTO>> GetPublisherGamesWithPaginationAsync(PaginationDTO paginationDTO);
    }
}
