using TimeTwoFix.Application.InterventionService.Dtos;
using TimeTwoFix.Application.VehicleServices.Dtos;

namespace TimeTwoFix.Application.WorkOrderService.Dtos
{
    public class ReadWorkOrderDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public ReadVehicleDto VehicleDto { get; set; }
        public int Mileage { get; set; }
        public DateTime StartDate { get; set; }
        //public TimeOnly StartTime { get; set; }
        public DateTime EndDate { get; set; }
        //public TimeOnly EndTime { get; set; }
        public decimal TolalLaborCost { get; set; }
        public string Status { get; set; }
        public bool Paid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Notes { get; set; }
        public ICollection<ReadInterventionDto> InterventionDtos { get; set; }
    }
}