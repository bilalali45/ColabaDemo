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

    // vEmployeePhoneInfo
    
    public partial class VEmployeePhoneInfo : URF.Core.EF.Trackable.Entity
    {
        public string UserName { get; set; } // UserName (Primary key) (length: 256)
        public int EmployeeId { get; set; } // EmployeeId (Primary key)
        public string Phone { get; set; } // Phone (length: 150)
        public int TypeId { get; set; } // TypeId (Primary key)

        public VEmployeePhoneInfo()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>