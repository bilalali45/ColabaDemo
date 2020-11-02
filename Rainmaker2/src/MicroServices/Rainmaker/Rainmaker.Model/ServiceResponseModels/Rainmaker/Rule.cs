using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class Rule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? RuleTypeId { get; set; }
        public string LoanPurposes { get; set; }
        public string PropertyUsages { get; set; }
        public string PropertyTypes { get; set; }
        public int? CreditScoreMin { get; set; }
        public int? CreditScoreMax { get; set; }
        public int? LockDaysMin { get; set; }
        public int? LockDaysMax { get; set; }
        public string States { get; set; }
        public string Counties { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string GeoStates { get; set; }
        public string Regions { get; set; }
        public string Products { get; set; }
        public string Investors { get; set; }

        public bool? SubOrdination { get; set; }
        public int? PropertyValueMin { get; set; }
        public int? PropertyValueMax { get; set; }
        public int? LoanAmountMin { get; set; }
        public int? LoanAmountMax { get; set; }
        public decimal? LtvMin { get; set; }
        public decimal? LtvMax { get; set; }
        public decimal? CltvMin { get; set; }
        public decimal? CltvMax { get; set; }
        public decimal? HltvMin { get; set; }
        public decimal? HltvMax { get; set; }
        public int? CashOutAmountMin { get; set; }
        public int? CashOutAmountMax { get; set; }
        public int? FirstMortgageBalanceMin { get; set; }
        public int? FirstMortgageBalanceMax { get; set; }
        public int? TotalJuniorLienMin { get; set; }
        public int? TotalJuniorLienMax { get; set; }
        public int? MortgageToBePaidMin { get; set; }
        public int? MortgageToBePaidMax { get; set; }
        public int? MortgageToBeSubordinateMin { get; set; }
        public int? MortgageToBeSubordinateMax { get; set; }
        public decimal? DtiHousingMin { get; set; }
        public decimal? DtiHousingMax { get; set; }
        public decimal? DtiTotalMin { get; set; }
        public decimal? DtiTotalMax { get; set; }
        public bool? IsNewlyBuiltHome { get; set; }
        public bool? EscrowWaiver { get; set; }
        public string ResidencyTypes { get; set; }
        public int? RentalPropertiesMin { get; set; }
        public int? RentalPropertiesMax { get; set; }
        public string DocTypes { get; set; }
        public string Statuses { get; set; }
        public string StatusTypes { get; set; }
        public string StatusCategories { get; set; }
        public string LockStatuses { get; set; }
        public string LockStatusTypes { get; set; }
        public bool? IsAutoAssigned { get; set; }
        public bool? IsPicked { get; set; }
        public bool? IsEligible { get; set; }
        public bool? IsValid { get; set; }
        public string LeadSources { get; set; }
        public string LeadSourceTypes { get; set; }
        public string LeadCreatedFrom { get; set; }
        public DateTime? LeadCreatedDateUtcFrom { get; set; }
        public DateTime? LeadCreatedDateUtcTo { get; set; }
        public string Causes { get; set; }
        public string Owners { get; set; }
        public string Teams { get; set; }
        public string Opportunities { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public DateTime? StartDateUtc { get; set; }
        public DateTime? EndDateUtc { get; set; }
        public string TpId { get; set; }
        public bool IsDeleted { get; set; }
        public string XmlRule { get; set; }
        public string QueryFilter { get; set; }
        public string QueryViewName { get; set; }
        public int? SecondLienBalanceMin { get; set; }
        public int? SecondLienBalanceMax { get; set; }
        public string AdSources { get; set; }
        public string Branches { get; set; }
        public string BusinessUnits { get; set; }
        public string ProductLoanTypes { get; set; }
        public string ProductQualifiers { get; set; }
        public string AusProcessingTypes { get; set; }
        public string AmortizationTypes { get; set; }
        public string Agencies { get; set; }
        public decimal? RateMin { get; set; }
        public decimal? RateMax { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public string LoanGoals { get; set; }
        public string LeadTypes { get; set; }
        public int? OpportunityStatusDateOffset { get; set; }
        public int? LastSearchDateOffset { get; set; }
        public int? CreatedDateOffset { get; set; }
        public string ProductTypes { get; set; }
        public string ProductFamilies { get; set; }
        public string ProductClasses { get; set; }
        public string PrepayPenalties { get; set; }
        public string ProductBestExs { get; set; }
        public string LoanIndexTypes { get; set; }
        public string QuestionsBoolYes { get; set; }

        public string QuestionsBoolNo { get; set; }

        //        public bool? IsPickedHighestBenchmarkRate { get; set; }
        public bool? IsEmployeeLoggedIn { get; set; }
        public bool? IsDesireRateSelected { get; set; }
        public DateTime? OpportunityCreatedDateUtcFrom { get; set; }
        public DateTime? OpportunityCreatedDateUtcTo { get; set; }
        public int? ClosingDateOffset { get; set; }
        public string LeadGroups { get; set; }

        public ICollection<Adjustment> Adjustments { get; set; }

        public ICollection<Campaign> Campaigns { get; set; }

        public ICollection<EmpAssignmentRuleBinder> EmpAssignmentRuleBinders { get; set; }

        public ICollection<FeeDetail> FeeDetails { get; set; }

        public ICollection<LockDaysOnRule> LockDaysOnRules { get; set; }

        public ICollection<ProfitTable> ProfitTables { get; set; }

        public ICollection<RuleMessage> RuleMessages { get; set; }

        public ICollection<TriggerValue> TriggerValues { get; set; }
    }
}