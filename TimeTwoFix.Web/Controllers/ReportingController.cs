using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TimeTwoFix.Application.ReportingServices.Interfaces;
using TimeTwoFix.Web.Hubs;
using TimeTwoFix.Web.Models.ReportingModels;

namespace TimeTwoFix.Web.Controllers
{
    public class ReportingController : Controller
    {

        private readonly IReportingService _reportingService;
        private readonly IMapper _mapper;
        private readonly IHubContext<MechanicPerformanceHub> _mechanicHubContext;
        public ReportingController(IReportingService reportingService, IMapper mapper, IHubContext<MechanicPerformanceHub> hubContext)
        {
            _reportingService = reportingService;
            _mapper = mapper;
            _mechanicHubContext = hubContext;
        }
        public async Task<IActionResult> MechanicPerformance(DateTime? from, DateTime? to)
        {
            DateTime fromDate;
            DateTime toDate;

            // Apply defaults if missing
            //fromDate = from ?? DateTime.Now.AddMonths(-1);
            fromDate = from ?? new DateTime(DateTime.Now.Year, 1, 1);
            toDate = to ?? DateTime.Now;

            var dataDto = await _reportingService.GetMechanicPerformanceAsync(fromDate, toDate);
            var data = _mapper.Map<IEnumerable<MechanicPerformanceViewModel>>(dataDto);

            // Use yyyy-MM-dd for <input type="date"> compatibility
            ViewData["From"] = fromDate.ToString("yyyy-MM-dd");
            ViewData["To"] = toDate.ToString("yyyy-MM-dd");

            return View(data);
        }
        public async Task<IActionResult> MechanicPerformanceTrend(DateTime? from, DateTime? to)
        {
            //var fromDate = from ?? DateTime.Now.AddMonths(-6);
            var fromDate = from ?? new DateTime(DateTime.Now.Year, 1, 1);
            var toDate = to ?? DateTime.Now;

            var dto = await _reportingService.GetMechanicPerformanceTrendAsync(fromDate, toDate);
            var vm = _mapper.Map<IEnumerable<MechanicPerformanceTrendViewModel>>(dto);

            ViewData["From"] = fromDate.ToString("yyyy-MM-dd");
            ViewData["To"] = toDate.ToString("yyyy-MM-dd");

            return View(vm);
        }
        public async Task<IActionResult> TopCustomers(DateTime? from, DateTime? to)
        {
            DateTime fromDate;
            DateTime toDate;


            // Apply defaults if missing
            //fromDate = from ?? DateTime.Now.AddMonths(-1);
            fromDate = from ?? new DateTime(DateTime.Now.Year, 1, 1);
            toDate = to ?? DateTime.Now;

            var dataDto = await _reportingService.GetTopCustomersAsync(fromDate, toDate, top: 10);
            var data = _mapper.Map<IEnumerable<CustomerInsightViewModel>>(dataDto);

            ViewData["From"] = fromDate.ToString("yyyy-MM-dd");
            ViewData["To"] = toDate.ToString("yyyy-MM-dd");

            return View(data);
        }
        public async Task<IActionResult> TopConsumedParts(DateTime? from, DateTime? to)
        {
            //var fromDate = from ?? DateTime.Now.AddMonths(-1);
            var fromDate = from ?? new DateTime(DateTime.Now.Year, 1, 1);
            var toDate = to ?? DateTime.Now;

            var dto = await _reportingService.GetTopConsumedPartsAsync(fromDate, toDate);
            var vm = _mapper.Map<IEnumerable<PartConsumptionViewModel>>(dto);

            ViewData["From"] = fromDate.ToString("yyyy-MM-dd");
            ViewData["To"] = toDate.ToString("yyyy-MM-dd");

            return View(vm);
        }
        public async Task<IActionResult> PauseAnalysis(DateTime? from, DateTime? to)
        {
            //var fromDate = from ?? DateTime.Now.AddMonths(-1);
            var fromDate = from ?? new DateTime(DateTime.Now.Year, 1, 1);
            var toDate = to ?? DateTime.Now;

            var dto = await _reportingService.GetPauseAnalysisAsync(fromDate, toDate);
            var vm = _mapper.Map<IEnumerable<PauseAnalysisViewModel>>(dto);

            ViewData["From"] = fromDate.ToString("yyyy-MM-dd");
            ViewData["To"] = toDate.ToString("yyyy-MM-dd");

            return View(vm);
        }
        public async Task<IActionResult> PauseAnalysisTrend(DateTime? from, DateTime? to)
        {
            //var fromDate = from ?? DateTime.Now.AddMonths(-3);
            var fromDate = from ?? new DateTime(DateTime.Now.Year, 1, 1);
            var toDate = to ?? DateTime.Now;

            var dto = await _reportingService.GetPauseAnalysisTrendAsync(fromDate, toDate);
            var vm = _mapper.Map<IEnumerable<PauseAnalysisTrendViewModel>>(dto);

            ViewData["From"] = fromDate.ToString("yyyy-MM-dd");
            ViewData["To"] = toDate.ToString("yyyy-MM-dd");

            return View(vm);
        }
        public async Task<IActionResult> PaymentAging(DateTime? asOfDate)
        {

            var toDate = asOfDate ?? DateTime.Now;

            var dto = await _reportingService.GetPaymentAgingAsync(toDate);
            var vm = _mapper.Map<IEnumerable<PaymentAgingViewModel>>(dto);

            ViewData["AsOfDate"] = toDate.ToString("yyyy-MM-dd");

            return View(vm);
        }

        public async Task<IActionResult> RevenueByMonth(DateTime? from, DateTime? to)
        {
            //var fromDate = from ?? DateTime.Now.AddYears(-1);
            var fromDate = from ?? new DateTime(DateTime.Now.Year, 1, 1);
            var toDate = to ?? DateTime.Now;

            var dto = await _reportingService.GetRevenueByMonthAsync(fromDate, toDate);
            var vm = _mapper.Map<IEnumerable<RevenueByMonthViewModel>>(dto);


            ViewData["From"] = fromDate.ToString("yyyy-MM-dd");
            ViewData["To"] = toDate.ToString("yyyy-MM-dd");

            return View(vm);
        }




























        public async Task BroadcastPerformance(DateTime? from = null, DateTime? to = null)
        {
            var dataDto = await _reportingService.GetMechanicPerformanceAsync(from ?? DateTime.Now.AddMonths(-1), to ?? DateTime.Now);
            var data = _mapper.Map<IEnumerable<MechanicPerformanceViewModel>>(dataDto);
            await _mechanicHubContext.Clients.All.SendAsync("UpdatePerformance", data);
        }


    }
}
