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

    // _RmOtherIncomeTypeMapping
    
    public partial class RmOtherIncomeTypeMapping : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? RmOtherIncomeTypeId { get; set; } // RmOtherIncomeTypeId
        public int? ColabaOtherIncomeTypeId { get; set; } // ColabaOtherIncomeTypeId
        public string Name { get; set; } // Name (length: 100)

        public RmOtherIncomeTypeMapping()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
