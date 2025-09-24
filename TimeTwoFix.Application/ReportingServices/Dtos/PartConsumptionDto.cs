using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Application.ReportingServices.Dtos
{
    public class PartConsumptionDto
    {
        public int SparePartId { get; set; }
        public string PartCode { get; set; } = string.Empty;
        public string PartName { get; set; } = string.Empty;
        public int QuantityUsed { get; set; }
        public decimal TotalValue { get; set; }
    }

}
