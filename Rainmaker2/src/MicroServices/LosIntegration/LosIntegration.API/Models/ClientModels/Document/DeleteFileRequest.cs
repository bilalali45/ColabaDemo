namespace LosIntegration.API.Models.ClientModels.Document
{
    public class DeleteFileRequest
    {
        public string  FileId { get; set; }
        public int LoanApplicationId { get; set; }
    }
}
