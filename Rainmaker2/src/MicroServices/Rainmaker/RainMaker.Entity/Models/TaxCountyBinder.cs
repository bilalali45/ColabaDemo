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

    // TaxCountyBinder
    
    public partial class TaxCountyBinder : URF.Core.EF.Trackable.Entity
    {
        public int PropertyTaxId { get; set; } // PropertyTaxId (Primary key)
        public int CountyId { get; set; } // CountyId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent County pointed by [TaxCountyBinder].([CountyId]) (FK_TaxCountyBinder_County)
        /// </summary>
        public virtual County County { get; set; } // FK_TaxCountyBinder_County

        /// <summary>
        /// Parent PropertyTax pointed by [TaxCountyBinder].([PropertyTaxId]) (FK_TaxCountyBinder_PropertyTax)
        /// </summary>
        public virtual PropertyTax PropertyTax { get; set; } // FK_TaxCountyBinder_PropertyTax

        public TaxCountyBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
