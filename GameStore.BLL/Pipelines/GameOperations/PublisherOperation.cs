using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Pipelines.GameOperations
{
    public class PublisherOperation : IOperation<IQueryable<Game>>
    {
        private readonly List<int> _publishers;

        public PublisherOperation(List<int> publishers)
        {
            _publishers = publishers;
        }

        public IQueryable<Game> Invoke(IQueryable<Game> games)
        {
            return _publishers != null && _publishers.Any() ? games.Where(game =>
                game.PublisherId.HasValue && _publishers.Contains(game.PublisherId.Value)) : games;
        }
    }
}
