namespace ByteWebConnector.API.Models
{
    public class ByteAsset
    {
        public int AppNo { get; set; }
        public int AssetId { get; set; }
        public int? BorrowerId { get; set; }
        public int DisplayOrder { get; set; }
        public string Name { get; set; }
        public string Attn { get; set; }
        public string FullAddress { get; set; }
        public string CityStateZip { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string AccountType1 { get; set; }
        public string AccountNo1 { get; set; }
        public double? AccountBalance1 { get; set; }
        public string AccountType2 { get; set; }
        public string AccountNo2 { get; set; }
        public double? AccountBalance2 { get; set; }
        public string AccountType3 { get; set; }
        public string AccountNo3 { get; set; }
        public double? AccountBalance3 { get; set; }
        public string AccountType4 { get; set; }
        public string AccountNo4 { get; set; }
        public double? AccountBalance4 { get; set; }
        public string Notes { get; set; }
        public string Fax { get; set; }
        public string AccountOtherDesc { get; set; }
        public string AccountHeldByType { get; set; }
        public long FileDataId { get; set; }


        public object GetRainmakerBorrowerAsset()
        {
            throw new System.NotImplementedException();
        }
    }
}
