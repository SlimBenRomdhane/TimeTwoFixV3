using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.ServiceManagement;
using TimeTwoFix.Core.Interfaces.Repositories.ServiceManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.Base;

namespace TimeTwoFix.Infrastructure.Persistence.Repositories.ServiceManagement
{
    public class ServiceRepository : BaseRepository<Service>, IServiceRepository
    {
        public ServiceRepository(TimeTwoFixDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Service>> GetServicesByNameAsync(string name)
        {
            return await _context.Services
                .Where(s => s.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetServicesByCategoryIdAsync(int categoryId)
        {
            return await _context.Services
                .Where(s => s.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}