namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class VendorCustomerBinder
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public int CustomerId { get; set; }
        public int? OwnTypeId { get; set; }

        public Customer Customer { get; set; }

        public OwnType OwnType { get; set; }

        public Vendor Vendor { get; set; }
    }
}