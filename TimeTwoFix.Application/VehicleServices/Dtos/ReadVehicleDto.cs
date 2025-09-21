using TimeTwoFix.Application.ClientServices.Dtos;
using TimeTwoFix.Application.WorkOrderService.Dtos;

namespace TimeTwoFix.Application.VehicleServices.Dtos
{
    public class ReadVehicleDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public ReadClientDto ReadClientDto { get; set; }
        public string Vin { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public string FuelType { get; set; }
        public string TransmissionType { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public List<ReadWorkOrderDto> WorkOrders { get; set; }
        public string Notes { get; set; }
    }
}