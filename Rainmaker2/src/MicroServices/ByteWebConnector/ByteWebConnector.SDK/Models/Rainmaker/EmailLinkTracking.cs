using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class EmailLinkTracking
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TrackingKey { get; set; }
        public string Description { get; set; }

        public ICollection<EmailTracking> EmailTrackings { get; set; }
    }
}