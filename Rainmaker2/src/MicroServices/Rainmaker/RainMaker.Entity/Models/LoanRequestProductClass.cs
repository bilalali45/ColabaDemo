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
    // LoanRequestProductClass

    public partial class LoanRequestProductClass : URF.Core.EF.Trackable.Entity
    {
        public int LoanRequestId { get; set; } // LoanRequestId (Primary key)
        public int ProductClassId { get; set; } // ProductClassId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent LoanRequest pointed by [LoanRequestProductClass].([LoanRequestId]) (FK_LoanRequestProductClass_LoanRequest)
        /// </summary>
        public virtual LoanRequest LoanRequest { get; set; } // FK_LoanRequestProductClass_LoanRequest

        /// <summary>
        /// Parent ProductClass pointed by [LoanRequestProductClass].([ProductClassId]) (FK_LoanRequestProductClass_ProductClass)
        /// </summary>
        public virtual ProductClass ProductClass { get; set; } // FK_LoanRequestProductClass_ProductClass

        public LoanRequestProductClass()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
