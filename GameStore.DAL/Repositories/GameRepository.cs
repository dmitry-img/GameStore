using GameStore.DAL.Data;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DAL.Repositories
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        private GameStoreDbContext _context;

        public GameRepository(GameStoreDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Game> GetByKeyAsync(Guid key) 
        {
            return await _context.Games.FirstOrDefaultAsync(g => g.Key == key);
        }
    }
}
