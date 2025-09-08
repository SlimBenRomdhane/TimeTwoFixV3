namespace TimeTwoFix.Application.SparePartCategoryServices.Dtos
{
    internal class DeleteSparePartCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DeletedAt { get; set; }
        string? DeletedBy { get; set; }
    }
}
