using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Enums;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Interfaces
{
    public interface IGameFilterOperations
    {
        IOperation<IQueryable<Game>> CreateNameOperation(string nameFragment);

        IOperation<IQueryable<Game>> CreateGenreOperation(List<int> genres);

        IOperation<IQueryable<Game>> CreatePlatformOperation(List<int> platforms);

        IOperation<IQueryable<Game>> CreatePublisherOperation(List<int> publishers);

        IOperation<IQueryable<Game>> CreatePriceOperation(decimal? priceFrom, decimal? priceTo);

        IOperation<IQueryable<Game>> CreateDateFilterOperation(DateFilterOption dateFilterOption);
    }
}
