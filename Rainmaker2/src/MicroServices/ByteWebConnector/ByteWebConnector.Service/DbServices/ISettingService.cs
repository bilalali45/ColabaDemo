

using ByteWebConnector.Entity.Models;
using System.Collections.Generic;
using ByteWebConnector.Model.Models.OwnModels.Settings;

namespace ByteWebConnector.Service.DbServices
{
    public interface ISettingService : IServiceBase<ByteWebConnector.Entity.Models.Setting>
    {
        List<Entity.Models.Setting> GetSettingWithDetails(int? id= null,
                                                          string tag = null);


        ByteProSettings GetByteProSettings();
    }
}