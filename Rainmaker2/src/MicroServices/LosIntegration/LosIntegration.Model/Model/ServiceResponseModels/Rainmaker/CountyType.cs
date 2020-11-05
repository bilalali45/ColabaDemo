













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // CountyType

    public partial class CountyType 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child Counties where [County].[CountyTypeId] point to this entity (FK_County_CountyType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<County> Counties { get; set; } // County.FK_County_CountyType

        public CountyType()
        {
            DisplayOrder = 0;
            IsActive = true;
            IsDefault = false;
            IsSystem = false;
            EntityTypeId = 175;
            IsDeleted = false;
            Counties = new System.Collections.Generic.HashSet<County>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>