













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // BankRateInstance

    public partial class BankRateInstance 
    {
        public int Id { get; set; } // Id (Primary key)
        public string InstId { get; set; } // InstId (length: 50)
        public string InstanceName { get; set; } // InstanceName (length: 500)
        public int? StateId { get; set; } // StateId
        public int? CountyId { get; set; } // CountyId
        public decimal? LoanAmountMinLimit { get; set; } // LoanAmountMinLimit
        public decimal? LoanAmountMaxLimit { get; set; } // LoanAmountMaxLimit
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child BankRateParameters where [BankRateParameter].[InstanceId] point to this entity (FK_BankRateParameter_BankRateInstance)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRateParameter> BankRateParameters { get; set; } // BankRateParameter.FK_BankRateParameter_BankRateInstance

        // Foreign keys

        /// <summary>
        /// Parent County pointed by [BankRateInstance].([CountyId]) (FK_BankRateInstance_County)
        /// </summary>
        public virtual County County { get; set; } // FK_BankRateInstance_County

        /// <summary>
        /// Parent State pointed by [BankRateInstance].([StateId]) (FK_BankRateInstance_State)
        /// </summary>
        public virtual State State { get; set; } // FK_BankRateInstance_State

        public BankRateInstance()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 9;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            BankRateParameters = new System.Collections.Generic.HashSet<BankRateParameter>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
