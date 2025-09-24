using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Application.ReportingServices.Dtos
{
    public class VehicleInsightDto
    {
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int WorkOrderCount { get; set; }
        public double AverageMileage { get; set; }
        public string VIN { get; set; } = string.Empty;
        public decimal TotalSpend { get; set; }
    }


}
