using TimeTwoFix.Application.VehicleServices.Dtos;

namespace TimeTwoFix.Application.VehicleServices.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<ReadVehicleDto>> GetVehiclesByMultipleParam(string searchName, string searchPhone, string searchEmail);

        Task<ReadVehicleDto> GetVehicleByVin(string vin);

        Task<IEnumerable<ReadVehicleDto>> GetAllDeletedVehicles();
    }
}
