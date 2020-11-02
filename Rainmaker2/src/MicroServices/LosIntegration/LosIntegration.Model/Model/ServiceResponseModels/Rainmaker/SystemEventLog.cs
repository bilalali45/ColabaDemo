













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // SystemEventLog

    public partial class SystemEventLog 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? VisitorId { get; set; } // VisitorId
        public int? UserId { get; set; } // UserId
        public int? SessionId { get; set; } // SessionId
        public int? EventTypeId { get; set; } // EventTypeId
        public string ResourceName { get; set; } // ResourceName (length: 50)
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string Remarks { get; set; } // Remarks
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsActive { get; set; } // IsActive
        public bool IsDeleted { get; set; } // IsDeleted
        public int? LoanRequestId { get; set; } // LoanRequestId
        public int? OpportunityId { get; set; } // OpportunityId

        // Foreign keys

        /// <summary>
        /// Parent SystemEvent pointed by [SystemEventLog].([EventTypeId]) (FK_SystemEventLog_SystemEvent)
        /// </summary>
        public virtual SystemEvent SystemEvent { get; set; } // FK_SystemEventLog_SystemEvent

        /// <summary>
        /// Parent UserProfile pointed by [SystemEventLog].([UserId]) (FK_SystemEventLog_UserProfile)
        /// </summary>
        public virtual UserProfile UserProfile { get; set; } // FK_SystemEventLog_UserProfile

        /// <summary>
        /// Parent Visitor pointed by [SystemEventLog].([VisitorId]) (FK_SystemEventLog_Visitor)
        /// </summary>
        public virtual Visitor Visitor { get; set; } // FK_SystemEventLog_Visitor

        public SystemEventLog()
        {
            EntityTypeId = 138;
            IsActive = true;
            IsDeleted = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
