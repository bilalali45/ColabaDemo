namespace ByteWebConnector.Model.Models
{
    public class ByteSubProperty
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


        public ByteSubProperty GetSubProperty()
        {
            var byteSubProperty = new ByteSubProperty();
            byteSubProperty.SubPropId = this.SubPropId;
            byteSubProperty.FullAddress = this.FullAddress;
            byteSubProperty.CityStateZip = this.CityStateZip;
            byteSubProperty.Street = this.Street;
            byteSubProperty.City = this.City;
            byteSubProperty.County = this.County;
            byteSubProperty.CountyCode = this.CountyCode;
            byteSubProperty.State = this.State;
            byteSubProperty.Zip = this.Zip;
            byteSubProperty.AppraisedValue = this.AppraisedValue;
            byteSubProperty.PropertyType = this.PropertyType;
            byteSubProperty.NoUnits = this.NoUnits;
            byteSubProperty.LegalDesc = this.LegalDesc;
            byteSubProperty.MetesAndBounds = this.MetesAndBounds;
            byteSubProperty.YearBuilt = this.YearBuilt;
            byteSubProperty.FirstMortPI = this.FirstMortPI;
            byteSubProperty.FirstMortBalance = this.FirstMortBalance;
            byteSubProperty.SecondMortPI = this.SecondMortPI;
            byteSubProperty.SecondMortBalance = this.SecondMortBalance;
            byteSubProperty.GrossRentalIncome = this.GrossRentalIncome;
            byteSubProperty.VacancyFactorOV = this.VacancyFactorOV;
            byteSubProperty.ReservesRequired = this.ReservesRequired;
            byteSubProperty.CYearLotAcq = this.CYearLotAcq;
            byteSubProperty.COrigCost = this.COrigCost;
            byteSubProperty.CAmtExLiens = this.CAmtExLiens;
            byteSubProperty.CPresValLot = this.CPresValLot;
            byteSubProperty.CImprvCost = this.CImprvCost;
            byteSubProperty.RYearLotAcq = this.RYearLotAcq;
            byteSubProperty.ROrigCost = this.ROrigCost;
            byteSubProperty.RAmtExLiens = this.RAmtExLiens;
            byteSubProperty.ImprvMade = this.ImprvMade;
            byteSubProperty.ImprvDesc = this.ImprvDesc;
            byteSubProperty.ImprvCost = this.ImprvCost;
            byteSubProperty.MannerTitleHeld = this.MannerTitleHeld;
            byteSubProperty.EstHeld = this.EstHeld;
            byteSubProperty.EstLeaseHoldEx = this.EstLeaseHoldEx;
            byteSubProperty.MSA = this.MSA;
            byteSubProperty.NetCashFlowOV = this.NetCashFlowOV;
            byteSubProperty.AltImpRep = this.AltImpRep;
            byteSubProperty.LandValue = this.LandValue;
            byteSubProperty.IsPUD = this.IsPUD;
            byteSubProperty.PropertyClass = this.PropertyClass;
            byteSubProperty.ProjectName = this.ProjectName;
            byteSubProperty.SquareFeet = this.SquareFeet;
            byteSubProperty.PropertyAge = this.PropertyAge;
            byteSubProperty.TotalRooms = this.TotalRooms;
            byteSubProperty.Bathrooms = this.Bathrooms;
            byteSubProperty.Bedrooms = this.Bedrooms;
            byteSubProperty.FHAVAUnpaidBalance = this.FHAVAUnpaidBalance;
            byteSubProperty.RemainingEconomicLife = this.RemainingEconomicLife;
            byteSubProperty.PropertyTypeCustom = this.PropertyTypeCustom;
            byteSubProperty.AssessorsParcelNo = this.AssessorsParcelNo;
            byteSubProperty.PriorSaleDate = this.PriorSaleDate;
            byteSubProperty.PriorSaleAmount = this.PriorSaleAmount;
            byteSubProperty.FirstMortOrigAmount = this.FirstMortOrigAmount;
            byteSubProperty.DateLandAcquired = this.DateLandAcquired;
            byteSubProperty.LandPurchasePrice = this.LandPurchasePrice;
            byteSubProperty.LandAcquiredNotByPurchase = this.LandAcquiredNotByPurchase;
            byteSubProperty.AVMConfidenceScore = this.AVMConfidenceScore;
            byteSubProperty.AVMDeterminationDate = this.AVMDeterminationDate;
            byteSubProperty.Stories = this.Stories;
            byteSubProperty.WarrantableCondo = this.WarrantableCondo;
            byteSubProperty.PropertyTBD = this.PropertyTBD;
            byteSubProperty.SpecialFloodHazardArea = this.SpecialFloodHazardArea;
            byteSubProperty.ProjectStatusType = this.ProjectStatusType;
            byteSubProperty.ProjectDesignType = this.ProjectDesignType;
            byteSubProperty.ProjectDwellingUnitCount = this.ProjectDwellingUnitCount;
            byteSubProperty.ProjectDwellingUnitsSoldCount = this.ProjectDwellingUnitsSoldCount;
            byteSubProperty.PropertyValuationEffectiveDate = this.PropertyValuationEffectiveDate;
            byteSubProperty.PropertyValuationMethod = this.PropertyValuationMethod;
            byteSubProperty.AVMModelType = this.AVMModelType;
            byteSubProperty.PropertyValuationUCDPDocumentIdentifier = this.PropertyValuationUCDPDocumentIdentifier;
            byteSubProperty.BedroomsUnit1 = this.BedroomsUnit1;
            byteSubProperty.BedroomsUnit2 = this.BedroomsUnit2;
            byteSubProperty.BedroomsUnit3 = this.BedroomsUnit3;
            byteSubProperty.BedroomsUnit4 = this.BedroomsUnit4;
            byteSubProperty.GrossRentUnit1 = this.GrossRentUnit1;
            byteSubProperty.GrossRentUnit2 = this.GrossRentUnit2;
            byteSubProperty.GrossRentUnit3 = this.GrossRentUnit3;
            byteSubProperty.GrossRentUnit4 = this.GrossRentUnit4;
            byteSubProperty.OriginalLoanGSEIdentifier = this.OriginalLoanGSEIdentifier;
            byteSubProperty.OriginalLoanOwner = this.OriginalLoanOwner;
            byteSubProperty.AssessedValue = this.AssessedValue;
            byteSubProperty.CreditSaleIndicator = this.CreditSaleIndicator;
            byteSubProperty.UCDPFindingsStatusFannie = this.UCDPFindingsStatusFannie;
            byteSubProperty.UCDPFindingsStatusFreddie = this.UCDPFindingsStatusFreddie;
            byteSubProperty.LegalDescShort = this.LegalDescShort;
            byteSubProperty.PartialSFHA = this.PartialSFHA;
            byteSubProperty.FirstMortQMATRNotes = this.FirstMortQMATRNotes;
            byteSubProperty.SecondMortQMATRNotes = this.SecondMortQMATRNotes;
            byteSubProperty.ULDDManufacturedWidthType = this.ULDDManufacturedWidthType;
            byteSubProperty.ULDDPropertyValuationForm = this.ULDDPropertyValuationForm;
            byteSubProperty.ParsedHouseNumber = this.ParsedHouseNumber;
            byteSubProperty.ParsedDirectionPrefix = this.ParsedDirectionPrefix;
            byteSubProperty.ParsedStreetName = this.ParsedStreetName;
            byteSubProperty.ParsedStreetSuffix = this.ParsedStreetSuffix;
            byteSubProperty.ParsedDirectionSuffix = this.ParsedDirectionSuffix;
            byteSubProperty.ParsedUnitNumber = this.ParsedUnitNumber;
            byteSubProperty.DelayedSettlementDueToConstruction = this.DelayedSettlementDueToConstruction;
            byteSubProperty.AppraisedValueStatusOV = this.AppraisedValueStatusOV;
            byteSubProperty.LandLoanStatus = this.LandLoanStatus;
            byteSubProperty.TRIDAltImpRepOption = this.TRIDAltImpRepOption;
            byteSubProperty.PartialSFHAStructureCount = this.PartialSFHAStructureCount;
            byteSubProperty.PreviousLoanNumber = this.PreviousLoanNumber;
            byteSubProperty.FREAppraisalFormType = this.FREAppraisalFormType;
            byteSubProperty.FREAppraisalFormTypeOther = this.FREAppraisalFormTypeOther;
            byteSubProperty.ManufacturedHomeLandPropertyInterest = this.ManufacturedHomeLandPropertyInterest;
            byteSubProperty.MultifamilyAffordableUnitsCount = this.MultifamilyAffordableUnitsCount;
            byteSubProperty.ManufacturedHomeSecuredPropertyType = this.ManufacturedHomeSecuredPropertyType;
            byteSubProperty.IsChattelLoan = this.IsChattelLoan;
            byteSubProperty.PropertyHasNoAddress = this.PropertyHasNoAddress;
            byteSubProperty.Lot = this.Lot;
            byteSubProperty.Block = this.Block;
            byteSubProperty.ManufacturedHomeWidth = this.ManufacturedHomeWidth;
            byteSubProperty.ManufacturedHomeLength = this.ManufacturedHomeLength;
            byteSubProperty.ManufacturedHomeAttachedToFoundation = this.ManufacturedHomeAttachedToFoundation;
            byteSubProperty.ManufacturedHomeCondition = this.ManufacturedHomeCondition;
            byteSubProperty.ManufacturedHomeHudCertLabelId1 = this.ManufacturedHomeHudCertLabelId1;
            byteSubProperty.ManufacturedHomeHudCertLabelId2 = this.ManufacturedHomeHudCertLabelId2;
            byteSubProperty.ManufacturedHomeHudCertLabelId3 = this.ManufacturedHomeHudCertLabelId3;
            byteSubProperty.ManufacturedHomeMake = this.ManufacturedHomeMake;
            byteSubProperty.ManufacturedHomeModel = this.ManufacturedHomeModel;
            byteSubProperty.ManufacturedHomeSerialNo = this.ManufacturedHomeSerialNo;
            byteSubProperty.HasHomesteadExemption = this.HasHomesteadExemption;
            byteSubProperty.IsMixedUseProperty = this.IsMixedUseProperty;
            byteSubProperty.NetCashFlowDnaDesired = this.NetCashFlowDnaDesired;
            byteSubProperty.OtherLoansDnaDesired = this.OtherLoansDnaDesired;
            byteSubProperty.IsConversionOfLandContract = this.IsConversionOfLandContract;
            byteSubProperty.IsRenovation = this.IsRenovation;
            byteSubProperty.HasCleanEnergyLien = this.HasCleanEnergyLien;
            byteSubProperty.LotAcquiredDate = this.LotAcquiredDate;
            byteSubProperty.IndianCountryLandTenure = this.IndianCountryLandTenure;
            byteSubProperty.StreetContainsUnitNumberOv = this.StreetContainsUnitNumberOv;
            byteSubProperty.LandValueType = this.LandValueType;
            byteSubProperty.PropertyDataId = this.PropertyDataId;
            byteSubProperty.FileDataId = this.FileDataId;
            return byteSubProperty;
        }
    }
}
