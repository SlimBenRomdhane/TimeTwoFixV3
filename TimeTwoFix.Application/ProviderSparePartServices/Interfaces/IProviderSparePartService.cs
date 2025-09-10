using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.ProviderSparePartServices.Dtos;
using TimeTwoFix.Core.Entities.SparePartManagement;

namespace TimeTwoFix.Application.ProviderSparePartServices.Interfaces
{
    public interface IProviderSparePartService : IBaseService<ProviderSparePart>
    {
        Task<IEnumerable<ReadProviderSparePartDto>> GetProviderSparePartsByProviderIdService(int providerId);

        Task<IEnumerable<ReadProviderSparePartDto>> GetProviderSparePartsBySparePartIdService(int sparePartId);
    }
}
