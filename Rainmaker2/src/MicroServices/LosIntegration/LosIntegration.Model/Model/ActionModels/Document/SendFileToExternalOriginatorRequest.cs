using System.Runtime.Serialization;

namespace LosIntegration.Model.Model.ActionModels.Document
{
    [DataContract]
    public class SendFileToExternalOriginatorRequest
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
    [DataContract]
    public class UpdateByteStatusRequest
    {
        [DataMember(Name = "id")]
        public string DocumentLoanApplicationId { get; set; }
        [DataMember(Name = "requestId")]
        public string RequestId { get; set; }
        [DataMember(Name = "docId")]
        public string DocumentId { get; set; }
        [DataMember(Name = "fileId")]
        public string FileId { get; set; }
        [DataMember(Name = "isUploaded")]
        public bool isUploaded { get; set; }
    }
}
