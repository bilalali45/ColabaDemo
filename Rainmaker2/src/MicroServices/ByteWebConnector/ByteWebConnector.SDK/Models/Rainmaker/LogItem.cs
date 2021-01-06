using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class LogItem
    {
        public Guid EventId { get; set; }
        public string AppName { get; set; }
        public DateTime? LogDateTimeUtc { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string Form { get; set; }
        public string QueryString { get; set; }
        public string TargetSite { get; set; }
        public string StackTrace { get; set; }
        public string Referer { get; set; }
        public string Data { get; set; }
        public string Path { get; set; }
        public int EntityTypeId { get; set; }
    }
}