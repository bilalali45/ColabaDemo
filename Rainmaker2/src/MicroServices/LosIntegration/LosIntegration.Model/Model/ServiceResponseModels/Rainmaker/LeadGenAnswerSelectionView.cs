













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LeadGenAnswerSelectionView

    public partial class LeadGenAnswerSelectionView 
    {
        public int? QuestionGroupId { get; set; } // QuestionGroupId
        public int QuestionId { get; set; } // QuestionId (Primary key)
        public string QuestionText { get; set; } // QuestionText (Primary key) (length: 1000)
        public int? DisplayOrder { get; set; } // DisplayOrder
        public int AnswerId { get; set; } // AnswerID (Primary key)
        public string TableName { get; set; } // TableName (Primary key) (length: 23)
        public string AnswerText { get; set; } // AnswerText (length: 500)
        public string Description { get; set; } // Description (length: 500)
        public int? NexQuestionId { get; set; } // NexQuestionId
        public string EventName { get; set; } // EventName (length: 100)
        public int? NextQuestionGroup { get; set; } // NextQuestionGroup
        public int? AnswerDisplayOrder { get; set; } // AnswerDisplayOrder
        public string ImagePath { get; set; } // ImagePath (length: 1028)

        public LeadGenAnswerSelectionView()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
