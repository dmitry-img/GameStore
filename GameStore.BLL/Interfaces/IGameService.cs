using GameStore.BLL.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface IGameService
    {
        Task CreateAsync(GameDTO gameDTO);
        Task UpdateAsync(GameDTO gameDTO);
        Task DeleteAsync(int id);
        Task<GameDTO> GetByKeyAsync(Guid key);
        IEnumerable<GameDTO> GetAll();
        IEnumerable<GameDTO> GetAllByGenre(int genreId);
        IEnumerable<GameDTO> GetAllByPlatformType(int platformTypeId);

    }
}
