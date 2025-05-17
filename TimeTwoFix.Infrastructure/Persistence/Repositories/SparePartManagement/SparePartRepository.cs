using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Interfaces.Repositories.SparePartManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.Base;

namespace TimeTwoFix.Infrastructure.Persistence.Repositories.SparePartManagement
{
    public class SparePartRepository : BaseRepository<SparePart>, ISparePartRepository
    {
        public SparePartRepository(TimeTwoFixDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SparePart>> GetSparePartsByNameAsync(string name)
        {
            var spareParts = await _context.SpareParts.Where(s => s.Name.Contains(name))
                .ToListAsync();
            return spareParts;
        }

        public async Task<SparePart?> GetSparePartByPartCode(string partCode)
        {
            var sparePart = await _context.SpareParts.FirstOrDefaultAsync(s => s.PartCode == partCode);
            return sparePart;
        }

        public Task<IEnumerable<SparePart>> GetSparePartsByQuantityAsync(int quantity)
        {
            throw new NotImplementedException();
        }
    }
}