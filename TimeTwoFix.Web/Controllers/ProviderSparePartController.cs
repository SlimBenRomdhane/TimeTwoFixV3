using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.ProviderSparePartServices.Dtos;
using TimeTwoFix.Application.ProviderSparePartServices.Interfaces;
using TimeTwoFix.Application.SparePartCategoryServices.Dtos;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Web.Models.ProviderSparePartModels;

namespace TimeTwoFix.Web.Controllers
{
    public class ProviderSparePartController : BaseController<ProviderSparePart
        , CreateProviderSparePartDto, ReadProviderSparePartDto, UpdateProviderSparePartDto, DeleteProviderSparePartDto
        , CreateProviderSparePartViewModel, ReadProviderSparePartViewModel, UpdateProviderSparePartViewModel, DeleteProviderSparePartViewModel>
    {
        private readonly IProviderSparePartService _providerSparePartService;
        public ProviderSparePartController(IProviderSparePartService providerSparePartService, IMapper mapper) : base(providerSparePartService, mapper)
        {
            _providerSparePartService = providerSparePartService;
        }
    }
}
