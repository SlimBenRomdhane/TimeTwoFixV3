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

        [MaxLength(50)]
        private string _status;
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

                    //if (intervention.InterventionSpareParts != null)
                    //{
                    //    foreach (var part in intervention.InterventionSpareParts)
                    //    {
                    //        if (part.SparePart != null)
                    //        {
                    //            total += part.Quantity * part.SparePart.Price;
                    //        }
                    //    }
                    //}
                }
            }

            TolalLaborCost = total;
            Status = Interventions != null && Interventions.Any(i => i.Status != "Completed") ? "In Progress" : "Completed";
            //EndDate = Interventions != null && Interventions.Any(i => i.Status == "Completed") ? Interventions.Where(i => i.Status == "Completed").Max(i => i.EndDate)?.Date ?? EndDate : EndDate;
        }

    }
}