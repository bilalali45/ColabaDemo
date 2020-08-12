using System.Runtime.Serialization;

namespace ByteWebConnector.API.Models
{
    [DataContract]
    public class SendToExternalOriginatorRequest
    {
        [DataMember]
        public int LoanApplicationId { get; set; }
        [DataMember(Name = "id")]
        public string DocumentLoanApplicationId { get; set; }
        [DataMember(Name = "requestId")]
        public string RequestId { get; set; }
        [DataMember(Name = "docId")]
        public string DocumentId { get; set; }
        [DataMember(Name = "fileId")]
        public string FileId { get; set; }

        
    }
}
