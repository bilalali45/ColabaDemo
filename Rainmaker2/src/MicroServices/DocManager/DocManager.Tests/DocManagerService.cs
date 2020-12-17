using DocManager.API.Controllers;
using DocManager.Model;
using DocManager.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using URF.Core.EF;
using URF.Core.EF.Factories;
using URF.Core.EF.Trackable;
using Xunit;

namespace DocManager.Tests
{

    public partial class UnitTest
    {
        [Fact]
        public async Task TestServiceGetTrashFiles()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {
              new BsonDocument
                    {
                        //Cover all empty fields except typeMessage
                        { "_id" , BsonString.Empty },
                        { "files" , BsonNull.Value}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } })}
                    }
                 };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new TrashService(mock.Object);
            //Act
            List<WorkbenchFile> dto = await service.GetTrashFiles(1, 1);
            //Assert
            Assert.NotNull(dto);
            Assert.Equal("asd", dto[0].mcuName);
        }
        [Fact]
        public async Task TestServiceMoveFromTrashToWorkBench()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            MoveFromTrashToWorkBench moveFromTrashToWorkBench = new MoveFromTrashToWorkBench();
            moveFromTrashToWorkBench.id = "5fb51519e223e0428d82c41b";
            moveFromTrashToWorkBench.fromFileId = "5fbbb0ca5a5666bd808a2a69";
            List<BsonDocument> list = new List<BsonDocument>()
            {
              new BsonDocument
                    {
                        //Cover all empty fields except typeMessage
                        { "_id" , BsonString.Empty },
                        { "files" , BsonNull.Value}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } })}
                    }
                 };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            var service = new TrashService(mock.Object);
            //Act
            var result = await service.MoveFromTrashToWorkBench(moveFromTrashToWorkBench, 1);
            //Assert
            Assert.IsType<bool>(result);

        }
        [Fact]
        public async Task TestServiceMoveFromTrashToWorkBenchIsNotNull()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            MoveFromTrashToWorkBench moveFromTrashToWorkBench = new MoveFromTrashToWorkBench();
            moveFromTrashToWorkBench.id = "5fb51519e223e0428d82c41b";
            moveFromTrashToWorkBench.fromFileId = "5fbbb0ca5a5666bd808a2a69";
            List<BsonDocument> list = new List<BsonDocument>()
            {
              new BsonDocument
                    {
                        //Cover all empty fields except typeMessage
                        { "_id" , BsonString.Empty },
                        { "files" , BsonNull.Value}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } })}
                    }
                 };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

        
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            var service = new TrashService(mock.Object);
            //Act
            var result = await service.MoveFromTrashToWorkBench(moveFromTrashToWorkBench, 1);
            //Assert
            Assert.IsType<bool>(result);

        }

        [Fact]
        public async Task TestServiceGetWorkbenchFiles()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {
              new BsonDocument
                    {
                        //Cover all empty fields except typeMessage
                        { "_id" , BsonString.Empty },
                        { "files" , BsonNull.Value}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } })}
                    }
                 };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new WorkbenchService(mock.Object);
            //Act
            List<WorkbenchFile> dto = await service.GetWorkbenchFiles(1, 1);
            //Assert
            Assert.NotNull(dto);
            Assert.Equal("asd", dto[0].mcuName);
        }


        [Fact]
        public async Task TestServiceMoveFromWorkBenchToTrash()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            MoveFromWorkBenchToTrash moveFromWorkBenchToTrash = new MoveFromWorkBenchToTrash();
            moveFromWorkBenchToTrash.fromFileId = "5fbbb0e35a5666bd808a2a6b";
            moveFromWorkBenchToTrash.id = "5fb51519e223e0428d82c41b";
            List<BsonDocument> list = new List<BsonDocument>()
            {
              new BsonDocument
                    {
                        //Cover all empty fields except typeMessage
                        { "_id" , BsonString.Empty },
                        { "files" , BsonNull.Value}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } })}
                    }
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            var service = new WorkbenchService(mock.Object);

            //Act
            var result = await service.MoveFromWorkBenchToTrash(moveFromWorkBenchToTrash, 1);
            //Assert
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task TestServiceMoveFromWorkBenchToTrashIsNotNull()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            MoveFromWorkBenchToTrash moveFromWorkBenchToTrash = new MoveFromWorkBenchToTrash();
            moveFromWorkBenchToTrash.fromFileId = "5fbbb0e35a5666bd808a2a6b";
            moveFromWorkBenchToTrash.id = "5fb51519e223e0428d82c41b";
            List<BsonDocument> list = new List<BsonDocument>()
            {
              new BsonDocument
                    {
                        //Cover all empty fields except typeMessage
                        { "_id" , BsonString.Empty },
                        { "files" , BsonNull.Value}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } })}
                    }
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);
      
            var service = new WorkbenchService(mock.Object);

            //Act
            var result = await service.MoveFromWorkBenchToTrash(moveFromWorkBenchToTrash, 1);
            //Assert
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task TestServiceMoveFromWorkBenchToCategory()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            MoveFromWorkBenchToCategory moveFromWorkBenchToCategory = new MoveFromWorkBenchToCategory();
            moveFromWorkBenchToCategory.fromFileId = "5fbbb0e35a5666bd808a2a6b";
            moveFromWorkBenchToCategory.id = "5fb51519e223e0428d82c41b";
            moveFromWorkBenchToCategory.toRequestId = "5fb6650080996b6a2c6a1e4e";
            moveFromWorkBenchToCategory.toDocId = "5fb6650080996b6a2c6a1e4f";
            List<BsonDocument> list = new List<BsonDocument>()
            {
              new BsonDocument
                    {
                        //Cover all empty fields except typeMessage
                        { "_id" , BsonString.Empty },
                        { "files" , BsonNull.Value}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } })}
                    }
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            var service = new WorkbenchService(mock.Object);

            //Act
            var result = await service.MoveFromWorkBenchToCategory(moveFromWorkBenchToCategory, 1);
            //Assert
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task TestServiceMoveFromWorkBenchToCategoryIsNotNull()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            MoveFromWorkBenchToCategory moveFromWorkBenchToCategory = new MoveFromWorkBenchToCategory();
            moveFromWorkBenchToCategory.fromFileId = "5fbbb0e35a5666bd808a2a6b";
            moveFromWorkBenchToCategory.id = "5fb51519e223e0428d82c41b";
            moveFromWorkBenchToCategory.toRequestId = "5fb6650080996b6a2c6a1e4e";
            moveFromWorkBenchToCategory.toDocId = "5fb6650080996b6a2c6a1e4f";
            List<BsonDocument> list = new List<BsonDocument>()
            {
              new BsonDocument
                    {
                        //Cover all empty fields except typeMessage
                        { "_id" , BsonString.Empty },
                        { "files" , BsonNull.Value}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } })}
                    }
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mock.SetupGet(x => x.db).Returns(mockdb.Object); 
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            var service = new WorkbenchService(mock.Object);

            //Act
            var result = await service.MoveFromWorkBenchToCategory(moveFromWorkBenchToCategory, 1);
            //Assert
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task TestServiceMoveFromCategoryToTrash()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            MoveFromCategoryToTrash moveFromCategoryToTrash = new MoveFromCategoryToTrash();
            moveFromCategoryToTrash.id = "5fb51519e223e0428d82c41b";
            moveFromCategoryToTrash.fromRequestId = "5fb5152ee223e0428d82c41c";
            moveFromCategoryToTrash.fromDocId = "5fb51533e223e0428d82c41d";
            moveFromCategoryToTrash.fromFileId = "5fbc8ac67501aedc18992591";


            List<BsonDocument> list = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "files" ,  new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } }
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new DocumentService(mock.Object);
            //Act
            var result = await service.MoveFromCategoryToTrash(moveFromCategoryToTrash, 1);
            //Assert
            Assert.IsType<bool>(result);

        }
        [Fact]
        public async Task TestServiceMoveFromCategoryToWorkBench()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            MoveFromCategoryToWorkBench moveFromCategoryToWorkBench = new MoveFromCategoryToWorkBench();
            moveFromCategoryToWorkBench.id = "5fb51519e223e0428d82c41b";
            moveFromCategoryToWorkBench.fromRequestId = "5fb5152ee223e0428d82c41c";
            moveFromCategoryToWorkBench.fromDocId = "5fb51533e223e0428d82c41d";
            moveFromCategoryToWorkBench.fromFileId = "5fbc8ac67501aedc18992591";


            List<BsonDocument> list = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "files" ,  new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } }
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new DocumentService(mock.Object);
            //Act
            var result = await service.MoveFromCategoryToWorkBench(moveFromCategoryToWorkBench, 1);
            //Assert
            Assert.IsType<bool>(result);

        }


        [Fact]
        public async Task TestServiceViewCategoryAnnotations()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            ViewCategoryAnnotations viewCategoryAnnotations = new ViewCategoryAnnotations();
            viewCategoryAnnotations.id = "5fb51519e223e0428d82c41b";
            viewCategoryAnnotations.fromRequestId = "5fb5152ee223e0428d82c41c";
            viewCategoryAnnotations.fromDocId = "5fb51533e223e0428d82c41d";
            viewCategoryAnnotations.fromFileId = "5fbc8ac67501aedc18992591";


            List<BsonDocument> list = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "files" ,  new BsonDocument() { { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } }
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new DocumentService(mock.Object);
            //Act
            var result = await service.ViewCategoryAnnotations(viewCategoryAnnotations, 1);
            //Assert
            Assert.NotNull(result);
            Assert.Equal("asd", result);
        }



        [Fact]
        public async Task TestServiceSaveCategoryAnnotations()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            SaveCategoryAnnotations saveCategoryAnnotations = new SaveCategoryAnnotations();
            saveCategoryAnnotations.id = "5fb51519e223e0428d82c41b";
            saveCategoryAnnotations.requestId = "5fb5152ee223e0428d82c41c";
            saveCategoryAnnotations.docId = "5fb51533e223e0428d82c41d";
            saveCategoryAnnotations.fileId = "5fbc8ac67501aedc18992591";
            saveCategoryAnnotations.annotations = "abc";

            List<BsonDocument> list = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "files" ,  new BsonDocument() { { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } }
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new DocumentService(mock.Object);
            //Act
            var result = await service.SaveCategoryAnnotations(saveCategoryAnnotations, 1);
            //Assert
            Assert.IsType<bool>(result);

        }


        [Fact]
        public async Task TestServiceSaveTrashAnnotations()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            SaveTrashAnnotations saveTrashAnnotations = new SaveTrashAnnotations();
            saveTrashAnnotations.id = "5fb51519e223e0428d82c41b";

            saveTrashAnnotations.fileId = "5fbc8ac67501aedc18992591";
            saveTrashAnnotations.annotations = "abc";

            List<BsonDocument> list = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "files" ,  new BsonDocument() { { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } }
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new TrashService(mock.Object);
            //Act
            var result = await service.SaveTrashAnnotations(saveTrashAnnotations, 1);
            //Assert
            Assert.IsType<bool>(result);

        }


        [Fact]
        public async Task TestServiceViewWorkbenchAnnotations()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            ViewWorkbenchAnnotations viewWorkbenchAnnotations = new ViewWorkbenchAnnotations();
            viewWorkbenchAnnotations.id = "5fb51519e223e0428d82c41b";
            viewWorkbenchAnnotations.fromRequestId = "5fb5152ee223e0428d82c41c";
            viewWorkbenchAnnotations.fromDocId = "5fb51533e223e0428d82c41d";
            viewWorkbenchAnnotations.fromFileId = "5fbc8ac67501aedc18992591";


            List<BsonDocument> list = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "files" ,  new BsonDocument() { { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } }
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new WorkbenchService(mock.Object);
            //Act
            var result = await service.ViewWorkbenchAnnotations(viewWorkbenchAnnotations, 1);
            //Assert
            Assert.NotNull(result);
            Assert.Equal("asd", result);
        }


        [Fact]
        public async Task TestServiceSaveWorkbenchAnnotations()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            SaveWorkbenchAnnotations saveWorkbenchAnnotations = new SaveWorkbenchAnnotations();
            saveWorkbenchAnnotations.id = "5fb51519e223e0428d82c41b";

            saveWorkbenchAnnotations.fileId = "5fbc8ac67501aedc18992591";
            saveWorkbenchAnnotations.annotations = "abc";

            List<BsonDocument> list = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "files" ,  new BsonDocument() { { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } }
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new WorkbenchService(mock.Object);
            //Act
            var result = await service.SaveWorkbenchAnnotations(saveWorkbenchAnnotations, 1);
            //Assert
            Assert.IsType<bool>(result);

        }



        [Fact]
        public async Task TestServiceSaveWorkbenchDocument()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.oldFile = "5fb51519e223e0428d82c41b";

            saveWorkbenchDocument.fileId = "5fbc8ac67501aedc18992591";

            List<BsonDocument> list = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "files" ,  new BsonDocument() { { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } }
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new ThumbnailService(mock.Object);
            //Act
            var result = await service.SaveWorkbenchDocument("5fb51519e223e0428d82c41b", "5fc0a3795e7b907ff896c1f1",
                1, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey");
            //Assert
            Assert.NotNull(result);
            //    Assert.Equal("5fc4ff933323758e51aa640a", result.fileId);

        }
        [Fact]
        public async Task TestServiceSaveWorkbenchDocumentOldFileIsNotEmpty()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.oldFile = "5fb51519e223e0428d82c41b";

            saveWorkbenchDocument.fileId = "5fbc8ac67501aedc18992591";

            List<BsonDocument> list = new List<BsonDocument>()
            {


                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , "5fb51519e223e0428d82c41b" },
                        { "tenantId" , 1 },
                        { "files" ,  new BsonDocument() { { "serverName", "asd" }, { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } },
                        { "$workbench" ,   new BsonDocument() {
                             { "_id", "5fc0a3795e7b907ff896c1f1" },
                            { "annotations", "asd" },
                            { "fileUploadedOn", BsonDateTime.Create(DateTime.Now) },
                            { "size", 1 },
                            { "order",1 },
                            { "serverName","ac" }
                        }}
                    }

                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new ThumbnailService(mock.Object);
            //Act
            var result = await service.SaveWorkbenchDocument("5fb51519e223e0428d82c41b", "5fc0a3795e7b907ff896c1f1",
                1, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey");
            //Assert
            Assert.NotNull(result);
            //    Assert.Equal("5fc4ff933323758e51aa640a", result.fileId);

        }
        [Fact]
        public async Task TestServiceSaveTrashDocument()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.oldFile = "5fb51519e223e0428d82c41b";

            saveWorkbenchDocument.fileId = "5fbc8ac67501aedc18992591";

            List<BsonDocument> list = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "files" ,  new BsonDocument() { { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } }
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new ThumbnailService(mock.Object);
            //Act
            var result = await service.SaveTrashDocument("5fb51519e223e0428d82c41b", "5fc0a3795e7b907ff896c1f1",
                1, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey");
            //Assert
            Assert.NotNull(result);
            // Assert.Equal("5fc4ff933323758e51aa640c", result.fileId);

        }
        [Fact]
        public async Task TestServiceSaveTrashDocumentOldFileIsNotEmpty()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.oldFile = "5fb51519e223e0428d82c41b";

            saveWorkbenchDocument.fileId = "5fbc8ac67501aedc18992591";

            List<BsonDocument> list = new List<BsonDocument>()
            {

                 
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , "5fb51519e223e0428d82c41b" },
                        { "tenantId" , 1 },
                        { "files" ,  new BsonDocument() { { "serverName", "asd" }, { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } },
                        { "$trash" ,   new BsonDocument() {
                             { "_id", "5fc0a3795e7b907ff896c1f1" },
                            { "annotations", "asd" },
                            { "fileUploadedOn", BsonDateTime.Create(DateTime.Now) },
                            { "size", 1 },
                            { "order",1 },
                            { "serverName","ac" }
                        }}
                    }
                 
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new ThumbnailService(mock.Object);
            //Act
            var result = await service.SaveTrashDocument("5fb51519e223e0428d82c41b", "5fc0a3795e7b907ff896c1f1",
                1, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey");
            //Assert
            Assert.NotNull(result);
            // Assert.Equal("5fc4ff933323758e51aa640c", result.fileId);

        }

        [Fact]
        public async Task TestServiceSaveCategoryDocumentIsFileExist()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.oldFile = "5fb51519e223e0428d82c41b";

            saveWorkbenchDocument.fileId = "5fbc8ac67501aedc18992591";

            List<BsonDocument> list = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "files" ,  new BsonDocument() { { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } }
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    } 
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);


            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new ThumbnailService(mock.Object);
            //Act
            var result = await service.SaveCategoryDocument("5fb51519e223e0428d82c41b", "5fb5152ee223e0428d82c41c", "5fb51533e223e0428d82c41d", "5fbc8ac67501aedc18992591", 1, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey");
            //Assert
            Assert.NotNull(result);
            //Assert.Equal("5fb51533e223e0428d82c41d", result.oldFile);

        }


        [Fact]
        public async Task TestServiceSaveCategoryDocumentIsFileNotExist()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.oldFile = "5fb51519e223e0428d82c41b";

            saveWorkbenchDocument.fileId = "5fbc8ac67501aedc18992591";

            List<BsonDocument> list = new List<BsonDocument>()
            {
                new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" ,"5fb51519e223e0428d82c41b" },
                        { "requestId" ,"5fb5152ee623e0428d82c41c"  },
                           { "tenantId" ,1  },
                        { "docId","5fb51533e223e0428d82c41d" } ,
                        { "files" ,   new BsonDocument() {
                            {"id","5fbc8ac67501aedc18992591" },
                            { "mcuName", "asd" },
                            { "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, 
                            { "size", 1 },{ "order",1 } }}
                    }
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false).ReturnsAsync(true);
            mockCursor.SetupGet(x => x.Current).Returns(list);


            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new ThumbnailService(mock.Object);
            //Act
            var result = await service.SaveCategoryDocument("5fb51519e223e0428d82c41b", "5fb5152ee223e0428d82c41c", "5fb51533e223e0428d82c41d", "5fbc8ac67501aedc18992591", 2, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey");
            //Assert
            Assert.NotNull(result);
            //Assert.Equal("5fb51533e223e0428d82c41d", result.oldFile);

        }

        [Fact]
        public async Task TestServiceSaveCategoryDocumentIsFileNotExistAndoldFileIsNotEmpty()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.oldFile = "5fb51519e223e0428d82c41b";

            saveWorkbenchDocument.fileId = "5fbc8ac67501aedc18992591";

            List<BsonDocument> list = new List<BsonDocument>()
            {
                new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" ,"5fb51519e223e0428d82c41b" },
                        { "requestId" ,"5fb5152ee623e0428d82c41c"  },
                           { "tenantId" ,1  },
                        { "docId","5fb51533e223e0428d82c41d" } ,
                        { "files" ,   new BsonDocument() {
                            {"id","5fbc8ac67501aedc18992591" },
                            { "mcuName", "asd" },
                            { "fileUploadedOn", BsonDateTime.Create(DateTime.Now) },
                            { "size", 1 },{ "order",1 },
                             { "serverName", "ac" } 
                        }}
                    }
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false).ReturnsAsync(true);
            mockCursor.SetupGet(x => x.Current).Returns(list);


            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new ThumbnailService(mock.Object);
            //Act
            var result = await service.SaveCategoryDocument("5fb51519e223e0428d82c41b", "5fb5152ee223e0428d82c41c", "5fb51533e223e0428d82c41d", "5fbc8ac67501aedc18992591", 2, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey");
            //Assert
            Assert.NotNull(result);
            //Assert.Equal("5fb51533e223e0428d82c41d", result.oldFile);

        }
        [Fact]
        public async Task TestServiceAcquireLockIsNull()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();
            Mock<ILockService> mockLockService = new Mock<ILockService>();
            Mock<IMongoCollection<Lock>> mockCollection = new Mock<IMongoCollection<Lock>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            
            BsonDocument bsonElements =

                new BsonDocument
                   {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "lockDateTime" , DateTime.UtcNow },
                        { "lockUserId" , 1 },
                        { "lockUserName" , "ABC"},
                        { "loanApplicationId" ,1 }
                   };
            LockSetting lockSetting = new LockSetting();
            lockSetting.lockTimeInMinutes = 5;
            mockSettingService.Setup(x => x.GetLockSetting()).ReturnsAsync(lockSetting);

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Lock, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Lock>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Lock>>(), It.IsAny<UpdateDefinition<Lock>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new LockService(mock.Object, mockSettingService.Object);
            //Act
            LockModel lockModel = new LockModel();

            lockModel.loanApplicationId = 1002;
            var result = await service.AcquireLock(lockModel, It.IsAny<int>(), It.IsAny<string>());
            //Assert
            Assert.NotNull(result);
            //Assert.Equal("5fb51533e223e0428d82c41d", result.oldFile);

        }


        [Fact]
        public async Task TestServiceAcquireLockIsNotNull()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();
            Mock<IMongoCollection<Lock>> mockCollection = new Mock<IMongoCollection<Lock>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();


            List<BsonDocument> list = new List<BsonDocument>()
            {

             new BsonDocument
            {
              
                        //Cover all empty fields except typeMessage
                        { "_id" , "5fcf7d048c317adf72924eea" },
                        { "lockDateTime" , DateTime.UtcNow},
                        { "lockUserId" ,0},
                        { "lockUserName" ,"System Administrator"},
                        { "loanApplicationId" , 1002}

                 },
              new BsonDocument
            {
              
                        //Cover all empty fields except typeMessage
                        { "_id" , "5fcf7d048c317adf72924eea" },
                        { "lockDateTime" , DateTime.UtcNow},
                        { "lockUserId" ,0},
                        { "lockUserName" ,"System Administrator"},
                        { "loanApplicationId" , 1002}

                 }
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Lock, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Lock>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Lock>>(), It.IsAny<UpdateDefinition<Lock>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new LockService(mock.Object, mockSettingService.Object);
            //Act
            LockModel lockModel = new LockModel();

            lockModel.loanApplicationId = 1002;
            var result = await service.AcquireLock(lockModel, It.IsAny<int>(), It.IsAny<string>());
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestServiceAcquireLockIsTimePassed()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();
            Mock<IMongoCollection<Lock>> mockCollection = new Mock<IMongoCollection<Lock>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

   
            List<BsonDocument> list = new List<BsonDocument>()
            {

           
              new BsonDocument
            {
              
                        //Cover all empty fields except typeMessage
                        { "_id" , "5fcf7d048c317adf72924eea" },
                        { "lockDateTime" , DateTime.UtcNow.AddMinutes(- 10)},
                        { "lockUserId" ,1},
                        { "lockUserName" ,"System Administrator"},
                        { "loanApplicationId" , 1002}

                 }
            };
            LockSetting lockSetting = new LockSetting();
            lockSetting.lockTimeInMinutes = 5;
            mockSettingService.Setup(x => x.GetLockSetting()).ReturnsAsync(lockSetting);
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Lock, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Lock>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Lock>>(), It.IsAny<UpdateDefinition<Lock>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new LockService(mock.Object, mockSettingService.Object);
            //Act
            LockModel lockModel = new LockModel();

            lockModel.loanApplicationId = 1002;
            var result = await service.AcquireLock(lockModel, It.IsAny<int>(), It.IsAny<string>());
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestServiceAcquireLockIsNotTimePassed()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();
            Mock<IMongoCollection<Lock>> mockCollection = new Mock<IMongoCollection<Lock>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();


            List<BsonDocument> list = new List<BsonDocument>()
            {


              new BsonDocument
            {
              
                        //Cover all empty fields except typeMessage
                        { "_id" , "5fcf7d048c317adf72924eea" },
                        { "lockDateTime" , DateTime.UtcNow},
                        { "lockUserId" ,1},
                        { "lockUserName" ,"System Administrator"},
                        { "loanApplicationId" , 1002}

                 }
            };
            LockSetting lockSetting = new LockSetting();
            lockSetting.lockTimeInMinutes = 5;
            mockSettingService.Setup(x => x.GetLockSetting()).ReturnsAsync(lockSetting);
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Lock, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Lock>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Lock>>(), It.IsAny<UpdateDefinition<Lock>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new LockService(mock.Object, mockSettingService.Object);
            //Act
            LockModel lockModel = new LockModel();

            lockModel.loanApplicationId = 1002;
            var result = await service.AcquireLock(lockModel, It.IsAny<int>(), It.IsAny<string>());
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestServiceRetainLockIsNull()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();
            Mock<ILockService> mockLockService = new Mock<ILockService>();
            Mock<IMongoCollection<Lock>> mockCollection = new Mock<IMongoCollection<Lock>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            BsonDocument bsonElements =

                new BsonDocument
                   {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "lockDateTime" , DateTime.UtcNow },
                        { "lockUserId" , 1 },
                        { "lockUserName" , "ABC"},
                        { "loanApplicationId" ,1 }
                   };
            LockSetting lockSetting = new LockSetting();
            lockSetting.lockTimeInMinutes = 5;
            mockSettingService.Setup(x => x.GetLockSetting()).ReturnsAsync(lockSetting);

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Lock, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Lock>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Lock>>(), It.IsAny<UpdateDefinition<Lock>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new LockService(mock.Object, mockSettingService.Object);
            //Act
            LockModel lockModel = new LockModel();

            lockModel.loanApplicationId = 1002;
            var result = await service.RetainLock(lockModel, It.IsAny<int>(), It.IsAny<string>());
            //Assert
            Assert.Null(result);
            //Assert.Equal("5fb51533e223e0428d82c41d", result.oldFile);

        }
        [Fact]
        public async Task TestServiceRetainLockIsNotNull()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();
            Mock<ILockService> mockLockService = new Mock<ILockService>();
            Mock<IMongoCollection<Lock>> mockCollection = new Mock<IMongoCollection<Lock>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {


              new BsonDocument
            {
              
                        //Cover all empty fields except typeMessage
                        { "_id" , "5fcf7d048c317adf72924eea" },
                        { "lockDateTime" , DateTime.UtcNow},
                        { "lockUserId" ,0},
                        { "lockUserName" ,"System Administrator"},
                        { "loanApplicationId" , 1002}

                 }
            };
            LockSetting lockSetting = new LockSetting();
            lockSetting.lockTimeInMinutes = 5;
            mockSettingService.Setup(x => x.GetLockSetting()).ReturnsAsync(lockSetting);
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Lock, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Lock>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Lock>>(), It.IsAny<UpdateDefinition<Lock>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new LockService(mock.Object, mockSettingService.Object);
            //Act
            LockModel lockModel = new LockModel();

            lockModel.loanApplicationId = 1002;
            var result = await service.RetainLock(lockModel, It.IsAny<int>(), It.IsAny<string>());
            //Assert
            Assert.NotNull(result);
           

        }
    }
}
