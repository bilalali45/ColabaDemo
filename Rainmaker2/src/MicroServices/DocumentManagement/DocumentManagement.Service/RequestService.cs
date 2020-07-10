using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Service
{
    public class RequestService : IRequestService
    {
        private readonly IMongoService mongoService;
        public RequestService(IMongoService mongoService)
        {
            this.mongoService = mongoService;
        }
    }
}
