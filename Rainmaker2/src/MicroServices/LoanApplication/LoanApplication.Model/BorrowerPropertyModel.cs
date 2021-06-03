using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApplication.Model
{
    public class BorrowerPropertyRequestModel
    {
        public int? Id { get; set; }
        public int BorrowerId { get; set; }
        public int LoanApplicationId { get; set; }
        public int PropertyTypeId { get; set; }
        public decimal? RentalIncome { get; set; }
        public string State { get; set; }
    }

    public class BorrowerPropertyResponseModel
    {
        public int? Id { get; set; }
        public int PropertyTypeId { get; set; }
        public decimal? RentalIncome { get; set; }
       
    }

    public class BorrowerAdditionalPropertyRequestModel
    {
        public int? Id { get; set; }
        public int BorrowerId { get; set; }
        public int LoanApplicationId { get; set; }
        public int PropertyTypeId { get; set; }
        public string State { get; set; }
    }

    public class BorrowerAdditionalPropertyResponseModel
    {
        public int? Id { get; set; }
        public int PropertyTypeId { get; set; }
    }

    public class BorrowerAdditionalPropertyInfoRequestModel
    {
        public int? Id { get; set; }
        public int BorrowerId { get; set; }
        public int PropertyUsageId { get; set; }
        public decimal? RentalIncome { get; set; }
        public int LoanApplicationId { get; set; }
        public string State { get; set; }
    }

    public class BorrowerAdditionalPropertyInfoResponseModel
    {
        public int? Id { get; set; }
        public int PropertyUsageId { get; set; }
        public decimal? RentalIncome { get; set; }
    }




    public class BorrowerAdditionalPropertyAddressRequestModel
    {
        //[Required]
        public int LoanApplicationId { get; set; }
        public int BorrowerPropertyId { get; set; }
        public string Street { get; set; }
        public string Unit { get; set; }
        public string City { get; set; }
        public int? StateId { get; set; }
        public string ZipCode { get; set; }
        public int? CountryId { get; set; }
        public string State { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
    }

    public class BorrowerAdditionalPropertyAddressRsponseModel
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Unit { get; set; }
        public string City { get; set; }
        public int? StateId { get; set; }
        public string ZipCode { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
    }
   
}
