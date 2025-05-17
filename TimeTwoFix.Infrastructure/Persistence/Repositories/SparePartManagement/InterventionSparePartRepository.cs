using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Interfaces.Repositories.SparePartManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.Base;

namespace TimeTwoFix.Infrastructure.Persistence.Repositories.SparePartManagement
{
    public class InterventionSparePartRepository : BaseRepository<InterventionSparePart>, IInterventionSparePartRepository
    {
        public InterventionSparePartRepository(TimeTwoFixDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<InterventionSparePart>> GetInterventionSparePartsByInterventionIdAsync(int interventionId)
        {
            var interventionSpareParts = await _context.InterventionSpareParts
                .Where(isp => isp.InterventionId == interventionId)
                .ToListAsync();
            return interventionSpareParts;
        }

        public async Task<IEnumerable<InterventionSparePart>> GetInterventionSparePartsBySparePartIdAsync(int sparePartId)
        {
            var interventionSpareParts = await _context.InterventionSpareParts
                .Where(isp => isp.SparePartId == sparePartId)
                .ToListAsync();
            return interventionSpareParts;
        }
    }
}