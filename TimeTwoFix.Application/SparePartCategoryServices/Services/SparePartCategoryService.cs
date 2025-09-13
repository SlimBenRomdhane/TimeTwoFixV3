using AutoMapper;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.SparePartCategoryServices.Dtos;
using TimeTwoFix.Application.SparePartCategoryServices.Interfaces;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Interfaces;

namespace TimeTwoFix.Application.SparePartCategoryServices.Services
{
    public class SparePartCategoryService : BaseService<SparePartCategory>, ISparePartCategoryService
    {
        public SparePartCategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<ReadSparePartCategoryDto>> GetSparePartCategoryByNameAsync(string categoryName)
        {
            var categories = await _unitOfWork.SparePartCategories.GetSparePartCategoryByNameAsync(categoryName);
            if (categories == null || !categories.Any())
            {
                return new List<ReadSparePartCategoryDto>();

            }
            return _mapper.Map<IEnumerable<ReadSparePartCategoryDto>>(categories);
        }

        public async Task<IEnumerable<SparePartCategoryWithUsageDto>> GetSparePartCategoryWithUsageAsync()
        {
            var categoriesWithUsage = await _unitOfWork.SparePartCategories.GetSparePartCategoryWithUsageAsync();
            var dtoList = _mapper.Map<IEnumerable<SparePartCategoryWithUsageDto>>(categoriesWithUsage);
            return dtoList;
        }
    }
}
