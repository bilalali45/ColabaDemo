













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // Branch

    public partial class Branch 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted
        public bool IsHeadOffice { get; set; } // IsHeadOffice
        public int? HeadOfficeId { get; set; } // HeadOfficeId
        public int? EmailAccountId { get; set; } // EmailAccountId
        public int? TimeZoneId { get; set; } // TimeZoneId
        public int? CountryId { get; set; } // CountryId
        public int? StateId { get; set; } // StateId
        public int? CountyId { get; set; } // CountyId
        public string CityName { get; set; } // CityName (length: 200)
        public int? CityId { get; set; } // CityId
        public string Address1 { get; set; } // Address1 (length: 500)
        public string Address2 { get; set; } // Address2 (length: 500)
        public string ZipPostalCode { get; set; } // ZipPostalCode (length: 5)
        public string NmlsNo { get; set; } // NmlsNo (length: 50)
        public string LoanUrl { get; set; } // LoanUrl (length: 500)
        public string ScheduleUrl { get; set; } // ScheduleUrl (length: 500)

        // Reverse navigation

        /// <summary>
        /// Child Branches where [Branch].[HeadOfficeId] point to this entity (FK_Branch_Branch)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Branch> Branches { get; set; } // Branch.FK_Branch_Branch
        /// <summary>
        /// Child BranchEmails where [BranchEmail].[BranchId] point to this entity (FK_BranchEmail_Branch)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BranchEmail> BranchEmails { get; set; } // BranchEmail.FK_BranchEmail_Branch
        /// <summary>
        /// Child BranchPhones where [BranchPhone].[BranchId] point to this entity (FK_BranchPhone_Branch)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BranchPhone> BranchPhones { get; set; } // BranchPhone.FK_BranchPhone_Branch
        /// <summary>
        /// Child BranchPhoneBinders where [BranchPhoneBinder].[BranchId] point to this entity (FK_BranchPhoneBinder_Branch)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BranchPhoneBinder> BranchPhoneBinders { get; set; } // BranchPhoneBinder.FK_BranchPhoneBinder_Branch
        /// <summary>
        /// Child Employees where [Employee].[BranchId] point to this entity (FK_Employee_Branch)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Employee> Employees { get; set; } // Employee.FK_Employee_Branch
        /// <summary>
        /// Child OfficeMetroBinders where [OfficeMetroBinder].[BranchId] point to this entity (FK_OfficeMetroBinder_Branch)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OfficeMetroBinder> OfficeMetroBinders { get; set; } // OfficeMetroBinder.FK_OfficeMetroBinder_Branch
        /// <summary>
        /// Child Opportunities where [Opportunity].[BranchId] point to this entity (FK_Opportunity_Branch)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Opportunity> Opportunities { get; set; } // Opportunity.FK_Opportunity_Branch
        /// <summary>
        /// Child Settings where [Setting].[BranchId] point to this entity (FK_Setting_Branch)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Setting> Settings { get; set; } // Setting.FK_Setting_Branch

        // Foreign keys

        /// <summary>
        /// Parent Branch pointed by [Branch].([HeadOfficeId]) (FK_Branch_Branch)
        /// </summary>
        public virtual Branch HeadOffice { get; set; } // FK_Branch_Branch

        /// <summary>
        /// Parent City pointed by [Branch].([CityId]) (FK_Branch_City)
        /// </summary>
        public virtual City City { get; set; } // FK_Branch_City

        /// <summary>
        /// Parent County pointed by [Branch].([CountyId]) (FK_Branch_County)
        /// </summary>
        public virtual County County { get; set; } // FK_Branch_County

        /// <summary>
        /// Parent EmailAccount pointed by [Branch].([EmailAccountId]) (FK_Branch_EmailAccount)
        /// </summary>
        public virtual EmailAccount EmailAccount { get; set; } // FK_Branch_EmailAccount

        /// <summary>
        /// Parent State pointed by [Branch].([StateId]) (FK_Branch_State)
        /// </summary>
        public virtual State State { get; set; } // FK_Branch_State

        /// <summary>
        /// Parent TimeZone pointed by [Branch].([TimeZoneId]) (FK_Branch_TimeZone)
        /// </summary>
        public virtual TimeZone TimeZone { get; set; } // FK_Branch_TimeZone

        public Branch()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 87;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            IsHeadOffice = false;
            Branches = new System.Collections.Generic.HashSet<Branch>();
            BranchEmails = new System.Collections.Generic.HashSet<BranchEmail>();
            BranchPhones = new System.Collections.Generic.HashSet<BranchPhone>();
            BranchPhoneBinders = new System.Collections.Generic.HashSet<BranchPhoneBinder>();
            Employees = new System.Collections.Generic.HashSet<Employee>();
            OfficeMetroBinders = new System.Collections.Generic.HashSet<OfficeMetroBinder>();
            Opportunities = new System.Collections.Generic.HashSet<Opportunity>();
            Settings = new System.Collections.Generic.HashSet<Setting>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
