













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // HolidayType

    public partial class HolidayType 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public bool IsFederal { get; set; } // IsFederal
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
        /// Child OfficeHolidays where [OfficeHoliday].[HolidayTypeId] point to this entity (FK_OfficeHoliday_HolidayType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OfficeHoliday> OfficeHolidays { get; set; } // OfficeHoliday.FK_OfficeHoliday_HolidayType

        public HolidayType()
        {
            DisplayOrder = 0;
            IsActive = true;
            IsDefault = false;
            IsSystem = false;
            EntityTypeId = 200;
            IsDeleted = false;
            OfficeHolidays = new System.Collections.Generic.HashSet<OfficeHoliday>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>