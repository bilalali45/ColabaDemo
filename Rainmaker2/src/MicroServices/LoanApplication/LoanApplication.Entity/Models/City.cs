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


namespace LoanApplicationDb.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // City
    
    public partial class City : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 200)
        public string Description { get; set; } // Description (length: 500)
        public int? StateId { get; set; } // StateId
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
        public string TpId { get; set; } // TpId (length: 50)

        // Reverse navigation

        /// <summary>
        /// Child AddressInfoes where [AddressInfo].[CityId] point to this entity (FK_AddressInfo_City)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AddressInfo> AddressInfoes { get; set; } // AddressInfo.FK_AddressInfo_City
        /// <summary>
        /// Child ZipCodes where [ZipCode].[CityId] point to this entity (FK_ZipCode_City)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ZipCode> ZipCodes { get; set; } // ZipCode.FK_ZipCode_City

        // Foreign keys

        /// <summary>
        /// Parent State pointed by [City].([StateId]) (FK_City_State)
        /// </summary>
        public virtual State State { get; set; } // FK_City_State

        public City()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 119;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            AddressInfoes = new System.Collections.Generic.HashSet<AddressInfo>();
            ZipCodes = new System.Collections.Generic.HashSet<ZipCode>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>