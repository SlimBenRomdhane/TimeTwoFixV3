using TimeTwoFix.Core.Entities.VehicleManagement;
using TimeTwoFix.Core.Interfaces.Repositories.Base;

namespace TimeTwoFix.Core.Interfaces.Repositories.VehicleManagement
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        Task<Vehicle?> GetVehicleByLicensePlateAsync(string licensePlate);

        Task<Vehicle?> GetVehiculeByVinAsync(string vin);



        Task<IEnumerable<Vehicle>> GetVehiclesByYearsRangeAsync(int startYear, int endYear);



        Task<IEnumerable<Vehicle>> GetVehiclesByClientIdAsync(int clientId);
        Task<IEnumerable<Vehicle>> GetVehiclesByMultipleParamAsync(string brand, string model, string fuelType, string transmissionType);

    }
}