using GameStore.DAL.Entities;

namespace GameStore.DAL.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Game GetByKey(string key);
    }
}
