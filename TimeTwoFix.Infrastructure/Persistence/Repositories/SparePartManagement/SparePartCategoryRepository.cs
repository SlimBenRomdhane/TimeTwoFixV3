using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Interfaces.Repositories.SparePartManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.Base;

namespace TimeTwoFix.Infrastructure.Persistence.Repositories.SparePartManagement
{
    public class SparePartCategoryRepository : BaseRepository<SparePartCategory>, ISparePartCategoryRepository
    {
        public SparePartCategoryRepository(TimeTwoFixDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SparePartCategory>> GetSparePartCategoryByNameAsync(string name)
        {
            var sparePartCategories = await _context.SparePartCategories
                .Where(spc => spc.Name.Contains(name))
                .ToListAsync();
            return sparePartCategories;
        }

        public async Task<IEnumerable<SparePartCategoryWithUsage>> GetSparePartCategoryWithUsageAsync()
        {
            var categWithUsage = await _context.SparePartCategories
                .Select(c => new SparePartCategoryWithUsage
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    UsageCount = c.SpareParts.Count(sp => !sp.IsDeleted) // Count only non-deleted spare parts
                })
                .OrderBy(sp => sp.UsageCount)
                .ThenBy(sp => sp.Name)
                .ToListAsync();
            return categWithUsage;

        }
    }
}