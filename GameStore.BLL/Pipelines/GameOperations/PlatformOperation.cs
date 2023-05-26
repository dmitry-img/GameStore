using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Pipelines.GameOperations
{
    public class PlatformOperation : IOperation<IQueryable<Game>>
    {
        private readonly List<int> _platforms;

        public PlatformOperation(List<int> platforms)
        {
            _platforms = platforms;
        }

        public IQueryable<Game> Invoke(IQueryable<Game> games)
        {
            return _platforms != null && _platforms.Any() ? games.Where(game =>
                game.PlatformTypes.Any(p => _platforms.Contains(p.Id))) : games;
        }
    }
}
