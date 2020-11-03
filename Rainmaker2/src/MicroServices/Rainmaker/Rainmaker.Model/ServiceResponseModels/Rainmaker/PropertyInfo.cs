using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class PropertyInfo
    {
        public int Id { get; set; }
        public string LenderName { get; set; }
        public string AccountNumber { get; set; }
        public int? AddressInfoId { get; set; }
        public string PropertyStatus { get; set; }
        public int? PropertyTypeId { get; set; }
        public int? PropertyUsageId { get; set; }
        public bool? IsMixedUseProperty { get; set; }
        public decimal? OriginalPurchasePrice { get; set; }
        public DateTime? BuiltDate { get; set; }
        public DateTime? DateAcquired { get; set; }
        public int? NoOfUnits { get; set; }
        public decimal? LandValue { get; set; }
        public decimal? ImprovementCost { get; set; }
        public int? ConstructionPeriod { get; set; }
        public string DescribeImprovement { get; set; }
        public bool? IsImprovementToBeMade { get; set; }
        public decimal? PropertyValue { get; set; }
        public decimal? RentalIncome { get; set; }
        public decimal? RentalPercentage { get; set; }
        public decimal? ParticipationPercentage { get; set; }
        public decimal? FirstMortgageBalance { get; set; }
        public bool? IsSecondLien { get; set; }
        public decimal? SecondLienBalance { get; set; }
        public decimal? SecondLienLimit { get; set; }
        public decimal? MortgageToBePaid { get; set; }
        public decimal? MortgageToBeSubordinate { get; set; }
        public decimal? TotalMonthlyPayment { get; set; }
        public bool? IsWithEscrow { get; set; }
        public string LegalDescription { get; set; }
        public string HoaName { get; set; }
        public decimal? HoaDues { get; set; }
        public string TitleNamesWillbe { get; set; }
        public int? TitleMannerId { get; set; }
        public int? TitleEstateId { get; set; }
        public int? TitleTrustInfoId { get; set; }
        public int? TitleLandTenureId { get; set; }
        public DateTime? LeaseholdExpireDate { get; set; }
        public bool? IsCompletedCashout { get; set; }
        public bool? IntentToSellPriorToPurchase { get; set; }
        public decimal? MonthlyDue { get; set; }

        //public System.Collections.Generic.ICollection<BorrowerProperty> BorrowerProperties { get; set; }

        //public System.Collections.Generic.ICollection<LoanApplication> LoanApplications { get; set; }

        public ICollection<MortgageOnProperty> MortgageOnProperties { get; set; }

        public ICollection<PropertyTaxEscrow> PropertyTaxEscrows { get; set; }

        public AddressInfo AddressInfo { get; set; }

        public PropertyType PropertyType { get; set; }

        public PropertyUsage PropertyUsage { get; set; }

        public TitleEstate TitleEstate { get; set; }

        public TitleLandTenure TitleLandTenure { get; set; }

        public TitleManner TitleManner { get; set; }

        public TitleTrustInfo TitleTrustInfo { get; set; }

    }
}