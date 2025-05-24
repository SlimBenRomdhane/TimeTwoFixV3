using Microsoft.AspNetCore.Mvc;

namespace TimeTwoFix.Web.Controllers
{
    public class SharedController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
