using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LoanApplication.Model
{
    public class SelfBusinessModel
    {
        public int LoanApplicationId { get; set; }
        public int? Id { get; set; }
        public int BorrowerId { get; set; }
        [Required(ErrorMessage = "BusinessName Is Required")]
        public string BusinessName { get; set; }
        [Required(ErrorMessage = "BusinessPhone Is Required")]
        public string BusinessPhone { get; set; }
        [Required(ErrorMessage = "Business StartDate Is Required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Business Job Title Is Required")]
        public string JobTitle { get; set; }
        public GenericAddressModel Address { get; set; }
        public decimal AnnualIncome { get; set; }
        public string State { get; set; }
    }
    public class BusinessModel // todo reqiured and range is missing
    {
        public int LoanApplicationId { get; set; }
        public int? Id { get; set; }
        public int BorrowerId { get; set; }
        public int IncomeTypeId { get; set; }
        public string BusinessName { get; set; }
        public string BusinessPhone { get; set; }
        public DateTime StartDate { get; set; }
        public string JobTitle { get; set; }
        public decimal OwnershipPercentage { get; set; }
        public GenericAddressModel Address { get; set; }
        public decimal AnnualIncome { get; set; }
        public string State { get; set; }
    }
    public class BusinessTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class IncomeTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FieldsInfo { get; set; }
    }
    public class GenericAddressModel
    {
        public string Street { get; set; }
        public string Unit { get; set; }
        public string City { get; set; }
        public int? StateId { get; set; }
        public string ZipCode { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
    }

    public class ReviewAddressModel : GenericAddressModel
    {
        public int TypeId { get; set; }
        public int BorrowerResidenceId { get; set; }
    }
    public class AddPropertyTypeModel
    {
        public int LoanApplicationId { get; set; }
        public int PropertyTypeId { get; set; }
        public string State { get; set; }
    }

    public class PropertyUsageBorrowerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public bool? WillLiveIn { get; set; }
    }

    public class RefinancePropertyUsageBorrowerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public bool? IsMixedUseProperty { get; set; }
        public string IsMixedUsePropertyExplanation { get; set; }
        public bool? IsInvestmentProperty { get; set; }
        public decimal RentalIncome { get; set; }
    }

    public class GetPropertyUsageModel
    {
        public int PropertyUsageId { get; set; }
        public List<PropertyUsageBorrowerModel> Borrowers { get; set; }
    }

    public class GetRefinancePropertyUsageModel
    {
        public int? PropertyUsageId { get; set; }
        public decimal? RentalIncome { get; set; }
        public bool? IsMixedUseProperty { get; set; }
        public string MixedUsePropertyExplanation { get; set; }
        public int LoanApplicationId { get; set; }
        public string State { get; set; }
    }

    public class AddPropertyUsageModel
    {
        public int LoanApplicationId { get; set; }
        public int PropertyUsageId { get; set; }
        public List<PropertyUsageBorrowerModel> Borrowers { get; set; }
        public string State { get; set; }
    }


    public class AddPropertyUsageRefinanceModel
    {
        public int LoanApplicationId { get; set; }
        public int PropertyUsageId { get; set; }
        public decimal? RentalIncome { get; set; }
        public bool? IsMixedUseProperty { get; set; }
        public string IsMixedUsePropertyExplanation { get; set; }
        public string State { get; set; }
    }

    public class AddBorrowerModel
    {
        public int Id { get; set; }
        public int OwnTypeId { get; set; }
    }
    public class GetBorrowerMaritalStatusModel
    {
        public int? MaritalStatus { get; set; }
        public int? RelationshipWithPrimary { get; set; }
        public bool? MarriedToPrimary { get; set; }
    }
    public class DeleteBorrowerModel
    {
        public int LoanApplicationId { get; set; }
        public int BorrowerId { get; set; }
        public string State { get; set; }
    }

    public class BorrowerMaritalStatusModel
    {
        public int LoanApplicationId { get; set; }
        public int Id { get; set; }
        public int MaritalStatus { get; set; }
        //public int? RelationshipWithPrimary { get; set; }
        public string State { get; set; }
        public bool MarriedToPrimary { get; set; }
    }
    public class MaritalStatusModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ConsentTypeGetModel
    {
        public string ConsentHash { get; set; }
        public List<ConsentTypeModel> ConsentList { get; set; }
    }
    public class ConsentTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class BorrowerAddressModel
    {
        [Required]
        public int LoanApplicationId { get; set; }
        public int Id { get; set; }
        public string Street { get; set; }
        public string Unit { get; set; }
        public string City { get; set; }
        public int? StateId { get; set; }
        public string ZipCode { get; set; }
        public int? CountryId { get; set; }
        public int HousingStatusId { get; set; }
        public decimal? Rent { get; set; }
        public string State { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
    }
    public class GetPropertyAddressModel
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Unit { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string ZipCode { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public DateTime? EstimatedClosingDate { get; set; }
    }
    public class AddPropertyAddressModel
    {
        public int LoanApplicationId { get; set; }
        public string Street { get; set; }
        public string Unit { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string ZipCode { get; set; }
        public DateTime? EstimatedClosingDate { get; set; }
        public string State { get; set; }
    }
    public class AddressModel
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Unit { get; set; }
        public string City { get; set; }
        public int? StateId { get; set; }
        public string ZipCode { get; set; }
        public int? CountryId { get; set; }
        public int HousingStatusId { get; set; }
        public decimal? Rent { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
    }
    public class BorrowerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Suffix { get; set; }
        public string Email { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string WorkExt { get; set; }
        public string CellPhone { get; set; }
        public int? OwnTypeId { get; set; }
    }

    public class BorrowerInfoModel
    {
        [Required]
        public int LoanApplicationId { get; set; }
        public int? Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Suffix { get; set; }
        public string Email { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string WorkExt { get; set; }
        public string CellPhone { get; set; }
        public string State { get; set; }
    }

    public class TenantSetting
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
    public class PendingLoanApplicationModel
    {
        public int? LoanApplicationId { get; set; }
        public List<TenantSetting> Setting { get; set; }
        public string State { get; set; }
        public bool RestartLoanApplication { get; set; }
    }
    public class GetLoanGoalModel
    {
        public int LoanGoal { get; set; }
        public int LoanPurpose { get; set; }
    }
    public class AddOrUpdateLoanGoalModel
    {
        public int? LoanApplicationId { get; set; }
        [Required]
        public int LoanPurpose { get; set; }
        [Required]
        public int LoanGoal { get; set; }
        public string State { get; set; }
    }
    public class LoInfo
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string NmlsNo { get; set; }
        public bool IsLoanOfficer { get; set; }
    }

    public class LoanGoalModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
    public class LoanPurposeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
    public class PropertyTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
    public class PropertyUsageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
    public class ZipcodeModel
    {
        public string ZipPostalCode { get; set; }
    }
    public class CountryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortCode { get; set; }

    }
    public class StateModel
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string ShortCode { get; set; }
    }
    public class OwnershipTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class LoanCommentsModel
    {
        public int LoanApplicationId { get; set; }
        public string Comments { get; set; }
        public string State { get; set; }
    }

    public class MilitaryAffiliationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }



    public class GetBorrowerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int OwnTypeId { get; set; }
    }

    public class BorrowerAcceptedConsentsModel
    {
        public bool IsAccepted { get; set; }
        public List<ConsentTypeModel> AcceptedConsentList { get; set; }
    }

    public class EarnestMoneyDepositModel
    {
        public int LoanApplicationId { get; set; }
        public decimal? Deposit { get; set; }
        public string State { get; set; }
        public bool? IsEarnestMoneyProvided { get; set; }
    }



    public class BorrowerResidenceStatusModel
    {
        public int LoanApplicationId { get; set; }
        public int borrowerId { get; set; }
        public int borrowerResidenceId { get; set; }
        public int? OwnershipTypeId { get; set; }

        public bool? IsSameAsPropertyAddress { get; set; }

    }
}

