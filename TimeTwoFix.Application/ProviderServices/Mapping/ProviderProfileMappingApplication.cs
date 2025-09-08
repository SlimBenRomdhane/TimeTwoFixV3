using AutoMapper;
using TimeTwoFix.Application.ProviderServices.Dtos;
using TimeTwoFix.Core.Entities.SparePartManagement;

namespace TimeTwoFix.Application.ProviderServices.Mapping
{
    public class ProviderProfileMappingApplication : Profile
    {
        public ProviderProfileMappingApplication()
        {
            // CreateMap<Source, Destination>();
            // Example:
            // CreateMap<Provider, ReadProviderDto>();
            CreateMap<Provider, ReadProviderDto>().ReverseMap();
            CreateMap<Provider, CreateProviderDto>().ReverseMap();
            CreateMap<Provider, UpdateProviderDto>().ReverseMap();
            CreateMap<Provider, DeleteProviderDto>().ReverseMap();
        }
    }
}