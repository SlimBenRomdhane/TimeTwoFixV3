namespace TimeTwoFix.Application.CategoryService.Dtos
{
    public class CreateCategoryDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; } // This could be the username or ID of the user who created the category
    }
}