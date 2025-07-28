using AutoMapper;
using TimeTwoFix.Application.UserServices.Dtos.Roles;
using TimeTwoFix.Application.UserServices.Dtos.Users;
using TimeTwoFix.Core.Entities.UserManagement;

namespace TimeTwoFix.Application.UserServices.Mapping
{
    public class UserProfileMappingApplication : Profile
    {
        public UserProfileMappingApplication()
        {
            //User Mapping
            CreateMap<ApplicationUser, ReadUserDto>();
            CreateMap<Mechanic, ReadUserDto>().ReverseMap();
            CreateMap<FrontDeskAssistant, ReadUserDto>().ReverseMap();
            CreateMap<WorkshopManager, ReadUserDto>().ReverseMap();
            CreateMap<WareHouseManager, ReadUserDto>().ReverseMap();
            CreateMap<GeneralManager, ReadUserDto>().ReverseMap();

            CreateMap<UpdateUserDto, ApplicationUser>();
            CreateMap<DeleteUserDto, ApplicationUser>();
            CreateMap<CreateUserDto, ApplicationUser>();
            //Mechanic Mapping
            CreateMap<Mechanic, CreateUserDto>().ReverseMap();
            CreateMap<Mechanic, UpdateUserDto>().ReverseMap();
            //Assistant Mapping
            CreateMap<FrontDeskAssistant, CreateUserDto>().ReverseMap();
            CreateMap<FrontDeskAssistant, UpdateUserDto>().ReverseMap();
            //Workshop Manager Mapping
            CreateMap<WorkshopManager, CreateUserDto>().ReverseMap();
            CreateMap<WorkshopManager, UpdateUserDto>().ReverseMap();
            //Warehouse Manager Mapping
            CreateMap<WareHouseManager, CreateUserDto>().ReverseMap();
            CreateMap<WareHouseManager, UpdateUserDto>().ReverseMap();
            //General Manager Mapping
            CreateMap<GeneralManager, CreateUserDto>().ReverseMap();
            CreateMap<GeneralManager, UpdateUserDto>().ReverseMap();

            //Role Mapping
            CreateMap<ApplicationRole, ReadRoleDto>().ReverseMap();
            CreateMap<CreateRoleDto, ApplicationRole>();
            CreateMap<UpdateRoleDto, ApplicationRole>();
            CreateMap<DeleteRoleDto, ApplicationRole>();
        }
    }
}