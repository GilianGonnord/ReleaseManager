using Microsoft.AspNetCore.SignalR;

namespace ReleaseManager.Api.Hubs;

public class GitLogHub: Hub
{
    public async Task SendMessage(string user, string message) => await Clients.All.SendAsync("ReceiveMessage", user, message);

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}
