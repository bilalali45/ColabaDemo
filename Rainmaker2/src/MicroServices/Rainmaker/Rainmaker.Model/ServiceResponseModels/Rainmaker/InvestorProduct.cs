namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class InvestorProduct
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? InvestorId { get; set; }
        public int? DtiHousing { get; set; }
        public int? DtiTotal { get; set; }
        public string Code { get; set; }
        public int EntityTypeId { get; set; }
        public string ProductDescription { get; set; }

        public Investor Investor { get; set; }

        public Product Product { get; set; }
    }
}