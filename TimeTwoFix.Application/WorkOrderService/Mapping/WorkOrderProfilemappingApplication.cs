using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTwoFix.Application.WorkOrderService.Dtos;
using TimeTwoFix.Core.Entities.WorkOrderManagement;

namespace TimeTwoFix.Application.WorkOrderService.Mapping
{
    public class WorkOrderProfilemappingApplication : Profile
    {
        public WorkOrderProfilemappingApplication()
        {
            CreateMap<WorkOrder, ReadWorkOrderDto>();
            CreateMap<CreateWorkOrderDto, WorkOrder>();
            CreateMap<UpdateWorkOrderDto, WorkOrder>();
            CreateMap<DeleteWorkOrderDto, WorkOrder>();

        }
    }
}
