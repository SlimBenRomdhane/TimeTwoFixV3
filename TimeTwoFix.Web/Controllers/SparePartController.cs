using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.CategoryService.Interfaces;
using TimeTwoFix.Application.SparePartServices.Dtos;
using TimeTwoFix.Application.SparePartServices.Interfaces;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Web.Models.SparePartModels;

namespace TimeTwoFix.Web.Controllers
{
    public class SparePartController : BaseController<SparePart, CreateSparePartDto, ReadSparePartDto, UpdateSparePartDto, DeleteSparePartDto
        , CreateSparePartViewModel, ReadSparePartViewModel, UpdateSparePartViewModel, DeleteSparePartViewModel>
    {
        private readonly ISparePartService _sparePartService;

        public SparePartController(ISparePartService sparePartService, IMapper mapper) : base(sparePartService, mapper)
        {
            _sparePartService = sparePartService;
        }
    }
}
