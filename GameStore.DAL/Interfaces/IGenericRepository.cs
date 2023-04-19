using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStore.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> FilterAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetAsync(int id);

        IQueryable<T> GetQuery();

        void Create(T item);

        void Update(T item);

        void Delete(int id);
    }
}
