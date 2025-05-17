using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTwoFix.Application.UserServices.Dtos.Users;
using TimeTwoFix.Core.Entities.UserManagement;

namespace TimeTwoFix.Application.UserServices.Mapping
{
    internal class UserProfileMappingApplication : Profile
    {
        public UserProfileMappingApplication()
        {
            CreateMap<ApplicationUser, ReadUserDto>().ReverseMap();
            CreateMap<UpdateUserDto, ApplicationUser>();
            CreateMap<DeleteUserDto, ApplicationUser>();
            CreateMap<CreateUserDto, ApplicationUser>();
        }
    }
}
