namespace TimeTwoFix.Application.AppointmentServices.Dtos
{
    public class CreateAppointmentDto
    {
        public int VehicleId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public required string ContactName { get; set; }
        public required string ContactPhone { get; set; }
        public required string Urgency { get; set; }
        public required string Status { get; set; }
    }
}