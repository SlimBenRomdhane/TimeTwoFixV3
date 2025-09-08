namespace TimeTwoFix.Application.ProvidedServicesService.Dtos
{
    public class UpdateProvidedServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int EstimatedTime { get; set; }
        public decimal PricePerHour { get; set; }
        public int CategoryId { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
    }
}