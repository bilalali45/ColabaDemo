// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

using System.Collections.Generic;

namespace ByteWebConnector.Model.Models.Document
{
    public class EmbeddedDoc
    {
        public int FileDataId { get; set; }
        public int DocumentId { get; set; }
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
        public string ExpirationDate { get; set; }
        public string DocumentData { get; set; }
    }

    public class EmbeddedDocList
    {
        public List<EmbeddedDoc> EmbeddedDocs { get; set; }
    }
}