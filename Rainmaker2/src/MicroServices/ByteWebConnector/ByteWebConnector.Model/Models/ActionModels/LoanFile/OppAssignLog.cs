













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // OppAssignLog

    public partial class OppAssignLog 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? OpportunityId { get; set; } // OpportunityId
        public int? EmployeeId { get; set; } // EmployeeId
        public System.DateTime? DateUtc { get; set; } // DateUtc
        public bool? IsAutoAssigned { get; set; } // IsAutoAssigned
        public int? AssignedById { get; set; } // AssignedById
        public bool? IsPicked { get; set; } // IsPicked
        public bool? IsBroadcast { get; set; } // IsBroadcast
        public bool? IsPitched { get; set; } // IsPitched

        // Foreign keys

        /// <summary>
        /// Parent Employee pointed by [OppAssignLog].([EmployeeId]) (FK_OppAssignLog_Employee)
        /// </summary>
        public virtual Employee Employee { get; set; } // FK_OppAssignLog_Employee

        /// <summary>
        /// Parent Opportunity pointed by [OppAssignLog].([OpportunityId]) (FK_OppAssignLog_Opportunity)
        /// </summary>
        public virtual Opportunity Opportunity { get; set; } // FK_OppAssignLog_Opportunity

        public OppAssignLog()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
