﻿using System;
using System.Collections.Generic;
using System.Linq;
using LosIntegration.Data;
using LosIntegration.Entity.Models;
using LosIntegration.Service.Interface;
using URF.Core.Abstractions;

namespace LosIntegration.Service
{
    public class ByteDocStatusMappingService : ServiceBase<Context, ByteDocStatusMapping>, IByteDocStatusMappingService
    {
        public ByteDocStatusMappingService(IUnitOfWork<Context> previousUow,
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

            //if (includes.HasValue)
            //    mappings = ProcessIncludes(query: mappings,
            //                               includes: includes.Value);

            return byteDocStatusMappings.ToList();
        }


    
    }
}