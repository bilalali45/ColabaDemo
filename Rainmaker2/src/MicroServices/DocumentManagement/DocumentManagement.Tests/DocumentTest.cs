using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.API.Controllers;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Xunit;

namespace DocumentManagement.Tests
{
    public class DocumentTest
    {
        [Fact]
        public async Task TestGetDocumemntsByTemplateIdsController()
        {
            //Arrange
            Mock<IDocumentService> mock = new Mock<IDocumentService>();
            List<DocumentModel> list = new List<DocumentModel>() { { new DocumentModel() { docId = "5ebc18cba5d847268075ad4f" } } };

            mock.Setup(x => x.GetDocumemntsByTemplateIds(It.IsAny<TemplateIdModel>())).ReturnsAsync(list);

            var documentController = new DocumentController(mock.Object);

            //Act
            IActionResult result = await documentController.GetDocumemntsByTemplateIds(It.IsAny<TemplateIdModel>());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<DocumentModel>;
            Assert.Single(content);
            Assert.Equal("5ebc18cba5d847268075ad4f", content[0].docId);
        }
    }
}
