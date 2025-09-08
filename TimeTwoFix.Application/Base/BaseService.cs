using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TimeTwoFix.Core.Common;
using TimeTwoFix.Core.Interfaces;
using TimeTwoFix.Core.Interfaces.Repositories.Base;
using TimeTwoFix.Infrastructure.Persistence.Includes;

namespace TimeTwoFix.Application.Base
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly IBaseRepository<T> _baseRepository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _baseRepository = unitOfWork.GetRepository<T>();
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Adds a new entity to the repository and saves changes
        public async Task<T?> AddAsyncServiceGeneric(T entity)
        {
            if (entity == null)
            {
                return null;
            }

            var addedEntity = await _baseRepository.AddAsyncGeneric(entity);
            if (addedEntity == null)
            {
                return null;
            }
            var numberOfChangesSaved = await _unitOfWork.SaveChangesAsync();
            if (numberOfChangesSaved <= 0)
            {
                return null;
            }
            return addedEntity;
        }

        // Attaches an entity to the context with the specified state
        public async Task AttachAsyncServiceGeneric(T entity, EntityState entityState)
        {
            if (entity == null)
            {
                return;
            }
            await _baseRepository.AttachAsyncGeneric(entity, entityState);
        }

        // Returns the total count of entities in the repository
        public async Task<int> CountAsyncServiceGeneric()
        {
            var entities = await _baseRepository.GetAllAsyncGeneric();
            return entities?.Count() ?? 0;
        }

        // Deletes an entity by its identifier and saves changes
        public async Task DeleteAsyncServiceGeneric(int id)
        {
            var entityToDelete = await _baseRepository.GetByIdAsyncGeneric(id);
            if (entityToDelete == null)
            {
                return;
            }
            await _baseRepository.DeleteAsyncGeneric(entityToDelete);
            var numberOfChangesSaved = await _unitOfWork.SaveChangesAsync();
            if (numberOfChangesSaved <= 0)
            {
                return;
            }
        }

        // Detaches an entity from the context
        public async Task DetachAsyncServiceGeneric(T entity)
        {
            if (entity == null)
            {
                return;
            }
            await _baseRepository.DetachAsyncGeneric(entity);
        }

        // Retrieves all entities from the repository
        public async Task<IEnumerable<T>> GetAllAsyncServiceGeneric()
        {
            var entities = await _baseRepository.GetAllAsyncGeneric();
            return entities ?? Enumerable.Empty<T>();
        }

        public async Task<IEnumerable<T>> GetAllWithDynamicIncludesGeneric()
        {
            var includes = EntityIncludeHelper.GetIncludes<T>();
            var entities = await _baseRepository.GetAllWithIncludesAsyncGeneric(includes);
            return entities ?? Enumerable.Empty<T>();
        }

        // Retrieves all entities with included related properties
        public async Task<IEnumerable<T>> GetAllWithIncludesAsyncServiceGeneric(params Expression<Func<T, object>>[] includeProperties)
        {
            var entities = await _baseRepository.GetAllWithIncludesAsyncGeneric(includeProperties);
            return entities ?? Enumerable.Empty<T>();
        }

        // Retrieves an entity by its identifier, including related properties if specified
        public async Task<T?> GetByIdAsyncServiceGeneric(int id, Func<IQueryable<T>, IQueryable<T>>? includeBuilder = null, params Expression<Func<T, object>>[] includeProperties)
        {
            return await _baseRepository.GetByIdAsyncGeneric(id, includeBuilder, includeProperties);
        }

        public Task<int> GetCountByPredicateAsyncServiceGeneric(Expression<Func<T, bool>> predicate)
        {
            return _baseRepository.GetCountByPredicateAsync(predicate);
        }

        public Task<IEnumerable<T>> GetPagedByPredicateAsyncServiceGeneric<TOrderKey>(Expression<Func<T, bool>> predicate, int skip, int take, Expression<Func<T, TOrderKey>> orderBy, bool descending = true, Expression<Func<T, object>>[] includes = null)
        {
            return _baseRepository.GetPagedByPredicateAsync(predicate, skip, take, orderBy, descending, includes);
        }

        public async Task<IReadOnlyList<GroupCount<TKey>>> GroupCountAsynServiceGeneric<TKey>(Expression<Func<T, TKey>> groupByExpression)
        {
            return await _baseRepository.GroupCountAsynGeneric(groupByExpression);
        }

        // Saves all changes made in the context
        public async Task<int> SaveChangesServiceGeneric()
        {
            return await _unitOfWork.SaveChangesAsync();
        }

        // Updates an entity and saves changes
        public async Task UpdateAsyncServiceGeneric(T entity)
        {
            if (entity == null)
            {
                return;
            }

            await _baseRepository.UpdateAsyncGeneric(entity);
            var numberOfChangesSaved = await _unitOfWork.SaveChangesAsync();
            // The caller should handle the case where no changes were saved
        }
    }
}