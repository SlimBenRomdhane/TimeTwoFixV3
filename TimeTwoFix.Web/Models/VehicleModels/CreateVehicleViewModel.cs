using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Web.Models.VehicleModels
{
    public class CreateVehicleViewModel
    {
        public int ClientId { get; set; }
        [Required]
        [StringLength(17, MinimumLength = 17)]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{17}$", ErrorMessage = "VIN format is not valide.")]
        public string Vin { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public string FuelType { get; set; }
        public string TransmissionType { get; set; }

        [Range(1950, int.MaxValue)]
        public int Year { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive numbers allowed")]
        public int Mileage { get; set; }
    }
}
