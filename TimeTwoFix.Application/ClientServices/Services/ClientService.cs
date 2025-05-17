using AutoMapper;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.ClientServices.Dtos;
using TimeTwoFix.Core.Entities.ClientManagement;
using TimeTwoFix.Core.Interfaces;

namespace TimeTwoFix.Application.ClientServices.Services
{
    public class ClientService : BaseService<Client>, TimeTwoFix.Application.ClientServices.Interfaces.IClientServices
    {
        public ClientService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<ReadClientDto>> GetAllDeletedClients()
        {
            var deletedClients = await _unitOfWork.Clients.GetAllDeletedClients();
            var clientsDto = _mapper.Map<IEnumerable<ReadClientDto>>(deletedClients);
            return clientsDto;
        }

        public async Task<ReadClientDto?> GetClientByEmail(string email)
        {
            var client = await _unitOfWork.Clients.GetClientByEmail(email);
            if (client == null)
            {
                return null;
            }
            var clientDto = _mapper.Map<ReadClientDto>(client);
            return clientDto;
        }

        public async Task<IEnumerable<ReadClientDto>> GetClientByMultipleParam(string searchName, string searchPhone, string searchEmail)
        {
            var res = await _unitOfWork.Clients.GetClientsByMultipleParam(searchName, searchPhone, searchEmail);
            var clientsDto = _mapper.Map<IEnumerable<ReadClientDto>>(res);
            return clientsDto;
        }
    }
}