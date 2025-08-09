using AutoMapper;
using TimeTwoFix.Application.ProvidedServicesService.Dtos;
using TimeTwoFix.Web.Models.ProvidedServiceModels;

namespace TimeTwoFix.Web.Mapping
{
    public class ProvidedServiceProfileMapping : Profile
    {
        public ProvidedServiceProfileMapping()
        {
            CreateMap<ReadProvidedServiceDto, ReadProvidedServiceViewModel>()
                .ForMember(dest => dest.ReadCategoryViewModel, opt => opt.MapFrom(src => src.CategoryDto))
                .ReverseMap();
            CreateMap<DeleteProvidedServiceDto, DeleteProvidedServiceViewModel>()
                .ForMember(dest => dest.DeleteCategoryViewModel, opt => opt.MapFrom(src => src.DeleteCategoryDto))
                .ReverseMap();
            CreateMap<CreateProvidedServiceViewModel, CreateProvidedServiceDto>();
            CreateMap<UpdateProvidedServiceViewModel, UpdateProvidedServiceDto>().ReverseMap();
        }
    }
}
