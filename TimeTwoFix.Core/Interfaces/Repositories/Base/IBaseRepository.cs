namespace TimeTwoFix.Core.Interfaces.Repositories.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> AddAsyncGeneric(T entity);

        Task UpdateAsyncGeneric(T entity);

        Task DeleteAsyncGeneric(T entity);

        Task<T?> GetByIdAsyncGeneric(int id);

        Task<IEnumerable<T>> GetAllAsyncGeneric();

        Task AttachAsyncGeneric(T entity);

        Task DetachAsyncGeneric(T entity);
    }
}