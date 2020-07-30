namespace ByteWebConnector.API.Models
{
    public class ByteAsset
    {
        public int AppNo { get; set; }
        public int AssetID { get; set; }
        public int? BorrowerID { get; set; }
        public int DisplayOrder { get; set; }
        public string Name { get; set; }
        public string Attn { get; set; }
        public string FullAddress { get; set; }
        public string CityStateZip { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int AccountType1 { get; set; }
        public string AccountNo1 { get; set; }
        public string AccountBalance1 { get; set; }
        public int AccountType2 { get; set; }
        public string AccountNo2 { get; set; }
        public object AccountBalance2 { get; set; }
        public int AccountType3 { get; set; }
        public string AccountNo3 { get; set; }
        public object AccountBalance3 { get; set; }
        public int AccountType4 { get; set; }
        public string AccountNo4 { get; set; }
        public object AccountBalance4 { get; set; }
        public string Notes { get; set; }
        public string Fax { get; set; }
        public string AccountOtherDesc { get; set; }
        public int AccountHeldByType { get; set; }
        public long FileDataID { get; set; }


        public object GetRainmakerBorrowerAsset()
        {
            throw new System.NotImplementedException();
        }
    }
}
