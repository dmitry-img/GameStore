using GameStore.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace GameStore.DAL.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        Task<Game> GetByKeyAsync(Guid key);
    }
}
