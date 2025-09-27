using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Core.Interfaces.Repositories.ReportingModels
{
    public class MechanicPerformanceTrendResult
    {
        public int MechanicId { get; set; }
        public string MechanicName { get; set; } = string.Empty;
        public DateTime Period { get; set; }   // e.g. first day of the month
        public int JobsCompleted { get; set; }
        public decimal AverageCompletionHours { get; set; }
        public decimal TotalRevenue { get; set; }

    }
}
