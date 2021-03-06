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
    // LeadGenQuestionGroups

    public partial class LeadGenQuestionGroup : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string GroupName { get; set; } // GroupName (length: 50)
        public string GroupDescription { get; set; } // GroupDescription
        public string GroupViewName { get; set; } // GroupViewName (length: 100)

        // Reverse navigation

        /// <summary>
        /// Child LeadGenQuestionTrees where [LeadGenQuestionTree].[QuestionGroupId] point to this entity (FK_LeadGenQuestionTree_LeadGenQuestionGroups)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LeadGenQuestionTree> LeadGenQuestionTrees { get; set; } // LeadGenQuestionTree.FK_LeadGenQuestionTree_LeadGenQuestionGroups

        public LeadGenQuestionGroup()
        {
            LeadGenQuestionTrees = new System.Collections.Generic.HashSet<LeadGenQuestionTree>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
