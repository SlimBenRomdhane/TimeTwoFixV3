using AutoMapper;
using TimeTwoFix.Application.SparePartCategoryServices.Dtos;
using TimeTwoFix.Web.Models.SparePartCategoryModel;

namespace TimeTwoFix.Web.Mapping
{
    public class SparePartCategoryProfileMapping : Profile
    {
        public SparePartCategoryProfileMapping()
        {
            CreateMap<ReadSparePartCategoryDto, ReadSparePartCategoryViewModel>().ReverseMap();
            CreateMap<CreateSparePartCategoryDto, CreateSparePartCategoryViewModel>().ReverseMap();
            CreateMap<UpdateSparePartCategoryDto, UpdateSparePartCategoryViewModel>().ReverseMap();
            CreateMap<DeleteSparePartCategoryDto, DeleteSparePartCategoryViewModel>().ReverseMap();
        }
    }
}
