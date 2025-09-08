namespace TimeTwoFix.Application.SparePartCategoryServices.Dtos
{
    public class CreateSparePartCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        string? CreatedBy { get; set; }
    }
}
