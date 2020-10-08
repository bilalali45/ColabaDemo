namespace ByteWebConnector.API.Models
{
    public class ByteReo
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


        public ByteReo GetBorrowerReo()
        {
            var byteReo = new ByteReo();
            byteReo.AppNo = this.AppNo;
            byteReo.Reoid = this.Reoid;
            byteReo.BorrowerId = this.BorrowerId;
            byteReo.DisplayOrder = this.DisplayOrder;
            byteReo.FullAddress = this.FullAddress;
            byteReo.CityStateZip = this.CityStateZip;
            byteReo.Street = this.Street;
            byteReo.City = this.City;
            byteReo.State = this.State;
            byteReo.Zip = this.Zip;
            byteReo.ReoStatus = this.ReoStatus;
            byteReo.ReoType = this.ReoType;
            byteReo.MarketValue = this.MarketValue;
            byteReo.GrossRentalIncome = this.GrossRentalIncome;
            byteReo.Taxes = this.Taxes;
            byteReo.NetRentalIncomeOV = this.NetRentalIncomeOV;
            byteReo.VacancyFactorOV = this.VacancyFactorOV;
            byteReo.IsSubjectProperty = this.IsSubjectProperty;
            byteReo.IsCurrentResidence = this.IsCurrentResidence;
            byteReo.PITIOV = this.PITIOV;
            byteReo.TIIncludedInMortgage = this.TIIncludedInMortgage;
            byteReo.VAPurchasedOrRefinancedWithVALoan = this.VAPurchasedOrRefinancedWithVALoan;
            byteReo.VALoanDate = this.VALoanDate;
            byteReo.VAEntitlementRestoration = this.VAEntitlementRestoration;
            byteReo.MortgagesDNADesired = this.MortgagesDNADesired;
            byteReo.StreetContainsUnitNumberOV = this.StreetContainsUnitNumberOV;
            byteReo.AccountHeldByType = this.AccountHeldByType;
            byteReo.NoUnits = this.NoUnits;
            byteReo.CurrentUsageType = this.CurrentUsageType;
            byteReo.IntendedUsageType = this.IntendedUsageType;
            byteReo.Country = this.Country;
            byteReo.FileDataId = this.FileDataId;
            return byteReo;
        }
    }
}
