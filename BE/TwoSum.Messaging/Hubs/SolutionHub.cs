using Microsoft.AspNetCore.SignalR;

namespace TwoSum.Messaging.Hubs;

public class SolutionHub : Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}
