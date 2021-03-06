













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // EntityType

    public partial class EntityType 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted
        public bool IsAcl { get; set; } // IsAcl
        public bool IsAuditEnabled { get; set; } // IsAuditEnabled

        // Reverse navigation

        /// <summary>
        /// Child Acls where [Acl].[EntityRefTypeId] point to this entity (FK_Acl_EntityType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Acl> Acls { get; set; } // Acl.FK_Acl_EntityType
        /// <summary>
        /// Child AuditTrails where [AuditTrail].[EntityTypeId] point to this entity (FK_AuditTrail_EntityType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AuditTrail> AuditTrails { get; set; } // AuditTrail.FK_AuditTrail_EntityType
        /// <summary>
        /// Child Customers where [Customer].[EntityTypeId] point to this entity (FK_Customer_EntityType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Customer> Customers { get; set; } // Customer.FK_Customer_EntityType
        /// <summary>
        /// Child Employees where [Employee].[EntityTypeId] point to this entity (FK_Employee_EntityType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Employee> Employees { get; set; } // Employee.FK_Employee_EntityType
        /// <summary>
        /// Child Notes where [Note].[EntityRefTypeId] point to this entity (FK_Note_EntityRefType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Note> Notes_EntityRefTypeId { get; set; } // Note.FK_Note_EntityRefType
        /// <summary>
        /// Child Notes where [Note].[EntityTypeId] point to this entity (FK_Note_EntityType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Note> Notes_EntityTypeId { get; set; } // Note.FK_Note_EntityType
        /// <summary>
        /// Child SetupTables where [SetupTable].[EntityTypeId] point to this entity (FK_SetupTable_EntityType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<SetupTable> SetupTables { get; set; } // SetupTable.FK_SetupTable_EntityType
        /// <summary>
        /// Child ThirdPartyCodes where [ThirdPartyCode].[EntityRefTypeId] point to this entity (FK_ThirdPartyCode_EntityType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ThirdPartyCode> ThirdPartyCodes { get; set; } // ThirdPartyCode.FK_ThirdPartyCode_EntityType
        /// <summary>
        /// Child UserProfiles where [UserProfile].[EntityRefTypeId] point to this entity (FK_UserProfile_EntityRefType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserProfile> UserProfiles_EntityRefTypeId { get; set; } // UserProfile.FK_UserProfile_EntityRefType
        /// <summary>
        /// Child UserProfiles where [UserProfile].[EntityTypeId] point to this entity (FK_UserProfile_EntityType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserProfile> UserProfiles_EntityTypeId { get; set; } // UserProfile.FK_UserProfile_EntityType

        public EntityType()
        {
            DisplayOrder = 0;
            IsActive = true;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            IsAuditEnabled = false;
            Acls = new System.Collections.Generic.HashSet<Acl>();
            AuditTrails = new System.Collections.Generic.HashSet<AuditTrail>();
            Customers = new System.Collections.Generic.HashSet<Customer>();
            Employees = new System.Collections.Generic.HashSet<Employee>();
            Notes_EntityRefTypeId = new System.Collections.Generic.HashSet<Note>();
            Notes_EntityTypeId = new System.Collections.Generic.HashSet<Note>();
            SetupTables = new System.Collections.Generic.HashSet<SetupTable>();
            ThirdPartyCodes = new System.Collections.Generic.HashSet<ThirdPartyCode>();
            UserProfiles_EntityRefTypeId = new System.Collections.Generic.HashSet<UserProfile>();
            UserProfiles_EntityTypeId = new System.Collections.Generic.HashSet<UserProfile>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
