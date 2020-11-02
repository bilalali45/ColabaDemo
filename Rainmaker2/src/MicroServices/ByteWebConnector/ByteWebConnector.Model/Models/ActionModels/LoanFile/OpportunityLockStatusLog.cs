













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // OpportunityLockStatusLog

    public partial class OpportunityLockStatusLog 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? LockStatusId { get; set; } // LockStatusId
        public int? LockCauseId { get; set; } // LockCauseId
        public System.DateTime? DatetimeUtc { get; set; } // DatetimeUtc
        public int? OpportunityId { get; set; } // OpportunityId
        public bool IsActive { get; set; } // IsActive
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int EntityTypeId { get; set; } // EntityTypeId

        // Foreign keys

        /// <summary>
        /// Parent LockStatusCause pointed by [OpportunityLockStatusLog].([LockCauseId]) (FK_OpportunityLockStatusLog_LockStatusCause)
        /// </summary>
        public virtual LockStatusCause LockStatusCause { get; set; } // FK_OpportunityLockStatusLog_LockStatusCause

        /// <summary>
        /// Parent LockStatusList pointed by [OpportunityLockStatusLog].([LockStatusId]) (FK_OpportunityLockStatusLog_LockStatusList)
        /// </summary>
        public virtual LockStatusList LockStatusList { get; set; } // FK_OpportunityLockStatusLog_LockStatusList

        /// <summary>
        /// Parent Opportunity pointed by [OpportunityLockStatusLog].([OpportunityId]) (FK_OpportunityLockStatusLog_Opportunity)
        /// </summary>
        public virtual Opportunity Opportunity { get; set; } // FK_OpportunityLockStatusLog_Opportunity

        public OpportunityLockStatusLog()
        {
            IsActive = true;
            EntityTypeId = 169;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
