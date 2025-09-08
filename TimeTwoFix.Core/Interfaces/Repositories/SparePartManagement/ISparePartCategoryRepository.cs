using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Interfaces.Repositories.Base;

namespace TimeTwoFix.Core.Interfaces.Repositories.SparePartManagement
{
    public interface ISparePartCategoryRepository : IBaseRepository<SparePartCategory>
    {
        Task<IEnumerable<SparePartCategory>> GetSparePartCategoryByNameAsync(string name);
    }
}