﻿using System.Linq;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Strategies.Sorting
{
    public class MostViewedSortStrategy : ISortStrategy<Game>
    {
        public IQueryable<Game> Sort(IQueryable<Game> query)
        {
            return query.OrderByDescending(game => game.Views);
        }
    }
}
