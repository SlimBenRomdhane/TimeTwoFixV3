using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTwoFix.Application.ReportingServices.Interfaces;

namespace TimeTwoFix.Web.Controllers
{
    public class ReportingController : Controller
    {

        private readonly IReportingService _reportingService;
        private readonly IMapper _mapper;
        public ReportingController(IReportingService reportingService, IMapper mapper)
        {
            _reportingService = reportingService;
            _mapper = mapper;
        }
        public async Task<IActionResult> MechanicPerformance(DateTime from, DateTime to)
        {
            // Provide defaults if user didn’t select anything
            var fromDate = from;
            var toDate = to;


            var data = await _reportingService.GetMechanicPerformanceAsync(from, to);
            ViewData["From"] = fromDate.ToString("yyyy-MM-dd");
            ViewData["To"] = toDate.ToString("yyyy-MM-dd");

            return View(data); // pass DTOs directly to the view
        }

    }
}
