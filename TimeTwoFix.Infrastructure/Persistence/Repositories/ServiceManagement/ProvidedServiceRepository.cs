using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.ServiceManagement;
using TimeTwoFix.Core.Interfaces.Repositories.ServiceManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.Base;

namespace TimeTwoFix.Infrastructure.Persistence.Repositories.ServiceManagement
{
    public class ProvidedServiceRepository : BaseRepository<ProvidedService>, IProvidedServiceRepository
    {
        public ProvidedServiceRepository(TimeTwoFixDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProvidedService>> GetServicesByNameAsync(string name)
        {
            return await _context.ProvidedServices
                .Where(s => s.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<ProvidedService>> GetServicesByCategoryIdAsync(int categoryId)
        {
            return await _context.ProvidedServices
                .Where(s => s.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}