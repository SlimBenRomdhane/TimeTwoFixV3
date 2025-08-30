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
        Task<T?> GetByIdAsyncServiceGeneric(int id, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetAllAsyncServiceGeneric();
        Task AttachAsyncServiceGeneric(T entity, EntityState entityState);
        Task DetachAsyncServiceGeneric(T entity);
        Task<int> SaveChangesServiceGeneric();
        Task<int> CountAsyncServiceGeneric();
        Task<IEnumerable<T>> GetAllWithIncludesAsyncServiceGeneric(params Expression<Func<T, object>>[] includeProperties);
        Task<IReadOnlyList<GroupCount<TKey>>> GroupCountAsynServiceGeneric<TKey>(Expression<Func<T, TKey>> groupByExpression);
    }
}