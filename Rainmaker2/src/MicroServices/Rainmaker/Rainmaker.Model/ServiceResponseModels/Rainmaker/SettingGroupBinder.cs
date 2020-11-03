namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class SettingGroupBinder
    {
        public int Id { get; set; }
        public int? SettingId { get; set; }
        public int? GroupId { get; set; }
        public int? DisplayOrder { get; set; }

        public Setting Setting { get; set; }

        public SettingGroup SettingGroup { get; set; }
    }
}