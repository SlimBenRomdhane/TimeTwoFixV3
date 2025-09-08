using AutoMapper;
using TimeTwoFix.Application.SparePartServices.Dtos;
using TimeTwoFix.Web.Models.SparePartModels;

namespace TimeTwoFix.Web.Mapping
{
    public class SparePartProfileMapping : Profile
    {
        public SparePartProfileMapping()
        {
            CreateMap<CreateSparePartDto, CreateSparePartViewModel>().ReverseMap();
            CreateMap<ReadSparePartDto, ReadSparePartViewModel>().ReverseMap();
            CreateMap<UpdateSparePartDto, UpdateSparePartViewModel>().ReverseMap();
            CreateMap<DeleteSparePartDto, DeleteSparePartViewModel>().ReverseMap();
        }
    }
}