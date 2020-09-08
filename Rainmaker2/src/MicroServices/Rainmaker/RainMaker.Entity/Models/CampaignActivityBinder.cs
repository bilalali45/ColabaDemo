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
    // CampaignActivityBinder

    public partial class CampaignActivityBinder : URF.Core.EF.Trackable.Entity
    {
        public int CampaignId { get; set; } // CampaignId (Primary key)
        public int ActivityId { get; set; } // ActivityId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent Activity pointed by [CampaignActivityBinder].([ActivityId]) (FK_CampaignActivityBinder_Activity)
        /// </summary>
        public virtual Activity Activity { get; set; } // FK_CampaignActivityBinder_Activity

        /// <summary>
        /// Parent Campaign pointed by [CampaignActivityBinder].([CampaignId]) (FK_CampaignActivityBinder_Campaign)
        /// </summary>
        public virtual Campaign Campaign { get; set; } // FK_CampaignActivityBinder_Campaign

        public CampaignActivityBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
