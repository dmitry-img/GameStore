using GameStore.DAL.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Interfaces
{
    public interface IGameGenreRepository : IGenericRepository<GameGenre>
    {
        IEnumerable<Game> GetGamesByGenre(int genreId);
    }
}
