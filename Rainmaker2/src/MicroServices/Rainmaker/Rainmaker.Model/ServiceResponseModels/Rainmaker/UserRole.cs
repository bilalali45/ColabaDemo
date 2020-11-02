using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class UserRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string LoweredRoleName { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public string TpId { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsCustomerRole { get; set; }

        public ICollection<UserInRole> UserInRoles { get; set; }

        public ICollection<UserPermissionRoleBinder> UserPermissionRoleBinders { get; set; }
    }
}