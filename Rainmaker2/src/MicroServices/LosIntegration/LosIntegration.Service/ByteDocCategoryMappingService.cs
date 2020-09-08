using LosIntegration.Data;
using LosIntegration.Entity.Models;
using LosIntegration.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using URF.Core.Abstractions;

namespace LosIntegration.Service
{
    public class ByteDocCategoryMappingService : ServiceBase<Context, ByteDocCategoryMapping>, IByteDocCategoryMappingService
    {
        public ByteDocCategoryMappingService(IUnitOfWork<Context> previousUow,
                                         IServiceProvider services) : base(previousUow: previousUow,
                                                                           services: services)
        {
        }


        public List<ByteDocCategoryMapping> GetByteDocCategoryMappingWithDetails( int? id = null)
        {
            var byteDocCategoryMappings = Repository.Query().AsQueryable();

            // @formatter:off 

            if (id.HasValue) byteDocCategoryMappings = byteDocCategoryMappings.Where(predicate: byteDocCategoryMapping => byteDocCategoryMapping.Id == id);
            

            // @formatter:on 

            return byteDocCategoryMappings.ToList();
        }
    }
}