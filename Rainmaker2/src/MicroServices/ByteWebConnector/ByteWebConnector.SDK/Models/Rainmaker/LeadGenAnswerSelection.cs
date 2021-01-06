namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class LeadGenAnswerSelection
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public int? NextQuestionId { get; set; }
        public string EventName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? NextQuestionGroup { get; set; }
        public string ImageFilePath { get; set; }
        public int? DisplayOrder { get; set; }
    }
}