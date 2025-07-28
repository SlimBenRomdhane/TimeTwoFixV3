using TimeTwoFix.Web.Models.VehicleModels;

namespace TimeTwoFix.Web.Models.AppointmentModels
{
    public class ReadAppointmentViewModel
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public ReadVehicleViewModel ReadVehicleViewModel { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string Urgency { get; set; }
        public string Status { get; set; }
    }
}