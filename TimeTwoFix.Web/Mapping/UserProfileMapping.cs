using AutoMapper;
using TimeTwoFix.Application.UserServices.Dtos.Roles;
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
            CreateMap<CreateRoleViewModel, CreateRoleDto>();
            CreateMap<ReadRoleDto, ReadRoleViewModel>();

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
            CreateMap<CreateFrontDeskAssistantViewModel, CreateUserDto>().ReverseMap();
            CreateMap<CreateMechanicViewModel, CreateUserDto>().ReverseMap();
            CreateMap<CreateWareHouseManagerViewModel, CreateUserDto>().ReverseMap();
            CreateMap<CreateWorkshopManagerViewModel, CreateUserDto>().ReverseMap();
            CreateMap<CreateGeneralManagerViewModel, CreateUserDto>().ReverseMap();

            CreateMap<CreateFrontDeskAssistantViewModel, ReadUserDto>().ReverseMap();
            CreateMap<CreateMechanicViewModel, ReadUserDto>().ReverseMap();
            CreateMap<CreateWareHouseManagerViewModel, ReadUserDto>().ReverseMap();
            CreateMap<CreateWorkshopManagerViewModel, CreateUserDto>().ReverseMap();
            CreateMap<CreateGeneralManagerViewModel, ReadUserDto>().ReverseMap();
        }
    }
}