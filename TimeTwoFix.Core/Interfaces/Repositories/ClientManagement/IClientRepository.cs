using TimeTwoFix.Core.Entities.ClientManagement;
using TimeTwoFix.Core.Interfaces.Repositories.Base;

namespace TimeTwoFix.Core.Interfaces.Repositories.ClientManagement
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        Task<IEnumerable<Client>> GetClientsByMultipleParam(string searchName, string searchPhone, string searchEmail);

        Task<Client?> GetClientByEmail(string email);

        Task<IEnumerable<Client>> GetAllDeletedClients();
    }
}