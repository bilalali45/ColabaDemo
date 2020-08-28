using ByteWebConnector.API.Controllers;
using ByteWebConnector.API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Rainmaker.Service;
using RainMaker.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ByteWebConnector.Tests
{
    public class DocumentTest
    {
        [Fact]
        public async Task TestSendDocumentController()
        {
        //    //Arrangs
        //    Mock<ILoanApplicationService> mockLoanApplicationService = new Mock<ILoanApplicationService>();
        //    //Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
        //    //Mock<IConfigurationSection> oneSectionMock = new Mock<IConfigurationSection>();
        //    //oneSectionMock.Setup(s => s.Value).Returns("image/jpeg");
          
        //    LoanApplication loanApplication = new LoanApplication();
        //    loanApplication.Id = 1;
        //    loanApplication.EncompassNumber = "abc";

        //    List<LoanApplication> loanApplications = new List<LoanApplication>();
        //    loanApplications.Add(loanApplication);

        //    mockLoanApplicationService.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<LoanApplicationService.RelatedEntity?>())).Returns(loanApplications);
        //    string[] lstMediaTypesToBeWrappedInPdf = new string[1];
        //    lstMediaTypesToBeWrappedInPdf[0] = "image/jpeg";
        //    //IConfigurationSection configurationSection ;
        //    //configurationSection.GetSection($"MediaTypesToBeWrappedInPdf");
        //    //mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>()).Get<string[]>()).Returns(lstMediaTypesToBeWrappedInPdf);


        //    //mockConfiguration.SetupGet(x => x.GetSection(It.IsAny<string>()).Get<string[]>()).Returns(lstMediaTypesToBeWrappedInPdf);

        //    // mockConfiguration.SetupGet(x => x.GetSection(It.IsAny<string>()).Get<string[]>()).Returns(lstMediaTypesToBeWrappedInPdf);
        //    //mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("image/jpeg");

        //    var mockConfiguration = new Mock<IConfiguration>();

        //    var configurationSection = new Mock<IConfigurationSection>();
        //    configurationSection.Setup(a => a.Value).Returns("image/jpeg");

        //    //var configurationSection = new Mock<IConfigurationSection>();
        //    //configurationSection.SetupGet(m => m[It.Is<string>(s => s == "image/jpeg")]).Returns("image/jpeg");

        //    mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "MediaTypesToBeWrappedInPdf"))).Returns(configurationSection.Object);
        //    var controller = new DocumentController(mockLoanApplicationService.Object, null, null, mockConfiguration.Object, Mock.Of<ILogger<DocumentController>>());

        //    //Act
        //    SendDocumentRequest sendDocumentRequest = new SendDocumentRequest();
        //    sendDocumentRequest.LoanApplicationId = 1;
        //    sendDocumentRequest.MediaType = "image/jpeg";
        //    sendDocumentRequest.DocumentExension = "jpeg";
        //    ApiResponse result =  controller.SendDocument(sendDocumentRequest);

        //    //Assert
        //    Assert.NotNull(result);

        }
    }
}
