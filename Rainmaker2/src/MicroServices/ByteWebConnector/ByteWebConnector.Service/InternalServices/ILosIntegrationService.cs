using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ByteWebConnector.Model.Models;
using ByteWebConnector.Model.Models.ServiceResponseModels.ByteApi;

namespace ByteWebConnector.Service.InternalServices
{
    public interface ILosIntegrationService
    {
        Task<bool> DocumentDelete(string content);


        Task<bool> DocumentAddDocument(int fileDataId,
                                       List<EmbeddedDoc> embeddedDocs);
    }
}