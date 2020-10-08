using LosIntegration.Entity.Models;
using System.Collections.Generic;
using System.Linq;

namespace LosIntegration.Service.Interface
{
    public interface IByteDocTypeMappingService : IServiceBase<ByteDocTypeMapping>
    {
        List<ByteDocTypeMapping> GetByteDocTypeMappingWithDetails(int? id = null,
                                                                  string docType = "",
                                                                  ByteDocTypeMappingService.RelatedEntities? includes = null);


        IQueryable<ByteDocTypeMapping> ProcessIncludes(IQueryable<ByteDocTypeMapping> query,
                                                       ByteDocTypeMappingService.RelatedEntities includes);
    }
}
