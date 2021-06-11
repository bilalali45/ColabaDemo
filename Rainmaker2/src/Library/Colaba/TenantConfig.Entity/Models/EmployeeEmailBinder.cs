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

    // EmployeeEmailBinder
    
    public partial class EmployeeEmailBinder : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int EmployeeId { get; set; } // EmployeeId
        public int EmailAccountId { get; set; } // EmailAccountId
        public int? TypeId { get; set; } // TypeId

        // Foreign keys

        /// <summary>
        /// Parent EmailAccount pointed by [EmployeeEmailBinder].([EmailAccountId]) (FK_BranchEmployeeEmailBinder_EmailAccount_Id)
        /// </summary>
        public virtual EmailAccount EmailAccount { get; set; } // FK_BranchEmployeeEmailBinder_EmailAccount_Id

        /// <summary>
        /// Parent Employee pointed by [EmployeeEmailBinder].([EmployeeId]) (FK_BranchEmployeeEmailBinder_Employee_Id)
        /// </summary>
        public virtual Employee Employee { get; set; } // FK_BranchEmployeeEmailBinder_Employee_Id

        public EmployeeEmailBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>