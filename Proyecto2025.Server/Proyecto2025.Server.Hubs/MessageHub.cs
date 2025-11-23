using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Proyecto2025.Server.Hubs
{
    public class MessageHub : Hub
    {
        // Usar PascalCase para coincidir con SendAsync("JoinGroup", ...)
        public Task JoinGroup(string groupName) => Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        public Task LeaveGroup(string groupName) => Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Proyecto2025.Server.Hubs
{
    public class MessageHub : Hub
    {
        // Usar PascalCase para coincidir con SendAsync("JoinGroup", ...)
        public Task JoinGroup(string groupName) => Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        public Task LeaveGroup(string groupName) => Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}