namespace ByteWebConnector.API.Models
{
    public class PrepaidItem
    {
        public long PrepaidItemID { get; set; }
        public long LoanID { get; set; }
        public int PrepaidItemType { get; set; }
        public string NameOV { get; set; }
        public double? HazPayment { get; set; }
        public double? PropTaxPayment { get; set; }
        public long FileDataID { get; set; }
        public int PremiumPaidToType { get; set; }

    }
}
