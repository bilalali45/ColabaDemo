using System;
using System.Collections.Generic;
using System.Text;
using LosIntegration.Entity.Models;
using LosIntegration.Model.Model;

namespace LosIntegration.Service.Interface
{
    public interface IByteDocStatusMappingService : IServiceBase<ByteDocStatusMapping>
    {

        List<ByteDocStatusMapping> GetByteDocStatusMappingWithDetails(string status = "");
    }
}
