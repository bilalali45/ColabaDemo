namespace ByteWebConnector.API.Models.Document
{
    public class DeleteRequest
    {
        public int FileDataId { get; set; }
        public int EmbeddedDocId { get; set; }


        public ClientModels.Document.DeleteRequest GetLosModel()
        {
            var modal =  new ClientModels.Document.DeleteRequest();

            modal.ExtOriginatorLoanApplicationId = FileDataId;
            modal.ExtOriginatorFileId = EmbeddedDocId;
            return modal;

        }
    }
}