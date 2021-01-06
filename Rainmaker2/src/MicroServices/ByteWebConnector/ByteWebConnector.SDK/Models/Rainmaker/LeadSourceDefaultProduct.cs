namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class LeadSourceDefaultProduct
    {
        public int LeadSourceId { get; set; }
        public int ProductTypeId { get; set; }

        public LeadSource LeadSource { get; set; }

        public ProductType ProductType { get; set; }
    }
}