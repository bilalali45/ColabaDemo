













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // SettingGroup

    public partial class SettingGroup 
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
        /// Child SettingGroupBinders where [SettingGroupBinder].[GroupId] point to this entity (FK_SettingGroupBinder_SettingGroup)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<SettingGroupBinder> SettingGroupBinders { get; set; } // SettingGroupBinder.FK_SettingGroupBinder_SettingGroup

        public SettingGroup()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 147;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            SettingGroupBinders = new System.Collections.Generic.HashSet<SettingGroupBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
