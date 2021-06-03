using System.ComponentModel.DataAnnotations;

namespace LoanApplication.Model
{
    public class AddOrUpdateOtherAssetsInfoModel 
    {
        public AssetTypes AssetTypeId { get; set; }
        [Required(ErrorMessage = "Value is Required")]
        public int Value { get; set; } 
        public int? AssetId { get; set; }
        public string InstitutionName { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Borrower Id is Required")]
        public int BorrowerId { get; set; } 
    }
}