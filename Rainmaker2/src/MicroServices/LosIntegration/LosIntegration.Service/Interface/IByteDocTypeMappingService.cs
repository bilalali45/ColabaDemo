using System;
using System.Collections.Generic;
using System.Text;
using LosIntegration.Entity.Models;
using LosIntegration.Model.Model;

namespace LosIntegration.Service.Interface
{
    public interface IByteDocTypeMappingService : IServiceBase<ByteDocTypeMapping>
    {

        List<ByteDocTypeMapping> GetByteDocTypeMappingWithDetails(int? id = null, string docType = "");
    }
}
