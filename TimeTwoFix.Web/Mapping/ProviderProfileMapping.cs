using AutoMapper;
using TimeTwoFix.Application.ProviderServices.Dtos;
using TimeTwoFix.Web.Models.ProviderModels;

namespace TimeTwoFix.Web.Mapping
{
    public class ProviderProfileMapping : Profile
    {
        public ProviderProfileMapping()
        {
            CreateMap<CreateProviderDto, CreateProviderViewModel>().ReverseMap();
            CreateMap<UpdateProviderDto, UpdateProviderViewModel>().ReverseMap();
            CreateMap<ReadProviderDto, ReadProviderViewModel>().ReverseMap();
            CreateMap<DeleteProviderDto, DeleteProviderViewModel>().ReverseMap();
        }
    }
}
