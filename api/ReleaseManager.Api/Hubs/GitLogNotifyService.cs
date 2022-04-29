using Microsoft.AspNetCore.SignalR;

namespace ReleaseManager.Api.Hubs
{
    public class GitLogNotifyService
    {
        private readonly IHubContext<GitLogHub> _hub;

        public GitLogNotifyService(IHubContext<GitLogHub> hub)
        {
            _hub = hub;
        }

        public Task SendNotificationAsync(string message)
        {
            return _hub.Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
