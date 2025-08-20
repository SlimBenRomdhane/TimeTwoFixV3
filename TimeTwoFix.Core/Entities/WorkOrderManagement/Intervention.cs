using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTwoFix.Core.Common;
using TimeTwoFix.Core.Entities.BridgeManagement;
using TimeTwoFix.Core.Entities.ServiceManagement;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Entities.UserManagement;

namespace TimeTwoFix.Core.Entities.WorkOrderManagement
{
    public class Intervention : BaseEntity
    {
        [ForeignKey("WorkOrder")]
        public int WorkOrderId { get; set; }

        [ForeignKey("Mechanic")]
        public int MechanicId { get; set; }

        [ForeignKey("Service")]
        public int ServiceId { get; set; }

        [ForeignKey("LiftingBridge")]
        public int LiftingBridgeId { get; set; }
        public DateTime StartDate { get; set; }
        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                if (_endDate != null)
                    CalculateActualTimeSpent();
            }
        }
        public TimeSpan? ActualTimeSpent
        {
            get; set;
            //get
            //{
            //    if (EndDate == null)
            //    {
            //        return TimeSpan.Zero;
            //    }
            //    var duration = EndDate - StartDate;
            //    var totalPauses = PauseRecords?.Aggregate(
            //        TimeSpan.Zero,
            //        (total, pause) => total + (pause.PauseDuration ?? TimeSpan.Zero)
            //    ) ?? TimeSpan.Zero;

            //    return duration - totalPauses;
            //}
        }
        public decimal InterventionPrice { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        public Mechanic Mechanic { get; set; }
        public WorkOrder WorkOrder { get; set; }
        public ProvidedService Service { get; set; }
        public LiftingBridge LiftingBridge { get; set; }
        public ICollection<InterventionSparePart> InterventionSpareParts { get; set; }
        public ICollection<PauseRecord> PauseRecords { get; set; }

        private void CalculateActualTimeSpent()
        {
            if (EndDate == null)
            {
                ActualTimeSpent = null;
                return;

            }
            var duration = EndDate - StartDate;
            var totalPauses = PauseRecords?.Aggregate(
                TimeSpan.Zero,
                (total, pause) => total + (pause.PauseDuration ?? TimeSpan.Zero)
            ) ?? TimeSpan.Zero;
            ActualTimeSpent = duration - totalPauses;
        }
    }
}