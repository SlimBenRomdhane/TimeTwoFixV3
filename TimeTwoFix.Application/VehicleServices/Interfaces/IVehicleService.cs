using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.VehicleServices.Dtos;
using TimeTwoFix.Core.Entities.VehicleManagement;

namespace TimeTwoFix.Application.VehicleServices.Interfaces
{
    public interface IVehicleService : IBaseService<Vehicle>
    {
        Task<IEnumerable<ReadVehicleDto>> GetVehiclesByMultipleParam(string brand, string model,
            string fuelType, string transmissionType);

        Task<IEnumerable<ReadVehicleDto>> GetVehicleByVin(string vin);
    }
}