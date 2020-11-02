













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LeadGenQuestionGroups

    public partial class LeadGenQuestionGroup 
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
