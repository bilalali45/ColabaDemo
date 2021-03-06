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

    // NoteDepartmentBinder
    
    public partial class NoteDepartmentBinder : URF.Core.EF.Trackable.Entity
    {
        public int DepartmentId { get; set; } // DepartmentId (Primary key)
        public int NoteId { get; set; } // NoteId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent Department pointed by [NoteDepartmentBinder].([DepartmentId]) (FK_NoteDepartmentBinder_Department)
        /// </summary>
        public virtual Department Department { get; set; } // FK_NoteDepartmentBinder_Department

        /// <summary>
        /// Parent Note pointed by [NoteDepartmentBinder].([NoteId]) (FK_NoteDepartmentBinder_Note)
        /// </summary>
        public virtual Note Note { get; set; } // FK_NoteDepartmentBinder_Note

        public NoteDepartmentBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
