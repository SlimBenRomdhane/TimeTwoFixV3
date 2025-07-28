namespace TimeTwoFix.Web.Models.AppointmentModels
{
    public class CreateAppointmentViewModel
    {
        public int VehicleId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public required string ContactName { get; set; }
        public required string ContactPhone { get; set; }
        public required string Urgency { get; set; }
        public required string Status { get; set; }
    }
}