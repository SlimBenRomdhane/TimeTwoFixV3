using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TimeTwoFix.Core.Common;

namespace TimeTwoFix.Application.Base
{
    public interface IBaseService<T> where T : class
    {
        Task<T?> AddAsyncServiceGeneric(T entity);

        Task UpdateAsyncServiceGeneric(T entity);

        Task DeleteAsyncServiceGeneric(int id);

        Task<T?> GetByIdAsyncServiceGeneric(int id, Func<IQueryable<T>, IQueryable<T>>? includeBuilder = null, params Expression<Func<T, object>>[] includeProperties);

        Task<IEnumerable<T>> GetAllAsyncServiceGeneric();

        Task AttachAsyncServiceGeneric(T entity, EntityState entityState);

        Task DetachAsyncServiceGeneric(T entity);

        Task<int> SaveChangesServiceGeneric();

        Task<int> CountAsyncServiceGeneric();

        Task<IEnumerable<T>> GetAllWithIncludesAsyncServiceGeneric(Func<IQueryable<T>, IQueryable<T>>? includeBuilder = null, params Expression<Func<T, object>>[] includeProperties);

        Task<IReadOnlyList<GroupCount<TKey>>> GroupCountAsynServiceGeneric<TKey>(Expression<Func<T, TKey>> groupByExpression);

        Task<IEnumerable<T>> GetPagedByPredicateAsyncServiceGeneric<TOrderKey>(
            Expression<Func<T, bool>> predicate,
            int skip,
            int take,
            Expression<Func<T, TOrderKey>> orderBy,
            bool descending = true,
            Expression<Func<T, object>>[]? includes = null);

        Task<int> GetCountByPredicateAsyncServiceGeneric(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetByTextServiceGeneric(string text);

    }
}