using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTwoFix.Core.Common;
using TimeTwoFix.Core.Entities.VehicleManagement;

namespace TimeTwoFix.Core.Entities.WorkOrderManagement
{
    public class WorkOrder : BaseEntity
    {
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

        [Required]
        public int Mileage { get; set; }

        public DateOnly StartDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public DateOnly EndDate { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool Paid { get; set; }
        public DateTime? PaymentDate { get; set; }

        private decimal _totalLaborCost;
        //public decimal TolalLaborCost { get; set; }

        public decimal TolalLaborCost
        {
            get => _totalLaborCost;
            set
            {
                _totalLaborCost = value;
            }
        }

        private string _status;

        [MaxLength(50)]
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
            }
        }

        public Vehicle Vehicle { get; set; }
        public ICollection<Intervention> Interventions { get; set; }

        public void RecalculateLaborCost()
        {
            decimal total = 0;

            if (Interventions != null)
            {
                foreach (var intervention in Interventions.Where(i => !i.IsDeleted && i.Status == "Completed"))
                {
                    total += intervention.InterventionPrice;

                    if (intervention.InterventionSpareParts != null)
                    {
                        foreach (var part in intervention.InterventionSpareParts)
                        {
                            if (part.SparePart != null)
                            {
                                total += part.Quantity * part.SparePart.UnitPrice;
                            }
                        }
                    }
                }
            }

            TolalLaborCost = total;
        }


        public void UpdateStatus2()
        {
            if (Status == "Cancelled" || Status == "Paused")
            {
                return; // Do not change status if it's Cancelled or Paused
            }
            if (Interventions == null || !Interventions.Any())
            {
                Status = "Pending";
                return;
            }

            var completedInterventions = Interventions
                .Where(i => !i.IsDeleted && i.Status == "Completed")
                .ToList();

            if (Interventions.Any(i => !i.IsDeleted && i.Status != "Completed"))
            {
                Status = "In Progress";
            }
            else if (completedInterventions.Any())
            {
                Status = "Completed";

                var latestEndDateTime = completedInterventions
                    .Where(i => i.EndDate.HasValue)
                    .Max(i => i.EndDate.Value);
                var earliestStartDateTime = completedInterventions
                    .Min(i => i.StartDate);
                StartDate = DateOnly.FromDateTime(earliestStartDateTime);
                StartTime = TimeOnly.FromDateTime(earliestStartDateTime);
                EndDate = DateOnly.FromDateTime(latestEndDateTime);
                EndTime = TimeOnly.FromDateTime(latestEndDateTime);
            }
            else
            {
                Status = "Paused";
            }
        }
    }
}