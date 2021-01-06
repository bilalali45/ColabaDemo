namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class PropertyTaxEscrow
    {
        public int Id { get; set; }
        public int PaidById { get; set; }
        public int PropertyDetailId { get; set; }
        public decimal AnnuallyPayment { get; set; }
        public int? EscrowMonth { get; set; }
        public int? EscrowEntityTypeId { get; set; }
        public decimal? PrePaid { get; set; }
        public int? PrePaidMonth { get; set; }

        //public EscrowEntityType EscrowEntityType { get; set; }

        //public PaidBy PaidBy { get; set; }

        //public PropertyInfo PropertyInfo { get; set; }
    }
}