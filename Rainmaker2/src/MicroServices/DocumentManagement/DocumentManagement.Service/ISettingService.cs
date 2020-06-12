using DocumentManagement.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface ISettingService
    {
        Task<Setting> GetSetting();
    }
}
