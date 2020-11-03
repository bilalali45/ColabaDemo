













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // RateServiceParameter

    public partial class RateServiceParameter 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public int? LeadSourceId { get; set; } // LeadSourceId
        public int? LoanRequestId { get; set; } // LoanRequestId
        public int? ProductFamilyId { get; set; } // ProductFamilyId
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public int EntityTypeId { get; set; } // EntityTypeId

        ///<summary>
        /// Assumption string on customer home page
        ///</summary>
        public int? StringResourceId { get; set; } // StringResourceId
        public bool IsDeleted { get; set; } // IsDeleted
        public bool IsActive { get; set; } // IsActive

        // Reverse navigation

        /// <summary>
        /// Child CurrentRates where [CurrentRate].[RateParameterId] point to this entity (FK_CurrentRate_RateServiceParameter)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CurrentRate> CurrentRates { get; set; } // CurrentRate.FK_CurrentRate_RateServiceParameter
        /// <summary>
        /// Child RateArchives where [RateArchive].[RateParameterId] point to this entity (FK_RateArchive_RateServiceParameter)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<RateArchive> RateArchives { get; set; } // RateArchive.FK_RateArchive_RateServiceParameter

        // Foreign keys

        /// <summary>
        /// Parent BusinessUnit pointed by [RateServiceParameter].([BusinessUnitId]) (FK_RateServiceParameter_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_RateServiceParameter_BusinessUnit

        /// <summary>
        /// Parent LeadSource pointed by [RateServiceParameter].([LeadSourceId]) (FK_RateServiceParameter_LeadSource)
        /// </summary>
        public virtual LeadSource LeadSource { get; set; } // FK_RateServiceParameter_LeadSource

        /// <summary>
        /// Parent LoanRequest pointed by [RateServiceParameter].([LoanRequestId]) (FK_RateServiceParameter_LoanRequest)
        /// </summary>
        public virtual LoanRequest LoanRequest { get; set; } // FK_RateServiceParameter_LoanRequest

        /// <summary>
        /// Parent LocaleStringResource pointed by [RateServiceParameter].([StringResourceId]) (FK_RateServiceParameter_LocaleStringResource)
        /// </summary>
        public virtual LocaleStringResource LocaleStringResource { get; set; } // FK_RateServiceParameter_LocaleStringResource

        /// <summary>
        /// Parent ProductFamily pointed by [RateServiceParameter].([ProductFamilyId]) (FK_RateServiceParameter_ProductFamily)
        /// </summary>
        public virtual ProductFamily ProductFamily { get; set; } // FK_RateServiceParameter_ProductFamily

        public RateServiceParameter()
        {
            EntityTypeId = 55;
            IsDeleted = false;
            IsActive = true;
            CurrentRates = new System.Collections.Generic.HashSet<CurrentRate>();
            RateArchives = new System.Collections.Generic.HashSet<RateArchive>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
