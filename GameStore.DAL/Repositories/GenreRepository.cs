using GameStore.DAL.Data;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;

namespace GameStore.DAL.Repositories
{
    public class GenreRepository : IGenericRepository<Genre>
    {
        private GameStoreDbContext _context;

        public GenreRepository(GameStoreDbContext context)
        {
            _context = context;
        }

        public void Create(Genre item)
        {
            _context.Genres.Add(item);
        }

        public void Delete(int id)
        {
            var genre = _context.Genres.Find(id);
            if (genre != null)
                _context.Genres.Remove(genre);
        }

        public Genre Get(int id)
        {
            return _context.Genres.Find(id);
        }

        public IEnumerable<Genre> GetAll()
        {
            return _context.Genres;
        }

        public void Update(Genre item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
