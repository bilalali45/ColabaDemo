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

    // ObjectType
    
    public partial class ObjectType : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 50)
        public string Description { get; set; } // Description (length: 200)

        // Reverse navigation

        /// <summary>
        /// Child UserPermissions where [UserPermission].[ObjectTypeId] point to this entity (FK_UserPermission_ObjectType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserPermission> UserPermissions { get; set; } // UserPermission.FK_UserPermission_ObjectType

        public ObjectType()
        {
            UserPermissions = new System.Collections.Generic.HashSet<UserPermission>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
