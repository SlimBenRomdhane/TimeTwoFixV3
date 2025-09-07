using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Interfaces.Repositories.SparePartManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.Base;

namespace TimeTwoFix.Infrastructure.Persistence.Repositories.SparePartManagement
{
    public class ProviderRepository : BaseRepository<Provider>, IProviderRepository
    {
        public ProviderRepository(TimeTwoFixDbContext context) : base(context)
        {
        }

        public async Task<Provider> GetProviderByFiscalIdAsync(string fiscalId)
        {
            var provider = await _context.Providers
                .Where(p => p.FiscalId == fiscalId)
                .FirstOrDefaultAsync();
            return provider;
        }
    }
}
