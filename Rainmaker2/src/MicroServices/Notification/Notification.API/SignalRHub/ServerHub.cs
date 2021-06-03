using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Notification.Model;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Notification.API
{
    public interface IClientHub
    {
        Task TestSignalR(string message);
        Task SendNotification(string model);
        Task NotificationSeen(long[] ids);
        Task NotificationRead(long[] ids);
        Task NotificationDelete(long id);
        Task NotificationDeleteAll();
    }
    [Authorize(Roles ="MCU")]
    public class ServerHub : Hub<IClientHub>
    {
        public static readonly ClientConnection<int> _clientConnections = ClientConnection<int>.Current;
        
        public ServerHub()
        {
            
        }

        #region EventHandlers

        public override async Task OnConnectedAsync()
        {
            var identity = Context.User;
            var connectionId = Context.ConnectionId;
            await _clientConnections.Add(int.Parse(identity.FindFirst(type: "UserProfileId").Value),connectionId,(IConnectionMultiplexer)Context.GetHttpContext().RequestServices.GetService(typeof(IConnectionMultiplexer)));
            await base.OnConnectedAsync();
        }


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var identity = Context.User;
            var connectionId = Context.ConnectionId;
            await _clientConnections.Remove(int.Parse(identity.FindFirst(type: "UserProfileId").Value), connectionId, (IConnectionMultiplexer)Context.GetHttpContext().RequestServices.GetService(typeof(IConnectionMultiplexer)));
            await base.OnDisconnectedAsync(exception);
        }

        #endregion

        #region Methods

        public static async Task TestSignalR(IHubContext<ServerHub,IClientHub> hubContext)
        {
            await hubContext.Clients.All.TestSignalR("Hello");
        }

        public static async Task SendNotification(IHubContext<ServerHub, IClientHub> hubContext, int userId, NotificationMediumModel model, IConnectionMultiplexer connectionMultiplexer)
        {
            var connections = (await _clientConnections.GetConnections(userId, connectionMultiplexer)).ToList();
            await hubContext.Clients.Clients(connections).SendNotification(JsonConvert.SerializeObject(model));
        }
        public static async Task NotificationSeen(IHubContext<ServerHub, IClientHub> hubContext, int userId, long[] ids, IConnectionMultiplexer connectionMultiplexer)
        {
            var connections = (await _clientConnections.GetConnections(userId, connectionMultiplexer)).ToList();
            await hubContext.Clients.Clients(connections).NotificationSeen(ids);
        }
        public static async Task NotificationRead(IHubContext<ServerHub, IClientHub> hubContext, int userId, long[] ids, IConnectionMultiplexer connectionMultiplexer)
        {
            var connections = (await _clientConnections.GetConnections(userId, connectionMultiplexer)).ToList();
            await hubContext.Clients.Clients(connections).NotificationRead(ids);
        }
        public static async Task NotificationDelete(IHubContext<ServerHub, IClientHub> hubContext, int userId, long id, IConnectionMultiplexer connectionMultiplexer)
        {
            var connections = (await _clientConnections.GetConnections(userId, connectionMultiplexer)).ToList();
            await hubContext.Clients.Clients(connections).NotificationDelete(id);
        }
        public static async Task NotificationDeleteAll(IHubContext<ServerHub, IClientHub> hubContext, int userId, IConnectionMultiplexer connectionMultiplexer)
        {
            var connections = (await _clientConnections.GetConnections(userId, connectionMultiplexer)).ToList();
            await hubContext.Clients.Clients(connections).NotificationDeleteAll();
        }
        #endregion
    }
}