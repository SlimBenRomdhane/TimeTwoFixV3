using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.CategoryService.Dtos;
using TimeTwoFix.Core.Entities.ServiceManagement;

namespace TimeTwoFix.Application.CategoryService.Interfaces
{
    public interface ICategoryService : IBaseService<Category>
    {
        Task<IEnumerable<ReadCategoryDto>> GetCategoryByName(string categoryName);
    }
}
