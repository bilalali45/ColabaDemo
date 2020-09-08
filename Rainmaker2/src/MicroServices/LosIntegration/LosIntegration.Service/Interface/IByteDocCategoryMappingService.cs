using LosIntegration.Entity.Models;
using System.Collections.Generic;

namespace LosIntegration.Service.Interface
{
    public interface IByteDocCategoryMappingService : IServiceBase<ByteDocCategoryMapping>
    {

        List<ByteDocCategoryMapping> GetByteDocCategoryMappingWithDetails(int? id = null);
    }
}
