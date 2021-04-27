
using DocManager.API.Controllers;
using DocManager.Model;
using DocManager.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
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
        public async Task TestServiceGetTrashEmptyFiles()
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
                        { "_id" , "1"},
                       { "files" ,  new BsonArray(){ new BsonDocument() {
                            {"id","5fbc8ac67501aedc18992591" },
                            { "mcuName", "asd" },
                            { "fileUploadedOn", BsonDateTime.Create(DateTime.Now) },
                            { "size", 1 },{ "order",1 },
                             { "serverName", "ac" }
                        }} }
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
        public async Task TestServiceMoveFromTrashToWorkBenchIsNull()
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

            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false);
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
        public async Task TestServiceMoveFromCategoryToTrashNoFile()
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
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false).ReturnsAsync(true);
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
        public async Task TestServiceMoveFromCategoryToTrashNoFileMcuNameEmpty()
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
                                              { "files" ,  new BsonDocument() { { "mcuName", string.Empty },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } }
                                          }
                                          ,
                                          new BsonDocument
                                          {
                                              //Cover all empty fields except files
                                              { "_id" , BsonString.Empty },
                                              { "mcuFiles" ,   new BsonDocument() { { "mcuName", string.Empty },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                                          }
                                          ,
                                          new BsonDocument
                                          {
                                              //Cover all empty fields except files
                                              { "_id" , BsonString.Empty },
                                              { "mcuFiles" ,   new BsonDocument() { { "mcuName", string.Empty },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                                          }
                                      };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false).ReturnsAsync(true);
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
        public async Task TestServiceMoveFromCategoryToWorkBenchNoFile()
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
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false).ReturnsAsync(true);
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
        public async Task TestServiceViewCategoryAnnotationsNull()
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
                        { "_id" , "5fbc8ac67501aedc18992591"},
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
            Assert.Null(result);
        }

        [Fact]
        public async Task TestServiceViewCategoryAnnotationsIsNotNull()
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
                          { "_id" , "5fbc8ac67501aedc18992591"},
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
                         { "_id" , "5fbc8ac67501aedc18992591"},
                        { "mcuFiles" ,   new BsonDocument() { { "mcuName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(true).ReturnsAsync(true);
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
        public async Task TestServiceViewCategoryAnnotationsMcFilesIsNotNull()
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
                         { "_id" , "5fbc8ac67501aedc18992591"},
                        { "files" ,  new BsonDocument() { { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } }
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "mcuFiles" ,   new BsonDocument() { { "annotations", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } }}
                    }

                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false).ReturnsAsync(true).ReturnsAsync(true);
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
        public async Task TestServiceSaveCategoryAnnotationsMCU()
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
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false);
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
        public async Task TestServiceViewWorkbenchAnnotationsNull()
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
            Assert.Null(result);

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
                1, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey","");
            //Assert
            Assert.NotNull(result);
          

        }

        [Fact]
        public async Task TestServiceSaveWorkbenchDocumentWorkBenchFileNull()
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
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new ThumbnailService(mock.Object);
            //Act
            var result = await service.SaveWorkbenchDocument("5fb51519e223e0428d82c41b", "5fc0a3795e7b907ff896c1f1",
                1, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey","");
            //Assert
            Assert.NotNull(result);
            

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
                1, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey","");
            //Assert
            Assert.NotNull(result);
            

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
                1, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey", "");
            //Assert
            Assert.NotNull(result);
            

        }
        [Fact]
        public async Task TestServiceSaveTrashDocumentIsExistTrashFileNull()
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
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new ThumbnailService(mock.Object);
            //Act
            var result = await service.SaveTrashDocument("5fb51519e223e0428d82c41b", "5fc0a3795e7b907ff896c1f1",
                1, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey", "");
            //Assert
            Assert.NotNull(result);
           

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
                1, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey", "");
            //Assert
            Assert.NotNull(result);
           

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
            var result = await service.SaveCategoryDocument("5fb51519e223e0428d82c41b", "5fb5152ee223e0428d82c41c", "5fb51533e223e0428d82c41d", "5fbc8ac67501aedc18992591", 1, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey", "");
            //Assert
            Assert.NotNull(result);
            

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
                        { "requestId" ,"5fb5152ee223e0428d82c41c"  },
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
            var result = await service.SaveCategoryDocument("5fb51519e223e0428d82c41b", "5fb5152ee223e0428d82c41c", "5fb51533e223e0428d82c41d", "5fbc8ac67501aedc18992591", 2, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey", "");
            //Assert
            Assert.NotNull(result);
           

        }
        [Fact]
        public async Task TestServiceSaveCategoryDocumentIsFileNull()
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
                        { "_id" ,"5fb51519e223e0428d82c40b" },
                        { "requestId" ,"5fb5152ee623e0428d82c40c"  },
                           { "tenantId" ,1  },
                        { "docId","5fb51533e223e0428d82c40d" } ,
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
            var result = await service.SaveCategoryDocument("5fb51519e223e0428d82c41b", "5fb5152ee223e0428d82c41c", "5fb51533e223e0428d82c41d", "5fbc8ac67501aedc18992591", 2, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey", "");
            //Assert
            Assert.NotNull(result);
           

        }

        [Fact]
        public async Task TestServiceSaveCategoryDocumentIsFileNullMcuFileEmpty()
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
                        { "_id" ,"5fb51519e223e0428d82c40b" },
                        { "requestId" ,"5fb5152ee623e0428d82c40c"  },
                           { "tenantId" ,1  },
                        { "docId","5fb51533e223e0428d82c40d" } ,
                        { "files" ,   new BsonDocument() {
                            {"id","5fbc8ac67501aedc18992591" },
                            { "mcuName", "asd" },
                            { "fileUploadedOn", BsonDateTime.Create(DateTime.Now) },
                            { "size", 1 },{ "order",1 } }}
                    }
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);


            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new ThumbnailService(mock.Object);
            //Act
            var result = await service.SaveCategoryDocument("5fb51519e223e0428d82c41b", "5fb5152ee223e0428d82c41c", "5fb51533e223e0428d82c41d", "5fbc8ac67501aedc18992591", 2, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey", "");
            //Assert
            Assert.NotNull(result);
           

        }

        [Fact]
        public async Task TestServiceSaveCategoryDocumentIsFileNullMcuFileEmptyUpdatemcuFileFalse()
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
                        { "_id" ,"5fb51519e223e0428d82c40b" },
                        { "requestId" ,"5fb5152ee623e0428d82c40c"  },
                           { "tenantId" ,1  },
                        { "docId","5fb51533e223e0428d82c40d" } ,
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
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 0, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new ThumbnailService(mock.Object);
            //Act
            var result = await service.SaveCategoryDocument("5fb51519e223e0428d82c41b", "5fb5152ee223e0428d82c41c", "5fb51533e223e0428d82c41d", "5fbc8ac67501aedc18992591", 2, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey", "");
            //Assert
            Assert.NotNull(result);
           

        }
        [Fact]
        public async Task TestServiceSaveCategoryDocumentIsFileNullMcuFileEmptyUpdatemcuFileFalseUpdatemcuFileFalse()
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
                        { "_id" ,"5fb51519e223e0428d82c40b" },
                        { "requestId" ,"5fb5152ee623e0428d82c40c"  },
                           { "tenantId" ,1  },
                        { "docId","5fb51533e223e0428d82c40d" } ,
                        { "files" ,   new BsonDocument() {
                            {"id","5fbc8ac67501aedc18992591" },
                            { "mcuName", "asd" },
                            { "fileUploadedOn", BsonDateTime.Create(DateTime.Now) },
                            { "size", 1 },{ "order",1 }
                            ,{"serverName","abc"}}}
                    }
                 };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false).ReturnsAsync(true);
            mockCursor.SetupGet(x => x.Current).Returns(list);


            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 0, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new ThumbnailService(mock.Object);
            //Act
            var result = await service.SaveCategoryDocument("5fb51519e223e0428d82c41b", "5fb5152ee223e0428d82c41c", "5fb51533e223e0428d82c41d", "5fbc8ac67501aedc18992591", 2, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey", "");
            //Assert
            Assert.Null(result.fileId);
          

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
            var result = await service.SaveCategoryDocument("5fb51519e223e0428d82c41b", "5fb5152ee223e0428d82c41c", "5fb51533e223e0428d82c41d", "5fbc8ac67501aedc18992591", 2, "abc", "mcuName", 2500, "application/pdf", 1, "salman", "AES", "FileKey", "");
            //Assert
            Assert.NotNull(result);
            

        }
        [Fact]
        public async Task TestServiceAcquireLockIsNull()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();
           
            Mock<IMongoCollection<Lock>> mockCollection = new Mock<IMongoCollection<Lock>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            










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
        public async Task TestServiceAcquireLockIsGreaterlockTimeInMinutes()
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
            lockSetting.lockTimeInMinutes = 0;
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
            var result = await service.AcquireLock(lockModel, It.IsAny<int>(), "rainsoft");
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
           
            Mock<IMongoCollection<Lock>> mockCollection = new Mock<IMongoCollection<Lock>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

           










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
           

        }
        [Fact]
        public async Task TestServiceRetainLockIsNotNull()
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

        [Fact]
        public async Task TestServiceMoveFromoneCategoryToAnotherCategoryIsMcuFilesExists()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            MoveFromOneCategoryToAnotherCategory moveFromOneCategoryToAnotherCategory = new MoveFromOneCategoryToAnotherCategory();
            moveFromOneCategoryToAnotherCategory.id = "5fb51519e223e0428d82c41b";
            moveFromOneCategoryToAnotherCategory.fromRequestId = "5fb5152ee223e0428d82c41c";
            moveFromOneCategoryToAnotherCategory.fromDocId = "5fb51533e223e0428d82c41d";
            moveFromOneCategoryToAnotherCategory.fromFileId = "5fbc8ac67501aedc18992591";
            moveFromOneCategoryToAnotherCategory.toRequestId = "5ff805bd37795ecc4f97c6d3";
            moveFromOneCategoryToAnotherCategory.toDocId = "5ff805bd37795ecc4f97c6d3";

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
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false).ReturnsAsync(true);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new DocumentService(mock.Object);
            //Act
            var result = await service.MoveFromoneCategoryToAnotherCategory(moveFromOneCategoryToAnotherCategory, 1);
            //Assert
            Assert.IsType<bool>(result);

        }

        [Fact]
        public async Task TestServiceMoveFromoneCategoryToAnotherCategoryIsFilesExists()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            MoveFromOneCategoryToAnotherCategory moveFromOneCategoryToAnotherCategory = new MoveFromOneCategoryToAnotherCategory();
            moveFromOneCategoryToAnotherCategory.id = "5fb51519e223e0428d82c41b";
            moveFromOneCategoryToAnotherCategory.fromRequestId = "5fb5152ee223e0428d82c41c";
            moveFromOneCategoryToAnotherCategory.fromDocId = "5fb51533e223e0428d82c41d";
            moveFromOneCategoryToAnotherCategory.fromFileId = "5fbc8ac67501aedc18992591";
            moveFromOneCategoryToAnotherCategory.toRequestId = "5ff805bd37795ecc4f97c6d3";
            moveFromOneCategoryToAnotherCategory.toDocId = "5ff805bd37795ecc4f97c6d3";

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
            var result = await service.MoveFromoneCategoryToAnotherCategory(moveFromOneCategoryToAnotherCategory, 1);
            //Assert
            Assert.IsType<bool>(result);

        }

        [Fact]
        public async Task TestServiceViewTrashAnnotations()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            ViewTrashAnnotations viewTrashAnnotations = new ViewTrashAnnotations();
            viewTrashAnnotations.id = "5fb51519e223e0428d82c41b";
            viewTrashAnnotations.fromFileId = "5fbc8ac67501aedc18992591";


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
            var result = await service.ViewTrashAnnotations(viewTrashAnnotations, 1);
            //Assert
            Assert.NotNull(result);
            Assert.Equal("asd", result);
        }
        [Fact]
        public async Task TestServiceViewTrashAnnotationsNull()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            ViewTrashAnnotations viewTrashAnnotations = new ViewTrashAnnotations();
            viewTrashAnnotations.id = "5fb51519e223e0428d82c41b";
            viewTrashAnnotations.fromFileId = "5fbc8ac67501aedc18992591";


            List<BsonDocument> list = new List<BsonDocument>()
            {
            };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new TrashService(mock.Object);
            //Act
            var result = await service.ViewTrashAnnotations(viewTrashAnnotations, 1);
            //Assert
            Assert.Null(result);

        }
        [Fact]
        public async Task TestServiceMoveFromTrashToCategory()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            MoveFromTrashToCategory moveFromTrashToCategory = new MoveFromTrashToCategory();
            moveFromTrashToCategory.fromFileId = "5fbbb0e35a5666bd808a2a6b";
            moveFromTrashToCategory.id = "5fb51519e223e0428d82c41b";
            moveFromTrashToCategory.toRequestId = "5fb6650080996b6a2c6a1e4e";
            moveFromTrashToCategory.toDocId = "5fb6650080996b6a2c6a1e4f";
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
            var result = await service.MoveFromTrashToCategory(moveFromTrashToCategory, 1);
            //Assert
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task TestServiceMoveFromTrashToCategoryIsNotNull()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            MoveFromTrashToCategory moveFromTrashToCategory = new MoveFromTrashToCategory();
            moveFromTrashToCategory.fromFileId = "5fbbb0e35a5666bd808a2a6b";
            moveFromTrashToCategory.id = "5fb51519e223e0428d82c41b";
            moveFromTrashToCategory.toRequestId = "5fb6650080996b6a2c6a1e4e";
            moveFromTrashToCategory.toDocId = "5fb6650080996b6a2c6a1e4f";
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
            var service = new TrashService(mock.Object);

            //Act
            var result = await service.MoveFromTrashToCategory(moveFromTrashToCategory, 1);
            //Assert
            Assert.IsType<bool>(result);
        }


        [Fact]
        public async Task TestServiceSaveRequest()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<StatusList>> mockCollection = new Mock<IMongoCollection<StatusList>>();
            Mock<IMongoCollection<Request>> mockRequestCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorRequest = new Mock<IAsyncCursor<BsonDocument>>();
            List<BsonDocument> list = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                         { "_id" ,"5fbc8ac67501aedc18992591"}
                          }
                 };
            mockdb.Setup(x => x.GetCollection<StatusList>("StatusList", It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<StatusList, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);


            List<BsonDocument> requestlist = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" ,"5fbc8ac67501aedc18992591"}
                          }
                 };
            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockRequestCollection.Object);
            mockRequestCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorRequest.Object);
            mockCursorRequest.Setup(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);
            mockCursorRequest.SetupGet(x => x.Current).Returns(requestlist);



            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            var service = new RequestService(mock.Object, Mock.Of<IRainmakerService>());
            SaveModel saveModel = new SaveModel()
            {
                id = "5fbc8ac67501aedc18992591",
                request = new Request()
                {
                    userId = 1,
                    userName = "rainsoft"
                    ,
                    document = new RequestDocument
                    {
                        typeId = string.Empty,
                        displayName = "xyz",
                        message = "abc",
                        status = RequestStatus.Active,
                        files = new List<RequestFile>()
                    }
                }

            };
            //Act
            var result = await service.Save(saveModel, new List<string>());
            //Assert
            Assert.True(result);


        }


        [Fact]
        public async Task TestServiceSaveRequestNotEqualLoanApplication()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<StatusList>> mockCollection = new Mock<IMongoCollection<StatusList>>();
            Mock<IMongoCollection<Request>> mockRequestCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorRequest = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IMongoCollection<LoanApplication>> mockLoanApplicationCollection = new Mock<IMongoCollection<LoanApplication>>();


            List<BsonDocument> list = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                         { "_id" ,"5fbc8ac67501aedc18992591"}
                          }
                 };
            mockdb.Setup(x => x.GetCollection<StatusList>("StatusList", It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<StatusList, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);


            List<BsonDocument> requestlist = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" ,"5fbc8ac67501aedc18992591"}
                          }
                 };
            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockRequestCollection.Object);
            mockRequestCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorRequest.Object);
            mockCursorRequest.Setup(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);
            mockCursorRequest.SetupGet(x => x.Current).Returns(requestlist);
            mockdb.Setup(x => x.GetCollection<LoanApplication>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockLoanApplicationCollection.Object);

            mockLoanApplicationCollection.Setup(s => s.InsertOneAsync(It.IsAny<LoanApplication>(), It.IsAny<InsertOneOptions>(), It.IsAny<System.Threading.CancellationToken>()));


            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            var service = new RequestService(mock.Object, Mock.Of<IRainmakerService>());
            SaveModel saveModel = new SaveModel()
            {
                id = "5fbc8ac67501aedc18992591",
                loanApplicationId = -1,
                request = new Request()
                {
                    userId = 1,
                    userName = "rainsoft"
                    ,
                    document = new RequestDocument
                    {
                        typeId = string.Empty,
                        displayName = "xyz",
                        message = "abc",
                        status = RequestStatus.Active,
                        files = new List<RequestFile>()
                    }
                }

            };
            //Act
            var result = await service.Save(saveModel, new List<string>());
            //Assert
            Assert.True(result);


        }
        [Fact]
        public async Task TestServiceSaveRequestTypeIdNotNull()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<StatusList>> mockCollection = new Mock<IMongoCollection<StatusList>>();
            Mock<IMongoCollection<Request>> mockRequestCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorRequest = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IMongoCollection<LoanApplication>> mockLoanApplicationCollection = new Mock<IMongoCollection<LoanApplication>>();


            List<BsonDocument> list = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                         { "_id" ,"5fbc8ac67501aedc18992591"}
                          }
                 };
            mockdb.Setup(x => x.GetCollection<StatusList>("StatusList", It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<StatusList, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);


            List<BsonDocument> requestlist = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" ,"5fbc8ac67501aedc18992591"}
                          }
                 };
            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockRequestCollection.Object);
            mockRequestCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorRequest.Object);
            mockCursorRequest.Setup(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);
            mockCursorRequest.SetupGet(x => x.Current).Returns(requestlist);
            mockdb.Setup(x => x.GetCollection<LoanApplication>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockLoanApplicationCollection.Object);

            mockLoanApplicationCollection.Setup(s => s.InsertOneAsync(It.IsAny<LoanApplication>(), It.IsAny<InsertOneOptions>(), It.IsAny<System.Threading.CancellationToken>()));


            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            var service = new RequestService(mock.Object, Mock.Of<IRainmakerService>());
            SaveModel saveModel = new SaveModel()
            {
                id = "5fbc8ac67501aedc18992591",
                loanApplicationId = -1,
                request = new Request()
                {
                    userId = 1,
                    userName = "rainsoft"
                    ,
                    document = new RequestDocument
                    {
                        typeId = "5fbc8ac67501aedc18992591",
                        displayName = "xyz",
                        message = "abc",
                        status = RequestStatus.Active,
                        files = new List<RequestFile>()
                    }
                }

            };
            //Act
            var result = await service.Save(saveModel, new List<string>());
            //Assert
            Assert.True(result);


        }
        [Fact]
        public async Task TestServiceSubmit()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockRequestCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorRequest = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                         { "_id" ,"5fbc8ac67501aedc18992591"}
                          }
                 };
            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockRequestCollection.Object);
            mockRequestCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorRequest.Object);
            mockCursorRequest.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorRequest.SetupGet(x => x.Current).Returns(list);
            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockRequestCollection.Object);
            mockRequestCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(0, 1, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            var service = new RequestService(mock.Object, Mock.Of<IRainmakerService>());

            //Act
            var result = await service.Submit(string.Empty, "5fb51519e223e0428d82c41b", "5fb5152ee223e0428d82c41c", "5fb51533e223e0428d82c41d", string.Empty,
          string.Empty, It.IsAny<int>(), string.Empty, string.Empty, It.IsAny<int>(), It.IsAny<int>(), string.Empty, new List<string>(), "");
            //Assert
            Assert.NotNull(result);


        }
        [Fact]
        public async Task TestServiceSubmitNull()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockRequestCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorRequest = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                         { "_id" ,"5fbc8ac67501aedc18992591"}
                          }
                 };
            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockRequestCollection.Object);

            mockRequestCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorRequest.Object);
            mockCursorRequest.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorRequest.SetupGet(x => x.Current).Returns(list);
            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockRequestCollection.Object);
            mockRequestCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(0, 0, BsonInt32.Create(1)));



            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            var service = new RequestService(mock.Object, Mock.Of<IRainmakerService>());

            //Act
            var result = await service.Submit(string.Empty, "5fb51519e223e0428d82c41b", "5fb5152ee223e0428d82c41c", "5fb51533e223e0428d82c41d", string.Empty,
          string.Empty, It.IsAny<int>(), string.Empty, string.Empty, It.IsAny<int>(), It.IsAny<int>(), string.Empty, new List<string>(), "");
            //Assert
            Assert.Null(result);


        }
        [Fact]
        public async Task TestServiceGetFileByDocId()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockRequestCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorRequest = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                         { "loanApplicationId" ,1}
                          }
                 };
            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockRequestCollection.Object);
            mockRequestCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorRequest.Object);
            mockCursorRequest.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorRequest.SetupGet(x => x.Current).Returns(list);


            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            var service = new RequestService(mock.Object, Mock.Of<IRainmakerService>());
            FileViewModel fileViewModel = new FileViewModel
            {
                id = "5fb51519e223e0428d82c41b",
                requestId = "5fb5152ee223e0428d82c41c",
                docId = "5fb51533e223e0428d82c41d",
                fileId = "5ff6f3d4d154cabbab9b30fe"
            };
            //Act
            var result = await service.GetFileByDocId(fileViewModel, It.IsAny<string>(), It.IsAny<int>());
            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result[0].loanApplicationId);

        }


        [Fact]
        public async Task TestServiceGetActivityLogId()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<ActivityLog>> mockCollection = new Mock<IMongoCollection<ActivityLog>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                         { "_id" ,"5fb51519e223e0428d82c41b"}
                          }
                 };
            mockdb.Setup(x => x.GetCollection<ActivityLog>("ActivityLog", It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<ActivityLog, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);


            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            var service = new ActivityLogService(mock.Object);
            







            var result = await service.GetActivityLogId("5fb51519e223e0428d82c41b", "5fb5152ee223e0428d82c41c", "5fb51533e223e0428d82c41d");
            //Assert
            Assert.NotNull(result);
            Assert.Equal("5fb51519e223e0428d82c41b", result);

        }
        [Fact]
        public async Task TestServiceInsertLog()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<ActivityLog>> mockCollection = new Mock<IMongoCollection<ActivityLog>>();
           

            mockdb.Setup(x => x.GetCollection<ActivityLog>("ActivityLog", It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);


            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<ActivityLog>>(), It.IsAny<UpdateDefinition<ActivityLog>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));


            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            var service = new ActivityLogService(mock.Object);

            //Act
            await service.InsertLog("5fb51519e223e0428d82c41b", "abc");
            Assert.Equal(1, 1);

        }
        [Fact]
        public async Task TestServiceGetLoanApplicationId()
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
                        //Cover all empty fields except files
                         { "loanApplicationId" ,1}
                          }
                 };

            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);

            mockCursor.SetupGet(x => x.Current).Returns(list);
            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            var service = new RainmakerService(Mock.Of<HttpClient>(), Mock.Of<IConfiguration>(), mock.Object);

            //Act
            await service.GetLoanApplicationId("5fb51519e223e0428d82c41b");
            Assert.Equal(1,1);
        }
        [Fact]
        public async Task TestServiceUpdateLoanInfo()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            Mock<IMongoCollection<Request>> mockMcuCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorMcu = new Mock<IAsyncCursor<BsonDocument>>();

            Mock<IMongoCollection<Request>> mockLastDocRequestSentDateCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorLastDocRequestSentDate = new Mock<IAsyncCursor<BsonDocument>>();



            Mock<IMongoCollection<Request>> mockRemainingDocumentsCollection = new Mock<IMongoCollection<Request>>();     
            Mock<IAsyncCursor<BsonDocument>> mockCursorRemainingDocuments = new Mock<IAsyncCursor<BsonDocument>>();

            Mock<IMongoCollection<Request>> mockOutstandingDocumentsCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorOutstandingDocuments = new Mock<IAsyncCursor<BsonDocument>>();

            Mock<IMongoCollection<Request>> mockCompletedDocumentsCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorCompletedDocuments = new Mock<IAsyncCursor<BsonDocument>>();

            mockConfiguration.Setup(x => x["RainMaker:Url"]).Returns("http://test.com");

            List<BsonDocument> list = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                         
                           { "LastDocUploadDate" ,DateTime.UtcNow}
                 }
                 };

            mockdb.SetupSequence(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object).Returns(mockMcuCollection.Object).Returns(mockLastDocRequestSentDateCollection.Object).Returns(mockRemainingDocumentsCollection.Object).Returns(mockOutstandingDocumentsCollection.Object).Returns(mockCompletedDocumentsCollection.Object);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockCursor.Setup(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            List<BsonDocument> mculist = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                         
                           { "LastDocUploadDate" ,DateTime.UtcNow}
                 }
                 };

            mockMcuCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorMcu.Object);
            mockCursorMcu.Setup(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);
            mockCursorMcu.SetupGet(x => x.Current).Returns(mculist);


            List<BsonDocument> LastDocRequestSentDatelist = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                         
                           { "LastDocRequestSentDate" ,DateTime.UtcNow}
                 }
                 };

            mockLastDocRequestSentDateCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorLastDocRequestSentDate.Object);
            mockCursorLastDocRequestSentDate.Setup(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);
            mockCursorLastDocRequestSentDate.SetupGet(x => x.Current).Returns(LastDocRequestSentDatelist);



            List<BsonDocument> RemainingDocumentslist = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                         
                           { "isMcuVisible" ,true}
                 }
                 };

            mockRemainingDocumentsCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorRemainingDocuments.Object);
            mockCursorRemainingDocuments.Setup(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);
            mockCursorRemainingDocuments.SetupGet(x => x.Current).Returns(RemainingDocumentslist);


            List<BsonDocument> OutstandingDocumentslist = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                         
                           { "isMcuVisible" ,true}
                 }
                 };

            mockOutstandingDocumentsCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorOutstandingDocuments.Object);
            mockCursorOutstandingDocuments.Setup(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);
            mockCursorOutstandingDocuments.SetupGet(x => x.Current).Returns(OutstandingDocumentslist);
           
            
            List<BsonDocument> mockCursorCompletedDocumentslist = new List<BsonDocument>()
            {

                 new BsonDocument
                    {
                        //Cover all empty fields except files
                         
                           { "isMcuVisible" ,true}
                 }
                 };

            mockCompletedDocumentsCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorCompletedDocuments.Object);
            mockCursorCompletedDocuments.Setup(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);
            mockCursorCompletedDocuments.SetupGet(x => x.Current).Returns(mockCursorCompletedDocumentslist);
            
            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK
                });

            var service = new RainmakerService(new HttpClient(mockMessageHandler.Object), mockConfiguration.Object, mock.Object);

            //Act
            await service.UpdateLoanInfo(1, "5fb51519e223e0428d82c41b", new List<string>());
            Assert.Equal(1, 1);
        }

        [Fact]
        public async Task TestKeyStoreGetFileKey()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x["KeyStore:Url"]).Returns("http://test.com");
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("test")
                });

            var service = new KeyStoreService(new HttpClient(mockMessageHandler.Object), mockConfiguration.Object);
            var result = await service.GetFileKey();
            Assert.Equal("test",result);
        }

        [Fact]
        public async Task TestKeyStoreGetFtpKey()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x["KeyStore:Url"]).Returns("http://test.com");
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("test")
                });

            var service = new KeyStoreService(new HttpClient(mockMessageHandler.Object), mockConfiguration.Object);
            var result = await service.GetFtpKey();
            Assert.Equal("test", result);
        }
        [Fact]
        public async Task TestSendFilesToBytePro()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x["LosIntegration:Url"]).Returns("http://test.com");
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("test")
                });

            var service = new LosIntegrationService(new HttpClient(mockMessageHandler.Object), mockConfiguration.Object, Mock.Of<ILogger<LosIntegrationService>>());
            var result = await service.SendFilesToBytePro(1,"","","","",new List<string>());
            Assert.Equal("test", result);
        }
        [Fact]
        public async Task TestPostLoanApplication()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x["RainMaker:Url"]).Returns("http://test.com");
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("test")
                });

            var service = new RainmakerService(new HttpClient(mockMessageHandler.Object), mockConfiguration.Object, null);
            var result = await service.PostLoanApplication(1, false, new List<string>());
            Assert.Equal("test", result);
        }
    }
}
