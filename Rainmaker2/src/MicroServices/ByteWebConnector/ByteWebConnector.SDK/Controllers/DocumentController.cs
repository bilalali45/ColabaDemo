using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using ByteWebConnector.SDK.Models;
using ByteWebConnector.SDK.Models.ControllerModels.Document.Response;
using LOSAutomation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using Xceed.Pdf;

namespace ByteWebConnector.SDK.Controllers
{
    [Route(template: "api/ByteWebConnectorSdk/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly ILogger<DocumentController> _logger;


        public DocumentController(ILogger<DocumentController> logger)
        {
            _logger = logger;
        }

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public string Get(int id)
        {
            return "value";
        }


        private static SDKSession _sessionStatic;

        [Route(template: "[action]")]
        [HttpPost]
        public SendSdkDocumentResponse SendSdkDocument([FromBody] SdkSendDocumentRequest sdkSendDocumentRequest)
        {
            try
            {
                _logger.LogInformation("====DocsyncSDK SendSdkDocument Start====");
                var byteProSettings = sdkSendDocumentRequest.ByteProSettings;
                var documentUploadRequest = sdkSendDocumentRequest.DocumentUploadRequest;

                SDKApplication application = null;

                SDKSession session = _sessionStatic;
                if (session != null)
                {
                    _logger.LogInformation("====DocsyncSDK old session found====");
                    try
                    {
                        application = session.GetApplication();
                    }
                    catch (Exception e)
                    {
                        _logger.LogInformation($"DocsyncSDK Session Exception={e.Message}");
                        session = null;
                    }
                }
                
                if (application == null)
                {
                    _logger.LogInformation($"DocsyncSDK application= null");
                    session = new SDKSession();
                    session.Login(byteProSettings.ByteUserName, byteProSettings.BytePassword, byteProSettings.ByteConnectionName);
                    session.Authorize(byteProSettings.ByteCompanyCode, byteProSettings.ByteUserNo, byteProSettings.ByteAuthKey);
                    _logger.LogInformation($"DocsyncSDK ===Authorized===");
                    _sessionStatic = session;
                    application = session.GetApplication();
                }

                _logger.LogInformation($"DocsyncSDK application found");

                SDKFile file = application.OpenFile(documentUploadRequest.FileName, false);
                _logger.LogInformation($"DocsyncSDK File Opened");
                int index = file.AddCollectionObject("EmbeddedDoc");
                SDKEmbeddedDoc embeddedDoc = file.GetCollectionObject("EmbeddedDoc", index) as SDKEmbeddedDoc;
                if (embeddedDoc != null)
                {
                    _logger.LogInformation($"DocsyncSDK EmbeddedDoc Added");
                    string fileNameAndPath = Path.GetTempPath() + documentUploadRequest.DocumentName + "." +
                                               documentUploadRequest.DocumentExension;
                    embeddedDoc.SetFieldValue("FileExtension",
                                              documentUploadRequest.DocumentExension);
                    embeddedDoc.SetFieldValue("DocumentCategoryCode",
                                              documentUploadRequest.DocumentCategory);
                    embeddedDoc.SetFieldValue("DocumentTypeCode",
                                              documentUploadRequest.DocumentType);
                    embeddedDoc.SetFieldValue("Status",
                                              documentUploadRequest.DocumentStatus);
                    embeddedDoc.SetFieldValue("Description",
                                              documentUploadRequest.DocumentName);
                    _logger.LogInformation($"DocsyncSDK All Fields Set in Embedded Doc");
                    System.IO.File.WriteAllBytes(fileNameAndPath,
                                                 Convert.FromBase64String(documentUploadRequest.DocumentData));
                    _logger.LogInformation($"DocsyncSDK File Write location = {fileNameAndPath}");
                    //MemoryStream stream = GenerateStreamFromString(documentUploadRequest.DocumentData);

                    embeddedDoc.LoadFromFile(fileNameAndPath);
                    _logger.LogInformation($"DocsyncSDK LoadFromFile = true");
                }

                file.Save();
                _logger.LogInformation($"DocsyncSDK LoadApplication File saved");
                object documentId = null;
                SDKEmbeddedDoc sdkEmbeddedDoc = file.GetCollectionObject("EmbeddedDoc", index) as SDKEmbeddedDoc;
                if (sdkEmbeddedDoc != null)
                {
                    _logger.LogInformation($"DocsyncSDK Getting Embedded Doc");
                    documentId = sdkEmbeddedDoc.GetField("EmbeddedDocID");
                   _logger.LogInformation($"DocsyncSDK DocumentId= {documentId}");
                }
                EmbedDocumentResponse documentResponse = new EmbedDocumentResponse()
                {
                    DocumentId = Convert.ToInt64(documentId),
                    ExtOriginatorId = 1
                };
                _logger.LogInformation($"DocsyncSDK EmbedDocumentResponse= {documentResponse.DocumentId}");
                _logger.LogInformation($"DocsyncSDK EmbedDocumentResponse= {documentResponse.ExtOriginatorId}");
                return new SendSdkDocumentResponse() { Status = SendSdkDocumentResponse.SdkDocumentResponseStatus.Success,Data = JsonConvert.SerializeObject(documentResponse) };
            }
            catch (Exception e)
            {
                _logger.LogInformation($"DocsyncSDK Exception= {e.Message}");
                return new SendSdkDocumentResponse() { Status = SendSdkDocumentResponse.SdkDocumentResponseStatus.Error };
            }

        }


        // PUT: api/Document/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Document/5
        public void Delete(int id)
        {
        }

        #region private Methods
        public static MemoryStream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        #endregion
    }
}
