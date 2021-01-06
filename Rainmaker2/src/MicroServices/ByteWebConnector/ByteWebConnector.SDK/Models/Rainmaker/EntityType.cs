using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class EntityType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAcl { get; set; }
        public bool IsAuditEnabled { get; set; }

        public ICollection<Acl> Acls { get; set; }

        //public System.Collections.Generic.ICollection<AuditTrail> AuditTrails { get; set; }

        public ICollection<Customer> Customers { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<Note> Notes_EntityRefTypeId { get; set; }

        public ICollection<Note> Notes_EntityTypeId { get; set; }

        public ICollection<SetupTable> SetupTables { get; set; }

        public ICollection<ThirdPartyCode> ThirdPartyCodes { get; set; }

        public ICollection<UserProfile> UserProfiles_EntityRefTypeId { get; set; }

        public ICollection<UserProfile> UserProfiles_EntityTypeId { get; set; }
    }
}