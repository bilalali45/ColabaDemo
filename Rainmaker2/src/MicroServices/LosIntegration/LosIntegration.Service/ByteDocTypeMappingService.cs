using System;
using System.Collections.Generic;
using System.Linq;
using LosIntegration.Data;
using LosIntegration.Entity.Models;
using LosIntegration.Service.Interface;
using URF.Core.Abstractions;

namespace LosIntegration.Service
{
    public class ByteDocTypeMappingService : ServiceBase<Context, ByteDocTypeMapping>, IByteDocTypeMappingService
    {
        public ByteDocTypeMappingService(IUnitOfWork<Context> previousUow,
                                         IServiceProvider services) : base(previousUow: previousUow,
                                                                           services: services)
        {
        }


        public List<ByteDocTypeMapping> GetByteDocTypeMappingWithDetails( int? id = null, string docType = "")
        {
            var byteDocTypeMappings = Repository.Query().AsQueryable();

            // @formatter:off 

            if (id.HasValue) byteDocTypeMappings = byteDocTypeMappings.Where(predicate: byteDocTypeMapping => byteDocTypeMapping.Id == id);
            if (docType.HasValue()) byteDocTypeMappings = byteDocTypeMappings.Where(predicate: byteDocTypeMapping => byteDocTypeMapping.RmDocTypeName == docType);
      


            // @formatter:on 

            //if (includes.HasValue)
            //    mappings = ProcessIncludes(query: mappings,
            //                               includes: includes.Value);

            return byteDocTypeMappings.ToList();
        }


    
    }
}