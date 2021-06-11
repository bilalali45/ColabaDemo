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


namespace TenantConfig.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // County
    
    public partial class County : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public int StateId { get; set; } // StateId
        public int CountyTypeId { get; set; } // CountyTypeId
        public bool IsActive { get; set; } // IsActive
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc

        // Reverse navigation

        /// <summary>
        /// Child AddressInfoes where [AddressInfo].[CountyId] point to this entity (FK_AddressInfo_County_Id)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AddressInfo> AddressInfoes { get; set; } // AddressInfo.FK_AddressInfo_County_Id

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
            IsActive = true;
            AddressInfoes = new System.Collections.Generic.HashSet<AddressInfo>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>