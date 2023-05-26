using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Pipelines.GameOperations
{
    public class GenreOperation : IOperation<IQueryable<Game>>
    {
        private readonly List<int> _genres;

        public GenreOperation(List<int> genres)
        {
            _genres = genres;
        }

        public IQueryable<Game> Invoke(IQueryable<Game> games)
        {
            if (_genres == null || !_genres.Any())
            {
                return games;
            }

            List<int> filterIds = _genres
                .Where(id => games.SelectMany(g => g.Genres).Any(g => g.Id == id && (g.ParentGenre != null || !g.ChildGenres.Any())))
                .ToList();

            return games.Where(game => game.Genres.Any(g => filterIds.Contains(g.Id)));
        }
    }
}
