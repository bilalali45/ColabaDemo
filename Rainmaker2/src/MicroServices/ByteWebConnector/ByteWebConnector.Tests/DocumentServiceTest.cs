using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ByteWebConnector.Model.Models.ServiceResponseModels.ByteWebConnectorSDK;
using ByteWebConnector.Model.Models.ServiceResponseModels.Rainmaker.LoanApplication;
using ByteWebConnector.Service.DbServices;
using ByteWebConnector.Service.InternalServices;
using ServiceCallHelper;
using Xunit;

using System.Collections.Generic;
using System.IO;
using ByteWebConnector.Model.Models;
using ByteWebConnector.Model.Models.ActionModels.Document;
using ByteWebConnector.Service.ExternalServices;
using HttpWebAdapters;
using DocumentUploadRequest = ByteWebConnector.Model.Models.ServiceRequestModels.ByteApi.DocumentUploadRequest;
using ByteWebConnector.Model.Models.OwnModels.Settings;
using ByteWebConnector.Model.Models.OwnModels;
using ByteWebConnector.Model.Models.ServiceResponseModels.ByteApi;
using CsQuery.ExtensionMethods;
using Extensions.ExtensionClasses;
using DeleteRequest = ByteWebConnector.Model.Models.ActionModels.Document.DeleteRequest;
using Setting = ByteWebConnector.Entity.Models.Setting;
using Task = System.Threading.Tasks.Task;
using AutoMapper;
using Moq;
using Microsoft.Extensions.Logging;

namespace ByteWebConnector.Tests
{
    public class DocumentServiceTest
    {
        [Fact]
        public void TestSendDocumentToByteService()
        {
            Mock<ISettingService> settingService = new Mock<ISettingService>();
            Mock<IMapper> mappingService = new Mock<IMapper>();
            string documentUploadResponse = @"{
                                              ""fileDataId"": 0,
                                              ""documentId"": 0,
                                              ""documentName"": null,
                                              ""documentType"": null,
                                              ""documentCategory"": null,
                                              ""documentStatus"": 0,
                                              ""documentExension"": null,
                                              ""viewable"": false,
                                              ""neededItemId"": 0,
                                              ""conditionId"": 0,
                                              ""internal"": false,
                                              ""outdated"": false,
                                              ""expirationDate"": null,
                                              ""documentData"": null,
                                              ""extOriginatorId"": 0
                                            }";
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("https://devbyteapi123.rainsoftfn.com/byteapi/document/", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(documentUploadResponse)
            });
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            var handlerMock = new Mock<TestMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                                                  "SendAsync",
                                                  ItExpr.IsAny<HttpRequestMessage>(),
                                                  ItExpr.IsAny<CancellationToken>()
                                                 )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {

                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(documentUploadResponse, Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
            httpClientFactory.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(httpClient);
            Entity.Models.Setting byteApiAuthKey = new Entity.Models.Setting();
            byteApiAuthKey.Name = "ByteApiAuthKey";

            Entity.Models.Setting byteApiUrl = new Entity.Models.Setting();
            byteApiUrl.Name = "ByteApiUrl";

            Entity.Models.Setting byteAuthKey = new Entity.Models.Setting();
            byteAuthKey.Name = "ByteAuthKey";

            Entity.Models.Setting byteCompanyCode = new Entity.Models.Setting();
            byteCompanyCode.Name = "ByteCompanyCode";

            Entity.Models.Setting byteApiPassword = new Entity.Models.Setting();
            byteApiPassword.Name = "ByteApiPassword";

            Entity.Models.Setting byteApiUserName = new Entity.Models.Setting();
            byteApiUserName.Name = "ByteApiUserName";

            Entity.Models.Setting bytePassword = new Entity.Models.Setting();
            bytePassword.Name = "BytePassword";

            Entity.Models.Setting byteUserName = new Entity.Models.Setting();
            byteUserName.Name = "ByteUserName";

            Entity.Models.Setting byteUserNo = new Entity.Models.Setting();
            byteUserNo.Name = "ByteUserNo";

            Entity.Models.Setting byteConnectionName = new Entity.Models.Setting();
            byteConnectionName.Name = "ByteConnectionName";

            List<Entity.Models.Setting> lsSettings = new List<Setting>();
            lsSettings.Add(byteApiAuthKey);
            lsSettings.Add(byteApiUrl);
            lsSettings.Add(byteAuthKey);
            lsSettings.Add(byteCompanyCode);
            lsSettings.Add(byteApiPassword);
            lsSettings.Add(byteApiUserName);
            lsSettings.Add(bytePassword);
            lsSettings.Add(byteUserName);
            lsSettings.Add(byteUserNo);
            lsSettings.Add(byteConnectionName);

            ByteProSettings byteProSettings = new ByteProSettings(lsSettings);
            byteProSettings.ByteApiUrl = "https://devbyteapi123.rainsoftfn.com/byteapi/";
            byteProSettings.ByteApiAuthKey = "6c496dde-02d0-5a6e-d2c8-3efe20588c9b";
            byteProSettings.ByteApiUserName = "testuser";
            byteProSettings.ByteApiPassword = "faith123";
            settingService.Setup(x => x.GetByteProSettings()).Returns(byteProSettings);
            IByteProService byteProService = new ByteProService(Mock.Of<ILogger<ByteProService>>(), settingService.Object, httpClient, mappingService.Object);
            DocumentUploadRequest documentUploadRequest = new DocumentUploadRequest()
            {

                FileDataId = 100234,
                DocumentName = "test123",
                DocumentType = "Bank Statement",
                DocumentCategory = "Bank",
                DocumentStatus = "Review",
                DocumentExension = ".pdf",
                DocumentData = "sadasdasdjvhbx=xkznxnsdsgdysgfd=aasd=asdasdasdasd"
            };
            ApiResponse<DocumentUploadResponse> respone = byteProService.SendDocumentToByte(documentUploadRequest, "asddsdasdmmxcmnxcbnxc=cxvxcvxcvxcv=xcvxcvxcvxcvxcv");
            Assert.NotNull(respone.Data);
        }
        [Fact]
        public void TestGetFileDataService()
        {
            Mock<ISettingService> settingService = new Mock<ISettingService>();
            Mock<IMapper> mappingService = new Mock<IMapper>();
            string fileDataResponse = @"{
                                  ""loanId"": 0,
                                  ""organizationId"": 0,
                                  ""fileName"": null,
                                  ""occupancyType"": 0,
                                  ""agencyCaseNo"": null,
                                  ""fileDataId"": 0
                                }";

            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("https://devbyteapi123.rainsoftfn.com/byteapi/filedata/" + 1004565, new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(fileDataResponse)
            });
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            var handlerMock = new Mock<TestMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                                                  "SendAsync",
                                                  ItExpr.IsAny<HttpRequestMessage>(),
                                                  ItExpr.IsAny<CancellationToken>()
                                                 )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {

                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(fileDataResponse, Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
            httpClientFactory.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(httpClient);


            Entity.Models.Setting byteApiAuthKey = new Entity.Models.Setting();
            byteApiAuthKey.Name = "ByteApiAuthKey";

            Entity.Models.Setting byteApiUrl = new Entity.Models.Setting();
            byteApiUrl.Name = "ByteApiUrl";

            Entity.Models.Setting byteAuthKey = new Entity.Models.Setting();
            byteAuthKey.Name = "ByteAuthKey";

            Entity.Models.Setting byteCompanyCode = new Entity.Models.Setting();
            byteCompanyCode.Name = "ByteCompanyCode";

            Entity.Models.Setting byteApiPassword = new Entity.Models.Setting();
            byteApiPassword.Name = "ByteApiPassword";

            Entity.Models.Setting byteApiUserName = new Entity.Models.Setting();
            byteApiUserName.Name = "ByteApiUserName";

            Entity.Models.Setting bytePassword = new Entity.Models.Setting();
            bytePassword.Name = "BytePassword";

            Entity.Models.Setting byteUserName = new Entity.Models.Setting();
            byteUserName.Name = "ByteUserName";

            Entity.Models.Setting byteUserNo = new Entity.Models.Setting();
            byteUserNo.Name = "ByteUserNo";

            Entity.Models.Setting byteConnectionName = new Entity.Models.Setting();
            byteConnectionName.Name = "ByteConnectionName";

            List<Entity.Models.Setting> lsSettings = new List<Setting>();
            lsSettings.Add(byteApiAuthKey);
            lsSettings.Add(byteApiUrl);
            lsSettings.Add(byteAuthKey);
            lsSettings.Add(byteCompanyCode);
            lsSettings.Add(byteApiPassword);
            lsSettings.Add(byteApiUserName);
            lsSettings.Add(bytePassword);
            lsSettings.Add(byteUserName);
            lsSettings.Add(byteUserNo);
            lsSettings.Add(byteConnectionName);

            ByteProSettings byteProSettings = new ByteProSettings(lsSettings);
            byteProSettings.ByteApiUrl = "https://devbyteapi123.rainsoftfn.com/byteapi/";
            byteProSettings.ByteApiAuthKey = "6c496dde-02d0-5a6e-d2c8-3efe20588c9b";
            byteProSettings.ByteApiUserName = "testuser";
            byteProSettings.ByteApiPassword = "faith123";
            settingService.Setup(x => x.GetByteProSettings()).Returns(byteProSettings);
            IByteProService byteProService = new ByteProService(Mock.Of<ILogger<ByteProService>>(), settingService.Object, httpClient, mappingService.Object);
            Task<FileDataResponse> response = byteProService.GetFileDataAsync("asddsdasdmmxcmnxcbnxc=cxvxcvxcvxcv=xcvxcvxcvxcvxcv", "1004565");
            Assert.NotNull(response.Result);
        }
    }
}
