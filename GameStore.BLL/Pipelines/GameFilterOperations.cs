﻿using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Pipelines
{
    public class GameFilterOperations : IGameFilterOperations
    {
        private readonly Dictionary<DateFilterOption, Func<DateTime, bool>> _dateFilters = new Dictionary<DateFilterOption, Func<DateTime, bool>>
        {
            { DateFilterOption.LastWeek, date => date >= DateTime.UtcNow.AddDays(-7) },
            { DateFilterOption.LastMonth, date => date >= DateTime.UtcNow.AddMonths(-1) },
            { DateFilterOption.LastYear, date => date >= DateTime.UtcNow.AddYears(-1) },
            { DateFilterOption.TwoYears, date => date >= DateTime.UtcNow.AddYears(-2) },
            { DateFilterOption.ThreeYears, date => date >= DateTime.UtcNow.AddYears(-3) }
        };

        public IOperation<IQueryable<Game>> CreateNameOperation(string nameFragment)
        {
            return new Operation<IQueryable<Game>>(games =>
            {
                if (!string.IsNullOrEmpty(nameFragment) && nameFragment.Length >= 3)
                {
                    return games.Where(game => game.Name.Contains(nameFragment));
                }

                return games;
            });
        }

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

        public IOperation<IQueryable<Game>> CreateDateFilterOperation(DateFilterOption dateFilterOption)
        {
            if (_dateFilters.TryGetValue(dateFilterOption, out var filter))
            {
                return new Operation<IQueryable<Game>>(games => games.Where(game => filter(game.CreatedAt)));
            }

            return new Operation<IQueryable<Game>>(games => games);
        }
    }
}
