using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Notification.API
{

    public class ClientConnection<T>
    {
        private ClientConnection()
        {
        }

        public async Task Add(T key, string connectionId, IConnectionMultiplexer connectionMultiplexer)
        {
            var database = connectionMultiplexer.GetDatabase();
            await database.ListLeftPushAsync($"ServerHub#{key}",connectionId);
        }

        public async Task<HashSet<string>> GetConnections(T key, IConnectionMultiplexer connectionMultiplexer)
        {
            HashSet<string> connections = new HashSet<string>();
            var database = connectionMultiplexer.GetDatabase();
            if (database != null)
            {
                var redisKeys = await database.ListRangeAsync($"ServerHub#{key}");
                if (redisKeys != null)
                {
                    redisKeys.Select(x => x.ToString()).ToList().ForEach(x => { connections.Add(x); });
                }
            }
            return connections;
        }

        public async Task Remove(T key, string connectionId, IConnectionMultiplexer connectionMultiplexer)
        {
            var database = connectionMultiplexer.GetDatabase();
            await database.ListRemoveAsync($"ServerHub#{key}", connectionId);
        }

        private static ClientConnection<T> _currentServiceUser=new ClientConnection<T>();

        public static ClientConnection<T> Current
        {
            get
            {
                return _currentServiceUser;
            }
        }
    }
}