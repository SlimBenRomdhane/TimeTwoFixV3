using Microsoft.AspNetCore.SignalR;
using TimeTwoFix.Application.InterventionService.Interfaces;
using TimeTwoFix.Web.Hubs;

namespace TimeTwoFix.Web.OtherTools
{
    public class InterventionStatusUpdater : BackgroundService
    {
        private readonly IHubContext<InterventionHub> _hubContext;
        private readonly IServiceScopeFactory _scopeFactory;

        public InterventionStatusUpdater(
            IHubContext<InterventionHub> hubContext,
            IServiceScopeFactory scopeFactory)
        {
            _hubContext = hubContext;
            _scopeFactory = scopeFactory;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var interventionService = scope.ServiceProvider.GetRequiredService<IInterventionService>();

                var now = DateTime.Now;
                var interventions = (await interventionService.GetAllAsyncServiceGeneric()).Where(i => i.Status == "Planned");

                foreach (var intervention in interventions.Where(i => i.StartDate <= now))
                {
                    intervention.Status = "In Progress";
                    intervention.UpdatedAt = DateTime.Now;
                    intervention.UpdatedBy = "AutoUpdate"; // Or any other system identifier
                    await interventionService.UpdateAsyncServiceGeneric(intervention);
                    await _hubContext.Clients.All.SendAsync("ReceiveInterventionUpdate", intervention.Id, intervention.Status);
                }

                await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken); // Run every 15 minutes
            }
        }
    }
}
