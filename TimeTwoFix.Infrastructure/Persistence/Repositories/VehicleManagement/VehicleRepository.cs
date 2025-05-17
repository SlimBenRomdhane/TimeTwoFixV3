using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.VehicleManagement;
using TimeTwoFix.Core.Interfaces.Repositories.VehicleManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.Base;

namespace TimeTwoFix.Infrastructure.Persistence.Repositories.VehicleManagement
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(TimeTwoFixDbContext context) : base(context)
        {
        }
        public async Task<Vehicle?> GetVehiculeByVinAsync(string vin)
        {
            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(v => v.Vin == vin);
            return vehicle;
        }
        public async Task<Vehicle?> GetVehicleByLicensePlateAsync(string licensePlate)
        {
            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(v => v.LicensePlate == licensePlate);
            return vehicle;
        }
        public async Task<IEnumerable<Vehicle>> GetVehiclesByYearsRangeAsync(int startYear, int endYear)
        {
            var vehicles = await _context.Vehicles
                .Where(v => v.Year >= startYear && v.Year <= endYear)
                .ToListAsync();
            return vehicles;
        }
        public async Task<IEnumerable<Vehicle>> GetVehiclesByClientIdAsync(int clientId)
        {
            var vehicles = await _context.Vehicles
                .Where(v => v.ClientId == clientId)
                .ToListAsync();
            return vehicles;
        }
        public async Task<IEnumerable<Vehicle>> GetVehiclesByMultipleParamAsync(string brand, string model,
            string fuelType, string transmissionType)
        {
            IQueryable<Vehicle> query = _context.Vehicles.AsQueryable();
            if (!string.IsNullOrEmpty(brand))
            {
                query = query.Where(v => v.Brand.Contains(brand));
            }
            if (!string.IsNullOrEmpty(model))
            {
                query = query.Where(v => v.Model.Contains(model));
            }
            if (!string.IsNullOrEmpty(fuelType))
            {
                query = query.Where(v => v.FuelType.Contains(fuelType));
            }
            if (!string.IsNullOrEmpty(transmissionType))
            {
                query = query.Where(v => v.TransmissionType.Contains(transmissionType));
            }
            return await query.ToListAsync();
        }
    }
}