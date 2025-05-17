using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Core.Entities.WorkOrderManagement;
using TimeTwoFix.Core.Interfaces.Repositories.WorkOrderManagement;
using TimeTwoFix.Infrastructure.Persistence.Repositories.Base;

namespace TimeTwoFix.Infrastructure.Persistence.Repositories.WorkOrderManagement
{
    public class InterventionRepository : BaseRepository<Intervention>, IInterventionRepository
    {
        public InterventionRepository(TimeTwoFixDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Intervention>> GetInterventionsByWorkOrderIdAsync(int workOrderId)
        {
            var interventions = await _context.Interventions
                .Where(i => i.WorkOrderId == workOrderId)
                .ToListAsync();
            return interventions;
        }

        public async Task<IEnumerable<Intervention>> GetInterventionsByMechanicIdAsync(int mechanicId)
        {
            var interventions = await _context.Interventions
                .Where(i => i.MechanicId == mechanicId)
                .ToListAsync();
            return interventions;
        }

        public async Task<IEnumerable<Intervention>> GetInterventionsByServiceIdAsync(int serviceId)
        {
            var interventions = await _context.Interventions
                .Where(i => i.ServiceId == serviceId)
                .ToListAsync();
            return interventions;
        }

        public async Task<IEnumerable<Intervention>> GetInterventionsByLiftingBridgeIdAsync(int liftingBridgeId)
        {
            var interventions = await _context.Interventions
                .Where(i => i.LiftingBridgeId == liftingBridgeId)
                .ToListAsync();
            return interventions;
        }

        public async Task<IEnumerable<Intervention>> GetInterventionsByStatusAsync(string status)
        {
            var interventions = await _context.Interventions
                .Where(i => i.Status.Contains(status))
                .ToListAsync();
            return interventions;
        }

        public async Task<IEnumerable<Intervention>> GetInterventionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var interventions = await _context.Interventions
                .Where(i => i.StartDate >= startDate && i.EndDate <= endDate)
                .ToListAsync();
            return interventions;
        }
    }
}