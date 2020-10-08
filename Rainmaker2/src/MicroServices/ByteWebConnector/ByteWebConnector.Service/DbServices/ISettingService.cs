

using ByteWebConnector.Entity.Models;
using System.Collections.Generic;
using ByteWebConnector.Model.Models.Settings;

namespace ByteWebConnector.Service.DbServices
{
    public interface ISettingService : IServiceBase<Setting>
    {
        List<Setting> GetSettingWithDetails(int? id= null,
                                            string tag = null);


        ByteProSettings GetByteProSettings();
    }
}