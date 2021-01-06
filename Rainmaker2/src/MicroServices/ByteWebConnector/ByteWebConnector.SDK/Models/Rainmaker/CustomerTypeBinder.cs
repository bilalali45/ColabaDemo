namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class CustomerTypeBinder
    {
        public int CustomerId { get; set; }
        public int CustomerTypeId { get; set; }

        public Customer Customer { get; set; }

        public CustomerType CustomerType { get; set; }
    }
}