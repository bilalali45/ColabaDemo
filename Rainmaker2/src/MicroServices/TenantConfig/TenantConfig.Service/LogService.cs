using ColabaLog.Data;
using ColabaLog.Entity.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using TenantConfig.Common;
using URF.Core.Abstractions;

namespace TenantConfig.Service
{

    public  class LogService : ServiceBase<ColabaLogContext, ContactLog>, ILogService
    {

        public LogService(IUnitOfWork<ColabaLogContext> previousUow, IServiceProvider services) : base(previousUow, services)
        {
        }
        public async Task InsertLogContactEmail(string FirstName, string LastName, string Email, int tenantId)
        {
            ContactLog contactLog = new ContactLog
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                CreatedOn = DateTime.UtcNow,
                TenantId = tenantId
            };
            Uow.Repository<ContactLog>().Insert(contactLog);
            await Uow.SaveChangesAsync();
        }

        public async Task InsertLogContactEmailPhone(string FirstName, string LastName, string Email, string phone, int tenantId)
        {
            ContactLog contactLog = new ContactLog
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                CreatedOn = DateTime.UtcNow,
                TenantId = tenantId,
                PhoneNumber= PhoneHelper.UnMask(phone)
            };
            Uow.Repository<ContactLog>().Insert(contactLog);
            await Uow.SaveChangesAsync();
        }
    }
}
