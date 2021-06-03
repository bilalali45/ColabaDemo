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

    // Country
    
    public partial class Country : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string TwoLetterIsoCode { get; set; } // TwoLetterIsoCode (length: 2)
        public string ThreeLetterIsoCode { get; set; } // ThreeLetterIsoCode (length: 3)
        public int NumericIsoCode { get; set; } // NumericIsoCode
        public bool IsActive { get; set; } // IsActive
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc

        // Reverse navigation

        /// <summary>
        /// Child AddressInfoes where [AddressInfo].[CountryId] point to this entity (FK_AddressInfo_Country_Id)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AddressInfo> AddressInfoes { get; set; } // AddressInfo.FK_AddressInfo_Country_Id
        /// <summary>
        /// Child States where [State].[CountryId] point to this entity (FK_State_Country)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<State> States { get; set; } // State.FK_State_Country

        public Country()
        {
            IsActive = true;
            AddressInfoes = new System.Collections.Generic.HashSet<AddressInfo>();
            States = new System.Collections.Generic.HashSet<State>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
