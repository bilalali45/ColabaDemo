using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApplication.Model
{
    public class CurrentResidenceModel
    {
        public int LoanApplicationId { get; set; }
        public int? Id { get; set; }
        public int BorrowerId { get; set; }
        public decimal? PropertyValue { get; set; }
        public decimal? OwnersDue { get; set; }
        public bool? IsSelling { get; set; }
        public string State { get; set; }
    }

    public class CurrentResidenceRequestModel
    {
        public int BorrowerResidenceId { get; set; }
        public int LoanApplicationId { get; set; }
        public System.DateTime? FromDate { get; set; }
        public string State { get; set; }
    }

    public class CurrentResidenceResponseModel
    {
        public int? Id { get; set; }
        public int LoanApplicationId { get; set; }
        public System.DateTime? FromDate { get; set; }
    }

    public class CoApplicantDetails
    {
        public System.DateTime? PrimaryBorrowerFromDate { get; set; }
        public List<SecondaryBorrowerDetail> SecondaryBorrowerDetailList { get; set; }

    }
     public class SecondaryBorrowerDetail
    {
        public int BorrowerId { get; set; }
        public int BorrowerResidenceId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSameAsPrimaryAddress { get; set; }
        public System.DateTime? FromDate { get; set; }
        public GenericAddressModel AddressModel { get; set; }
    }

    public class BorrowerResidenceHistory
    {
        public int BorrowerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ResidenceHistoryModel
    {
        public int LoanApplicationId { get; set; }
        public bool RequiresHistory { get; set; }
        public List<BorrowerResidenceHistory> BorrowerResidenceHistory { get; set; }
    }


    public class BorrowersDetail
    {
        public int BorrowerId { get; set; }
        public int TypeId { get; set; }
        public int BorrowerResidenceId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime? FromDate { get; set; }
        public System.DateTime? ToDate { get; set; }
        public GenericAddressModel AddressModel { get; set; }
    }

    public class BorrowerCitizenshipRequestModel
    {
        public int BorrowerId { get; set; }
        public int LoanApplicationId { get; set; }
        public int? ResidencyTypeId { get; set; }
        public int? ResidencyStateId { get; set; }
        public string ResidencyStateExplanation { get; set; }
        public string State { get; set; }
    }

    public class BorrowerCitizenshipResponseModel
    {
        public int BorrowerId { get; set; }
        public int LoanApplicationId { get; set; }
        public int? ResidencyTypeId { get; set; }
        public int? ResidencyStateId { get; set; }
        public string ResidencyStateExplanation { get; set; }

    }

    public class BorrowerSecondaryAddressRequestModel
    {
        public int BorrowerId { get; set; }
        public int? BorrowerResidenceId { get; set; }
        public int LoanApplicationId { get; set; }
        public System.DateTime? FromDate { get; set; }
        public System.DateTime? ToDate { get; set; }
        public int HousingStatusId { get; set; }
        public decimal? MonthlyRent { get; set; }
        public GenericAddressModel AddressModel { get; set; }
        public string State { get; set; }
    }

    public class BorrowerSecondaryAddressResponseModel
    {
        public int BorrowerId { get; set; }
        public int LoanApplicationId { get; set; }
        public System.DateTime? FromDate { get; set; }
        public System.DateTime? ToDate { get; set; }
        public int HousingStatusId { get; set; }
        public decimal? MonthlyRent { get; set; }
        public GenericAddressModel AddressModel { get; set; }
        public string State { get; set; }
    }


    public class SpouseInfo
    {
        public int BorrowerId { get; set; }
        public int? SpouseLoanContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class SpouseInfoRequestModel
    {
        public int BorrowerId { get; set; }
        public int? SpouseLoanContactId { get; set; }
        public int LoanApplicationId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string State { get; set; }
    }

    public class SpouseInfoResponseModel
    {
        public int BorrowerId { get; set; }
        public int? SpouseLoanContactId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
    }

    public class ReviewSpouseInfo
    {
        public int BorrowerId { get; set; }
        public int OwnTypeId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<ReviewAddressModel> AddressModel { get; set; }
        public string ResidencyName { get; set; }
        public string DependentAge { get; set; }

    }
}
