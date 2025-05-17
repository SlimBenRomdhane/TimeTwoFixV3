using AutoMapper;
using TimeTwoFix.Application.UserServices.Dtos.Users;
using TimeTwoFix.Core.Entities.UserManagement;
using TimeTwoFix.Web.Models.RoleModels;
using TimeTwoFix.Web.Models.UserModels;

namespace TimeTwoFix.Web.Mapping
{
    public class UserProfileMapping : Profile
    {
        public UserProfileMapping()
        {
            CreateMap<ReadUserDto, ReadUserViewModel>().ReverseMap();

            CreateMap<ApplicationUser, ReadUserViewModel>();
            CreateMap<ApplicationRole, ReadRoleViewModel>();

            CreateMap<ReadUserDto, Mechanic>().ReverseMap();
            CreateMap<ReadUserDto, FrontDeskAssistant>().ReverseMap();
            CreateMap<ReadUserDto, WareHouseManager>().ReverseMap();
            CreateMap<ReadUserDto, GeneralManager>().ReverseMap();
            CreateMap<ReadUserDto, WorkshopManager>().ReverseMap();

            //Entity To ViewModel
            CreateMap<ApplicationUser, ReadUserViewModel>().
                ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.GetType().Name));
            CreateMap<Mechanic, ReadUserViewModel>();
            CreateMap<FrontDeskAssistant, ReadUserViewModel>();
            CreateMap<WareHouseManager, ReadUserViewModel>();
            CreateMap<WorkshopManager, ReadUserViewModel>();
            CreateMap<GeneralManager, ReadUserViewModel>();
            //View To Dto
            CreateMap<CreateFrontDeskAssistantViewModel, ReadUserDto>().ReverseMap();
            CreateMap<CreateMechanicViewModel, ReadUserDto>().ReverseMap();
            CreateMap<CreateWareHouseManagerViewModel, ReadUserDto>().ReverseMap();
            CreateMap<CreateWorkshopManagerViewModel, ReadUserDto>().ReverseMap();
            CreateMap<CreateGeneralManagerViewModel, ReadUserDto>().ReverseMap();
        }
    }
}