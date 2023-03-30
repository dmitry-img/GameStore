using GameStore.DAL.Data;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Repositories
{
    public class GamePlatformTypeRepository 
        : GenericRepository<GamePlatformType>, IGamePlatformTypeRepository
    {
        private GameStoreDbContext _context;
        public GamePlatformTypeRepository(GameStoreDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Game> GetGamesByPlatformType(int platformTypeId)
        {
            return _context.GamePlatformTypes
               .Where(gg => gg.PlatformTypeId == platformTypeId)
               .Select(gg => gg.Game);
        }
    }
}
