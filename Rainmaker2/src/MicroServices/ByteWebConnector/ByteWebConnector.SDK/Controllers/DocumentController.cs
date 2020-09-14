using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ByteWebConnector.SDK.Models;
using ByteWebConnector.SDK.Models.ControllerModels.Document.Response;
using LOSAutomation;

namespace ByteWebConnector.SDK.Controllers
{
    //[Route(template: "api/ByteWebConnectorSdk/[controller]")]
    public class DocumentController : ApiController
    {

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        
        public string Get(int id)
        {
            return "value";
        }

        public SendSdkDocumentResponse SendSdkDocument([FromBody] SdkSendDocumentRequest sdkSendDocumentRequest)
        {
            try
            {
                var byteProSettings = sdkSendDocumentRequest.ByteProSettings;
                var documentUploadRequest = sdkSendDocumentRequest.DocumentUploadRequest;



                SDKSession session = new SDKSession();
                string sessionDataFolder = session.DataFolder;
                session.Login(byteProSettings.ByteUserName, byteProSettings.BytePassword, "");
                session.Authorize(byteProSettings.ByteCompanyCode, byteProSettings.ByteUserNo, byteProSettings.ByteAuthKey);
                var application = session.GetApplication();

                SDKFile file = application.OpenFile(documentUploadRequest.FileName, false);

                int index = file.AddCollectionObject("EmbeddedDoc");
                SDKEmbeddedDoc embeddedDoc = file.GetCollectionObject("EmbeddedDoc", index) as SDKEmbeddedDoc;
                MemoryStream stream = GenerateStreamFromString(sdkSendDocumentRequest.DocumentUploadRequest.DocumentData);
                if (embeddedDoc != null) embeddedDoc.LoadFromStream(stream);
                file.Save();
                return new SendSdkDocumentResponse(){Status = SendSdkDocumentResponse.SdkDocumentResponseStatus.Success};
            }
            catch (Exception e)
            {
                

            }
            return new SendSdkDocumentResponse() { Status = SendSdkDocumentResponse.SdkDocumentResponseStatus.Error };
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
