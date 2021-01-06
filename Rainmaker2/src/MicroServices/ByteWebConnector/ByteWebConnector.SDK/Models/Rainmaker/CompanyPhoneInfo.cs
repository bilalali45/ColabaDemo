using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class CompanyPhoneInfo
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string ServiceName { get; set; }
        public bool IsDeleted { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public string TpId { get; set; }
        public int? ServiceSettingId { get; set; }
        public string Type { get; set; }
        public int? DefaultStatusId { get; set; }

        //public System.Collections.Generic.ICollection<BranchPhone> BranchPhones { get; set; }

        //public System.Collections.Generic.ICollection<BranchPhoneBinder> BranchPhoneBinders { get; set; }

        //public System.Collections.Generic.ICollection<BusinessUnitPhone> BusinessUnitPhones { get; set; }

        //public System.Collections.Generic.ICollection<BusinessUnitPhoneBinder> BusinessUnitPhoneBinders { get; set; }

        //public System.Collections.Generic.ICollection<EmployeePhoneBinder> EmployeePhoneBinders { get; set; }

        //public ServicesSetting ServicesSetting { get; set; }

    }
}