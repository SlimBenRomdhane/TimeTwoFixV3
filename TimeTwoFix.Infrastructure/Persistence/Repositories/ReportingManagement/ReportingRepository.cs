using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //return await _timeTwoFixDbContext.Interventions
            //    .Include(i => i.Mechanic)
            //    .Where(i => i.StartDate >= from && i.StartDate <= to && i.Status == "Completed")
            //    .GroupBy(i => new { i.MechanicId, i.Mechanic.FirstName, i.Mechanic.LastName })
            //    .Select(g => new MechanicPerformanceResult
            //    {
            //        MechanicId = g.Key.MechanicId,
            //        MechanicName = g.Key.FirstName + " " + g.Key.LastName,
            //        JobsCompleted = g.Count(),
            //        AverageCompletionHours = g.Average(i => i.ActualTimeSpent.HasValue ? i.ActualTimeSpent.Value.TotalHours : 0),
            //        TotalRevenue = g.Sum(i => i.InterventionPrice +
            //            i.InterventionSpareParts.Sum(sp => sp.Quantity * sp.SparePart.UnitPrice))
            //    })
            //    .ToListAsync();
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

        public async Task<IEnumerable<PaymentAgingResult>> GetPaymentAgingAsync(DateTime asOfDate)
        {
            return await _timeTwoFixDbContext.WorkOrders
                .Include(w => w.Vehicle).ThenInclude(v => v.Client)
                .Where(w => !w.Paid)
                .Select(w => new PaymentAgingResult
                {
                    WorkOrderId = w.Id,
                    ClientName = w.Vehicle.Client.FirstName + " " + w.Vehicle.Client.LastName,
                    AmountDue = w.TolalLaborCost,
                    DaysOutstanding = EF.Functions.DateDiffDay(w.EndDate.ToDateTime(w.EndTime), asOfDate),
                    AgingBucket =
                        EF.Functions.DateDiffDay(w.EndDate.ToDateTime(w.EndTime), asOfDate) <= 30 ? "0-30" :
                        EF.Functions.DateDiffDay(w.EndDate.ToDateTime(w.EndTime), asOfDate) <= 60 ? "31-60" :
                        EF.Functions.DateDiffDay(w.EndDate.ToDateTime(w.EndTime), asOfDate) <= 90 ? "61-90" : "90+"
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<RevenueByMonthResult>> GetRevenueByMonthAsync(DateTime from, DateTime to)
        {
            return await _timeTwoFixDbContext.WorkOrders
                .Where(w => w.CreatedAt >= from && w.CreatedAt <= to && w.Status == "Completed")
                .GroupBy(w => new { w.PaymentDate!.Value.Year, w.PaymentDate!.Value.Month })
                .Select(g => new RevenueByMonthResult
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Revenue = g.Sum(w => w.TolalLaborCost)
                })
                .OrderBy(r => r.Year).ThenBy(r => r.Month)
                .ToListAsync();
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
