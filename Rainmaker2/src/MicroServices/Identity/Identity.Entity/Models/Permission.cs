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

    // Permission
    
    public partial class Permission : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 50)
        public string Module { get; set; } // Module (length: 50)
        public string Page { get; set; } // Page (length: 50)
        public string Action { get; set; } // Action (length: 50)
        public bool IsActive { get; set; } // IsActive

        // Reverse navigation

        /// <summary>
        /// Child RolePermissionBinders where [RolePermissionBinder].[PermissionId] point to this entity (FK_RolePermissionBinder_Permission_Id)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<RolePermissionBinder> RolePermissionBinders { get; set; } // RolePermissionBinder.FK_RolePermissionBinder_Permission_Id
        /// <summary>
        /// Child UserPermissionBinders where [UserPermissionBinder].[PermissionId] point to this entity (FK_UserPermissionBinder_Permission_Id)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserPermissionBinder> UserPermissionBinders { get; set; } // UserPermissionBinder.FK_UserPermissionBinder_Permission_Id

        public Permission()
        {
            RolePermissionBinders = new System.Collections.Generic.HashSet<RolePermissionBinder>();
            UserPermissionBinders = new System.Collections.Generic.HashSet<UserPermissionBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>