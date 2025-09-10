using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.SparePartCategoryServices.Dtos;
using TimeTwoFix.Core.Entities.SparePartManagement;

namespace TimeTwoFix.Application.SparePartCategoryServices.Interfaces
{
    public interface ISparePartCategoryService : IBaseService<SparePartCategory>
    {
        // Additional methods specific to SparePartCategory can be added here
        Task<IEnumerable<ReadSparePartCategoryDto>> GetSparePartCategoryByNameAsync(string categoryName);
        Task<IEnumerable<SparePartCategoryWithUsageDto>> GetSparePartCategoryWithUsageAsync();
    }
}
