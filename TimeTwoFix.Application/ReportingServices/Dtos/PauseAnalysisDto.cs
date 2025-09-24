using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Application.ReportingServices.Dtos
{
    public class PauseAnalysisDto
    {
        public string Reason { get; set; } = string.Empty;
        public int Occurrences { get; set; }
        public double TotalHoursLost { get; set; }
        public double AveragePauseMinutes { get; set; }
    }

}
