using LosIntegration.Entity.Models;
using System.Collections.Generic;

namespace LosIntegration.Service.Interface
{
    public interface IMappingService : IServiceBase<Mapping>
    {
         


        List<Mapping> GetMappingWithDetails(string extOriginatorEntityId="",
                                            string extOriginatorEntityName = "",
                                            string rmEnittyId = "",
                                            string rmEntityName = "",
                                            int? extOriginatorId=null);


   

        List<Mapping> GetMapping(List<string> rmEnittyIds,
                                 string rmEntityName );
    }
}
