namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class RuleMessage
    {
        public int MessageId { get; set; }
        public int RuleId { get; set; }

        public MessageOnRule MessageOnRule { get; set; }

        public Rule Rule { get; set; }
    }
}