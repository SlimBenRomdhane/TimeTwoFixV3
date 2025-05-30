namespace TimeTwoFix.Application.VehicleServices.Dtos
{
    public class CreateVehicleDto
    {
        public int ClientId { get; set; }
        public string Vin { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public string FuelType { get; set; }
        public string TransmissionType { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
    }
}