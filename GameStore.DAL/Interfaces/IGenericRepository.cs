using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Filter(Expression<Func<T, bool>> predicate);
        T Get(int id);
        IQueryable<T> GetQuery();
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
