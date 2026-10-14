using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BusinessLayer.Bl.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(object id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<PagedResult<T>> GetPagedAsync<TKey>(
            Expression<Func<T, bool>>? predicate,
            Expression<Func<T, TKey>> orderBy,
            int pageNumber,
            int pageSize,
            bool sortDescending
        );
    }
}
