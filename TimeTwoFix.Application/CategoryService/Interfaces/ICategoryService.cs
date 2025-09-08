using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.CategoryService.Dtos;
using TimeTwoFix.Core.Entities.ServiceManagement;

namespace TimeTwoFix.Application.CategoryService.Interfaces
{
    public interface ICategoryService : IBaseService<Category>
    {
        Task<IEnumerable<ReadCategoryDto>> GetCategoryByName(string categoryName);
    }
}