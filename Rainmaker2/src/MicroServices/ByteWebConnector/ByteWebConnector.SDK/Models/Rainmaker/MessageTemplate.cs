namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class MessageTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BccEmailAddresses { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public int EmailAccountId { get; set; }
        public bool IsDeleted { get; set; }
    }
}