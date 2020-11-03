













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // TimeZone

    public partial class TimeZone 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child Branches where [Branch].[TimeZoneId] point to this entity (FK_Branch_TimeZone)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Branch> Branches { get; set; } // Branch.FK_Branch_TimeZone

        public TimeZone()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 110;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            Branches = new System.Collections.Generic.HashSet<Branch>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
