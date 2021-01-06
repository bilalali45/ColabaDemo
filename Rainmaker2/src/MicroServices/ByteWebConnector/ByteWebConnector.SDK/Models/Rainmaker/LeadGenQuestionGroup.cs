using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class LeadGenQuestionGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string GroupViewName { get; set; }

        public ICollection<LeadGenQuestionTree> LeadGenQuestionTrees { get; set; }
    }
}