using TimeTwoFix.Application.VehicleServices.Dtos;

namespace TimeTwoFix.Application.ClientServices.Dtos
{
    public class ReadClientDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Email { get; set; }
        public string? Notes { get; set; }
        public List<ReadVehicleDto> Vehicles { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}