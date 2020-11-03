using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
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