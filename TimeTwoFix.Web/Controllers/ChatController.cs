using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.UserManagement;

namespace TimeTwoFix.Web.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ChatController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<IActionResult> UserList()
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                //return Content(string.Empty);
                return PartialView("_UserDropdownNotLoggedIn");

            }

            var currentUserId = int.Parse(_userManager.GetUserId(User));

            var users = await _userManager.Users
                .Where(u => u.Id != currentUserId && u.Status == "Active")
                .Select(u => new SelectListItem
                {
                    //Value = u.Id.ToString(),
                    Value = u.UserName,
                    Text = $"{u.UserName} ({u.Email})"
                })
                .ToListAsync();

            return PartialView("_UserDropdown", users);
            //return Json(users);
        }

    }
}
