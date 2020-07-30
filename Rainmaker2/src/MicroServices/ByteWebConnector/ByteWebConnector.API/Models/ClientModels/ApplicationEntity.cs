namespace ByteWebConnector.API.Models.ClientModels
{
    public class ApplicationEntity
    {
        public long ApplicationId { get; set; }
        public int ApplicationMethod { get; set; }
        public decimal? LifeInsuranceEstimatedMonthlyAmount { get; set; }
        public int? BorrowerId { get; set; }
        public int? CoBorrowerId { get; set; }
        public long FileDataId { get; set; }
    }
}