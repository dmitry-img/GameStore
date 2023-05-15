using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Pipelines
{
    public class GameFilterOperations : IGameFilterOperations
    {
        public IOperation<IQueryable<Game>> CreateGenreOperation(List<int> genres)
        {
            return new Operation<IQueryable<Game>>(games => genres != null && genres.Any() ? games.Where(game =>
                game.Genres.Any(g => genres.Contains(g.Id))) : games);
        }

        public IOperation<IQueryable<Game>> CreatePlatformOperation(List<int> platforms)
        {
            return new Operation<IQueryable<Game>>(games => platforms != null && platforms.Any() ? games.Where(game =>
                game.PlatformTypes.Any(p => platforms.Contains(p.Id))) : games);
        }

        public IOperation<IQueryable<Game>> CreatePublisherOperation(List<int> publishers)
        {
            return new Operation<IQueryable<Game>>(games => publishers != null && publishers.Any() ? games.Where(game =>
                publishers.Contains(game.PublisherId)) : games);
        }

        public IOperation<IQueryable<Game>> CreatePriceOperation(decimal? priceFrom, decimal? priceTo)
        {
            return new Operation<IQueryable<Game>>(games =>
            {
                if (priceFrom != null)
                {
                    games = games.Where(game => game.Price >= priceFrom);
                }

                if (priceTo != null)
                {
                    games = games.Where(game => game.Price <= priceTo);
                }

                return games;
            });
        }
    }
}
