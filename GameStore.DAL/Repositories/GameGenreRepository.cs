using GameStore.DAL.Data;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Repositories
{
    public class GameGenreRepository : GenericRepository<GameGenre>, IGameGenreRepository
    {
        private GameStoreDbContext _context;

        public GameGenreRepository(GameStoreDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Game> GetGamesByGenre(int genreId)
        {
            return _context.GameGenres
                .Where(gg => gg.GenreId == genreId)
                .Select(gg => gg.Game);
        }
    }
}
