using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LoanApplication.Model
{
    public class DependentModel
    {
        [Required(ErrorMessage = "BorrowerId Id Is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Borrower Id")]
        public int BorrowerId { get; set; }
        public int? LoanApplicationId { get; set; }
        public int? DependentCount { get; set; }
        public string DependentAges { get; set; }
        public string State { get; set; }
    }
}
