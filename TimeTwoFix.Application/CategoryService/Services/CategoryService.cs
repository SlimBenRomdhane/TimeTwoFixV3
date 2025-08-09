using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.CategoryService.Dtos;
using TimeTwoFix.Application.CategoryService.Interfaces;
using TimeTwoFix.Core.Entities.ServiceManagement;
using TimeTwoFix.Core.Interfaces;

namespace TimeTwoFix.Application.CategoryService.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<ReadCategoryDto>> GetCategoryByName(string categoryName)
        {
            var categories = await _unitOfWork.Categories.GetCategoriesByNameAsync(categoryName);
            if (categories == null || !categories.Any())
            {
                throw new KeyNotFoundException($"No categories found with the name: {categoryName}");
            }
            var readCategoryDtos = _mapper.Map<IEnumerable<ReadCategoryDto>>(categories);
            if (readCategoryDtos == null || !readCategoryDtos.Any())
            {
                throw new Exception("Mapping to ReadCategoryDto failed or returned no results.");
            }
            return (readCategoryDtos);
        }
    }
}
