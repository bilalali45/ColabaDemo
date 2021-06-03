using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanApplication.Model;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TenantConfig.Common.DistributedCache;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;

namespace LoanApplication.Service
{
    public interface ISubjectPropertyService : IServiceBase<PropertyInfo>
    {
        //Task<bool> AddOrUpdateSubjectPropertyAddress(TenantModel tenant, RainmakerAddressInfoModel addressInfoModel, int userId);
        //Task<RainmakerAddressInfoModel> GetSubjectPropertyAddress(TenantModel tenant, int loanApplicationId, int userId);

        Task<bool> AddOrUpdateSubjectPropertyState(TenantModel tenant, UpdateSubjectPropertyStateModel model, int userId);
        Task<GetSubjectPropertyStateModel> GetSubjectPropertyState(TenantModel tenant, int loanApplicationId, int userId);

        Task<bool> AddOrUpdateLoanAmountDetail(TenantModel tenant, LoanAmountDetailModel model, int userId);
        Task<LoanAmountDetailBase> GetSubjectPropertyLoanAmountDetail(TenantModel tenant, int loanApplicationId, int userId);
        Task<int> UpdatePropertyIdentifiedFlag(TenantModel tenant, int userId, PropertyIdentifiedModel model);

        Task<PropertyIdentifiedBaseModel> GetPropertyIdentifiedFlag(TenantModel tenant, int userId, int loanApplicationId);
    }
    public class SubjectPropertyService : ServiceBase<LoanApplicationContext, PropertyInfo>, ISubjectPropertyService
    {
        private readonly ILogger<SubjectPropertyService> _logger;
        private readonly IDbFunctionService _dbFunctionService;
        public SubjectPropertyService(IUnitOfWork<LoanApplicationContext> uow, IServiceProvider services, ILogger<SubjectPropertyService> logger, IDbFunctionService dbFunctionService) : base(uow, services)
        {
            _logger = logger;
            _dbFunctionService = dbFunctionService;
        }

        //public async Task<bool> AddOrUpdateSubjectPropertyAddress(TenantModel tenant, RainmakerAddressInfoModel addressInfoModel, int userId)
        //{
        //    var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == addressInfoModel.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
        //        .Include(x => x.PropertyInfo.AddressInfo)
        //        .FirstOrDefaultAsync();

        //    if (loanApplication == null)
        //    {
        //        _logger.LogInformation($"Loan application not found. Loan Application ID {addressInfoModel.LoanApplicationId}");
        //        return false;
        //    }

        //    if (loanApplication.PropertyInfo == null)
        //    {
        //        _logger.LogInformation($"Loan application property info not found. Loan Application ID {addressInfoModel.LoanApplicationId}");
        //        return false;
        //    }

        //    var propertyInfo = loanApplication.PropertyInfo;

        //    if (propertyInfo.AddressInfo == null)
        //    {
        //        propertyInfo.AddressInfo = new AddressInfo()
        //        {
        //            TenantId = tenant.Id,
        //            ZipCode = addressInfoModel.ZipCode,
        //            CityName = addressInfoModel.CityName,
        //            CountryId = addressInfoModel.CountryId,
        //            StateId = addressInfoModel.StateId,
        //            StreetAddress = addressInfoModel.StreetAddress,
        //            UnitNo = addressInfoModel.UnitNo,
        //            TrackingState = TrackingState.Added
        //        };
        //        propertyInfo.TrackingState = TrackingState.Modified;
        //        base.Uow.Repository<PropertyInfo>().Update(propertyInfo);
        //    }
        //    else
        //    {
        //        propertyInfo.AddressInfo.ZipCode = addressInfoModel.ZipCode;
        //        propertyInfo.AddressInfo.CityName = addressInfoModel.CityName;
        //        propertyInfo.AddressInfo.CountryId = addressInfoModel.CountryId;
        //        propertyInfo.AddressInfo.StateId = addressInfoModel.StateId;
        //        propertyInfo.AddressInfo.StreetAddress = addressInfoModel.StreetAddress;
        //        propertyInfo.AddressInfo.UnitNo = addressInfoModel.UnitNo;
        //        propertyInfo.AddressInfo.TrackingState = TrackingState.Modified;
        //    }

        //    if (loanApplication.LoanGoalId == ((int) LoanGoals.PropertyUnderContract))
        //    {
        //        loanApplication.ExpectedClosingDate = addressInfoModel.ExpectedClosingDate;
        //    }
        //    else
        //    {
        //        loanApplication.ExpectedClosingDate = null;
        //    }
        //    loanApplication.State = addressInfoModel.State;
        //    Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
        //    await Uow.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<RainmakerAddressInfoModel> GetSubjectPropertyAddress(TenantModel tenant, int loanApplicationId, int userId)
        //{
        //    RainmakerAddressInfoBase model = new RainmakerAddressInfoBase();
        //    var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == loanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
        //        .Include(x => x.PropertyInfo.AddressInfo)
        //        .FirstOrDefaultAsync();

        //    if (loanApplication == null)
        //    {
        //        _logger.LogWarning($"Loan application not found. Loan Application ID {loanApplicationId}");
        //    }
        //    else
        //    {
        //        if (loanApplication.PropertyInfo == null)
        //        {
        //            _logger.LogWarning($"Loan application property info not found. Loan Application ID {loanApplicationId}");
        //        }
        //        else
        //        {
        //            var propertyInfo = loanApplication.PropertyInfo;
        //            AddressInfo addressInfo = propertyInfo.AddressInfo;
        //            if (addressInfo != null)
        //            {
        //                return new RainmakerAddressInfoModel()
        //                {
        //                    ZipCode = addressInfo.ZipCode,
        //                    CityName = addressInfo.CityName,
        //                    CountryId = addressInfo.CountryId,
        //                    LoanApplicationId = loanApplicationId,
        //                    StateId = addressInfo.StateId,
        //                    StreetAddress = addressInfo.StreetAddress,
        //                    UnitNo = addressInfo.UnitNo,
        //                    ExpectedClosingDate = loanApplication.ExpectedClosingDate
        //                };
        //            }
        //        }
        //    }

        //    return null;
        //}

        public async Task<bool> AddOrUpdateSubjectPropertyState(TenantModel tenant, UpdateSubjectPropertyStateModel model, int userId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.PropertyInfo.AddressInfo)
                .FirstOrDefaultAsync();

            if (loanApplication == null)
            {
                _logger.LogInformation($"Loan application not found. Loan Application ID {model.LoanApplicationId}");
                return false;
            }

            if (loanApplication.PropertyInfo == null)
            {
                _logger.LogInformation($"Loan application property info not found. Loan Application ID {model.LoanApplicationId}");
                return false;
            }

            var propertyInfo = loanApplication.PropertyInfo;

            if (propertyInfo.AddressInfo == null)
            {
                propertyInfo.AddressInfo = new AddressInfo()
                {
                    TenantId = tenant.Id,
                    StateId = model.StateId,
                    TrackingState = TrackingState.Added
                };
                propertyInfo.TrackingState = TrackingState.Modified;
                base.Uow.Repository<PropertyInfo>().Update(propertyInfo);
            }
            else
            {   propertyInfo.AddressInfo.StateId = model.StateId;
                propertyInfo.AddressInfo.TrackingState = TrackingState.Modified;
            }
            loanApplication.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
            await Uow.SaveChangesAsync();
            return true;
        }

        public async Task<GetSubjectPropertyStateModel> GetSubjectPropertyState(TenantModel tenant, int loanApplicationId, int userId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == loanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.PropertyInfo.AddressInfo)
                .FirstOrDefaultAsync();

            if (loanApplication == null)
            {
                _logger.LogWarning($"Loan application not found. Loan Application ID {loanApplicationId}");
            }
            else
            {
                if (loanApplication.PropertyInfo == null)
                {
                    _logger.LogWarning($"Loan application property info not found. Loan Application ID {loanApplicationId}");
                }
                else
                {
                    var propertyInfo = loanApplication.PropertyInfo;
                    AddressInfo addressInfo = propertyInfo.AddressInfo;
                    if (addressInfo != null)
                    {
                        return new GetSubjectPropertyStateModel()
                        {
                            LoanApplicationId = loanApplicationId,
                            StateId = addressInfo.StateId
                        };
                    }
                }
            }

            return null;
        }

        public async Task<bool> AddOrUpdateLoanAmountDetail(TenantModel tenant, LoanAmountDetailModel model, int userId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.PropertyInfo.AddressInfo)
                .FirstOrDefaultAsync();

            if (loanApplication == null)
            {
                _logger.LogInformation($"Loan application not found. Loan Application ID {model.LoanApplicationId}");
                return false;
            }

            if (loanApplication.PropertyInfo == null)
            {
                _logger.LogInformation($"Loan application property info not found. Loan Application ID {model.LoanApplicationId}");
                return false;
            }

            var propertyInfo = loanApplication.PropertyInfo;

            propertyInfo.PropertyValue = model.PropertyValue;
            loanApplication.LoanAmount = model.PropertyValue - model.DownPayment;

            var primaryBorrower = Uow.Repository<Borrower>()
                .Query(b => b.LoanApplicationId == model.LoanApplicationId
                            && b.TenantId == tenant.Id
                            && b.OwnTypeId == ((int)BorrowerTypes.PrimaryBorrower))
                .Include(b => b.AssetBorrowerBinders)
                .ThenInclude(b => b.BorrowerAsset)
                .FirstOrDefault();

            var giftAssetBinder = primaryBorrower?.AssetBorrowerBinders
                .FirstOrDefault(ab => ab.BorrowerAsset.AssetTypeId == ((int)AssetTypes.CashGift)
                                      && ab.BorrowerAsset.UseForDownpayment.HasValue);

            var downPaymentGiftAsset = giftAssetBinder?.BorrowerAsset;

            if (model.GiftPartOfDownPayment)
            {   
                

                if (downPaymentGiftAsset == null)
                {
                    BorrowerAsset downpaymentAsset = new BorrowerAsset()
                    {
                        AssetTypeId = ((int) AssetTypes.CashGift),
                        TenantId = tenant.Id,
                        Value = model.GiftAmount,
                        UseForDownpayment = model.DownPayment,
                        ValueDate = model.DateOfTransfer,
                        TrackingState = TrackingState.Added
                    };
                    Uow.Repository<BorrowerAsset>().Insert(downpaymentAsset);
                    await Uow.SaveChangesAsync();

                    Uow.Repository<AssetBorrowerBinder>().Insert(new AssetBorrowerBinder()
                    {
                        TenantId = tenant.Id,
                        BorrowerId = primaryBorrower.Id,
                        BorrowerAssetId = downpaymentAsset.Id,
                        TrackingState = TrackingState.Added
                    });
                    await Uow.SaveChangesAsync();
                }
                else
                {
                    downPaymentGiftAsset.UseForDownpayment = model.DownPayment;
                    downPaymentGiftAsset.ValueDate = model.DateOfTransfer;
                    downPaymentGiftAsset.Value = model.GiftAmount;
                    downPaymentGiftAsset.TrackingState = TrackingState.Modified;
                }
            }
            else
            {
                if (giftAssetBinder != null && downPaymentGiftAsset != null)
                {
                    giftAssetBinder.TrackingState = TrackingState.Deleted;
                    downPaymentGiftAsset.TrackingState = TrackingState.Deleted;
                }
            }

            propertyInfo.TrackingState = TrackingState.Modified;
            
            
            loanApplication.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
            await Uow.SaveChangesAsync();
            return true;
        }

        public async Task<LoanAmountDetailBase> GetSubjectPropertyLoanAmountDetail(TenantModel tenant, int loanApplicationId, int userId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == loanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.PropertyInfo.AddressInfo)
                .FirstOrDefaultAsync();

            if (loanApplication == null)
            {
                _logger.LogWarning($"Loan application not found. Loan Application ID {loanApplicationId}");
            }
            else
            {
                if (loanApplication.PropertyInfo == null)
                {
                    _logger.LogWarning($"Loan application property info not found. Loan Application ID {loanApplicationId}");
                }
                else
                {
                    var propertyInfo = loanApplication.PropertyInfo;

                    var primaryBorrower = Uow.Repository<Borrower>()
                        .Query(b => b.LoanApplicationId == loanApplicationId
                                    && b.TenantId == tenant.Id
                                    && b.OwnTypeId == ((int)BorrowerTypes.PrimaryBorrower))
                        .Include(b => b.AssetBorrowerBinders)
                        .ThenInclude(b => b.BorrowerAsset)
                        .FirstOrDefault();

                    var giftAssetBinder = primaryBorrower?.AssetBorrowerBinders
                        .FirstOrDefault(ab => ab.BorrowerAsset.AssetTypeId == ((int)AssetTypes.CashGift)
                                              && ab.BorrowerAsset.UseForDownpayment.HasValue);

                    var downPaymentGiftAsset = giftAssetBinder?.BorrowerAsset;

                    bool giftPartOfDownPayment = downPaymentGiftAsset != null;
                    bool? giftPartReceived = downPaymentGiftAsset != null && downPaymentGiftAsset.ValueDate <= DateTime.Now; // TODO Discuss with Danish Bhai
                    if (downPaymentGiftAsset == null)
                    {
                        giftPartReceived = null;
                    }
                    return new LoanAmountDetailBase()
                    {
                        LoanApplicationId = loanApplicationId,
                        PropertyValue = propertyInfo.PropertyValue,
                        DownPayment = propertyInfo.PropertyValue - loanApplication.LoanAmount,
                        GiftPartOfDownPayment = giftPartOfDownPayment,
                        GiftPartReceived = giftPartReceived,
                        DateOfTransfer = downPaymentGiftAsset?.ValueDate,
                        GiftAmount = downPaymentGiftAsset?.Value
                    };
                }
            }

            return null;
        }

        public async Task<int> UpdatePropertyIdentifiedFlag(TenantModel tenant, int userId, PropertyIdentifiedModel model)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
                .Include(x => x.PropertyInfo).ThenInclude(pi => pi.AddressInfo)
                .FirstOrDefaultAsync();
            if(loanApplication == null)
            {
                _logger.LogWarning($"Loan application not found. Loan Application ID {model.LoanApplicationId} User ID : {userId}");
                return -1;
            }

            if (loanApplication.PropertyInfo == null)
            {
                _logger.LogWarning($"Subject property detail not found. Loan Application ID {model.LoanApplicationId} User ID : {userId}");
                return -2;
            }

            var tenantSateList = _dbFunctionService.UdfState(tenant.Id).ToList();

            var addressInfo = loanApplication.PropertyInfo.AddressInfo;
            if (addressInfo == null)
            {
                addressInfo = new AddressInfo()
                {
                    StateId = model.StateId,
                    StateName = tenantSateList.FirstOrDefault(state => state.Id == model.StateId)?.Name,
                    TrackingState = TrackingState.Added
                };
                loanApplication.PropertyInfo.AddressInfo = addressInfo;
            }
            else
            {
                if (model.IsIdentified == false)
                {
                    addressInfo.StateId = model.StateId;
                }
                addressInfo.TrackingState = TrackingState.Modified;
            }

            if (model.IsIdentified != null && model.IsIdentified == false)
            {
                addressInfo.CityId = null;
                addressInfo.CityName = null;
                addressInfo.StreetAddress = null;
                addressInfo.ZipCode = null;
                addressInfo.UnitNo = null;
                addressInfo.StateName = tenantSateList.FirstOrDefault(state => state.Id == model.StateId)?.Name;
                //addressInfo.TrackingState = TrackingState.Modified;
            }

            Uow.Repository<PropertyInfo>().Update(loanApplication.PropertyInfo);

            loanApplication.IsPropertyIdentified = model.IsIdentified;
            loanApplication.State = model.State;
            loanApplication.TrackingState = TrackingState.Modified;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
            await Uow.SaveChangesAsync();

            return addressInfo.Id;
        }

        public async Task<PropertyIdentifiedBaseModel> GetPropertyIdentifiedFlag(TenantModel tenant, int userId, int loanApplicationId)
        {
            PropertyIdentifiedBaseModel model = null;
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == loanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
                .Include(x => x.PropertyInfo).ThenInclude(pi => pi.AddressInfo)
                .FirstOrDefaultAsync();
            if (loanApplication == null)
            {
                _logger.LogWarning($"Loan application not found. Loan Application ID {loanApplicationId} User ID : {userId}");
                return null;
            }

            if (loanApplication.PropertyInfo == null)
            {
                _logger.LogWarning($"Property info not found. Loan Application ID {loanApplicationId} User ID : {userId}");
                return null;
            }
            int? stateId = null;

            if (loanApplication.PropertyInfo.AddressInfo != null)
            {
                stateId = loanApplication.PropertyInfo.AddressInfo.StateId;
            }

            var tenantStates = _dbFunctionService.UdfState(tenant.Id).ToList();

            model = new PropertyIdentifiedBaseModel()
            {
                IsIdentified = loanApplication.IsPropertyIdentified,
                StateId = stateId,
                StateName = tenantStates.FirstOrDefault(state => state.Id == stateId)?.Name,
                LoanApplicationId = loanApplicationId
            };

            

            return model;
        }
    }
}
