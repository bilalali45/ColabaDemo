using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApplication.Model
{
    public class RefinancePropertyModel
    {

        public class PropertyAddressModel
        {
            public bool? IsSameAsPropertyAddress { get; set; }
            public GenericAddressModel Address { get; set; }

        }

        public class SameAsPropertyAddress
        {
            public int BorrowerId { get; set; }
            public int BorrowerResidenceId { get; set; }
            public int LoanApplicationId { get; set; }
            public string State { get; set; }
            public bool IsSameAsPropertyAddress { get; set; }
        }


        public class PropertyUsageModel
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public bool? WillLiveIn { get; set; }
        }

        public class AddPropertyUsagerequestModel
        {
            public int LoanApplicationId { get; set; }

            public decimal? RentalIncome { get; set; }
            public bool? IsMixedUseProperty { get; set; }
            public string IsMixedUsePropertyExplanation { get; set; }

            public int PropertyUsageId { get; set; }
            public List<PropertyUsageModel> Borrowers { get; set; }
            public string State { get; set; }
        }

        public class SubjectPropertyDetails
        {
            public System.DateTime? DateAcquired { get; set; }
            public decimal? HoaDues { get; set; }
            public decimal? PropertyValue { get; set; }
        }

        public class SubjectPropertyDetailsRequestModel
        {
            public int LoanApplicationId { get; set; }
            public System.DateTime? DateAcquired { get; set; }
            public decimal? HoaDues { get; set; }
            public decimal? PropertyValue { get; set; }
            public string State { get; set; }
        }

        public class HasMortgageModel
        {
            public bool? HasFirstMortgage { get; set; }
            public decimal? PropertyTax { get; set; }
            public decimal? HomeOwnerInsurance { get; set; }
            public decimal? FloodInsurance { get; set; }
            public int LoanApplicationId { get; set; }
           // public int Id { get; set; }
            public string State { get; set; }
        }
    }
}
