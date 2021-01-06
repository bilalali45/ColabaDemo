using System;
using System.Collections.Generic;
using System.Text;

namespace LosIntegration.Model.Model.ServiceResponseModels
{
    public class MilestoneMappingResponse
    {
        public int Id { get; set; } // Id (Primary key)
        public int TenantId { get; set; } // TenantId
        public string Name { get; set; } // Name (length: 50)
        public short ExternalOriginatorId { get; set; } // ExternalOriginatorId
        public int StatusId { get; set; } // StatusId
    }
}
