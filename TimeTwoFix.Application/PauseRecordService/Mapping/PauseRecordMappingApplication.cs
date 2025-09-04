using AutoMapper;
using TimeTwoFix.Application.PauseRecordService.Dtos;
using TimeTwoFix.Core.Entities.WorkOrderManagement;

namespace TimeTwoFix.Application.PauseRecordService.Mapping
{
    public class PauseRecordMappingApplication : Profile
    {
        public PauseRecordMappingApplication()
        {
            CreateMap<PauseRecord, ReadPauseRecordDto>().ReverseMap();
            CreateMap<PauseRecord, UpdatePauseRecordDto>().ReverseMap();
            CreateMap<PauseRecord, CreatePauseRecordDto>().ReverseMap();
            CreateMap<PauseRecord, DeletePauseRecordDto>().ReverseMap();
        }
    }
}