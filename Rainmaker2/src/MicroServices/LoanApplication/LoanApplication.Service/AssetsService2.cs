using Extensions.ExtensionClasses;
using LoanApplication.Model;
using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;
using TrackableEntities.Common.Core;

namespace LoanApplication.Service
{

    public partial class AssetsService
    {
        public List<GetAllAssetCategoriesModel> GetAllAssetCategories(TenantModel tenant)
        {
            var assetCategories = _dbFunctionService.UdfAssetCategory(tenant.Id).ToList();
            var assetTypes = _dbFunctionService.UdfAssetType(tenant.Id).ToList();
            return assetCategories.Where(x=>assetTypes.Any(y=>y.AssetCategoryId==x.Id)).Select(x => new GetAllAssetCategoriesModel()
            {
                DisplayName = x.DisplayName,
                Id = x.Id,
                ImageUrl = null,
                Name = x.Name,
                TenantAlternateName = x.TenantAlternateName
            }).ToList();
        }

        public List<LoanApplication.Model.GetAssetTypesByCategoryModel> GetAssetTypesByCategory(TenantModel tenant, int categoryId)
        {
            var assetTypes = _dbFunctionService.UdfAssetType(tenant.Id).ToList();
            return assetTypes
                .Where(x => x.AssetCategoryId == categoryId)
                .Select(x => new LoanApplication.Model.GetAssetTypesByCategoryModel()
                {
                    DisplayName = x.DisplayName,
                    Id = x.Id,
                    ImageUrl = null,
                    Name = x.Name,
                    TenantAlternateName = x.TenantAlternateName,
                    AssetCategoryId = x.AssetCategoryId,
                    FieldsInfo = x.FieldsInfo
                }).ToList();
        }

        public async Task<int> AddOrUpdateBorrowerAsset(TenantModel tenant, int userId, AddOrUpdateBorrowerAssetModel model)
        {
            List<BorrowerAssetCategory> allowedCategories = new List<BorrowerAssetCategory>()
            {
                BorrowerAssetCategory.BankAccount,
                BorrowerAssetCategory.FinancialStatement,
                BorrowerAssetCategory.RetirementAccount
            };

            if (!allowedCategories.Contains((BorrowerAssetCategory)model.AssetCategoryId))
            {
                return AssetsService.AssetCategoryNotAllowed;
            }

            var loanApplication = Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == model.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(app => app.Borrowers).ThenInclude(b => b.AssetBorrowerBinders).ThenInclude(binder => binder.BorrowerAsset)
                .FirstOrDefault();

            if (loanApplication == null)
            {
                _logger.LogWarning($"Loan application not found. Loan Application ID : {model.LoanApplicationId} User ID : {userId}");
                return AssetsService.LoanApplicationNotFound;
            }

            var borrower = loanApplication.Borrowers.FirstOrDefault(b => b.Id == model.BorrowerId);
            if (borrower == null)
            {
                _logger.LogWarning($"Borrower detail not found. Loan Application ID : {model.LoanApplicationId} Borrower ID: {model.BorrowerId}");
                return BorrowerDetailNotFound;
            }

            var customAssetTypes = _dbFunctionService.UdfAssetType(tenant.Id).ToList();
            var assetType = customAssetTypes.FirstOrDefault(type => type.Id == model.AssetTypeId);
            if (assetType == null)
            {
                _logger.LogWarning($"Asset type detail not found. Loan Application ID : {model.LoanApplicationId} Asset Type ID : {model.AssetTypeId}");
                return AssetTypeNotFound;
            }

            var customAssetCategories = _dbFunctionService.UdfAssetCategory(tenant.Id).ToList();
            var assetCategory = customAssetCategories.FirstOrDefault(type => type.Id == model.AssetCategoryId);
            if (assetCategory == null)
            {
                _logger.LogWarning($"Asset type category detail not found. Loan Application ID : {model.LoanApplicationId} Asset Type Category ID : {model.AssetCategoryId}");
                return AssetCategoryNotFound;
            }

            //if (assetType.AssetCategoryId != assetCategory.Id)
            //{
            //    _logger.LogWarning($"Asset type does not belong to category. Loan Application ID : {model.LoanApplicationId} Asset Type ID : {model.AssetTypeId} Asset Type Category ID : {model.AssetCategoryId}");
            //    return AssetTypeDoesNotBelongToCategory;
            //}

            BorrowerAsset borrowerAsset = null;
            var borrowerAssetBinder = borrower.AssetBorrowerBinders.FirstOrDefault(binder => binder.BorrowerAssetId == model.BorrowerAssetId && binder.TenantId == tenant.Id);
            if (borrowerAssetBinder == null)
            {
                borrowerAsset = new BorrowerAsset()
                {
                    AccountNumber = model.AccountNumber,
                    InstitutionName = model.InstitutionName,
                    AssetTypeId = model.AssetTypeId,
                    Value = model.AssetValue,
                    TenantId = tenant.Id,
                    TrackingState = TrackingState.Added
                };
                borrowerAssetBinder = new AssetBorrowerBinder()
                {
                    BorrowerId = model.BorrowerId,
                    BorrowerAsset = borrowerAsset,
                    TenantId = tenant.Id,
                    TrackingState = TrackingState.Added
                };
                Uow.Repository<BorrowerAsset>().Insert(borrowerAsset);
                Uow.Repository<AssetBorrowerBinder>().Insert(borrowerAssetBinder);
            }
            else
            {
                borrowerAsset = Uow.Repository<BorrowerAsset>().Query(asset =>
                        asset.TenantId == tenant.Id && asset.Id == borrowerAssetBinder.BorrowerAssetId)
                    .FirstOrDefault();
                if (borrowerAsset != null)
                {
                    borrowerAsset.AccountNumber = model.AccountNumber;
                    borrowerAsset.InstitutionName = model.InstitutionName;
                    borrowerAsset.Value = model.AssetValue;
                    //borrowerAsset.AssetTypeId = model.AssetTypeId;

                    borrowerAsset.TrackingState = TrackingState.Modified;
                    Uow.Repository<BorrowerAsset>().Update(borrowerAsset);
                }
            }

            //if (model.AssetCategoryId == ((int) BorrowerAssetCategory.RetirementAccount))
            //{
            //    borrowerAsset.AssetTypeId = null; // Retirement category is not mapped with any asset type.
            //}

            loanApplication.State = model.State;
            loanApplication.TrackingState = TrackingState.Modified;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
            await Uow.SaveChangesAsync();

            return borrowerAsset.Id;
        }

        public async Task<LoanApplicationBorrowersAssets> GetLoanApplicationBorrowersAssets(TenantModel tenant, int userId, int loanApplicationId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == loanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(app => app.Borrowers).ThenInclude(b => b.LoanContact_LoanContactId)
                .Include(app => app.Borrowers).ThenInclude(b => b.AssetBorrowerBinders).ThenInclude(binder => binder.BorrowerAsset)
                .FirstOrDefaultAsync();

            if (loanApplication == null)
            {
                _logger.LogWarning($"Loan application not found. Loan Application ID : {loanApplicationId} User ID : {userId}");
                return new LoanApplicationBorrowersAssets()
                {
                    ErrorMessage = "Loan application not found"
                };
            }

            LoanApplicationBorrowersAssets modelToReturn = new LoanApplicationBorrowersAssets()
            {
                LoanApplicationId = loanApplicationId,
                Borrowers = new List<BorrowerAssetsGetModel>()
            };

            var customAssetTypes = _dbFunctionService.UdfAssetType(tenant.Id).ToList();
            var customCategoryTypes = _dbFunctionService.UdfAssetCategory(tenant.Id).ToList();

            foreach (var borrower in loanApplication.Borrowers)
            {
                BorrowerAssetsGetModel borrowerToAdd = new BorrowerAssetsGetModel()
                {
                    BorrowerId = borrower.Id,
                    BorrowerName = $"{borrower.LoanContact_LoanContactId.FirstName} {borrower.LoanContact_LoanContactId.LastName}",
                    BorrowerAssets = new List<AssetGetModel>()
                };
                foreach (var binder in borrower.AssetBorrowerBinders)
                {
                    var assetTypeToSet = customAssetTypes.FirstOrDefault(type => type.Id == binder.BorrowerAsset.AssetTypeId);
                    var categoryTypeToSet = customCategoryTypes.FirstOrDefault(cat => cat.Id == assetTypeToSet?.AssetCategoryId);
                    if (assetTypeToSet != null)
                    {
                        assetTypeToSet.AssetCategory = categoryTypeToSet;
                    }
                    binder.BorrowerAsset.AssetType = assetTypeToSet;
                    AssetGetModel assetToAdd = new AssetGetModel()
                    {
                        BorrowerAssetId = binder.BorrowerAssetId,
                        AssetTypeId = binder.BorrowerAsset.AssetTypeId,
                        //AssetType = assetTypeToSet?.Name,
                        //AssetCategoryId = assetTypeToSet?.AssetCategoryId,
                        //AssetCategory = categoryTypeToSet?.Name,
                        AccountNumber = binder.BorrowerAsset.AccountNumber,
                        AssetValue = binder.BorrowerAsset.Value,
                        InstitutionName = binder.BorrowerAsset.InstitutionName
                    };
                    if (assetTypeToSet != null)
                    {
                        assetToAdd.AssetType = assetTypeToSet.DisplayName;
                        assetToAdd.AssetCategoryId = assetTypeToSet.AssetCategoryId;
                    }
                    if (categoryTypeToSet != null)
                    {
                        assetToAdd.AssetCategory = categoryTypeToSet.DisplayName;
                    }
                    borrowerToAdd.BorrowerAssets.Add(assetToAdd);
                }

                modelToReturn.Borrowers.Add(borrowerToAdd);
            }



            return modelToReturn;

        }

        public async Task<BorrowerAssetsGetModel> GetBorrowerAssets(TenantModel tenant, int userId, int loanApplicationId, int borrowerId)
        {
            BorrowerAssetsGetModel modelToReturn = null;
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == loanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(app => app.Borrowers).ThenInclude(b => b.LoanContact_LoanContactId)
                .Include(app => app.Borrowers).ThenInclude(b => b.AssetBorrowerBinders).ThenInclude(binder => binder.BorrowerAsset)
                .FirstOrDefaultAsync();

            if (loanApplication == null)
            {
                _logger.LogWarning($"Loan application not found. Loan Application ID : {loanApplicationId} User ID : {userId}");
                return new BorrowerAssetsGetModel()
                {
                    ErrorMessage = "Loan application not found"
                };
            }

            var borrower = loanApplication.Borrowers.FirstOrDefault(b => b.Id == borrowerId);
            if (borrower == null)
            {
                _logger.LogWarning($"borrower detail not found. Loan Application ID : {loanApplicationId} Borrower ID : {borrowerId}");
                return new BorrowerAssetsGetModel()
                {
                    ErrorMessage = "Borrower detail not found"
                };
            }

            var customAssetTypes = _dbFunctionService.UdfAssetType(tenant.Id).ToList();
            var customCategoryTypes = _dbFunctionService.UdfAssetCategory(tenant.Id).ToList();



            modelToReturn = new BorrowerAssetsGetModel()
            {
                BorrowerId = borrower.Id,
                BorrowerName = $"{borrower.LoanContact_LoanContactId.FirstName} {borrower.LoanContact_LoanContactId.LastName}",
                BorrowerAssets = new List<AssetGetModel>()
            };
            foreach (var binder in borrower.AssetBorrowerBinders)
            {
                var assetTypeToSet = customAssetTypes.FirstOrDefault(type => type.Id == binder.BorrowerAsset.AssetTypeId);
                var categoryTypeToSet = customCategoryTypes.FirstOrDefault(cat => cat.Id == assetTypeToSet?.AssetCategoryId);
                if (assetTypeToSet != null)
                {
                    assetTypeToSet.AssetCategory = categoryTypeToSet;
                }
                binder.BorrowerAsset.AssetType = assetTypeToSet;
                AssetGetModel assetToAdd = new AssetGetModel()
                {
                    BorrowerAssetId = binder.BorrowerAssetId,
                    AssetTypeId = binder.BorrowerAsset.AssetTypeId,
                    //AssetType = assetTypeToSet?.Name,
                    //AssetCategoryId = assetTypeToSet?.AssetCategoryId,
                    //AssetCategory = categoryTypeToSet?.Name,
                    AccountNumber = binder.BorrowerAsset.AccountNumber,
                    AssetValue = binder.BorrowerAsset.Value,
                    InstitutionName = binder.BorrowerAsset.InstitutionName
                };
                if (assetTypeToSet != null)
                {
                    assetToAdd.AssetType = assetTypeToSet.DisplayName;
                    assetToAdd.AssetCategoryId = assetTypeToSet.AssetCategoryId;
                }
                if (categoryTypeToSet != null)
                {
                    assetToAdd.AssetCategory = categoryTypeToSet.DisplayName;
                }
                modelToReturn.BorrowerAssets.Add(assetToAdd);
            }

            return modelToReturn;
        }

        public async Task<BorrowerAssetsGetModel> GetBorrowerAssetDetail(TenantModel tenant, int userId, BorrowerAssetDetailGetModel model)
        {
            BorrowerAssetsGetModel modelToReturn = null;
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == model.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(app => app.Borrowers).ThenInclude(b => b.LoanContact_LoanContactId)
                .Include(app => app.Borrowers).ThenInclude(b => b.AssetBorrowerBinders).ThenInclude(binder => binder.BorrowerAsset)
                .FirstOrDefaultAsync();

            if (loanApplication == null)
            {
                _logger.LogWarning($"Loan application not found. Loan Application ID : {model.LoanApplicationId} User ID : {userId}");
                return new BorrowerAssetsGetModel()
                {
                    ErrorMessage = ErrorMessages[LoanApplicationNotFound]
                };
            }

            var borrower = loanApplication.Borrowers.FirstOrDefault(b => b.Id == model.BorrowerId);
            if (borrower == null)
            {
                _logger.LogWarning($"borrower detail not found. Loan Application ID : {model.LoanApplicationId} Borrower ID : {model.BorrowerId}");
                return new BorrowerAssetsGetModel()
                {
                    ErrorMessage = ErrorMessages[BorrowerDetailNotFound]
                };
            }

            var binder = borrower.AssetBorrowerBinders.FirstOrDefault(abb => abb.BorrowerAssetId == model.BorrowerAssetId);
            if (binder == null)
            {
                _logger.LogWarning($"borrower asset detail not found. Loan Application ID : {model.LoanApplicationId} Borrower ID : {model.BorrowerId} Asset ID : {model.BorrowerAssetId}");
                return new BorrowerAssetsGetModel()
                {
                    ErrorMessage = ErrorMessages[BorrowerAssetDetailNotFound]
                };
            }

            var customAssetTypes = _dbFunctionService.UdfAssetType(tenant.Id).ToList();
            var customCategoryTypes = _dbFunctionService.UdfAssetCategory(tenant.Id).ToList();

            modelToReturn = new BorrowerAssetsGetModel()
            {
                BorrowerId = borrower.Id,
                BorrowerName = $"{borrower.LoanContact_LoanContactId.FirstName} {borrower.LoanContact_LoanContactId.LastName}",
                BorrowerAssets = new List<AssetGetModel>()
            };
            var assetTypeToSet = customAssetTypes.FirstOrDefault(type => type.Id == binder.BorrowerAsset.AssetTypeId);
            var categoryTypeToSet = customCategoryTypes.FirstOrDefault(cat => cat.Id == assetTypeToSet?.AssetCategoryId);
            if (assetTypeToSet != null)
            {
                assetTypeToSet.AssetCategory = categoryTypeToSet;
            }
            binder.BorrowerAsset.AssetType = assetTypeToSet;
            AssetGetModel assetToAdd = new AssetGetModel()
            {
                BorrowerAssetId = binder.BorrowerAssetId,
                AssetTypeId = binder.BorrowerAsset.AssetTypeId,
                //AssetType = assetTypeToSet?.Name,
                //AssetCategoryId = assetTypeToSet?.AssetCategoryId,
                //AssetCategory = categoryTypeToSet?.Name,
                AccountNumber = binder.BorrowerAsset.AccountNumber,
                AssetValue = binder.BorrowerAsset.Value,
                InstitutionName = binder.BorrowerAsset.InstitutionName
            };
            if (assetTypeToSet != null)
            {
                assetToAdd.AssetType = assetTypeToSet.DisplayName;
                assetToAdd.AssetCategoryId = assetTypeToSet.AssetCategoryId;
            }
            if (categoryTypeToSet != null)
            {
                assetToAdd.AssetCategory = categoryTypeToSet.DisplayName;
            }

            modelToReturn.BorrowerAssets.Add(assetToAdd);

            return modelToReturn;
        }





        /**************************************************************/


        public async Task<ProceedsFromNonRealState> GetFromLoanNonRealState(TenantModel tenant, int userId, int BorrowerAssetId, int AssetTypeId, int BorrowerId, int LoanApplicationId)
        {
            ProceedsFromNonRealState modelToReturn = null;
            var loanApplication = Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                        .Query(x => x.Id == LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                        .Include(app => app.Borrowers).ThenInclude(b => b.AssetBorrowerBinders).ThenInclude(binder => binder.BorrowerAsset)
                        .FirstOrDefault();


            if (loanApplication == null)
            {
                _logger.LogWarning($"Loan application not found. Loan Application ID : {LoanApplicationId} User ID : {userId}");
                return null;
            }

            var borrower = loanApplication.Borrowers.FirstOrDefault(b => b.Id == BorrowerId);
            if (borrower == null)
            {
                _logger.LogWarning($"Borrower detail not found. Loan Application ID : {LoanApplicationId} Borrower ID: {BorrowerId}");
                return null;
            }

            var customAssetTypes = _dbFunctionService.UdfAssetType(tenant.Id).ToList();
            var assetType = customAssetTypes.FirstOrDefault(type => type.Id == AssetTypeId);
            if (assetType == null)
            {
                _logger.LogWarning($"Asset type detail not found. Loan Application ID : {LoanApplicationId} Asset Type ID : {AssetTypeId}");
                return null;
            }
            var customAssetCategories = _dbFunctionService.UdfAssetCategory(tenant.Id).ToList();
            var assetCategory = customAssetCategories.FirstOrDefault(type => type.Id == assetType.AssetCategoryId);
            if (assetCategory == null)
            {
                _logger.LogWarning($"Asset type category detail not found. Loan Application ID : {LoanApplicationId}");
                return null;
            }
            var borrowerAssetBinder = borrower.AssetBorrowerBinders.FirstOrDefault(binder => binder.BorrowerAssetId == BorrowerAssetId);

            if (borrowerAssetBinder != null)
            {
                modelToReturn = new ProceedsFromNonRealState()
                {
                    Id = borrowerAssetBinder.BorrowerAsset.Id,
                    Description = borrowerAssetBinder.BorrowerAsset.Description,
                    AssetTypeId = borrowerAssetBinder.BorrowerAsset.AssetTypeId,
                    AsstTypeName = assetType.Name,
                    AssetTypeCategoryName = assetCategory.Name,
                    Value = borrowerAssetBinder.BorrowerAsset.Value
                };

            }



            return modelToReturn;

        }

        public async Task<int> AddOrUpdateAssestsNonRealState(TenantModel tenant, int userId, AddOrUpdateAssetModelNonRealState model)
        {
            List<BorrowerAssetCategory> allowedCategories = new List<BorrowerAssetCategory>()
            {
                BorrowerAssetCategory.ProceedsFromTransactions
            };

            if (!allowedCategories.Contains((BorrowerAssetCategory)model.AssetCategoryId))
            {
                return AssetCategoryNotAllowed;
            }


            var loanApplication = Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == model.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(app => app.Borrowers).ThenInclude(b => b.AssetBorrowerBinders).ThenInclude(binder => binder.BorrowerAsset)
                .FirstOrDefault();

            if (loanApplication == null)
            {
                _logger.LogWarning($"Loan application not found. Loan Application ID : {model.LoanApplicationId} User ID : {userId}");
                return LoanApplicationNotFound;
            }

            var borrower = loanApplication.Borrowers.FirstOrDefault(b => b.Id == model.BorrowerId);
            if (borrower == null)
            {
                _logger.LogWarning($"Borrower detail not found. Loan Application ID : {model.LoanApplicationId} Borrower ID: {model.BorrowerId}");
                return LoanApplicationNotFound;
            }

            var customAssetTypes = _dbFunctionService.UdfAssetType(tenant.Id).ToList();
            var assetType = customAssetTypes.FirstOrDefault(type => type.Id == model.AssetTypeId);
            if (assetType == null)
            {
                _logger.LogWarning($"Asset type detail not found. Loan Application ID : {model.LoanApplicationId} Asset Type ID : {model.AssetTypeId}");
                return AssetTypeNotFound;
            }

            var customAssetCategories = _dbFunctionService.UdfAssetCategory(tenant.Id).ToList();
            //var assetCategory = customAssetTypes.FirstOrDefault(type => type.Id == model.AssetTypeId);
            var assetCategory = customAssetCategories.FirstOrDefault(type => type.Id == model.AssetCategoryId);
            if (assetCategory == null)
            {
                _logger.LogWarning($"Asset type category detail not found. Loan Application ID : {model.LoanApplicationId} Asset Type Category ID : {model.AssetCategoryId}");
                return AssetTypeNotFound;
            }




            BorrowerAsset borrowerAsset = null;
            var borrowerAssetBinder = borrower.AssetBorrowerBinders.FirstOrDefault(binder => binder.BorrowerAssetId == model.BorrowerAssetId);
            if (borrowerAssetBinder == null)
            {
                borrowerAsset = new BorrowerAsset()
                {
                    Description = model.Description,
                    AssetTypeId = model.AssetTypeId,
                    Value = model.AssetValue,
                    TenantId = tenant.Id,
                    TrackingState = TrackingState.Added
                };
                borrowerAssetBinder = new AssetBorrowerBinder()
                {
                    BorrowerId = model.BorrowerId,
                    BorrowerAsset = borrowerAsset,
                    TenantId = tenant.Id,
                    TrackingState = TrackingState.Added
                };
                Uow.Repository<BorrowerAsset>().Insert(borrowerAsset);
                Uow.Repository<AssetBorrowerBinder>().Insert(borrowerAssetBinder);
            }
            else
            {
                borrowerAssetBinder.BorrowerAsset.Description = model.Description;
                borrowerAssetBinder.BorrowerAsset.AssetTypeId = model.AssetTypeId;
                borrowerAssetBinder.BorrowerAsset.Value = model.AssetValue;
                borrowerAssetBinder.BorrowerAsset.TenantId = tenant.Id;
                borrowerAssetBinder.BorrowerAsset.TenantId = tenant.Id;
                borrowerAssetBinder.TrackingState = TrackingState.Modified;

                Uow.Repository<AssetBorrowerBinder>().Update(borrowerAssetBinder);





            }

            loanApplication.State = model.State;
            loanApplication.TrackingState = TrackingState.Modified;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
            await Uow.SaveChangesAsync();

            return borrowerAsset != null ? borrowerAsset.Id : model.BorrowerAssetId;


        }

        public async Task<int> AddOrUpdateAssestsRealState(TenantModel tenant, int userId, AddOrUpdateAssetModelRealState model)
        {
            List<BorrowerAssetCategory> allowedCategories = new List<BorrowerAssetCategory>()
            {
                BorrowerAssetCategory.ProceedsFromTransactions
            };

            if (!allowedCategories.Contains((BorrowerAssetCategory)model.AssetCategoryId))
            {
                return AssetCategoryNotAllowed;
            }


            var loanApplication = Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == model.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(app => app.Borrowers).ThenInclude(b => b.AssetBorrowerBinders).ThenInclude(binder => binder.BorrowerAsset)
                .FirstOrDefault();

            if (loanApplication == null)
            {
                _logger.LogWarning($"Loan application not found. Loan Application ID : {model.LoanApplicationId} User ID : {userId}");
                return LoanApplicationNotFound;
            }

            var borrower = loanApplication.Borrowers.FirstOrDefault(b => b.Id == model.BorrowerId);
            if (borrower == null)
            {
                _logger.LogWarning($"Borrower detail not found. Loan Application ID : {model.LoanApplicationId} Borrower ID: {model.BorrowerId}");
                return LoanApplicationNotFound;
            }

            var customAssetTypes = _dbFunctionService.UdfAssetType(tenant.Id).ToList();
            var assetType = customAssetTypes.FirstOrDefault(type => type.Id == model.AssetTypeId);
            if (assetType == null)
            {
                _logger.LogWarning($"Asset type detail not found. Loan Application ID : {model.LoanApplicationId} Asset Type ID : {model.AssetTypeId}");
                return AssetTypeNotFound;
            }

            var customAssetCategories = _dbFunctionService.UdfAssetCategory(tenant.Id).ToList();
            //var assetCategory = customAssetTypes.FirstOrDefault(type => type.Id == model.AssetTypeId);
            var assetCategory = customAssetCategories.FirstOrDefault(type => type.Id == model.AssetCategoryId); // doubt on fix.. need to confirm
            if (assetCategory == null)
            {
                _logger.LogWarning($"Asset type category detail not found. Loan Application ID : {model.LoanApplicationId} Asset Type Category ID : {model.AssetCategoryId}");
                return AssetTypeNotFound;
            }




            BorrowerAsset borrowerAsset = null;
            var borrowerAssetBinder = borrower.AssetBorrowerBinders.FirstOrDefault(binder => binder.BorrowerAssetId == model.BorrowerAssetId);
            if (borrowerAssetBinder == null)
            {
                borrowerAsset = new BorrowerAsset()
                {
                    Description = model.Description,
                    AssetTypeId = model.AssetTypeId,
                    Value = model.AssetValue,
                    TenantId = tenant.Id,
                    TrackingState = TrackingState.Added
                };
                borrowerAssetBinder = new AssetBorrowerBinder()
                {
                    BorrowerId = model.BorrowerId,
                    BorrowerAsset = borrowerAsset,
                    TenantId = tenant.Id,
                    TrackingState = TrackingState.Added
                };
                Uow.Repository<BorrowerAsset>().Insert(borrowerAsset);
                Uow.Repository<AssetBorrowerBinder>().Insert(borrowerAssetBinder);
            }
            else
            {
                borrowerAssetBinder.BorrowerAsset.Description = model.Description;
                borrowerAssetBinder.BorrowerAsset.AssetTypeId = model.AssetTypeId;
                borrowerAssetBinder.BorrowerAsset.Value = model.AssetValue;
                borrowerAssetBinder.BorrowerAsset.TenantId = tenant.Id;
                borrowerAssetBinder.BorrowerAsset.TenantId = tenant.Id;
                borrowerAssetBinder.TrackingState = TrackingState.Modified;

                Uow.Repository<AssetBorrowerBinder>().Update(borrowerAssetBinder);





            }

            loanApplication.State = model.State;
            loanApplication.TrackingState = TrackingState.Modified;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
            await Uow.SaveChangesAsync();

            return borrowerAsset != null ? borrowerAsset.Id : model.BorrowerAssetId;


        }


        public async Task<ProceedsFromRealState> GetFromLoanRealState(TenantModel tenant, int userId, int BorrowerAssetId, int AssetTypeId, int BorrowerId, int LoanApplicationId)
        {
            ProceedsFromRealState modelToReturn = null;
            var loanApplication = Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                        .Query(x => x.Id == LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                        .Include(app => app.Borrowers).ThenInclude(b => b.AssetBorrowerBinders).ThenInclude(binder => binder.BorrowerAsset)
                        .FirstOrDefault();


            if (loanApplication == null)
            {
                _logger.LogWarning($"Loan application not found. Loan Application ID : {LoanApplicationId} User ID : {userId}");
                return null;
            }

            var borrower = loanApplication.Borrowers.FirstOrDefault(b => b.Id == BorrowerId);
            if (borrower == null)
            {
                _logger.LogWarning($"Borrower detail not found. Loan Application ID : {LoanApplicationId} Borrower ID: {BorrowerId}");
                return null;
            }

            var customAssetTypes = _dbFunctionService.UdfAssetType(tenant.Id).ToList();
            var assetType = customAssetTypes.FirstOrDefault(type => type.Id == AssetTypeId);
            if (assetType == null)
            {
                _logger.LogWarning($"Asset type detail not found. Loan Application ID : {LoanApplicationId} Asset Type ID : {AssetTypeId}");
                return null;
            }
            var customAssetCategories = _dbFunctionService.UdfAssetCategory(tenant.Id).ToList();
            var assetCategory = customAssetCategories.FirstOrDefault(type => type.Id == assetType.AssetCategoryId);
            if (assetCategory == null)
            {
                _logger.LogWarning($"Asset type category detail not found. Loan Application ID : {LoanApplicationId}");
                return null;
            }


            var borrowerAssetBinder = borrower.AssetBorrowerBinders.FirstOrDefault(binder => binder.BorrowerAssetId == BorrowerAssetId);

            if (borrowerAssetBinder != null)
            {
                modelToReturn = new ProceedsFromRealState()
                {
                    Id = borrowerAssetBinder.BorrowerAsset.Id,
                    Description = borrowerAssetBinder.BorrowerAsset.Description,
                    AssetTypeId = borrowerAssetBinder.BorrowerAsset.AssetTypeId,
                    AsstTypeName = assetType.Name,
                    AssetTypeCategoryName = assetCategory.Name,
                    Value = borrowerAssetBinder.BorrowerAsset.Value
                };

            }



            return modelToReturn;

        }

        public async Task<int> AddOrUpdateProceedsfromloan(TenantModel tenant, int userId, ProceedFromLoanModel model)
        {
            List<BorrowerAssetCategory> allowedCategories = new List<BorrowerAssetCategory>()
            {
                BorrowerAssetCategory.ProceedsFromTransactions
            };
            List<BorrowerAssetCollateral> allowedCollateral = new List<BorrowerAssetCollateral>()
            {
                BorrowerAssetCollateral.House,
                BorrowerAssetCollateral.Automobile,
                BorrowerAssetCollateral.FinancialAccount

            };
            if (!allowedCategories.Contains((BorrowerAssetCategory)model.AssetCategoryId))
            {
                return AssetCategoryNotAllowed;
            }

            if (model.SecuredByColletral == true)
            {
                if (!allowedCollateral.Contains((BorrowerAssetCollateral)model.ColletralAssetTypeId) && model.ColletralAssetTypeId != 0)
                {
                    return AssetCollateralNotAllowed;
                }
            }
           

            var loanApplication = Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == model.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(app => app.Borrowers).ThenInclude(b => b.AssetBorrowerBinders).ThenInclude(binder => binder.BorrowerAsset)
                .FirstOrDefault();

            if (loanApplication == null)
            {
                _logger.LogWarning($"Loan application not found. Loan Application ID : {model.LoanApplicationId} User ID : {userId}");
                return LoanApplicationNotFound;
            }

            var borrower = loanApplication.Borrowers.FirstOrDefault(b => b.Id == model.BorrowerId);
            if (borrower == null)
            {
                _logger.LogWarning($"Borrower detail not found. Loan Application ID : {model.LoanApplicationId} Borrower ID: {model.BorrowerId}");
                return LoanApplicationNotFound;
            }

            var customAssetTypes = _dbFunctionService.UdfAssetType(tenant.Id).ToList();
            var assetType = customAssetTypes.FirstOrDefault(type => type.Id == model.AssetTypeId);
            if (assetType == null)
            {
                _logger.LogWarning($"Asset type detail not found. Loan Application ID : {model.LoanApplicationId} Asset Type ID : {model.AssetTypeId}");
                return AssetTypeNotFound;
            }

            var customAssetCategories = _dbFunctionService.UdfAssetCategory(tenant.Id).ToList();
            //var assetCategory = customAssetTypes.FirstOrDefault(type => type.Id == model.AssetTypeId);
            var assetCategory = customAssetCategories.FirstOrDefault(type => type.Id == model.AssetCategoryId); // doubt on fix.. need to confirm
            if (assetCategory == null)
            {
                _logger.LogWarning($"Asset type category detail not found. Loan Application ID : {model.LoanApplicationId} Asset Type Category ID : {model.AssetCategoryId}");
                return AssetTypeNotFound;
            }




            BorrowerAsset borrowerAsset = null;
            var borrowerAssetBinder = borrower.AssetBorrowerBinders.FirstOrDefault(binder => binder.BorrowerAssetId == model.BorrowerAssetId);
            if (borrowerAssetBinder == null)
            {
                if (model.SecuredByColletral == true)
                {
                    borrowerAsset = new BorrowerAsset()
                    {

                        CollateralAssetTypeId = model.ColletralAssetTypeId,
                        SecuredByCollateral = model.SecuredByColletral,
                        AssetTypeId = model.AssetTypeId,
                        Value = model.AssetValue,
                        TenantId = tenant.Id,
                        TrackingState = TrackingState.Added
                    };
                }
                else
                {
                    borrowerAsset = new BorrowerAsset()
                    {
                        SecuredByCollateral = model.SecuredByColletral,
                        AssetTypeId = model.AssetTypeId,
                        Value = model.AssetValue,
                        TenantId = tenant.Id,
                        TrackingState = TrackingState.Added
                    };
                }


                borrowerAssetBinder = new AssetBorrowerBinder()
                {
                    BorrowerId = model.BorrowerId,
                    BorrowerAsset = borrowerAsset,
                    TenantId = tenant.Id,
                    TrackingState = TrackingState.Added
                };
                Uow.Repository<BorrowerAsset>().Insert(borrowerAsset);
                Uow.Repository<AssetBorrowerBinder>().Insert(borrowerAssetBinder);
            }
            else
            {
                borrowerAssetBinder.BorrowerAsset.CollateralAssetTypeId = model.ColletralAssetTypeId;
                borrowerAssetBinder.BorrowerAsset.SecuredByCollateral = model.SecuredByColletral;
                borrowerAssetBinder.BorrowerAsset.CollateralAssetDescription = model.CollateralAssetDescription;
                borrowerAssetBinder.BorrowerAsset.AssetTypeId = model.AssetTypeId;
                borrowerAssetBinder.BorrowerAsset.Value = model.AssetValue;
                borrowerAssetBinder.BorrowerAsset.TenantId = tenant.Id;
                borrowerAssetBinder.BorrowerAsset.TenantId = tenant.Id;
                borrowerAssetBinder.TrackingState = TrackingState.Modified;

                Uow.Repository<AssetBorrowerBinder>().Update(borrowerAssetBinder);


            }

            loanApplication.State = model.State;
            loanApplication.TrackingState = TrackingState.Modified;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
            await Uow.SaveChangesAsync();

            return borrowerAsset != null ? borrowerAsset.Id : model.BorrowerAssetId;


        }

        public async Task<int> AddOrUpdateProceedsfromloanOther(TenantModel tenant, int userId, ProceedFromLoanOtherModel model)
        {
            List<BorrowerAssetCategory> allowedCategories = new List<BorrowerAssetCategory>()
            {
                BorrowerAssetCategory.ProceedsFromTransactions
            };
            List<BorrowerAssetCollateral> allowedCollateral = new List<BorrowerAssetCollateral>()
            {
                BorrowerAssetCollateral.Other

            };
            if (!allowedCategories.Contains((BorrowerAssetCategory)model.AssetCategoryId))
            {
                return AssetCategoryNotAllowed;
            }
            if (!allowedCollateral.Contains((BorrowerAssetCollateral)model.ColletralAssetTypeId))
            {
                return AssetCollateralNotAllowed;
            }

            var loanApplication = Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == model.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(app => app.Borrowers).ThenInclude(b => b.AssetBorrowerBinders).ThenInclude(binder => binder.BorrowerAsset)
                .FirstOrDefault();

            if (loanApplication == null)
            {
                _logger.LogWarning($"Loan application not found. Loan Application ID : {model.LoanApplicationId} User ID : {userId}");
                return LoanApplicationNotFound;
            }

            var borrower = loanApplication.Borrowers.FirstOrDefault(b => b.Id == model.BorrowerId);
            if (borrower == null)
            {
                _logger.LogWarning($"Borrower detail not found. Loan Application ID : {model.LoanApplicationId} Borrower ID: {model.BorrowerId}");
                return LoanApplicationNotFound;
            }

            var customAssetTypes = _dbFunctionService.UdfAssetType(tenant.Id).ToList();
            var assetType = customAssetTypes.FirstOrDefault(type => type.Id == model.AssetTypeId);
            if (assetType == null)
            {
                _logger.LogWarning($"Asset type detail not found. Loan Application ID : {model.LoanApplicationId} Asset Type ID : {model.AssetTypeId}");
                return AssetTypeNotFound;
            }

            var customAssetCategories = _dbFunctionService.UdfAssetCategory(tenant.Id).ToList();
            //var assetCategory = customAssetTypes.FirstOrDefault(type => type.Id == model.AssetTypeId);
            var assetCategory = customAssetCategories.FirstOrDefault(type => type.Id == model.AssetCategoryId); // doubt on fix.. need to confirm
            if (assetCategory == null)
            {
                _logger.LogWarning($"Asset type category detail not found. Loan Application ID : {model.LoanApplicationId} Asset Type Category ID : {model.AssetCategoryId}");
                return AssetTypeNotFound;
            }




            BorrowerAsset borrowerAsset = null;
            var borrowerAssetBinder = borrower.AssetBorrowerBinders.FirstOrDefault(binder => binder.BorrowerAssetId == model.BorrowerAssetId);
            if (borrowerAssetBinder == null)
            {

                borrowerAsset = new BorrowerAsset()
                {
                    CollateralAssetTypeId = model.ColletralAssetTypeId,
                    SecuredByCollateral = true,
                    CollateralAssetDescription = model.CollateralAssetDescription,
                    AssetTypeId = model.AssetTypeId,
                    Value = model.AssetValue,
                    TenantId = tenant.Id,
                    TrackingState = TrackingState.Added
                };


                borrowerAssetBinder = new AssetBorrowerBinder()
                {
                    BorrowerId = model.BorrowerId,
                    BorrowerAsset = borrowerAsset,
                    TenantId = tenant.Id,
                    TrackingState = TrackingState.Added
                };
                Uow.Repository<BorrowerAsset>().Insert(borrowerAsset);
                Uow.Repository<AssetBorrowerBinder>().Insert(borrowerAssetBinder);
            }
            else
            {

                borrowerAssetBinder.BorrowerAsset.CollateralAssetDescription = model.CollateralAssetDescription;
                borrowerAssetBinder.BorrowerAsset.CollateralAssetTypeId = model.ColletralAssetTypeId;
                borrowerAssetBinder.BorrowerAsset.SecuredByCollateral = true;
                borrowerAssetBinder.BorrowerAsset.AssetTypeId = model.AssetTypeId;
                borrowerAssetBinder.BorrowerAsset.Value = model.AssetValue;
                borrowerAssetBinder.BorrowerAsset.TenantId = tenant.Id;
                borrowerAssetBinder.BorrowerAsset.TenantId = tenant.Id;
                borrowerAssetBinder.TrackingState = TrackingState.Modified;

                Uow.Repository<AssetBorrowerBinder>().Update(borrowerAssetBinder);


            }

            loanApplication.State = model.State;
            loanApplication.TrackingState = TrackingState.Modified;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
            await Uow.SaveChangesAsync();

            return borrowerAsset != null ? borrowerAsset.Id : model.BorrowerAssetId;


        }

        public async Task<ProceedsFromLoan> GetProceedsfromloan(TenantModel tenant, int userId, int BorrowerAssetId, int AssetTypeId, int BorrowerId, int LoanApplicationId)
        {
            ProceedsFromLoan modelToReturn = null;
            var loanApplication = Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                        .Query(x => x.Id == LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                        .Include(app => app.Borrowers).ThenInclude(b => b.AssetBorrowerBinders).ThenInclude(binder => binder.BorrowerAsset)
                        .FirstOrDefault();


            if (loanApplication == null)
            {
                _logger.LogWarning($"Loan application not found. Loan Application ID : {LoanApplicationId} User ID : {userId}");
                return null;
            }

            var borrower = loanApplication.Borrowers.FirstOrDefault(b => b.Id == BorrowerId);
            if (borrower == null)
            {
                _logger.LogWarning($"Borrower detail not found. Loan Application ID : {LoanApplicationId} Borrower ID: {BorrowerId}");
                return null;
            }

            var customAssetTypes = _dbFunctionService.UdfAssetType(tenant.Id).ToList();
            var assetType = customAssetTypes.FirstOrDefault(type => type.Id == AssetTypeId);
            if (assetType == null)
            {
                _logger.LogWarning($"Asset type detail not found. Loan Application ID : {LoanApplicationId} Asset Type ID : {AssetTypeId}");
                return null;
            }

            var customAssetCategories = _dbFunctionService.UdfAssetCategory(tenant.Id).ToList();
            var assetCategory = customAssetCategories.FirstOrDefault(type => type.Id == assetType.AssetCategoryId);
            if (assetCategory == null)
            {
                _logger.LogWarning($"Asset type category detail not found. Loan Application ID : {LoanApplicationId}");
                return null;
            }



            var borrowerAssetBinder = borrower.AssetBorrowerBinders.FirstOrDefault(binder => binder.BorrowerAssetId == BorrowerAssetId);
            var borrowerCollateralAssetType = _dbFunctionService.UdfCollateralAssetType(tenant.Id).ToList();
            var customCollateralAsstType = borrowerCollateralAssetType.Where(x => x.Id == borrowerAssetBinder.BorrowerAsset.CollateralAssetTypeId).FirstOrDefault();
            if (borrowerAssetBinder != null)
            {
                modelToReturn = new ProceedsFromLoan()
                {
                    Id = borrowerAssetBinder.BorrowerAsset.Id,
                    BorrowerId = BorrowerId,
                    AssetTypeId = borrowerAssetBinder.BorrowerAsset.AssetTypeId,
                    AsstTypeName = assetType.Name,
                    AssetTypeCategoryName = assetCategory.Name,
                    Value = borrowerAssetBinder.BorrowerAsset.Value,
                    CollateralAssetTypeId = borrowerAssetBinder.BorrowerAsset.CollateralAssetTypeId,
                    CollateralAssetName = customCollateralAsstType != null ? customCollateralAsstType.Name : null,
                    CollateralAssetOtherDescription = borrowerAssetBinder.BorrowerAsset.CollateralAssetDescription,
                    SecuredByCollateral = borrowerAssetBinder.BorrowerAsset.SecuredByCollateral,

                };

            }



            return modelToReturn;

        }

        public async Task<List<GetGiftSourceAssetsModel>> GetGiftSourceAssets(TenantModel tenant, int giftSourceId)
        {
            List<GetGiftSourceAssetsModel> modelToReturn = new List<GetGiftSourceAssetsModel>();

            var tenantAssets = _dbFunctionService.UdfAssetType(tenant.Id).ToList();
            var tenantCategories = _dbFunctionService.UdfAssetCategory(tenant.Id).ToList();

            var giftSourceAssets = await Uow.Repository<AssetTypeGiftSourceBinder>()
                .Query(source => source.GiftSourceId == giftSourceId)
                .ToListAsync();

            // Filter gift source asset by tenant enabled asset types
            giftSourceAssets = (from source in giftSourceAssets
                                join tenantAsset in tenantAssets on source.AssetTypeId equals tenantAsset.Id
                                select source
                    )
                .ToList();

            foreach (var giftAsset in giftSourceAssets)
            {
                var assetTypeDetail = tenantAssets.FirstOrDefault(asset => asset.Id == giftAsset.AssetTypeId);
                if (assetTypeDetail != null)
                {
                    var categoryToSet = tenantCategories.FirstOrDefault(cat => cat.Id == assetTypeDetail.AssetCategoryId);
                    assetTypeDetail.AssetCategory = categoryToSet;

                    var itemToAdd = new GetGiftSourceAssetsModel()
                    {
                        AssetTypeId = assetTypeDetail.Id,
                        Name = assetTypeDetail.Name,
                        DisplayName = assetTypeDetail.DisplayName,
                        //CategoryDetail = new GetGiftSourceAssetCategoryModel()
                        //{
                        //    DisplayName = assetTypeDetail.AssetCategory?.DisplayName,
                        //    CategoryId = assetTypeDetail.AssetCategory?.Id,
                        //    Name = assetTypeDetail.AssetCategory?.Name
                        //}
                    };
                    if (assetTypeDetail.AssetCategory != null)
                    {
                        itemToAdd.CategoryDetail = new GetGiftSourceAssetCategoryModel()
                        {
                            DisplayName = assetTypeDetail.AssetCategory.DisplayName,
                            CategoryId = assetTypeDetail.AssetCategory.Id,
                            Name = assetTypeDetail.AssetCategory.Name
                        };
                    }
                    modelToReturn.Add(itemToAdd);
                }
            }

            return modelToReturn;
        }

        public List<GetCollateralAssetTypesModel> GetCollateralAssetTypes(TenantModel tenant)
        {
            List<GetCollateralAssetTypesModel> modelToReturn = new List<GetCollateralAssetTypesModel>();

            var collateralTypes = _dbFunctionService.UdfCollateralAssetType(tenant.Id).ToList();

            foreach (var colType in collateralTypes)
            {
                var itemToAdd = new GetCollateralAssetTypesModel()
                {
                    DisplayName = colType.DisplayName,
                    Id = colType.Id,
                    Name = colType.DisplayName
                };
                modelToReturn.Add(itemToAdd);
            }

            return modelToReturn;
        }

        public async Task<GetAllAssetsForHomeScreenModel> GetAllAssetsForHomeScreen(TenantModel tenant,
                                                                                      int userId,
                                                                                      int loanApplicationId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                                           .Query(query: application => application.Id == loanApplicationId && application.UserId == userId && application.TenantId == tenant.Id)
                                           .Include(application => application.Borrowers).ThenInclude(borrower => borrower.AssetBorrowerBinders).ThenInclude(binder => binder.BorrowerAsset)
                                           .Include(application => application.Borrowers).ThenInclude(borrower => borrower.LoanContact_LoanContactId)
                                           .SingleAsync();

            var model = new GetAllAssetsForHomeScreenModel();
            model.Borrowers = new List<GetAllAssetsForHomeScreenModel.Borrower>();


            var assetTypes = _dbFunctionService.UdfAssetType(tenant.Id).ToList();
            var assetCategory = _dbFunctionService.UdfAssetCategory(tenant.Id).ToList();

            var borrowerAssets = loanApplication.Borrowers.SelectMany(b => b.AssetBorrowerBinders).Select(binder => binder.BorrowerAsset).ToList();
            borrowerAssets.ForEach(asset => asset.AssetType = assetTypes.Single(assetType => assetType.Id == asset.AssetTypeId));

            borrowerAssets.ForEach(asset => asset.AssetType.AssetCategory = assetCategory.Single(assetCategory => assetCategory.Id == asset.AssetType.AssetCategoryId));

            var tenantOwnTypes = _dbFunctionService.UdfOwnType(tenant.Id).ToList();

            foreach (var borrower in loanApplication.Borrowers)
            {
                borrower.OwnType = tenantOwnTypes.FirstOrDefault(x => x.Id == borrower.OwnTypeId);
                var borrowerIncome = new GetAllAssetsForHomeScreenModel.Borrower();
                model.Borrowers.Add(item: borrowerIncome);
                borrowerIncome.BorrowerName = $"{borrower.LoanContact_LoanContactId.FirstName}";
                borrowerIncome.BorrowerId = borrower.Id;
                borrowerIncome.OwnTypeId = borrower.OwnTypeId;
                borrowerIncome.OwnTypeName = borrower.OwnType?.Name;
                borrowerIncome.OwnTypeDisplayName = borrower.OwnType?.DisplayName;
                borrowerIncome.BorrowerAssets = new List<GetAllAssetsForHomeScreenModel.BorrowerAsset>();
                foreach (var asset in borrower.AssetBorrowerBinders.Select(binder => binder.BorrowerAsset))
                {
                    var borrowerAsset = new GetAllAssetsForHomeScreenModel.BorrowerAsset();
                    borrowerAsset.AssetName = asset.InstitutionName ?? asset.AssetType.DisplayName;
                    borrowerAsset.AssetValue = asset.Value;
                    borrowerAsset.AssetId = asset.Id;
                    borrowerAsset.AssetTypeID = asset.AssetType.Id;
                    borrowerAsset.AssetCategoryId = asset.AssetType.AssetCategoryId;
                    borrowerAsset.AssetCategoryName = asset.AssetType.AssetCategory.DisplayName;
                    borrowerAsset.AssetTypeName = asset.AssetType.DisplayName;
                    borrowerIncome.BorrowerAssets.Add(borrowerAsset);
                }
            }

            model.Borrowers.ForEach(action: borrower =>
            {
                borrower.AssetsValue = borrower.BorrowerAssets.Sum(selector: asset => asset.AssetValue);
            });
            model.TotalAssetsValue = model.Borrowers.Sum(selector: borrowerIncome => borrowerIncome.AssetsValue);

            return model;
        }


        public async Task<object> GetOtherAssetInfo(TenantModel tenant,
                                                    int userId,
                                                    int assetId)
        {
            var borrowerAsset = await Uow.Repository<BorrowerAsset>()
                                         .Query(query: asset => asset.Id == assetId && asset.AssetBorrowerBinders.Any(binder => binder.Borrower.LoanApplication.UserId == userId) && asset.TenantId == tenant.Id)
                                         .SingleAsync();

            borrowerAsset.AssetType = _dbFunctionService.UdfAssetType(tenantId: tenant.Id).Single(predicate: assetType => assetType.Id == borrowerAsset.AssetTypeId);

            var models = new GetOtherAssetInfo
            {
                AssetId = borrowerAsset.Id,
                AssetTypeId = borrowerAsset.AssetType.Id,
                AssetTypeName = borrowerAsset.AssetType.DisplayName,
                AssetValue = borrowerAsset.Value,
                AssetDescription = borrowerAsset.Description,
                InstitutionName = borrowerAsset.InstitutionName,
                AccountNumber = borrowerAsset.AccountNumber

            };

            //review comment list is not required here only single, tenant id check missing 

            return models;
        }


        public async Task<object> AddOrUpdateOtherAssetInfo(TenantModel tenant,
                                                            int userId,
                                                            AddOrUpdateOtherAssetsInfoModel model)
        {
            var borrowerAsset = await Uow.Repository<BorrowerAsset>()
                                         .Query(query: asset => asset.Id == model.AssetId && asset.AssetBorrowerBinders.Any(b => b.Borrower.LoanApplication.UserId == userId) && asset.TenantId == tenant.Id)
                                         .SingleOrDefaultAsync();

            if (borrowerAsset == null)
            {
                borrowerAsset = new BorrowerAsset();
                borrowerAsset.TrackingState = TrackingState.Added;

                var binder = new AssetBorrowerBinder();
                binder.BorrowerAsset = borrowerAsset;
                binder.BorrowerId = model.BorrowerId;

                borrowerAsset.AssetBorrowerBinders.Add(item: binder);
            }
            else
            {
                borrowerAsset.TrackingState = TrackingState.Modified;
            }

            borrowerAsset.InstitutionName = model.InstitutionName;
            borrowerAsset.AccountNumber = model.AccountNumber;
            borrowerAsset.Value = model.Value;
            borrowerAsset.Description = model.Description;
            borrowerAsset.AssetTypeId = model.AssetTypeId.ToInt();
            borrowerAsset.TenantId = tenant.Id;

            Uow.Repository<BorrowerAsset>().ApplyChanges(borrowerAsset);
            await Uow.SaveChangesAsync();
            return borrowerAsset.Id;
        }


        public async Task<GetBorrowerWithAssetsForReviewModel> GetBorrowerAssetsForReview(TenantModel tenant,
                                                                                          int userId,
                                                                                          int loanApplicationId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                                           .Query(query: application => application.Id == loanApplicationId && application.UserId == userId && application.TenantId == tenant.Id)
                                           .Include(application => application.Borrowers).ThenInclude(borrower => borrower.AssetBorrowerBinders).ThenInclude(binder => binder.BorrowerAsset)
                                           .Include(application => application.Borrowers).ThenInclude(borrower => borrower.LoanContact_LoanContactId)
                                           .SingleAsync();



            if (loanApplication == null)
            {
                return new GetBorrowerWithAssetsForReviewModel()
                {
                    ErrorMessage = ErrorMessages[LoanApplicationNotFound]
                };
            }
            List<int> borrowerAssetsTypes = new List<int>();
            var tenantAssetsTypes = _dbFunctionService.UdfAssetType(tenant.Id);
            var tenantAssetsCategories = _dbFunctionService.UdfAssetCategory(tenant.Id);
            //var tenantIncomeCategories = _dbFunctionService.UdfIncomeCategory(tenant.Id);
            var tenantOwnTypes = _dbFunctionService.UdfOwnType(tenant.Id);

            GetBorrowerWithAssetsForReviewModel modelToReturn = new GetBorrowerWithAssetsForReviewModel()
            {
                Borrowers = new List<AssetsReviewBorrowerModel>()
            };

            foreach (var borrower in loanApplication.Borrowers)
            {
                borrower.OwnType = tenantOwnTypes.FirstOrDefault(ownType => ownType.Id == borrower.OwnTypeId);
                //AssetsReviewBorrowerModel borrowerToAdd = new AssetsReviewBorrowerModel()
                //{
                //    BorrowerId = borrower.Id,
                //    BorrowerName = $"{borrower.LoanContact.FirstName} {borrower.LoanContact.LastName}",
                //    OwnType = new AssetsReviewOwnType()
                //    {
                //        DisplayName = borrower.OwnType.DisplayName,
                //        Id = borrower.OwnType.Id,
                //        Name = borrower.OwnType.DisplayName
                //    }
                //};
                //modelToReturn.Borrowers.Add(borrowerToAdd);

                foreach (var assetsBinder in borrower.AssetBorrowerBinders)
                {
                    var assetTypeToSet = tenantAssetsTypes.FirstOrDefault(type => type.Id == assetsBinder.BorrowerAsset.AssetTypeId);
                    var categoryTypeToSet = tenantAssetsCategories.FirstOrDefault(cat => cat.Id == assetTypeToSet.AssetCategoryId);
                    if (assetTypeToSet != null)
                    {
                        assetTypeToSet.AssetCategory = categoryTypeToSet;
                    }
                    assetsBinder.BorrowerAsset.AssetType = assetTypeToSet;
                    if (assetsBinder.BorrowerAsset.AssetType != null && (!borrowerAssetsTypes.Contains(assetsBinder.BorrowerAsset.AssetType.Id)))
                    {
                        borrowerAssetsTypes.Add(assetsBinder.BorrowerAsset.AssetType.Id);
                    }


                }
            }

            foreach (var assetType in borrowerAssetsTypes)
            {
                foreach (var borrower in loanApplication.Borrowers)
                {
                    AssetsReviewBorrowerModel borrowerToAdd = new AssetsReviewBorrowerModel()
                    {
                        BorrowerId = borrower.Id,
                        BorrowerName = $"{borrower.LoanContact_LoanContactId.FirstName} {borrower.LoanContact_LoanContactId.LastName}",
                        OwnType = new AssetsReviewOwnType()
                        {
                            DisplayName = borrower.OwnType.DisplayName,
                            Id = borrower.OwnType.Id,
                            Name = borrower.OwnType.DisplayName
                        }
                    };
                    modelToReturn.Borrowers.Add(borrowerToAdd);
                    var borrowerAssetIds = borrower.AssetBorrowerBinders.Select(asset => asset.BorrowerAssetId).ToList();
                    var borrowerAssetsDetail = Uow.Repository<BorrowerAsset>()
                                                  .Query(asset => borrowerAssetIds.Contains(asset.Id) && asset.AssetTypeId == assetType)
                                                  .ToList();
                    if (borrowerAssetsDetail.Count > 0)
                    {

                        var assetsTypeInfo = tenantAssetsTypes.FirstOrDefault(type => type.Id == assetType);
                        var assetsCategory = tenantAssetsCategories.FirstOrDefault(cat => cat.Id == assetsTypeInfo.AssetCategoryId);
                        AssetsReviewAssetsType typeToAdd = new AssetsReviewAssetsType()
                        {
                            DisplayName = assetsTypeInfo.DisplayName,
                            Id = assetsTypeInfo.Id,
                            Name = assetsTypeInfo.Name,
                            AssetsCategory = new AssetsReviewAssetsCategory()
                            {
                                DisplayName = assetsCategory.DisplayName,
                                Id = assetsCategory.Id,
                                Name = assetsCategory.Name
                            },
                            AssetsList = new List<AssetsReviewList>()
                        };

                        if (borrowerToAdd.AssetsTypes == null)
                        {
                            borrowerToAdd.AssetsTypes = new List<AssetsReviewAssetsType>();
                        }
                        borrowerToAdd.AssetsTypes.Add(typeToAdd);


                        foreach (var assetsInfoWithType in borrowerAssetsDetail)
                        {
                            var assetsToAdd = new AssetsReviewList();
                            assetsToAdd.AssetsInfo = new AssetsInfoForReview()
                            {
                                Id = assetsInfoWithType.Id,
                                Value = assetsInfoWithType.Value,
                                UseForPayment = assetsInfoWithType.UseForDownpayment,
                                ValueDate = assetsInfoWithType.ValueDate,
                                InstitutionName = assetsInfoWithType.InstitutionName,

                                AccountNumber = assetsInfoWithType.AccountNumber,
                                AccountTitle = assetsInfoWithType.AccountTitle,
                                AssetsTypeId = assetsInfoWithType.AssetTypeId,
                                CollateralAssetDescription = assetsInfoWithType.CollateralAssetDescription,
                                SecuredByCollateral = assetsInfoWithType.SecuredByCollateral
                            };



                            typeToAdd.AssetsList.Add(assetsToAdd);
                        }

                    }


                }
            }
            return modelToReturn;
        }


        public async Task<int> DeleteAsset(TenantModel tenant, int userId, AssetDeleteModel model)
        {

            var loanApplication = Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                                     .Query(x => x.Id == model.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                                     .Include(app => app.Borrowers).ThenInclude(b => b.AssetBorrowerBinders).ThenInclude(binder => binder.BorrowerAsset)
                                     .FirstOrDefault();

            if (loanApplication == null)
            {
                _logger.LogWarning(message: $"Loan application not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
                return LoanApplicationNotFound;
            }

            var borrowerInfo = loanApplication.Borrowers
                                              .FirstOrDefault(predicate: b => b.Id == model.BorrowerId);
            if (borrowerInfo == null)
            {
                _logger.LogWarning(message: $"Borrower not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
                return BorrowerDetailNotFound;
            }
            if (borrowerInfo.AssetBorrowerBinders == null || borrowerInfo.AssetBorrowerBinders.Count == 0)
            {
                _logger.LogWarning(message: $"No Assets is associates with borrower. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
                return BorrowerAssetDetailNotFound;
            }
            var assetbinderToDelete = borrowerInfo.AssetBorrowerBinders.FirstOrDefault(binder => binder.BorrowerAssetId == model.AssetId);

            if (assetbinderToDelete == null)
            {
                _logger.LogWarning(message: $"Borrower Asset detail not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId} Income Info ID : {model.AssetId}");
                return BorrowerAssetDetailNotFound;
            }

            var assetToDelete = assetbinderToDelete.BorrowerAsset;
            assetbinderToDelete.TrackingState = TrackingState.Deleted;
            if (assetToDelete != null)
            {
                Uow.Repository<AssetBorrowerBinder>().Delete(assetbinderToDelete);
             

            }
            Uow.Repository<BorrowerAsset>().Delete(assetToDelete);
           
      
            await Uow.SaveChangesAsync();
            return 1;

        }
    }
}
