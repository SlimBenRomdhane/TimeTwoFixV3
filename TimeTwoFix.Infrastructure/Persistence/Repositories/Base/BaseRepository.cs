using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<T>> GetAllWithIncludesAsyncGeneric(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsyncGeneric(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            //If the entity has a property named "Id", we can use EF.Property to access it dynamically.
            //Otherwise, we can use a different approach to get the entity by its primary key.
            //Assuming "Id" is the primary key property.

            var res = await query.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
            return res;
        }

        public async Task UpdateAsyncGeneric(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}