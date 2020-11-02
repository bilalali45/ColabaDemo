namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class LeadGenAnswerSelectionView
    {
        public int? QuestionGroupId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int? DisplayOrder { get; set; }
        public int AnswerId { get; set; }
        public string TableName { get; set; }
        public string AnswerText { get; set; }
        public string Description { get; set; }
        public int? NexQuestionId { get; set; }
        public string EventName { get; set; }
        public int? NextQuestionGroup { get; set; }
        public int? AnswerDisplayOrder { get; set; }
        public string ImagePath { get; set; }
    }
}