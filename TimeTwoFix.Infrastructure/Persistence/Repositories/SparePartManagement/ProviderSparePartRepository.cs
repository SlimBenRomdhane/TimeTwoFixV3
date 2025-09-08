using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Interfaces.Repositories.SparePartManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.Base;

namespace TimeTwoFix.Infrastructure.Persistence.Repositories.SparePartManagement
{
    public class ProviderSparePartRepository : BaseRepository<ProviderSparePart>, IProviderSparePartRepository
    {
        public ProviderSparePartRepository(TimeTwoFixDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProviderSparePart>> GetProviderSparePartsByProviderIdAsync(int providerId)
        {
            var providerSpareParts = await _context.ProviderSpareParts
                .Where(psp => psp.ProviderId == providerId).ToListAsync();
            return providerSpareParts;
        }

        public async Task<IEnumerable<ProviderSparePart>> GetProviderSparePartsBySparePartIdAsync(int sparePartId)
        {
            var providerSpareParts = await _context.ProviderSpareParts
                .Where(psp => psp.SparePartId == sparePartId).ToListAsync();
            return providerSpareParts;
        }
    }
}