using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Core.Interfaces.Repositories.ReportingModels
{
    // Work order counts and durations
    public class WorkOrderSummaryResult
    {
        public int TotalCreated { get; set; }
        public int TotalClosed { get; set; }
        public double AverageDurationHours { get; set; }
        public int PaidCount { get; set; }
        public int UnpaidCount { get; set; }
    }

}
