using LoanApplication.API.Controllers;
using LoanApplication.Model;
using LoanApplication.Service;
using LoanApplication.Tests.Helpers;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;
using TrackableEntities.Common.Core;
using Xunit;
using LoanApplication.Tests.Services;
using LoanApplication = LoanApplicationDb.Entity.Models.LoanApplication;
using System.Text.Json;

namespace LoanApplication.Tests.Services
{
    public partial class QuestionServiceTests
    {

        private readonly TenantModel _tenant;
        public QuestionServiceTests()
        {
            _tenant = ObjectHelper.GetTenantModel(1);
        }


        [Fact]
        public async Task GetSection2PrimaryQuestions_ShouldReturnSection2PrimaryQuestions()
        {
            //Arrange
            const string testMethodName = nameof(GetSection2PrimaryQuestions_ShouldReturnSection2PrimaryQuestions);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);

            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            var questionService = new QuestionService(unitOfWork, null, dbFunctionServiceMock.Object);

            const int userId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            var borrower = new LoanApplicationDb.Entity.Models.Borrower()
            {
                Id = borrowerId,
                TenantId = _tenant.Id,
                LoanContactId = 1,
                LoanApplicationId = loanApplicationId,
                OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                BorrowerQuestionResponses = new HashSet<BorrowerQuestionResponse>
                {
                    new BorrowerQuestionResponse
                    {
                        Id = 1,
                        QuestionId = 1,
                        SelectionOptionId = 1,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    }
                }
            };

            Question fakeQuestion = new Question()
            {
                Id = 1,
                QuestionSectionId = 2,
                BorrowerDisplayOptionId = (int)Model.QuestionBorrowerDisplayOption.PrimaryBorrower,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Name = "Fake Name"
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Question>().Insert(fakeQuestion);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);

            await unitOfWork.SaveChangesAsync();
            dbFunctionServiceMock.Setup(x => x.UdfQuestion(It.IsAny<int>(), It.IsAny<int?>())).Returns(unitOfWork.Repository<Question>().Query());

            //Act
            var result = await questionService.GetSection2PrimaryQuestions(_tenant, userId, loanApplicationId, borrowerId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetSection2SecondaryQuestion_ShouldReturnSection2SecondaryQuestion()
        {
            //Arrange
            const string testMethodName = nameof(GetSection2SecondaryQuestion_ShouldReturnSection2SecondaryQuestion);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);

            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            var questionService = new QuestionService(unitOfWork, null, dbFunctionServiceMock.Object);

            const int userId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            var borrower = new LoanApplicationDb.Entity.Models.Borrower()
            {
                Id = borrowerId,
                TenantId = _tenant.Id,
                LoanContactId = 1,
                LoanApplicationId = loanApplicationId,
                OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                LoanContact_LoanContactId = new LoanContact
                {
                    FirstName = "fName",
                    LastName = "lName",
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                },

                BorrowerQuestionResponses = new HashSet<BorrowerQuestionResponse>
                {
                    new BorrowerQuestionResponse
                    {
                        Id = 1,
                        QuestionId = 1,
                        SelectionOptionId = 1,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    }
                }
            };

            Question fakeQuestion = new Question()
            {
                Id = 1,
                QuestionSectionId = 2,
                BorrowerDisplayOptionId = (int)Model.QuestionBorrowerDisplayOption.CoBorrower,

                CoBorrowerText = "abc",
                Description = "fake desc",

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Name = "Fake Name"
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Question>().Insert(fakeQuestion);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);

            await unitOfWork.SaveChangesAsync();
            dbFunctionServiceMock.Setup(x => x.UdfQuestion(It.IsAny<int>(), It.IsAny<int?>())).Returns(unitOfWork.Repository<Question>().Query());

            //Act
            var result = await questionService.GetSection2SecondaryQuestion(_tenant, userId, loanApplicationId, borrowerId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetSectionOneForPrimaryBorrower_ShouldReturnSectionOneForPrimaryBorrower()
        {
            //Arrange
            const string testMethodName = nameof(GetSectionOneForPrimaryBorrower_ShouldReturnSectionOneForPrimaryBorrower);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);

            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            var questionService = new QuestionService(unitOfWork, null, dbFunctionServiceMock.Object);

            const int userId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            var borrower = new LoanApplicationDb.Entity.Models.Borrower()
            {
                Id = borrowerId,
                TenantId = _tenant.Id,
                LoanContactId = 1,
                LoanApplicationId = loanApplicationId,
                OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                BorrowerQuestionResponses = new HashSet<BorrowerQuestionResponse>
                {
                    new BorrowerQuestionResponse
                    {
                        Id = 1,
                        QuestionId = 1,
                        SelectionOptionId = 1,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    }
                }
            };

            Question fakeQuestion = new Question()
            {
                Id = 21,
                QuestionSectionId = 1,
                BorrowerDisplayOptionId = (int)Model.QuestionBorrowerDisplayOption.PrimaryBorrower,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Name = "Fake Name"
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Question>().Insert(fakeQuestion);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);

            await unitOfWork.SaveChangesAsync();
            dbFunctionServiceMock.Setup(x => x.UdfQuestion(It.IsAny<int>(), It.IsAny<int?>())).Returns(unitOfWork.Repository<Question>().Query());

            //Act
            var result = await questionService.GetSectionOneForPrimaryBorrower(_tenant, userId, loanApplicationId, borrowerId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetSectionOneForSecondaryBorrower_ShouldReturnSectionOneForSecondaryBorrower()
        {
            //Arrange
            const string testMethodName = nameof(GetSectionOneForSecondaryBorrower_ShouldReturnSectionOneForSecondaryBorrower);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);

            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            var questionService = new QuestionService(unitOfWork, null, dbFunctionServiceMock.Object);

            const int userId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            var borrower = new LoanApplicationDb.Entity.Models.Borrower()
            {
                Id = borrowerId,
                TenantId = _tenant.Id,
                LoanContactId = 1,
                LoanApplicationId = loanApplicationId,
                OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                LoanContact_LoanContactId = new LoanContact
                {
                    FirstName = "fName",
                    LastName = "lName",
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                },

                BorrowerQuestionResponses = new HashSet<BorrowerQuestionResponse>
                {
                    new BorrowerQuestionResponse
                    {
                        Id = 1,
                        QuestionId = 1,
                        SelectionOptionId = 1,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    }
                }
            };

            Question fakeQuestion = new Question()
            {
                Id = 21,
                QuestionSectionId = 1,
                BorrowerDisplayOptionId = (int)Model.QuestionBorrowerDisplayOption.CoBorrower,

                CoBorrowerText = "abc",
                Description = "fake desc",

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Name = "Fake Name"
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Question>().Insert(fakeQuestion);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);

            await unitOfWork.SaveChangesAsync();
            dbFunctionServiceMock.Setup(x => x.UdfQuestion(It.IsAny<int>(), It.IsAny<int?>())).Returns(unitOfWork.Repository<Question>().Query());

            //Act
            var result = await questionService.GetSectionOneForSecondaryBorrower(_tenant, userId, loanApplicationId, borrowerId);

            //Assert
            Assert.NotNull(result);
        }

        
        [Fact]
        public async Task AddOrUpdateSectionOne_ShouldUpdateRecord() //Always Update
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateSectionOne_ShouldUpdateRecord));
            var questionService = new QuestionService(unitOfWork, null, null);

            const int userId = 1;
            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            const int borrowerId = 1;

            var borrowerQuestionResponse = new BorrowerQuestionResponse
            {
                Id = 1,
                TenantId = _tenant.Id,
                QuestionId = 21,
                BorrowerId = borrowerId,
                TrackingState = TrackingState.Added,

                Borrower = new Borrower
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplication.Id,
                    TenantId = _tenant.Id,
                    OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                }
            };

            var borrowerQuestionResponse2 = new BorrowerQuestionResponse
            {
                Id = 2,
                TenantId = _tenant.Id,
                QuestionId = 30,
                BorrowerId = borrowerId,
                TrackingState = TrackingState.Added,

                Borrower = new Borrower
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplication.Id,
                    TenantId = _tenant.Id,
                    OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                }
            };

            var borrowerQuestionResponse4 = new BorrowerQuestionResponse
            {
                Id = 3,
                TenantId = _tenant.Id,
                QuestionId = 30,
                BorrowerId = 4,
                TrackingState = TrackingState.Added,

                Borrower = new Borrower
                {
                    Id = 2,
                    LoanApplicationId = loanApplication.Id,
                    TenantId = _tenant.Id,
                    OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                }
            };

            var borrower2 = new Borrower
            {
                Id = 2,
                LoanApplicationId = 1,
                TenantId = _tenant.Id,
                OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,
                TrackingState = TrackingState.Added,

                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    Id = 1,
                    UserId = userId,

                    TrackingState = TrackingState.Added,
                }
            };

            var borrower3 = new Borrower
            {
                Id = 3,
                LoanApplicationId = 1,
                TenantId = _tenant.Id,
                OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,
                TrackingState = TrackingState.Added,

                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    Id = 1,
                    UserId = userId,

                    TrackingState = TrackingState.Added,
                }
            };

            var borrower4 = new Borrower
            {
                Id = 4,
                LoanApplicationId = 1,
                TenantId = _tenant.Id,
                OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,
                TrackingState = TrackingState.Added,

                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    Id = 1,
                    UserId = userId,

                    TrackingState = TrackingState.Added,
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>().Insert(borrowerQuestionResponse);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>().Insert(borrowerQuestionResponse2);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>().Insert(borrowerQuestionResponse4);

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower2);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower3);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower4);

            await unitOfWork.SaveChangesAsync();


            //Act
            const string updatedAnswer = "Answer";
            QuestionRequestModel questionRequestModel = new QuestionRequestModel()
            {
                BorrowerId = borrowerId,
                LoanApplicationId = loanApplication.Id,
                State = "abc",
                Questions = new List<QuestionModel>()
                {
                    new QuestionModel()
                    {
                        Id =21,
                        Answer = updatedAnswer,
                        Question= "Question",
                        AnswerDetail = "Detail",
                        Data = null
                    },
                    new QuestionModel()
                    {
                        Id =30,
                        Answer = updatedAnswer,
                        Question= "Question",
                        AnswerDetail = "Detail",
                        Data = null
                    }
                }
            };

            var result = await questionService.AddOrUpdateSectionOne(_tenant, userId, questionRequestModel);


            //Assert
            //var record = unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>().Query(x => x.Id == 1).Single();

            Assert.NotNull(result);
            Assert.IsType<bool>(result);
            //Assert.Equal(updatedAnswer, record.AnswerText);

        }

        [Fact]
        public async Task AddOrUpdateAddOrUpdateSectionOne_ShouldAddRecord() //Always Add
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateAddOrUpdateSectionOne_ShouldAddRecord));
            var questionService = new QuestionService(unitOfWork, null, null);

            const int userId = 1;
            const int borrowerId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            await unitOfWork.SaveChangesAsync();


            //Act
            QuestionRequestModel questionRequestModel = new QuestionRequestModel()
            {
                BorrowerId = borrowerId,
                LoanApplicationId = loanApplication.Id,
                State = "abc",
                Questions = new List<QuestionModel>()
                {
                    new QuestionModel()
                    {
                        Id =1,
                        Answer = "Answer",
                        Question= "Question",
                        AnswerDetail = "Detail",
                        Data = null
                    }
                }
            };

            await questionService.AddOrUpdateSectionOne(_tenant, userId, questionRequestModel);


            //Assert
            var record = unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>().Query(x => x.Id == 1).Single();

            Assert.NotNull(record);

        }

        [Fact]
        public async Task AddOrUpdateSection2_ShouldUpdateRecord() //Always Update
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateSection2_ShouldUpdateRecord));
            var questionService = new QuestionService(unitOfWork, null, null);

            const int userId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            const int borrowerId = 1;

            var borrowerQuestionResponse = new BorrowerQuestionResponse
            {
                TenantId = _tenant.Id,
                QuestionId = 1,
                BorrowerId = borrowerId,
                TrackingState = TrackingState.Added,

                Borrower = new Borrower
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplication.Id,
                    TenantId = _tenant.Id,
                    OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                }
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>().Insert(borrowerQuestionResponse);
            await unitOfWork.SaveChangesAsync();


            //Act
            const string updatedAnswer = "Answer";
            QuestionRequestModel questionRequestModel = new QuestionRequestModel()
            {
                BorrowerId = borrowerId,
                LoanApplicationId = loanApplication.Id,
                State = "abc",
                Questions = new List<QuestionModel>()
                {
                    new QuestionModel()
                    {
                        Id =1,
                        Answer = updatedAnswer,
                        Question= "Question",
                        AnswerDetail = "Detail",
                        Data = null
                    }
                }
            };

            await questionService.AddOrUpdateSection2(_tenant, userId, questionRequestModel);


            //Assert
            var record = unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>().Query(x => x.Id == 1).Single();

            Assert.NotNull(record);
            Assert.Equal(updatedAnswer, record.AnswerText);

        }

        [Fact]
        public async Task AddOrUpdateSection2_ShouldAddRecord() //Always Add
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateSection2_ShouldAddRecord));
            var questionService = new QuestionService(unitOfWork, null, null);

            const int userId = 1;
            const int borrowerId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            await unitOfWork.SaveChangesAsync();


            //Act
            QuestionRequestModel questionRequestModel = new QuestionRequestModel()
            {
                BorrowerId = borrowerId,
                LoanApplicationId = loanApplication.Id,
                State = "abc",
                Questions = new List<QuestionModel>()
                {
                    new QuestionModel()
                    {
                        Id =1,
                        Answer = "Answer",
                        Question= "Question",
                        AnswerDetail = "Detail",
                        Data = null
                    }
                }
            };

            await questionService.AddOrUpdateSection2(_tenant, userId, questionRequestModel);


            //Assert
            var record = unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>().Query(x => x.Id == 1).Single();

            Assert.NotNull(record);

        }

        [Fact]
        public async Task GetSection3ForPrimaryBorrower_ShouldReturnSection3ForPrimaryBorrower()
        {
            //Arrange
            const string testMethodName = nameof(GetSection3ForPrimaryBorrower_ShouldReturnSection3ForPrimaryBorrower);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);

            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            var questionService = new QuestionService(unitOfWork, null, dbFunctionServiceMock.Object);

            const int userId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            var borrower = new LoanApplicationDb.Entity.Models.Borrower()
            {
                Id = borrowerId,
                TenantId = _tenant.Id,
                LoanContactId = 1,
                LoanApplicationId = loanApplicationId,
                OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                BorrowerBankRuptcies = new HashSet<BorrowerBankRuptcy>
                {
                    new BorrowerBankRuptcy
                    {
                        Id = 1,
                        BankRuptcyId = 1,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    }
                },

                BorrowerQuestionResponses = new HashSet<BorrowerQuestionResponse>
                {
                    new BorrowerQuestionResponse
                    {
                        Id = 1,
                        QuestionId = 1,
                        SelectionOptionId = 1,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    }
                },
                BorrowerLiabilities = new HashSet<BorrowerLiability>
                {
                    new BorrowerLiability
                    {
                        Id = 1,
                        BorrowerId = borrowerId,
                        LiabilityTypeId = (int)LiabilityTypeEnum.Alimony,
                        RemainingMonth = 2500,
                        MonthlyPayment = 4000,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                        AddressInfo = new AddressInfo
                        {
                            Id = 1,
                            Name = "abc",
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                        }
                    }
                }
            };

            Question fakeQuestion = new Question()
            {
                Id = 140,
                QuestionSectionId = 3,
                BorrowerDisplayOptionId = (int)Model.QuestionBorrowerDisplayOption.PrimaryBorrower,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Name = "Fake Name"
            };

            Question fakeQuestion131 = new Question()
            {
                Id = 131,
                QuestionSectionId = 3,
                BorrowerDisplayOptionId = (int)Model.QuestionBorrowerDisplayOption.PrimaryBorrower,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Name = "Fake Name"
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Question>().Insert(fakeQuestion);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Question>().Insert(fakeQuestion131);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);

            await unitOfWork.SaveChangesAsync();
            dbFunctionServiceMock.Setup(x => x.UdfQuestion(It.IsAny<int>(), It.IsAny<int?>())).Returns(unitOfWork.Repository<Question>().Query());

            //Act
            var result = await questionService.GetSection3ForPrimaryBorrower(_tenant, userId, borrowerId, loanApplicationId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetSection3ForSecondaryBorrower_ShouldReturnSection3ForSecondaryBorrower()
        {
            //Arrange
            const string testMethodName = nameof(GetSection3ForSecondaryBorrower_ShouldReturnSection3ForSecondaryBorrower);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);

            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            var questionService = new QuestionService(unitOfWork, null, dbFunctionServiceMock.Object);

            const int userId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            var borrower = new LoanApplicationDb.Entity.Models.Borrower()
            {
                Id = borrowerId,
                TenantId = _tenant.Id,
                LoanContactId = 1,
                LoanApplicationId = loanApplicationId,
                OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                LoanContact_LoanContactId = new LoanContact
                {
                    FirstName = "fName",
                    LastName = "lName",
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                },

                BorrowerQuestionResponses = new HashSet<BorrowerQuestionResponse>
                {
                    new BorrowerQuestionResponse
                    {
                        Id = 1,
                        QuestionId = 1,
                        SelectionOptionId = 1,
                        AnswerText = "fake answer",
                        Description = "fake Description",
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    }
                },
                BorrowerLiabilities = new HashSet<BorrowerLiability>
                {
                    new BorrowerLiability
                    {
                        Id = 1,
                        BorrowerId = borrowerId,
                        LiabilityTypeId = (int)LiabilityTypeEnum.Alimony,
                        RemainingMonth = 2500,
                        MonthlyPayment = 4000,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                        AddressInfo = new AddressInfo
                        {
                            Id = 1,
                            Name = "abc",
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                        }
                    }
                },
                BorrowerBankRuptcies = new HashSet<BorrowerBankRuptcy>
                {
                    new BorrowerBankRuptcy
                    {
                        Id = 1,
                        BankRuptcyId =1,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                }
            };

            Question fakeQuestion = new Question()
            {
                Id = 131,
                QuestionSectionId = 3,
                BorrowerDisplayOptionId = (int)Model.QuestionBorrowerDisplayOption.CoBorrower,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Name = "Fake Name",
                CoBorrowerText = "abc",
            };

            Question fakeQuestion140 = new Question()
            {
                Id = 140,
                QuestionSectionId = 3,
                BorrowerDisplayOptionId = (int)Model.QuestionBorrowerDisplayOption.CoBorrower,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Name = "Fake Name",
                CoBorrowerText = "abc",
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Question>().Insert(fakeQuestion);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Question>().Insert(fakeQuestion140);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);

            await unitOfWork.SaveChangesAsync();
            dbFunctionServiceMock.Setup(x => x.UdfQuestion(It.IsAny<int>(), It.IsAny<int?>())).Returns(unitOfWork.Repository<Question>().Query());

            //Act
            var result = await questionService.GetSection3ForSecondaryBorrower(_tenant, userId, borrowerId, loanApplicationId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddOrUpdateSection3_ShouldUpdateRecord() //Always Update
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>("AddOrUpdateSection3_ShouldUpdateRecord123");
            var questionService = new QuestionService(unitOfWork, null, null);

            const int userId = 2;
            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            const int borrowerId = 2;


            var borrower = new Borrower
            {
                Id = borrowerId,
                LoanApplicationId = loanApplication.Id,
                TenantId = _tenant.Id,
                OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                BorrowerQuestionResponses = new HashSet<BorrowerQuestionResponse>
                {
                    new BorrowerQuestionResponse
                    {
                        //Id = 1,
                        Id = 2,
                        TenantId = _tenant.Id,
                        QuestionId = 1,
                        BorrowerId = borrowerId,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                },

                BorrowerLiabilities = new HashSet<BorrowerLiability>
                {
                    new BorrowerLiability
                    {
                        //Id= 1,
                        Id= 2,
                        LiabilityTypeId = (int)LiabilityTypeEnum.Alimony, //
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                        AddressInfo = new AddressInfo
                        {
                            //Id = 1,
                            Id = 2,
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                        }
                    }
                },

                BorrowerBankRuptcies = new HashSet<BorrowerBankRuptcy>
                {
                    new BorrowerBankRuptcy
                    {
                        //Id = 1,
                        Id = 2 ,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                }
            };
          


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            await unitOfWork.SaveChangesAsync();


            //Act
            const string updatedAnswer = "Answer";
            object obj = new { liabilityTypeId = 2, remainingMonth = 1, monthlyPayment = 2500, name = "abc" };
            QuestionRequestModel questionRequestModel = new QuestionRequestModel()
            {
                BorrowerId = borrowerId,
                LoanApplicationId = loanApplication.Id,
                State = "abc",
                Questions = new List<QuestionModel>()
                {
                    new QuestionModel()
                    {
                        Id =2,
                        Answer = updatedAnswer,
                        Question= "Question",
                        AnswerDetail = "Detail",
                        Data = null
                    }
                    ,
                    new QuestionModel()
                    {
                        Id =131,
                        Answer = updatedAnswer,
                        Question= "Question",
                        AnswerDetail = "Detail",
                        Data = ObjectToJosonElement(new int[] { 1 })
                    }
                    ,
                    new QuestionModel()
                    {
                        Id =140,
                        Answer = updatedAnswer,
                        Question= "Question",
                        AnswerDetail = "Detail",
                        //Data =  ObjectToJosonElement(new {liabilityTypeId =  2,remainingMonth = 1 ,monthlyPayment = 2500 , name = "abc"})
                        Data =  ObjectToJosonElement(new object[] { obj })
                    }
                }
            };

            await questionService.AddOrUpdateSection3(_tenant, userId, questionRequestModel);


            //Assert
            var record = unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>().Query(x => x.Id == 2).Single();

            Assert.NotNull(record);
            //Assert.Equal(updatedAnswer, record.AnswerText);

        }

        [Fact]
        public async Task AddOrUpdateSection3_BorrowerLiabilitiesIsNotNull_ShouldUpdateRecord() //Always Update
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateSection3_ShouldUpdateRecord));
            var questionService = new QuestionService(unitOfWork, null, null);

            const int userId = 1;
            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            const int borrowerId = 1;


            var borrower = new Borrower
            {
                Id = borrowerId,
                LoanApplicationId = loanApplication.Id,
                TenantId = _tenant.Id,
                OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                BorrowerQuestionResponses = new HashSet<BorrowerQuestionResponse>
                {
                    new BorrowerQuestionResponse
                    {
                        Id = 1,
                        TenantId = _tenant.Id,
                        QuestionId = 1,
                        BorrowerId = borrowerId,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                },

                BorrowerLiabilities = new HashSet<BorrowerLiability>
                {
                    new BorrowerLiability
                    {
                        Id= 2,
                        LiabilityTypeId = (int)LiabilityTypeEnum.SeparateMaintenance, //
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                        AddressInfo = new AddressInfo
                        {
                            Id = 1,
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                        }
                    }
                },

                BorrowerBankRuptcies = new HashSet<BorrowerBankRuptcy>
                {
                    new BorrowerBankRuptcy
                    {
                        Id = 1,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                }
            };



            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            await unitOfWork.SaveChangesAsync();


            //Act
            const string updatedAnswer = "Answer";
            object obj = new { liabilityTypeId = 2, remainingMonth = 1, monthlyPayment = 2500, name = "abc" };
            QuestionRequestModel questionRequestModel = new QuestionRequestModel()
            {
                BorrowerId = borrowerId,
                LoanApplicationId = loanApplication.Id,
                State = "abc",
                Questions = new List<QuestionModel>()
                {
                    new QuestionModel()
                    {
                        Id =1,
                        Answer = updatedAnswer,
                        Question= "Question",
                        AnswerDetail = "Detail",
                        Data = null
                    }
                    ,
                    new QuestionModel()
                    {
                        Id =131,
                        Answer = updatedAnswer,
                        Question= "Question",
                        AnswerDetail = "Detail",
                        Data = ObjectToJosonElement(new int[] { 1 })
                    }
                    ,
                    new QuestionModel()
                    {
                        Id =140,
                        Answer = updatedAnswer,
                        Question= "Question",
                        AnswerDetail = "Detail",
                        Data =  ObjectToJosonElement(new object[] { obj })
                    }
                }
            };

            await questionService.AddOrUpdateSection3(_tenant, userId, questionRequestModel);


            //Assert
            var record = unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>().Query(x => x.Id == 1).Single();

            Assert.NotNull(record);
            Assert.Equal(updatedAnswer, record.AnswerText);

        }

        private JsonElement? ObjectToJosonElement(Object value)
        {
            if (value == null) return null;
            var json = JsonSerializer.Serialize(value);
            using var document = JsonDocument.Parse(json);
            return document.RootElement.Clone();
        }

        [Fact]
        public async Task AddOrUpdateSection3_ShouldAddRecord() //Always Add
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateSection3_ShouldAddRecord));
            var questionService = new QuestionService(unitOfWork, null, null);

            const int userId = 1;
            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            const int borrowerId = 1;

            var borrower = new Borrower
            {
                Id = borrowerId,
                LoanApplicationId = loanApplication.Id,
                TenantId = _tenant.Id,
                OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                BorrowerQuestionResponses = null,

                BorrowerLiabilities = new HashSet<BorrowerLiability>
                {
                    new BorrowerLiability
                    {
                        Id= 1,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                        AddressInfo = new AddressInfo
                        {
                            Id = 1,
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                        }
                    }
                },

                BorrowerBankRuptcies = new HashSet<BorrowerBankRuptcy>
                {
                    new BorrowerBankRuptcy
                    {
                        Id = 1,
                        BankRuptcyId = 1,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    },
                    new BorrowerBankRuptcy
                    {
                        Id = 2,
                        BankRuptcyId = 2,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                }
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            await unitOfWork.SaveChangesAsync();


            //Act
            QuestionRequestModel questionRequestModel = new QuestionRequestModel()
            {
                BorrowerId = borrowerId,
                LoanApplicationId = loanApplication.Id,
                State = "abc",
                Questions = new List<QuestionModel>()
                {
                    new QuestionModel()
                    {
                        Id= 1,
                        //Id =131,
                        Answer = "Answer",
                        Question= "Question",
                        AnswerDetail = "Detail",

                        Data = null
                    }
                    //,
                    //new QuestionModel()
                    //{
                    //    Id =140,
                    //    Answer = "Answer",
                    //    Question= "Question",
                    //    AnswerDetail = "Detail",
                    //    Data = null
                    //}
                }
            };

            await questionService.AddOrUpdateSection3(_tenant, userId, questionRequestModel);


            //Assert
            var record = unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>().Query(x => x.Id == 1).Single();

            Assert.NotNull(record);

        }

        [Fact]
        public async Task GetDemographicInformation_ShouldReturnDemographicInformation()
        {
            //Arrange
            const string testMethodName = nameof(GetDemographicInformation_ShouldReturnDemographicInformation);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var questionService = new QuestionService(unitOfWork, null, null);

            const int userId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            var borrower = new LoanApplicationDb.Entity.Models.Borrower()
            {
                Id = borrowerId,
                TenantId = _tenant.Id,
                LoanContactId = 1,
                LoanApplicationId = loanApplicationId,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                LoanContact_LoanContactId = new LoanContact
                {
                    Id = 1,
                    TenantId = _tenant.Id,
                    GenderId = 1,
                    ContactId = 1,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                    LoanContactRaceBinders = new HashSet<LoanContactRaceBinder>
                    {
                        new LoanContactRaceBinder
                        {
                            Id = 1,
                            RaceId = 1,
                            RaceDetailId = 1,
                            LoanContactId = 1,
                            OtherRace = "abc",
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                            RaceDetail = new RaceDetail
                            {
                                Id = 1,
                                RaceId = 1,
                                IsOther = false,
                                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                            }
                        }
                    },

                    LoanContactEthnicityBinders = new HashSet<LoanContactEthnicityBinder>
                    {
                        new LoanContactEthnicityBinder
                        {
                            Id = 1,
                            EthnicityId = 1,
                            EthnicityDetailId = 1,
                            LoanContactId = 1,
                            OtherEthnicity = "abc",
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                            EthnicityDetail = new EthnicityDetail
                            {
                                Id = 1,
                                EthnicityId = 1,
                                IsOther = false,
                                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                            }
                        }
                    }
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);

            await unitOfWork.SaveChangesAsync();

            //Act
            var result = await questionService.GetDemographicInformation(_tenant, userId, borrowerId, loanApplicationId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddOrUpdateDemogrhphicInfo_ShouldAddRecord() //Always Add
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateDemogrhphicInfo_ShouldAddRecord));
            var questionService = new QuestionService(unitOfWork, null, null);

            const int userId = 1;
            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            const int borrowerId = 1;


            var borrower = new LoanApplicationDb.Entity.Models.Borrower()
            {
                Id = borrowerId,
                TenantId = _tenant.Id,
                LoanContactId = 1,
                LoanApplicationId = loanApplication.Id,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                LoanContact_LoanContactId = new LoanContact
                {
                    Id = 1,
                    TenantId = _tenant.Id,
                    GenderId = 1,
                    ContactId = 1,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                    LoanContactRaceBinders = new HashSet<LoanContactRaceBinder>
                    {
                        new LoanContactRaceBinder
                        {
                            Id = 1,
                            RaceId = 1,
                            RaceDetailId = 1,
                            LoanContactId = 1,
                            OtherRace = "abc",
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                            RaceDetail = new RaceDetail
                            {
                                Id = 1,
                                RaceId = 1,
                                IsOther = false,
                                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                            }
                        }
                    },

                    LoanContactEthnicityBinders = new HashSet<LoanContactEthnicityBinder>
                    {
                        new LoanContactEthnicityBinder
                        {
                            Id = 1,
                            EthnicityId = 1,
                            EthnicityDetailId = 1,
                            LoanContactId = 1,
                            OtherEthnicity = "abc",
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                            EthnicityDetail = new EthnicityDetail
                            {
                                Id = 1,
                                EthnicityId = 1,
                                IsOther = false,
                                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                            }
                        }
                    }
                }
            };



            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            await unitOfWork.SaveChangesAsync();


            //Act
            DemographicInfoResponseModel demographicInfoResponseModel = new DemographicInfoResponseModel
            {
                BorrowerId = borrowerId,
                GenderId = 1,
                LoanApplicationId = 1,
                State = "abc",
                EthenticityResponseModel = new List<EthenticityResponseModel>
                {
                    new EthenticityResponseModel
                    {
                        EthenticityId = 1,
                        EthnicityDetailIds = new List<EthenticityDetailResponseModel>
                        {
                            new EthenticityDetailResponseModel
                            {
                                DetailId = 1,
                                IsOther = false,
                                OtherEthnicity = "abc"
                            }
                        }
                    }
                    ,
                    new EthenticityResponseModel
                    {
                        EthenticityId = 1,
                        EthnicityDetailIds = null
                    }

                },
                RaceResponseModel = new List<RaceResponseModel>
                {
                    new RaceResponseModel
                    {
                        RaceId = 1,
                        RaceDetailIds = new List<RaceDetailResponseModel>
                        {
                            new RaceDetailResponseModel
                            {
                                DetailId = 1,
                                IsOther = false,
                                OtherRace = "abc"
                            }
                        }
                    }

                    ,
                    new RaceResponseModel
                    {
                        RaceId = 1,
                        RaceDetailIds = null
                    }

                }
            };


            var result = await questionService.AddOrUpdateDemogrhphicInfo(_tenant, userId, demographicInfoResponseModel);


            //Assert

            Assert.IsType<bool>(result);
            Assert.True(result);

        }


        [Fact]
        public async Task GetAllPropertyUsageDropDown_ShouldReturnPropertyUsageDropDown()
        {
            //Arrange
            const string testMethodName = nameof(GetAllPropertyUsageDropDown_ShouldReturnPropertyUsageDropDown);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);

            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            var questionService = new QuestionService(unitOfWork, null, dbFunctionServiceMock.Object);


            PropertyUsage fakePropertyUsage = new PropertyUsage()
            {
                Id = 131,
                Name = "fake Name",
                TrackingState = TrackingState.Added
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.PropertyUsage>().Insert(fakePropertyUsage);

            await unitOfWork.SaveChangesAsync();
            dbFunctionServiceMock.Setup(x => x.UdfPropertyUsage(It.IsAny<int>(), It.IsAny<int?>())).Returns(unitOfWork.Repository<PropertyUsage>().Query());

            //Act
            var result = questionService.GetAllPropertyUsageDropDown(_tenant);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAllTitleHeldWithDropDown_ShouldReturnAllTitleHeldWithDropDown()
        {
            //Arrange
            const string testMethodName = nameof(GetAllTitleHeldWithDropDown_ShouldReturnAllTitleHeldWithDropDown);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);

            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            var questionService = new QuestionService(unitOfWork, null, dbFunctionServiceMock.Object);


            TitleHeldWith fakeTitleHeldWith = new TitleHeldWith()
            {
                Id = 1,
                Name = "fake Name",
                TrackingState = TrackingState.Added
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.TitleHeldWith>().Insert(fakeTitleHeldWith);

            await unitOfWork.SaveChangesAsync();
            dbFunctionServiceMock.Setup(x => x.UdfTitleHeldWith(It.IsAny<int>(), It.IsAny<int?>())).Returns(unitOfWork.Repository<TitleHeldWith>().Query());

            //Act
            var result = questionService.GetAllTitleHeldWithDropDown(_tenant);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAllBankruptcy_ShouldReturnAllBankruptcy()
        {
            //Arrange
            const string testMethodName = nameof(GetAllBankruptcy_ShouldReturnAllBankruptcy);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);

            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            var questionService = new QuestionService(unitOfWork, null, dbFunctionServiceMock.Object);


            BankRuptcy fakeBankRuptcy = new BankRuptcy()
            {
                Id = 1,
                Name = "fake Name",
                TrackingState = TrackingState.Added
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BankRuptcy>().Insert(fakeBankRuptcy);

            await unitOfWork.SaveChangesAsync();
            dbFunctionServiceMock.Setup(x => x.UdfBankRuptcy(It.IsAny<int>(), It.IsAny<int?>())).Returns(unitOfWork.Repository<BankRuptcy>().Query());

            //Act
            var result = questionService.GetAllBankruptcy(_tenant);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAllLiablilityType_ShouldReturnAllLiablilityType()
        {
            //Arrange
            const string testMethodName = nameof(GetAllLiablilityType_ShouldReturnAllLiablilityType);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);

            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            var questionService = new QuestionService(unitOfWork, null, dbFunctionServiceMock.Object);


            LiabilityType fakeLiabilityType = new LiabilityType()
            {
                Id = 1,
                Name = "fake Name",
                TrackingState = TrackingState.Added
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LiabilityType>().Insert(fakeLiabilityType);

            await unitOfWork.SaveChangesAsync();
            dbFunctionServiceMock.Setup(x => x.UdfLiabilityType(It.IsAny<int>(), It.IsAny<int?>())).Returns(unitOfWork.Repository<LiabilityType>().Query());

            //Act
            var result = questionService.GetAllLiablilityType(_tenant);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAllRaceList_ShouldReturnAllRaceList()
        {
            //Arrange
            const string testMethodName = nameof(GetAllRaceList_ShouldReturnAllRaceList);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);

            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            var questionService = new QuestionService(unitOfWork, null, dbFunctionServiceMock.Object);


            Race fakeRace = new Race()
            {
                Id = 1,
                Name = "fake Name",
                TrackingState = TrackingState.Added
            };

            RaceDetail fakeRaceDetail = new RaceDetail()
            {
                Id = 1,
                RaceId = 1,
                Name = "fake Name",
                TrackingState = TrackingState.Added
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Race>().Insert(fakeRace);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.RaceDetail>().Insert(fakeRaceDetail);

            await unitOfWork.SaveChangesAsync();
            dbFunctionServiceMock.Setup(x => x.UdfRace(It.IsAny<int>(), It.IsAny<int?>())).Returns(unitOfWork.Repository<Race>().Query());
            dbFunctionServiceMock.Setup(x => x.UdfRaceDetail(It.IsAny<int>(), It.IsAny<int?>())).Returns(unitOfWork.Repository<RaceDetail>().Query());

            //Act
            var result = questionService.GetAllRaceList(_tenant);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetGenderList_ShouldReturnGenderList()
        {
            //Arrange
            const string testMethodName = nameof(GetGenderList_ShouldReturnGenderList);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);

            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            var questionService = new QuestionService(unitOfWork, null, dbFunctionServiceMock.Object);


            Gender fakeGender = new Gender()
            {
                Id = 1,
                Name = "fake Name",
                TrackingState = TrackingState.Added
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Gender>().Insert(fakeGender);

            await unitOfWork.SaveChangesAsync();
            dbFunctionServiceMock.Setup(x => x.UdfGender(It.IsAny<int>(), It.IsAny<int?>())).Returns(unitOfWork.Repository<Gender>().Query());

            //Act
            var result = questionService.GetGenderList(_tenant);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAllEthnicityList_ShouldReturnAllEthnicityList()
        {
            //Arrange
            const string testMethodName = nameof(GetAllEthnicityList_ShouldReturnAllEthnicityList);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);

            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            var questionService = new QuestionService(unitOfWork, null, dbFunctionServiceMock.Object);


            Ethnicity fakeEthnicity = new Ethnicity()
            {
                Id = 1,
                Name = "fake Name",
                TrackingState = TrackingState.Added
            };

            EthnicityDetail fakeEthnicityDetail = new EthnicityDetail()
            {
                Id = 1,
                EthnicityId = 1,
                Name = "fake Name",
                TrackingState = TrackingState.Added
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Ethnicity>().Insert(fakeEthnicity);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.EthnicityDetail>().Insert(fakeEthnicityDetail);

            await unitOfWork.SaveChangesAsync();
            dbFunctionServiceMock.Setup(x => x.UdfEthnicity(It.IsAny<int>(), It.IsAny<int?>())).Returns(unitOfWork.Repository<Ethnicity>().Query());
            dbFunctionServiceMock.Setup(x => x.UdfEthnicityDetail(It.IsAny<int>(), It.IsAny<int?>())).Returns(unitOfWork.Repository<EthnicityDetail>().Query());

            //Act
            var result = questionService.GetAllEthnicityList(_tenant);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CheckPrimaryBorrowerSubjectProperty_ShouldReturnPrimaryBorrowerSubjectProperty()
        {
            //Arrange
            const string testMethodName = nameof(CheckPrimaryBorrowerSubjectProperty_ShouldReturnPrimaryBorrowerSubjectProperty);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);

            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            var questionService = new QuestionService(unitOfWork, null, dbFunctionServiceMock.Object);

            const int userId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;


            var borrower = new LoanApplicationDb.Entity.Models.Borrower()
            {
                Id = borrowerId,
                TenantId = _tenant.Id,
                LoanContactId = 1,
                LoanApplicationId = loanApplicationId,
                OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    Id = userId,
                    TenantId = _tenant.Id,
                    UserId = userId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                    PropertyInfo = new PropertyInfo
                    {
                        Id = 1,
                        PropertyUsageId = (int)PropertyUsageEnum.IWillLiveHerePrimaryResidence,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                },

                BorrowerProperties = new HashSet<BorrowerProperty>
                {
                    new BorrowerProperty
                    {
                        Id =1,
                        TrackingState = TrackingState.Added,

                        PropertyInfo = new PropertyInfo
                        {
                            Id = 10,
                            TrackingState = TrackingState.Added
                        }
                    }
                }

            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);

            await unitOfWork.SaveChangesAsync();


            //Act
            var result = await questionService.CheckPrimaryBorrowerSubjectProperty(_tenant, userId, borrowerId, loanApplicationId);

            //Assert
            Assert.NotNull(result);
        }


        [Fact]
        public async Task CheckSecondaryBorrowerSubjectProperty_ShouldReturnSecondaryBorrowerSubjectProperty()
        {
            //Arrange
            const string testMethodName = nameof(CheckSecondaryBorrowerSubjectProperty_ShouldReturnSecondaryBorrowerSubjectProperty);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);

            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            var questionService = new QuestionService(unitOfWork, null, dbFunctionServiceMock.Object);

            const int userId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;


            var borrower = new LoanApplicationDb.Entity.Models.Borrower()
            {
                Id = borrowerId,
                TenantId = _tenant.Id,
                LoanContactId = 1,
                LoanApplicationId = loanApplicationId,
                OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,
                WillLiveInSubjectProperty = true,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    Id = userId,
                    TenantId = _tenant.Id,
                    UserId = userId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                },

                BorrowerProperties = new HashSet<BorrowerProperty>
                {
                    new BorrowerProperty
                    {
                        Id =2,
                        TrackingState = TrackingState.Added,

                        PropertyInfo = new PropertyInfo
                        {
                            Id = 20,
                            TrackingState = TrackingState.Added
                        }
                    }
                }

            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);

            await unitOfWork.SaveChangesAsync();


            //Act
            var result = await questionService.CheckSecondaryBorrowerSubjectProperty(_tenant, userId, borrowerId, loanApplicationId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetGetGovtQuestionReview()
        {
            //Arrange
            const string testMethodName = nameof(GetGetGovtQuestionReview);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);

            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            var questionService = new QuestionService(unitOfWork, null, dbFunctionServiceMock.Object);

            const int userId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            var borrower = new LoanApplicationDb.Entity.Models.Borrower()
            {
                Id = borrowerId,
                TenantId = _tenant.Id,
                LoanContactId = 1,
                LoanApplicationId = loanApplicationId,
                OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                LoanContact_LoanContactId = new LoanContact
                {
                    Id = 1,
                    TenantId = _tenant.Id,
                    GenderId = 1,
                    ContactId = 1,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                },
                BorrowerBankRuptcies = new HashSet<BorrowerBankRuptcy>
                {
                    new BorrowerBankRuptcy
                    {
                        Id = 1,
                        BankRuptcyId = 1,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    }
                },

                BorrowerQuestionResponses = new HashSet<BorrowerQuestionResponse>
                {
                    new BorrowerQuestionResponse
                    {
                        Id = 1,
                        QuestionId = 1,
                        SelectionOptionId = 1,
                        BorrowerId=borrowerId,
                        TenantId = _tenant.Id,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    },
                   new BorrowerQuestionResponse
                    {
                        Id = 2,
                        QuestionId = 21,
                        BorrowerId=borrowerId,
                        TenantId = _tenant.Id,
                        SelectionOptionId = 1,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    },
                },
                BorrowerLiabilities = new HashSet<BorrowerLiability>
                {
                    new BorrowerLiability
                    {
                        Id = 1,
                        BorrowerId = borrowerId,
                        LiabilityTypeId = (int)LiabilityTypeEnum.Alimony,
                        RemainingMonth = 2500,
                        MonthlyPayment = 4000,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                        LiabilityType = new LiabilityType()
                        {
                        Id=1,
                        Name="abc"

                        },
                        AddressInfo = new AddressInfo
                        {
                            Id = 1,
                            Name = "abc",
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                        }
                    }
                }
            };


            Question fakeQuestionsec1 = new Question()
            {
                Id = 21,
                QuestionSectionId = 1,
                BorrowerDisplayOptionId = (int)Model.QuestionBorrowerDisplayOption.PrimaryBorrower,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Name = "Fake Name",

            };

            Question fakeQuestionsec2 = new Question()
            {
                Id = 1,
                QuestionSectionId = 2,
                BorrowerDisplayOptionId = (int)Model.QuestionBorrowerDisplayOption.PrimaryBorrower,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Name = "Fake Name"
            };


            Question fakeQuestion = new Question()
            {
                Id = 140,
                QuestionSectionId = 3,
                BorrowerDisplayOptionId = (int)Model.QuestionBorrowerDisplayOption.PrimaryBorrower,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Name = "Fake Name"
            };

            Question fakeQuestion131 = new Question()
            {
                Id = 131,
                QuestionSectionId = 3,
                BorrowerDisplayOptionId = (int)Model.QuestionBorrowerDisplayOption.PrimaryBorrower,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Name = "Fake Name"
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Question>().Insert(fakeQuestionsec1);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Question>().Insert(fakeQuestionsec2);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Question>().Insert(fakeQuestion);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Question>().Insert(fakeQuestion131);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);

            await unitOfWork.SaveChangesAsync();
            dbFunctionServiceMock.Setup(x => x.UdfQuestion(It.IsAny<int>(), It.IsAny<int?>())).Returns(unitOfWork.Repository<Question>().Query());

            //Act
            var result = await questionService.GetGovernmentQuestionReview(_tenant, userId, loanApplicationId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetDemographicInformationReview()
        {
            //Arrange
            const string testMethodName = nameof(GetDemographicInformationReview);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var questionService = new QuestionService(unitOfWork, null, null);

            const int userId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            var borrower = new LoanApplicationDb.Entity.Models.Borrower()
            {
                Id = borrowerId,
                TenantId = _tenant.Id,
                LoanContactId = 1,
                LoanApplicationId = loanApplicationId,
                OwnTypeId = 1,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                LoanContact_LoanContactId = new LoanContact
                {
                    Id = 1,
                    TenantId = _tenant.Id,
                    GenderId = 1,
                    ContactId = 1,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                    LoanContactRaceBinders = new HashSet<LoanContactRaceBinder>
                    {
                        new LoanContactRaceBinder
                        {
                            Id = 1,
                            RaceId = 1,
                            Race= new Race()
                            {
                            Id=1,
                            Name= "abc",
                             TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                            },
                            RaceDetailId = 1,
                            LoanContactId = 1,
                            OtherRace = "abc",
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                            RaceDetail = new RaceDetail
                            {
                                Id = 1,
                                RaceId = 1,
                                IsOther = false,
                                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                            }
                        }
                    },

                    LoanContactEthnicityBinders = new HashSet<LoanContactEthnicityBinder>
                    {
                        new LoanContactEthnicityBinder
                        {
                            Id = 1,Ethnicity = new Ethnicity(){ Id=1,Name="abc eth"},
                            EthnicityId = 1,
                            EthnicityDetailId = 1,
                            LoanContactId = 1,
                            OtherEthnicity = "abc",
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                            EthnicityDetail = new EthnicityDetail
                            {
                                Id = 1,
                                EthnicityId = 1,
                                IsOther = false,
                                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                            }
                        }
                    }
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);

            await unitOfWork.SaveChangesAsync();

            //Act
            var result = await questionService.GetDemographicInformationReview(_tenant, userId, loanApplicationId);

            //Assert
            Assert.NotNull(result);
        }


    }
}
