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
    public class MyPropertyService : ServiceBase<LoanApplicationContext, BorrowerProperty>, IMyPropertyService
    {
        public MyPropertyService(IUnitOfWork<LoanApplicationContext> previousUow,
                           IServiceProvider services) : base(previousUow: previousUow,
                                                             services: services)
        {
        }
        public async Task<List<MyPropertyModel>> GetPropertyList(TenantModel tenant, int userId, int loanApplicationId, int borrowerId)
        {
            return await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>()
                                      .Query(query: x => x.TenantId == tenant.Id && x.BorrowerId == borrowerId && x.Borrower.LoanApplication.UserId == userId
                                       && x.Borrower.LoanApplicationId == loanApplicationId)
                                      .OrderBy(x => x.TypeId)
                                      .Select(x => new MyPropertyModel
                                      {
                                          Id = x.Id,
                                          TypeId = x.TypeId.Value,
                                          PropertyType = x.PropertyInfo.PropertyType.Name,
                                          Address = new GenericAddressModel
                                          {
                                              City = x.PropertyInfo.AddressInfo.CityName,
                                              CountryId = x.PropertyInfo.AddressInfo.CountryId,
                                              StateId = x.PropertyInfo.AddressInfo.StateId,
                                              Street = x.PropertyInfo.AddressInfo.StreetAddress,
                                              Unit = x.PropertyInfo.AddressInfo.UnitNo,
                                              ZipCode = x.PropertyInfo.AddressInfo.ZipCode,
                                              CountryName = x.PropertyInfo.AddressInfo.CountryName,
                                              StateName = x.PropertyInfo.AddressInfo.StateName
                                          }
                                      }).ToListAsync();
        }
        public async Task<HasFirstMortgageModel> DoYouHaveFirstMortgage(TenantModel tenant, int userId, int loanApplicationId, int borrowerPropertyId)
        {
            var borrowerProperty = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>()
                                      .Query(query: x => x.TenantId == tenant.Id && x.Id == borrowerPropertyId && x.Borrower.LoanApplication.UserId == userId
                                       && x.Borrower.LoanApplicationId == loanApplicationId).Include(x => x.PropertyInfo).ThenInclude(x => x.PropertyTaxEscrows).SingleAsync();
            var model = new HasFirstMortgageModel();
            model.HasFirstMortgage = borrowerProperty.PropertyInfo.HasFirstMortgage;
            model.LoanApplicationId = loanApplicationId;
            model.Id = borrowerPropertyId;
            if (model.HasFirstMortgage == false)
            {
                model.PropertyTax = borrowerProperty.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.PropertyTax)?.AnnuallyPayment;
                model.HomeOwnerInsurance = borrowerProperty.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.HomeOwnerInsurance)?.AnnuallyPayment;
                model.FloodInsurance = borrowerProperty.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.FloodInsurance)?.AnnuallyPayment;
            }
            return model;
        }
        public async Task<int> AddOrUpdateHasFirstMortgage(TenantModel tenant, int userId, HasFirstMortgageModel model)
        {
            LoanApplicationDb.Entity.Models.BorrowerProperty borrowerProperty = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>()
                .Query(query: x => x.TenantId == tenant.Id && x.Id == model.Id && x.Borrower.LoanApplication.UserId == userId
                        && x.Borrower.LoanApplicationId == model.LoanApplicationId)
                .Include(x => x.PropertyInfo).ThenInclude(x => x.PropertyTaxEscrows)
                .SingleAsync();

            PropertyTaxEscrow AddEscrow(EscrowEntityTypeEnum escrow)
            {
                var tax = borrowerProperty.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)escrow);
                if (tax == null)
                {
                    tax = new PropertyTaxEscrow();
                    tax.TrackingState = TrackingState.Added;
                    tax.TenantId = tenant.Id;
                    tax.EscrowEntityTypeId = (int)escrow;
                    borrowerProperty.PropertyInfo.PropertyTaxEscrows.Add(tax);
                }
                else
                {
                    tax.TrackingState = TrackingState.Modified;
                }
                return tax;
            }

            borrowerProperty.PropertyInfo.HasFirstMortgage = model.HasFirstMortgage;
            borrowerProperty.PropertyInfo.TrackingState = TrackingState.Modified;

            if (model.HasFirstMortgage == false)
            {
                if (model.PropertyTax.HasValue)
                {
                    var propertyTax = AddEscrow(EscrowEntityTypeEnum.PropertyTax);
                    propertyTax.AnnuallyPayment = model.PropertyTax.Value;
                }

                if (model.HomeOwnerInsurance.HasValue)
                {
                    var hoi = AddEscrow(EscrowEntityTypeEnum.HomeOwnerInsurance);
                    hoi.AnnuallyPayment = model.HomeOwnerInsurance.Value;
                }

                if (model.FloodInsurance.HasValue)
                {
                    var flood = AddEscrow(EscrowEntityTypeEnum.FloodInsurance);
                    flood.AnnuallyPayment = model.FloodInsurance.Value;
                }
            }
            else
            {
                //foreach (var tax in borrowerProperty.PropertyInfo.PropertyTaxEscrows)
                //{
                //    tax.TrackingState = TrackingState.Deleted;
                //}
            }

            Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Update(borrowerProperty);

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();
            return borrowerProperty.Id;
        }
        public async Task<bool?> DoYouHaveSecondMortgage(TenantModel tenant, int userId, int loanApplicationId, int borrowerPropertyId)
        {
            return (await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>()
                                      .Query(query: x => x.TenantId == tenant.Id && x.Id == borrowerPropertyId && x.Borrower.LoanApplication.UserId == userId
                                       && x.Borrower.LoanApplicationId == loanApplicationId).Include(x => x.PropertyInfo).SingleAsync()).PropertyInfo.HasSecondMortgage;
        }
        public async Task<int> AddOrUpdateHasSecondMortgage(TenantModel tenant, int userId, HasSecondMortgageModel model)
        {
            LoanApplicationDb.Entity.Models.BorrowerProperty borrowerProperty = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>()
                   .Query(query: x => x.TenantId == tenant.Id && x.Id == model.Id && x.Borrower.LoanApplication.UserId == userId
                   && x.Borrower.LoanApplicationId == model.LoanApplicationId)
                   .Include(x => x.PropertyInfo)
                   .SingleAsync();

            borrowerProperty.PropertyInfo.HasSecondMortgage = model.HasSecondMortgage;
            borrowerProperty.PropertyInfo.TrackingState = TrackingState.Modified;

            Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Update(borrowerProperty);

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();
            return borrowerProperty.Id;
        }
        public async Task<PrimaryBorrowerDetailModel> GetPrimaryBorrowerAddressDetail(TenantModel tenant, int userId, int loanApplicationId)
        {

            PrimaryBorrowerDetailModel primaryBorrowerDetail = await Uow.Repository<Borrower>().Query(x => x.LoanApplicationId == loanApplicationId && x.LoanApplication.UserId == userId && x.TenantId == tenant.Id && x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary)
                    .Include(x => x.LoanApplication)
                    .Include(x => x.BorrowerResidences).ThenInclude(x => x.LoanAddress)
                    .Select(x => x.BorrowerResidences.SingleOrDefault(y => y.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary))
                     .Select(x => new PrimaryBorrowerDetailModel
                     {
                         BorrowerId = x.BorrowerId.Value,
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
                         },
                         HousingStatusId = x.OwnershipTypeId.Value

                     })
                    .SingleAsync();
            return primaryBorrowerDetail;
        }

        public async Task<int> AddOrUpdatePrimaryPropertyType(TenantModel tenant, int userId, BorrowerPropertyRequestModel model)
        {
            LoanApplicationDb.Entity.Models.BorrowerProperty borrowerProperty = default(LoanApplicationDb.Entity.Models.BorrowerProperty);
            if (!model.Id.HasValue)
            {
                BorrowerResidence borrowerResidence = await Uow.Repository<Borrower>().Query(x => x.Id == model.BorrowerId && x.LoanApplicationId == model.LoanApplicationId && x.LoanApplication.UserId == userId && x.TenantId == tenant.Id)
                                                            .Include(x => x.LoanApplication)
                                                            .Include(x => x.BorrowerResidences).ThenInclude(x => x.LoanAddress)
                                                            .Select(x => x.BorrowerResidences.SingleOrDefault(y => y.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary))
                                                            .SingleAsync();

                borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty
                {
                    TrackingState = TrackingState.Added,
                    TenantId = tenant.Id,
                    BorrowerId = model.BorrowerId,
                    TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,
                    PropertyInfo = new PropertyInfo()
                    {
                        TrackingState = TrackingState.Added,
                        TenantId = tenant.Id,
                        AddressInfoId = borrowerResidence.LoanAddressId,
                        PropertyUsageId = (int)PropertyUsageEnum.IWillLiveHerePrimaryResidence
                    }
                };
                Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            }
            else
            {
                borrowerProperty = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>()
                                      .Query(query: x => x.TenantId == tenant.Id && x.Id == model.Id && x.Borrower.Id == model.BorrowerId && x.Borrower.LoanApplication.UserId == userId
                                       && x.Borrower.LoanApplicationId == model.LoanApplicationId && x.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary)
                                      .Include(x => x.PropertyInfo)
                                      .SingleAsync();

                borrowerProperty.TrackingState = TrackingState.Modified;
            }

            if ((int)PropertyTypes.Duplex2Unit == model.PropertyTypeId || (int)PropertyTypes.Quadplex4Unit == model.PropertyTypeId || (int)PropertyTypes.Triplex3Unit == model.PropertyTypeId)
            {
                borrowerProperty.PropertyInfo.RentalIncome = model.RentalIncome;
            }
            else
            {
                borrowerProperty.PropertyInfo.RentalIncome = null;
            }

            borrowerProperty.TenantId = tenant.Id;
            borrowerProperty.PropertyInfo.PropertyTypeId = model.PropertyTypeId;

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();
            return borrowerProperty.Id;
        }

        public async Task<BorrowerPropertyResponseModel> GetBorrowerPrimaryPropertyType(TenantModel tenant, int userId, int borrowerPropertyId, int loanApplicationId)
        {
            BorrowerPropertyResponseModel borrowerPropertyResponseModel = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>()
                                      .Query(query: x => x.TenantId == tenant.Id && x.Id == borrowerPropertyId && x.Borrower.LoanApplication.UserId == userId
                                       && x.Borrower.LoanApplicationId == loanApplicationId && x.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary)
                                      .Select(y => new BorrowerPropertyResponseModel
                                      {
                                          Id = y.Id,
                                          PropertyTypeId = y.PropertyInfo.PropertyTypeId.Value,
                                          RentalIncome = y.PropertyInfo.RentalIncome
                                      })
                                      .SingleAsync();

            return borrowerPropertyResponseModel;
        }

        public async Task<BorrowerAdditionalPropertyResponseModel> GetBorrowerAdditionalPropertyType(TenantModel tenant, int userId, int borrowerPropertyId, int loanApplicationId)
        {
            BorrowerAdditionalPropertyResponseModel borrowerAdditionalPropertyResponseModel = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>()
                                     .Query(query: x => x.TenantId == tenant.Id && x.Id == borrowerPropertyId && x.Borrower.LoanApplication.UserId == userId
                                      && x.Borrower.LoanApplicationId == loanApplicationId && x.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary)
                                     .Select(y => new BorrowerAdditionalPropertyResponseModel
                                     {
                                         Id = y.Id,
                                         PropertyTypeId = y.PropertyInfo.PropertyTypeId.Value,
                                     })
                                     .SingleAsync();

            return borrowerAdditionalPropertyResponseModel;
        }

        public async Task<int> AddOrUpdateAdditionalPropertyType(TenantModel tenant, int userId, BorrowerAdditionalPropertyRequestModel model)
        {
            LoanApplicationDb.Entity.Models.BorrowerProperty borrowerProperty = default(LoanApplicationDb.Entity.Models.BorrowerProperty);
            if (!model.Id.HasValue)
            {


                borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty
                {
                    TrackingState = TrackingState.Added,
                    TenantId = tenant.Id,
                    BorrowerId = model.BorrowerId,
                    TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,
                    PropertyInfo = new PropertyInfo()
                    {
                        TrackingState = TrackingState.Added,
                        TenantId = tenant.Id
                    }
                };
                Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            }
            else
            {
                borrowerProperty = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>()
                                      .Query(query: x => x.TenantId == tenant.Id && x.Id == model.Id && x.Borrower.Id == model.BorrowerId && x.Borrower.LoanApplication.UserId == userId
                                       && x.Borrower.LoanApplicationId == model.LoanApplicationId && x.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary)
                                      .Include(x => x.PropertyInfo)
                                      .SingleAsync();

                borrowerProperty.TrackingState = TrackingState.Modified;
            }

            borrowerProperty.PropertyInfo.PropertyTypeId = model.PropertyTypeId;

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();
            return borrowerProperty.Id;
        }

        public async Task<int> AddOrUpdatePropertyValue(TenantModel tenant, int userId, CurrentResidenceModel model)
        {
            LoanApplicationDb.Entity.Models.BorrowerProperty borrowerProperty = default(LoanApplicationDb.Entity.Models.BorrowerProperty);
            if (!model.Id.HasValue)
            {
                BorrowerResidence borrowerResidence = await Uow.Repository<Borrower>().Query(x => x.Id == model.BorrowerId && x.LoanApplicationId == model.LoanApplicationId && x.LoanApplication.UserId == userId && x.TenantId == tenant.Id)
                                                            .Include(x => x.LoanApplication)
                                                            .Include(x => x.BorrowerResidences).ThenInclude(x => x.LoanAddress)
                                                            .Select(x => x.BorrowerResidences.SingleOrDefault(y => y.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary))
                                                            .SingleAsync();

                borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty
                {
                    TrackingState = TrackingState.Added,
                    TenantId = tenant.Id,
                    BorrowerId = model.BorrowerId,
                    TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,
                    PropertyInfo = new PropertyInfo()
                    {
                        TrackingState = TrackingState.Added,
                        TenantId = tenant.Id,
                        AddressInfoId = borrowerResidence.LoanAddressId,
                        PropertyUsageId = (int)PropertyUsageEnum.IWillLiveHerePrimaryResidence
                    }
                };
                Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            }
            else
            {
                borrowerProperty = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>()
                                      .Query(query: x => x.Id==model.Id.Value && x.TenantId == tenant.Id && x.Id == model.Id && x.Borrower.Id == model.BorrowerId && x.Borrower.LoanApplication.UserId == userId
                                       && x.Borrower.LoanApplicationId == model.LoanApplicationId)
                                      .Include(x => x.PropertyInfo)
                                      .SingleAsync();

                borrowerProperty.TrackingState = TrackingState.Modified;
                borrowerProperty.PropertyInfo.TrackingState = TrackingState.Modified;
            }

            borrowerProperty.PropertyInfo.PropertyValue = model.PropertyValue;
            borrowerProperty.PropertyInfo.HoaDues = model.OwnersDue;
            borrowerProperty.PropertyInfo.IntentToSellPriorToPurchase = model.IsSelling;

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();
            return borrowerProperty.Id;
        }

        public async Task<CurrentResidenceModel> GetPropertyValue(TenantModel tenant, int userId, int borrowerPropertyId, int loanApplicationId)
        {
            CurrentResidenceModel currentResidenceModel = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>()
                                      .Query(query: x => x.TenantId == tenant.Id && x.Id == borrowerPropertyId && x.Borrower.LoanApplication.UserId == userId
                                      && x.Borrower.LoanApplicationId == loanApplicationId)
                                      .Select(y => new CurrentResidenceModel
                                      {
                                          Id = y.Id,
                                          LoanApplicationId = y.Borrower.LoanApplicationId.Value,
                                          PropertyValue = y.PropertyInfo.PropertyValue,
                                          OwnersDue = y.PropertyInfo.HoaDues,
                                          IsSelling = y.PropertyInfo.IntentToSellPriorToPurchase,
                                      })
                                      .SingleAsync();

            return currentResidenceModel;
        }

        public async Task<int> AddOrUpdateAdditionalPropertyInfo(TenantModel tenant, int userId, BorrowerAdditionalPropertyInfoRequestModel model)
        {
            LoanApplicationDb.Entity.Models.BorrowerProperty borrowerProperty = default(LoanApplicationDb.Entity.Models.BorrowerProperty);
            if (!model.Id.HasValue)
            {
                borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty
                {
                    TrackingState = TrackingState.Added,
                    TenantId = tenant.Id,
                    BorrowerId = model.BorrowerId,
                    TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,
                    PropertyInfo = new PropertyInfo()
                    {
                        TrackingState = TrackingState.Added,
                        TenantId = tenant.Id
                    }
                };
                Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            }
            else
            {
                borrowerProperty = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>()
                                      .Query(query: x => x.TenantId == tenant.Id && x.Id == model.Id && x.Borrower.Id == model.BorrowerId && x.Borrower.LoanApplication.UserId == userId
                                       && x.Borrower.LoanApplicationId == model.LoanApplicationId && x.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary)
                                      .Include(x => x.PropertyInfo)
                                      .SingleAsync();

                borrowerProperty.TrackingState = TrackingState.Modified;
                borrowerProperty.TrackingState = TrackingState.Modified;
            }

            borrowerProperty.PropertyInfo.PropertyUsageId = model.PropertyUsageId;
            if (model.PropertyUsageId == (int)PropertyUsageEnum.ThisIsAnInvestmentProperty)
            {
                borrowerProperty.PropertyInfo.RentalIncome = model.RentalIncome;
            }
            else
            {
                borrowerProperty.PropertyInfo.RentalIncome = null;
            }


            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();
            return borrowerProperty.Id;
        }

        public async Task<BorrowerAdditionalPropertyInfoResponseModel> GetBorrowerAdditionalPropertyInfo(TenantModel tenant, int userId, int borrowerPropertyId, int loanApplicationId)
        {
            BorrowerAdditionalPropertyInfoResponseModel borrowerAdditionalPropertyInfoResponseModel = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>()
                                     .Query(query: x => x.TenantId == tenant.Id && x.Id == borrowerPropertyId && x.Borrower.LoanApplication.UserId == userId
                                      && x.Borrower.LoanApplicationId == loanApplicationId && x.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary)
                                     .Select(y => new BorrowerAdditionalPropertyInfoResponseModel
                                     {
                                         Id = y.Id,
                                         PropertyUsageId = y.PropertyInfo.PropertyUsageId.Value,
                                         RentalIncome = y.PropertyInfo.RentalIncome.Value
                                     })
                                     .SingleAsync();

            return borrowerAdditionalPropertyInfoResponseModel;
        }

        public async Task<int> AddOrUpdateFirstMortgageValue(TenantModel tenant, int userId, FirstMortgageModel model)
        {

            LoanApplicationDb.Entity.Models.BorrowerProperty borrowerProperty = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>()
                .Query(query: x => x.TenantId == tenant.Id && x.Id == model.Id && x.Borrower.LoanApplication.UserId == userId
                        && x.Borrower.LoanApplicationId == model.LoanApplicationId)
                .Include(x => x.PropertyInfo).ThenInclude(x => x.PropertyTaxEscrows)
                 .Include(x => x.PropertyInfo).ThenInclude(x => x.MortgageOnProperties)
                .SingleAsync();



            PropertyTaxEscrow AddEscrow(EscrowEntityTypeEnum escrow)
            {
                var tax = borrowerProperty.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)escrow);
                if (tax == null)
                {
                    tax = new PropertyTaxEscrow();
                    tax.TrackingState = TrackingState.Added;
                    tax.TenantId = tenant.Id;
                    tax.EscrowEntityTypeId = (int)escrow;
                    borrowerProperty.PropertyInfo.PropertyTaxEscrows.Add(tax);
                }
                else
                {
                    tax.TrackingState = TrackingState.Modified;
                }
                return tax;
            }


            MortgageOnProperty AddMortgage(MortgageTypeEnum mortgageType)
            {
                var mortgageOnProperty = borrowerProperty.PropertyInfo.MortgageOnProperties.FirstOrDefault(x => x.MortgageTypeId == (int)mortgageType);
                if (mortgageOnProperty == null)
                {
                    mortgageOnProperty = new MortgageOnProperty();
                    mortgageOnProperty.TrackingState = TrackingState.Added;
                    mortgageOnProperty.TenantId = tenant.Id;

                    mortgageOnProperty.MortgageTypeId = (int)mortgageType;

                    borrowerProperty.PropertyInfo.MortgageOnProperties.Add(mortgageOnProperty);
                }
                else
                {
                    mortgageOnProperty.TrackingState = TrackingState.Modified;
                }
                return mortgageOnProperty;
            }



            borrowerProperty.PropertyInfo.TrackingState = TrackingState.Modified;
            borrowerProperty.PropertyInfo.HasFirstMortgage = true;

            if (model.PropertyTax.HasValue)
            {
                var propertyTax = AddEscrow(EscrowEntityTypeEnum.PropertyTax);
                propertyTax.AnnuallyPayment = model.PropertyTax.Value;
                propertyTax.IncludedInMortgagePayment = model.PropertyTaxesIncludeinPayment;
            }

            if (model.HomeOwnerInsurance.HasValue)
            {
                var hoi = AddEscrow(EscrowEntityTypeEnum.HomeOwnerInsurance);
                hoi.AnnuallyPayment = model.HomeOwnerInsurance.Value;
                hoi.IncludedInMortgagePayment = model.HomeOwnerInsuranceIncludeinPayment;
            }

            if (model.FloodInsurance.HasValue)
            {
                var flood = AddEscrow(EscrowEntityTypeEnum.FloodInsurance);
                flood.AnnuallyPayment = model.FloodInsurance.Value;
                flood.IncludedInMortgagePayment = model.FloodInsuranceIncludeinPayment;
            }

            var firstmortgage = AddMortgage(MortgageTypeEnum.FirstMortgage);
            firstmortgage.MonthlyPayment = model.FirstMortgagePayment;
            firstmortgage.MortgageBalance = model.UnpaidFirstMortgagePayment;
            firstmortgage.PaidAtClosing = model.PaidAtClosing.Value;

            firstmortgage.IsHeloc = model.IsHeloc;
            if (model.IsHeloc == true)
            {
                firstmortgage.MortgageLimit = model.HelocCreditLimit;
            }
            else {
                firstmortgage.MortgageLimit = null ;

            }


            Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Update(borrowerProperty);

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();
            return borrowerProperty.Id;



        }

        public async Task<FirstMortgageModel> GetFirstMortgageValue(TenantModel tenant, int userId, int borrowerPropertyId, int loanApplicationId)
        {
            var borrowerproperty = await Uow.Repository<BorrowerProperty>().Query(x => x.Id == borrowerPropertyId && x.TenantId == tenant.Id && x.Borrower.LoanApplicationId == loanApplicationId && x.Borrower.LoanApplication.UserId == userId)
                .Include(x => x.PropertyInfo)
                .ThenInclude(x => x.PropertyTaxEscrows)
                .Include(x => x.PropertyInfo)
                .ThenInclude(x => x.MortgageOnProperties)
                .SingleOrDefaultAsync();



            if (borrowerproperty?.PropertyInfo?.MortgageOnProperties.Any(x => x.MortgageTypeId == (int)MortgageTypeEnum.FirstMortgage) == true)
            {
                FirstMortgageModel model = new FirstMortgageModel();

                model.Id = borrowerproperty.Id;
                model.LoanApplicationId = loanApplicationId;
                model.FirstMortgagePayment = borrowerproperty.PropertyInfo.MortgageOnProperties.FirstOrDefault(x => x.MortgageTypeId == (int)MortgageTypeEnum.FirstMortgage)?.MonthlyPayment;
                model.UnpaidFirstMortgagePayment = borrowerproperty.PropertyInfo.MortgageOnProperties.FirstOrDefault(x => x.MortgageTypeId == (int)MortgageTypeEnum.FirstMortgage)?.MortgageBalance;

                model.HomeOwnerInsurance = borrowerproperty.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.HomeOwnerInsurance)?.AnnuallyPayment;
                model.HomeOwnerInsuranceIncludeinPayment = borrowerproperty.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.HomeOwnerInsurance)?.IncludedInMortgagePayment;

                model.FloodInsurance = borrowerproperty.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.FloodInsurance)?.AnnuallyPayment;
                model.FloodInsuranceIncludeinPayment = borrowerproperty.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.FloodInsurance)?.IncludedInMortgagePayment;

                model.PropertyTax = borrowerproperty.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.PropertyTax)?.AnnuallyPayment;
                model.PropertyTaxesIncludeinPayment = borrowerproperty.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.PropertyTax)?.IncludedInMortgagePayment;

                model.IsHeloc = borrowerproperty.PropertyInfo.MortgageOnProperties.FirstOrDefault(x => x.MortgageTypeId == (int)MortgageTypeEnum.FirstMortgage)?.IsHeloc;
                model.HelocCreditLimit = borrowerproperty.PropertyInfo.MortgageOnProperties.FirstOrDefault(x => x.MortgageTypeId == (int)MortgageTypeEnum.FirstMortgage)?.MortgageLimit;

                model.PaidAtClosing = borrowerproperty.PropertyInfo.MortgageOnProperties.FirstOrDefault(x => x.MortgageTypeId == (int)MortgageTypeEnum.FirstMortgage)?.PaidAtClosing;

                return model;
            }
            else
                return null;
        }

        public async Task<int> AddOrUpdatBorrowerAdditionalPropertyAddress(TenantModel tenant, int userId, BorrowerAdditionalPropertyAddressRequestModel model)
        {
            BorrowerProperty borrowerProperty = await Repository.Query(x => x.Id == model.BorrowerPropertyId && x.TenantId == tenant.Id && x.Borrower.LoanApplication.UserId == userId
                                    && x.Borrower.LoanApplicationId == model.LoanApplicationId)
                .Include(x => x.PropertyInfo).ThenInclude(x => x.AddressInfo).SingleAsync();


            if (borrowerProperty.PropertyInfo.AddressInfo == null)
            {
                borrowerProperty.PropertyInfo.AddressInfo = new AddressInfo
                {
                    //TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    TrackingState = TrackingState.Added,
                    TenantId = tenant.Id,

                };
            }
            else
            {
                borrowerProperty.PropertyInfo.AddressInfo.TrackingState = TrackingState.Modified;
            }
            borrowerProperty.TrackingState = TrackingState.Modified;
            borrowerProperty.PropertyInfo.TrackingState = TrackingState.Modified;

            borrowerProperty.PropertyInfo.AddressInfo.StreetAddress = model.Street;
            borrowerProperty.PropertyInfo.AddressInfo.UnitNo = model.Unit;
            borrowerProperty.PropertyInfo.AddressInfo.CityName = model.City;
            borrowerProperty.PropertyInfo.AddressInfo.StateId = model.StateId;
            borrowerProperty.PropertyInfo.AddressInfo.ZipCode = model.ZipCode;
            borrowerProperty.PropertyInfo.AddressInfo.CountryId = model.CountryId;
            borrowerProperty.PropertyInfo.AddressInfo.CountryName = model.CountryName;
            borrowerProperty.PropertyInfo.AddressInfo.StateName = model.StateName;

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(application);

            await Uow.SaveChangesAsync();

            return borrowerProperty.PropertyInfo.AddressInfo.Id;
        }


        public async Task<BorrowerAdditionalPropertyAddressRsponseModel> GetBorrowerAdditionalPropertyAddress(TenantModel tenant, int userId, int loanApplicationId, int borrowerPropertyId)
        {
            var address = await Repository.Query(x => x.Id == borrowerPropertyId && x.TenantId == tenant.Id && x.Borrower.LoanApplication.UserId == userId
                                   && x.Borrower.LoanApplicationId == loanApplicationId)
                .Include(x => x.PropertyInfo).ThenInclude(x => x.AddressInfo)

                .Select(x => new BorrowerAdditionalPropertyAddressRsponseModel
                {
                    Id = x.PropertyInfo.AddressInfo.Id,
                    City = x.PropertyInfo.AddressInfo.CityName,
                    CountryId = x.PropertyInfo.AddressInfo.CountryId,
                    StateId = x.PropertyInfo.AddressInfo.StateId,
                    Street = x.PropertyInfo.AddressInfo.StreetAddress,
                    Unit = x.PropertyInfo.AddressInfo.UnitNo,
                    ZipCode = x.PropertyInfo.AddressInfo.ZipCode,
                    CountryName = x.PropertyInfo.AddressInfo.CountryName,
                    StateName = x.PropertyInfo.AddressInfo.StateName
                }
                ).SingleAsync();
            if (address?.Id == 0)
                address = null;
            return address;

        }

        public async Task<int> AddOrUpdateSecondMortgageValue(TenantModel tenant, int userId, SecondMortgageModel model)
        {

            LoanApplicationDb.Entity.Models.BorrowerProperty borrowerProperty = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>()
                .Query(query: x => x.TenantId == tenant.Id && x.Id == model.Id && x.Borrower.LoanApplication.UserId == userId
                        && x.Borrower.LoanApplicationId == model.LoanApplicationId)
                .Include(x => x.PropertyInfo).ThenInclude(x => x.PropertyTaxEscrows)
                 .Include(x => x.PropertyInfo).ThenInclude(x => x.MortgageOnProperties)
                .SingleAsync();


            MortgageOnProperty AddMortgage(MortgageTypeEnum mortgageType)
            {
                var mortgageOnProperty = borrowerProperty.PropertyInfo.MortgageOnProperties.FirstOrDefault(x => x.MortgageTypeId == (int)mortgageType);
                if (mortgageOnProperty == null)
                {
                    mortgageOnProperty = new MortgageOnProperty();
                    mortgageOnProperty.TrackingState = TrackingState.Added;
                    mortgageOnProperty.TenantId = tenant.Id;

                    mortgageOnProperty.MortgageTypeId = (int)mortgageType;

                    borrowerProperty.PropertyInfo.MortgageOnProperties.Add(mortgageOnProperty);
                }
                else
                {
                    mortgageOnProperty.TrackingState = TrackingState.Modified;
                }
                return mortgageOnProperty;
            }



            borrowerProperty.PropertyInfo.TrackingState = TrackingState.Modified;
            borrowerProperty.PropertyInfo.HasSecondMortgage = true;

            var mortgage = AddMortgage(MortgageTypeEnum.SecondMortgage);
            mortgage.MonthlyPayment = model.SecondMortgagePayment;
            mortgage.MortgageBalance = model.UnpaidSecondMortgagePayment;
            mortgage.PaidAtClosing = model.PaidAtClosing.Value;

            mortgage.IsHeloc = model.IsHeloc;
            if (model.IsHeloc == true)
            {
                mortgage.MortgageLimit = model.HelocCreditLimit;
            }
            else
            {
                mortgage.MortgageLimit = null;

            }


            Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Update(borrowerProperty);

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();
            return borrowerProperty.Id;



        }

        public async Task<SecondMortgageModel> GetSecondMortgageValue(TenantModel tenant, int userId, int borrowerPropertyId, int loanApplicationId)
        {
            var borrowerproperty = await Uow.Repository<BorrowerProperty>().Query(x => x.Id == borrowerPropertyId && x.TenantId == tenant.Id && x.Borrower.LoanApplicationId == loanApplicationId && x.Borrower.LoanApplication.UserId == userId)
                .Include(x => x.PropertyInfo)
                .ThenInclude(x => x.PropertyTaxEscrows)
                .Include(x => x.PropertyInfo)
                .ThenInclude(x => x.MortgageOnProperties)
                .SingleOrDefaultAsync();



            if (borrowerproperty?.PropertyInfo?.MortgageOnProperties.Any(x => x.MortgageTypeId == (int)MortgageTypeEnum.SecondMortgage) == true)
            {
                SecondMortgageModel model = new SecondMortgageModel();

                model.Id = borrowerproperty.Id;
                model.LoanApplicationId = loanApplicationId;
                model.SecondMortgagePayment = borrowerproperty.PropertyInfo.MortgageOnProperties.FirstOrDefault(x => x.MortgageTypeId == (int)MortgageTypeEnum.SecondMortgage)?.MonthlyPayment;
                model.UnpaidSecondMortgagePayment = borrowerproperty.PropertyInfo.MortgageOnProperties.FirstOrDefault(x => x.MortgageTypeId == (int)MortgageTypeEnum.SecondMortgage)?.MortgageBalance;
                model.PaidAtClosing = borrowerproperty.PropertyInfo.MortgageOnProperties.FirstOrDefault(x => x.MortgageTypeId == (int)MortgageTypeEnum.SecondMortgage)?.PaidAtClosing;
                model.IsHeloc = borrowerproperty.PropertyInfo.MortgageOnProperties.FirstOrDefault(x => x.MortgageTypeId == (int)MortgageTypeEnum.SecondMortgage)?.IsHeloc;
                model.HelocCreditLimit = borrowerproperty.PropertyInfo.MortgageOnProperties.FirstOrDefault(x => x.MortgageTypeId == (int)MortgageTypeEnum.SecondMortgage)?.MortgageLimit;

                return model;
            }
            else
                return null;
        }


        public async Task<List<MyPropertyModel>> GetFinalScreenReview(TenantModel tenant, int userId, int loanApplicationId, int borrowerId)
        {
            var myPropertyModel = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>()
                                      .Query(query: x => x.TenantId == tenant.Id && x.BorrowerId == borrowerId && x.Borrower.LoanApplication.UserId == userId
                                       && x.Borrower.LoanApplicationId == loanApplicationId )
                                      .OrderBy(x => x.TypeId)
                                      .Select(x => new MyPropertyModel
                                      {
                                              Id = x.Id,
                                              FirstName = x.Borrower.LoanContact_LoanContactId.FirstName,
                                              LastName = x.Borrower.LoanContact_LoanContactId.LastName,
                                              OwnTypeId = x.Borrower.OwnTypeId.Value,
                                              TypeId = x.TypeId.Value,
                                              PropertyType = x.PropertyInfo.PropertyType.Name,
                                              Address = new GenericAddressModel
                                              {
                                                  City = x.PropertyInfo.AddressInfo.CityName,
                                                  CountryId = x.PropertyInfo.AddressInfo.CountryId,
                                                  StateId = x.PropertyInfo.AddressInfo.StateId,
                                                  Street = x.PropertyInfo.AddressInfo.StreetAddress,
                                                  Unit = x.PropertyInfo.AddressInfo.UnitNo,
                                                  ZipCode = x.PropertyInfo.AddressInfo.ZipCode,
                                                  CountryName = x.PropertyInfo.AddressInfo.CountryName,
                                                  StateName = x.PropertyInfo.AddressInfo.StateName
                                              }
                                      }).ToListAsync();
            
           
            return myPropertyModel;
        }

        public async Task<bool> DoYouOwnAdditionalProperty(TenantModel tenant, int userId, int loanApplicationId, int borrowerId)
        {
            LoanApplicationDb.Entity.Models.BorrowerProperty BorrowerProperty  = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>()
                                    .Query(query: x => x.TenantId == tenant.Id && x.BorrowerId == borrowerId && x.Borrower.LoanApplication.UserId == userId
                                     && x.Borrower.LoanApplicationId == loanApplicationId && x.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary)
                                    .FirstOrDefaultAsync();

            if (BorrowerProperty != null)
                return true;

            else
                return false;


        }

        public async Task<bool> DeleteProperty(TenantModel tenant, int userId, int loanApplicationId, int borrowerPropertyId)
        {
            var borrowerproperty = await Uow.Repository<BorrowerProperty>().Query(x => x.Id == borrowerPropertyId && x.TenantId == tenant.Id && x.Borrower.LoanApplicationId == loanApplicationId && x.Borrower.LoanApplication.UserId == userId)
                .SingleAsync();
            if (borrowerproperty.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary)
                return false;
            await DeleteProperty(borrowerPropertyId);
            return true;
        }

        public async Task DeleteProperty(int borrowerPropertyId)
        {
            await Uow.DataContext.Database.ExecuteSqlRawAsync($"exec dbo.DeleteBorrowerProperty {borrowerPropertyId}");
        }

    
    }
}
