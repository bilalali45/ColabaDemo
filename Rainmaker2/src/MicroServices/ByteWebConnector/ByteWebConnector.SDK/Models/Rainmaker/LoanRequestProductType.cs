namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class LoanRequestProductType
    {
        public int LoanRequestId { get; set; }
        public int ProductTypeId { get; set; }

        public LoanRequest LoanRequest { get; set; }

        public ProductType ProductType { get; set; }
    }
}