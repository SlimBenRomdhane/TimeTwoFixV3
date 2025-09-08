namespace TimeTwoFix.Application.ProvidedServicesService.Dtos
{
    public class CreateProvidedServiceDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int EstimatedTime { get; set; }
        public decimal PricePerHour { get; set; }
        public int CategoryId { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? Createdby { get; set; }
    }
}