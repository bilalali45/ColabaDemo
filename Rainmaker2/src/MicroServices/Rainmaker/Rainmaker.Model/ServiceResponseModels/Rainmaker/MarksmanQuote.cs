namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class MarksmanQuote
    {
        public int Id { get; set; }
        public int? MarksmanLeadId { get; set; }
        public string VendorId { get; set; }
        public string ProductId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Price { get; set; }
        public decimal? Apr { get; set; }
        public string LtProductName { get; set; }
        public decimal? MonthlyPayment { get; set; }

        public MarksmanLead MarksmanLead { get; set; }
    }
}