namespace ByteWebConnector.API.Models
{
    public class ByteREO
    {
        public int AppNo { get; set; }
        public int Reoid { get; set; }
        public int? BorrowerId { get; set; }
        public int DisplayOrder { get; set; }
        public string FullAddress { get; set; }
        public string CityStateZip { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string ReoStatus { get; set; }
        public string ReoType { get; set; }
        public double? MarketValue { get; set; }
        public double? GrossRentalIncome { get; set; }
        public double? Taxes { get; set; }
        public object NetRentalIncomeOV { get; set; }
        public object VacancyFactorOV { get; set; }
        public bool IsSubjectProperty { get; set; }
        public bool IsCurrentResidence { get; set; }
        public object PITIOV { get; set; }
        public bool TIIncludedInMortgage { get; set; }
        public bool VAPurchasedOrRefinancedWithVALoan { get; set; }
        public object VALoanDate { get; set; }
        public int VAEntitlementRestoration { get; set; }
        public bool MortgagesDNADesired { get; set; }
        public int StreetContainsUnitNumberOV { get; set; }
        public int AccountHeldByType { get; set; }
        public int? NoUnits { get; set; }
        public string CurrentUsageType { get; set; }
        public int IntendedUsageType { get; set; }
        public string Country { get; set; }
        public long FileDataId { get; set; }


        public object GetRainmakerBorrowerReo()
        {
            throw new System.NotImplementedException();
        }
    }
}
