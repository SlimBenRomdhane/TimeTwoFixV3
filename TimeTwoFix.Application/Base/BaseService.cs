using AutoMapper;
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

            return res;
        }

        public async Task AttachAsyncServiceGeneric(T entity)
        {
            await _baseRepository.AttachAsyncGeneric(entity);
        }

        public async Task DeleteAsyncServiceGeneric(int id)
        {
            var enityToDelete = await _baseRepository.GetByIdAsyncGeneric(id);
            if (enityToDelete == null)
            {
                throw new Exception("Entity not found");
            }
            await _baseRepository.DeleteAsyncGeneric(enityToDelete);
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

        public async Task<T?> GetByIdAsyncServiceGeneric(int id)
        {
            var res = await _baseRepository.GetByIdAsyncGeneric(id);
            if (res == null)
            {
                throw new Exception("Entity not found");
            }
            return res;
        }

        public async Task UpdateAsyncServiceGeneric(T entity)
        {
            await _baseRepository.UpdateAsyncGeneric(entity);
        }
    }
}