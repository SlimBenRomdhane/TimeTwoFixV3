using AutoMapper;
using TimeTwoFix.Application.CategoryService.Dtos;
using TimeTwoFix.Web.Models.CategoryModels;

namespace TimeTwoFix.Web.Mapping
{
    public class CategoryProfileMapping : Profile
    {
        public CategoryProfileMapping()
        {
            CreateMap<CreateCategoryViewModel, CreateCategoryDto>();
            CreateMap<UpdateCategoryViewModel, UpdateCategoryDto>().ReverseMap();
            CreateMap<DeleteCategoryViewModel, DeleteCategoryDto>().ReverseMap();
            CreateMap<ReadCategoryDto, ReadCategoryViewModel>().ReverseMap();
        }
    }
}