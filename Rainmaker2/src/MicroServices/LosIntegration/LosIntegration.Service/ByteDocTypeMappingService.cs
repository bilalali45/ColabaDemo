using LosIntegration.Data;
using LosIntegration.Entity.Models;
using LosIntegration.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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

            return byteDocTypeMappings.ToList();
        }
    }
}