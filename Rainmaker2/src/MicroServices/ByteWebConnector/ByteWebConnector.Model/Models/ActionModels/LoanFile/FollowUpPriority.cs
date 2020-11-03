













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // FollowUpPriority

    public partial class FollowUpPriority 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child FollowUps where [FollowUp].[FollowUpPriorityId] point to this entity (FK_FollowUp_FollowUpPriority)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FollowUp> FollowUps { get; set; } // FollowUp.FK_FollowUp_FollowUpPriority

        public FollowUpPriority()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 195;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            FollowUps = new System.Collections.Generic.HashSet<FollowUp>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
