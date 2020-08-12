using System;
using System.Collections.Generic;
using System.Text;
using LosIntegration.Entity.Models;
using LosIntegration.Model.Model;

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
