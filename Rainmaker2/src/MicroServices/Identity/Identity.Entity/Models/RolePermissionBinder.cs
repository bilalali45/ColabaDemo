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


namespace Identity.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // RolePermissionBinder
    
    public partial class RolePermissionBinder : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int RoleId { get; set; } // RoleId
        public int PermissionId { get; set; } // PermissionId
        public bool Granted { get; set; } // Granted

        // Foreign keys

        /// <summary>
        /// Parent Permission pointed by [RolePermissionBinder].([PermissionId]) (FK_RolePermissionBinder_Permission_Id)
        /// </summary>
        public virtual Permission Permission { get; set; } // FK_RolePermissionBinder_Permission_Id

        /// <summary>
        /// Parent Role pointed by [RolePermissionBinder].([RoleId]) (FK_RolePermissionBinder_Role_Id)
        /// </summary>
        public virtual Role Role { get; set; } // FK_RolePermissionBinder_Role_Id

        public RolePermissionBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>