













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // RaceDetail

    public partial class RaceDetail 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int? RaceId { get; set; } // RaceId
        public bool? IsOther { get; set; } // IsOther
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
        /// Child LoanContactRaceBinders where [LoanContactRaceBinder].[RaceDetailId] point to this entity (FK_LoanContactRaceBinder_RaceDetail)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanContactRaceBinder> LoanContactRaceBinders { get; set; } // LoanContactRaceBinder.FK_LoanContactRaceBinder_RaceDetail

        // Foreign keys

        /// <summary>
        /// Parent Race pointed by [RaceDetail].([RaceId]) (FK_RaceDetail_Race)
        /// </summary>
        public virtual Race Race { get; set; } // FK_RaceDetail_Race

        public RaceDetail()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 220;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            LoanContactRaceBinders = new System.Collections.Generic.HashSet<LoanContactRaceBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
