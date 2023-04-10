using GameStore.DAL.Data;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private GameStoreDbContext _context;

        public GenericRepository(GameStoreDbContext context)
        {
            _context = context;
        }

        public void Create(T item)
        {
            _context.Set<T>().Add(item);
        }

        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var item = _context.Set<T>().Find(id);
            if (item != null)
                _context.Set<T>().Remove(item);
        }

        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

        public IQueryable<T> GetQuery()
        {
            return _context.Set<T>();
        }
    }
}
