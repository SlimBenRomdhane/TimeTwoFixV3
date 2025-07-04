using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.ClientManagement;
using TimeTwoFix.Core.Interfaces.Repositories.ClientManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.Base;

namespace TimeTwoFix.Infrastructure.Persistence.Repositories.ClientManagement
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(TimeTwoFixDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Client>> GetActiveClientsAsync()
        {
            var activeClient = await _context.Clients
                .Where(c => !c.IsDeleted)
                .ToListAsync();
            return activeClient;
        }

        public async Task<IEnumerable<Client>> GetAllDeletedClients()
        {
            //var deletedClients = await _context.Clients
            //    .IgnoreQueryFilters()
            //    .Where(c => c.IsDeleted)
            //    .ToListAsync();
            //return deletedClients;
            var deletedClients = await _context.Clients
                //.IgnoreQueryFilters()
                .Where(c => c.IsDeleted)
                .ToListAsync();
            return deletedClients;
        }

        public Task<Client?> GetClientByEmail(string email)
        {
            var client = _context.Clients.Where(c => c.Email == email).FirstOrDefaultAsync();
            return client;
        }

        public async Task<IEnumerable<Client>> GetClientsByMultipleParam(string searchName, string searchPhone, string searchEmail)
        {
            var query = _context.Clients.AsQueryable();
            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(c => c.FirstName.Contains(searchName) || c.LastName.Contains(searchName));
            }
            if (!string.IsNullOrEmpty(searchPhone))
            {
                query = query.Where(c => c.PhoneNumber.Contains(searchPhone));
            }
            if (!string.IsNullOrEmpty(searchEmail))
            {
                query = query.Where(c => c.Email.Contains(searchEmail));
            }

            return await query.AsQueryable().ToListAsync();
        }

        public async Task<Client?> GetDeletedClientByIdAsync(int id)
        {
            var deletedClient = await _context.Clients
                .Where(c => c.IsDeleted && c.Id == id)
                .FirstOrDefaultAsync();
            return deletedClient;
        }
    }
}