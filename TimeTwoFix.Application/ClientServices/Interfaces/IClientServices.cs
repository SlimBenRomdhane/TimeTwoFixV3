using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.ClientServices.Dtos;
using TimeTwoFix.Core.Entities.ClientManagement;

namespace TimeTwoFix.Application.ClientServices.Interfaces
{
    public interface IClientServices : IBaseService<Client>
    {
        Task<IEnumerable<ReadClientDto>> GetClientByMultipleParam(string searchName, string searchPhone, string searchEmail);

        Task<ReadClientDto?> GetClientByEmail(string email);

        Task<IEnumerable<ReadClientDto>> GetAllDeletedClients();
    }
}