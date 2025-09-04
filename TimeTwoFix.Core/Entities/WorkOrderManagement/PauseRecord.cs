using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTwoFix.Core.Common;

namespace TimeTwoFix.Core.Entities.WorkOrderManagement
{
    public class PauseRecord : BaseEntity
    {
        [Required]
        [MaxLength(255)]

        public required string Reason { get; set; }
        public DateTime StartTime { get; set; }
        private DateTime? _endTime;
        public DateTime? EndTime
        {
            get => _endTime;
            set
            {
                if (value.HasValue && value.Value < StartTime)
                {
                    throw new ArgumentException("EndTime cannot be earlier than StartTime.");
                }
                _endTime = value;
                if (_endTime.HasValue)
                {
                    PauseDuration = _endTime.Value - StartTime;
                }
            }
        }
        [ForeignKey("Intervention")]
        public int InterventionId { get; set; }
        public Intervention Intervention { get; set; }
        public TimeSpan? PauseDuration { get; set; }
        //public TimeSpan? PauseDuration
        //{
        //    get
        //    {
        //        if (EndTime.HasValue)
        //        {
        //            return EndTime.Value - StartTime;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}



    }
}
