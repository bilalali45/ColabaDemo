namespace LosIntegration.API.Models
{
    public class PrepaidItem
    {
        public long PrepaidItemId { get; set; }
        public long LoanId { get; set; }
        public string PrepaidItemType { get; set; }
        public string NameOV { get; set; }
        public double? HazPayment { get; set; }
        public double? PropTaxPayment { get; set; }
        public long FileDataId { get; set; }
        public int PremiumPaidToType { get; set; }


        public object GetRainmakerPrepaidItem()
        {
            throw new System.NotImplementedException();
        }
    }
}
