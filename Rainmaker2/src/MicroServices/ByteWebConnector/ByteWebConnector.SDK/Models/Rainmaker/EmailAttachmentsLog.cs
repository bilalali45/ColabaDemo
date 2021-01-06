namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class EmailAttachmentsLog
    {
        public int Id { get; set; }
        public int EmailLogId { get; set; }
        public string FilePath { get; set; }
        public string FileDisplayName { get; set; }
        public string FileType { get; set; }
        public int? FileSizeKBs { get; set; }
        public int EntityTypeId { get; set; }

        public EmailLog EmailLog { get; set; }
    }
}