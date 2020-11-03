using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class EmailAccount
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string FromEmail { get; set; }
        public string Host { get; set; }
        public int? Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool? EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool UseReplyTo { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public string TpId { get; set; }
        public bool IsDeleted { get; set; }

        //public System.Collections.Generic.ICollection<Branch> Branches { get; set; }

        //public System.Collections.Generic.ICollection<BranchEmail> BranchEmails { get; set; }

        //public System.Collections.Generic.ICollection<BusinessUnit> BusinessUnits { get; set; }

        //public System.Collections.Generic.ICollection<BusinessUnitEmail> BusinessUnitEmails { get; set; }

        //public System.Collections.Generic.ICollection<Employee> Employees { get; set; }

        //public System.Collections.Generic.ICollection<EmployeeBusinessUnitEmail> EmployeeBusinessUnitEmails { get; set; }

        //public System.Collections.Generic.ICollection<QueuedEmail> QueuedEmails { get; set; }

        //public System.Collections.Generic.ICollection<Template> Templates { get; set; }

    }
}