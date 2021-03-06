// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 2.1
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace RainMaker.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // County
    
    public partial class County : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int StateId { get; set; } // StateId
        public int CountyTypeId { get; set; } // CountyTypeId
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
        public string ZipTasticName { get; set; } // ZipTasticName (length: 500)
        public string SmartName { get; set; } // SmartName (length: 500)

        // Reverse navigation

        /// <summary>
        /// Child AddressInfoes where [AddressInfo].[CountyId] point to this entity (FK_AddressInfo_County)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AddressInfo> AddressInfoes { get; set; } // AddressInfo.FK_AddressInfo_County
        /// <summary>
        /// Child BankRateInstances where [BankRateInstance].[CountyId] point to this entity (FK_BankRateInstance_County)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRateInstance> BankRateInstances { get; set; } // BankRateInstance.FK_BankRateInstance_County
        /// <summary>
        /// Child Branches where [Branch].[CountyId] point to this entity (FK_Branch_County)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Branch> Branches { get; set; } // Branch.FK_Branch_County
        /// <summary>
        /// Child ContactAddresses where [ContactAddress].[CountyId] point to this entity (FK_ContactAddress_County)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ContactAddress> ContactAddresses { get; set; } // ContactAddress.FK_ContactAddress_County
        /// <summary>
        /// Child LoanRequests where [LoanRequest].[CountyId] point to this entity (FK_LoanRequest_County)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; } // LoanRequest.FK_LoanRequest_County
        /// <summary>
        /// Child OfficeMetroBinders where [OfficeMetroBinder].[CountyId] point to this entity (FK_OfficeMetroBinder_County)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OfficeMetroBinder> OfficeMetroBinders { get; set; } // OfficeMetroBinder.FK_OfficeMetroBinder_County
        /// <summary>
        /// Child PromotionalPrograms where [PromotionalProgram].[CountyId] point to this entity (FK_PromotionalProgram_County)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PromotionalProgram> PromotionalPrograms { get; set; } // PromotionalProgram.FK_PromotionalProgram_County
        /// <summary>
        /// Child ReviewComments where [ReviewComment].[CountyId] point to this entity (FK_ReviewComment_County)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ReviewComment> ReviewComments { get; set; } // ReviewComment.FK_ReviewComment_County
        /// <summary>
        /// Child ReviewProperties where [ReviewProperty].[CountyId] point to this entity (FK_ReviewProperty_County)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ReviewProperty> ReviewProperties { get; set; } // ReviewProperty.FK_ReviewProperty_County
        /// <summary>
        /// Child TaxCountyBinders where [TaxCountyBinder].[CountyId] point to this entity (FK_TaxCountyBinder_County)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TaxCountyBinder> TaxCountyBinders { get; set; } // TaxCountyBinder.FK_TaxCountyBinder_County
        /// <summary>
        /// Child ZipCodes where [ZipCode].[CountyId] point to this entity (FK_ZipCode_County)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ZipCode> ZipCodes { get; set; } // ZipCode.FK_ZipCode_County

        // Foreign keys

        /// <summary>
        /// Parent CountyType pointed by [County].([CountyTypeId]) (FK_County_CountyType)
        /// </summary>
        public virtual CountyType CountyType { get; set; } // FK_County_CountyType

        /// <summary>
        /// Parent State pointed by [County].([StateId]) (FK_County_State)
        /// </summary>
        public virtual State State { get; set; } // FK_County_State

        public County()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 153;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            AddressInfoes = new System.Collections.Generic.HashSet<AddressInfo>();
            BankRateInstances = new System.Collections.Generic.HashSet<BankRateInstance>();
            Branches = new System.Collections.Generic.HashSet<Branch>();
            ContactAddresses = new System.Collections.Generic.HashSet<ContactAddress>();
            LoanRequests = new System.Collections.Generic.HashSet<LoanRequest>();
            OfficeMetroBinders = new System.Collections.Generic.HashSet<OfficeMetroBinder>();
            PromotionalPrograms = new System.Collections.Generic.HashSet<PromotionalProgram>();
            ReviewComments = new System.Collections.Generic.HashSet<ReviewComment>();
            ReviewProperties = new System.Collections.Generic.HashSet<ReviewProperty>();
            TaxCountyBinders = new System.Collections.Generic.HashSet<TaxCountyBinder>();
            ZipCodes = new System.Collections.Generic.HashSet<ZipCode>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
