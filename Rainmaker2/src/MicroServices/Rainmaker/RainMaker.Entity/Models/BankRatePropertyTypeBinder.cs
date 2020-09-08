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
    // BankRatePropertyTypeBinder

    public partial class BankRatePropertyTypeBinder : URF.Core.EF.Trackable.Entity
    {
        public int BankRateProductId { get; set; } // BankRateProductId (Primary key)
        public int PropertyTypeId { get; set; } // PropertyTypeId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent BankRateProduct pointed by [BankRatePropertyTypeBinder].([BankRateProductId]) (FK_BankRatePropertyTypeBinder_BankRateProduct)
        /// </summary>
        public virtual BankRateProduct BankRateProduct { get; set; } // FK_BankRatePropertyTypeBinder_BankRateProduct

        /// <summary>
        /// Parent PropertyType pointed by [BankRatePropertyTypeBinder].([PropertyTypeId]) (FK_BankRatePropertyTypeBinder_PropertyType)
        /// </summary>
        public virtual PropertyType PropertyType { get; set; } // FK_BankRatePropertyTypeBinder_PropertyType

        public BankRatePropertyTypeBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
