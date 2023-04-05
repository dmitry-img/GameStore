using GameStore.DAL.Data;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;

namespace GameStore.DAL.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        private GameStoreDbContext _context;

        public GenreRepository(GameStoreDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
