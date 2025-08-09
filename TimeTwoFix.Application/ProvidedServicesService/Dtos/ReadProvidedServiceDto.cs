using TimeTwoFix.Application.CategoryService.Dtos;

namespace TimeTwoFix.Application.ProvidedServicesService.Dtos
{
    public class ReadProvidedServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int EstimatedTime { get; set; }
        public decimal PricePerHour { get; set; }
        public int CategoryId { get; set; }
        public ReadCategoryDto CategoryDto { get; set; }
    }
}
