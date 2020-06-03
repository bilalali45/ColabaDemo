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

    // FollowUpActivityPurposeBinder
    
    public partial class FollowUpActivityPurposeBinder : URF.Core.EF.Trackable.Entity
    {
        public int FollowUpPurposeId { get; set; } // FollowUpPurposeId (Primary key)
        public int FollowUpActivityId { get; set; } // FollowUpActivityId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent FollowUpActivity pointed by [FollowUpActivityPurposeBinder].([FollowUpActivityId]) (FK_FollowUpActivityPurposeBinder_FollowUpActivity)
        /// </summary>
        public virtual FollowUpActivity FollowUpActivity { get; set; } // FK_FollowUpActivityPurposeBinder_FollowUpActivity

        /// <summary>
        /// Parent FollowUpPurpose pointed by [FollowUpActivityPurposeBinder].([FollowUpPurposeId]) (FK_FollowUpActivityPurposeBinder_FollowUpPurpose)
        /// </summary>
        public virtual FollowUpPurpose FollowUpPurpose { get; set; } // FK_FollowUpActivityPurposeBinder_FollowUpPurpose

        public FollowUpActivityPurposeBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>