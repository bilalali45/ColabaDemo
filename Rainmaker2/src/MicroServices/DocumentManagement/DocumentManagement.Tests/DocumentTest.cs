using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.API.Controllers;
using DocumentManagement.Entity;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Xunit;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading;

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

        [Fact]
        public async Task TestGetDocumemntsByTemplateIdsService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Template>> mockCollection = new Mock<IMongoCollection<Entity.Template>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {
                new BsonDocument
                 {
                     { "docId" ,"5ebc18cba5d847268075ad4f" },
                     { "typeName" , BsonString.Empty},
                     { "docMessage" , "Credit report has been uploaded"},
                     { "messages" , BsonArray.Create(new Message[]{ })},
                     { "docName" , "Credit Report"}
                 }
                  ,
                 new BsonDocument
                 {
                     { "docId" , "5ebc18cba5d847268075ad4f"},
                     { "typeName" ,BsonString.Empty},
                     { "docMessage" , BsonString.Empty},
                     { "messages" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "tenantId", 1 },{ "message", "Credit report has been uploaded" } } })},
                     { "docName" , "Credit Report"},
                 }
                 ,
                 new BsonDocument
                 {
                     { "docId" , BsonString.Empty},
                     { "typeName" , BsonString.Empty},
                     { "docMessage" , "Credit report has been uploaded"},
                     { "messages" , BsonArray.Create(new Message[]{ })},
                     { "docName" , BsonString.Empty}
                 }
                 ,
                 new BsonDocument
                 {
                     { "docId" , BsonString.Empty},
                     { "typeName" ,BsonString.Empty},
                     { "docMessage" , "Credit report has been uploaded"},
                     { "messages" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "tenantId", 1 },{ "message", BsonString.Empty } } })},
                     { "docName" , BsonString.Empty}
                 }
                 ,
                 new BsonDocument
                 {
                     { "docId" , BsonString.Empty},
                     { "typeName" ,BsonString.Empty},
                     { "docMessage" ,BsonString.Empty},
                     { "messages" , BsonNull.Value},
                     { "docName" , BsonString.Empty}
                    
                 }
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Template, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Entity.Template>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new DocumentService(mock.Object);

            TemplateIdModel templateIdModel = new TemplateIdModel();
            templateIdModel.tenantId = 1;
            List<string> listIds = new List<string>();
            listIds.Add("5eb25acde519051af2eeb111");
            listIds.Add("5eb25acde519051af2eeb211");

            templateIdModel.id = listIds;
            //Act
            List<DocumentModel> dto = await service.GetDocumemntsByTemplateIds(templateIdModel);

            //Assert
            Assert.NotNull(dto);
            Assert.Equal(2, dto.Count);
            Assert.Equal("5ebc18cba5d847268075ad4f", dto[0].docId);
            Assert.Equal("Credit report has been uploaded", dto[1].docMessage);
        }
    }
}
