using TimeTwoFix.Application.VehicleServices.Dtos;

namespace TimeTwoFix.Application.AppointmentServices.Dtos
{
    public class DeleteAppointmentDto
    {
        public int Id { get; set; }
        public ReadVehicleDto? ReadVehicleDto { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public string? ContactName { get; set; }
        public string? ContactPhone { get; set; }
        public string? Urgency { get; set; }
        public string? Status { get; set; }
    }
}