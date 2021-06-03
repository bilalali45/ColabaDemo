using System.Collections.Generic;

namespace LoanApplication.Model
{
    public class LoanApplicationReview
    {
        public int LoanPurpose { get; set; }
        public bool ApplyingWithSomeone { get; set; }
        public List<BorrowerReview> BorrowerReviews { get; set; }
        public int? LoanGoal { get; set; }
    }

    public class LoanApplicationFirstReview
    {
        public int LoanPurpose { get; set; }
        public bool ApplyingWithSomeone { get; set; }
        public List<BorrowerReview> BorrowerReviews { get; set; }
        public int? LoanGoal { get; set; }
    }

    public class LoanApplicationSecondReview
    {
        public int LoanPurpose { get; set; }
        public bool ApplyingWithSomeone { get; set; }
        public List<BorrowerReview> BorrowerReviews { get; set; }
        public int? LoanGoal { get; set; }
        public string LoanGoalName { get; set; }
        public bool? PropertyIdentified { get; set; }
        
        public PropertyInfoReview PropertyInfo { get; set; }
    }

    public class PropertyInfoReview
    {
        public int? PropertyUsageId { get; set; }
        public int? PropertyTypeId { get; set; }
        public string PropertyTypeName { get; set; }
        public string PropertyUsageName { get; set; }

        public PropertyAddress AddressInfo { get; set; }
    }

    public class BorrowersFirstReviewModel
    {
        public List<BorrowerReview> BorrowerReviews { get; set; }
    }

    public class BorrowersSecondReviewModel
    {
        public List<BorrowerReview> BorrowerReviews { get; set; }
    }

    public class BorrowerReview
    {
        public BorrowerAddress BorrowerAddress { get; set; }
        public int BorrowerId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string WorkPhoneExt { get; set; }
        public bool? IsVaEligible { get; set; }
        public int? MaritalStatusId { get; set; }
        public string CellPhone { get; set; }
        public int? OwnTypeId { get; set; }
        public string OwnTypeName { get; set; }
    }
   

    public class BorrowerAddress
    {
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public int? CountyId { get; set; }
        public string CountyName { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public string UnitNo { get; set; }
    }
    public class PropertyAddress
    {
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public int? CountyId { get; set; }
        public string CountyName { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public string UnitNo { get; set; }
    }

}
