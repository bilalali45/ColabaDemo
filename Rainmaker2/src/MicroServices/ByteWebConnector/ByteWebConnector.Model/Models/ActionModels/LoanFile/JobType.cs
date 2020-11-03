













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // JobType

    public partial class JobType 
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
        /// Child EmploymentInfoes where [EmploymentInfo].[JobTypeId] point to this entity (FK_EmploymentInfo_JobType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmploymentInfo> EmploymentInfoes { get; set; } // EmploymentInfo.FK_EmploymentInfo_JobType

        public JobType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 184;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            EmploymentInfoes = new System.Collections.Generic.HashSet<EmploymentInfo>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
