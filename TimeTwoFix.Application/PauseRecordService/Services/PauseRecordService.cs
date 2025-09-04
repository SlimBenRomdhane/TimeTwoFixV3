using AutoMapper;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.PauseRecordService.Interfaces;
using TimeTwoFix.Core.Entities.WorkOrderManagement;
using TimeTwoFix.Core.Interfaces;

namespace TimeTwoFix.Application.PauseRecordService.Services
{
    public class PauseRecordService : BaseService<PauseRecord>, IPauseRecordService
    {
        public PauseRecordService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
