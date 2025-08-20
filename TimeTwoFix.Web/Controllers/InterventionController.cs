using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTwoFix.Application.InterventionService.Dtos;
using TimeTwoFix.Application.InterventionService.Interfaces;
using TimeTwoFix.Core.Entities.WorkOrderManagement;
using TimeTwoFix.Web.Models.InterventionModels;

namespace TimeTwoFix.Web.Controllers
{
    public class InterventionController : BaseController<Intervention, CreateInterventionDto, ReadInterventionDto, UpdateInterventionDto, DeleteInterventionDto,
        CreateInterventionViewModel, ReadInterventionViewModel, UpdateInterventionViewModel, DeleteInterventionViewModel>

    {
        private readonly IInterventionService _interventionService;
        public InterventionController(IInterventionService interventionService, IMapper mapper) : base(interventionService, mapper)
        {
            _interventionService = interventionService;
        }

        //public override IActionResult Create()
        //{

        //    return base.Create();
        //}
    }




}
