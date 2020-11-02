













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LockStatusCause

    public partial class LockStatusCause 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int? LockStatusId { get; set; } // LockStatusId
        public bool IsSystem { get; set; } // IsSystem
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child Opportunities where [Opportunity].[LockCauseId] point to this entity (FK_Opportunity_LockStatusCause)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Opportunity> Opportunities { get; set; } // Opportunity.FK_Opportunity_LockStatusCause
        /// <summary>
        /// Child OpportunityLockStatusLogs where [OpportunityLockStatusLog].[LockCauseId] point to this entity (FK_OpportunityLockStatusLog_LockStatusCause)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityLockStatusLog> OpportunityLockStatusLogs { get; set; } // OpportunityLockStatusLog.FK_OpportunityLockStatusLog_LockStatusCause

        // Foreign keys

        /// <summary>
        /// Parent LockStatusList pointed by [LockStatusCause].([LockStatusId]) (FK_LockStatusCause_LockStatusList)
        /// </summary>
        public virtual LockStatusList LockStatusList { get; set; } // FK_LockStatusCause_LockStatusList

        public LockStatusCause()
        {
            IsSystem = false;
            IsActive = true;
            EntityTypeId = 168;
            IsDeleted = false;
            Opportunities = new System.Collections.Generic.HashSet<Opportunity>();
            OpportunityLockStatusLogs = new System.Collections.Generic.HashSet<OpportunityLockStatusLog>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
