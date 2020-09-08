using LosIntegration.Entity.Models;
using System.Collections.Generic;

namespace LosIntegration.Service.Interface
{
    public interface IByteDocStatusMappingService : IServiceBase<ByteDocStatusMapping>
    {

        List<ByteDocStatusMapping> GetByteDocStatusMappingWithDetails(string status = "");
    }
}
