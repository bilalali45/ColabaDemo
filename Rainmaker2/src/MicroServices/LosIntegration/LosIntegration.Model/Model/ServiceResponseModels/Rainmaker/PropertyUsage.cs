
















namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // PropertyUsage

    public partial class PropertyUsage 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
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
        public int? LoanApplicationDisplayOrder { get; set; } // LoanApplicationDisplayOrder

        // Reverse navigation

        /// <summary>
        /// Child BankRatePropertyUsageBinders where [BankRatePropertyUsageBinder].[PropertyUsageId] point to this entity (FK_BankRatePropertyUsageBinder_PropertyUsage)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRatePropertyUsageBinder> BankRatePropertyUsageBinders { get; set; } // BankRatePropertyUsageBinder.FK_BankRatePropertyUsageBinder_PropertyUsage
        /// <summary>
        /// Child LoanRequests where [LoanRequest].[PropertyUsageId] point to this entity (FK_LoanRequest_PropertyUsage)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; } // LoanRequest.FK_LoanRequest_PropertyUsage
        /// <summary>
        /// Child PropertyInfoes where [PropertyInfo].[PropertyUsageId] point to this entity (FK_PropertyInfo_PropertyUsage)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PropertyInfo> PropertyInfoes { get; set; } // PropertyInfo.FK_PropertyInfo_PropertyUsage

        public PropertyUsage()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 16;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            BankRatePropertyUsageBinders = new System.Collections.Generic.HashSet<BankRatePropertyUsageBinder>();
            LoanRequests = new System.Collections.Generic.HashSet<LoanRequest>();
            PropertyInfoes = new System.Collections.Generic.HashSet<PropertyInfo>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
