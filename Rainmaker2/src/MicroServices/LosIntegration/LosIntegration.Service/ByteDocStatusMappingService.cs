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
    public class ByteDocStatusMappingService : ServiceBase<LosIntegrationContext, ByteDocStatusMapping>, IByteDocStatusMappingService
    {
        public ByteDocStatusMappingService(IUnitOfWork<LosIntegrationContext> previousUow,
                                         IServiceProvider services) : base(previousUow: previousUow,
                                                                           services: services)
        {
        }


        public List<ByteDocStatusMapping> GetByteDocStatusMappingWithDetails(string status = "")
        {
            var byteDocStatusMappings = Repository.Query().AsQueryable();

            // @formatter:off 

            if (status.HasValue()) byteDocStatusMappings = byteDocStatusMappings.Where(predicate: byteDocStatusMapping => byteDocStatusMapping.RmDocStatusName == status);
            

            // @formatter:on 

            return byteDocStatusMappings.ToList();
        }
    }
}