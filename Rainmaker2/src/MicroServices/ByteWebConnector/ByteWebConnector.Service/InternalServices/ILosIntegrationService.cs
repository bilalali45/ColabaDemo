﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ByteWebConnector.Model.Models.Document;

namespace ByteWebConnector.Service.InternalServices
{
    public interface ILosIntegrationService
    {
        Task<HttpResponseMessage> DocumentDelete(string content);


        Task<HttpResponseMessage> DocumentAddDocument(int fileDataId,
                                                      List<EmbeddedDoc> embeddedDocs);
    }
}