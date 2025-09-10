using AutoMapper;
using TimeTwoFix.Application.ProviderSparePartServices.Dtos;
using TimeTwoFix.Web.Models.ProviderSparePartModels;

namespace TimeTwoFix.Web.Mapping
{
    public class ProviderSparePartProfileMapping : Profile
    {
        public ProviderSparePartProfileMapping()
        {
            CreateMap<CreateProviderSparePartDto, CreateProviderSparePartViewModel>().ReverseMap();
            CreateMap<UpdateProviderSparePartDto, UpdateProviderSparePartViewModel>().ReverseMap();
            CreateMap<DeleteProviderSparePartDto, DeleteProviderSparePartViewModel>().ReverseMap();
            CreateMap<ReadProviderSparePartDto, ReadProviderSparePartViewModel>().ReverseMap();
        }
    }
}
