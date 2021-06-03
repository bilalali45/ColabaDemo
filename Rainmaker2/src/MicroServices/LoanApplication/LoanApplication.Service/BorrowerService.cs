using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions.ExtensionClasses;
using LoanApplication.Model;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using URF.Core.Abstractions;

namespace LoanApplication.Service
{
    public interface IBorrowerService : IServiceBase<Borrower>
    {
        //Task<int> RemoveRelationshipIfRequired(TenantModel tenant, MaritalStatus selectedMaritalStatus, int userId,
        //    int borrowerId, string state);
        Task<bool> AddOrUpdateMaritalStatus(TenantModel tenant, int userId, BorrowerMaritalStatusModel model);
        Task<GetBorrowerMaritalStatusModel> GetMaritalStatus(TenantModel tenant, int userId, int loanApplicationId, int borrowerId);
        Task<BorrowerModel> GetBorrowerInfo(TenantModel tenant, int userId, int loanApplicationId, int borrowerId);
        Task<AddressModel> GetBorrowerAddress(TenantModel tenant, int userId, int loanApplicationId, int borrowerId);
        Task<BorrowerModel> PopulatePrimaryBorrowerInfo(TenantModel tenant, int userId);
        Task<AddBorrowerModel> AddOrUpdateBorrowerInfo(TenantModel tenant, int userId, BorrowerInfoModel model);
        Task<int> AddOrUpdateAddress(TenantModel tenant, int userId, BorrowerAddressModel model);
        Task<bool> DeleteSecondaryBorrower(TenantModel tenant, int userId, int loanApplicationId, int borrowerId, string state);
        Task<List<GetBorrowerModel>> GetAllBorrower(TenantModel tenant, int userId, int loanApplicationId);
        Task<bool> IsRelationAlreadyMapped(TenantModel tenant, int userId, int loanApplicationId, int borrowerId);
        Task<BorrowersFirstReviewModel> GetBorrowersForFirstReview(TenantModel tenant, int userId, int? loanApplicationId);
    }
    public class BorrowerService : ServiceBase<LoanApplicationContext, Borrower>, IBorrowerService
    {
        private readonly IUnitOfWork<TenantConfigContext> _tenantConfigUow;
        private readonly IMyPropertyService _myPropertyService;
        public BorrowerService(IUnitOfWork<TenantConfigContext> tenantConfigUow, IUnitOfWork<LoanApplicationContext> previousUow, IServiceProvider services, IMyPropertyService myPropertyService) : base(previousUow, services)
        {
            _tenantConfigUow = tenantConfigUow;
            _myPropertyService = myPropertyService;
        }
        public async Task<bool> IsRelationAlreadyMapped(TenantModel tenant, int userId, int loanApplicationId, int borrowerId)
        {
            var anotherCoBorrowerMarkedAsSpouse = await Repository.Query(borrower => borrower.LoanApplicationId == loanApplicationId &&
                    borrower.LoanApplication.UserId == userId &&
                    borrower.TenantId == tenant.Id &&
                    borrower.Id != borrowerId &&
                    borrower.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary)
                .AnyAsync(CoBorrower => CoBorrower.RelationWithPrimaryId ==
                                        ((byte)FamilyRelationTypeEnum.Spouse));
            return anotherCoBorrowerMarkedAsSpouse;
        }
        public async Task<List<GetBorrowerModel>> GetAllBorrower(TenantModel tenant, int userId, int loanApplicationId)
        {
            return await Repository.Query(x => x.LoanApplicationId == loanApplicationId
             && x.TenantId == tenant.Id && x.LoanApplication.UserId == userId).Include(x => x.LoanApplication).Include(x => x.LoanContact_LoanContactId).Select(x => new
                GetBorrowerModel
             {
                 Id = x.Id,
                 FirstName = x.LoanContact_LoanContactId.FirstName,
                 LastName = x.LoanContact_LoanContactId.LastName,
                 OwnTypeId = x.OwnTypeId.Value
             }).OrderBy(x => x.Id).ToListAsync();
        }
        private async Task RemoveRelationshipIfRequired(TenantModel tenant, int userId, int borrowerId, int loanApplicationId)
        {
            var borrower = await Repository.Query(b => b.Id != borrowerId
                && b.LoanApplicationId == loanApplicationId
                && b.LoanApplication.UserId == userId
                && b.TenantId == tenant.Id
                && b.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary // Make sure borrower is secondary
                && b.RelationWithPrimaryId == (byte)FamilyRelationTypeEnum.Spouse)
                .Include(b => b.LoanContact_LoanContactId)
                .SingleOrDefaultAsync();
            if(borrower!=null)
            {
                borrower.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
                borrower.RelationWithPrimaryId = null;
                borrower.SpouseLoanContactId = null;
            }
        }

        public async Task<GetBorrowerMaritalStatusModel> GetMaritalStatus(TenantModel tenant, int userId, int loanApplicationId, int borrowerId)
        {
            //return await Repository.Query(x => x.Id == borrowerId && x.LoanApplicationId == loanApplicationId && x.LoanApplication.UserId == userId && x.TenantId == tenant.Id)
            //    .Include(x => x.LoanApplication)
            //    .Include(x => x.LoanContact)
            //    .Select(x => new GetBorrowerMaritalStatusModel { MaritalStatus = x.LoanContact.MaritalStatusId, RelationshipWithPrimary = x.RelationWithPrimaryId, MarriedToPrimary = ((FamilyRelationTypeEnum)x.RelationWithPrimaryId) == FamilyRelationTypeEnum.Spouse })
            //    .SingleAsync();
            var results = await Repository.Query(x => x.Id == borrowerId && x.LoanApplicationId == loanApplicationId && x.LoanApplication.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.LoanApplication)
                .Include(x => x.LoanContact_LoanContactId)
                //.Select(x => new GetBorrowerMaritalStatusModel { MaritalStatus = x.LoanContact.MaritalStatusId, RelationshipWithPrimary = x.RelationWithPrimaryId, MarriedToPrimary = ((FamilyRelationTypeEnum)x.RelationWithPrimaryId) == FamilyRelationTypeEnum.Spouse })
                .SingleAsync();
            GetBorrowerMaritalStatusModel maritalStatus = new GetBorrowerMaritalStatusModel()
            {
                MaritalStatus = results.LoanContact_LoanContactId.MaritalStatusId,
                RelationshipWithPrimary = results.RelationWithPrimaryId
            };
            if (results.RelationWithPrimaryId == ((int) FamilyRelationTypeEnum.Other))
            {
                maritalStatus.MarriedToPrimary = null;
            }
            else
            {
                maritalStatus.MarriedToPrimary =  results.RelationWithPrimaryId == (int)FamilyRelationTypeEnum.Spouse;
            }
            return maritalStatus;
        }

        public async Task<bool> AddOrUpdateMaritalStatus(TenantModel tenant, int userId, BorrowerMaritalStatusModel model)
        {
            var borrower = await Repository.Query(x => x.Id == model.Id && x.LoanApplicationId == model.LoanApplicationId && x.LoanApplication.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.LoanApplication)
                .Include(x => x.LoanContact_LoanContactId)
                .SingleAsync();

            LoanContact loanContact = borrower.LoanContact_LoanContactId;

            loanContact.MaritalStatusId = model.MaritalStatus;
            if (borrower.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary)
            {
                borrower.RelationWithPrimaryId = null;
            }
            else
            {
                var primaryBorrower = await Repository.Query(x => x.LoanApplicationId == model.LoanApplicationId && x.LoanApplication.UserId == userId && x.TenantId == tenant.Id && x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary)
                                .SingleAsync();

                primaryBorrower.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
                if (model.MarriedToPrimary)
                {
                    await this.RemoveRelationshipIfRequired(tenant, userId, model.Id, model.LoanApplicationId);
                    borrower.RelationWithPrimaryId = ((int)FamilyRelationTypeEnum.Spouse);
                    primaryBorrower.SpouseLoanContactId = borrower.LoanContactId;
                    borrower.SpouseLoanContactId = primaryBorrower.LoanContactId;
                }
                else
                {
                    borrower.RelationWithPrimaryId = null;
                    if (primaryBorrower.SpouseLoanContactId == borrower.LoanContactId)
                    {
                        primaryBorrower.SpouseLoanContactId = null;
                        borrower.SpouseLoanContactId = null;
                    }
                }
            }
            Uow.Repository<Borrower>().Update(borrower);
            Uow.Repository<LoanContact>().Update(loanContact);
            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(application);
            await Uow.SaveChangesAsync();
            return true;
        }

        public async Task<BorrowerModel> GetBorrowerInfo(TenantModel tenant, int userId, int loanApplicationId, int borrowerId)
        {
            return await Repository.Query(borrower => borrower.Id == borrowerId && borrower.LoanApplicationId == loanApplicationId && borrower.LoanApplication.UserId == userId && borrower.TenantId == tenant.Id)
                .Include(borrower => borrower.LoanApplication).Include(x => x.LoanContact_LoanContactId).Select(borrower => new BorrowerModel
                {
                    CellPhone = borrower.LoanContact_LoanContactId.CellPhone,
                    Email = borrower.LoanContact_LoanContactId.EmailAddress,
                    FirstName = borrower.LoanContact_LoanContactId.FirstName,
                    MiddleName = borrower.LoanContact_LoanContactId.MiddleName,
                    LastName = borrower.LoanContact_LoanContactId.LastName,
                    HomePhone = borrower.LoanContact_LoanContactId.HomePhone,
                    Id = borrower.Id,
                    Suffix = borrower.LoanContact_LoanContactId.Suffix,
                    WorkExt = borrower.LoanContact_LoanContactId.WorkPhoneExt,
                    WorkPhone = borrower.LoanContact_LoanContactId.WorkPhone,
                    OwnTypeId = borrower.OwnTypeId
                }
                ).SingleAsync();
        }
        public async Task<BorrowerModel> PopulatePrimaryBorrowerInfo(TenantModel tenant, int userId)
        {
            return await _tenantConfigUow.Repository<TenantConfig.Entity.Models.Customer>().Query(x => x.TenantId == tenant.Id && x.UserId == userId)
                .Include(x => x.Contact).ThenInclude(x => x.ContactEmailInfoes)
                .Include(x => x.Contact).ThenInclude(x => x.ContactPhoneInfoes)
                .Select(x => new BorrowerModel
                {
                    FirstName = x.Contact.FirstName,
                    LastName = x.Contact.LastName,
                    Email = x.Contact.ContactEmailInfoes.Any(x => x.TypeId == (int)EmailType.Primary) ? x.Contact.ContactEmailInfoes.First(x => x.TypeId == (int)EmailType.Primary).Email : null,
                    CellPhone = x.Contact.ContactPhoneInfoes.Any(x => x.TypeId == (int)PhoneType.Mobile) ? x.Contact.ContactPhoneInfoes.First(x => x.TypeId == (int)PhoneType.Mobile).Phone : null
                }).SingleOrDefaultAsync();
        }
        public async Task<AddBorrowerModel> AddOrUpdateBorrowerInfo(TenantModel tenant, int userId, BorrowerInfoModel model)
        {
            Borrower borrower = null;
            int count = await Repository.Query(x => x.LoanApplicationId == model.LoanApplicationId && x.LoanApplication.UserId == userId && x.TenantId == tenant.Id).CountAsync();
            if (model.Id.HasValue)
            {
                borrower = await Repository.Query(x => x.LoanApplicationId == model.LoanApplicationId && x.LoanApplication.UserId == userId && x.TenantId == tenant.Id && x.Id == model.Id.Value)
                    .Include(x => x.LoanApplication)
                    .Include(x => x.LoanContact_LoanContactId).SingleAsync();
            }
            if (borrower != null)
            {
                Repository.Update(borrower);
            }
            else
            {
                count++;
                if (count > 4)
                    throw new Exception("Borrower limit reached");
                borrower = new Borrower
                {
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    LoanApplicationId = model.LoanApplicationId,
                    OwnTypeId = (int)(count <= 1 ? TenantConfig.Common.DistributedCache.OwnType.Primary : TenantConfig.Common.DistributedCache.OwnType.Secondary),
                    TenantId = tenant.Id,
                    LoanContact_LoanContactId = new LoanContact
                    {
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                        TenantId = tenant.Id
                    }
                };
                Repository.Insert(borrower);
            }
            borrower.LoanContact_LoanContactId.CellPhone = model.CellPhone;
            borrower.LoanContact_LoanContactId.EmailAddress = model.Email;
            borrower.LoanContact_LoanContactId.FirstName = model.FirstName;
            borrower.LoanContact_LoanContactId.MiddleName = model.MiddleName;
            borrower.LoanContact_LoanContactId.LastName = model.LastName;
            borrower.LoanContact_LoanContactId.HomePhone = model.HomePhone;
            borrower.LoanContact_LoanContactId.Suffix = model.Suffix;
            borrower.LoanContact_LoanContactId.WorkPhone = model.WorkPhone;
            borrower.LoanContact_LoanContactId.WorkPhoneExt = model.WorkExt;
            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            application.NoOfBorrower = count;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(application);
            await Uow.SaveChangesAsync();
            return new AddBorrowerModel { Id = borrower.Id, OwnTypeId = borrower.OwnTypeId.Value };
        }
        public async Task<AddressModel> GetBorrowerAddress(TenantModel tenant, int userId, int loanApplicationId, int borrowerId)
        {
            var address = await Repository.Query(x => x.Id == borrowerId && x.LoanApplicationId == loanApplicationId && x.LoanApplication.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.LoanApplication)
                .Include(x => x.BorrowerResidences).ThenInclude(x => x.LoanAddress)
                .Select(x => x.BorrowerResidences.SingleOrDefault(y => y.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary))
                .Select(x => new AddressModel
                {
                    Id = x.BorrowerId.Value,
                    City = x.LoanAddress.CityName,
                    CountryId = x.LoanAddress.CountryId,
                    HousingStatusId = x.OwnershipTypeId.Value,
                    Rent = x.MonthlyRent,
                    StateId = x.LoanAddress.StateId,
                    Street = x.LoanAddress.StreetAddress,
                    Unit = x.LoanAddress.UnitNo,
                    ZipCode = x.LoanAddress.ZipCode,
                    CountryName = x.LoanAddress.CountryName,
                    StateName = x.LoanAddress.StateName
                }
                ).SingleAsync();
            if (address?.Id == 0)
                address = null;
            return address;
        }

        public async Task<int> AddOrUpdateAddress(TenantModel tenant, int userId, BorrowerAddressModel model)
        {
            BorrowerResidence borrowerResidence = await Repository.Query(x => x.Id == model.Id && x.LoanApplicationId == model.LoanApplicationId && x.LoanApplication.UserId == userId && x.TenantId == tenant.Id)
                    .Include(x => x.LoanApplication)
                    .Include(x => x.BorrowerResidences).ThenInclude(x => x.LoanAddress)
                    .Select(x => x.BorrowerResidences.SingleOrDefault(y => y.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary))
                    .SingleOrDefaultAsync();
            if (borrowerResidence == null)
            {
                borrowerResidence = new BorrowerResidence
                {
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    TenantId = tenant.Id,
                    BorrowerId = model.Id,
                    LoanAddress = new LoanApplicationDb.Entity.Models.AddressInfo
                    {
                        IsDeleted = false,
                        TenantId = tenant.Id,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    },
                    TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary
                };
                Uow.Repository<BorrowerResidence>().Insert(borrowerResidence);
            }
            else
            {
                Uow.Repository<BorrowerResidence>().Update(borrowerResidence);
            }
            borrowerResidence.LoanAddress.CityName = model.City;
            borrowerResidence.LoanAddress.CountryId = model.CountryId;
            borrowerResidence.OwnershipTypeId = model.HousingStatusId;
            borrowerResidence.MonthlyRent = model.Rent;
            borrowerResidence.LoanAddress.StateId = model.StateId;
            borrowerResidence.LoanAddress.StreetAddress = model.Street;
            borrowerResidence.LoanAddress.UnitNo = model.Unit;
            borrowerResidence.LoanAddress.ZipCode = model.ZipCode;
            borrowerResidence.LoanAddress.CountryName = model.CountryName;
            borrowerResidence.LoanAddress.StateName = model.StateName;
            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(application);

            if(model.HousingStatusId!=1)// if not own, delete from my property
            {
                var borrowerProperty = await Uow.Repository<BorrowerProperty>().Query(x => x.BorrowerId == model.Id && x.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary).SingleOrDefaultAsync();
                if(borrowerProperty!=null)
                {
                    await _myPropertyService.DeleteProperty(borrowerProperty.Id);
                }
            }
            await Uow.SaveChangesAsync();
            return borrowerResidence.BorrowerId.Value;
        }

        public async Task<bool> DeleteSecondaryBorrower(TenantModel tenant, int userId, int loanApplicationId, int borrowerId, string state)
        {
            var id = await Repository.Query(x => x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary
            && x.LoanApplicationId == loanApplicationId && x.LoanApplication.UserId == userId && x.TenantId == tenant.Id && x.Id == borrowerId)
                .Include(x => x.LoanApplication).Select(x => x.Id).SingleAsync();
            await Uow.DataContext.Database.ExecuteSqlRawAsync($"exec dbo.[spDeleteBorrower] {id}");
            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == loanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = state;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(application);
            await Uow.SaveChangesAsync();
            return true;
        }

        public async Task<BorrowersFirstReviewModel> GetBorrowersForFirstReview(TenantModel tenant, int userId, int? loanApplicationId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(query: x => x.Id == loanApplicationId.Value &&
                    x.IsActive == true &&
                    x.UserId == userId &&
                    x.TenantId == tenant.Id)
                .Include(app => app.Borrowers).ThenInclude(b => b.BorrowerResidences).ThenInclude(borrowerResidences=>borrowerResidences.LoanAddress)
                .Include(app => app.Borrowers).ThenInclude(b => b.LoanContact_LoanContactId)

                .SingleAsync();

            var borrowerReviewModel = new BorrowersFirstReviewModel
            {
                BorrowerReviews = new List<BorrowerReview>()
            };

            foreach (var borrower in loanApplication.Borrowers)
            {
                var borrowerReview = new BorrowerReview();

                var borrowerResidence = borrower.BorrowerResidences.FirstOrDefault();
                borrowerReview.BorrowerId = borrower.Id;
                borrowerReview.FirstName = borrower.LoanContact_LoanContactId.FirstName;
                borrowerReview.MiddleName = borrower.LoanContact_LoanContactId.MiddleName;
                borrowerReview.LastName = borrower.LoanContact_LoanContactId.LastName;
                borrowerReview.OwnTypeId = borrower.OwnTypeId;
                borrowerReview.EmailAddress = borrower.LoanContact_LoanContactId.EmailAddress;
                borrowerReview.HomePhone = borrower.LoanContact_LoanContactId.HomePhone;
                borrowerReview.WorkPhone = borrower.LoanContact_LoanContactId.WorkPhone;
                borrowerReview.WorkPhoneExt = borrower.LoanContact_LoanContactId.WorkPhoneExt;
                borrowerReview.CellPhone = borrower.LoanContact_LoanContactId.CellPhone;
                borrowerReview.IsVaEligible = borrower.IsVaEligible;
                borrowerReview.MaritalStatusId = borrower.LoanContact_LoanContactId.MaritalStatusId;
                if (borrowerResidence != null && borrowerResidence.LoanAddress != null)
                {
                    borrowerReview.BorrowerAddress = new BorrowerAddress();
                    borrowerReview.BorrowerAddress.CountryId = borrowerResidence.LoanAddress.CountryId;
                    borrowerReview.BorrowerAddress.CountryName = borrowerResidence.LoanAddress.CountryName;
                    borrowerReview.BorrowerAddress.StateId = borrowerResidence.LoanAddress.StateId;
                    borrowerReview.BorrowerAddress.StateName = borrowerResidence.LoanAddress.StateName;
                    borrowerReview.BorrowerAddress.CountyId = borrowerResidence.LoanAddress.CountyId;
                    borrowerReview.BorrowerAddress.CountyName = borrowerResidence.LoanAddress.CountyName;
                    borrowerReview.BorrowerAddress.CityId = borrowerResidence.LoanAddress.CityId;
                    borrowerReview.BorrowerAddress.CityName = borrowerResidence.LoanAddress.CityName;
                    borrowerReview.BorrowerAddress.StreetAddress = borrowerResidence.LoanAddress.StreetAddress;
                    borrowerReview.BorrowerAddress.ZipCode = borrowerResidence.LoanAddress.ZipCode;
                    borrowerReview.BorrowerAddress.UnitNo = borrowerResidence.LoanAddress.UnitNo;
                }
                borrowerReviewModel.BorrowerReviews.Add(borrowerReview);
            }

            return borrowerReviewModel;
        }
    }
}
