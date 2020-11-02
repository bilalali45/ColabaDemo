namespace LosIntegration.Model.Model.ServiceRequestModels.DocumentManagement
{
    public class DeleteFileRequest
    {
        public string  FileId { get; set; }
        public int LoanApplicationId { get; set; }
    }
}
