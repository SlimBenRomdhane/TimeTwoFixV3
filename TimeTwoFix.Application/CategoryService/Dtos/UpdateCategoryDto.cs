namespace TimeTwoFix.Application.CategoryService.Dtos
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; } // This could be the username or ID of the user who updated the category
    }
}