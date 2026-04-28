using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace API.SignalR
{
    [Authorize]
    public class NotificationHub : Hub
    {
        private static readonly ConcurrentDictionary<string, string> UserConnections = new();

        public override async Task OnConnectedAsync()
        {
            var email = Context.User.GetEmail();
            if (!string.IsNullOrEmpty(email))
                UserConnections[email] = Context.ConnectionId;

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var email = Context.User.GetEmail();
            if (!string.IsNullOrEmpty(email))
                UserConnections.TryRemove(email, out _);
            
            await base.OnDisconnectedAsync(exception);
        }

        public static string? GetConnectionId(string email)
        {
            if (UserConnections.TryGetValue(email, out var connectionId))
                return connectionId;
            return null;
        }

        public static string? GetConnectionIdByEmail(string email)
        {
            if (UserConnections.TryGetValue(email, out var connectionId))
                return connectionId;
            return null;
        }
    }
}
