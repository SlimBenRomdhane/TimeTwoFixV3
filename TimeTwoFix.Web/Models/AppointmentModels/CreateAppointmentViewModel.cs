﻿namespace TimeTwoFix.Web.Models.AppointmentModels
{
    public class CreateAppointmentViewModel
    {
        public int VehicleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string Urgency { get; set; }
        public string Status { get; set; }
    }
}