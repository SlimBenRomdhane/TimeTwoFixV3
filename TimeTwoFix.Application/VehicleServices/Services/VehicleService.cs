using AutoMapper;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.VehicleServices.Dtos;
using TimeTwoFix.Application.VehicleServices.Interfaces;
using TimeTwoFix.Core.Entities.VehicleManagement;
using TimeTwoFix.Core.Interfaces;

namespace TimeTwoFix.Application.VehicleServices.Services
{
    public class VehicleService : BaseService<Vehicle>, IVehicleService
    {
        public VehicleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<ReadVehicleDto> GetVehicleByVin(string vin)
        {
            var vehicle = await _unitOfWork.Vehicles.GetVehiculeByVinAsync(vin);
            if (vehicle == null)
            {
                throw new Exception("Vehicle not found");
            }
            var readVehicleDto = _mapper.Map<ReadVehicleDto>(vehicle);
            return readVehicleDto;
        }

        public async Task<IEnumerable<ReadVehicleDto>> GetVehiclesByMultipleParam(string brand, string model, string fuelType, string transmissionType)
        {
            var vehicles = await _unitOfWork.Vehicles.GetVehiclesByMultipleParamAsync(brand, model, fuelType, transmissionType);
            if (vehicles == null)
            {
                throw new Exception("No vehicles found with the provided parameters");
            }
            var readVehicleDtos = _mapper.Map<IEnumerable<ReadVehicleDto>>(vehicles);
            return readVehicleDtos;
        }
    }
}