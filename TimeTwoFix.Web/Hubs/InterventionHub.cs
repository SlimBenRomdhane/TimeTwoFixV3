using Microsoft.AspNetCore.SignalR;

namespace TimeTwoFix.Web.Hubs
{
    public class InterventionHub : Hub
    {
        public async Task SendInterventionUpdate(int interventionId, string newStatus)
        {
            await Clients.All.SendAsync("ReceiveInterventionUpdate", interventionId, newStatus);
        }
    }
}
