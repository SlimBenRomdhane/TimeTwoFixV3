using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Application.ReportingServices.Dtos
{
    public class ServiceCategoryDto
    {
        public string CategoryName { get; set; } = string.Empty;
        public int WorkOrderCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }


}
