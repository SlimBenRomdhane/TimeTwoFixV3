using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTwoFix.Core.Common.Constants;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Web.Models.ProviderModels;
using TimeTwoFix.Application.ProviderServices.Dtos;
using TimeTwoFix.Application.ProviderServices.Interfaces;

namespace TimeTwoFix.Web.Controllers
{
    [Authorize(Roles = RoleNames.GeneralManager)]
    public class ProviderController : BaseController<Provider
        , CreateProviderDto, ReadProviderDto, UpdateProviderDto, DeleteProviderDto
        , CreateProviderViewModel, ReadProviderViewModel, UpdateProviderViewModel, DeleteProviderViewModel>
    {
        private readonly IProviderService _serviceProvider;
        public ProviderController(IProviderService serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
        {
            _serviceProvider = serviceProvider;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SearchProviders(string term)
        {
            if (string.IsNullOrWhiteSpace(term) || term.Length < 2)
            {
                return Json(new List<object>());
            }

            var results = await _serviceProvider.GetProviderByNameAsync(term);
            var viewModels = _mapper.Map<IEnumerable<ReadProviderViewModel>>(results);

            var response = viewModels.Select(p => new
            {
                id = p.Id,
                name = p.Name
            });

            return Json(response);
        }
        [AllowAnonymous]
        [Authorize(Roles = RoleNames.Combined.AllManagers)]
        [HttpGet]
        public override Task<IActionResult> Index()
        {
            return base.Index();
        }
    }
}
