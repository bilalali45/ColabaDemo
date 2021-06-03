using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LoanApplication.Model
{
    public class LoanApplicationIdModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id.")]
        public int LoanApplicationId { get; set; }
    }
    public class BorrowerIdModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id.")]
        public int BorrowerId { get; set; }
    }

    public class AssetCategoryIdModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid asset category id")]
        public int CategoryId { get; set; }
    }

    public class UpdateStateModel
    {
        [Required]
        public int LoanApplicationId { get; set; }

        [Required]
        public string State { get; set; }
    }
    public class AssetDeleteModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid income info id")]
        public int AssetId { get; set; }
    }

}
