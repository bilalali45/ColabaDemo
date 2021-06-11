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

    // OtpTracing
    
    public partial class OtpTracing : URF.Core.EF.Trackable.Entity
    {
        public long Id { get; set; } // Id (Primary key)
        public string Phone { get; set; } // Phone (length: 20)
        public string IpAddress { get; set; } // IpAddress (length: 50)
        public System.DateTime? DateUtc { get; set; } // DateUtc
        public int? TracingTypeId { get; set; } // TracingTypeId
        public string CodeEntered { get; set; } // CodeEntered (length: 6)
        public int? ContactId { get; set; } // ContactId
        public System.DateTime? OtpCreatedOn { get; set; } // OtpCreatedOn
        public string Email { get; set; } // Email (length: 150)
        public System.DateTime? OtpUpdatedOn { get; set; } // OtpUpdatedOn
        public int? StatusCode { get; set; } // StatusCode
        public string Message { get; set; } // Message (length: 500)
        public string ResponseJson { get; set; } // ResponseJson
        public int? BranchId { get; set; } // BranchId
        public string CarrierType { get; set; } // CarrierType (length: 100)
        public string CarrierName { get; set; } // CarrierName (length: 200)
        public int? TenantId { get; set; } // TenantId
        public string OtpRequestId { get; set; } // OtpRequestId (length: 50)

        public OtpTracing()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>