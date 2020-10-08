using System;
using System.Collections.Generic;
using System.Linq;
using Extensions.ExtensionClasses;
using LosIntegration.Data;
using LosIntegration.Entity.Models;
using LosIntegration.Service.Interface;
using Microsoft.EntityFrameworkCore;
using URF.Core.Abstractions;

namespace LosIntegration.Service
{
    public class ByteDocTypeMappingService : ServiceBase<LosIntegrationContext, ByteDocTypeMapping>, IByteDocTypeMappingService
    {
        [Flags]
        public enum RelatedEntities
        {
            ByteDocCategoryMapping = 1 << 0,
        }


        public ByteDocTypeMappingService(IUnitOfWork<LosIntegrationContext> previousUow,
                                         IServiceProvider services) : base(previousUow: previousUow,
                                                                           services: services)
        {
        }


        public List<ByteDocTypeMapping> GetByteDocTypeMappingWithDetails(int? id = null,
                                                                         string docType = "",
                                                                         RelatedEntities? includes = null)
        {
            var byteDocTypeMappings = Repository.Query().AsQueryable();

            // @formatter:off 

            if (id.HasValue) byteDocTypeMappings = byteDocTypeMappings.Where(predicate: byteDocTypeMapping => byteDocTypeMapping.Id == id);
            if (docType.HasValue()) byteDocTypeMappings = byteDocTypeMappings.Where(predicate: byteDocTypeMapping => byteDocTypeMapping.RmDocTypeName == docType);



            // @formatter:on 

            if (includes.HasValue)
                byteDocTypeMappings = ProcessIncludes(query: byteDocTypeMappings,
                                                      includes: includes.Value);

            return byteDocTypeMappings.ToList();
        }


        public IQueryable<ByteDocTypeMapping> ProcessIncludes(IQueryable<ByteDocTypeMapping> query,
                                                              RelatedEntities includes)
        {
            // @formatter:off 
            if (includes.HasFlag(flag: RelatedEntities.ByteDocCategoryMapping)) query = query.Include(navigationPropertyPath: byteDocTypeMapping => byteDocTypeMapping.ByteDocCategoryMapping);
            // @formatter:on 
            return query;
        }
    }
}