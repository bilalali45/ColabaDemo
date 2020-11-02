namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class LoanRequestProduct
    {
        public int LoanRequestId { get; set; }
        public int ProductId { get; set; }

        public LoanRequest LoanRequest { get; set; }

        public Product Product { get; set; }
    }
}