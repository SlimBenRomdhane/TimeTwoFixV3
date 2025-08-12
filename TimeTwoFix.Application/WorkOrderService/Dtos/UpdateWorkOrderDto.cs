using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Application.WorkOrderService.Dtos
{
    public class UpdateWorkOrderDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int Mileage { get; set; }
        public DateOnly StartDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public DateOnly EndDate { get; set; }
        public TimeOnly EndTime { get; set; }
        public decimal TolalLaborCost { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
    }
}
