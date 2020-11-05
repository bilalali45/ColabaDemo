













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // StatusList

    public partial class StatusList 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public bool? IsSystemInputOnly { get; set; } // IsSystemInputOnly
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted
        public int TypeId { get; set; } // TypeId
        public string CustomerDisplay { get; set; } // CustomerDisplay (length: 150)
        public string EmployeeDisplay { get; set; } // EmployeeDisplay (length: 150)
        public int CategoryId { get; set; } // CategoryId
        public bool CanLockOpportunity { get; set; } // CanLockOpportunity

        // Reverse navigation

        /// <summary>
        /// Child LoanApplications where [LoanApplication].[StatusId] point to this entity (FK_LoanApplication_StatusList)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanApplication> LoanApplications { get; set; } // LoanApplication.FK_LoanApplication_StatusList
        /// <summary>
        /// Child Opportunities where [Opportunity].[StatusId] point to this entity (FK_Opportunity_StatusList)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Opportunity> Opportunities { get; set; } // Opportunity.FK_Opportunity_StatusList
        /// <summary>
        /// Child OpportunityStatusLogs where [OpportunityStatusLog].[StatusId] point to this entity (FK_OpportunityStatusLog_StatusList)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityStatusLog> OpportunityStatusLogs { get; set; } // OpportunityStatusLog.FK_OpportunityStatusLog_StatusList
        /// <summary>
        /// Child StatusCauses where [StatusCause].[StatusId] point to this entity (FK_StatusCause_StatusList)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<StatusCause> StatusCauses { get; set; } // StatusCause.FK_StatusCause_StatusList
        /// <summary>
        /// Child WorkFlows where [WorkFlow].[StatusIdFrom] point to this entity (FK_WorkFlow_StatusListFrom)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<WorkFlow> WorkFlows_StatusIdFrom { get; set; } // WorkFlow.FK_WorkFlow_StatusListFrom
        /// <summary>
        /// Child WorkFlows where [WorkFlow].[StatusIdTo] point to this entity (FK_WorkFlow_StatusList)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<WorkFlow> WorkFlows_StatusIdTo { get; set; } // WorkFlow.FK_WorkFlow_StatusList

        public StatusList()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 135;
            IsDefault = false;
            IsSystem = false;
            IsSystemInputOnly = false;
            IsDeleted = false;
            CanLockOpportunity = false;
            LoanApplications = new System.Collections.Generic.HashSet<LoanApplication>();
            Opportunities = new System.Collections.Generic.HashSet<Opportunity>();
            OpportunityStatusLogs = new System.Collections.Generic.HashSet<OpportunityStatusLog>();
            StatusCauses = new System.Collections.Generic.HashSet<StatusCause>();
            WorkFlows_StatusIdFrom = new System.Collections.Generic.HashSet<WorkFlow>();
            WorkFlows_StatusIdTo = new System.Collections.Generic.HashSet<WorkFlow>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>