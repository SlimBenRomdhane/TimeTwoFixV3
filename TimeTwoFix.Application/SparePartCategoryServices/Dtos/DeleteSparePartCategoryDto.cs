namespace TimeTwoFix.Application.SparePartCategoryServices.Dtos
{
    public class DeleteSparePartCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DeletedAt { get; set; } = DateTime.Now;
        string? DeletedBy { get; set; }
    }
}
