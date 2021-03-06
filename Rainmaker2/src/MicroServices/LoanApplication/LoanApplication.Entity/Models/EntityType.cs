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

    // EntityType
    
    public partial class EntityType : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 50)

        // Reverse navigation

        /// <summary>
        /// Child Entities where [Entity].[EntityTypeId] point to this entity (FK_Entity_EntityType_Id)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Entity> Entities { get; set; } // Entity.FK_Entity_EntityType_Id

        public EntityType()
        {
            Entities = new System.Collections.Generic.HashSet<Entity>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
