using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Notification.Model;

namespace Notification.API
{
    public interface IClientHub
    {
        Task TestSignalR(string message);
        Task SendNotification(NotificationMediumModel model);
    }
    [Authorize(Roles ="MCU")]
    public class ServerHub : Hub<IClientHub>
    {
        public static readonly ClientConnection<int> _clientConnections = ClientConnection<int>.Current;
        
        public ServerHub()
        {
            
        }

        #region EventHandlers

        public override Task OnConnectedAsync()
        {
            var identity = Context.User;
            var connectionId = Context.ConnectionId;
            _clientConnections.Add(int.Parse(identity.FindFirst(type: "UserProfileId").Value),connectionId);
            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception ex)
        {
            var identity = Context.User;
            var connectionId = Context.ConnectionId;
            _clientConnections.Remove(int.Parse(identity.FindFirst(type: "UserProfileId").Value), connectionId);
            return base.OnDisconnectedAsync(null);
        }

        #endregion

        #region Methods

        public static async Task TestSignalR(IHubContext<ServerHub,IClientHub> hubContext)
        {
            await hubContext.Clients.All.TestSignalR("Hello");
        }

        public static async Task SendNotification(IHubContext<ServerHub, IClientHub> hubContext, int userId, NotificationMediumModel model)
        {
            var connections = _clientConnections.GetConnections(userId).ToList();
            await hubContext.Clients.Clients(connections).SendNotification(model);
        }
        #endregion
    }
}