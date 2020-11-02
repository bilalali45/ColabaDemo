













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LoanGoal

    public partial class LoanGoal 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? LoanPurposeId { get; set; } // LoanPurposeId
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public bool? IsCustomerVisible { get; set; } // IsCustomerVisible
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
        /// Child LoanApplications where [LoanApplication].[LoanGoalId] point to this entity (FK_LoanApplication_LoanGoal)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanApplication> LoanApplications { get; set; } // LoanApplication.FK_LoanApplication_LoanGoal
        /// <summary>
        /// Child LoanRequests where [LoanRequest].[LoanGoalId] point to this entity (FK_LoanRequest_LoanGoal)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; } // LoanRequest.FK_LoanRequest_LoanGoal

        // Foreign keys

        /// <summary>
        /// Parent LoanPurpose pointed by [LoanGoal].([LoanPurposeId]) (FK_LoanGoal_LoanPurpose)
        /// </summary>
        public virtual LoanPurpose LoanPurpose { get; set; } // FK_LoanGoal_LoanPurpose

        public LoanGoal()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 3;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            LoanApplications = new System.Collections.Generic.HashSet<LoanApplication>();
            LoanRequests = new System.Collections.Generic.HashSet<LoanRequest>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
