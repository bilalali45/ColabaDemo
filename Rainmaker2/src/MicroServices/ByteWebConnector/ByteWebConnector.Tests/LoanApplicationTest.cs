using ByteWebConnector.API.Controllers;
using ByteWebConnector.Model.Models;
using ByteWebConnector.Model.Models.ActionModels.LoanFile;
using ByteWebConnector.Service.ExternalServices;
using ByteWebConnector.Service.InternalServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ByteWebConnector.Tests
{
    public class LoanApplicationTest
    {
        [Fact]
        public void SendLoanFileController()
        {
            Mock<IByteProService> byteProService = new Mock<IByteProService>();
            Mock<IByteWebConnectorSdkService> bwcsdkService = new Mock<IByteWebConnectorSdkService>();
            LoanFileRequest loanFileRequest = new LoanFileRequest()
            {
                LoanApplication = new LoanApplication(),
                LoanRequest = new LoanRequest(),
                ThirdPartyCodeList = new ThirdPartyCodeList()

            };
            ByteFile byteFile = new ByteFile()
            {
                FileDataID = 101,
                FileData = new FileData() { FileName = "50932323" }
            };
            byteProService.Setup(x => x.SendFile(It.IsAny<LoanFileRequest>())).Returns(byteFile);

            var controller = new LoanFileController(Mock.Of<ILogger<LoanFileController>>(), byteProService.Object, bwcsdkService.Object);

            Microsoft.AspNetCore.Mvc.IActionResult actionResult = controller.SendLoanFile(loanFileRequest);
            Assert.NotNull(actionResult);
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async void GetLoanStatusController()
        {
            Mock<IByteProService> byteProService = new Mock<IByteProService>();
            Mock<IByteWebConnectorSdkService> bwcsdkService = new Mock<IByteWebConnectorSdkService>();
            var controller = new LoanFileController(Mock.Of<ILogger<LoanFileController>>(), byteProService.Object, bwcsdkService.Object);

            var loanStatus = controller.GetLoanStatus("10041252");
            Assert.NotNull(loanStatus.Result);
            Assert.IsType<OkObjectResult>(loanStatus.Result);

        }

        [Fact]
        public async void GetLoanStatusControllerBadRequest()
        {
            Mock<IByteProService> byteProService = new Mock<IByteProService>();
            Mock<IByteWebConnectorSdkService> bwcsdkService = new Mock<IByteWebConnectorSdkService>();
            var controller = new LoanFileController(Mock.Of<ILogger<LoanFileController>>(), byteProService.Object,bwcsdkService.Object);

            var loanStatus = controller.GetLoanStatus("");
            Assert.NotNull(loanStatus.Result);
            Assert.IsType<BadRequestObjectResult>(loanStatus.Result);

        }
        [Fact]
        public async void GetLoanStatusControllerBadRequestParse()
        {
            Mock<IByteProService> byteProService = new Mock<IByteProService>();
            Mock<IByteWebConnectorSdkService> bwcsdkService = new Mock<IByteWebConnectorSdkService>();
            var controller = new LoanFileController(Mock.Of<ILogger<LoanFileController>>(), byteProService.Object, bwcsdkService.Object);

            var loanStatus = controller.GetLoanStatus("123s33");
            Assert.NotNull(loanStatus.Result);
            Assert.IsType<BadRequestObjectResult>(loanStatus.Result);

        }
    }
}
