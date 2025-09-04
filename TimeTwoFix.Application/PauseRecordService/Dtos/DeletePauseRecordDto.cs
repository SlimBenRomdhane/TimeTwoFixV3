using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTwoFix.Application.PauseRecordService.Dtos
{
    public class DeletePauseRecordDto
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int InterventionId { get; set; }
        public TimeSpan? PauseDuration { get; set; }
    }
}
