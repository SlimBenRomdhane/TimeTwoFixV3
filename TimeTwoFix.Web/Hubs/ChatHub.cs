using Microsoft.AspNetCore.SignalR;
namespace TimeTwoFix.Web.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string receiverUserName, string message)
        {
            //var senderUserId = Context.UserIdentifier; // comes from IUserIdProvider

            var senderName = Context.User?.Identity?.Name
            ?? Context.User?.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value
            ?? "Unknown";

            await Clients.User(receiverUserName)
                .SendAsync("ReceiveMessage", senderName, senderName, message);
            // Also send to sender so they see their own message
            await Clients.User(senderName)
            .SendAsync("ReceiveMessage", receiverUserName, "You", message);

        }


        public override Task OnConnectedAsync()
        {
            // Optional: track presence or log connection
            Console.WriteLine($"Connected: {Context.UserIdentifier}");

            return base.OnConnectedAsync();
        }
    }
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            //return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return connection.User?.Identity?.Name;

        }
    }

}
