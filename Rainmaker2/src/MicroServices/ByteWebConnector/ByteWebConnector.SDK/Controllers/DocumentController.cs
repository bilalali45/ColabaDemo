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
using Newtonsoft.Json;
using Xceed.Pdf;

namespace ByteWebConnector.SDK.Controllers
{
    [Route(template: "api/ByteWebConnectorSdk/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {

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
                var byteProSettings = sdkSendDocumentRequest.ByteProSettings;
                var documentUploadRequest = sdkSendDocumentRequest.DocumentUploadRequest;

                SDKApplication application = null;

                SDKSession session = _sessionStatic;
                if (session != null)
                {
                    try
                    {
                        application = session.GetApplication();
                    }
                    catch (Exception e)
                    {
                        session = null;
                    }
                }

                if (application == null)
                {
                    session = new SDKSession();
                    string sessionDataFolder = session.DataFolder;
                    session.Login(byteProSettings.ByteUserName, byteProSettings.BytePassword, byteProSettings.ByteConnectionName);
                    session.Authorize(byteProSettings.ByteCompanyCode, byteProSettings.ByteUserNo, byteProSettings.ByteAuthKey);
                    _sessionStatic = session;
                    application = session.GetApplication();
                }



                SDKFile file = application.OpenFile(documentUploadRequest.FileName, false);

                int index = file.AddCollectionObject("EmbeddedDoc");
                SDKEmbeddedDoc embeddedDoc = file.GetCollectionObject("EmbeddedDoc", index) as SDKEmbeddedDoc;
                if (embeddedDoc != null)
                {
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
                    System.IO.File.WriteAllBytes(fileNameAndPath,
                                                 Convert.FromBase64String(documentUploadRequest.DocumentData));
                    //MemoryStream stream = GenerateStreamFromString(documentUploadRequest.DocumentData);

                    embeddedDoc.LoadFromFile(fileNameAndPath);
                }

                file.Save();
                object documentId = null;
                SDKEmbeddedDoc sdkEmbeddedDoc = file.GetCollectionObject("EmbeddedDoc", index) as SDKEmbeddedDoc;
                if (sdkEmbeddedDoc != null)
                {
                   documentId = sdkEmbeddedDoc.GetField("EmbeddedDocID");
                }
                EmbedDocumentResponse documentResponse = new EmbedDocumentResponse()
                {
                    DocumentId = Convert.ToInt64(documentId),
                    ExtOriginatorId = 1
                };
                return new SendSdkDocumentResponse() { Status = SendSdkDocumentResponse.SdkDocumentResponseStatus.Success,Data = JsonConvert.SerializeObject(documentResponse) };
            }
            catch (Exception e)
            {

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
