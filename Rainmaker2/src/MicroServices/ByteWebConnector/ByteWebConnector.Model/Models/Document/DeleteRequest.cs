namespace ByteWebConnector.Model.Models.Document
{
    public class DeleteRequest
    {
        public int FileDataId { get; set; }
        public int EmbeddedDocId { get; set; }


        public Model.Models.ServiceRequestModels.Document.DeleteRequest GetLosModel()
        {
            var modal =  new Model.Models.ServiceRequestModels.Document.DeleteRequest();

            modal.ExtOriginatorLoanApplicationId = FileDataId;
            modal.ExtOriginatorFileId = EmbeddedDocId;
            modal.ExtOriginatorId = 1;
            return modal;

        }
    }
}