using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Application.ReportingServices.Dtos
{
    public class PauseAnalysisTrendDto
    {
        public string Reason { get; set; } = string.Empty;
        public string Period { get; set; } = string.Empty; // formatted as "MMM yyyy"
        public double HoursLost { get; set; }
    }
}
