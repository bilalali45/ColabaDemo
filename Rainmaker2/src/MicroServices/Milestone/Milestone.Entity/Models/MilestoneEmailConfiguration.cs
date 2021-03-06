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

    // MilestoneEmailConfiguration
    
    public partial class MilestoneEmailConfiguration : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? StatusUpdateId { get; set; } // StatusUpdateId
        public string FromAddress { get; set; } // FromAddress (length: 200)
        public string ToAddress { get; set; } // ToAddress (length: 200)
        public string CcAddress { get; set; } // CCAddress (length: 200)
        public string Subject { get; set; } // Subject (length: 500)
        public string Body { get; set; } // Body

        // Foreign keys

        /// <summary>
        /// Parent MilestoneStatusConfiguration pointed by [MilestoneEmailConfiguration].([StatusUpdateId]) (FK_MilestoneEmailConfiguration_MilestoneStatusConfiguration)
        /// </summary>
        public virtual MilestoneStatusConfiguration MilestoneStatusConfiguration { get; set; } // FK_MilestoneEmailConfiguration_MilestoneStatusConfiguration

        public MilestoneEmailConfiguration()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
