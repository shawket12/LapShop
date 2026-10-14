using BusinessLayer.Bl.Interfaces;
using BusinessLayer.Data;
using Domains.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BusinessLayer.Bl.Classes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly LapShopContext _context;
        internal DbSet<T> dbSet;

        public GenericRepository(LapShopContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
        //public async Task<PagedResult<T>> GetPagedAsync(int pageNumber,int pageSize)
        //{
        //    var totalCount = await dbSet.CountAsync();

        //    var items = await dbSet
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToListAsync();

        //    return new PagedResult<T>
        //    {
        //        Items = items,
        //        TotalCount = totalCount
        //    };
        //}

        public async Task<PagedResult<T>> GetPagedAsync<TKey>(
            Expression<Func<T, bool>>? predicate,
            Expression<Func<T, TKey>> orderBy,
            int pageNumber,
            int pageSize,
            bool sortDescending = false)
        {
            IQueryable<T> query = dbSet.AsNoTracking();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (sortDescending)
            {
                query = query.OrderByDescending(orderBy);
            }
            else
            {
                query = query.OrderBy(orderBy);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }


    }
}
