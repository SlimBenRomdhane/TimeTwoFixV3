namespace TimeTwoFix.Application.SparePartCategoryServices.Dtos
{
    public class CreateSparePartCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        string? CreatedBy { get; set; }
    }
}
