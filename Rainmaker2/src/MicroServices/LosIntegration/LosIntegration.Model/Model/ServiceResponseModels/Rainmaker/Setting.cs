













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // Setting

    public partial class Setting 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Value { get; set; } // Value
        public string Remarks { get; set; } // Remarks
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDeleted { get; set; } // IsDeleted
        public int? BranchId { get; set; } // BranchId
        public string Description { get; set; } // Description (length: 500)
        public int? DataTypeId { get; set; } // DataTypeId
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public bool IsDifferentForBusinessUnit { get; set; } // IsDifferentForBusinessUnit

        // Reverse navigation

        /// <summary>
        /// Child SettingGroupBinders where [SettingGroupBinder].[SettingId] point to this entity (FK_SettingGroupBinder_Setting)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<SettingGroupBinder> SettingGroupBinders { get; set; } // SettingGroupBinder.FK_SettingGroupBinder_Setting

        // Foreign keys

        /// <summary>
        /// Parent Branch pointed by [Setting].([BranchId]) (FK_Setting_Branch)
        /// </summary>
        public virtual Branch Branch { get; set; } // FK_Setting_Branch

        /// <summary>
        /// Parent BusinessUnit pointed by [Setting].([BusinessUnitId]) (FK_Setting_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_Setting_BusinessUnit

        public Setting()
        {
            IsActive = true;
            EntityTypeId = 56;
            IsDeleted = false;
            IsDifferentForBusinessUnit = true;
            SettingGroupBinders = new System.Collections.Generic.HashSet<SettingGroupBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
