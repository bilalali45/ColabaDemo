













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // LeadGenAnswerSelection

    public partial class LeadGenAnswerSelection 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Text { get; set; } // Text (length: 500)
        public int QuestionId { get; set; } // QuestionId
        public int? NextQuestionId { get; set; } // NextQuestionId
        public string EventName { get; set; } // EventName (length: 100)
        public bool IsActive { get; set; } // IsActive
        public bool IsDeleted { get; set; } // IsDeleted
        public int? NextQuestionGroup { get; set; } // NextQuestionGroup
        public string ImageFilePath { get; set; } // ImageFilePath (length: 100)
        public int? DisplayOrder { get; set; } // DisplayOrder

        public LeadGenAnswerSelection()
        {
            IsActive = true;
            IsDeleted = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
