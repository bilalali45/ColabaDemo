using System;

namespace ByteWebConnector.Model.Models.ServiceResponseModels.ByteApi
{
    public class DocumentUploadResponse
    {
        public long FileDataId { get; set; }
        public long DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentCategory { get; set; }
        public int DocumentStatus { get; set; }
        public string DocumentExension { get; set; }
        public bool Viewable { get; set; }
        public int NeededItemId { get; set; }
        public int ConditionId { get; set; }
        public bool Internal { get; set; }
        public bool Outdated { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string DocumentData { get; set; }
        public short ExtOriginatorId { get; set; }
    }
}