using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Pipelines.GameOperations
{
    public class DateFilterOperation : IOperation<IQueryable<Game>>
    {
        private readonly DateFilterOption _dateFilterOption;
        private readonly Dictionary<DateFilterOption, DateTime?> _dateFilters = new Dictionary<DateFilterOption, DateTime?>
        {
            { DateFilterOption.LastWeek, DateTime.UtcNow.AddDays(-7) },
            { DateFilterOption.LastMonth, DateTime.UtcNow.AddMonths(-1) },
            { DateFilterOption.LastYear, DateTime.UtcNow.AddYears(-1) },
            { DateFilterOption.TwoYears, DateTime.UtcNow.AddYears(-2) },
            { DateFilterOption.ThreeYears, DateTime.UtcNow.AddYears(-3) }
        };

        public DateFilterOperation(DateFilterOption dateFilterOption)
        {
            _dateFilterOption = dateFilterOption;
        }

        public IQueryable<Game> Invoke(IQueryable<Game> games)
        {
            if (_dateFilters.TryGetValue(_dateFilterOption, out var date))
            {
                return games.Where(game => game.CreatedAt >= date);
            }

            return games;
        }
    }
}
