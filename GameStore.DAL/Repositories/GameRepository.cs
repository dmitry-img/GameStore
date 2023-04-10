using GameStore.DAL.Data;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task<Game> GetByKeyAsync(string key) 
        {
            return await _context.Games
                .FirstOrDefaultAsync(g => g.Key == key && g.IsDeleted == false);
        }

        public async Task<IEnumerable<Game>> GetGamesByGenre(int genreId)
        {
            var genre = await _context.Genres
                .Include(g => g.Games)
                .FirstOrDefaultAsync(g => g.Id == genreId && g.IsDeleted == false);
            
            return genre.Games;
        }

        public async Task<IEnumerable<Game>> GetGamesByPlatformType(int platformTypeId)
        {
            var platformtype = await _context.PlatformTypes
                .Include(pt => pt.Games)
                .FirstOrDefaultAsync(pt => pt.Id == platformTypeId && pt.IsDeleted == false);
            return platformtype.Games;
        }
    }
}
