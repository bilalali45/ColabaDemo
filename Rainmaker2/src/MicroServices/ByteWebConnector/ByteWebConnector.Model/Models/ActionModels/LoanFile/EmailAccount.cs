













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // EmailAccount

    public partial class EmailAccount 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Email { get; set; } // Email (length: 255)
        public string DisplayName { get; set; } // DisplayName (length: 255)
        public string FromEmail { get; set; } // FromEmail (length: 255)
        public string Host { get; set; } // Host (length: 255)
        public int? Port { get; set; } // Port
        public string Username { get; set; } // Username (length: 255)
        public string Password { get; set; } // Password (length: 255)
        public bool? EnableSsl { get; set; } // EnableSsl
        public bool UseDefaultCredentials { get; set; } // UseDefaultCredentials
        public bool UseReplyTo { get; set; } // UseReplyTo
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child Branches where [Branch].[EmailAccountId] point to this entity (FK_Branch_EmailAccount)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Branch> Branches { get; set; } // Branch.FK_Branch_EmailAccount
        /// <summary>
        /// Child BranchEmails where [BranchEmail].[EmailAccountId] point to this entity (FK_BranchEmail_EmailAccount)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BranchEmail> BranchEmails { get; set; } // BranchEmail.FK_BranchEmail_EmailAccount
        /// <summary>
        /// Child BusinessUnits where [BusinessUnit].[EmailAccountId] point to this entity (FK_BusinessUnit_EmailAccount)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BusinessUnit> BusinessUnits { get; set; } // BusinessUnit.FK_BusinessUnit_EmailAccount
        /// <summary>
        /// Child BusinessUnitEmails where [BusinessUnitEmail].[EmailAccountId] point to this entity (FK_BusinessUnitEmail_EmailAccount)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BusinessUnitEmail> BusinessUnitEmails { get; set; } // BusinessUnitEmail.FK_BusinessUnitEmail_EmailAccount
        /// <summary>
        /// Child Employees where [Employee].[EmailAccountId] point to this entity (FK_Employee_EmailAccount)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Employee> Employees { get; set; } // Employee.FK_Employee_EmailAccount
        /// <summary>
        /// Child EmployeeBusinessUnitEmails where [EmployeeBusinessUnitEmail].[EmailAccountId] point to this entity (FK_EmployeeBusinessUnitEmail_EmailAccount)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmployeeBusinessUnitEmail> EmployeeBusinessUnitEmails { get; set; } // EmployeeBusinessUnitEmail.FK_EmployeeBusinessUnitEmail_EmailAccount
        /// <summary>
        /// Child QueuedEmails where [QueuedEmail].[EmailAccountId] point to this entity (FK_QueuedEmail_EmailAccount)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<QueuedEmail> QueuedEmails { get; set; } // QueuedEmail.FK_QueuedEmail_EmailAccount
        /// <summary>
        /// Child Templates where [Template].[FromEmailAccountId] point to this entity (FK_Template_EmailAccount)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Template> Templates { get; set; } // Template.FK_Template_EmailAccount

        public EmailAccount()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 13;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            Branches = new System.Collections.Generic.HashSet<Branch>();
            BranchEmails = new System.Collections.Generic.HashSet<BranchEmail>();
            BusinessUnits = new System.Collections.Generic.HashSet<BusinessUnit>();
            BusinessUnitEmails = new System.Collections.Generic.HashSet<BusinessUnitEmail>();
            Employees = new System.Collections.Generic.HashSet<Employee>();
            EmployeeBusinessUnitEmails = new System.Collections.Generic.HashSet<EmployeeBusinessUnitEmail>();
            QueuedEmails = new System.Collections.Generic.HashSet<QueuedEmail>();
            Templates = new System.Collections.Generic.HashSet<Template>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
