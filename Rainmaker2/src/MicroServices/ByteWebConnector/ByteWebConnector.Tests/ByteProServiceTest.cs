using AutoMapper;
using ByteWebConnector.Model.Models;
using ByteWebConnector.Model.Models.ActionModels.LoanFile;
using ByteWebConnector.Model.Models.OwnModels.Settings;
using ByteWebConnector.Model.Models.ServiceResponseModels.ByteApi;
using ByteWebConnector.Service.DbServices;
using ByteWebConnector.Service.ExternalServices;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Extensions.ExtensionClasses;
using Task = System.Threading.Tasks.Task;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using ByteWebConnector.Model.Models.ServiceResponseModels.ByteWebConnectorSDK;
using ByteWebConnector.Service.InternalServices;
using ByteWebConnector.Model.Models.ServiceRequestModels.ByteWebConnectorSDK;
using Setting = ByteWebConnector.Entity.Models.Setting;
using ServiceCallHelper;

namespace ByteWebConnector.Tests
{
    public class ByteProServiceTest
    {
        [Fact]
        public void SendFileService()
        {
            Mock<ISettingService> settingService = new Mock<ISettingService>();
            Mock<IMapper> mappingService = new Mock<IMapper>();
            ByteFile file = new ByteFile()
            {
                Applications = new List<Application>()
                                               {
                                                   new Application()
                                                   {
                                                       ApplicationId = 1004001,
                                                       Borrower = new ByteBorrower()
                                                                  {
                                                                      Employers = new List<Employer>()
                                                                                  {
                                                                                      new Employer(),
                                                                                  },
                                                                      Residences = new List<Residence>()
                                                                                   {
                                                                                       new Residence()
                                                                                   },
                                                                      Incomes = new List<Income>()
                                                                                {
                                                                          new Income(),
                                                                          new Income()
                                                                                },
                                                                      REOs = new List<Reo>(),
                                                                      Debts = new List<Debt>(),

                                                                      Borrower = new Model.Models.Borrower()
                                                                       {
                                                                           FirstName = "Danish"
                                                                       },
                                                            BorrowerID = 1083333
                                                                  },
                                                       BorrowerId = 1004012,
                                                       CoBorrower = new ByteBorrower()
                                                                    {   Employers = new List<Employer>()
                                                                                    {
                                                                                        new Employer(),
                                                                                        new Employer()
                                                                                    },
                                                                        Residences = new List<Residence>()
                                                                                     {
                                                                                         new Residence()
                                                                                     },
                                                                        Incomes = new List<Income>()
                                                                                  {
                                                                                      new Income(),
                                                                                      new Income()
                                                                                  },
                                                                        REOs = new List<Reo>(),
                                                                        Debts = new List<Debt>(),
                                                           Borrower = new Model.Models.Borrower()
                                                                      {
                                                                          FirstName = "Hammad"
                                                                      },
                                                           BorrowerID = 1004012,

                                                                    },
                                                       CoBorrowerId = 1083333
                                                   },
                                                   new Application()
                                                   {
                                                       ApplicationId = 1004001,
                                                       Borrower = new ByteBorrower()
                                                                  {
                                                                      Employers = new List<Employer>()
                                                                                  {
                                                                                      new Employer(),
                                                                                      new Employer()
                                                                                  },
                                                                      Residences = new List<Residence>()
                                                                                   {
                                                                                       new Residence()
                                                                                   },
                                                                      Incomes = new List<Income>()
                                                                                {
                                                                          new Income(),
                                                                          new Income()
                                                                                },
                                                                      REOs = new List<Reo>(),
                                                                      Debts = new List<Debt>(),

                                                                      Borrower = new Model.Models.Borrower()
                                                                       {
                                                                           FirstName = "Danish"
                                                                       },
                                                            BorrowerID = 1083333
                                                                  },
                                                       BorrowerId = 1004012,
                                                   },

                                               },
                FileData = new FileData(),
                FileDataID = 1004084,
                Loan = new Loan(),
                Parties = new List<Party>()
                          {
                              new Party(),
                              new Party(),
                              new Party()
                          },
                PrepaidItems = new List<PrepaidItem>()
                               {
                                   new PrepaidItem(),
                                   new PrepaidItem(),
                                   new PrepaidItem(),
                                   new PrepaidItem(),
                               },
                Status = new Status(),
                SubProp = new SubProp(),
                CustomField = new CustomField()
                {
                    Field01 = "bank"
                },
                CustomValues = new List<CustomValue>()
                {

                }
            };
            ByteNewFileResponse bytenewFileresponse = new ByteNewFileResponse()
            {
                OrganizationCode = "2002",
                TemplateId = 0,
                SubPropState = "TX",
                DesiredOriginationChannel = 0,
                LoanOfficerUserName = "Aliya.Prasla",
                LoanProcessorUserName = "",
                FileDataId = 1004084,
                FileName = "90020000162",
                BorrowerFirstName = "Jill",
                BorrowerLastName = "Tester",
                BorrowerMiddleName = "",
                BorrowerSuffix = "",
                CoBorrowerFirstName = "Joe",
                CoBorrowerLastName = "Tester",
                CoBorrowerMiddleName = "",
                CoBorrowerSuffix = "",
                LoanAmount = 0.0,
                ApplicationId = 1004100
            };
            ByteNewFile newfile = new ByteNewFile()
            {
                OrganizationCode = "2002",
                SubPropState = "TX",
                LoanOfficerUserName = "Aliya.Prasla"
            };

            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("https://devbyteapi123.rainsoftfn.com/byteapi/newfile/", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(bytenewFileresponse.ToJson())
            });
            messages.Add("https://devbyteapi123.rainsoftfn.com/byteapi/loanfile/" + 1004084, new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(file.ToJson())
            });
            messages.Add("https://devbyteapi123.rainsoftfn.com/byteapi/loanfile/", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(file.ToJson())
            });

            messages.Add("https://devbyteapi123.rainsoftfn.com/byteapi/auth/", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("t+/qXgIlfua90bVVMVPjWlCYIO9c7yjeb+JY2CioL7o9Zt6SVHSrMLG2gK+RzGK4c58b2Tna/bSXPa810TZTSQ==")
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
                    Content = new StringContent(bytenewFileresponse.ToJson(), Encoding.UTF8, "application/json"),
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

            List<Entity.Models.Setting> lsSettings = new List<Entity.Models.Setting>();
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

            //Act
            LoanFileRequest loanFileRequest = new LoanFileRequest()
            {
                LoanApplication = new LoanApplication()
                {
                    Id = 1033,
                    Borrowers = new List<Model.Models.ActionModels.LoanFile.Borrower>()
                                {
                                    new Model.Models.ActionModels.LoanFile.Borrower()
                                    {
                                        Id = 1,
                                        LoanContact = new LoanContact(),
                                        BorrowerResidences = new List<BorrowerResidence>()
                                                             {
                                                                 new BorrowerResidence()
                                                                 {

                                                                     FromDate = new DateTime(2020,1,1),
                                                                     ToDate = new DateTime(2020,5,5),
                                                                     MonthlyRent = 2000,
                                                                     LoanAddress = new AddressInfo()
                                                                                   {
                                                                                       CityName = "Karachi",
                                                                                       StateName = "",
                                                                                       CountyName = "",
                                                                                       StreetAddress = "",
                                                                                       UnitNo = "2",
                                                                                       ZipCode = "67655"
                                                                                   }
                                                                 }
                                                             }
                                    },
                                    new Model.Models.ActionModels.LoanFile.Borrower()
                                    {
                                        Id = 2,
                                        LoanContact = new LoanContact()
                                    },
                                    new Model.Models.ActionModels.LoanFile.Borrower()
                                    {
                                        Id = 3,
                                        LoanContact = new LoanContact()
                                    },
                                   
                                },
                    LoanGoal = new LoanGoal(),
                    BusinessUnit = new BusinessUnit(),
                    PropertyInfo = new PropertyInfo()
                    {
                        MortgageOnProperties = new List<MortgageOnProperty>(),
                        AddressInfo = new AddressInfo()
                        {
                            State = new State()
                            {
                                Abbreviation = "TX"
                            }
                        },
                        HoaDues = 500,
                        PropertyTaxEscrows = new List<PropertyTaxEscrow>()
                                              {
                                                   new PropertyTaxEscrow()
                                                   {
                                                        EscrowEntityTypeId = 1,
                                                        AnnuallyPayment = 5000
                                                   },
                                                   new PropertyTaxEscrow()
                                                   {
                                                        EscrowEntityTypeId = 2,
                                                        AnnuallyPayment = 4000
                                                   }
                                              }
                    },
                    Opportunity = new Opportunity()
                    {
                        Id = 0,
                        BusinessUnit = new BusinessUnit()
                        {
                            LeadSource = new LeadSource() { Id = 0 }
                        },

                        Owner = new Employee()
                        {
                            Contact = new Model.Models.ActionModels.LoanFile.Contact(),
                            EmployeePhoneBinders = new List<EmployeePhoneBinder>()
                                                   {
                                                     new EmployeePhoneBinder(){ CompanyPhoneInfo = new CompanyPhoneInfo()}
                                                   },
                            EmployeeBusinessUnitEmails = new List<EmployeeBusinessUnitEmail>()
                                                         {
                                                             new EmployeeBusinessUnitEmail()
                                                             {
                                                                  EmailAccount = new EmailAccount()
                                                             }
                                                         }
                        }
                    }
                },
                LoanRequest = new LoanRequest(),
                ThirdPartyCodeList = new ThirdPartyCodeList() { ThirdPartyCodes = new List<ThirdPartyCode>() }
            };
            byteProService.SendFile(loanFileRequest);
        }


        [Fact]
        public void GetLoanStatusAsyncService()
        {
            Mock<ISettingService> settingService = new Mock<ISettingService>();
            Mock<IMapper> mappingService = new Mock<IMapper>();
            StatusResponse statusResponse = new StatusResponse();
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("https://devbyteapi123.rainsoftfn.com/byteapi/status/" + 10345677, new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(statusResponse.ToJson())
            });
            messages.Add("https://devbyteapi123.rainsoftfn.com/byteapi/auth/", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("t+/qXgIlfua90bVVMVPjWlCYIO9c7yjeb+JY2CioL7o9Zt6SVHSrMLG2gK+RzGK4c58b2Tna/bSXPa810TZTSQ==")
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
                    Content = new StringContent(statusResponse.ToJson(), Encoding.UTF8, "application/json"),
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

            List<Entity.Models.Setting> lsSettings = new List<Entity.Models.Setting>();
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
            Task<short> result = byteProService.GetLoanStatusAsync(10345677);

            
            Assert.NotEqual(-1,result.Result);
        }

        [Fact]
        public void SendDocumentToByteService()
        {
            Mock<ISettingService> settingService = new Mock<ISettingService>();
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            HttpContext httpContext = new DefaultHttpContext();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            ServiceCallHelper.AppContext.Configure(contextAccessorMock.Object);
            contextAccessorMock.Setup(_ => _.HttpContext).Returns(httpContext);
            SendSdkDocumentResponse sendSdkDocumentResponse = new SendSdkDocumentResponse();
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/bytewebconnectorsdk/document/sendsdkdocument", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(sendSdkDocumentResponse.ToJson())
            });
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
                    Content = new StringContent(sendSdkDocumentResponse.ToJson(), Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
            httpClientFactory.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(httpClient);
            IByteWebConnectorSdkService byteWebConnectorSdkService = new ByteWebConnectorSdkService(contextAccessorMock.Object, httpClient, mockConfiguration.Object, settingService.Object);
            DocumentUploadRequest documentUploadRequest = new DocumentUploadRequest() { FileDataId = 1,FileName="byte"};
            CallResponse<SendSdkDocumentResponse> result = byteWebConnectorSdkService.SendDocumentToByte(documentUploadRequest);
            Assert.NotNull(result);

        }

        [Fact]
        public void SendLoanApplicationToByteViaSDKService()
        {
            Mock<ISettingService> settingService = new Mock<ISettingService>();
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            HttpContext httpContext = new DefaultHttpContext();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            ServiceCallHelper.AppContext.Configure(contextAccessorMock.Object);
            contextAccessorMock.Setup(_ => _.HttpContext).Returns(httpContext);
            SendLoanApplicationResponse sendLoanApplicationResponse = new SendLoanApplicationResponse();
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/bytewebconnectorsdk/loanapplication/postloanapplicationtobyte", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(sendLoanApplicationResponse.ToJson())
            });
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
                    Content = new StringContent(sendLoanApplicationResponse.ToJson(), Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
            httpClientFactory.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(httpClient);
            IByteWebConnectorSdkService byteWebConnectorSdkService = new ByteWebConnectorSdkService(contextAccessorMock.Object, httpClient, mockConfiguration.Object, settingService.Object);
            LoanFileRequest loanFileRequest = new LoanFileRequest();
            var result = byteWebConnectorSdkService.SendLoanApplicationToByteViaSDK(loanFileRequest);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetLoanApplicationStatusNameViaSDKService()
        {
            Mock<ISettingService> settingService = new Mock<ISettingService>();
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            HttpContext httpContext = new DefaultHttpContext();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            ServiceCallHelper.AppContext.Configure(contextAccessorMock.Object);
            contextAccessorMock.Setup(_ => _.HttpContext).Returns(httpContext);
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/bytewebconnectorsdk/loanapplication/getloanapplicationstatusname", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("")
            });
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
                    Content = new StringContent("", Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
            httpClientFactory.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(httpClient);
            IByteWebConnectorSdkService byteWebConnectorSdkService = new ByteWebConnectorSdkService(contextAccessorMock.Object, httpClient, mockConfiguration.Object, settingService.Object);
            var result = byteWebConnectorSdkService.GetLoanApplicationStatusNameViaSDK("byte");

            Assert.Null(result);
        }

        [Fact]
        public void ValidateByteSessionService()
        {
            Mock<ISettingService> settingService = new Mock<ISettingService>();
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            Mock<IMapper> mockMapper= new Mock<IMapper>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            HttpContext httpContext = new DefaultHttpContext();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            ServiceCallHelper.AppContext.Configure(contextAccessorMock.Object);
            contextAccessorMock.Setup(_ => _.HttpContext).Returns(httpContext);
            Setting byteApiAuthKey = new Setting();
            byteApiAuthKey.Name = "ByteApiAuthKey";

            Setting byteApiUrl = new Setting();
            byteApiUrl.Name = "ByteApiUrl";

            Setting byteAuthKey = new Setting();
            byteAuthKey.Name = "ByteAuthKey";

            Setting byteCompanyCode = new Setting();
            byteCompanyCode.Name = "ByteCompanyCode";

            Setting byteApiPassword = new Setting();
            byteApiPassword.Name = "ByteApiPassword";

            Setting byteApiUserName = new Setting();
            byteApiUserName.Name = "ByteApiUserName";

            Setting bytePassword = new Setting();
            bytePassword.Name = "BytePassword";

            Setting byteUserName = new Setting();
            byteUserName.Name = "ByteUserName";

            Setting byteUserNo = new Setting();
            byteUserNo.Name = "ByteUserNo";

            Setting byteConnectionName = new Setting();
            byteConnectionName.Name = "ByteConnectionName";
            List<Setting> lsSettings = new List<Setting>();
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
            byteProSettings.ByteApiUrl = "http://test.com/";
            byteProSettings.ByteApiAuthKey = "6c49dddd-0000-5555-dddd-3efe20588111";
            byteProSettings.ByteApiUserName = "test";
            byteProSettings.ByteApiPassword = "123";
            settingService.Setup(x => x.GetByteProSettings()).Returns(byteProSettings);
           
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/organization/", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("")
            });
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
                    Content = new StringContent("", Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
            httpClientFactory.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(httpClient);
            IByteProService byteProService = new ByteProService(Mock.Of<ILogger<ByteProService>>(), settingService.Object,httpClient, mockMapper.Object);
        }
    }
}
