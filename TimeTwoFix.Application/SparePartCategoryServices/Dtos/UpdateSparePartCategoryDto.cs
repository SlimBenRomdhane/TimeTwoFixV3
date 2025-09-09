namespace TimeTwoFix.Application.SparePartCategoryServices.Dtos
{
    public class UpdateSparePartCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Notes { get; set; }
        public DateTime UpdatedAt { get; set; }
        string? UpdatedBy { get; set; }

    }
}
