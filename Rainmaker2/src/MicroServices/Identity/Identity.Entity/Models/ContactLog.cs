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

    // ContactLog
    
    public partial class ContactLog : URF.Core.EF.Trackable.Entity
    {
        public long Id { get; set; } // Id (Primary key)
        public string FirstName { get; set; } // FirstName (length: 100)
        public string LastName { get; set; } // LastName (length: 100)
        public string Email { get; set; } // Email (length: 100)
        public string PhoneNumber { get; set; } // PhoneNumber (length: 50)
        public System.DateTime? CreatedOn { get; set; } // CreatedOn
        public int TenantId { get; set; } // TenantId (Primary key)

        public ContactLog()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
