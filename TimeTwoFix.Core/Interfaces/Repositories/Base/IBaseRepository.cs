using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TimeTwoFix.Core.Interfaces.Repositories.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> AddAsyncGeneric(T entity);
        Task UpdateAsyncGeneric(T entity);
        Task DeleteAsyncGeneric(T entity);
        Task<T?> GetByIdAsyncGeneric(int id, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetAllAsyncGeneric();
        Task AttachAsyncGeneric(T entity, EntityState entityState);
        Task DetachAsyncGeneric(T entity);
        Task<IEnumerable<T>> GetAllWithIncludesAsyncGeneric(params Expression<Func<T, object>>[] includeProperties);
    }
}