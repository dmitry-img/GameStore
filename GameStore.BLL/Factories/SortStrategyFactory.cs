using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using Unity;

namespace GameStore.BLL.Factories
{
    public class SortStrategyFactory : ISortStrategyFactory
    {
        private readonly IUnityContainer _container;

        public SortStrategyFactory(IUnityContainer container)
        {
            _container = container;
        }

        public ISortStrategy<Game> GetSortStrategy(SortOption sortOption)
        {
            string strategyName = sortOption.ToString();
            return _container.Resolve<ISortStrategy<Game>>(strategyName);
        }
    }
}
