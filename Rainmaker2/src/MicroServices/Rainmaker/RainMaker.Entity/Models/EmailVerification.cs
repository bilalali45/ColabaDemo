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
    // EmailVerification

    public partial class EmailVerification : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string EmailAddress { get; set; } // EmailAddress (length: 200)
        public string Code { get; set; } // Code (length: 50)
        public int? TypeId { get; set; } // TypeId
        public System.DateTime? ExpiryDateUtc { get; set; } // ExpiryDateUtc

        public EmailVerification()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
