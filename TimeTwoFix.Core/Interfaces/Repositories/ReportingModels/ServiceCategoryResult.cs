using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Core.Interfaces.Repositories.ReportingModels
{
    public class ServiceCategoryResult
    {
        public string CategoryName { get; set; } = string.Empty;
        public int WorkOrderCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }


}
