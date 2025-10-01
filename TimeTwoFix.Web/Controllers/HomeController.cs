using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TimeTwoFix.Web.Models;

namespace TimeTwoFix.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var roleClaims = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => $"{c.Type}: {c.Value}");
            Console.WriteLine($"Role Claims: {string.Join(", ", roleClaims)}");
            Console.WriteLine($"Using connection: {_configuration.GetConnectionString("AzureStorage")}");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}