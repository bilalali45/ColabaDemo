













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // StatusCause

    public partial class StatusCause 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int? StatusId { get; set; } // StatusId
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
        /// Child Opportunities where [Opportunity].[StatusCauseId] point to this entity (FK_Opportunity_StatusCause)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Opportunity> Opportunities { get; set; } // Opportunity.FK_Opportunity_StatusCause
        /// <summary>
        /// Child OpportunityStatusLogs where [OpportunityStatusLog].[StatusCauseId] point to this entity (FK_OpportunityStatusLog_StatusCause)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityStatusLog> OpportunityStatusLogs { get; set; } // OpportunityStatusLog.FK_OpportunityStatusLog_StatusCause

        // Foreign keys

        /// <summary>
        /// Parent StatusList pointed by [StatusCause].([StatusId]) (FK_StatusCause_StatusList)
        /// </summary>
        public virtual StatusList StatusList { get; set; } // FK_StatusCause_StatusList

        public StatusCause()
        {
            IsSystem = false;
            IsActive = true;
            EntityTypeId = 126;
            IsDeleted = false;
            Opportunities = new System.Collections.Generic.HashSet<Opportunity>();
            OpportunityStatusLogs = new System.Collections.Generic.HashSet<OpportunityStatusLog>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
