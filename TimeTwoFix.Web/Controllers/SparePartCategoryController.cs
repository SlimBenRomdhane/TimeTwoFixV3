using AutoMapper;
using TimeTwoFix.Application.SparePartCategoryServices.Dtos;
using TimeTwoFix.Application.SparePartCategoryServices.Interfaces;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Web.Models.SparePartCategoryModel;

namespace TimeTwoFix.Web.Controllers
{
    public class SparePartCategoryController : BaseController<SparePartCategory
        , CreateSparePartCategoryDto, ReadSparePartCategoryDto, UpdateSparePartCategoryDto, DeleteSparePartCategoryDto
        , CreateSparePartCategoryViewModel, ReadSparePartCategoryViewModel, UpdateSparePartCategoryViewModel, DeleteSparePartCategoryViewModel>

    {
        private readonly ISparePartCategoryService _sparePartCategoryService;
        public SparePartCategoryController(ISparePartCategoryService sparePartCategoryService, IMapper mapper) : base(sparePartCategoryService, mapper)
        {
            _sparePartCategoryService = sparePartCategoryService;
        }
    }
}
