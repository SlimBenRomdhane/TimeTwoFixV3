using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTwoFix.Application.VehicleServices.Dtos;

namespace TimeTwoFix.Application.WorkOrderService.Dtos
{
    public class DeleteWorkOrderDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DeleteVehicleDto VehicleDto { get; set; }
        public int Mileage { get; set; }
        public DateOnly StartDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public DateOnly EndDate { get; set; }
        public TimeOnly EndTime { get; set; }
        public decimal TolalLaborCost { get; set; }
        public string Status { get; set; }
        public bool Paid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Notes { get; set; }
        public DateTime DeletedAt { get; set; } = DateTime.Now;
        public string DeletedBy { get; set; }

    }
}
