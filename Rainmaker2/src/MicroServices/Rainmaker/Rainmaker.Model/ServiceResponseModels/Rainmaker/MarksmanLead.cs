using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class MarksmanLead
    {
        public int Id { get; set; }
        public string LeadFileName { get; set; }
        public string AckFileName { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? ProspectId { get; set; }
        public string ProspectTrackingId { get; set; }
        public int? ProspectSource { get; set; }

        public MarksmanLeadDetail MarksmanLeadDetail { get; set; }

        public ICollection<MarksmanQuote> MarksmanQuotes { get; set; }
    }
}