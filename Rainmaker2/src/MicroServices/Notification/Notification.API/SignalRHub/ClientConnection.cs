using System.Collections.Generic;

namespace Notification.API
{

    public class ClientConnection<T>
    {
        private ClientConnection()
        {
        }

        public static readonly Dictionary<T, HashSet<string>> _connections =
            new Dictionary<T, HashSet<string>>();
        
        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                    connections.Add(connectionId);
                }
                else
                {
                    lock (connections)
                    {
                        connections.Add(connectionId);
                    }
                }
                
            }
        }

        public HashSet<string> GetConnections(T key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(key, out connections))
            {
                return connections;
            }
            return new HashSet<string>();
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }

        private static volatile ClientConnection<T> _currentServiceUser;

        public static ClientConnection<T> Current
        {
            get
            {
                lock (_connections)
                {
                    return _currentServiceUser ?? (_currentServiceUser = new ClientConnection<T>());
                }
            }
        }
    }
}