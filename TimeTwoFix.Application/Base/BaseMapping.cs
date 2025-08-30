using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTwoFix.Application.Base.BaseDtos;
using TimeTwoFix.Core.Common;

namespace TimeTwoFix.Application.Base
{
    public class BaseMapping : Profile
    {
        public BaseMapping()
        {
            CreateMap<GroupCount<string>, StatusCountDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count));
        }
    }
}
