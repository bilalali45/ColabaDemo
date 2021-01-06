namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class OwnerShipInterest
    {
        public int Id { get; set; }
        public int? BorrowerId { get; set; }
        public int? PropertyTypeId { get; set; }
        public int? TitleHeldWithId { get; set; }

        public Borrower Borrower { get; set; }

        public PropertyType PropertyType { get; set; }

        public TitleHeldWith TitleHeldWith { get; set; }
    }
}