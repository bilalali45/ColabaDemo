using DocManager.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocManager.Service
{
    public interface ILockService
    {
        Task<Lock> AcquireLock(LockModel lockModel,int UserId,string UserName);
        Task<Lock> RetainLock(LockModel lockModel, int UserId, string UserName);
    }
}
