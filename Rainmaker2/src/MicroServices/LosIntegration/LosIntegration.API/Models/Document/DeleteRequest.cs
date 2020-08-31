namespace LosIntegration.API.Models.Document
{
    public class DeleteRequest
    {
        public int ExtOriginatorLoanApplicationId { get; set; }
        public int ExtOriginatorFileId { get; set; }
        public int ExtOriginatorId { get; set; }
    }
}