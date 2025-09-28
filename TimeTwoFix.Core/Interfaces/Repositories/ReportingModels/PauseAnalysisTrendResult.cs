using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Core.Interfaces.Repositories.ReportingModels
{
    public class PauseAnalysisTrendResult
    {
        public string Reason { get; set; } = string.Empty;
        public DateTime Period { get; set; } // normalized to first of month
        public double HoursLost { get; set; }
    }
}
