using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TimeTwoFix.Core.Common;

namespace TimeTwoFix.Core.Interfaces.Repositories.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> AddAsyncGeneric(T entity);

        Task UpdateAsyncGeneric(T entity);

        Task DeleteAsyncGeneric(T entity);

        Task<T?> GetByIdAsyncGeneric(int id, Func<IQueryable<T>, IQueryable<T>>? includeBuilder = null, params Expression<Func<T, object>>[] includeProperties);

        Task<IEnumerable<T>> GetAllAsyncGeneric();

        Task AttachAsyncGeneric(T entity, EntityState entityState);

        Task DetachAsyncGeneric(T entity);

        Task<IEnumerable<T>> GetAllWithIncludesAsyncGeneric(params Expression<Func<T, object>>[] includeProperties);

        Task<IReadOnlyList<GroupCount<TKey>>> GroupCountAsynGeneric<TKey>(Expression<Func<T, TKey>> groupByExpression);

        Task<IEnumerable<T>> GetPagedByPredicateAsync<TOrderKey>(
        Expression<Func<T, bool>> predicate,
        int skip,
        int take,
        Expression<Func<T, TOrderKey>> orderBy,
        bool descending = true,
        Expression<Func<T, object>>[] includes = null);

        Task<int> GetCountByPredicateAsync(Expression<Func<T, bool>> predicate);
    }
}