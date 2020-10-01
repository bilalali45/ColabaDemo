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


namespace Milestone.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // ByteDocTypeMapping
    
    public partial class ByteDocTypeMapping : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string RmDocTypeName { get; set; } // RmDocTypeName (length: 50)
        public string ByteDoctypeName { get; set; } // ByteDoctypeName (length: 50)
        public int? ByteDocCategoryId { get; set; } // ByteDocCategoryId

        // Foreign keys

        /// <summary>
        /// Parent ByteDocCategoryMapping pointed by [ByteDocTypeMapping].([ByteDocCategoryId]) (FK_ByteDocTypeMapping_ByteDocCategoryMapping)
        /// </summary>
        public virtual ByteDocCategoryMapping ByteDocCategoryMapping { get; set; } // FK_ByteDocTypeMapping_ByteDocCategoryMapping

        public ByteDocTypeMapping()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
