using GameStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStore.DAL.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        Task<Game> GetByKeyAsync(Guid key);
        Task<Game> GetByKeyWithDetailsAsync(Guid key);
        Task<IEnumerable<Game>> GetGamesByGenre(int genreId);
        Task<IEnumerable<Game>> GetGamesByPlatformType(int platformTypeId);
    }
}
