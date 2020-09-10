using LosIntegration.Entity.Models;
using System.Collections.Generic;

namespace LosIntegration.Service.Interface
{
    public interface IByteDocTypeMappingService : IServiceBase<ByteDocTypeMapping>
    {

        List<ByteDocTypeMapping> GetByteDocTypeMappingWithDetails(int? id = null,
                                                                  string docType = "",
                                                                  ByteDocTypeMappingService.RelatedEntity? includes = null);


        
    }
}
