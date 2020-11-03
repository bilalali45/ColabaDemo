













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // Contact

    public partial class Contact 
    {
        public int Id { get; set; } // Id (Primary key)
        public string FirstName { get; set; } // FirstName (length: 300)
        public string MiddleName { get; set; } // MiddleName (length: 50)
        public string LastName { get; set; } // LastName (length: 300)
        public string NickName { get; set; } // NickName (length: 50)
        public string Prefix { get; set; } // Prefix (length: 10)
        public string Suffix { get; set; } // Suffix (length: 10)
        public string Preferred { get; set; } // Preferred (length: 1000)
        public string Company { get; set; } // Company (length: 500)
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDeleted { get; set; } // IsDeleted
        public int? PreferredId { get; set; } // PreferredId
        public string Ssn { get; set; } // Ssn (length: 256)
        public System.DateTime? DobUtc { get; set; } // DobUtc
        public int? YrsSchool { get; set; } // YrsSchool
        public int? MaritalStatusId { get; set; } // MaritalStatusId
        public int? Gender { get; set; } // Gender
        public int? EthnicityId { get; set; } // EthnicityId

        // Reverse navigation

        /// <summary>
        /// Child ContactAddresses where [ContactAddress].[ContactId] point to this entity (FK_ContactAddress_Contact)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ContactAddress> ContactAddresses { get; set; } // ContactAddress.FK_ContactAddress_Contact
        /// <summary>
        /// Child ContactEmailInfoes where [ContactEmailInfo].[ContactId] point to this entity (FK_ContactEmailInfo_Contact)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ContactEmailInfo> ContactEmailInfoes { get; set; } // ContactEmailInfo.FK_ContactEmailInfo_Contact
        /// <summary>
        /// Child ContactInfoes where [ContactInfo].[ContactId] point to this entity (FK_ContactInfo_Contact)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ContactInfo> ContactInfoes { get; set; } // ContactInfo.FK_ContactInfo_Contact
        /// <summary>
        /// Child ContactPhoneInfoes where [ContactPhoneInfo].[ContactId] point to this entity (FK_ContactPhoneInfo_Contact)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ContactPhoneInfo> ContactPhoneInfoes { get; set; } // ContactPhoneInfo.FK_ContactPhoneInfo_Contact
        /// <summary>
        /// Child Customers where [Customer].[ContactId] point to this entity (FK_Customer_Contact)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Customer> Customers { get; set; } // Customer.FK_Customer_Contact
        /// <summary>
        /// Child EmailLogBinders where [EmailLogBinder].[ContactId] point to this entity (FK_EmailLogBinder_Contact)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmailLogBinder> EmailLogBinders { get; set; } // EmailLogBinder.FK_EmailLogBinder_Contact
        /// <summary>
        /// Child Employees where [Employee].[ContactId] point to this entity (FK_Employee_Contact)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Employee> Employees { get; set; } // Employee.FK_Employee_Contact
        /// <summary>
        /// Child FollowUps where [FollowUp].[ContactId] point to this entity (FK_FollowUp_Contact)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FollowUp> FollowUps { get; set; } // FollowUp.FK_FollowUp_Contact
        /// <summary>
        /// Child LoanContacts where [LoanContact].[ContactId] point to this entity (FK_LoanContact_Contact)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanContact> LoanContacts { get; set; } // LoanContact.FK_LoanContact_Contact
        /// <summary>
        /// Child OtpTracings where [OtpTracing].[ContactId] point to this entity (FK_OtpTracing_Contact)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OtpTracing> OtpTracings { get; set; } // OtpTracing.FK_OtpTracing_Contact
        

        public Contact()
        {
            EntityTypeId = 38;
            IsDeleted = false;
            ContactAddresses = new System.Collections.Generic.HashSet<ContactAddress>();
            ContactEmailInfoes = new System.Collections.Generic.HashSet<ContactEmailInfo>();
            ContactInfoes = new System.Collections.Generic.HashSet<ContactInfo>();
            ContactPhoneInfoes = new System.Collections.Generic.HashSet<ContactPhoneInfo>();
            Customers = new System.Collections.Generic.HashSet<Customer>();
            EmailLogBinders = new System.Collections.Generic.HashSet<EmailLogBinder>();
            Employees = new System.Collections.Generic.HashSet<Employee>();
            FollowUps = new System.Collections.Generic.HashSet<FollowUp>();
            LoanContacts = new System.Collections.Generic.HashSet<LoanContact>();
            OtpTracings = new System.Collections.Generic.HashSet<OtpTracing>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
