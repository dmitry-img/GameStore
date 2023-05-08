using GameStore.DAL.Data;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace GameStore.DAL.Repositories
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        private readonly GameStoreDbContext _context;

        public GameRepository(GameStoreDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Game> GetByKeyAsync(string key)
        {
            var game = await _context.Games
                .Include(g => g.Genres)
                .Include(g => g.PlatformTypes)
                .Include(g => g.Publisher)
                .FirstOrDefaultAsync(g => g.Key == key && !g.IsDeleted);

            return game;
        }

        public async Task<IEnumerable<Game>> GetGamesByGenreAsync(int genreId)
        {
            var genre = await _context.Genres
                .Include(g => g.Games)
                .FirstOrDefaultAsync(g => g.Id == genreId && !g.IsDeleted);

            return genre.Games;
        }

        public async Task<IEnumerable<Game>> GetGamesByPlatformTypeAsync(int platformTypeId)
        {
            var platformtype = await _context.PlatformTypes
                .Include(pt => pt.Games)
                .FirstOrDefaultAsync(pt => pt.Id == platformTypeId && !pt.IsDeleted);

            return platformtype.Games;
        }
    }
}
