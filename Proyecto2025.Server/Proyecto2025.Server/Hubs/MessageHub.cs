using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
namespace Proyecto2025.Server.Hubs
{
    public class MessageHub : Hub
    {
        public Task Joingroup(string groupName) => Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        public Task Leavegroup(string groupName) => Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}
