using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Notification.Service
{
    public class RedisService : IRedisService
    {
        private readonly IServiceProvider serviceProvider;

        public RedisService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public void Run()
        {
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                while (true)
                {
                    INotificationService notificationService =
                        scope.ServiceProvider.GetRequiredService<INotificationService>();
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
