namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class LeadGenQuestionTree
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string QuestionDesc { get; set; }
        public string AnswerType { get; set; }
        public string AnswerTitle { get; set; }
        public string AnswerValidationExpression { get; set; }
        public int? ParentQuestionId { get; set; }
        public int? NextQuestionId { get; set; }
        public string ViewName { get; set; }
        public int? QuestionGroupId { get; set; }
        public int? Order { get; set; }
        public int? DisplayOrder { get; set; }

        public LeadGenQuestionGroup LeadGenQuestionGroup { get; set; }
    }
}