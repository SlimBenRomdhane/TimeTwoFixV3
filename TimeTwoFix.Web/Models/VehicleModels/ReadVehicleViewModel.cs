using TimeTwoFix.Web.Models.ClientModels;
using TimeTwoFix.Web.Models.WorkOrderModels;

namespace TimeTwoFix.Web.Models.VehicleModels
{
    public class ReadVehicleViewModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public ReadClientViewModel ReadClientViewModel { get; set; }
        public string Vin { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public string FuelType { get; set; }
        public string TransmissionType { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public List<ReadWorkOrderViewModel> WorkOrders { get; set; }
        public string Notes { get; set; }
    }
}