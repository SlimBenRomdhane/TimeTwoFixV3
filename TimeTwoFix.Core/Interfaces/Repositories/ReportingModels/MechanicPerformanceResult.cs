using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Core.Interfaces.Repositories.ReportingModels
{
    // Technician performance
    public class MechanicPerformanceResult
    {
        public int MechanicId { get; set; }
        public string MechanicName { get; set; } = string.Empty;
        public int JobsCompleted { get; set; }
        public double AverageCompletionHours { get; set; }
        public decimal TotalRevenue { get; set; }
    }

}
