using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApplication.Model
{
    public enum IncomeCategory
    {
        Employment=1,
        SelfEmployment=2,
        Business=3,
        MilataryPay=4,
        Retirement=5,
        Rental=6,
        Other=7
    }
    public enum FamilyRelationTypeEnum : byte
    {
        Spouse = 1,
        DomesticPartner = 2,
        Parent = 3,
        Child = 4,
        Sibling = 5,
        Other = 6
    }


    public enum MilitaryAffiliationEnum : byte
    {
        Active_Military =1,
        Reserves_National_Guard = 2,
        Veteran = 3,
        Surviving_Spouse = 4,
    }

    public enum MaritalStatus : byte
    {
        Married = 1,
        Separated = 2,
        Single = 3,
        Divorced = 4,
        Widowed = 5,
        CivilUnion = 6,
        DomesticPartnership = 7,
        RegisteredReciprocalBeneficiaryRelationship = 8
    }

    public enum BorrowerTypes : byte
    {
        PrimaryBorrower = 1,
        CoBorrower = 2
    }

    public enum AssetTypes : byte
    {
        CheckingAccount = 1,
        SavingsAccoount = 2,
        MutualFunds = 3,
        Bonds = 4,
        Stocks = 5,
        StockOptions = 6,
        Moneymarket = 7,
        CertificateOfDeposit = 8,
        RetirementAccount = 9,
        CashGift = 10,
        Grant = 11,
        ProceedsFromALoan = 12,
        ProceedsFromSellingNonRealEstateAssets = 13,
        ProceedsFromSellingRealEstate = 14,
        TrustAccount = 15,
        BridgeLoanProceeds = 16,
        IndividualDevelopmentAccount_Ida=17,
        CashValueOfLifeInsurance = 18,
        EmployerAssistance = 19,
        RelocationFunds = 20,
        RentCredit = 21,
        LotEquity = 22,
        SweatEquity = 23,
        TradeEquity = 24,
        Other = 25,
        GiftOfEquity = 26,
    }

    public enum LoanGoals : byte
    {
        PreApproval = 3,
        PropertyUnderContract = 4,
        LowerPaymentsOrTerm = 5,
        CashOut = 6,
        DebtConsolidation = 7,
        NeedCashForHomeImprovement = 8,
        NeedCashForCollegeTutionAndFees = 9
    }

 

    public enum OtherIncomeTypes
    {
        MilitaryEntitlements = 4
    }

    public enum ProceedsFromTransaction : int
    {
        ProceedsFromALoan=12,
        ProceedsFromSellingNonRealEstate=13,
        ProceedsFromSellingRealEstate=14
    }

    public enum AssetCategory
    {
        BankAccount = 1,
        FinancialStatement =2,
        RetirementAccount =3,
        GiftFunds = 4,
        Credits = 5,
        ProceedsFromTransactions = 6,
        Other = 7
    }


    public enum PropertyTypes
    {
        SingleFamilyProperty = 1,
        Condominium = 2,
        Townhouse = 3,
        Duplex2Unit = 4,
        Triplex3Unit = 5,
        Quadplex4Unit = 6,
        ManufacturedHome = 9
    }

    public enum PropertyUsageEnum
    {
        IWillLiveHerePrimaryResidence = 1,
        FHASecondaryResidence = 2,
        ThisWillBeASecondHome = 3,
        ThisIsAnInvestmentProperty =4
    }
    public enum EscrowEntityTypeEnum
    {
        PropertyTax=1,
        HomeOwnerInsurance=2,
        FloodInsurance=3
    }

    public enum MortgageTypeEnum
    {
        FirstMortgage = 1,
        SecondMortgage = 2
    }

    public enum QuestionSection
    {
        Section1 = 1,
        Section2 = 2,
        Section3 = 3
    }

    public enum QuestionBorrowerDisplayOption
    {
        PrimaryBorrower = 1,
        CoBorrower = 2,
        Both = 3
    }
    public enum LiabilityTypeEnum
    {
        ChildSupport = 1,
        Alimony = 8,
        SeparateMaintenance = 2
    }

    public enum TitleHeldWithEnum
    {
        ByYourself = 1,
        Jointlywithyourspouse = 2,
        TenancyinCommon = 3,
        Jointlywithanotherperson = 4
    }
}
