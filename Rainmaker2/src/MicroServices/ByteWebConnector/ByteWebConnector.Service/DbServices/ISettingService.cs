

using System.Collections.Generic;
using ByteWebConnector.Entity.Models;
using Extensions.ExtensionClasses;
using URF.Core.Abstractions;

namespace ByteWebConnector.Service.DbServices
{
    public interface ISettingService : IServiceBase<Setting>
    {
        List<Setting> GetSettingWithDetails(int? id= null,
                                            string tag = null);


        SettingService.ByteProSettings GetByteProSettings();
    }
}