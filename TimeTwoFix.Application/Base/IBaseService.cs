namespace TimeTwoFix.Application.Base
{
    public interface IBaseService<T> where T : class
    {
        Task<T> AddAsyncServiceGeneric(T entity);

        Task UpdateAsyncServiceGeneric(T entity);

        Task DeleteAsyncServiceGeneric(int id);

        Task<T?> GetByIdAsyncServiceGeneric(int id);

        Task<IEnumerable<T>> GetAllAsyncServiceGeneric();

        Task AttachAsyncServiceGeneric(T entity);

        Task DetachAsyncServiceGeneric(T entity);
    }
}