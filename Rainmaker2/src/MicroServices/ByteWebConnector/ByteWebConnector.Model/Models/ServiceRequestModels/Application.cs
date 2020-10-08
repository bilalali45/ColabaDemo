namespace ByteWebConnector.Model.Models.ServiceRequestModels
{
    public class Application
    {
        public long ApplicationId { get; set; }
        public int ApplicationMethod { get; set; }
        public decimal? LifeInsuranceEstimatedMonthlyAmount { get; set; }
        public int? BorrowerId { get; set; }
        public int? CoBorrowerId { get; set; }
        public long FileDataId { get; set; }
    }
}