using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TimeTwoFix.Core.Interfaces;
using TimeTwoFix.Core.Interfaces.Repositories.Base;

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

        public async Task<T> AddAsyncServiceGeneric(T entity)
        {
            var res = await _baseRepository.AddAsyncGeneric(entity);
            if (res == null)
            {
                throw new Exception("Entity could not be added");
            }
            await _unitOfWork.SaveChangesAsync();
            return res;
        }

        public async Task AttachAsyncServiceGeneric(T entity, EntityState entityState)
        {
            await _baseRepository.AttachAsyncGeneric(entity, entityState);
        }

        public int CountAsyncServiceGeneric()
        {
            var count = _baseRepository.GetAllAsyncGeneric().Result.Count();
            return count;
        }

        public async Task DeleteAsyncServiceGeneric(int id)
        {
            var enityToDelete = await _baseRepository.GetByIdAsyncGeneric(id);
            if (enityToDelete == null)
            {
                throw new Exception("Entity not found");
            }
            await _baseRepository.DeleteAsyncGeneric(enityToDelete);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DetachAsyncServiceGeneric(T entity)
        {
            await _baseRepository.DetachAsyncGeneric(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsyncServiceGeneric()
        {
            var res = await _baseRepository.GetAllAsyncGeneric();
            return res;
        }

        public async Task<IEnumerable<T>> GetAllWithIncludesAsyncServiceGeneric(params Expression<Func<T, object>>[] includeProperties)
        {
            var res = await _baseRepository.GetAllWithIncludesAsyncGeneric(includeProperties);
            if (res == null)
            {
                throw new Exception("Entities not found");
            }
            return res;
        }

        public async Task<T?> GetByIdAsyncServiceGeneric(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            var res = await _baseRepository.GetByIdAsyncGeneric(id, includeProperties);
            if (res == null)
            {
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID {id} not found.");
            }
            return res;
        }

        public async Task<int> SaveChangesServiceGeneric()
        {
            var res = await _unitOfWork.SaveChangesAsync();
            return res;
        }

        public async Task UpdateAsyncServiceGeneric(T entity)
        {
            await _baseRepository.UpdateAsyncGeneric(entity);
            if (entity == null)
            {
                throw new Exception("Entity could not be updated");
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }
}