namespace LosIntegration.API.Models.ClientModels.Document
{
    public class GetDocumentsRequest
    {
        public int LoanApplicationId { get; set; }
        public bool Pending { get; set; }
    }
}
