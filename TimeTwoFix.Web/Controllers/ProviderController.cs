using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.ProviderServices.Dtos;
using TimeTwoFix.Application.ProviderServices.Interfaces;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Web.Models.ProviderModels;

namespace TimeTwoFix.Web.Controllers
{
    public class ProviderController : BaseController<Provider
        , CreateProviderDto, ReadProviderDto, UpdateProviderDto, DeleteProviderDto
        , CreateProviderViewModel, ReadProviderViewModel, UpdateProviderViewModel, DeleteProviderViewModel>
    {
        private readonly IProviderService _serviceProvider;
        public ProviderController(IProviderService serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
