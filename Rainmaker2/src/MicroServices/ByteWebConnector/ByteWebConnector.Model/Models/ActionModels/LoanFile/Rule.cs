













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // Rule

    public partial class Rule 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int? RuleTypeId { get; set; } // RuleTypeId
        public string LoanPurposes { get; set; } // LoanPurposes
        public string PropertyUsages { get; set; } // PropertyUsages
        public string PropertyTypes { get; set; } // PropertyTypes
        public int? CreditScoreMin { get; set; } // CreditScoreMin
        public int? CreditScoreMax { get; set; } // CreditScoreMax
        public int? LockDaysMin { get; set; } // LockDaysMin
        public int? LockDaysMax { get; set; } // LockDaysMax
        public string States { get; set; } // States
        public string Counties { get; set; } // Counties
        public string ZipCode { get; set; } // ZipCode
        public string City { get; set; } // City
        public string GeoStates { get; set; } // GeoStates
        public string Regions { get; set; } // Regions
        public string Products { get; set; } // Products
        public string Investors { get; set; } // Investors

        ///<summary>
        /// this is Is second lean Check box
        ///</summary>
        public bool? SubOrdination { get; set; } // SubOrdination
        public int? PropertyValueMin { get; set; } // PropertyValueMin
        public int? PropertyValueMax { get; set; } // PropertyValueMax
        public int? LoanAmountMin { get; set; } // LoanAmountMin
        public int? LoanAmountMax { get; set; } // LoanAmountMax
        public decimal? LtvMin { get; set; } // LtvMin
        public decimal? LtvMax { get; set; } // LtvMax
        public decimal? CltvMin { get; set; } // CltvMin
        public decimal? CltvMax { get; set; } // CltvMax
        public decimal? HltvMin { get; set; } // HltvMin
        public decimal? HltvMax { get; set; } // HltvMax
        public int? CashOutAmountMin { get; set; } // CashOutAmountMin
        public int? CashOutAmountMax { get; set; } // CashOutAmountMax
        public int? FirstMortgageBalanceMin { get; set; } // FirstMortgageBalanceMin
        public int? FirstMortgageBalanceMax { get; set; } // FirstMortgageBalanceMax
        public int? TotalJuniorLienMin { get; set; } // TotalJuniorLienMin
        public int? TotalJuniorLienMax { get; set; } // TotalJuniorLienMax
        public int? MortgageToBePaidMin { get; set; } // MortgageToBePaidMin
        public int? MortgageToBePaidMax { get; set; } // MortgageToBePaidMax
        public int? MortgageToBeSubordinateMin { get; set; } // MortgageToBeSubordinateMin
        public int? MortgageToBeSubordinateMax { get; set; } // MortgageToBeSubordinateMax
        public decimal? DtiHousingMin { get; set; } // DtiHousingMin
        public decimal? DtiHousingMax { get; set; } // DtiHousingMax
        public decimal? DtiTotalMin { get; set; } // DtiTotalMin
        public decimal? DtiTotalMax { get; set; } // DtiTotalMax
        public bool? IsNewlyBuiltHome { get; set; } // IsNewlyBuiltHome
        public bool? EscrowWaiver { get; set; } // EscrowWaiver
        public string ResidencyTypes { get; set; } // ResidencyTypes
        public int? RentalPropertiesMin { get; set; } // RentalPropertiesMin
        public int? RentalPropertiesMax { get; set; } // RentalPropertiesMax
        public string DocTypes { get; set; } // DocTypes
        public string Statuses { get; set; } // Statuses
        public string StatusTypes { get; set; } // StatusTypes
        public string StatusCategories { get; set; } // StatusCategories
        public string LockStatuses { get; set; } // LockStatuses
        public string LockStatusTypes { get; set; } // LockStatusTypes
        public bool? IsAutoAssigned { get; set; } // IsAutoAssigned
        public bool? IsPicked { get; set; } // IsPicked
        public bool? IsEligible { get; set; } // IsEligible
        public bool? IsValid { get; set; } // IsValid
        public string LeadSources { get; set; } // LeadSources
        public string LeadSourceTypes { get; set; } // LeadSourceTypes
        public string LeadCreatedFrom { get; set; } // LeadCreatedFrom
        public System.DateTime? LeadCreatedDateUtcFrom { get; set; } // LeadCreatedDateUtcFrom
        public System.DateTime? LeadCreatedDateUtcTo { get; set; } // LeadCreatedDateUtcTo
        public string Causes { get; set; } // Causes
        public string Owners { get; set; } // Owners
        public string Teams { get; set; } // Teams
        public string Opportunities { get; set; } // Opportunities
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public System.DateTime? StartDateUtc { get; set; } // StartDateUtc
        public System.DateTime? EndDateUtc { get; set; } // EndDateUtc
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted
        public string XmlRule { get; set; } // XmlRule
        public string QueryFilter { get; set; } // QueryFilter (length: 1073741823)
        public string QueryViewName { get; set; } // QueryViewName (length: 150)
        public int? SecondLienBalanceMin { get; set; } // SecondLienBalanceMin
        public int? SecondLienBalanceMax { get; set; } // SecondLienBalanceMax
        public string AdSources { get; set; } // AdSources
        public string Branches { get; set; } // Branches
        public string BusinessUnits { get; set; } // BusinessUnits
        public string ProductLoanTypes { get; set; } // ProductLoanTypes
        public string ProductQualifiers { get; set; } // ProductQualifiers
        public string AusProcessingTypes { get; set; } // AusProcessingTypes
        public string AmortizationTypes { get; set; } // AmortizationTypes
        public string Agencies { get; set; } // Agencies
        public decimal? RateMin { get; set; } // RateMin
        public decimal? RateMax { get; set; } // RateMax
        public decimal? PriceMin { get; set; } // PriceMin
        public decimal? PriceMax { get; set; } // PriceMax
        public string LoanGoals { get; set; } // LoanGoals
        public string LeadTypes { get; set; } // LeadTypes
        public int? OpportunityStatusDateOffset { get; set; } // OpportunityStatusDateOffset
        public int? LastSearchDateOffset { get; set; } // LastSearchDateOffset
        public int? CreatedDateOffset { get; set; } // CreatedDateOffset
        public string ProductTypes { get; set; } // ProductTypes
        public string ProductFamilies { get; set; } // ProductFamilies
        public string ProductClasses { get; set; } // ProductClasses
        public string PrepayPenalties { get; set; } // PrepayPenalties
        public string ProductBestExs { get; set; } // ProductBestExs
        public string LoanIndexTypes { get; set; } // LoanIndexTypes
        public string QuestionsBoolYes { get; set; } // QuestionsBoolYes
        public string QuestionsBoolNo { get; set; } // QuestionsBoolNo
        public bool? IsPickedHighestBenchmarkRate { get; set; } // IsPickedHighestBenchmarkRate
        public bool? IsEmployeeLoggedIn { get; set; } // IsEmployeeLoggedIn
        public bool? IsDesireRateSelected { get; set; } // IsDesireRateSelected
        public System.DateTime? OpportunityCreatedDateUtcFrom { get; set; } // OpportunityCreatedDateUtcFrom
        public System.DateTime? OpportunityCreatedDateUtcTo { get; set; } // OpportunityCreatedDateUtcTo
        public int? ClosingDateOffset { get; set; } // ClosingDateOffset
        public string LeadGroups { get; set; } // LeadGroups

        // Reverse navigation

        /// <summary>
        /// Child Adjustments where [Adjustment].[RuleId] point to this entity (FK_Adjustment_Rule)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Adjustment> Adjustments { get; set; } // Adjustment.FK_Adjustment_Rule
        /// <summary>
        /// Child Campaigns where [Campaign].[RuleId] point to this entity (FK_Campaign_Rule)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Campaign> Campaigns { get; set; } // Campaign.FK_Campaign_Rule
        /// <summary>
        /// Child EmpAssignmentRuleBinders where [EmpAssignmentRuleBinder].[RuleId] point to this entity (FK_EmpAssignmentRuleBinder_Rule)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmpAssignmentRuleBinder> EmpAssignmentRuleBinders { get; set; } // EmpAssignmentRuleBinder.FK_EmpAssignmentRuleBinder_Rule
        /// <summary>
        /// Child FeeDetails where [FeeDetail].[RuleId] point to this entity (FK_FeeDetail_Rule)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FeeDetail> FeeDetails { get; set; } // FeeDetail.FK_FeeDetail_Rule
        /// <summary>
        /// Child LockDaysOnRules where [LockDaysOnRule].[RuleId] point to this entity (FK_LockDaysOnRule_Rule)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LockDaysOnRule> LockDaysOnRules { get; set; } // LockDaysOnRule.FK_LockDaysOnRule_Rule
        /// <summary>
        /// Child ProfitTables where [ProfitTable].[RuleId] point to this entity (FK_ProfitTable_Rule)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ProfitTable> ProfitTables { get; set; } // ProfitTable.FK_ProfitTable_Rule
        /// <summary>
        /// Child RuleMessages where [RuleMessage].[RuleId] point to this entity (FK_RuleMessage_Rule)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<RuleMessage> RuleMessages { get; set; } // RuleMessage.FK_RuleMessage_Rule
        /// <summary>
        /// Child TriggerValues where [TriggerValue].[RuleId] point to this entity (FK_TriggerValue_Rule)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TriggerValue> TriggerValues { get; set; } // TriggerValue.FK_TriggerValue_Rule

        public Rule()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 130;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            Adjustments = new System.Collections.Generic.HashSet<Adjustment>();
            Campaigns = new System.Collections.Generic.HashSet<Campaign>();
            EmpAssignmentRuleBinders = new System.Collections.Generic.HashSet<EmpAssignmentRuleBinder>();
            FeeDetails = new System.Collections.Generic.HashSet<FeeDetail>();
            LockDaysOnRules = new System.Collections.Generic.HashSet<LockDaysOnRule>();
            ProfitTables = new System.Collections.Generic.HashSet<ProfitTable>();
            RuleMessages = new System.Collections.Generic.HashSet<RuleMessage>();
            TriggerValues = new System.Collections.Generic.HashSet<TriggerValue>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
