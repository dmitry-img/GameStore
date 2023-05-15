using GameStore.BLL.Enums;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Interfaces
{
    public interface ISortStrategyFactory
    {
        ISortStrategy<Game> GetSortStrategy(SortOption sortOption);
    }
}
