﻿using GameStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DAL.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        Task<Game> GetByKeyAsync(string key);
        Task<IEnumerable<Game>> GetGamesByGenreAsync(int genreId);
        Task<IEnumerable<Game>> GetGamesByPlatformTypeAsync(int platformTypeId);
    }
}
