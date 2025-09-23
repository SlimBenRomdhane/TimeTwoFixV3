using Microsoft.AspNetCore.SignalR;
using TimeTwoFix.Application.SparePartServices.Interfaces;
using TimeTwoFix.Web.Hubs;

namespace TimeTwoFix.Web.OtherTools
{
    public class StockChecker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubContext<SparePartHub> _hubContext;


        public StockChecker(IServiceScopeFactory serviceScopeFactory, IHubContext<SparePartHub> hubContext)
        {
            _scopeFactory = serviceScopeFactory;
            _hubContext = hubContext;

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var sparePart = scope.ServiceProvider.GetRequiredService<ISparePartService>();

                var now = DateTime.Now;
                var spareParts = (await sparePart.GetAllAsyncServiceGeneric()).Where(sp => sp.IsDeleted == false);

                foreach (var sp in spareParts.Where(sp => sp.QuantityInStock < 5))
                {
                    var message = $"⚠️ Low stock: <strong>{sp.Name}</strong> has only <strong>{sp.QuantityInStock}</strong> units left.";
                    await _hubContext.Clients.Group("InventoryManagers").SendAsync("ReceiveStockAlert", message);



                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Run every 60 minutes
            }

        }
    }
}
