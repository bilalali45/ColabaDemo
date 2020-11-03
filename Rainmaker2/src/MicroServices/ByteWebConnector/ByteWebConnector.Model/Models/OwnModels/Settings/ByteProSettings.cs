using System.Collections.Generic;
using System.Linq;
using ByteWebConnector.Entity.Models;

namespace ByteWebConnector.Model.Models.OwnModels.Settings
{

    public class ByteProSettings
    {
        public ByteProSettings(List<Entity.Models.Setting> settings)
        {
            ByteApiAuthKey = settings.Single(predicate: s => s.Name == "ByteApiAuthKey").Value;
            ByteApiUrl = settings.Single(predicate: s => s.Name == "ByteApiUrl").Value;
            ByteAuthKey = settings.Single(predicate: s => s.Name == "ByteAuthKey").Value;
            ByteCompanyCode = settings.Single(predicate: s => s.Name == "ByteCompanyCode").Value;
            ByteApiPassword = settings.Single(predicate: s => s.Name == "ByteApiPassword").Value;
            ByteApiUserName = settings.Single(predicate: s => s.Name == "ByteApiUserName").Value;
            BytePassword = settings.Single(predicate: s => s.Name == "BytePassword").Value;
            ByteUserName = settings.Single(predicate: s => s.Name == "ByteUserName").Value;
            ByteUserNo = settings.Single(predicate: s => s.Name == "ByteUserNo").Value;
            ByteConnectionName = settings.Single(predicate: s => s.Name == "ByteConnectionName").Value;
        }


        public string ByteApiAuthKey { get; set; }
        public string ByteApiUrl { get; set; }
        public string ByteAuthKey { get; set; }
        public string ByteCompanyCode { get; set; }
        public string ByteApiPassword { get; set; }
        public string ByteApiUserName { get; set; }
        public string BytePassword { get; set; }
        public string ByteUserName { get; set; }
        public string ByteUserNo { get; set; }
        public string ByteConnectionName { get; set; }
    }
}
