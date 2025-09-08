namespace TimeTwoFix.Application.CategoryService.Dtos
{
    public class DeleteCategoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public DateTime DeletedAt { get; set; } = DateTime.Now;
        public string? DeletedBy { get; set; } // This could be the username or ID of the user who deleted the category
    }
}