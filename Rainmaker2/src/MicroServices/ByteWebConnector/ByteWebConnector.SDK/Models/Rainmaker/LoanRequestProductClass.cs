namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class LoanRequestProductClass
    {
        public int LoanRequestId { get; set; }
        public int ProductClassId { get; set; }

        public LoanRequest LoanRequest { get; set; }

        public ProductClass ProductClass { get; set; }
    }
}