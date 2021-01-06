namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class SecondLien
    {
        public int Id { get; set; }
        public int? LoanRequestId { get; set; }
        public int? SecondLienTypeId { get; set; }
        public decimal? SecondLienBalance { get; set; }
        public decimal? SecondLienLimit { get; set; }
        public bool SecondLienPaidAtClosing { get; set; }
        public bool WasSmTaken { get; set; }

        public LoanRequest LoanRequest { get; set; }

        public SecondLienType SecondLienType { get; set; }
    }
}