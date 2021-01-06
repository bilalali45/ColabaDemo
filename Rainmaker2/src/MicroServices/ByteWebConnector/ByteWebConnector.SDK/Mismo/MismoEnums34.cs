using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByteWebConnector.SDK.Mismo
{
    public enum AssetAccountBase
    {

        /// <remarks/>
        BorrowerManagedAccount,

        /// <remarks/>
        Other,

        /// <remarks/>
        RelatedPartyManagedAccount,
    }

    public enum DataNotSuppliedReasonBase
    {

        /// <remarks/>
        NotCollected,

        /// <remarks/>
        NotReceived,

        /// <remarks/>
        NotRelevant,

        /// <remarks/>
        Omitted,

        /// <remarks/>
        Other,
    }

    public enum AssetBase : byte
    {

        /// <remarks/>
        Annuity = 101,

        /// <remarks/>
        Automobile = 102,

        /// <remarks/>
        Boat = 103,

        /// <remarks/>
        Bond = 10,

        /// <remarks/>
        BorrowerEstimatedTotalAssets = 104,

        /// <remarks/>
        BorrowerPrimaryHome = 105,

        /// <remarks/>
        BridgeLoanNotDeposited = 27,

        /// <remarks/>
        CashOnHand = 30,

        /// <remarks/>
        CertificateOfDepositTimeDeposit = 20,

        /// <remarks/>
        CheckingAccount = 15,

        /// <remarks/>
        EarnestMoneyCashDepositTowardPurchase = 1,

        /// <remarks/>
        EmployerAssistance = 5,

        /// <remarks/>
        GiftOfCash = 11,

        /// <remarks/>
        GiftOfPropertyEquity = 12,

        // GiftsNotDeposited TODO

        /// <remarks/>
        GiftsTotal = 24,

        /// <remarks/>
        Grant = 13,

        /// <remarks/>
        IndividualDevelopmentAccount = 106,

        /// <remarks/>
        LifeInsurance = 14,

        /// <remarks/>
        MoneyMarketFund = 21,

        /// <remarks/>
        MutualFund = 17,

        /// <remarks/>
        NetWorthOfBusinessOwned = 31,

        /// <remarks/>
        Other = 10,

        /// <remarks/>
        PendingNetSaleProceedsFromRealEstateAssets = 19,

        /// <remarks/>
        ProceedsFromSaleOfNonRealEstateAsset = 107,

        /// <remarks/>
        ProceedsFromSecuredLoan = 108,

        /// <remarks/>
        ProceedsFromUnsecuredLoan = 109,

        /// <remarks/>
        RealEstateOwned = 110,

        /// <remarks/>
        RecreationalVehicle = 111,

        /// <remarks/>
        RelocationMoney = 112,

        /// <remarks/>
        RetirementFund = 23,

        /// <remarks/>
        SaleOtherAssets = 113,

        /// <remarks/>
        SavingsAccount = 16,

        /// <remarks/>
        SavingsBond = 114,

        /// <remarks/>
        SeverancePackage = 115,

        /// <remarks/>
        Stock = 8,

        /// <remarks/>
        StockOptions = 116,

        /// <remarks/>
        TrustAccount = 22,
    }

    public enum FundsSourceBase
    {

        /// <remarks/>
        Borrower,

        /// <remarks/>
        Builder,

        /// <remarks/>
        CommunityNonProfit,

        /// <remarks/>
        Employer,

        /// <remarks/>
        FederalAgency,

        /// <remarks/>
        Institutional,

        /// <remarks/>
        Lender,

        /// <remarks/>
        LocalAgency,

        /// <remarks/>
        NonOriginatingFinancialInstitution,

        /// <remarks/>
        NonParentRelative,

        /// <remarks/>
        Other,

        /// <remarks/>
        Parent,

        /// <remarks/>
        PropertySeller,

        /// <remarks/>
        Relative,

        /// <remarks/>
        ReligiousNonProfit,

        /// <remarks/>
        RuralHousingService,

        /// <remarks/>
        StateAgency,

        /// <remarks/>
        Unknown,

        /// <remarks/>
        UnmarriedPartner,

        /// <remarks/>
        UnrelatedFriend,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public enum VerificationMethodBase
    {

        /// <remarks/>
        Other,

        /// <remarks/>
        Verbal,

        /// <remarks/>
        Written,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public enum VerificationStatusBase
    {

        /// <remarks/>
        NotVerified,

        /// <remarks/>
        ToBeVerified,

        /// <remarks/>
        Verified,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public enum AssetDocumentBase
    {

        /// <remarks/>
        BankStatement,

        /// <remarks/>
        FinancialStatement,

        /// <remarks/>
        InvestmentAccountStatement,

        /// <remarks/>
        Other,

        /// <remarks/>
        Receipt,

        /// <remarks/>
        RelocationBuyoutAgreement,

        /// <remarks/>
        RetirementAccountStatement,

        /// <remarks/>
        SettlementStatement,

        /// <remarks/>
        VerbalStatement,

        /// <remarks/>
        VerificationOfDeposit,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public enum VerificationRangeBase
    {

        /// <remarks/>
        MostRecentDays,

        /// <remarks/>
        MostRecentMonths,

        /// <remarks/>
        MostRecentYear,

        /// <remarks/>
        Other,

        /// <remarks/>
        PaymentPeriod,

        /// <remarks/>
        StatementPeriod,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public enum DocumentationStateBase
    {

        /// <remarks/>
        AsCollected,

        /// <remarks/>
        AsRequested,

        /// <remarks/>
        AsRequired,

        /// <remarks/>
        Other,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public enum PropertyUsageBase : byte
    {

        /// <remarks/>
        Investment = 2,

        /// <remarks/>
        Other = 101,

        /// <remarks/>
        PrimaryResidence = 1,

        /// <remarks/>
        SecondHome = 3,
    }

    public enum RainmakerPropertyUnitType
    {
        TwoUnitBuilding = 4,
        ThreeUnitBuilding = 5,
        FourUnitBuilding = 6,
    }

    public enum LoanPurposeBase : byte
    {

        /// <remarks/>
        MortgageModification = 101,

        /// <remarks/>
        Other = 3,

        /// <remarks/>
        Purchase = 1,

        /// <remarks/>
        Refinance = 2,

        /// <remarks/>
        Unknown = 102,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public enum LiabilityBase : byte
    {

        /// <remarks/>
        BorrowerEstimatedTotalMonthlyLiabilityPayment = 101,

        /// <remarks/>
        CollectionsJudgmentsAndLiens = 16,

        /// <remarks/>
        DeferredStudentLoan = 102,

        /// <remarks/>
        DelinquentTaxes = 103,

        /// <remarks/>
        FirstPositionMortgageLien = 104,

        /// <remarks/>
        Garnishments = 105,

        /// <remarks/>
        HELOC = 17,

        /// <remarks/>
        HomeownersAssociationLien = 106,

        /// <remarks/>
        Installment = 18,

        /// <remarks/>
        LeasePayment = 19,

        /// <remarks/>
        MonetaryJudgment = 107,

        /// <remarks/>
        MortgageLoan = 20,

        /// <remarks/>
        Open30DayChargeAccount = 21,

        /// <remarks/>
        Other = 22,

        /// <remarks/>
        PersonalLoan = 4,

        /// <remarks/>
        Revolving = 23,

        /// <remarks/>
        SecondPositionMortgageLien = 108,

        /// <remarks/>
        Taxes = 26,

        /// <remarks/>
        TaxLien = 109,

        /// <remarks/>
        ThirdPositionMortgageLien = 110,

        /// <remarks/>
        UnsecuredHomeImprovementLoanInstallment = 111,

        /// <remarks/>
        UnsecuredHomeImprovementLoanRevolving = 112,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public enum LoanRoleBase
    {

        /// <remarks/>
        HistoricalLoan,

        /// <remarks/>
        RelatedLoan,

        /// <remarks/>
        SubjectLoan,
    }

    public enum AmortizationBase : byte
    {

        /// <remarks/>
        AdjustableRate = 2,

        /// <remarks/>
        Fixed = 1,

        /// <remarks/>
        GEM = 101,

        /// <remarks/>
        GPM = 3,

        /// <remarks/>
        GraduatedPaymentARM = 102,

        /// <remarks/>
        Other = 4,

        /// <remarks/>
        RateImprovementMortgage = 103,

        /// <remarks/>
        Step = 104,
    }

    public enum LoanAmortizationPeriodBase : byte
    {

        /// <remarks/>
        Biweekly = 1,

        /// <remarks/>
        Day = 2,

        /// <remarks/>
        Month = 3,

        /// <remarks/>
        Quarter = 4,

        /// <remarks/>
        Semimonthly = 5,

        /// <remarks/>
        Week = 6,

        /// <remarks/>
        Year = 7,
    }

    public enum LienPriorityBase
    {

        /// <remarks/>
        FirstLien,

        /// <remarks/>
        FourthLien,

        /// <remarks/>
        Other,

        /// <remarks/>
        SecondLien,

        /// <remarks/>
        ThirdLien,
    }

    public enum MortgageBase : byte
    {

        /// <remarks/>
        Conventional = 1,

        /// <remarks/>
        FHA = 2,

        /// <remarks/>
        LocalAgency = 101,

        /// <remarks/>
        Other = 5,

        /// <remarks/>
        PublicAndIndianHousing = 102,

        /// <remarks/>
        StateAgency = 103,

        /// <remarks/>
        USDARuralDevelopment = 4,

        /// <remarks/>
        VA = 3,
    }

    public enum ContactPointRoleBase
    {

        /// <remarks/>
        Home,

        /// <remarks/>
        Mobile,

        /// <remarks/>
        Other,

        /// <remarks/>
        Work,
    }

    public enum MaritalStatusBase : byte
    {

        /// <remarks/>
        Divorced = 4,

        /// <remarks/>
        Married = 1,

        /// <remarks/>
        NotProvided = 101,

        /// <remarks/>
        Other = 102,

        /// <remarks/>
        Separated = 2,

        /// <remarks/>
        Unknown = 103,

        /// <remarks/>
        Unmarried = 3,
    }

    public enum CitizenshipResidencyBase : byte
    {

        /// <remarks/>
        NonPermanentResidentAlien = 3,

        /// <remarks/>
        NonResidentAlien = 4,

        /// <remarks/>
        PermanentResidentAlien = 2,

        /// <remarks/>
        Unknown = 100,

        /// <remarks/>
        USCitizen = 1,
    }


    public enum PriorPropertyTitleBase
    {

        /// <remarks/>
        JointWithOtherThanSpouse = 4,

        /// <remarks/>
        JointWithSpouse = 2,

        /// <remarks/>
        Sole = 1,
    }

    public enum PriorPropertyUsageBase : byte
    {

        /// <remarks/>
        Investment = 2,

        /// <remarks/>
        PrimaryResidence = 1,

        /// <remarks/>
        SecondHome = 3,
    }

    public enum IncomeBase : byte
    {

        /// <remarks/>
        AccessoryUnitIincome = 22,

        /// <remarks/>
        Alimony = 1,

        /// <remarks/>
        AutomobileAllowance = 2,

        /// <remarks/>
        Base = 101,

        /// <remarks/>
        BoarderIncome = 3,

        /// <remarks/>
        Bonus = 2,

        /// <remarks/>
        BorrowerEstimatedTotalMonthlyIncome = 102,

        /// <remarks/>
        CapitalGains = 4,

        /// <remarks/>
        ChildSupport = 5,

        /// <remarks/>
        Commissions = 3,

        /// <remarks/>
        ContractBasis = 103,

        /// <remarks/>
        DefinedContributionPlan = 104,

        /// <remarks/>
        Disability = 6,

        /// <remarks/>
        DividendsInterest = 9,

        /// <remarks/>
        EmploymentRelatedAccount = 23,

        /// <remarks/>
        FosterCare = 7,

        /// <remarks/>
        HousingAllowance = 8,

        /// <remarks/>
        HousingChoiceVoucherProgram = 105,

        /// <remarks/>
        MilitaryBasePay = 24,

        /// <remarks/>
        MilitaryClothesAllowance = 106,

        /// <remarks/>
        MilitaryCombatPay = 107,

        /// <remarks/>
        MilitaryFlightPay = 108,

        /// <remarks/>
        MilitaryHazardPay = 109,

        /// <remarks/>
        MilitaryOverseasPay = 110,

        /// <remarks/>
        MilitaryPropPay = 111,

        /// <remarks/>
        MilitaryQuartersAllowance = 112,

        /// <remarks/>
        MilitaryRationsAllowance = 113,

        /// <remarks/>
        MilitaryVariableHousingAllowance = 114,

        /// <remarks/>
        MiscellaneousIncome = 115,

        /// <remarks/>
        MortgageCreditCertificate = 12,

        /// <remarks/>
        MortgageDifferential = 13,

        /// <remarks/>
        NetRentalIncome = 116,

        /// <remarks/>
        NonBorrowerContribution = 117,

        /// <remarks/>
        NonBorrowerHouseholdIncome = 118,

        /// <remarks/>
        NotesReceivableInstallment = 10,

        /// <remarks/>
        Other = 5,

        /// <remarks/>
        Overtime = 128,

        /// <remarks/>
        Pension = 120,

        /// <remarks/>
        ProposedGrossRentForSubjectProperty = 121,

        /// <remarks/>
        PublicAssistance = 11,

        /// <remarks/>
        RealEstateOwnedGrossRentalIncome = 122,

        /// <remarks/>
        Royalties = 15,

        /// <remarks/>
        SelfEmploymentIncome = 123,

        /// <remarks/>
        SelfEmploymentLoss = 124,

        /// <remarks/>
        SeparateMaintenance = 16,

        /// <remarks/>
        SocialSecurity = 17,

        /// <remarks/>
        SubjectPropertyNetCashFlow = 125,

        /// <remarks/>
        TemporaryLeave = 26,

        /// <remarks/>
        TipIncome = 27,

        /// <remarks/>
        TrailingCoBorrowerIncome = 126,

        /// <remarks/>
        Trust = 18,

        /// <remarks/>
        Unemployment = 19,

        /// <remarks/>
        VABenefitsNonEducational = 20,

        /// <remarks/>
        WorkersCompensation = 127,
    }

    public enum EmploymentClassificationBase
    {

        /// <remarks/>
        Primary,

        /// <remarks/>
        Secondary,
    }

    public enum EmploymentStatusBase
    {

        /// <remarks/>
        Current,

        /// <remarks/>
        Previous,
    }

    public enum OwnershipInterestBase
    {

        /// <remarks/>
        GreaterThanOrEqualTo25Percent,

        /// <remarks/>
        LessThan25Percent,

        /// <remarks/>
        Other,
    }

    public enum OwnedPropertyDispositionStatusBase : byte
    {

        /// <remarks/>
        PendingSale = 2,

        /// <remarks/>
        Retain = 3,

        /// <remarks/>
        Sold = 1,
    }

    public enum PropertyCurrentUsageBase : byte
    {

        /// <remarks/>
        Investment = 2,

        /// <remarks/>
        Other,

        /// <remarks/>
        PrimaryResidence = 1,

        /// <remarks/>
        SecondHome = 3,
    }

    public enum PartyRoleBase
    {

        /// <remarks/>
        Appraiser,

        /// <remarks/>
        AppraiserSupervisor,

        /// <remarks/>
        AssignFrom,

        /// <remarks/>
        AssignTo,

        /// <remarks/>
        Attorney,

        /// <remarks/>
        AttorneyInFact,

        /// <remarks/>
        AuthorizedRepresentative,

        /// <remarks/>
        AuthorizedThirdParty,

        /// <remarks/>
        BankruptcyFiler,

        /// <remarks/>
        BankruptcyTrustee,

        /// <remarks/>
        BeneficialInterestParty,

        /// <remarks/>
        BillToParty,

        /// <remarks/>
        Borrower,

        /// <remarks/>
        Builder,

        /// <remarks/>
        Client,

        /// <remarks/>
        ClosingAgent,

        /// <remarks/>
        Conservator,

        /// <remarks/>
        ConsumerReportingAgency,

        /// <remarks/>
        CooperativeCompany,

        /// <remarks/>
        CorrespondentLender,

        /// <remarks/>
        Cosigner,

        /// <remarks/>
        CreditCounselingAgent,

        /// <remarks/>
        CreditEnhancementRiskHolder,

        /// <remarks/>
        CustodianNotePayTo,

        /// <remarks/>
        Defendant,

        /// <remarks/>
        DeliverRescissionTo,

        /// <remarks/>
        DesignatedContact,

        /// <remarks/>
        DocumentCustodian,

        /// <remarks/>
        ENoteController,

        /// <remarks/>
        ENoteControllerTransferee,

        /// <remarks/>
        ENoteCustodian,

        /// <remarks/>
        ENoteCustodianTransferee,

        /// <remarks/>
        ENoteDelegateeForTransfers,

        /// <remarks/>
        ENoteRegisteringParty,

        /// <remarks/>
        ENoteServicer,

        /// <remarks/>
        ENoteServicerTransferee,

        /// <remarks/>
        ENoteTransferInitiator,

        /// <remarks/>
        Executor,

        /// <remarks/>
        FHASponsor,

        /// <remarks/>
        FloodCertificateProvider,

        /// <remarks/>
        FulfillmentParty,

        /// <remarks/>
        GiftDonor,

        /// <remarks/>
        Grantee,

        /// <remarks/>
        Grantor,

        /// <remarks/>
        HazardInsuranceAgent,

        /// <remarks/>
        HazardInsuranceCompany,

        /// <remarks/>
        HomeownersAssociation,

        /// <remarks/>
        HousingCounselingAgency,

        /// <remarks/>
        HousingCounselingAgent,

        /// <remarks/>
        HUD1SettlementAgent,

        /// <remarks/>
        Interviewer,

        /// <remarks/>
        InterviewerEmployer,

        /// <remarks/>
        Investor,

        /// <remarks/>
        IRSTaxFormThirdParty,

        /// <remarks/>
        LawFirm,

        /// <remarks/>
        Lender,

        /// <remarks/>
        LenderBranch,

        /// <remarks/>
        LienHolder,

        /// <remarks/>
        LoanCloser,

        /// <remarks/>
        LoanDeliveryFilePreparer,

        /// <remarks/>
        LoanFunder,

        /// <remarks/>
        LoanOfficer,

        /// <remarks/>
        LoanOriginationCompany,

        /// <remarks/>
        LoanOriginator,

        /// <remarks/>
        LoanProcessor,

        /// <remarks/>
        LoanSeller,

        /// <remarks/>
        LoanUnderwriter,

        /// <remarks/>
        LossPayee,

        /// <remarks/>
        ManagementCompany,

        /// <remarks/>
        MICompany,

        /// <remarks/>
        MortgageBroker,

        /// <remarks/>
        NonTitleNonSpouseOwnershipInterest,

        /// <remarks/>
        NonTitleSpouse,

        /// <remarks/>
        Notary,

        /// <remarks/>
        NotePayTo,

        /// <remarks/>
        NotePayToRecipient,

        /// <remarks/>
        Other,

        /// <remarks/>
        Payee,

        /// <remarks/>
        Plaintiff,

        /// <remarks/>
        PoolInsurer,

        /// <remarks/>
        PoolIssuer,

        /// <remarks/>
        PoolIssuerTransferee,

        /// <remarks/>
        PreparedBy,

        /// <remarks/>
        ProjectDeveloper,

        /// <remarks/>
        ProjectManagementAgent,

        /// <remarks/>
        PropertyAccessContact,

        /// <remarks/>
        PropertyJurisdictionalAuthority,

        /// <remarks/>
        PropertyOwner,

        /// <remarks/>
        PropertyPreservationAgent,

        /// <remarks/>
        PropertyPurchaser,

        /// <remarks/>
        PropertySeller,

        /// <remarks/>
        RealEstateAgent,

        /// <remarks/>
        ReceivingParty,

        /// <remarks/>
        RegistryOperator,

        /// <remarks/>
        RegulatoryAgency,

        /// <remarks/>
        RequestingParty,

        /// <remarks/>
        RespondingParty,

        /// <remarks/>
        RespondToParty,

        /// <remarks/>
        ReturnTo,

        /// <remarks/>
        ReviewAppraiser,

        /// <remarks/>
        SecurityIssuer,

        /// <remarks/>
        ServiceBureau,

        /// <remarks/>
        ServiceProvider,

        /// <remarks/>
        Servicer,

        /// <remarks/>
        ServicerPaymentCollection,

        /// <remarks/>
        Settlor,

        /// <remarks/>
        Spouse,

        /// <remarks/>
        SubmittingParty,

        /// <remarks/>
        TaxableParty,

        /// <remarks/>
        TaxAssessor,

        /// <remarks/>
        TaxCollector,

        /// <remarks/>
        Taxpayer,

        /// <remarks/>
        TaxServiceProvider,

        /// <remarks/>
        TaxServicer,

        /// <remarks/>
        ThirdPartyInvestor,

        /// <remarks/>
        ThirdPartyOriginator,

        /// <remarks/>
        TitleCompany,

        /// <remarks/>
        TitleHolder,

        /// <remarks/>
        TitleUnderwriter,

        /// <remarks/>
        Trust,

        /// <remarks/>
        TrustBeneficiary,

        /// <remarks/>
        Trustee,

        /// <remarks/>
        Unspecified,

        /// <remarks/>
        WarehouseLender,

        /// <remarks/>
        Witness,
    }

    public enum BorrowerResidencyBasisBase : byte
    {

        /// <remarks/>
        LivingRentFree = 101,

        /// <remarks/>
        Own = 1,

        /// <remarks/>
        Rent = 2,

        /// <remarks/>
        Unknown = 102,
    }

    public enum BorrowerResidencyBase
    {

        /// <remarks/>
        Current,

        /// <remarks/>
        Prior,
    }

    public enum TaxpayerIdentifierBase : byte
    {

        /// <remarks/>
        EmployerIdentificationNumber = 1,

        /// <remarks/>
        IndividualTaxpayerIdentificationNumber = 2,

        /// <remarks/>
        PreparerTaxpayerIdentificationNumber = 101,

        /// <remarks/>
        SocialSecurityNumber = 0,

        /// <remarks/>
        TaxpayerIdentificationNumberForPendingUSAdoptions,
    }

    public enum GenderBase : byte
    {

        /// <remarks/>
        Female = 1,

        /// <remarks/>
        InformationNotProvidedUnknown = 3,

        /// <remarks/>
        Male = 2,

        /// <remarks/>
        NotApplicable = 101,
    }

    public enum HMDAEthnicityBase : byte
    {

        /// <remarks/>
        HispanicOrLatino = 1,

        /// <remarks/>
        InformationNotProvidedByApplicantInMailInternetOrTelephoneApplication = 3,

        /// <remarks/>
        NotApplicable = 101,

        /// <remarks/>
        NotHispanicOrLatino = 2,
    }

    public enum HMDAEthnicityOriginBase : byte
    {

        /// <remarks/>
        Cuban = 3,

        /// <remarks/>
        Mexican = 1,

        /// <remarks/>
        Other = 4,

        /// <remarks/>
        PuertoRican = 2,
    }

    public enum HMDARaceDesignationBase : byte
    {

        /// <remarks/>
        AsianIndian = 1,

        /// <remarks/>
        Chinese = 2,

        /// <remarks/>
        Filipino = 3,

        /// <remarks/>
        GuamanianOrChamorro = 9,

        /// <remarks/>
        Japanese = 4,

        /// <remarks/>
        Korean = 5,

        /// <remarks/>
        NativeHawaiian = 8,

        /// <remarks/>
        Other = 11,

        /// <remarks/>
        Samoan = 10,

        /// <remarks/>
        Vietnamese = 6,

        // Manually Added
        OtherAsian = 7
    }

    public enum HousingExpenseTimingBase
    {

        /// <remarks/>
        Present,

        /// <remarks/>
        Proposed,
    }


    public enum AttachmentBase
    {

        /// <remarks/>
        Attached,

        /// <remarks/>
        Detached = 1,

        /// <remarks/>
        SemiDetached,
    }

    public enum ProjectLegalStructureBase
    {

        /// <remarks/>
        CommonInterestApartment,

        /// <remarks/>
        Condominium,

        /// <remarks/>
        Cooperative,

        /// <remarks/>
        Unknown,
    }

    public enum ConstructionMethodBase
    {

        /// <remarks/>
        Manufactured,

        /// <remarks/>
        MobileHome,

        /// <remarks/>
        Modular,

        /// <remarks/>
        OnFrameModular,

        /// <remarks/>
        Other,

        /// <remarks/>
        SiteBuilt,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public enum HousingExpenseBase
    {

        /// <remarks/>
        Cable,

        /// <remarks/>
        Electricity,

        /// <remarks/>
        EscrowShortage,

        /// <remarks/>
        FirstMortgagePITI,

        /// <remarks/>
        FirstMortgagePrincipalAndInterest,

        /// <remarks/>
        FloodInsurance,

        /// <remarks/>
        GroundRent,

        /// <remarks/>
        Heating,

        /// <remarks/>
        HomeownersAssociationDuesAndCondominiumFees,

        /// <remarks/>
        HomeownersInsurance,

        /// <remarks/>
        LeaseholdPayments,

        /// <remarks/>
        MaintenanceAndMiscellaneous,

        /// <remarks/>
        MIPremium,

        /// <remarks/>
        Other,

        /// <remarks/>
        OtherMortgageLoanPrincipalAndInterest,

        /// <remarks/>
        OtherMortgageLoanPrincipalInterestTaxesAndInsurance,

        /// <remarks/>
        RealEstateTax,

        /// <remarks/>
        Rent,

        /// <remarks/>
        SupplementalPropertyInsurance,

        /// <remarks/>
        Telephone,

        /// <remarks/>
        Utilities,
    }

}