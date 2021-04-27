using LosIntegration.API.Controllers;
using LosIntegration.API.Models;
using LosIntegration.API.Models.Document;
using LosIntegration.Entity.Models;
using LosIntegration.Service;
using LosIntegration.Service.Interface;
using LosIntegration.Service.InternalServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using Moq.Protected;
using ServiceCallHelper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LosIntegration.Tests
{
    public class DocumentTest
    {
        [Fact]
        public async Task TestDeleteController()
        {
            //Arrange
            Mock<IMappingService> mockMappingService = new Mock<IMappingService>();
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com/");

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
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
                   Content = new StringContent("{\"id\":1}", Encoding.UTF8, "application/json"),
               })
               .Verifiable();
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            List<_Mapping> mappings= new List<_Mapping>();

            _Mapping mapping = new _Mapping();
            mapping.RMEnittyId = "1";
            mappings.Add(mapping);

            mockMappingService.Setup(x => x.GetMappingWithDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>())).Returns(mappings);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Bearer")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new DocumentController(httpClientFactory.Object, mockConfiguration.Object, mockMappingService.Object, Mock.Of<ILogger<DocumentController>>(),null,null,null);

            controller.ControllerContext = context;
            //Act
            DeleteRequest deleteRequest = new DeleteRequest();
            deleteRequest.ExtOriginatorFileId = 1;
            deleteRequest.ExtOriginatorId = 1;
            deleteRequest.ExtOriginatorLoanApplicationId = 1;
            IActionResult result = await controller.Delete(deleteRequest);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestDeleteMappingNullController()
        {
            //Arrange
            Mock<IMappingService> mockMappingService = new Mock<IMappingService>();
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com/");

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
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
                   Content = new StringContent("{\"id\":1}", Encoding.UTF8, "application/json"),
               })
               .Verifiable();
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            List<_Mapping> mappings = new List<_Mapping>();

            mockMappingService.Setup(x => x.GetMappingWithDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>())).Returns(mappings);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Bearer")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new DocumentController(httpClientFactory.Object, mockConfiguration.Object, mockMappingService.Object, Mock.Of<ILogger<DocumentController>>(), null, null, null);

            controller.ControllerContext = context;
            //Act
            DeleteRequest deleteRequest = new DeleteRequest();
            deleteRequest.ExtOriginatorFileId = 1;
            deleteRequest.ExtOriginatorId = 1;
            deleteRequest.ExtOriginatorLoanApplicationId = 1;
            IActionResult result = await controller.Delete(deleteRequest);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task TestDeleteBadRequestController()
        {
            //Arrange
            Mock<IMappingService> mockMappingService = new Mock<IMappingService>();
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com/");

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
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
                   StatusCode = HttpStatusCode.BadRequest,
                   Content = new StringContent("{\"id\":1}", Encoding.UTF8, "application/json"),
               })
               .Verifiable();
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            List<_Mapping> mappings = new List<_Mapping>();

            _Mapping mapping = new _Mapping();
            mapping.RMEnittyId = "1";
            mappings.Add(mapping);

            mockMappingService.Setup(x => x.GetMappingWithDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>())).Returns(mappings);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Bearer")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new DocumentController(httpClientFactory.Object, mockConfiguration.Object, mockMappingService.Object, Mock.Of<ILogger<DocumentController>>(), null, null, null);

            controller.ControllerContext = context;
            //Act
            DeleteRequest deleteRequest = new DeleteRequest();
            deleteRequest.ExtOriginatorFileId = 1;
            deleteRequest.ExtOriginatorId = 1;
            deleteRequest.ExtOriginatorLoanApplicationId = 1;
            IActionResult result = await controller.Delete(deleteRequest);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task TestSendFileToExternalOriginatorController()
        {
            //Arrange
            Mock<IMappingService> mockMappingService = new Mock<IMappingService>();
           
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            Mock<IByteDocTypeMappingService> mockBytedocTypeMapping = new Mock<IByteDocTypeMappingService>();
            Mock<IByteDocStatusMappingService> mockBytedocstatusMapping = new Mock<IByteDocStatusMappingService>();
            Mock<IRainmakerService> mockRainMakerMapping = new Mock<IRainmakerService>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            string documentResponse = @"{
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
            messages.Add("http://test.com/api/bytewebconnector/document/senddocument", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(documentResponse)
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/view?id=sd435&requestid=s34&docid=d4&fileid=d43&tenantid=1", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{   \"id\": null,   \"requestId\": null,   \"docId\": null,   \"docName\": null,   \"status\": null,   \"createdOn\": \"0001-01-01T00:00:00\",   \"files\": [{   \"id\": null,   \"clientName\": null,   \"fileUploadedOn\": \"0001-01-01T00:00:00\",   \"mcuName\": null,   \"byteProStatus\": null,   \"status\": null }],   \"typeId\": null,   \"userName\": null }] ")
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/getdocuments?loanapplicationid=1&tenantid=1&pending=false", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{   \"id\": null,   \"requestId\": null,   \"docId\": \"d4\",   \"docName\": null,   \"status\": null,   \"createdOn\": \"0001-01-01T00:00:00\",   \"files\": [{   \"id\": \"d43\",   \"clientName\": \"clientName.pdf\",   \"fileUploadedOn\": \"0001-01-01T00:00:00\",   \"mcuName\": \"mcuname.pdf\",   \"byteProStatus\": null,   \"status\": null }],   \"typeId\": null,   \"userName\": null }] ")
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/updatebyteprostatus", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("")
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
                    Content = new StringContent(documentResponse, Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
            

            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
            List<ByteDocTypeMapping> byteDocTypeMapping = new List<ByteDocTypeMapping>() { new ByteDocTypeMapping() { Id = 1,RmDocTypeName = "Other",ByteDocCategoryMapping = new ByteDocCategoryMapping() {ByteDocCategoryName = "",RmDocCategoryName = "",Id = 1 } } };
            mockBytedocTypeMapping.Setup(x => x.GetByteDocTypeMappingWithDetails(It.IsAny<int?>(),It.IsAny<string>(),It.IsAny<ByteDocTypeMappingService.RelatedEntities>())).Returns(byteDocTypeMapping);
            List<ByteDocStatusMapping> byteDocStatusMapping = new List<ByteDocStatusMapping>() { new ByteDocStatusMapping() { Id = 1, ByteDocStatusName="",RmDocStatusName = ""} };
            mockBytedocstatusMapping.Setup(x => x.GetByteDocStatusMappingWithDetails(It.IsAny<string>())).Returns(byteDocStatusMapping);
            List<_Mapping> mappings = new List<_Mapping>();

            _Mapping mapping = new _Mapping();
            mapping.RMEnittyId = "1";
            mappings.Add(mapping);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Bearer")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

           

            var controller = new DocumentController(httpClientFactory.Object, mockConfiguration.Object, mockMappingService.Object, Mock.Of<ILogger<DocumentController>>(), mockBytedocTypeMapping.Object, mockBytedocstatusMapping.Object, mockRainMakerMapping.Object);

            controller.ControllerContext = context;

            //Act
            SendFileToExternalOriginatorRequest sendFileToExternalOriginatorRequest = new SendFileToExternalOriginatorRequest();
            sendFileToExternalOriginatorRequest.DocumentId = "d4";
            sendFileToExternalOriginatorRequest.FileId = "d43";
            sendFileToExternalOriginatorRequest.RequestId = "s34";
            sendFileToExternalOriginatorRequest.DocumentLoanApplicationId = "sd435";
            sendFileToExternalOriginatorRequest.LoanApplicationId = 1;
           
           
            IActionResult result = await controller.SendFileToExternalOriginator(sendFileToExternalOriginatorRequest);
            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestSendFileToExternalOriginatorWithDocNameController()
        {
            //Arrange
            Mock<IMappingService> mockMappingService = new Mock<IMappingService>();
          
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            Mock<IByteDocTypeMappingService> mockBytedocTypeMapping = new Mock<IByteDocTypeMappingService>();
            Mock<IByteDocStatusMappingService> mockBytedocstatusMapping = new Mock<IByteDocStatusMappingService>();
            Mock<IRainmakerService> mockRainMakerMapping = new Mock<IRainmakerService>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            string documentResponse = @"{
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
            messages.Add("http://test.com/api/bytewebconnector/document/senddocument", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(documentResponse)
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/view?id=sd435&requestid=s34&docid=d4&fileid=d43&tenantid=1", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{   \"id\": null,   \"requestId\": null,   \"docId\": null,   \"docName\": null,   \"status\": null,   \"createdOn\": \"0001-01-01T00:00:00\",   \"files\": [{   \"id\": null,   \"clientName\": null,   \"fileUploadedOn\": \"0001-01-01T00:00:00\",   \"mcuName\": null,   \"byteProStatus\": null,   \"status\": null }],   \"typeId\": null,   \"userName\": null }] ")
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/getdocuments?loanapplicationid=1&tenantid=1&pending=false", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{   \"id\": null,   \"requestId\": null,   \"docId\": \"d4\",   \"docName\": \"Bank\",   \"status\": null,   \"createdOn\": \"0001-01-01T00:00:00\",   \"files\": [{   \"id\": \"d43\",   \"clientName\": \"clientname.pdf\",   \"fileUploadedOn\": \"0001-01-01T00:00:00\",   \"mcuName\": \"mcuname.pdf\",   \"byteProStatus\": null,   \"status\": null }],   \"typeId\": null,   \"userName\": null }] ")
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/updatebyteprostatus", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("")
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
                    Content = new StringContent(documentResponse, Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
            

            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
            List<ByteDocTypeMapping> byteDocTypeMapping = new List<ByteDocTypeMapping>() { new ByteDocTypeMapping() { Id = 1, RmDocTypeName = "Other", ByteDocCategoryMapping = new ByteDocCategoryMapping() { ByteDocCategoryName = "", RmDocCategoryName = "", Id = 1 } } };
            mockBytedocTypeMapping.Setup(x => x.GetByteDocTypeMappingWithDetails(It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<ByteDocTypeMappingService.RelatedEntities>())).Returns(byteDocTypeMapping);
            List<ByteDocStatusMapping> byteDocStatusMapping = new List<ByteDocStatusMapping>() { new ByteDocStatusMapping() { Id = 1, ByteDocStatusName = "", RmDocStatusName = "" } };
            mockBytedocstatusMapping.Setup(x => x.GetByteDocStatusMappingWithDetails(It.IsAny<string>())).Returns(byteDocStatusMapping);
            List<_Mapping> mappings = new List<_Mapping>();

            _Mapping mapping = new _Mapping();
            mapping.RMEnittyId = "1";
            mappings.Add(mapping);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Bearer")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            

            var controller = new DocumentController(httpClientFactory.Object, mockConfiguration.Object, mockMappingService.Object, Mock.Of<ILogger<DocumentController>>(), mockBytedocTypeMapping.Object, mockBytedocstatusMapping.Object, mockRainMakerMapping.Object);

            controller.ControllerContext = context;

            //Act
            SendFileToExternalOriginatorRequest sendFileToExternalOriginatorRequest = new SendFileToExternalOriginatorRequest();
            sendFileToExternalOriginatorRequest.DocumentId = "d4";
            sendFileToExternalOriginatorRequest.FileId = "d43";
            sendFileToExternalOriginatorRequest.RequestId = "s34";
            sendFileToExternalOriginatorRequest.DocumentLoanApplicationId = "sd435";
            sendFileToExternalOriginatorRequest.LoanApplicationId = 1;
           
           
            IActionResult result = await controller.SendFileToExternalOriginator(sendFileToExternalOriginatorRequest);
            //Assert
            Assert.IsType<OkResult>(result);
        }
        
        
        [Fact]
        public async Task TestGetByteProGetDocumentsFail()
        {
            //Arrange
            Mock<IMappingService> mockMappingService = new Mock<IMappingService>();
           
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            Mock<IByteDocTypeMappingService> mockBytedocTypeMapping = new Mock<IByteDocTypeMappingService>();
            Mock<IByteDocStatusMappingService> mockBytedocstatusMapping = new Mock<IByteDocStatusMappingService>();
            Mock<IRainmakerService> mockRainMakerMapping = new Mock<IRainmakerService>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            string documentResponse = @"{
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
            messages.Add("http://test.com/api/bytewebconnector/document/senddocument", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(documentResponse)
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/view?id=sd435&requestid=s34&docid=d4&fileid=d43&tenantid=1", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("")
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/getdocuments?loanapplicationid=1&tenantid=1&pending=false", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("[{   \"id\": null,   \"requestId\": null,   \"docId\": \"d4\",   \"docName\": \"Bank\",   \"status\": null,   \"createdOn\": \"0001-01-01T00:00:00\",   \"files\": [{   \"id\": \"d43\",   \"clientName\": null,   \"fileUploadedOn\": \"0001-01-01T00:00:00\",   \"mcuName\": null,   \"byteProStatus\": null,   \"status\": null }],   \"typeId\": null,   \"userName\": null }] ")
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/updatebyteprostatus", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("")
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
                    Content = new StringContent(documentResponse, Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
          

            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
            List<ByteDocTypeMapping> byteDocTypeMapping = new List<ByteDocTypeMapping>() { new ByteDocTypeMapping() { Id = 1, RmDocTypeName = "Other", ByteDocCategoryMapping = new ByteDocCategoryMapping() { ByteDocCategoryName = "", RmDocCategoryName = "", Id = 1 } } };
            mockBytedocTypeMapping.Setup(x => x.GetByteDocTypeMappingWithDetails(It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<ByteDocTypeMappingService.RelatedEntities>())).Returns(byteDocTypeMapping);
            List<ByteDocStatusMapping> byteDocStatusMapping = new List<ByteDocStatusMapping>() { new ByteDocStatusMapping() { Id = 1, ByteDocStatusName = "", RmDocStatusName = "" } };
            mockBytedocstatusMapping.Setup(x => x.GetByteDocStatusMappingWithDetails(It.IsAny<string>())).Returns(byteDocStatusMapping);
            List<_Mapping> mappings = new List<_Mapping>();

            _Mapping mapping = new _Mapping();
            mapping.RMEnittyId = "1";
            mappings.Add(mapping);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Bearer")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

           

            var controller = new DocumentController(httpClientFactory.Object, mockConfiguration.Object, mockMappingService.Object, Mock.Of<ILogger<DocumentController>>(), mockBytedocTypeMapping.Object, mockBytedocstatusMapping.Object, mockRainMakerMapping.Object);

            controller.ControllerContext = context;

            //Act
            SendFileToExternalOriginatorRequest sendFileToExternalOriginatorRequest = new SendFileToExternalOriginatorRequest();
            sendFileToExternalOriginatorRequest.DocumentId = "d4";
            sendFileToExternalOriginatorRequest.FileId = "d43";
            sendFileToExternalOriginatorRequest.RequestId = "s34";
            sendFileToExternalOriginatorRequest.DocumentLoanApplicationId = "sd435";
            sendFileToExternalOriginatorRequest.LoanApplicationId = 1;
            
            IActionResult result = await controller.SendFileToExternalOriginator(sendFileToExternalOriginatorRequest);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task TestUpdateByteStatusInDocumentManagementFail()
        {
            //Arrange
            Mock<IMappingService> mockMappingService = new Mock<IMappingService>();
           
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            Mock<IByteDocTypeMappingService> mockBytedocTypeMapping = new Mock<IByteDocTypeMappingService>();
            Mock<IByteDocStatusMappingService> mockBytedocstatusMapping = new Mock<IByteDocStatusMappingService>();
            Mock<IRainmakerService> mockRainMakerMapping = new Mock<IRainmakerService>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            string documentResponse = @"{
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
            messages.Add("http://test.com/api/bytewebconnector/document/senddocument", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(documentResponse)
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/view?id=sd435&requestid=s34&docid=d4&fileid=d43&tenantid=1", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("")
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/getdocuments?loanapplicationid=1&tenantid=1&pending=false", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("[{   \"id\": null,   \"requestId\": null,   \"docId\": \"d4\",   \"docName\": \"Bank\",   \"status\": null,   \"createdOn\": \"0001-01-01T00:00:00\",   \"files\": [{   \"id\": \"d43\",   \"clientName\": null,   \"fileUploadedOn\": \"0001-01-01T00:00:00\",   \"mcuName\": null,   \"byteProStatus\": null,   \"status\": null }],   \"typeId\": null,   \"userName\": null }] ")
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/updatebyteprostatus", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("")
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
                    Content = new StringContent(documentResponse, Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
           

            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
            List<ByteDocTypeMapping> byteDocTypeMapping = new List<ByteDocTypeMapping>() { new ByteDocTypeMapping() { Id = 1, RmDocTypeName = "Other", ByteDocCategoryMapping = new ByteDocCategoryMapping() { ByteDocCategoryName = "", RmDocCategoryName = "", Id = 1 } } };
            mockBytedocTypeMapping.Setup(x => x.GetByteDocTypeMappingWithDetails(It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<ByteDocTypeMappingService.RelatedEntities>())).Returns(byteDocTypeMapping);
            List<ByteDocStatusMapping> byteDocStatusMapping = new List<ByteDocStatusMapping>() { new ByteDocStatusMapping() { Id = 1, ByteDocStatusName = "", RmDocStatusName = "" } };
            mockBytedocstatusMapping.Setup(x => x.GetByteDocStatusMappingWithDetails(It.IsAny<string>())).Returns(byteDocStatusMapping);
            List<_Mapping> mappings = new List<_Mapping>();

            _Mapping mapping = new _Mapping();
            mapping.RMEnittyId = "1";
            mappings.Add(mapping);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Bearer")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

           

            var controller = new DocumentController(httpClientFactory.Object, mockConfiguration.Object, mockMappingService.Object, Mock.Of<ILogger<DocumentController>>(), mockBytedocTypeMapping.Object, mockBytedocstatusMapping.Object, mockRainMakerMapping.Object);

            controller.ControllerContext = context;

            //Act
            SendFileToExternalOriginatorRequest sendFileToExternalOriginatorRequest = new SendFileToExternalOriginatorRequest();
            sendFileToExternalOriginatorRequest.DocumentId = "d4";
            sendFileToExternalOriginatorRequest.FileId = "d43";
            sendFileToExternalOriginatorRequest.RequestId = "s34";
            sendFileToExternalOriginatorRequest.DocumentLoanApplicationId = "sd435";
            sendFileToExternalOriginatorRequest.LoanApplicationId = 1;
           
            IActionResult result = await controller.SendFileToExternalOriginator(sendFileToExternalOriginatorRequest);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task TestSendDocumentToExternalOriginatorFail()
        {
            //Arrange
            Mock<IMappingService> mockMappingService = new Mock<IMappingService>();
           
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            Mock<IByteDocTypeMappingService> mockBytedocTypeMapping = new Mock<IByteDocTypeMappingService>();
            Mock<IByteDocStatusMappingService> mockBytedocstatusMapping = new Mock<IByteDocStatusMappingService>();
            Mock<IRainmakerService> mockRainMakerMapping = new Mock<IRainmakerService>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            string documentResponse = @"{
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
            messages.Add("http://test.com/api/bytewebconnector/document/senddocument", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(documentResponse)
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/view?id=sd435&requestid=s34&docid=d4&fileid=d43&tenantid=1", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("asdasdasdasd")
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/getdocuments?loanapplicationid=1&tenantid=1&pending=false", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{   \"id\": null,   \"requestId\": null,   \"docId\": \"d4\",   \"docName\": \"Bank\",   \"status\": null,   \"createdOn\": \"0001-01-01T00:00:00\",   \"files\": [{   \"id\": \"d43\",   \"clientName\": \"clientname.pdf\",   \"fileUploadedOn\": \"0001-01-01T00:00:00\",   \"mcuName\": \"mcuname.pdf\",   \"byteProStatus\": null,   \"status\": null }],   \"typeId\": null,   \"userName\": null }] ")
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/updatebyteprostatus", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("")
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
                    Content = new StringContent(documentResponse, Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
           

            
            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
            List<ByteDocTypeMapping> byteDocTypeMapping = new List<ByteDocTypeMapping>() { new ByteDocTypeMapping() { Id = 1, RmDocTypeName = "Other", ByteDocCategoryMapping = new ByteDocCategoryMapping() { ByteDocCategoryName = "", RmDocCategoryName = "", Id = 1 } } };
            mockBytedocTypeMapping.Setup(x => x.GetByteDocTypeMappingWithDetails(It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<ByteDocTypeMappingService.RelatedEntities>())).Returns(byteDocTypeMapping);
            List<ByteDocStatusMapping> byteDocStatusMapping = new List<ByteDocStatusMapping>() { new ByteDocStatusMapping() { Id = 1, ByteDocStatusName = "", RmDocStatusName = "" } };
            mockBytedocstatusMapping.Setup(x => x.GetByteDocStatusMappingWithDetails(It.IsAny<string>())).Returns(byteDocStatusMapping);
            List<_Mapping> mappings = new List<_Mapping>();

            _Mapping mapping = new _Mapping();
            mapping.RMEnittyId = "1";
            mappings.Add(mapping);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Bearer")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

           

            var controller = new DocumentController(httpClientFactory.Object, mockConfiguration.Object, mockMappingService.Object, Mock.Of<ILogger<DocumentController>>(), mockBytedocTypeMapping.Object, mockBytedocstatusMapping.Object, mockRainMakerMapping.Object);

            controller.ControllerContext = context;

            //Act
            SendFileToExternalOriginatorRequest sendFileToExternalOriginatorRequest = new SendFileToExternalOriginatorRequest();
            sendFileToExternalOriginatorRequest.DocumentId = "d4";
            sendFileToExternalOriginatorRequest.FileId = "d43";
            sendFileToExternalOriginatorRequest.RequestId = "s34";
            sendFileToExternalOriginatorRequest.DocumentLoanApplicationId = "sd435";
            sendFileToExternalOriginatorRequest.LoanApplicationId = 1;
           
           
            IActionResult result = await controller.SendFileToExternalOriginator(sendFileToExternalOriginatorRequest);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task TestSendDocumentToExternalOriginatorException()
        {
            //Arrange
            Mock<IMappingService> mockMappingService = new Mock<IMappingService>();
            
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            Mock<IByteDocTypeMappingService> mockBytedocTypeMapping = new Mock<IByteDocTypeMappingService>();
            Mock<IByteDocStatusMappingService> mockBytedocstatusMapping = new Mock<IByteDocStatusMappingService>();
            Mock<IRainmakerService> mockRainMakerMapping = new Mock<IRainmakerService>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            string documentResponse = @"{
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
            messages.Add("http://test.com/api/bytewebconnector/document/senddocument", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("asdasdasdasd")
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/view?id=sd435&requestid=s34&docid=d4&fileid=d43&tenantid=1", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("asdasdasdasd")
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/getdocuments?loanapplicationid=1&tenantid=1&pending=false", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{   \"id\": null,   \"requestId\": null,   \"docId\": \"d4\",   \"docName\": \"Bank\",   \"status\": null,   \"createdOn\": \"0001-01-01T00:00:00\",   \"files\": [{   \"id\": \"d43\",   \"clientName\": \"clientname.pdf\",   \"fileUploadedOn\": \"0001-01-01T00:00:00\",   \"mcuName\": \"mcuname.pdf\",   \"byteProStatus\": null,   \"status\": null }],   \"typeId\": null,   \"userName\": null }] ")
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/updatebyteprostatus", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("")
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
                    Content = new StringContent(documentResponse, Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
           

            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
            List<ByteDocTypeMapping> byteDocTypeMapping = new List<ByteDocTypeMapping>() { new ByteDocTypeMapping() { Id = 1, RmDocTypeName = "Other", ByteDocCategoryMapping = new ByteDocCategoryMapping() { ByteDocCategoryName = "", RmDocCategoryName = "", Id = 1 } } };
            mockBytedocTypeMapping.Setup(x => x.GetByteDocTypeMappingWithDetails(It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<ByteDocTypeMappingService.RelatedEntities>())).Returns(byteDocTypeMapping);
            List<ByteDocStatusMapping> byteDocStatusMapping = new List<ByteDocStatusMapping>() { new ByteDocStatusMapping() { Id = 1, ByteDocStatusName = "", RmDocStatusName = "" } };
            mockBytedocstatusMapping.Setup(x => x.GetByteDocStatusMappingWithDetails(It.IsAny<string>())).Returns(byteDocStatusMapping);
            List<_Mapping> mappings = new List<_Mapping>();

            _Mapping mapping = new _Mapping();
            mapping.RMEnittyId = "1";
            mappings.Add(mapping);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Bearer")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

          

            var controller = new DocumentController(httpClientFactory.Object, mockConfiguration.Object, mockMappingService.Object, Mock.Of<ILogger<DocumentController>>(), mockBytedocTypeMapping.Object, mockBytedocstatusMapping.Object, mockRainMakerMapping.Object);

            controller.ControllerContext = context;

            //Act
            SendFileToExternalOriginatorRequest sendFileToExternalOriginatorRequest = new SendFileToExternalOriginatorRequest();
            sendFileToExternalOriginatorRequest.DocumentId = "d4";
            sendFileToExternalOriginatorRequest.FileId = "d43";
            sendFileToExternalOriginatorRequest.RequestId = "s34";
            sendFileToExternalOriginatorRequest.DocumentLoanApplicationId = "sd435";
            sendFileToExternalOriginatorRequest.LoanApplicationId = 1;
           
            IActionResult result = await controller.SendFileToExternalOriginator(sendFileToExternalOriginatorRequest);
            //Assert
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public async Task TestSendDocumentToExternalOriginatorBadRequest()
        {
            //Arrange
            Mock<IMappingService> mockMappingService = new Mock<IMappingService>();
          
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            Mock<IByteDocTypeMappingService> mockBytedocTypeMapping = new Mock<IByteDocTypeMappingService>();
            Mock<IByteDocStatusMappingService> mockBytedocstatusMapping = new Mock<IByteDocStatusMappingService>();
            Mock<IRainmakerService> mockRainMakerMapping = new Mock<IRainmakerService>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            string documentResponse = @"{
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
            messages.Add("http://test.com/api/bytewebconnector/document/senddocument", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("asdasdasdasd")
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/view?id=sd435&requestid=s34&docid=d4&fileid=d43&tenantid=1", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("asdasdasdasd")
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/getdocuments?loanapplicationid=1&tenantid=1&pending=false", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{   \"id\": null,   \"requestId\": null,   \"docId\": \"d4\",   \"docName\": \"Bank\",   \"status\": null,   \"createdOn\": \"0001-01-01T00:00:00\",   \"files\": [{   \"id\": \"d43\",   \"clientName\": null,   \"fileUploadedOn\": \"0001-01-01T00:00:00\",   \"mcuName\": null,   \"byteProStatus\": null,   \"status\": null }],   \"typeId\": null,   \"userName\": null }] ")
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/updatebyteprostatus", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("")
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
                    Content = new StringContent(documentResponse, Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));

            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
            List<ByteDocTypeMapping> byteDocTypeMapping = new List<ByteDocTypeMapping>() { new ByteDocTypeMapping() { Id = 1, RmDocTypeName = "Other", ByteDocCategoryMapping = new ByteDocCategoryMapping() { ByteDocCategoryName = "", RmDocCategoryName = "", Id = 1 } } };
            mockBytedocTypeMapping.Setup(x => x.GetByteDocTypeMappingWithDetails(It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<ByteDocTypeMappingService.RelatedEntities>())).Returns(byteDocTypeMapping);
            List<ByteDocStatusMapping> byteDocStatusMapping = new List<ByteDocStatusMapping>() { new ByteDocStatusMapping() { Id = 1, ByteDocStatusName = "", RmDocStatusName = "" } };
            mockBytedocstatusMapping.Setup(x => x.GetByteDocStatusMappingWithDetails(It.IsAny<string>())).Returns(byteDocStatusMapping);
            List<_Mapping> mappings = new List<_Mapping>();

            _Mapping mapping = new _Mapping();
            mapping.RMEnittyId = "1";
            mappings.Add(mapping);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Bearer")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            

            var controller = new DocumentController(httpClientFactory.Object, mockConfiguration.Object, mockMappingService.Object, Mock.Of<ILogger<DocumentController>>(), mockBytedocTypeMapping.Object, mockBytedocstatusMapping.Object, mockRainMakerMapping.Object);

            controller.ControllerContext = context;

            //Act
            SendDocumentToExternalOriginatorRequest sendDocumentToExternalOriginatorRequest = new SendDocumentToExternalOriginatorRequest();
            sendDocumentToExternalOriginatorRequest.LoanApplicationId = 1;
            sendDocumentToExternalOriginatorRequest.DocumentId = "1";
            sendDocumentToExternalOriginatorRequest.DocumentLoanApplicationId = "1";
            sendDocumentToExternalOriginatorRequest.RequestId = "1";
            //Not moq beacuse of static method call in API.
            IActionResult result = await controller.SendDocumentToExternalOriginator(sendDocumentToExternalOriginatorRequest);
            //Assert
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task TestSendDocumentToExternalOriginatort()
        {
            //Arrange
            Mock<IMappingService> mockMappingService = new Mock<IMappingService>();
         
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            Mock<IByteDocTypeMappingService> mockBytedocTypeMapping = new Mock<IByteDocTypeMappingService>();
            Mock<IByteDocStatusMappingService> mockBytedocstatusMapping = new Mock<IByteDocStatusMappingService>();
            Mock<IRainmakerService> mockRainMakerMapping = new Mock<IRainmakerService>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            string documentResponse = @"{
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
            messages.Add("http://test.com/api/bytewebconnector/document/senddocument", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("asdasdasdasd")
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/view?id=1&requestid=1&docid=1&fileid=d43&tenantid=1", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("asdasdasdasd")
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/getdocuments?loanapplicationid=1&tenantid=1&pending=false", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{   \"id\": null,   \"requestId\": null,   \"docId\": \"1\",   \"docName\": \"Bank\",   \"status\": null,   \"createdOn\": \"0001-01-01T00:00:00\",   \"files\": [{   \"id\": \"d43\",   \"clientName\": \"clientname.pdf\",   \"fileUploadedOn\": \"0001-01-01T00:00:00\",   \"mcuName\": \"mcuname.pdf\",   \"byteProStatus\": null,   \"status\": null }],   \"typeId\": null,   \"userName\": null }] ")
            });
            messages.Add("http://test.com/api/documentmanagement/bytepro/updatebyteprostatus", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("")
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
                    Content = new StringContent(documentResponse, Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));

            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
            List<ByteDocTypeMapping> byteDocTypeMapping = new List<ByteDocTypeMapping>() { new ByteDocTypeMapping() { Id = 1, RmDocTypeName = "Other", ByteDocCategoryMapping = new ByteDocCategoryMapping() { ByteDocCategoryName = "", RmDocCategoryName = "", Id = 1 } } };
            mockBytedocTypeMapping.Setup(x => x.GetByteDocTypeMappingWithDetails(It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<ByteDocTypeMappingService.RelatedEntities>())).Returns(byteDocTypeMapping);
            List<ByteDocStatusMapping> byteDocStatusMapping = new List<ByteDocStatusMapping>() { new ByteDocStatusMapping() { Id = 1, ByteDocStatusName = "", RmDocStatusName = "" } };
            mockBytedocstatusMapping.Setup(x => x.GetByteDocStatusMappingWithDetails(It.IsAny<string>())).Returns(byteDocStatusMapping);
            List<_Mapping> mappings = new List<_Mapping>();

            _Mapping mapping = new _Mapping();
            mapping.RMEnittyId = "1";
            mappings.Add(mapping);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Bearer")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

           

            var controller = new DocumentController(httpClientFactory.Object, mockConfiguration.Object, mockMappingService.Object, Mock.Of<ILogger<DocumentController>>(), mockBytedocTypeMapping.Object, mockBytedocstatusMapping.Object, mockRainMakerMapping.Object);

            controller.ControllerContext = context;

            //Act
            SendDocumentToExternalOriginatorRequest sendDocumentToExternalOriginatorRequest = new SendDocumentToExternalOriginatorRequest();
            sendDocumentToExternalOriginatorRequest.LoanApplicationId = 1;
            sendDocumentToExternalOriginatorRequest.DocumentId = "1";
            sendDocumentToExternalOriginatorRequest.DocumentLoanApplicationId = "1";
            sendDocumentToExternalOriginatorRequest.RequestId = "1";
            //Not moq beacuse of static method call in API.
            IActionResult result = await controller.SendDocumentToExternalOriginator(sendDocumentToExternalOriginatorRequest);
            //Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
