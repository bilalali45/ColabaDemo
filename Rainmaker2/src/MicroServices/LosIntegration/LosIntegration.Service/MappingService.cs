using LosIntegration.Data;
using LosIntegration.Entity.Models;
using LosIntegration.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Extensions.ExtensionClasses;
using URF.Core.Abstractions;

namespace LosIntegration.Service
{
    public class MappingService : ServiceBase<LosIntegrationContext, _Mapping>, IMappingService
    {
        public MappingService(IUnitOfWork<LosIntegrationContext> previousUow,
                              IServiceProvider services) : base(previousUow: previousUow,
                                                                services: services)
        {
        }


        public List<_Mapping> GetMappingWithDetails(string extOriginatorEntityId = "",
                                                   string extOriginatorEntityName = "",
                                                   string rmEnittyId = "",
                                                   string rmEntityName = "",
                                                   int? extOriginatorId = null)
        {
            var mappings = Repository.Query().AsQueryable();

            // @formatter:off 

            if (extOriginatorEntityId.HasValue()) mappings = mappings.Where(predicate: mapping => mapping.ExtOriginatorEntityId == extOriginatorEntityId);
            if (extOriginatorEntityName.HasValue()) mappings = mappings.Where(predicate: mapping => mapping.ExtOriginatorEntityName == extOriginatorEntityName);
            if (rmEnittyId.HasValue()) mappings = mappings.Where(predicate: mapping => mapping.RMEnittyId == rmEnittyId);
            if (rmEntityName.HasValue()) mappings = mappings.Where(predicate: mapping => mapping.RMEntityName ==rmEntityName);
            if (extOriginatorId.HasValue) mappings = mappings.Where(predicate: mapping => mapping.ExtOriginatorId == extOriginatorId);


            // @formatter:on 

            return mappings.ToList();
        }


        public List<_Mapping> GetMapping(List<string> rmEnittyIds,
                                                string rmEntityName )
        {
            var mappings = Repository.Query().AsQueryable();

            // @formatter:off 

            mappings = mappings.Where(predicate: mapping => rmEnittyIds.Contains(mapping.RMEnittyId) );
           
            mappings = mappings.Where(predicate: mapping => mapping.RMEntityName == rmEntityName);
            
            // @formatter:on 

            return mappings.ToList();
        }
    }
}