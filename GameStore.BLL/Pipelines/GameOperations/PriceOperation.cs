using System.Linq;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Pipelines.GameOperations
{
    public class PriceOperation : IOperation<IQueryable<Game>>
    {
        private readonly decimal? _priceFrom;
        private readonly decimal? _priceTo;

        public PriceOperation(decimal? priceFrom, decimal? priceTo)
        {
            _priceFrom = priceFrom;
            _priceTo = priceTo;
        }

        public IQueryable<Game> Invoke(IQueryable<Game> games)
        {
            if (_priceFrom.HasValue)
            {
                games = games.Where(game => game.Price >= _priceFrom);
            }

            if (_priceTo.HasValue)
            {
                games = games.Where(game => game.Price <= _priceTo);
            }

            return games;
        }
    }
}
