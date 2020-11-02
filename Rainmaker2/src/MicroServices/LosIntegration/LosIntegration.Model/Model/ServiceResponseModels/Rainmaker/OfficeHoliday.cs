













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // OfficeHoliday

    public partial class OfficeHoliday 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int HolidayTypeId { get; set; } // HolidayTypeId
        public System.DateTime? HolidayOn { get; set; } // HolidayOn
        public bool WillOfficeClosed { get; set; } // WillOfficeClosed
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

        // Foreign keys

        /// <summary>
        /// Parent HolidayType pointed by [OfficeHoliday].([HolidayTypeId]) (FK_OfficeHoliday_HolidayType)
        /// </summary>
        public virtual HolidayType HolidayType { get; set; } // FK_OfficeHoliday_HolidayType

        public OfficeHoliday()
        {
            DisplayOrder = 0;
            IsActive = true;
            IsDefault = false;
            IsSystem = false;
            EntityTypeId = 164;
            IsDeleted = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
