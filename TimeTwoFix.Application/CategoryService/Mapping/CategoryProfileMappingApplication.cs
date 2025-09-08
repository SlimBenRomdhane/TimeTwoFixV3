using AutoMapper;
using TimeTwoFix.Application.CategoryService.Dtos;
using TimeTwoFix.Core.Entities.ServiceManagement;

namespace TimeTwoFix.Application.CategoryService.Mapping
{
    public class CategoryProfileMappingApplication : Profile
    {
        public CategoryProfileMappingApplication()
        {
            CreateMap<Category, ReadCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, DeleteCategoryDto>().ReverseMap();
        }
    }
}