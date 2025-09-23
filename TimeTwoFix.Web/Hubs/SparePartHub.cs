using Microsoft.AspNetCore.SignalR;

namespace TimeTwoFix.Web.Hubs
{
    public class SparePartHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var user = Context.User;
            if (user.IsInRole("WareHouseManager") || user.IsInRole("WorkshopManager"))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "InventoryManagers");
            }


            await base.OnConnectedAsync();
        }

    }
}
