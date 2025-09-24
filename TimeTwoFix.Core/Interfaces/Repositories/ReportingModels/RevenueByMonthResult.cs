using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Core.Interfaces.Repositories.ReportingModels
{
    // Revenue breakdown
    public class RevenueByMonthResult
    {
        public int Year { get; set; }
        public int Month { get; set; }
        //public decimal LaborRevenue { get; set; }
        //public decimal PartsRevenue { get; set; }
        //public decimal TotalRevenue => LaborRevenue + PartsRevenue;

        public decimal Revenue { get; set; }
    }

}
