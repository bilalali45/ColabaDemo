using ByteWebConnector.Model.Models.ServiceRequestModels;

namespace ByteWebConnector.Model.Models
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


       




        public Asset GetAsset()
        {
            var asset = new Asset();
            asset.AppNo = this.AppNo;
            asset.AssetId = this.AssetId;
            asset.BorrowerId = this.BorrowerId;
            asset.DisplayOrder = this.DisplayOrder;
            asset.Name = this.Name;
            asset.Attn = this.Attn;
            asset.FullAddress = this.FullAddress;
            asset.CityStateZip = this.CityStateZip;
            asset.Street = this.Street;
            asset.City = this.City;
            asset.State = this.State;
            asset.Zip = this.Zip;
            asset.AccountType1 = this.AccountType1;
            asset.AccountNo1 = this.AccountNo1;
            asset.AccountBalance1 = this.AccountBalance1;
            asset.AccountType2 = this.AccountType2;
            asset.AccountNo2 = this.AccountNo2;
            asset.AccountBalance2 = this.AccountBalance2;
            asset.AccountType3 = this.AccountType3;
            asset.AccountNo3 = this.AccountNo3;
            asset.AccountBalance3 = this.AccountBalance3;
            asset.AccountType4 = this.AccountType4;
            asset.AccountNo4 = this.AccountNo4;
            asset.AccountBalance4 = this.AccountBalance4;
            asset.Notes = this.Notes;
            asset.Fax = this.Fax;
            asset.AccountOtherDesc = this.AccountOtherDesc;
            asset.AccountHeldByType = this.AccountHeldByType;
            asset.FileDataId = this.FileDataId;

            return asset;
        }
    }
}
