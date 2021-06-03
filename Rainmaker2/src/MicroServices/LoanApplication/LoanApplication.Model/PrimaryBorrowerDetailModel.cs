using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApplication.Model
{
    public class BorrowerPrimaryAddressDetailModel
    {
        public int BorrowerResidenceId { get; set; }

        public GenericAddressModel Address { get; set; }

    }
    public class PrimaryBorrowerDetailModel
    {
        public int BorrowerId { get; set; }

        public int HousingStatusId { get; set; }
        public GenericAddressModel Address { get; set; }

    }
    public class HasSecondMortgageModel
    {
        public bool HasSecondMortgage { get; set; }
        public int LoanApplicationId { get; set; }
        public int Id { get; set; }
        public string State { get; set; }
    }
    public class HasFirstMortgageModel
    {
        public bool? HasFirstMortgage { get; set; }
        public decimal? PropertyTax { get; set; }
        public decimal? HomeOwnerInsurance { get; set; }
        public decimal? FloodInsurance { get; set; }
        public int LoanApplicationId { get; set; }
        public int Id { get; set; }
        public string State { get; set; }
    }
    public class MyPropertyModel
    {
        public int Id { get; set; }
        public string PropertyType { get; set; }
        public int TypeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int OwnTypeId { get; set; }
        public GenericAddressModel Address { get; set; }

    }
}
