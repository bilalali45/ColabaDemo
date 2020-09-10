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
    public class ByteDocTypeMappingService : ServiceBase<Context, ByteDocTypeMapping>, IByteDocTypeMappingService
    {
        [Flags]
        public enum RelatedEntity
        {
            ByteDocCategoryMapping = 1 << 0,
        }


        public ByteDocTypeMappingService(IUnitOfWork<Context> previousUow,
                                         IServiceProvider services) : base(previousUow: previousUow,
                                                                           services: services)
        {
        }


        public List<ByteDocTypeMapping> GetByteDocTypeMappingWithDetails(int? id = null,
                                                                         string docType = "",
                                                                         RelatedEntity? includes = null)
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


        private IQueryable<ByteDocTypeMapping> ProcessIncludes(IQueryable<ByteDocTypeMapping> query,
                                                               RelatedEntity includes)
        {
            // @formatter:off 
            if (includes.HasFlag(flag: RelatedEntity.ByteDocCategoryMapping)) query = query.Include(navigationPropertyPath: byteDocTypeMapping => byteDocTypeMapping.ByteDocCategoryMapping);
            // @formatter:on 
            return query;
        }
    }
}