using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTwoFix.Core.Common;
using TimeTwoFix.Core.Entities.AppointmentManagement;
using TimeTwoFix.Core.Entities.ClientManagement;
using TimeTwoFix.Core.Entities.WorkOrderManagement;

namespace TimeTwoFix.Core.Entities.VehicleManagement
{
    public class Vehicle : BaseEntity
    {
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [Required]
        [MaxLength(17)]
        public string Vin { get; set; }

        [MaxLength(50)]
        public string Brand { get; set; }

        [MaxLength(50)]
        public string Model { get; set; }

        [Required]
        [MaxLength(50)]
        public string LicensePlate { get; set; }

        [MaxLength(50)]
        public string FuelType { get; set; }

        [MaxLength(50)]
        public string TransmissionType { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Mileage { get; set; }

        public Client Client { get; set; }
        public ICollection<WorkOrder> WorkOrders { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}