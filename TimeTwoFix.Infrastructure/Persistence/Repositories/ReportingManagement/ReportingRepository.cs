using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Interfaces.Repositories.ReportingManagement;
using TimeTwoFix.Core.Interfaces.Repositories.ReportingModels;

namespace TimeTwoFix.Infrastructure.Persistence.Repositories.ReportingManagement
{
    public class ReportingRepository : IReportingRepository
    {
        private readonly TimeTwoFixDbContext _timeTwoFixDbContext;
        public ReportingRepository(TimeTwoFixDbContext timeTwoFixDbContext)
        {
            _timeTwoFixDbContext = timeTwoFixDbContext;
        }

        public async Task<IEnumerable<MechanicPerformanceResult>> GetMechanicPerformanceAsync(DateTime from, DateTime to)
        {

            var data = await _timeTwoFixDbContext.Interventions
                    .Include(i => i.Mechanic)
                    .Where(i => i.StartDate >= from && i.StartDate <= to && i.Status == "Completed")
                    .Select(i => new
                    {
                        i.MechanicId,
                        i.Mechanic.FirstName,
                        i.Mechanic.LastName,
                        i.ActualTimeSpent,
                        Revenue = i.InterventionPrice +
                                  i.InterventionSpareParts.Sum(sp => sp.Quantity * sp.SparePart.UnitPrice)
                    })
                    .ToListAsync(); // materialize first

            return data
                .GroupBy(x => new { x.MechanicId, x.FirstName, x.LastName })
                .Select(g => new MechanicPerformanceResult
                {
                    MechanicId = g.Key.MechanicId,
                    MechanicName = g.Key.FirstName + " " + g.Key.LastName,
                    JobsCompleted = g.Count(),
                    AverageCompletionHours = g.Average(x => x.ActualTimeSpent?.TotalHours ?? 0),
                    TotalRevenue = g.Sum(x => x.Revenue)
                })
                .ToList();

        }

        public async Task<IEnumerable<MechanicPerformanceTrendResult>> GetMechanicPerformanceTrendAsync(DateTime from, DateTime to)
        {
            var data = await _timeTwoFixDbContext.Interventions
                .Include(i => i.Mechanic)
                .Where(i => i.StartDate >= from && i.StartDate <= to && i.Status == "Completed")
                .Select(i => new
                {
                    i.MechanicId,
                    i.Mechanic.FirstName,
                    i.Mechanic.LastName,
                    i.StartDate,
                    i.ActualTimeSpent,
                    Revenue = i.InterventionPrice +
                              i.InterventionSpareParts.Sum(sp => sp.Quantity * sp.SparePart.UnitPrice)
                })
                .ToListAsync();

            return data
                .GroupBy(x => new { x.MechanicId, x.FirstName, x.LastName, x.StartDate.Year, x.StartDate.Month })
                .Select(g => new MechanicPerformanceTrendResult
                {
                    MechanicId = g.Key.MechanicId,
                    MechanicName = g.Key.FirstName + " " + g.Key.LastName,
                    Period = new DateTime(g.Key.Year, g.Key.Month, 1),
                    JobsCompleted = g.Count(),
                    AverageCompletionHours = (decimal)g.Average(x => x.ActualTimeSpent?.TotalHours ?? 0),
                    TotalRevenue = g.Sum(x => x.Revenue)
                })
                .OrderBy(r => r.Period)
                .ToList();
        }

        public async Task<IEnumerable<PauseAnalysisResult>> GetPauseAnalysisAsync(DateTime from, DateTime to)
        {
            return await _timeTwoFixDbContext.PauseRecords
                .Where(p => p.StartTime >= from && p.EndTime != null && p.EndTime <= to)
                .GroupBy(p => p.Reason)
                .Select(g => new PauseAnalysisResult
                {
                    Reason = g.Key,
                    Occurrences = g.Count(),
                    TotalHoursLost = (double)(g.Sum(p => EF.Functions.DateDiffMinute(p.StartTime, p.EndTime)) / 60.0),
                    AveragePauseMinutes = (double)g.Average(p => EF.Functions.DateDiffMinute(p.StartTime, p.EndTime))
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PauseAnalysisTrendResult>> GetPauseAnalysisTrendAsync(DateTime from, DateTime to)
        {


            // Step 1: Pull raw data from EF (only simple fields EF can translate)
            var rawPauses = await _timeTwoFixDbContext.PauseRecords
                .Where(p => p.StartTime >= from && p.StartTime <= to)
                .Select(p => new
                {
                    p.Reason,
                    p.StartTime,
                    p.EndTime
                })
                .ToListAsync(); // 👈 ensures EF only executes what it can translate

            // Step 2: Do the time math and grouping in memory
            var grouped = rawPauses
                .Select(p => new
                {
                    p.Reason,
                    Period = new DateTime(p.StartTime.Year, p.StartTime.Month, 1),
                    DurationHours = p.EndTime.HasValue
                        ? (p.EndTime.Value - p.StartTime).TotalMinutes / 60.0
                        : 0
                })
                .GroupBy(x => new { x.Reason, x.Period })
                .Select(g => new PauseAnalysisTrendResult
                {
                    Reason = g.Key.Reason,
                    Period = g.Key.Period,
                    HoursLost = g.Sum(x => x.DurationHours)
                })
                .OrderBy(r => r.Period)
                .ToList();

            return grouped;

        }

        public async Task<IEnumerable<PaymentAgingResult>> GetPaymentAgingAsync(DateTime asOfDate)
        {
            var rawData = await _timeTwoFixDbContext.WorkOrders
                .Include(w => w.Vehicle).ThenInclude(v => v.Client)
                .Where(w => !w.Paid)
                .Select(w => new
                {
                    w.Id,
                    ClientName = w.Vehicle.Client.FirstName + " " + w.Vehicle.Client.LastName,
                    AmountDue = w.TolalLaborCost,
                    EndDateTime = w.EndDate, // capture raw DateOnly
                    EndTime = w.EndTime      // capture raw TimeOnly
                })
                .ToListAsync();

            // Now compute DateDiffDay safely in memory
            return rawData.Select(x =>
            {
                var endDateTime = x.EndDateTime.ToDateTime(x.EndTime);
                var daysOutstanding = (asOfDate.Date - endDateTime.Date).Days;

                return new PaymentAgingResult
                {
                    WorkOrderId = x.Id,
                    ClientName = x.ClientName,
                    AmountDue = x.AmountDue,
                    DaysOutstanding = daysOutstanding,
                    AgingBucket =
                        daysOutstanding <= 30 ? "0-30" :
                        daysOutstanding <= 60 ? "31-60" :
                        daysOutstanding <= 90 ? "61-90" : "90+"
                };
            }).ToList();
        }

        public async Task<IEnumerable<RevenueByMonthResult>> GetRevenueByMonthAsync(DateTime from, DateTime to)
        {
            var toInclusive = to.Date.AddDays(1).AddTicks(-1);

            // Query actual revenue data
            var revenueData = await _timeTwoFixDbContext.WorkOrders
                .Where(w => w.PaymentDate != null
                         && w.PaymentDate >= from
                         && w.PaymentDate <= toInclusive
                         && w.Status == "Completed")
                .GroupBy(w => new { w.PaymentDate.Value.Year, w.PaymentDate.Value.Month })
                .Select(g => new RevenueByMonthResult
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Revenue = g.Sum(w => w.TolalLaborCost)
                })
                .ToListAsync();

            // Build full month sequence
            var results = new List<RevenueByMonthResult>();
            var current = new DateTime(from.Year, from.Month, 1);

            while (current <= to)
            {
                var match = revenueData.FirstOrDefault(r => r.Year == current.Year && r.Month == current.Month);

                results.Add(new RevenueByMonthResult
                {
                    Year = current.Year,
                    Month = current.Month,
                    Revenue = match?.Revenue ?? 0m
                });

                current = current.AddMonths(1);
            }

            return results;
        }

        public async Task<IEnumerable<ServiceCategoryResult>> GetRevenueByServiceCategoryAsync(DateTime from, DateTime to)
        {
            return await _timeTwoFixDbContext.Interventions
                .Include(i => i.Service).ThenInclude(s => s.Category)
                .Where(i => i.StartDate >= from && i.StartDate <= to && i.Status == "Completed")
                .GroupBy(i => i.Service.Category.Name)
                .Select(g => new ServiceCategoryResult
                {
                    CategoryName = g.Key,
                    WorkOrderCount = g.Count(),
                    TotalRevenue = g.Sum(i => i.InterventionPrice +
                        i.InterventionSpareParts.Sum(sp => sp.Quantity * sp.SparePart.UnitPrice))
                })
                .OrderByDescending(r => r.TotalRevenue)
                .ToListAsync();
        }

        public async Task<IEnumerable<SupplierSpendResult>> GetSupplierSpendAsync(DateTime from, DateTime to)
        {
            return await _timeTwoFixDbContext.ProviderSpareParts
                .Include(psp => psp.Provider)
                .Where(psp => psp.CreatedAt >= from && psp.CreatedAt <= to)
                .GroupBy(psp => new { psp.ProviderId, psp.Provider.Name })
                .Select(g => new SupplierSpendResult
                {
                    ProviderId = g.Key.ProviderId,
                    ProviderName = g.Key.Name,
                    TotalSpend = g.Sum(psp => psp.QuantityReceived * psp.UnitPriceAtPurchase),
                    DeliveriesCount = g.Count()
                })
                .OrderByDescending(s => s.TotalSpend)
                .ToListAsync();
        }

        public async Task<IEnumerable<PartConsumptionResult>> GetTopConsumedPartsAsync(DateTime from, DateTime to, int top = 10)
        {
            return await _timeTwoFixDbContext.InterventionSpareParts
                .Include(sp => sp.SparePart)
                .Include(sp => sp.Intervention)
                .Where(sp => sp.Intervention.StartDate >= from && sp.Intervention.StartDate <= to)
                .GroupBy(sp => new { sp.SparePartId, sp.SparePart.PartCode, sp.SparePart.Name })
                .Select(g => new PartConsumptionResult
                {
                    SparePartId = g.Key.SparePartId,
                    PartCode = g.Key.PartCode,
                    PartName = g.Key.Name,
                    QuantityUsed = g.Sum(sp => sp.Quantity),
                    TotalValue = g.Sum(sp => sp.Quantity * sp.SparePart.UnitPrice)
                })
                .OrderByDescending(p => p.QuantityUsed)
                .Take(top)
                .ToListAsync();
        }

        public async Task<IEnumerable<CustomerInsightResult>> GetTopCustomersAsync(DateTime from, DateTime to, int top = 10)
        {
            return await _timeTwoFixDbContext.WorkOrders
                .Include(w => w.Vehicle).ThenInclude(v => v.Client)
                .Where(w => w.CreatedAt >= from && w.CreatedAt <= to && w.Status == "Completed")
                .GroupBy(w => new { w.Vehicle.ClientId, w.Vehicle.Client.FirstName, w.Vehicle.Client.LastName })
                .Select(g => new CustomerInsightResult
                {
                    CustomerId = g.Key.ClientId,
                    CustomerName = g.Key.FirstName + " " + g.Key.LastName,
                    TotalVisits = g.Count(),
                    TotalSpend = g.Sum(w => w.TolalLaborCost),
                    AverageInvoice = g.Average(w => w.TolalLaborCost)
                })
                .OrderByDescending(c => c.TotalSpend)
                .Take(top)
                .ToListAsync();
        }

        public async Task<IEnumerable<VehicleInsightResult>> GetTopVehiclesAsync(DateTime from, DateTime to, int top = 10)
        {
            return await _timeTwoFixDbContext.WorkOrders
                .Include(w => w.Vehicle)
                .Where(w => w.CreatedAt >= from && w.CreatedAt <= to && w.Status == "Completed")
                .GroupBy(w => new { w.VehicleId, w.Vehicle.LicensePlate, w.Vehicle.Brand, w.Vehicle.Model, w.Vehicle.Vin })
                .Select(g => new VehicleInsightResult
                {
                    VIN = g.Key.Vin,
                    Brand = g.Key.Brand,
                    Model = g.Key.Model,
                    WorkOrderCount = g.Count(),
                    AverageMileage = g.Average(w => w.Vehicle.Mileage),
                    TotalSpend = g.Sum(w => w.TolalLaborCost)
                })
                .OrderByDescending(v => v.TotalSpend)
                .Take(top)
                .ToListAsync();
        }

        public async Task<WorkOrderSummaryResult> GetWorkOrderSummaryAsync(DateTime from, DateTime to)
        {
            var query = _timeTwoFixDbContext.WorkOrders
                .Include(w => w.Interventions)
                .ThenInclude(i => i.InterventionSpareParts)
                .Where(w => w.CreatedAt >= from && w.CreatedAt <= to);
            var closed = query.Where(w => w.Status == "Completed");
            var avgHours = await closed
                .Select(w => EF.Functions.DateDiffHour(w.StartDate.ToDateTime(w.StartTime), w.EndDate.ToDateTime(w.EndTime)))
                .Where(h => h != null)
                .AverageAsync(h => (double)h!);

            return new WorkOrderSummaryResult
            {
                TotalCreated = await query.CountAsync(),
                TotalClosed = await closed.CountAsync(),
                AverageDurationHours = double.IsNaN(avgHours) ? 0 : avgHours,
                PaidCount = await query.CountAsync(w => w.Paid == true),
                UnpaidCount = await query.CountAsync(w => w.Paid == false)
            };
        }
    }
}
