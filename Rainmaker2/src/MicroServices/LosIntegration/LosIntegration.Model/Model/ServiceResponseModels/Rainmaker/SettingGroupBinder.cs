













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // SettingGroupBinder

    public partial class SettingGroupBinder 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? SettingId { get; set; } // SettingId
        public int? GroupId { get; set; } // GroupId
        public int? DisplayOrder { get; set; } // DisplayOrder

        // Foreign keys

        /// <summary>
        /// Parent Setting pointed by [SettingGroupBinder].([SettingId]) (FK_SettingGroupBinder_Setting)
        /// </summary>
        public virtual Setting Setting { get; set; } // FK_SettingGroupBinder_Setting

        /// <summary>
        /// Parent SettingGroup pointed by [SettingGroupBinder].([GroupId]) (FK_SettingGroupBinder_SettingGroup)
        /// </summary>
        public virtual SettingGroup SettingGroup { get; set; } // FK_SettingGroupBinder_SettingGroup

        public SettingGroupBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
