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

        public async Task<Game> GetByKeyAsync(Guid key) 
        {
            return await _context.Games.FirstOrDefaultAsync(g => g.Key == key);
        }

        public async Task<Game> GetByKeyWithDetailsAsync(Guid key)
        {
            return await _context.Games
                .Include(g => g.Genres)
                .Include(g => g.PlatformTypes)
                .Include(g => g.Comments)
                .FirstOrDefaultAsync(g => g.Key == key);
        }

        public async Task<IEnumerable<Game>> GetGamesByGenre(int genreId)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == genreId);
            
            return genre.Games;
        }

        public async Task<IEnumerable<Game>> GetGamesByPlatformType(int platformTypeId)
        {
            var platformtype = await _context.PlatformTypes
                                        .FirstOrDefaultAsync(pt => pt.Id == platformTypeId);
            return platformtype.Games;
        }

       
    }
}
