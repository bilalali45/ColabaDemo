namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class LendingTreeQuote
    {
        public int Id { get; set; }
        public int? LendingTreeLeadId { get; set; }
        public string VendorId { get; set; }
        public string ProductId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Price { get; set; }
        public decimal? Apr { get; set; }
        public string LtProductName { get; set; }
        public decimal? MonthlyPayment { get; set; }

        public LendingTreeLead LendingTreeLead { get; set; }
    }
}