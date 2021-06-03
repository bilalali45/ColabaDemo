using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LoanApplication.Model
{
    public class AssetTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class BorrowerAssetModelRequest 
    {
        public int? Id { get; set; }
        public int AssetTypeId { get; set; } 
        [Required(ErrorMessage = "Institution Name Is Required")]
        public string InstitutionName { get; set; }
        [Required(ErrorMessage = "Account Number Is Required")]
        public string AccountNumber { get; set; }
        [Required(ErrorMessage = "Balance Is Required")]
        [Range(1,int.MaxValue,ErrorMessage = "Invalid Balance")]
        public decimal? Balance { get; set; } 
        public int LoanApplicationId { get; set; }
        public string State { get; set; }
        public int BorrowerId { get; set; }

    }

    public class BorrowerAssetModelResponse
    {
        public int? Id { get; set; }
        public int AssetTypeId { get; set; }
        public string InstitutionName { get; set; }
        public string AccountNumber { get; set; }
        public decimal? Balance { get; set; }
        public int LoanApplicationId { get; set; }
        public string State { get; set; }
    }

    public class GiftSourceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }

    public class RetirementAccountModel
    {
        public int? Id { get; set; }
        [Required]
        public int LoanApplicationId { get; set; } 
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Borrower Id")]
        public int BorrowerId { get; set; }  
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Value")]
        public decimal? Value { get; set; }  
        [Required]
        public string InstitutionName { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        public string State { get; set; }
    }

    public class GiftAssetModel 
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "BorrowerId Id Is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Borrower Id")]
        public int BorrowerId { get; set; }
        public int AssetTypeId { get; set; }
        [Required(ErrorMessage = "Gift Source Id Is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid GiftSource Id")]
        public int GiftSourceId { get; set; }
        public string Description { get; set; }
        public bool? IsDeposited { get; set; }
        public decimal? Value { get; set; }
        public DateTime? ValueDate { get; set; }
        public string State { get; set; }
        public int LoanApplicationId { get; set; }
    }

    public class AssetTypeFinancialModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class BorrowerAssetFinancialModelRequest 
    {
        public int? Id { get; set; }
        public int AssetTypeId { get; set; }
        [Required(ErrorMessage = "Institution Name Id Is Required")]
        public string InstitutionName { get; set; }
        [Required(ErrorMessage = "Account Number Id Is Required")]
        public string AccountNumber { get; set; }
        [Required(ErrorMessage = "Balance Id Is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Balance")]
        public decimal? Balance { get; set; }
        public int LoanApplicationId { get; set; }
        public string State { get; set; }
        [Required(ErrorMessage = "BorrowerId Id Is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Borrower Id")]
        public int BorrowerId { get; set; }
    }

    public class BorrowerAssetFinancialModelResponse
    {
        public int? Id { get; set; }
        public int AssetTypeId { get; set; }
        public string InstitutionName { get; set; }
        public string AccountNumber { get; set; }
        public decimal? Balance { get; set; }
        public int LoanApplicationId { get; set; }
        public string State { get; set; }
    }
}
