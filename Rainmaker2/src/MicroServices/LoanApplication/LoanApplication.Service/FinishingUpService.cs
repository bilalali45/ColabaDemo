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
using System.Threading;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;

namespace LoanApplication.Service
{
    public class FinishingUpService : ServiceBase<LoanApplicationContext, BorrowerResidence>, IFinishingUpService
    {
        public FinishingUpService(IUnitOfWork<LoanApplicationContext> previousUow,
                           IServiceProvider services) : base(previousUow: previousUow,
                                                             services: services)
        {
        }
        // page 1, page 4
        public async Task<BorrowerPrimaryAddressDetailModel> GetBorrowerPrimaryAddressDetail(TenantModel tenant, int userId, int loanApplicationId, int borrowerId)
        {
            BorrowerPrimaryAddressDetailModel primaryBorrowerDetail = await Uow.Repository<Borrower>().Query(x => x.LoanApplicationId == loanApplicationId && x.LoanApplication.UserId == userId && x.TenantId == tenant.Id && x.Id == borrowerId)
                    .Include(x => x.LoanApplication)
                    .Include(x => x.BorrowerResidences).ThenInclude(x => x.LoanAddress)
                    .Select(x => x.BorrowerResidences.SingleOrDefault(y => y.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary))
                     .Select(x => new BorrowerPrimaryAddressDetailModel
                     {
                         BorrowerResidenceId = x.Id,
                         Address = new GenericAddressModel
                         {
                             City = x.LoanAddress.CityName,
                             CountryId = x.LoanAddress.CountryId,
                             StateId = x.LoanAddress.StateId,
                             Street = x.LoanAddress.StreetAddress,
                             Unit = x.LoanAddress.UnitNo,
                             ZipCode = x.LoanAddress.ZipCode,
                             CountryName = x.LoanAddress.CountryName,
                             StateName = x.LoanAddress.StateName
                         }
                     })
                    .SingleAsync();
            return primaryBorrowerDetail;
        }
        //page 1, page 3, page 4
        public async Task<int> AddOrUpdateBorrowerCurrentResidenceMoveInDate(TenantModel tenant, int userId, CurrentResidenceRequestModel model)
        {
            BorrowerResidence borrowerResidence = await Uow.Repository<BorrowerResidence>().Query(x => x.Borrower.LoanApplicationId == model.LoanApplicationId && x.Borrower.LoanApplication.UserId == userId && x.TenantId == tenant.Id && x.Id == model.BorrowerResidenceId)
                                                            .SingleAsync();

            borrowerResidence.FromDate = model.FromDate;
            borrowerResidence.TrackingState = TrackingState.Modified;

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();
            return borrowerResidence.Id;

        }
        // page 1
        public async Task<CurrentResidenceResponseModel> GetBorrowerCurrentResidenceMoveInDate(TenantModel tenant, int userId, int borrowerResidenceId, int loanApplicationId)
        {
            var borrowerResidence = await Uow.Repository<BorrowerResidence>().Query(x => x.Borrower.LoanApplicationId == loanApplicationId && x.Borrower.LoanApplication.UserId == userId && x.TenantId == tenant.Id && x.Id == borrowerResidenceId)
                    .Select(x => new CurrentResidenceResponseModel
                    {
                        Id = x.Id,
                        FromDate = x.FromDate,
                        LoanApplicationId = x.Borrower.LoanApplicationId.Value
                    })
                    .SingleAsync();
            return borrowerResidence;
        }
        // page 3
        public async Task<CoApplicantDetails> GetCoborrowerResidence(TenantModel tenant, int userId, int loanApplicationId)
        {
            var primaryBorrowerDetail = await Uow.Repository<BorrowerResidence>().Query(x => x.Borrower.LoanApplicationId == loanApplicationId && x.Borrower.LoanApplication.UserId == userId && x.TenantId == tenant.Id && x.Borrower.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary && x.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary)
                .Include(x => x.LoanAddress)
                .SingleAsync();

            var secondaryBorrowerDetail = await Uow.Repository<BorrowerResidence>().Query(x => x.Borrower.LoanApplicationId == loanApplicationId && x.Borrower.LoanApplication.UserId == userId && x.TenantId == tenant.Id && x.Borrower.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary && x.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary)
                            .Select(x => new SecondaryBorrowerDetail
                            {
                                BorrowerId = x.BorrowerId.Value,
                                BorrowerResidenceId = x.Id,
                                FromDate = x.FromDate,
                                FirstName = x.Borrower.LoanContact_LoanContactId.FirstName,
                                LastName = x.Borrower.LoanContact_LoanContactId.LastName,
                                AddressModel = new GenericAddressModel
                                {
                                    City = x.LoanAddress.CityName,
                                    CountryId = x.LoanAddress.CountryId,
                                    StateId = x.LoanAddress.StateId,
                                    Street = x.LoanAddress.StreetAddress,
                                    Unit = x.LoanAddress.UnitNo,
                                    ZipCode = x.LoanAddress.ZipCode,
                                    CountryName = x.LoanAddress.CountryName,
                                    StateName = x.LoanAddress.StateName
                                }
                            }
                            ).ToListAsync();

            bool isSameAsPrimaryAddress(GenericAddressModel address)
            {
                if (address.City == primaryBorrowerDetail.LoanAddress.CityName && address.CountryId == primaryBorrowerDetail.LoanAddress.CountryId
                    && address.CountryName == primaryBorrowerDetail.LoanAddress.CountryName && address.StateId == primaryBorrowerDetail.LoanAddress.StateId
                    && address.StateName == primaryBorrowerDetail.LoanAddress.StateName && address.Street == primaryBorrowerDetail.LoanAddress.StreetAddress
                    && address.ZipCode == primaryBorrowerDetail.LoanAddress.ZipCode && address.Unit == primaryBorrowerDetail.LoanAddress.UnitNo
                    )
                    return true;
                else
                    return false;

            }

            secondaryBorrowerDetail.ForEach(x =>
            {
                x.IsSameAsPrimaryAddress = isSameAsPrimaryAddress(x.AddressModel);
            });

            return new CoApplicantDetails
            {
                PrimaryBorrowerFromDate = primaryBorrowerDetail.FromDate,
                SecondaryBorrowerDetailList = secondaryBorrowerDetail
            };
        }
        // page 6
        public async Task<ResidenceHistoryModel> GetResidenceHistory(TenantModel tenant, int userId, int loanApplicationId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == loanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.Borrowers).ThenInclude(b => b.LoanContact_LoanContactId)
                .Include(x => x.Borrowers).ThenInclude(b => b.BorrowerResidences)
                .OrderBy(x => x.Id)
                .SingleAsync();

            ResidenceHistoryModel model = new ResidenceHistoryModel()
            {
                LoanApplicationId = loanApplicationId
            };

            List<BorrowerResidenceHistory> borrowerHistory = new List<BorrowerResidenceHistory>();
            DateTime yearsBack = DateTime.UtcNow.AddYears(-2);
            foreach (var borrower in loanApplication.Borrowers)
            {
                if (borrower.BorrowerResidences == null || borrower.BorrowerResidences.Count == 0)
                {
                    borrowerHistory.Add(new BorrowerResidenceHistory()
                    {
                        FirstName = borrower.LoanContact_LoanContactId.FirstName,
                        LastName = borrower.LoanContact_LoanContactId.LastName,
                        BorrowerId = borrower.Id
                    });
                }
                else
                {
                    var historyExists = borrower.BorrowerResidences.Any(ii => ii.FromDate <= yearsBack);
                    if (!historyExists)
                    {
                        BorrowerResidenceHistory borrowerToAdd = new BorrowerResidenceHistory()
                        {
                            FirstName = borrower.LoanContact_LoanContactId.FirstName,
                            LastName = borrower.LoanContact_LoanContactId.LastName,
                            BorrowerId = borrower.Id
                        };
                        borrowerHistory.Add(borrowerToAdd);
                    }
                }
            }

            model.BorrowerResidenceHistory = borrowerHistory;
            model.RequiresHistory = borrowerHistory.Count > 0;
            return model;
        }
        // page 7
        public async Task<List<BorrowersDetail>> GetResidenceDetails(TenantModel tenant, int userId, int borrowerId, int loanApplicationId)
        {
            var borrowersDetail = await Uow.Repository<BorrowerResidence>().Query(x => x.BorrowerId == borrowerId && x.Borrower.LoanApplicationId == loanApplicationId && x.Borrower.LoanApplication.UserId == userId && x.TenantId == tenant.Id)
                            .Select(x => new BorrowersDetail
                            {
                                BorrowerId = x.BorrowerId.Value,
                                BorrowerResidenceId = x.Id,
                                TypeId = x.TypeId.Value,
                                FirstName = x.Borrower.LoanContact_LoanContactId.FirstName,
                                LastName = x.Borrower.LoanContact_LoanContactId.LastName,
                                FromDate = x.FromDate,
                                ToDate = x.ToDate,
                                AddressModel = new GenericAddressModel
                                {
                                    City = x.LoanAddress.CityName,
                                    CountryId = x.LoanAddress.CountryId,
                                    StateId = x.LoanAddress.StateId,
                                    Street = x.LoanAddress.StreetAddress,
                                    Unit = x.LoanAddress.UnitNo,
                                    ZipCode = x.LoanAddress.ZipCode,
                                    CountryName = x.LoanAddress.CountryName,
                                    StateName = x.LoanAddress.StateName
                                }
                            }
                            )
                            .OrderByDescending(x => x.FromDate)
                            .ToListAsync();

            return borrowersDetail;

        }
        // page 11
        public async Task<int> AddOrUpdateBorrowerCitizenship(TenantModel tenant, int userId, BorrowerCitizenshipRequestModel model)
        {
            var borrower = await Uow.Repository<Borrower>().Query(x => x.Id == model.BorrowerId && x.LoanApplicationId == model.LoanApplicationId && x.LoanApplication.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.LoanApplication)
                .Include(x => x.LoanContact_LoanContactId)
                .SingleAsync();

            borrower.LoanContact_LoanContactId.ResidencyTypeId = model.ResidencyTypeId;
            borrower.LoanContact_LoanContactId.ResidencyStateId = model.ResidencyStateId;
            borrower.LoanContact_LoanContactId.ResidencyStateExplanation = model.ResidencyStateExplanation;
            borrower.LoanContact_LoanContactId.TrackingState = TrackingState.Modified;

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();
            return borrower.Id;

        }
        // page 11
        public async Task<BorrowerCitizenshipResponseModel> GetBorrowerCitizenship(TenantModel tenant, int userId, int borrowerId, int loanApplicationId)
        {
            var borrowerCitizenshipResponseModel = await Uow.Repository<Borrower>().Query(x => x.Id == borrowerId && x.LoanApplicationId == loanApplicationId && x.LoanApplication.UserId == userId && x.TenantId == tenant.Id)
                            .Select(x => new BorrowerCitizenshipResponseModel
                            {
                                BorrowerId = x.Id,
                                LoanApplicationId = x.LoanApplication.Id,
                                ResidencyStateId = x.LoanContact_LoanContactId.ResidencyStateId,
                                ResidencyTypeId = x.LoanContact_LoanContactId.ResidencyTypeId,
                                ResidencyStateExplanation = x.LoanContact_LoanContactId.ResidencyStateExplanation
                            })
                            .SingleAsync();

            return borrowerCitizenshipResponseModel;
        }
        // page 8,9,10
        public async Task<int> AddOrUpdateSecondaryAddress(TenantModel tenant, int userId, BorrowerSecondaryAddressRequestModel model)
        {
            BorrowerResidence borrowerResidence = null;
            if (model.BorrowerResidenceId.HasValue)
            {
                borrowerResidence = await Uow.Repository<BorrowerResidence>().Query(x => x.Id == model.BorrowerResidenceId && x.BorrowerId == model.BorrowerId && x.Borrower.LoanApplicationId == model.LoanApplicationId && x.Borrower.LoanApplication.UserId == userId && x.TenantId == tenant.Id && x.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary)
                    .Include(x => x.LoanAddress)
                    .SingleAsync();
                Uow.Repository<BorrowerResidence>().Update(borrowerResidence);
            }
            else
            {
                borrowerResidence = new BorrowerResidence
                {
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    TenantId = tenant.Id,
                    BorrowerId = model.BorrowerId,
                    LoanAddress = new LoanApplicationDb.Entity.Models.AddressInfo
                    {
                        IsDeleted = false,
                        TenantId = tenant.Id,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    },
                    TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary
                };
                Uow.Repository<BorrowerResidence>().Insert(borrowerResidence);
            }

            borrowerResidence.FromDate = model.FromDate;
            borrowerResidence.ToDate = model.ToDate;
            borrowerResidence.OwnershipTypeId = model.HousingStatusId;
            borrowerResidence.MonthlyRent = model.MonthlyRent;

            borrowerResidence.LoanAddress.CityName = model.AddressModel.City;
            borrowerResidence.LoanAddress.CountryId = model.AddressModel.CountryId;
            borrowerResidence.LoanAddress.StateId = model.AddressModel.StateId;
            borrowerResidence.LoanAddress.StreetAddress = model.AddressModel.Street;
            borrowerResidence.LoanAddress.UnitNo = model.AddressModel.Unit;
            borrowerResidence.LoanAddress.ZipCode = model.AddressModel.ZipCode;
            borrowerResidence.LoanAddress.CountryName = model.AddressModel.CountryName;
            borrowerResidence.LoanAddress.StateName = model.AddressModel.StateName;

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(application);

            await Uow.SaveChangesAsync();
            return borrowerResidence.Id;
        }
        // page 8,9,10
        public async Task<BorrowerSecondaryAddressResponseModel> GetSecondaryAddress(TenantModel tenant, int userId, int borrowerResidenceId, int loanApplicationId)
        {
            var borrowerResidence = await Uow.Repository<BorrowerResidence>().Query(x => x.Id == borrowerResidenceId && x.Borrower.LoanApplicationId == loanApplicationId
                                    && x.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary
                                    && x.Borrower.LoanApplication.UserId == userId && x.TenantId == tenant.Id)
                            .Select(x => new BorrowerSecondaryAddressResponseModel
                            {
                                BorrowerId = x.BorrowerId.Value,
                                LoanApplicationId = x.Borrower.LoanApplicationId.Value,
                                FromDate = x.FromDate,
                                ToDate = x.ToDate,
                                HousingStatusId = x.OwnershipTypeId.Value,
                                MonthlyRent = x.MonthlyRent,
                                AddressModel = new GenericAddressModel
                                {
                                    City = x.LoanAddress.CityName,
                                    CountryId = x.LoanAddress.CountryId,
                                    StateId = x.LoanAddress.StateId,
                                    Street = x.LoanAddress.StreetAddress,
                                    Unit = x.LoanAddress.UnitNo,
                                    ZipCode = x.LoanAddress.ZipCode,
                                    CountryName = x.LoanAddress.CountryName,
                                    StateName = x.LoanAddress.StateName
                                }
                            }).SingleAsync();

            return borrowerResidence;
        }
        //page 12,13
        public async Task<int> AddOrUpdateDependentinfo(TenantModel tenant, int userId, DependentModel model)
        {
            LoanApplicationDb.Entity.Models.Borrower borrower = await Uow.Repository<LoanApplicationDb.Entity.Models.Borrower>()
                    .Query(query: x => x.TenantId == tenant.Id && x.Id == model.BorrowerId && x.LoanApplication.UserId == userId
                    && x.LoanApplicationId == model.LoanApplicationId && x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary)
                    .SingleAsync();

            borrower.NoOfDependent = model.DependentCount;
            borrower.DependentAge = model.DependentAges;
            borrower.TrackingState = TrackingState.Modified;

            Uow.Repository<LoanApplicationDb.Entity.Models.Borrower>().Update(borrower);

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();
            return borrower.Id;
        }
        // page 12,13
        public async Task<DependentModel> GetDependentinfo(TenantModel tenant, int userId, int loanApplicationId, int borrowerId)
        {
            return (await Uow.Repository<LoanApplicationDb.Entity.Models.Borrower>()
                                      .Query(query: x => x.TenantId == tenant.Id && x.Id == borrowerId && x.LoanApplication.UserId == userId
                                       && x.LoanApplicationId == loanApplicationId && x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary)
                                      .Select(x => new DependentModel()
                                      {
                                          DependentAges = x.DependentAge,
                                          DependentCount = x.NoOfDependent,
                                          BorrowerId = x.Id,
                                          LoanApplicationId = x.LoanApplicationId,
                                      }).SingleAsync());
        }
        // page 7
        public async Task DeleteSecondaryAddress(TenantModel tenant, int userId, int borrowerResidenceId, int loanApplicationId)
        {
            var borrowerResidence = await Uow.Repository<BorrowerResidence>().Query(x => x.Id == borrowerResidenceId && x.Borrower.LoanApplicationId == loanApplicationId
                                   && x.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary
                                   && x.Borrower.LoanApplication.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.LoanAddress)
                .SingleAsync();
            borrowerResidence.TrackingState = TrackingState.Deleted;
            borrowerResidence.LoanAddress.TrackingState = TrackingState.Deleted;
            await Uow.SaveChangesAsync();
        }
        // page 14
        public async Task<List<SpouseInfo>> GetAllSpouseInfo(TenantModel tenant, int userId, int loanApplicationId)
        {
            var primaryBorrower = await Uow.Repository<Borrower>().Query(x => x.LoanApplicationId == loanApplicationId && x.LoanApplication.UserId == userId && x.TenantId == tenant.Id
                                        && x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary)
                                        .Include(x => x.SpouseLoanContact)
                                        .Include(x => x.LoanContact_LoanContactId)
                                        .SingleAsync();
            var secondaryBorrowers = await Uow.Repository<Borrower>().Query(x => x.LoanApplicationId == loanApplicationId && x.LoanApplication.UserId == userId && x.TenantId == tenant.Id
                                        && x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary)
                                        .Include(x => x.SpouseLoanContact)
                                        .Include(x => x.LoanContact_LoanContactId)
                                        .ToListAsync();
            var list = secondaryBorrowers.Where(x => x.SpouseLoanContactId != primaryBorrower.LoanContactId).ToList();
            if (!secondaryBorrowers.Any(x => primaryBorrower.SpouseLoanContactId == x.LoanContactId))
            {
                list.Add(primaryBorrower);
            }
            var spouseInfo = list.OrderBy(x => x.Id)
                            .Select(x => new SpouseInfo
                            {
                                BorrowerId = x.Id,
                                FirstName = x.LoanContact_LoanContactId.FirstName,
                                LastName = x.LoanContact_LoanContactId.LastName,
                                SpouseLoanContactId = x.SpouseLoanContactId
                            })
                            .ToList();
            return spouseInfo;
        }
        // page 14
        public async Task<int> AddOrUpdateSpouseInfo(TenantModel tenant, int userId, SpouseInfoRequestModel model)
        {
            Borrower borrower = default(Borrower);
            if (model.SpouseLoanContactId.HasValue)
            {
                borrower = await Uow.Repository<Borrower>().Query(query: x => x.TenantId == tenant.Id && x.Id == model.BorrowerId && x.LoanApplication.UserId == userId
                                    && x.LoanApplicationId == model.LoanApplicationId
                                    && x.SpouseLoanContactId == model.SpouseLoanContactId)
                                .Include(x => x.SpouseLoanContact)
                                .SingleAsync();
                Uow.Repository<Borrower>().Update(borrower);
                borrower.SpouseLoanContact.TrackingState = TrackingState.Modified;
            }
            else
            {
                borrower = await Uow.Repository<Borrower>().Query(query: x => x.TenantId == tenant.Id && x.Id == model.BorrowerId && x.LoanApplication.UserId == userId
                                    && x.LoanApplicationId == model.LoanApplicationId)
                                .Include(x => x.SpouseLoanContact)
                                .SingleAsync();

                borrower.SpouseLoanContact = new LoanContact
                {
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    TenantId = tenant.Id,
                };
                Uow.Repository<Borrower>().Update(borrower);
            }

            borrower.SpouseLoanContact.FirstName = model.FirstName;
            borrower.SpouseLoanContact.LastName = model.LastName;
            borrower.SpouseLoanContact.MiddleName = model.MiddleName;
            borrower.SpouseLoanContact.Suffix = model.Suffix;


            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();
            return borrower.SpouseLoanContact.Id;
        }
        // page 14
        public async Task<SpouseInfoResponseModel> GetSpouseInfo(TenantModel tenant, int userId, int borrowerId, int spouseLoanContactId, int loanApplicationId)
        {
            var spouseInfo = await Uow.Repository<Borrower>().Query(x => x.Id == borrowerId && x.LoanApplicationId == loanApplicationId && x.LoanApplication.UserId == userId
                             && x.TenantId == tenant.Id && x.SpouseLoanContactId == spouseLoanContactId)
                            .Select(x => new SpouseInfoResponseModel
                            {
                                BorrowerId = x.Id,
                                FirstName = x.SpouseLoanContact.FirstName,
                                LastName = x.SpouseLoanContact.LastName,
                                MiddleName = x.SpouseLoanContact.MiddleName,
                                Suffix = x.SpouseLoanContact.Suffix,
                                SpouseLoanContactId = x.SpouseLoanContactId.Value
                            })
                            .SingleAsync();

            return spouseInfo;
        }
        // page 15
        public async Task<List<ReviewSpouseInfo>> ReviewBorrowerAndAllCoBorrowersInfo(TenantModel tenant, int userId, int loanApplicationId)
        {
            var borrowerAndAllCoBorrowersInfo = await Uow.Repository<Borrower>().Query(x => x.LoanApplicationId == loanApplicationId && x.LoanApplication.UserId == userId && x.TenantId == tenant.Id)
                                .Select(x => new ReviewSpouseInfo
                                    {
                                        BorrowerId = x.Id,
                                        FirstName = x.LoanContact_LoanContactId.FirstName,
                                        LastName = x.LoanContact_LoanContactId.LastName,
                                        OwnTypeId = x.OwnTypeId.Value,
                                        ResidencyName = x.LoanContact_LoanContactId.ResidencyType.Name,
                                        DependentAge = x.DependentAge,
 
                                        AddressModel = x.BorrowerResidences.Select(y => new ReviewAddressModel()
                                        {
                                            BorrowerResidenceId = y.Id,
                                            CountryId = y.LoanAddress.CountryId,
                                            StateId = y.LoanAddress.StateId,
                                            TypeId = y.TypeId.Value,
                                            Street = y.LoanAddress.StreetAddress,
                                            Unit = y.LoanAddress.UnitNo,
                                            ZipCode = y.LoanAddress.ZipCode,

                                            City = y.LoanAddress.CityName,
                                            CountryName = y.LoanAddress.CountryName,
                                            StateName = y.LoanAddress.StateName
                                        }).OrderBy(y => y.TypeId).ToList()
                                    })
                                 .ToListAsync();

            return borrowerAndAllCoBorrowersInfo;
        }
    }
}
