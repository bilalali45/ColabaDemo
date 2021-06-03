using LoanApplication.Model;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using RainMaker.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;

namespace LoanApplication.Service
{
    public class QuestionService : ServiceBase<LoanApplicationContext, Question>, IQuestionService
    {
        private readonly IDbFunctionService _dbFunctionService;
        public QuestionService(IUnitOfWork<LoanApplicationContext> previousUow,
                           IServiceProvider services, IDbFunctionService dbFunctionService) : base(previousUow: previousUow,
                                                             services: services)
        {
            _dbFunctionService = dbFunctionService;
        }
        public async Task<List<QuestionModel>> GetSection2PrimaryQuestions(TenantModel tenant, int userId, int loanApplicationId, int borrowerId)
        {

            var borrower = await Uow.Repository<Borrower>().Query(x => x.LoanApplication.Id == loanApplicationId && x.Id == borrowerId && x.TenantId == tenant.Id && x.LoanApplication.UserId == userId && x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary)
                   .Include(x => x.BorrowerQuestionResponses)
                   .SingleAsync();

            var questions = await _dbFunctionService.UdfQuestion(tenant.Id).Where(x => x.IsActive && x.QuestionSectionId == (int)Model.QuestionSection.Section2 && (x.BorrowerDisplayOptionId == (int)Model.QuestionBorrowerDisplayOption.Both || x.BorrowerDisplayOptionId == (int)Model.QuestionBorrowerDisplayOption.PrimaryBorrower)).OrderBy(x => x.Id)
             .ToListAsync();

            return questions.Select(x => new QuestionModel()
            {
                Id = x.Id,
                Question = x.PrimaryBorrowerText,
                Answer = borrower.BorrowerQuestionResponses.FirstOrDefault(y => y.QuestionId == x.Id)?.AnswerText,
                AnswerDetail = borrower.BorrowerQuestionResponses.FirstOrDefault(y => y.QuestionId == x.Id)?.Description,
                Data = null
            }).ToList();
        }


        public async Task<List<QuestionModel>> GetSection2SecondaryQuestion(TenantModel tenant, int userId, int loanApplicationId, int borrowerId)
        {

            var borrower = await Uow.Repository<Borrower>().Query(x => x.LoanApplication.Id == loanApplicationId && x.Id == borrowerId && x.TenantId == tenant.Id && x.LoanApplication.UserId == userId && x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary)
                .Include(x => x.LoanContact_LoanContactId)
                .Include(x => x.BorrowerQuestionResponses)
                   .SingleAsync();


            var questions = await _dbFunctionService.UdfQuestion(tenant.Id).Where(x => x.IsActive && x.QuestionSectionId == (int)Model.QuestionSection.Section2 && (x.BorrowerDisplayOptionId == (int)Model.QuestionBorrowerDisplayOption.Both || x.BorrowerDisplayOptionId == (int)Model.QuestionBorrowerDisplayOption.CoBorrower)).OrderBy(x => x.Id)
               .ToListAsync();


            return questions.Select(x => new QuestionModel()
            {
                Id = x.Id,
                Question = x.CoBorrowerText.Replace("[Co-Applicant First Name]", borrower.LoanContact_LoanContactId.FirstName).Replace("[Co-Applicant Last Name]", borrower.LoanContact_LoanContactId.LastName),
                Answer = borrower.BorrowerQuestionResponses.FirstOrDefault(y => y.QuestionId == x.Id)?.AnswerText,
                AnswerDetail = borrower.BorrowerQuestionResponses.FirstOrDefault(y => y.QuestionId == x.Id)?.Description,
                Data = null
            }).ToList();
        }

        public async Task<List<QuestionModel>> GetSectionOneForPrimaryBorrower(TenantModel tenant, int userId, int borrowerId, int loanApplicationId)
        {
            var borrower = await Uow.Repository<Borrower>().Query(x => x.LoanApplication.Id == loanApplicationId && x.Id == borrowerId && x.TenantId == tenant.Id && x.LoanApplication.UserId == userId && x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary)
                   .Include(x => x.BorrowerQuestionResponses)
                   .SingleAsync();

            var questions = await _dbFunctionService.UdfQuestion(tenant.Id).Where(x => x.IsActive && x.QuestionSectionId == (int)Model.QuestionSection.Section1 && (x.BorrowerDisplayOptionId == (int)Model.QuestionBorrowerDisplayOption.Both || x.BorrowerDisplayOptionId == (int)Model.QuestionBorrowerDisplayOption.PrimaryBorrower)).OrderBy(x => x.Id)
               .ToListAsync();

            return questions.Select(x =>
            {
                var question = new QuestionModel()
                {
                    Id = x.Id,
                    Question = x.PrimaryBorrowerText,
                    Answer = borrower.BorrowerQuestionResponses.FirstOrDefault(y => y.QuestionId == x.Id)?.AnswerText,
                    AnswerDetail = borrower.BorrowerQuestionResponses.FirstOrDefault(y => y.QuestionId == x.Id)?.Description,
                    Data = null
                };
                switch (x.Id)
                {
                    case 22:
                    case 21:
                        question.Data = ObjectToJosonElement(new { selectionOptionId = borrower.BorrowerQuestionResponses.FirstOrDefault(y => y.QuestionId == x.Id)?.SelectionOptionId });
                        break;
                }
                return question;
            }).ToList();
        }

        private JsonElement? ObjectToJosonElement(Object value)
        {
            if (value == null) return null;
            var json = JsonSerializer.Serialize(value);
            using var document = JsonDocument.Parse(json);
            return document.RootElement.Clone();
        }

        public async Task<List<QuestionModel>> GetSectionOneForSecondaryBorrower(TenantModel tenant, int userId, int borrowerId, int loanApplicationId)
        {

            var borrower = await Uow.Repository<Borrower>().Query(x => x.LoanApplication.Id == loanApplicationId && x.Id == borrowerId && x.TenantId == tenant.Id && x.LoanApplication.UserId == userId && x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary)
                   .Include(x => x.BorrowerQuestionResponses)
                   .Include(x => x.LoanContact_LoanContactId)
                   .SingleAsync();

            var questions = await _dbFunctionService.UdfQuestion(tenant.Id).Where(x => x.IsActive && x.QuestionSectionId == (int)Model.QuestionSection.Section1 && (x.BorrowerDisplayOptionId == (int)Model.QuestionBorrowerDisplayOption.Both || x.BorrowerDisplayOptionId == (int)Model.QuestionBorrowerDisplayOption.CoBorrower)).OrderBy(x => x.Id)
               .ToListAsync();

            return questions.Select(x =>
            {
                var question = new QuestionModel()
                {
                    Id = x.Id,
                    Question = x.CoBorrowerText.Replace("[Co-Applicant First Name]", borrower.LoanContact_LoanContactId.FirstName).Replace("[Co-Applicant Last Name]", borrower.LoanContact_LoanContactId.LastName),
                    Answer = borrower.BorrowerQuestionResponses.FirstOrDefault(y => y.QuestionId == x.Id)?.AnswerText,
                    AnswerDetail = borrower.BorrowerQuestionResponses.FirstOrDefault(y => y.QuestionId == x.Id)?.Description,
                    Data = null
                };
                switch (x.Id)
                {
                    case 22:
                    case 21:
                        question.Data = ObjectToJosonElement(new { selectionOptionId = borrower.BorrowerQuestionResponses.FirstOrDefault(y => y.QuestionId == x.Id)?.SelectionOptionId });
                        break;
                }
                return question;
            }).ToList();

        }

        public async Task<bool> AddOrUpdateSectionOne(TenantModel tenant, int userId, QuestionRequestModel model)
        {
            foreach (QuestionModel question in model.Questions)
            {
                LoanApplicationDb.Entity.Models.BorrowerQuestionResponse borrowerresponse = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>()
                                         .Query(query: x => x.TenantId == tenant.Id && x.QuestionId == question.Id && x.BorrowerId == model.BorrowerId && x.Borrower.LoanApplicationId == model.LoanApplicationId && x.Borrower.LoanApplication.UserId == userId)
                                         .Include(x => x.Borrower)
                                         .SingleOrDefaultAsync();

                if (borrowerresponse != null)
                {
                    borrowerresponse.Description = question.AnswerDetail;
                    borrowerresponse.AnswerText = question.Answer;
                    borrowerresponse.TrackingState = TrackingState.Modified;
                    Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>().Update(borrowerresponse);
                }
                else
                {
                    borrowerresponse = new LoanApplicationDb.Entity.Models.BorrowerQuestionResponse
                    {
                        QuestionId = question.Id,
                        BorrowerId = model.BorrowerId,
                        TenantId = tenant.Id,
                        Description = question.AnswerDetail,
                        AnswerText = question.Answer,
                        TrackingState = TrackingState.Added,
                    };


                    Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>().Insert(borrowerresponse);
                }
                switch (question.Id)
                {
                    case 22:
                    case 21:
                        borrowerresponse.SelectionOptionId = question.Data?.GetProperty("selectionOptionId").GetInt32();
                        break;
                    case 30:
                        if (borrowerresponse.Borrower.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary)
                        {
                            var borrowers = await Uow.Repository<Borrower>().Query(x => x.LoanApplicationId == model.LoanApplicationId && x.LoanApplication.UserId == userId && x.TenantId == tenant.Id && x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary).ToListAsync();
                            foreach (var b in borrowers)
                            {
                                LoanApplicationDb.Entity.Models.BorrowerQuestionResponse br = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>()
                                         .Query(query: x => x.TenantId == tenant.Id && x.QuestionId == question.Id && x.BorrowerId == b.Id && x.Borrower.LoanApplicationId == model.LoanApplicationId && x.Borrower.LoanApplication.UserId == userId)
                                         .Include(x => x.Borrower)
                                         .SingleOrDefaultAsync();

                                if (br != null)
                                {
                                    br.AnswerText = "1";
                                    br.TrackingState = TrackingState.Modified;
                                    Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>().Update(br);
                                }
                                else
                                {
                                    br = new LoanApplicationDb.Entity.Models.BorrowerQuestionResponse
                                    {
                                        QuestionId = question.Id,
                                        BorrowerId = b.Id,
                                        TenantId = tenant.Id,
                                        AnswerText = "1",
                                        TrackingState = TrackingState.Added,
                                    };


                                    Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>().Insert(br);
                                }
                            }
                        }
                        break;
                }
            }
            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddOrUpdateSection2(TenantModel tenant, int userId, QuestionRequestModel model)
        {
            foreach (QuestionModel question in model.Questions)
            {
                LoanApplicationDb.Entity.Models.BorrowerQuestionResponse borrowerresponse = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>()
                                         .Query(query: x => x.TenantId == tenant.Id && x.QuestionId == question.Id && x.BorrowerId == model.BorrowerId &&
                                          x.Borrower.LoanApplicationId == model.LoanApplicationId && x.Borrower.LoanApplication.UserId == userId)
                                         .SingleOrDefaultAsync();

                if (borrowerresponse != null)
                {
                    borrowerresponse.Description = question.AnswerDetail;
                    borrowerresponse.AnswerText = question.Answer;
                    borrowerresponse.TrackingState = TrackingState.Modified;

                    Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>().Update(borrowerresponse);
                }
                else
                {

                    borrowerresponse = new LoanApplicationDb.Entity.Models.BorrowerQuestionResponse
                    {
                        QuestionId = question.Id,
                        BorrowerId = model.BorrowerId,
                        TenantId = tenant.Id,
                        Description = question.AnswerDetail,
                        AnswerText = question.Answer,
                        TrackingState = TrackingState.Added,

                    };

                    Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>().Insert(borrowerresponse);
                }

            }
            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);
            await SaveChangesAsync();

            return true;
        }
        public async Task<List<QuestionModel>> GetSection3ForPrimaryBorrower(TenantModel tenant, int userId, int borrowerId, int loanApplicationId)
        {
            var borrower = await Uow.Repository<Borrower>().Query(x => x.LoanApplication.Id == loanApplicationId && x.Id == borrowerId && x.TenantId == tenant.Id && x.LoanApplication.UserId == userId && x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary)
                  .Include(x => x.BorrowerQuestionResponses)
                  .Include(x => x.BorrowerLiabilities)
                  .ThenInclude(a => a.AddressInfo)
                  .Include(x => x.BorrowerBankRuptcies)
                  .SingleAsync();




            var questions = await _dbFunctionService.UdfQuestion(tenant.Id).Where(x => x.IsActive && x.QuestionSectionId == (int)Model.QuestionSection.Section3 && (x.BorrowerDisplayOptionId == (int)Model.QuestionBorrowerDisplayOption.Both || x.BorrowerDisplayOptionId == (int)Model.QuestionBorrowerDisplayOption.PrimaryBorrower)).OrderBy(x => x.Id)
               .ToListAsync();

            return questions.Select(x =>
            {
                var question = new QuestionModel()
                {
                    Id = x.Id,
                    Question = x.PrimaryBorrowerText,
                    Answer = borrower.BorrowerQuestionResponses.FirstOrDefault(y => y.QuestionId == x.Id)?.AnswerText,
                    AnswerDetail = borrower.BorrowerQuestionResponses.FirstOrDefault(y => y.QuestionId == x.Id)?.Description,
                    Data = null
                };
                switch (x.Id)
                {
                    case 131:
                        question.Data = ObjectToJosonElement(borrower.BorrowerBankRuptcies.Select(y => y.BankRuptcyId).ToArray());
                        break;
                    case 140:
                        question.Data = ObjectToJosonElement(borrower.BorrowerLiabilities.Where(x => (new List<int> { (int)LiabilityTypeEnum.Alimony, (int)LiabilityTypeEnum.ChildSupport, (int)LiabilityTypeEnum.SeparateMaintenance }).Contains(x.LiabilityTypeId.Value)).Select(y => new { liabilityTypeId = y.LiabilityTypeId, remainingMonth = y.RemainingMonth, monthlyPayment = y.MonthlyPayment, name = y.AddressInfo.Name }).ToArray());

                        break;

                }
                return question;
            }).ToList();
        }
        public async Task<List<QuestionModel>> GetSection3ForSecondaryBorrower(TenantModel tenant, int userId, int borrowerId, int loanApplicationId)
        {
            var borrower = await Uow.Repository<Borrower>().Query(x => x.LoanApplication.Id == loanApplicationId && x.Id == borrowerId && x.TenantId == tenant.Id && x.LoanApplication.UserId == userId && x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary)
              .Include(x => x.BorrowerQuestionResponses)
              .Include(x => x.LoanContact_LoanContactId)
                  .Include(x => x.BorrowerLiabilities)
                  .ThenInclude(a => a.AddressInfo)
                  .Include(x => x.BorrowerBankRuptcies)
                  .SingleAsync();


            var questions = await _dbFunctionService.UdfQuestion(tenant.Id).Where(x => x.IsActive && x.QuestionSectionId == (int)Model.QuestionSection.Section3 && (x.BorrowerDisplayOptionId == (int)Model.QuestionBorrowerDisplayOption.Both || x.BorrowerDisplayOptionId == (int)Model.QuestionBorrowerDisplayOption.CoBorrower)).OrderBy(x => x.Id)
               .ToListAsync();

            return questions.Select(x =>
            {
                var question = new QuestionModel()
                {
                    Id = x.Id,
                    Question = x.CoBorrowerText.Replace("[Co-Applicant First Name]", borrower.LoanContact_LoanContactId.FirstName).Replace("[Co-Applicant Last Name]", borrower.LoanContact_LoanContactId.LastName),
                    Answer = borrower.BorrowerQuestionResponses.FirstOrDefault(y => y.QuestionId == x.Id)?.AnswerText,

                    AnswerDetail = borrower.BorrowerQuestionResponses.FirstOrDefault(y => y.QuestionId == x.Id)?.Description,
                    Data = null
                };
                switch (x.Id)
                {
                    case 131:
                        question.Data = ObjectToJosonElement(borrower.BorrowerBankRuptcies.Select(y => y.BankRuptcyId).ToArray());
                        break;
                    case 140:
                        question.Data = ObjectToJosonElement(borrower.BorrowerLiabilities.Where(x => (new List<int> { (int)LiabilityTypeEnum.Alimony, (int)LiabilityTypeEnum.ChildSupport, (int)LiabilityTypeEnum.SeparateMaintenance }).Contains(x.LiabilityTypeId.Value)).Select(y => new { liabilityTypeId = y.LiabilityTypeId, remainingMonth = y.RemainingMonth, monthlyPayment = y.MonthlyPayment, name = y.AddressInfo.Name }).ToArray());

                        break;

                }
                return question;
            }).ToList();
        }


        public List<DropDownModel> GetAllPropertyUsageDropDown(TenantModel tenant)
        {
            List<DropDownModel> list = _dbFunctionService.UdfPropertyUsage(tenant.Id).Select(x => new DropDownModel { Id = x.Id, Name = x.Name }).ToList();
            return list;
        }

        public List<DropDownModel> GetAllTitleHeldWithDropDown(TenantModel tenant)
        {
            List<DropDownModel> list = _dbFunctionService.UdfTitleHeldWith(tenant.Id).Select(x => new DropDownModel { Id = x.Id, Name = x.Name }).ToList();
            return list;
        }

        public List<DropDownModel> GetAllBankruptcy(TenantModel tenant)
        {
            List<DropDownModel> list = _dbFunctionService.UdfBankRuptcy(tenant.Id).Select(x => new DropDownModel { Id = x.Id, Name = x.Name }).ToList();
            return list;
        }

        public List<DropDownModel> GetAllLiablilityType(TenantModel tenant)
        {
            List<DropDownModel> list = _dbFunctionService.UdfLiabilityType(tenant.Id).Select(x => new DropDownModel { Id = x.Id, Name = x.Name }).ToList();
            return list;
        }

        public async Task<bool> AddOrUpdateSection3(TenantModel tenant, int userId, QuestionRequestModel model)
        {

            var borrower = await Uow.Repository<Borrower>().Query(x => x.LoanApplication.Id == model.LoanApplicationId && x.Id == model.BorrowerId && x.TenantId == tenant.Id && x.LoanApplication.UserId == userId && x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary)
                 .Include(x => x.BorrowerQuestionResponses)
                 .Include(x => x.BorrowerLiabilities)
                 .ThenInclude(a => a.AddressInfo)
                 .Include(x => x.BorrowerBankRuptcies)
                 .SingleAsync();


            foreach (QuestionModel question in model.Questions)
            {
                LoanApplicationDb.Entity.Models.BorrowerQuestionResponse borrowerresponse = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>()
                                         .Query(query: x => x.TenantId == tenant.Id && x.QuestionId == question.Id && x.BorrowerId == model.BorrowerId && x.Borrower.LoanApplicationId == model.LoanApplicationId && x.Borrower.LoanApplication.UserId == userId)
                                         .SingleOrDefaultAsync();

                if (borrowerresponse != null)
                {
                    borrowerresponse.Description = question.AnswerDetail;
                    borrowerresponse.AnswerText = question.Answer;
                    borrowerresponse.TrackingState = TrackingState.Modified;
                    Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>().Update(borrowerresponse);
                }
                else
                {
                    borrowerresponse = new LoanApplicationDb.Entity.Models.BorrowerQuestionResponse
                    {
                        QuestionId = question.Id,
                        BorrowerId = model.BorrowerId,
                        TenantId = tenant.Id,
                        Description = question.AnswerDetail,
                        AnswerText = question.Answer,
                        TrackingState = TrackingState.Added,
                    };


                    Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerQuestionResponse>().Insert(borrowerresponse);
                }
                switch (question.Id)
                {
                    case 131:


                        int[] array = question.Data?.EnumerateArray().ToArray().Select(x => x.GetInt32()).ToArray() ?? new int[] { };

                        borrower.TrackingState = TrackingState.Modified;

                        for (int i = 0; i < array.Length; i++)
                        {
                            if (!borrower.BorrowerBankRuptcies.Any(y => y.BankRuptcyId == array[i]))
                            {
                                BorrowerBankRuptcy bankRuptcy = new BorrowerBankRuptcy()
                                {
                                    BankRuptcyId = array[i],
                                    TenantId = tenant.Id,
                                    BorrowerId = model.BorrowerId,
                                    TrackingState = TrackingState.Added
                                };

                                borrower.BorrowerBankRuptcies.Add(bankRuptcy);

                            }
                        }

                        foreach (var item in borrower.BorrowerBankRuptcies)
                        {
                            if (!array.Contains(item.BankRuptcyId))
                            {
                                item.TrackingState = TrackingState.Deleted;
                            }
                        }


                        break;
                    case 140:

                        JsonElement[] jarray = question.Data?.EnumerateArray().ToArray() ?? new JsonElement[] { };
                        borrower.TrackingState = TrackingState.Modified;
                        foreach (var item in jarray)
                        {
                            var borrliablity = borrower.BorrowerLiabilities.Where(x => (new List<int> { (int)LiabilityTypeEnum.Alimony, (int)LiabilityTypeEnum.ChildSupport, (int)LiabilityTypeEnum.SeparateMaintenance }).Contains(x.LiabilityTypeId.Value)).FirstOrDefault(x => x.LiabilityTypeId == item.GetProperty("liabilityTypeId").GetInt32());



                            if (borrliablity != null)
                            {
                                borrliablity.TrackingState = TrackingState.Modified;
                                borrliablity.AddressInfo.TrackingState = TrackingState.Modified;
                            }
                            else
                            {

                                borrliablity = new BorrowerLiability();
                                borrliablity.AddressInfo = new AddressInfo();
                                borrliablity.TenantId = tenant.Id;
                                borrliablity.BorrowerId = model.BorrowerId;
                                borrliablity.LiabilityTypeId = item.GetProperty("liabilityTypeId").GetInt32();
                                borrliablity.AddressInfo.TenantId = tenant.Id;


                                borrliablity.TrackingState = TrackingState.Added;
                                borrliablity.AddressInfo.TrackingState = TrackingState.Added;

                                borrower.BorrowerLiabilities.Add(borrliablity);
                            }


                            borrliablity.RemainingMonth = item.GetProperty("remainingMonth").GetInt32();
                            borrliablity.MonthlyPayment = item.GetProperty("monthlyPayment").GetDecimal();
                            borrliablity.AddressInfo.Name = item.GetProperty("name").GetString();



                        }


                        foreach (var item in borrower.BorrowerLiabilities.Where(x => (new List<int> { (int)LiabilityTypeEnum.Alimony, (int)LiabilityTypeEnum.ChildSupport, (int)LiabilityTypeEnum.SeparateMaintenance }).Contains(x.LiabilityTypeId.Value)))
                        {
                            if (!jarray.Select(x => x.GetProperty("liabilityTypeId").GetInt32()).Contains(item.LiabilityTypeId.Value))
                            {
                                item.TrackingState = TrackingState.Deleted;
                                item.AddressInfo.TrackingState = TrackingState.Deleted;
                            }
                        }


                        break;

                }
            }
            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);
            await SaveChangesAsync();
            return true;
        }


        public List<RaceModel> GetAllRaceList(TenantModel tenant)
        {
            var raceList = _dbFunctionService.UdfRace(tenant.Id).Select(x => new DropDownModel { Id = x.Id, Name = x.Name }).ToList();
            var raceDetailList = _dbFunctionService.UdfRaceDetail(tenant.Id).Select(x => new DetailDropDownModel { Id = x.Id, Name = x.Name, ParentId = x.RaceId }).ToList();

            var list = raceList.Select(x => new RaceModel()
            {
                Id = x.Id,
                Name = x.Name,
                RaceDetails = raceDetailList.Select(y => new DetailDropDownModel()
                {
                    Id = y.Id,
                    Name = y.Name,
                    ParentId = y.ParentId
                }).Where(y => y.ParentId == x.Id).ToList()
            }).ToList();

            return list;
        }

        public List<DropDownModel> GetGenderList(TenantModel tenant)
        {
            List<DropDownModel> list = _dbFunctionService.UdfGender(tenant.Id).Select(x => new DropDownModel { Id = x.Id, Name = x.Name }).ToList();
            return list;
        }

        public List<EthnicityModel> GetAllEthnicityList(TenantModel tenant)
        {
            var ethnicityList = _dbFunctionService.UdfEthnicity(tenant.Id).Select(x => new DropDownModel { Id = x.Id, Name = x.Name }).ToList();
            var ethnicityDetailList = _dbFunctionService.UdfEthnicityDetail(tenant.Id).Select(x => new DetailDropDownModel { Id = x.Id, Name = x.Name, ParentId = x.EthnicityId }).ToList();

            var list = ethnicityList.Select(x => new EthnicityModel()
            {
                Id = x.Id,
                Name = x.Name,
                EthnicityDetails = ethnicityDetailList.Select(y => new DetailDropDownModel()
                {
                    Id = y.Id,
                    Name = y.Name,
                    ParentId = y.ParentId
                }).Where(y => y.ParentId == x.Id).ToList()
            }).ToList();

            return list;
        }

        public async Task<DemographicInfoResponseModel> GetDemographicInformation(TenantModel tenant, int userId, int borrowerId, int loanApplicationId)
        {
            var demographicInfoResponseModel = (await Uow.Repository<Borrower>()
                                         .Query(query: x => x.Id == borrowerId && x.TenantId == tenant.Id && x.LoanApplicationId == loanApplicationId && x.LoanApplication.UserId == userId)
                                         .Include(x => x.LoanContact_LoanContactId).ThenInclude(x => x.LoanContactRaceBinders).ThenInclude(x => x.RaceDetail)
                                         .Include(x => x.LoanContact_LoanContactId).ThenInclude(x => x.LoanContactEthnicityBinders).ThenInclude(x => x.EthnicityDetail)
                                         .ToListAsync())
                                         .Select(x => new DemographicInfoResponseModel
                                         {
                                             GenderId = x.LoanContact_LoanContactId.GenderId,
                                             BorrowerId = x.Id,
                                             LoanApplicationId = x.LoanApplicationId.Value,
                                             RaceResponseModel = x.LoanContact_LoanContactId.LoanContactRaceBinders.GroupBy(g => g.RaceId).Select(y => new RaceResponseModel
                                             {
                                                 RaceId = y.Key,
                                                 RaceDetailIds = y.Where(g => g.RaceDetailId != null).Select(g => new RaceDetailResponseModel
                                                 {
                                                     DetailId = g.RaceDetailId,
                                                     IsOther = g.RaceDetail.IsOther,
                                                     OtherRace = g.OtherRace
                                                 }).ToList()

                                             }).ToList(),

                                             EthenticityResponseModel = x.LoanContact_LoanContactId.LoanContactEthnicityBinders.GroupBy(g => g.EthnicityId).Select(y => new EthenticityResponseModel
                                             {
                                                 EthenticityId = y.Key,
                                                 EthnicityDetailIds = y.Where(g => g.EthnicityDetailId != null).Select(g => new EthenticityDetailResponseModel
                                                 {
                                                     DetailId = g.EthnicityDetailId,
                                                     IsOther = g.EthnicityDetail.IsOther,
                                                     OtherEthnicity = g.OtherEthnicity
                                                 }).ToList()

                                             }).ToList()
                                         }).Single();
            return demographicInfoResponseModel;
        }

        public async Task<bool> AddOrUpdateDemogrhphicInfo(TenantModel tenant, int userId, DemographicInfoResponseModel model)
        {
            var demographicInfoResponseModel = await Uow.Repository<Borrower>()
                                         .Query(query: x => x.Id == model.BorrowerId && x.TenantId == tenant.Id && x.LoanApplicationId == model.LoanApplicationId && x.LoanApplication.UserId == userId)
                                         .Include(x => x.LoanContact_LoanContactId).ThenInclude(x => x.LoanContactRaceBinders)
                                         .Include(x => x.LoanContact_LoanContactId).ThenInclude(x => x.LoanContactEthnicityBinders)
                                         .SingleAsync();

            if (demographicInfoResponseModel.LoanContact_LoanContactId.LoanContactRaceBinders.Count > 0)
            {
                foreach (var loanContactRaceBinder in demographicInfoResponseModel.LoanContact_LoanContactId.LoanContactRaceBinders)
                {
                    loanContactRaceBinder.TrackingState = TrackingState.Deleted;
                }
            }

            if (demographicInfoResponseModel.LoanContact_LoanContactId.LoanContactEthnicityBinders.Count > 0)
            {
                foreach (var loanContactEthnicityBinder in demographicInfoResponseModel.LoanContact_LoanContactId.LoanContactEthnicityBinders)
                {
                    loanContactEthnicityBinder.TrackingState = TrackingState.Deleted;
                }
            }

            demographicInfoResponseModel.LoanContact_LoanContactId.GenderId = model.GenderId;


            if (model.RaceResponseModel != null)
            {
                foreach (var race in model.RaceResponseModel)
                {
                    if (race.RaceDetailIds == null || race.RaceDetailIds.Count <= 0)
                    {
                        LoanContactRaceBinder loanContactRaceBinder = new LoanContactRaceBinder();
                        loanContactRaceBinder.RaceId = race.RaceId;
                        loanContactRaceBinder.RaceDetailId = null;
                        loanContactRaceBinder.LoanContactId = demographicInfoResponseModel.LoanContactId.Value;
                        loanContactRaceBinder.TenantId = tenant.Id;
                        loanContactRaceBinder.TrackingState = TrackingState.Added;

                        demographicInfoResponseModel.LoanContact_LoanContactId.LoanContactRaceBinders.Add(loanContactRaceBinder);
                    }
                    else
                    {
                        race.RaceDetailIds.ForEach(x =>
                        {
                            LoanContactRaceBinder loanContactRaceBinder = new LoanContactRaceBinder();
                            loanContactRaceBinder.RaceId = race.RaceId;
                            loanContactRaceBinder.RaceDetailId = x.DetailId;

                            loanContactRaceBinder.LoanContactId = demographicInfoResponseModel.LoanContactId.Value;

                            loanContactRaceBinder.OtherRace = x.OtherRace;
                            loanContactRaceBinder.TenantId = tenant.Id;
                            loanContactRaceBinder.TrackingState = TrackingState.Added;

                            demographicInfoResponseModel.LoanContact_LoanContactId.LoanContactRaceBinders.Add(loanContactRaceBinder);
                        });
                    }

                }
            }


            if (model.EthenticityResponseModel != null)
            {
                foreach (var ethnicity in model.EthenticityResponseModel)
                {
                    if (ethnicity.EthnicityDetailIds == null || ethnicity.EthnicityDetailIds.Count <= 0)
                    {
                        LoanContactEthnicityBinder loanContactEthnicityBinder = new LoanContactEthnicityBinder();
                        loanContactEthnicityBinder.EthnicityId = ethnicity.EthenticityId;
                        loanContactEthnicityBinder.EthnicityDetailId = null;
                        loanContactEthnicityBinder.LoanContactId = demographicInfoResponseModel.LoanContactId.Value;
                        loanContactEthnicityBinder.TenantId = tenant.Id;
                        loanContactEthnicityBinder.TrackingState = TrackingState.Added;

                        demographicInfoResponseModel.LoanContact_LoanContactId.LoanContactEthnicityBinders.Add(loanContactEthnicityBinder);
                    }
                    else
                    {
                        ethnicity.EthnicityDetailIds.ForEach(x =>
                        {
                            LoanContactEthnicityBinder loanContactEthnicityBinder = new LoanContactEthnicityBinder();
                            loanContactEthnicityBinder.EthnicityId = ethnicity.EthenticityId;
                            loanContactEthnicityBinder.EthnicityDetailId = x.DetailId;
                            loanContactEthnicityBinder.LoanContactId = demographicInfoResponseModel.LoanContactId.Value;
                            loanContactEthnicityBinder.OtherEthnicity = x.OtherEthnicity;
                            loanContactEthnicityBinder.TenantId = tenant.Id;
                            loanContactEthnicityBinder.TrackingState = TrackingState.Added;

                            demographicInfoResponseModel.LoanContact_LoanContactId.LoanContactEthnicityBinders.Add(loanContactEthnicityBinder);
                        });
                    }

                }
            }


            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();


            return true;
        }

        public async Task<PrimaryBorrowerSubjectPropertyModel> CheckPrimaryBorrowerSubjectProperty(TenantModel tenant, int userId, int borrowerId, int loanApplicationId)
        {
            var borrower = await Uow.Repository<Borrower>().Query(x => x.LoanApplication.Id == loanApplicationId && x.Id == borrowerId && x.TenantId == tenant.Id && x.LoanApplication.UserId == userId && x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary)
                   .Include(x => x.LoanApplication).ThenInclude(x => x.PropertyInfo)
                   .Include(x => x.BorrowerProperties).ThenInclude(x => x.PropertyInfo)
                   .SingleAsync();
            return new PrimaryBorrowerSubjectPropertyModel
            {
                isSubjectPropertyPrimaryResidence = borrower.LoanApplication.PropertyInfo.PropertyUsageId == (int)PropertyUsageEnum.IWillLiveHerePrimaryResidence,
                isReoDefined = borrower.BorrowerProperties.Count() > 0,
                PropertyUsageId = borrower.BorrowerProperties.OrderBy(x => x.Id).FirstOrDefault()?.PropertyInfo?.PropertyUsageId
            };
        }

        public async Task<SecondaryBorrowerSubjectPropertyModel> CheckSecondaryBorrowerSubjectProperty(TenantModel tenant, int userId, int borrowerId, int loanApplicationId)
        {
            var borrower = await Uow.Repository<Borrower>().Query(x => x.LoanApplication.Id == loanApplicationId && x.Id == borrowerId && x.TenantId == tenant.Id && x.LoanApplication.UserId == userId && x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary)
                   .Include(x => x.BorrowerProperties).ThenInclude(x => x.PropertyInfo)
                   .SingleAsync();
            return new SecondaryBorrowerSubjectPropertyModel
            {
                willLiveInSubjectProperty = borrower.WillLiveInSubjectProperty == true,
                isReoDefined = borrower.BorrowerProperties.Count() > 0,
                PropertyUsageId = borrower.BorrowerProperties.OrderBy(x => x.Id).FirstOrDefault()?.PropertyInfo?.PropertyUsageId
            };
        }

        public async Task<object> GetDemographicInformationReview(TenantModel tenant, int userId, int loanApplicationId)
        {
            var demographicInfoResponseModel = (await Uow.Repository<Borrower>()
                                      .Query(query: x => x.TenantId == tenant.Id && x.LoanApplicationId == loanApplicationId && x.LoanApplication.UserId == userId)
                                      .Include(x => x.LoanContact_LoanContactId).ThenInclude(x => x.LoanContactRaceBinders).ThenInclude(x => x.RaceDetail)
                                      .Include(x => x.LoanContact_LoanContactId).ThenInclude(x => x.LoanContactEthnicityBinders).ThenInclude(x => x.EthnicityDetail)
                                      .Include(x => x.LoanContact_LoanContactId).ThenInclude(x=>x.Gender)
                                      .Include(x => x.LoanContact_LoanContactId).ThenInclude(x => x.LoanContactRaceBinders).ThenInclude(x => x.Race)
                                      .Include(x => x.LoanContact_LoanContactId).ThenInclude(x => x.LoanContactEthnicityBinders).ThenInclude(x => x.Ethnicity)

                                      .ToListAsync())
                                      .Select(x => new DemographicReviewResponseModel
                                      {
                                          OwnTypeId = x.OwnTypeId.Value,
                                          FirstName = x.LoanContact_LoanContactId.FirstName,
                                          LastName = x.LoanContact_LoanContactId.LastName,
                                          GenderId = x.LoanContact_LoanContactId.GenderId,
                                          Gender = x.LoanContact_LoanContactId.Gender?.Name,
                                          BorrowerId = x.Id,
                                          LoanApplicationId = x.LoanApplicationId.Value,
                                          RaceResponseModel = x.LoanContact_LoanContactId.LoanContactRaceBinders.GroupBy(g => new {g.RaceId,g.Race.Name } ).Select(y => new RaceReviewResponseModel
                                          {
                                              RaceId = y.Key.RaceId,
                                              Race  = y.Key.Name,
                                              RaceDetailIds = y.Where(g => g.RaceDetailId != null).Select(g => new RaceDetailReviewResponseModel
                                              {
                                                  DetailId = g.RaceDetailId,
                                                  RaceDetail = g.RaceDetail?.Name,
                                                  IsOther = g.RaceDetail.IsOther,
                                                  OtherRace = g.OtherRace
                                              }).ToList()

                                          }).ToList(),

                                          EthenticityResponseModel = x.LoanContact_LoanContactId.LoanContactEthnicityBinders.GroupBy(g => new {g.EthnicityId,g.Ethnicity.Name }).Select(y => new EthenticityReviewResponseModel
                                          {
                                              EthenticityId = y.Key.EthnicityId,
                                              Ethenticity = y.Key.Name,
                                              EthnicityDetailIds = y.Where(g => g.EthnicityDetailId != null).Select(g => new EthenticityDetailReviewResponseModel
                                              {
                                                  DetailId = g.EthnicityDetailId,
                                                  EthenticityDetail = g.EthnicityDetail?.Name,
                                                  IsOther = g.EthnicityDetail.IsOther,
                                                  OtherEthnicity = g.OtherEthnicity
                                              }).ToList()

                                          }).ToList()
                                      }).ToList();
            return demographicInfoResponseModel;
        }

        public async Task<object> GetGovernmentQuestionReview(TenantModel tenant, int userId, int loanApplicationId)
        {

            var borrowers = await Uow.Repository<Borrower>().Query(x => x.LoanApplication.Id == loanApplicationId  && x.TenantId == tenant.Id && x.LoanApplication.UserId == userId)
              .Include(x => x.BorrowerQuestionResponses)
              .Include(x => x.LoanContact_LoanContactId)
                  .Include(x => x.BorrowerLiabilities)
                  .ThenInclude(a => a.AddressInfo)
                  .Include(x => x.BorrowerBankRuptcies)
                    .Include(x => x.BorrowerLiabilities)
                  .ThenInclude(a => a.LiabilityType)
                  .ToListAsync();

            List<List<QuestionReviewModel>> result = new List<List<QuestionReviewModel>>();
            foreach (var borrower in borrowers)
            {
                var questions = await _dbFunctionService.UdfQuestion(tenant.Id).Where(x => x.IsActive && x.QuestionSectionId == (int)Model.QuestionSection.Section1 || x.QuestionSectionId == (int)Model.QuestionSection.Section2 || x.QuestionSectionId == (int)Model.QuestionSection.Section3).OrderBy(x => x.Id)
           .ToListAsync();



                result.Add(questions.Select(x =>
                {

                    var question = new QuestionReviewModel()
                    {
                        Id = x.Id,
                        Question = borrower.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary ? x.PrimaryBorrowerText : x.CoBorrowerText.Replace("[Co-Applicant First Name]", borrower.LoanContact_LoanContactId.FirstName).Replace("[Co-Applicant Last Name]", borrower.LoanContact_LoanContactId.LastName),
                        Answer = borrower.BorrowerQuestionResponses.FirstOrDefault(y => y.QuestionId == x.Id)?.AnswerText,
                        AnswerDetail = borrower.BorrowerQuestionResponses.FirstOrDefault(y => y.QuestionId == x.Id)?.Description,
                        SelectionOptionId = borrower.BorrowerQuestionResponses.FirstOrDefault(y => y.QuestionId == x.Id)?.SelectionOptionId,
                        OwnTypeId = borrower.OwnTypeId.Value,
                        FirstName = borrower.LoanContact_LoanContactId.FirstName,
                        LastName = borrower.LoanContact_LoanContactId.LastName,
                        Data = null
                    };
                    switch (x.Id)
                    {
                        case 21:
                            question.Data = ObjectToJosonElement(new
                            {
                                selectionOptionText = GetNameBySelectedOption(tenant.Id, x.Id,question.SelectionOptionId)
                            });
                            break;
                        case 22:
                            question.Data = ObjectToJosonElement(new
                            {
                                selectionOptionText = GetNameBySelectedOption(tenant.Id, x.Id, question.SelectionOptionId)
                            });
                            break;

                        case 131:

                            int[] lstarr = borrower.BorrowerBankRuptcies.Select(y => y.BankRuptcyId).ToArray();
                            string strlist = GetNameBySelectedOption(tenant.Id, x.Id, question.SelectionOptionId, lstarr);
                            string[] listbankruptcies = strlist.Split(",");

                            List<object> lstobj = new List<object>();

                            foreach (string item in listbankruptcies)
                            {
                                lstobj.Add( item );
                            }

                            question.Data = ObjectToJosonElement(lstobj);
                            break;

                        case 140:
                            question.Data = ObjectToJosonElement(borrower.BorrowerLiabilities.Where(x => (new List<int> { (int)LiabilityTypeEnum.Alimony, (int)LiabilityTypeEnum.ChildSupport, (int)LiabilityTypeEnum.SeparateMaintenance }).Contains(x.LiabilityTypeId.Value)).Select(y => new { liabilityTypeId = y.LiabilityTypeId, liabilityName = y.LiabilityType.Name, remainingMonth = y.RemainingMonth, monthlyPayment = y.MonthlyPayment, name = y.AddressInfo.Name }).ToArray());
                            break;

                    }
                    return question;
                }).ToList());

            }


            return result;
        }

        private string GetNameBySelectedOption(int tenantid, int questionid,int? SelectionOptionId=null, int[] selectedoptions = null)
        {
            string result = string.Empty;
            string unit = string.Empty;
            List<DropDownModel> list = new List<DropDownModel>();

            switch (questionid)
            {
                case 21:
                    list = _dbFunctionService.UdfPropertyUsage(tenantid).Select(x => new DropDownModel { Id = x.Id, Name = x.Name }).ToList();
                    result = list.Find(x => x.Id == SelectionOptionId)?.Name;
                    break;
                case 22:
                    list = _dbFunctionService.UdfTitleHeldWith(tenantid).Select(x => new DropDownModel { Id = x.Id, Name = x.Name }).ToList();
                    result = list.Find(x => x.Id == SelectionOptionId)?.Name;
                    break;

                case 131:

                    var bankruptcoes = Uow.Repository<BankRuptcy>().Query().ToList();

                    foreach (var item in selectedoptions)
                    {
                        unit = bankruptcoes.SingleOrDefault(x => x.Id == item)?.Name;

                        if (!string.IsNullOrEmpty(unit))
                            result += unit + ",";
                    }

                    result = result.TrimEnd(',');

                    break;


            }

            return result;
        }

    }
}

