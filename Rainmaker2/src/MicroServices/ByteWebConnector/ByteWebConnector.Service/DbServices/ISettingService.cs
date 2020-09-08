

using ByteWebConnector.Entity.Models;
using System.Collections.Generic;

namespace ByteWebConnector.Service.DbServices
{
    public interface ISettingService : IServiceBase<Setting>
    {
        List<Setting> GetSettingWithDetails(int? id= null,
                                            string tag = null);


        SettingService.ByteProSettings GetByteProSettings();
    }
}