using LosIntegration.API.Models.ClientModels;

namespace LosIntegration.API.Models
{
    public class SubProperty
    {
        public long SubPropId { get; set; }
        public string FullAddress { get; set; }
        public string CityStateZip { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public object CountyCode { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public object AppraisedValue { get; set; }
        public int PropertyType { get; set; }
        public int? NoUnits { get; set; }
        public string LegalDesc { get; set; }
        public bool MetesAndBounds { get; set; }
        public object YearBuilt { get; set; }
        public object FirstMortPI { get; set; }
        public object FirstMortBalance { get; set; }
        public object SecondMortPI { get; set; }
        public object SecondMortBalance { get; set; }
        public object GrossRentalIncome { get; set; }
        public object VacancyFactorOV { get; set; }
        public bool ReservesRequired { get; set; }
        public object CYearLotAcq { get; set; }
        public object COrigCost { get; set; }
        public object CAmtExLiens { get; set; }
        public decimal? CPresValLot { get; set; }
        public object CImprvCost { get; set; }
        public int? RYearLotAcq { get; set; }
        public decimal? ROrigCost { get; set; }
        public object RAmtExLiens { get; set; }
        public int ImprvMade { get; set; }
        public string ImprvDesc { get; set; }
        public object ImprvCost { get; set; }
        public string MannerTitleHeld { get; set; }
        public int EstHeld { get; set; }
        public object EstLeaseHoldEx { get; set; }
        public string MSA { get; set; }
        public object NetCashFlowOV { get; set; }
        public object AltImpRep { get; set; }
        public object LandValue { get; set; }
        public bool IsPUD { get; set; }
        public int PropertyClass { get; set; }
        public string ProjectName { get; set; }
        public object SquareFeet { get; set; }
        public object PropertyAge { get; set; }
        public object TotalRooms { get; set; }
        public object Bathrooms { get; set; }
        public object Bedrooms { get; set; }
        public object FHAVAUnpaidBalance { get; set; }
        public object RemainingEconomicLife { get; set; }
        public string PropertyTypeCustom { get; set; }
        public string AssessorsParcelNo { get; set; }
        public object PriorSaleDate { get; set; }
        public object PriorSaleAmount { get; set; }
        public object FirstMortOrigAmount { get; set; }
        public object DateLandAcquired { get; set; }
        public object LandPurchasePrice { get; set; }
        public bool LandAcquiredNotByPurchase { get; set; }
        public object AVMConfidenceScore { get; set; }
        public object AVMDeterminationDate { get; set; }
        public object Stories { get; set; }
        public int WarrantableCondo { get; set; }
        public bool PropertyTBD { get; set; }
        public bool SpecialFloodHazardArea { get; set; }
        public int ProjectStatusType { get; set; }
        public int ProjectDesignType { get; set; }
        public object ProjectDwellingUnitCount { get; set; }
        public object ProjectDwellingUnitsSoldCount { get; set; }
        public object PropertyValuationEffectiveDate { get; set; }
        public int PropertyValuationMethod { get; set; }
        public int AVMModelType { get; set; }
        public string PropertyValuationUCDPDocumentIdentifier { get; set; }
        public object BedroomsUnit1 { get; set; }
        public object BedroomsUnit2 { get; set; }
        public object BedroomsUnit3 { get; set; }
        public object BedroomsUnit4 { get; set; }
        public object GrossRentUnit1 { get; set; }
        public object GrossRentUnit2 { get; set; }
        public object GrossRentUnit3 { get; set; }
        public object GrossRentUnit4 { get; set; }
        public string OriginalLoanGSEIdentifier { get; set; }
        public int OriginalLoanOwner { get; set; }
        public object AssessedValue { get; set; }
        public bool CreditSaleIndicator { get; set; }
        public int UCDPFindingsStatusFannie { get; set; }
        public int UCDPFindingsStatusFreddie { get; set; }
        public string LegalDescShort { get; set; }
        public bool PartialSFHA { get; set; }
        public string FirstMortQMATRNotes { get; set; }
        public string SecondMortQMATRNotes { get; set; }
        public int ULDDManufacturedWidthType { get; set; }
        public int ULDDPropertyValuationForm { get; set; }
        public string ParsedHouseNumber { get; set; }
        public string ParsedDirectionPrefix { get; set; }
        public string ParsedStreetName { get; set; }
        public string ParsedStreetSuffix { get; set; }
        public string ParsedDirectionSuffix { get; set; }
        public string ParsedUnitNumber { get; set; }
        public int DelayedSettlementDueToConstruction { get; set; }
        public int AppraisedValueStatusOV { get; set; }
        public int LandLoanStatus { get; set; }
        public int TRIDAltImpRepOption { get; set; }
        public object PartialSFHAStructureCount { get; set; }
        public string PreviousLoanNumber { get; set; }
        public int FREAppraisalFormType { get; set; }
        public string FREAppraisalFormTypeOther { get; set; }
        public int ManufacturedHomeLandPropertyInterest { get; set; }
        public object MultifamilyAffordableUnitsCount { get; set; }
        public int ManufacturedHomeSecuredPropertyType { get; set; }
        public bool IsChattelLoan { get; set; }
        public bool PropertyHasNoAddress { get; set; }
        public string Lot { get; set; }
        public string Block { get; set; }
        public object ManufacturedHomeWidth { get; set; }
        public object ManufacturedHomeLength { get; set; }
        public int ManufacturedHomeAttachedToFoundation { get; set; }
        public int ManufacturedHomeCondition { get; set; }
        public string ManufacturedHomeHudCertLabelId1 { get; set; }
        public string ManufacturedHomeHudCertLabelId2 { get; set; }
        public string ManufacturedHomeHudCertLabelId3 { get; set; }
        public string ManufacturedHomeMake { get; set; }
        public string ManufacturedHomeModel { get; set; }
        public string ManufacturedHomeSerialNo { get; set; }
        public bool HasHomesteadExemption { get; set; }
        public int IsMixedUseProperty { get; set; }
        public bool NetCashFlowDnaDesired { get; set; }
        public bool OtherLoansDnaDesired { get; set; }
        public bool IsConversionOfLandContract { get; set; }
        public bool IsRenovation { get; set; }
        public bool HasCleanEnergyLien { get; set; }
        public object LotAcquiredDate { get; set; }
        public int IndianCountryLandTenure { get; set; }
        public int StreetContainsUnitNumberOv { get; set; }
        public int LandValueType { get; set; }
        public string PropertyDataId { get; set; }
        public long FileDataId { get; set; }


        public SubPropertyEntity GetRainmakerSubProperty()
        {
            var subPropertyEntity = new SubPropertyEntity();
            subPropertyEntity.PAddressCityName = this.City;
            subPropertyEntity.Abbreviation = this.State;
            subPropertyEntity.CountyName = this.County;
            subPropertyEntity.PAddressStreet = this.Street;
            subPropertyEntity.PAddressZipCode = this.Zip;
            subPropertyEntity.PAddressUnitNo = this.NoUnits;
            subPropertyEntity.PropertyValue = this.CPresValLot;
            //subPropertyEntity.NoUnits = this.CAmtExLiens;//loanDetailDb.FirstMOPMortgageBalance + loanDetailDb.SecondMOPMortgageBalance
            //subPropertyEntity.NoUnits = this.RAmtExLiens;//loanDetailDb.FirstMOPMortgageBalance + loanDetailDb.SecondMOPMortgageBalance
            subPropertyEntity.OriginalPurchasePrice = this.ROrigCost;
            subPropertyEntity.DateAcquiredYear = this.RYearLotAcq;//loanDetailDb.DateAcquired.Value.Year
            subPropertyEntity.FileDataId = this.FileDataId;
            return subPropertyEntity;
        }
    }
}
