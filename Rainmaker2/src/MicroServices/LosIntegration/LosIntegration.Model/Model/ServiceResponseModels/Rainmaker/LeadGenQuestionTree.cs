













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LeadGenQuestionTree

    public partial class LeadGenQuestionTree 
    {
        public int Id { get; set; } // Id (Primary key)
        public string QuestionText { get; set; } // QuestionText (length: 1000)
        public string QuestionDesc { get; set; } // QuestionDesc
        public string AnswerType { get; set; } // AnswerType (length: 50)
        public string AnswerTitle { get; set; } // AnswerTitle (length: 50)
        public string AnswerValidationExpression { get; set; } // AnswerValidationExpression (length: 1000)
        public int? ParentQuestionId { get; set; } // ParentQuestionId
        public int? NextQuestionId { get; set; } // NextQuestionId
        public string ViewName { get; set; } // ViewName (length: 100)
        public int? QuestionGroupId { get; set; } // QuestionGroupId
        public int? Order { get; set; } // Order
        public int? DisplayOrder { get; set; } // DisplayOrder

        // Foreign keys

        /// <summary>
        /// Parent LeadGenQuestionGroup pointed by [LeadGenQuestionTree].([QuestionGroupId]) (FK_LeadGenQuestionTree_LeadGenQuestionGroups)
        /// </summary>
        public virtual LeadGenQuestionGroup LeadGenQuestionGroup { get; set; } // FK_LeadGenQuestionTree_LeadGenQuestionGroups

        public LeadGenQuestionTree()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
