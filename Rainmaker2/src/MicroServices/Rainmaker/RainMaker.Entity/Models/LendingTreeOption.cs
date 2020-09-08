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
    // LendingTreeOption

    public partial class LendingTreeOption : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? LendingTreeLeadId { get; set; } // LendingTreeLeadId
        public int? OptionId { get; set; } // OptionId
        public string Option { get; set; } // Option (length: 500)

        // Foreign keys

        /// <summary>
        /// Parent LendingTreeLead pointed by [LendingTreeOption].([LendingTreeLeadId]) (FK_LendingTreeOption_LendingTreeLead)
        /// </summary>
        public virtual LendingTreeLead LendingTreeLead { get; set; } // FK_LendingTreeOption_LendingTreeLead

        public LendingTreeOption()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
