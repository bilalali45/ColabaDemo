using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notification.Data;
using Notification.Entity.Models;
using Notification.Model;
using URF.Core.Abstractions;

namespace Notification.Service
{
    public class NotificationService : ServiceBase<NotificationContext,NotificationObject>,INotificationService
    {
        public NotificationService(IUnitOfWork<NotificationContext> previousUow,
            IServiceProvider services) : base(previousUow: previousUow,
            services: services)
        {
        }
        public async Task<long> Add(NotificationModel model, int userId, int tenantId)
        {
            return -1;
        }
    }
}
