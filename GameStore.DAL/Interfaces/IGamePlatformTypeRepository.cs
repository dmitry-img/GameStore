using GameStore.DAL.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Interfaces
{
    public interface IGamePlatformTypeRepository : IGenericRepository<GamePlatformType>
    {
        IEnumerable<Game> GetGamesByPlatformType(int platformTypeId);
    }
}
