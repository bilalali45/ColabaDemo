













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // FollowUpActivity

    public partial class FollowUpActivity 
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
        /// Child FollowUpActivityBinders where [FollowUpActivityBinder].[FollowUpActivityId] point to this entity (FK_FollowUpActivityBinder_FollowUpActivity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FollowUpActivityBinder> FollowUpActivityBinders { get; set; } // FollowUpActivityBinder.FK_FollowUpActivityBinder_FollowUpActivity
        /// <summary>
        /// Child FollowUpActivityPurposeBinders where [FollowUpActivityPurposeBinder].[FollowUpActivityId] point to this entity (FK_FollowUpActivityPurposeBinder_FollowUpActivity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FollowUpActivityPurposeBinder> FollowUpActivityPurposeBinders { get; set; } // FollowUpActivityPurposeBinder.FK_FollowUpActivityPurposeBinder_FollowUpActivity

        public FollowUpActivity()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 171;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            FollowUpActivityBinders = new System.Collections.Generic.HashSet<FollowUpActivityBinder>();
            FollowUpActivityPurposeBinders = new System.Collections.Generic.HashSet<FollowUpActivityPurposeBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
