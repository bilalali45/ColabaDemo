using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using LoanApplication.Model.CustomAttributes;

namespace LoanApplication.Model
{

    public class BorrowerDobSsnGet
    {
        public DateTime? DobUtc { get; set; }
        public string Ssn { get; set; }
    }


    public class BorrowerDobSsnAddOrUpdate
    {

        [Required]
        public int BorrowerId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int LoanApplicationId { get; set; }
        [DOBDateValidation]
        public DateTime? DobUtc { get; set; }
      
        [RegularExpression(@"^\*{3}-\*{2}-\*{4}|\d{3}-\d{2}-\d{4}$", ErrorMessage = "Invalid Social Security Number")]
        public string Ssn { get; set; }
        public string State { get; set; }
    }


    public class BorrowerDobSsnGetModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid application id")]
        public int LoanApplicationId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
    }

  public  enum TenantConfigSelection : int
    {
        PrimaryBorrower_DOB =1,
        PrimaryBorrower_SSN=2,
        CoBorrower_DOB=3,
        CoBorrower_SSN=4,
        PrimaryBorower_VeteranStatus=5,
        CoBorower_VeteranStatus=6,
        PrimaryBorrower_HomeNumber=7,
        PrimaryBorrower_CellNumber=8,
        PrimaryBorrower_WorkNumber=9,
        CoBorrower_EmailAddress=10,
        CoBorrower_HomeNumber=11,
        CoBorrower_CellNumber=12,
        CoBorrower_WorkNumber=13,
        AnyPartOfDownPaymentGift=14,
        IncomeSection=16,
        EmploymentHistorySection=17

    }

  public enum TenantConfigSelectionItem : byte
  {
      On = 1,
      Off = 2,
      Optional=3


    }
}
