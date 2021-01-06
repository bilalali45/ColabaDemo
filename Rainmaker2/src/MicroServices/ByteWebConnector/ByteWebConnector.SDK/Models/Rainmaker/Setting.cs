using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class Setting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public int? BranchId { get; set; }
        public string Description { get; set; }
        public int? DataTypeId { get; set; }
        public int? BusinessUnitId { get; set; }
        public bool IsDifferentForBusinessUnit { get; set; }

        public ICollection<SettingGroupBinder> SettingGroupBinders { get; set; }

        public Branch Branch { get; set; }

        public BusinessUnit BusinessUnit { get; set; }
    }
}