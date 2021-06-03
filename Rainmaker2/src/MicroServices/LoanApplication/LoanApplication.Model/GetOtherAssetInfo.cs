namespace LoanApplication.Model
{
    public class GetOtherAssetInfo
    {
        public int AssetId { get; set; }
        public int AssetTypeId { get; set; }
        public string AssetTypeName { get; set; }
        public decimal? AssetValue { get; set; }
        public string AssetDescription { get; set; }
        public string InstitutionName { get; set; }
        public string AccountNumber { get; set; }
    }
}