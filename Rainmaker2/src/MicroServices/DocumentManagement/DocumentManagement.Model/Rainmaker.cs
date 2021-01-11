using System;

namespace DocumentManagement.Model
{
    public class LastDocUploadQuery
    {
        public DateTime? LastDocUploadDate { get; set; }
    }
    public class LastDocRequestSentDateQuery
    {
        public DateTime? LastDocRequestSentDate { get; set; }
    }
    public class RemainingDocumentsQuery
    {
        public bool? isMcuVisible { get; set; }
    }
    public class OutstandingDocumentsQuery
    {
        public bool? isMcuVisible { get; set; }
    }
    public class CompletedDocumentsQuery
    {
        public bool? isMcuVisible { get; set; }
    }
}
