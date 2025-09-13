using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TimeTwoFix.Core.Common;
using TimeTwoFix.Core.Interfaces.Repositories.Base;

namespace TimeTwoFix.Infrastructure.Persistence.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly TimeTwoFixDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(TimeTwoFixDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> AddAsyncGeneric(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task AttachAsyncGeneric(T entity, EntityState entityState)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = entityState;
        }

        public async Task DeleteAsyncGeneric(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task DetachAsyncGeneric(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public async Task<IEnumerable<T>> GetAllAsyncGeneric()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithIncludesAsyncGeneric(Func<IQueryable<T>, IQueryable<T>>? includeBuilder = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            if (includeBuilder != null)
            {
                query = includeBuilder(query);
            }
            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsyncGeneric(int id, Func<IQueryable<T>, IQueryable<T>>? includeBuilder = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            if (includeBuilder != null)
            {
                query = includeBuilder(query);
            }
            //If the entity has a property named "Id", we can use EF.Property to access it dynamically.
            //Otherwise, we can use a different approach to get the entity by its primary key.
            //Assuming "Id" is the primary key property.

            var res = await query.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
            return res;
        }

        public async Task<IEnumerable<T>> GetByTextAsyncGeneric(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return Enumerable.Empty<T>();

            return await _context.Set<T>()
                .AsNoTracking()
                .Where(e => EF.Property<string>(e, "Name").Contains(text))
                .ToListAsync();
        }

        public Task<int> GetCountByPredicateAsync(Expression<Func<T, bool>> predicate)
        {
            var query = _dbSet.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query.CountAsync();
        }

        public async Task<IEnumerable<T>> GetPagedByPredicateAsync<TOrderKey>(Expression<Func<T, bool>> predicate, int skip, int take, Expression<Func<T, TOrderKey>> orderBy, bool descending = true, Expression<Func<T, object>>[] includes = null)
        {
            var query = _dbSet.AsQueryable();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                query = descending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
            }
            query = query.Skip(skip).Take(take);
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<GroupCount<TKey>>> GroupCountAsynGeneric<TKey>(Expression<Func<T, TKey>> groupByExpression)
        {
            var res = await _dbSet
                .AsNoTracking()
                .GroupBy(groupByExpression)
                .Select(g => new GroupCount<TKey>
                {
                    Key = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();
            return res;
        }

        public async Task UpdateAsyncGeneric(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}