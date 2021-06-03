using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace LoanApplication.Model
{
    public enum BorrowerAssetCategory : int
    {
        BankAccount = 1,
        FinancialStatement = 2,
        RetirementAccount = 3,
        GiftFunds = 4,
        Credits = 5,
        ProceedsFromTransactions = 6,
        Other = 7
    }
    public enum BorrowerAssetCollateral : int
    {
        House = 1,
        Automobile = 2,
        FinancialAccount = 3,
        Other = 4
      
    }
    public class GetAllAssetCategoriesModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string TenantAlternateName { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        [IgnoreDataMember]
        public string ImageUrl { get; set; }
    }

    public class GetAssetTypesByCategoryModel
    {
        public int Id { get; set; }
        public int AssetCategoryId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string TenantAlternateName { get; set; }
        public string FieldsInfo { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        [IgnoreDataMember]
        public string ImageUrl { get; set; }
    }

    public class AddOrUpdateBorrowerAssetModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
        public int? BorrowerAssetId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid asset type id")]
        public int AssetTypeId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid asset category id")]
        public int AssetCategoryId { get; set; }
        [Required(ErrorMessage = "Financial institution name is required")]
        public string InstitutionName { get; set; }
        [Required(ErrorMessage = "Account number is required")]
        public string AccountNumber { get; set; }
        [Required(ErrorMessage = "Borrower asset amount is required")]
        public decimal? AssetValue { get; set; }
        public string State { get; set; }
    }

    public class AssetGetModel
    {
        public int BorrowerAssetId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string AssetType { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? AssetTypeId { get; set; }
        public string AssetCategory { get; set; }
        public int? AssetCategoryId { get; set; }
        public string InstitutionName { get; set; }
        public string AccountNumber { get; set; }
        public decimal? AssetValue { get; set; }
    }

    public class BorrowerAssetsGetModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string ErrorMessage { get; set; }
        public int BorrowerId { get; set; }
        public string BorrowerName { get; set; }
        public List<AssetGetModel> BorrowerAssets { get; set; }
    }

    public class LoanApplicationBorrowersAssets
    {
        public int LoanApplicationId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string ErrorMessage { get; set; }
        public List<BorrowerAssetsGetModel> Borrowers { get; set; }
    }

    public class BorrowerAssetDetailGetModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower asset id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? BorrowerAssetId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string ErrorMessage { get; set; }
    }
	
	
	 public class ProceedsFromNonRealState

    {

        public int Id { get; set; }
        public int? AssetTypeId { get; set; }
        public string AsstTypeName { get; set; }
        public string AssetTypeCategoryName { get; set; }
        public string Description { get; set; }
        public decimal? Value { get; set; }
    }
    public class ProceedsFromRealState

    {

        public int Id { get; set; }
        public int? AssetTypeId { get; set; }
        public string AsstTypeName { get; set; }
        public string AssetTypeCategoryName { get; set; }
        public string Description { get; set; }
        public decimal? Value { get; set; }
    }
    public class ProceedsFromLoan

    {

        public int Id { get; set; }
        public int BorrowerId { get; set; }
        public int? AssetTypeId { get; set; }
        public string AsstTypeName { get; set; }
        public string AssetTypeCategoryName { get; set; }
        public string Description { get; set; }
        public decimal? Value { get; set; }
        public int? CollateralAssetTypeId { get; set; }
        public string CollateralAssetName { get; set; }
        public string CollateralAssetOtherDescription { get; set; }
        public bool? SecuredByCollateral { get; set; }

        


    }
    public class ProceedsFromNonRealStateModel
    {
        [Required(ErrorMessage = "Asset Type Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Borrower Asset Id ")]
        public int BorrowerAssetId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Invalid asset category id")]
        public int AssetCategoryId { get; set; }

        [Required(ErrorMessage = "Asset Type Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Asset Type Id")]
        public int AssetTypeId { get; set; }

        [Required(ErrorMessage = "BorrowerId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }

        [Required(ErrorMessage = "LoanApplicationId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Loan Application id")]
        public int LoanApplicationId { get; set; }


    }

    public class AddOrUpdateAssetModelNonRealState
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
        public int BorrowerAssetId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid asset type id")]
        public int AssetTypeId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid asset category id")]
        public int AssetCategoryId { get; set; }

        [Required(ErrorMessage = "Asset description is required")]
       
        public string Description { get; set; }

        [Required(ErrorMessage = "Borrower asset amount is required")]
        public decimal? AssetValue { get; set; }
        public string State { get; set; }
    }
    public class AddOrUpdateAssetModelRealState
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
        public int BorrowerAssetId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid asset type id")]
        public int AssetTypeId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid asset category id")]
        public int AssetCategoryId { get; set; }

        [Required(ErrorMessage = "Asset description is required")]

        public string Description { get; set; }

        [Required(ErrorMessage = "Borrower asset amount is required")]
        public decimal? AssetValue { get; set; }
        public string State { get; set; }
    }
    public class ProceedFromLoanModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
        public int BorrowerAssetId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid asset type id")]
        public int AssetTypeId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid asset category id")]
        public int AssetCategoryId { get; set; }


        [Required(ErrorMessage = "Borrower asset amount is required")]
        public decimal? AssetValue { get; set; }

        public int? ColletralAssetTypeId { get; set; }

        [Required(ErrorMessage = "Secured By Colletral is required")]
        public bool SecuredByColletral { get; set; }
        public string CollateralAssetDescription { get; set; }

        
        public string State { get; set; }
    }

    public class ProceedFromLoanOtherModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
        public int BorrowerAssetId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid asset type id")]
        public int AssetTypeId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid asset category id")]
        public int AssetCategoryId { get; set; }


        [Required(ErrorMessage = "Borrower asset amount is required")]
        public decimal? AssetValue { get; set; }

        [Required(ErrorMessage = "Colletral asset type id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid colletral asset type id")]
        public int ColletralAssetTypeId { get; set; }

        [Required(ErrorMessage = "Collateral Asset Description is required")]
        public string CollateralAssetDescription { get; set; }

   
        public string State { get; set; }
    }

    public enum AssetGiftSource : int
    {
        FederalAgency = 1,
        LocalAgency = 2,
        StateAgency = 3,
        Employer = 4,
        Lender = 5,
        CommunityNonProfit = 6,
        ReligiousNonProfit = 7,
        Relative = 8,
        UnmarriedPartner = 9
    }

    public class GetGiftSourceAssetsModel
    {
        public int AssetTypeId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public GetGiftSourceAssetCategoryModel CategoryDetail { get; set; }
    }

    public class GetGiftSourceAssetCategoryModel
    {
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public class GetCollateralAssetTypesModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
    public class GetBorrowerWithAssetsForReviewModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string ErrorMessage { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<AssetsReviewBorrowerModel> Borrowers { get; set; }
    }

    public class AssetsReviewBorrowerModel
    {
        public int BorrowerId { get; set; }
        public string BorrowerName { get; set; }
        public AssetsReviewOwnType OwnType { get; set; }
        public List<AssetsReviewAssetsType> AssetsTypes { get; set; }
    }
    public class AssetsReviewOwnType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
    public class AssetsReviewAssetsCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public class AssetsReviewAssetsType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public AssetsReviewAssetsCategory AssetsCategory { get; set; }
        public List<AssetsReviewList> AssetsList { get; set; }
    }

    public class AssetsReviewList
    {
        public AssetsInfoForReview AssetsInfo { get; set; }
      
    }
    public class AssetsInfoForReview
    {
        public int Id { get; set; }

        public decimal? Value { get; set; }
        public decimal? UseForPayment { get; set; }
        public DateTime? ValueDate { get; set; }
        
        public string InstitutionName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountTitle { get; set; }
       
        public int? AssetsTypeId { get; set; }
        public string CollateralAssetDescription { get; set; }
        public bool? SecuredByCollateral { get; set; }
        //public EmployerAddressForIncomeReview IncomeAddress { get; set; }
        //public WayOfIncomeForReview WayOfIncome { get; set; }
    }
}
