using TimeTwoFix.Application.CategoryService.Dtos;

namespace TimeTwoFix.Application.ProvidedServicesService.Dtos
{
    public class DeleteProvidedServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int EstimatedTime { get; set; }
        public decimal PricePerHour { get; set; }
        public int CategoryId { get; set; }
        public DeleteCategoryDto DeleteCategoryDto { get; set; }
        public string? Notes { get; set; }
        public DateTime DeletedAt { get; set; } = DateTime.Now;
        public string? DeletedBy { get; set; }
    }
}