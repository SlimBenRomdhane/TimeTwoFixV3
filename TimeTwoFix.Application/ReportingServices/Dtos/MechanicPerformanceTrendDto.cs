using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Application.ReportingServices.Dtos
{
    public class MechanicPerformanceTrendDto
    {
        public string MechanicName { get; set; } = string.Empty;
        public string Period { get; set; } = string.Empty; // formatted "MMM yyyy"
        public int JobsCompleted { get; set; }
        public decimal AverageCompletionHours { get; set; }
        public decimal TotalRevenue { get; set; }

    }
}
