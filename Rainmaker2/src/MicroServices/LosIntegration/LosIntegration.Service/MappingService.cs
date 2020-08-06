using System;
using System.Collections.Generic;
using System.Linq;
using LosIntegration.Data;
using LosIntegration.Entity.Models;
using LosIntegration.Service.Interface;
using URF.Core.Abstractions;

namespace LosIntegration.Service
{
    public class MappingService : ServiceBase<Context, Mapping>, IMappingService
    {
        public MappingService(IUnitOfWork<Context> previousUow,
                              IServiceProvider services) : base(previousUow: previousUow,
                                                                services: services)
        {
        }


        public List<Mapping> GetMappingWithDetails(string extOriginatorEntityId="",
                                                   string extOriginatorEntityName = "",
                                                   string rmEnittyId = "",
                                                   string rmEntityName = "",
                                                   int? extOriginatorId=null)
        {
            var mappings = Repository.Query().AsQueryable();

            // @formatter:off 

            if (extOriginatorEntityId.HasValue()) mappings = mappings.Where(predicate: mapping => mapping.ExtOriginatorEntityId == extOriginatorEntityId);
            if (extOriginatorEntityName.HasValue()) mappings = mappings.Where(predicate: mapping => mapping.ExtOriginatorEntityName == extOriginatorEntityName);
            if (rmEnittyId.HasValue()) mappings = mappings.Where(predicate: mapping => mapping.RMEnittyId == rmEnittyId);
            if (rmEntityName.HasValue()) mappings = mappings.Where(predicate: mapping => mapping.RMEntityName ==rmEntityName);
            if (extOriginatorId.HasValue) mappings = mappings.Where(predicate: mapping => mapping.ExtOriginatorId == extOriginatorId);


            // @formatter:on 

            //if (includes.HasValue)
            //    mappings = ProcessIncludes(query: mappings,
            //                               includes: includes.Value);

            return mappings.ToList();
        }
    }
}