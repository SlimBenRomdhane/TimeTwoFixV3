﻿namespace TimeTwoFix.Application.CategoryService.Dtos
{
    public class ReadCategoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
    }
}