using System;
using System.Collections.Generic;
using System.Text;

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
        public int? RemainingDocuments { get; set; }
    }
    public class OutstandingDocumentsQuery
    {
        public int? OutstandingDocuments { get; set; }
    }
    public class CompletedDocumentsQuery
    {
        public int? CompletedDocuments { get; set; }
    }
}
