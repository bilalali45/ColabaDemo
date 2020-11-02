using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using ByteWebConnector.Model.Enums.Rainmaker;
using ByteWebConnector.Model.Models.ActionModels.LoanFile;
using Extensions.ExtensionClasses;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ByteWebConnector.Model.Models
{

    #region POCO classes

    public class ByteFile
    {
        public int FileDataID { get; set; }
        public FileData FileData { get; set; }
        public Status Status { get; set; }
        public List<Application> Applications { get; set; }
        public SubProp SubProp { get; set; }

        public Loan Loan { get; set; }

        //public List<ClosingCost> ClosingCosts { get; set; }
        public List<PrepaidItem> PrepaidItems { get; set; }

        //public List<object> HELOCPeriods { get; set; }
        //public List<object> DocPackages { get; set; }
        // public DOT DOT { get; set; }
        // public Fannie Fannie { get; set; }
        // public Freddie Freddie { get; set; }
        public List<Party> Parties { get; set; }

        //public PartyMisc PartyMisc { get; set; }
        //public List<object> Tasks { get; set; }
        //public List<object> NeededItems { get; set; }
        //public List<object> Conditions { get; set; }
        public List<CustomValue> CustomValues { get; set; }

        //public List<object> ExtendedTextValues { get; set; }
        // public List<object> ExtendedDateValues { get; set; }
        //public List<object> ExtendedBooleanValues { get; set; }
        //public List<object> ExtendedDecimalValues { get; set; }
        // public CreditDenial CreditDenial { get; set; }
        // public CA883 CA883 { get; set; }
        // public List<IRSForm4506s> IRSForm4506s { get; set; }
        // public IRSForm1098 IRSForm1098 { get; set; }
        // public Transmittal Transmittal { get; set; }
        //  public HMDA HMDA { get; set; }
        // public FHA FHA { get; set; }
        //   public FHA203K FHA203K { get; set; }
        //  public FHAForms FHAForms { get; set; }
        //  public FHAMCAW FHAMCAW { get; set; }
        // public VA VA { get; set; }
        // public VALoanSummary VALoanSummary { get; set; }
        // public VAValue VAValue { get; set; }
        //public List<object> SelfEmpIncs { get; set; }
        //public List<object> SelfEmpIncYears { get; set; }
        // public SalesTools SalesTools { get; set; }
        // public MiscForms MiscForms { get; set; }
        // public List<object> TrustAccountItems { get; set; }
        //  public TXForms TXForms { get; set; }
        public CustomField CustomField { get; set; }
        // public HUD1 HUD1 { get; set; }
        //public List<object> NYAppLogFees { get; set; }
        //public List<object> FieldNotes { get; set; }
        //public List<object> OSOResults { get; set; }
        //public List<object> PriceAdjustments { get; set; }
        //public List<object> VAServiceDatas { get; set; }
        //public List<object> VAPreviousLoans { get; set; }
        // public HAMP HAMP { get; set; }
        //public List<object> VAAuthorizedAgents { get; set; }
        //public List<object> ShoppableProviders { get; set; }
        //public List<object> LockHistories { get; set; }
        // public Secondary Secondary { get; set; }
        // public Closing Closing { get; set; }
        //public List<object> LoanPayments { get; set; }
        //public List<object> RelatedLoans { get; set; }
        //public List<object> Markups { get; set; }
        //public List<object> Snapshots { get; set; }
        //public List<object> AdditionalParties { get; set; }
        //public List<object> ClosingProrations { get; set; }
        //public List<object> ClosingAdjustments { get; set; }
        //public List<object> ClosingPayoffs { get; set; }
        //public List<object> DisclosureLogEntries { get; set; }
        //public List<object> DocSigners { get; set; }
        //public List<object> ServiceOrders { get; set; }
    }

    public class ByteNewFile
    {
        public string OrganizationCode { get; set; }
        public string SubPropState { get; set; }
        public string LoanOfficerUserName { get; set; }
        public string BorrowerFirstName { get; set; }
        public string BorrowerLastName { get; set; }
        public string BorrowerMiddleName { get; set; }
        public string BorrowerSuffix { get; set; }
        public string CoBorrowerFirstName { get; set; }
        public string CoBorrowerLastName { get; set; }
        public string CoBorrowerMiddleName { get; set; }
        public string CoBorrowerSuffix { get; set; }


        public static ByteNewFile Create(LoanApplication loanApplication
                                         )
        {

            var byteNewFile = new ByteNewFile();

            byteNewFile.OrganizationCode = loanApplication.GetByteOrganizationCode(); //CORP
            byteNewFile.SubPropState = loanApplication.PropertyInfo.AddressInfo.State?.Abbreviation ?? "";
            byteNewFile.LoanOfficerUserName = loanApplication.Opportunity?.Owner?.UserProfile?.UserName ?? "";
            byteNewFile.BorrowerFirstName = "";
            byteNewFile.BorrowerLastName = "";
            byteNewFile.BorrowerMiddleName = "";
            byteNewFile.BorrowerSuffix = "";
            byteNewFile.CoBorrowerFirstName = "";
            byteNewFile.CoBorrowerLastName = "";
            byteNewFile.CoBorrowerMiddleName = "";
            byteNewFile.CoBorrowerSuffix = "";

            return byteNewFile;

        }
        public ByteNewFile()
        { }

            public ByteNewFile(LoanApplication loanApplication)
        {
          

                

               this.OrganizationCode = loanApplication.GetByteOrganizationCode(); //CORP
               this.SubPropState = loanApplication.PropertyInfo.AddressInfo.State?.Abbreviation ?? "";
               this.LoanOfficerUserName = loanApplication.Opportunity?.Owner?.UserProfile?.UserName ?? "";
               this.BorrowerFirstName = "";
               this.BorrowerLastName = "";
               this.BorrowerMiddleName = "";
               this.BorrowerSuffix = "";
               this.CoBorrowerFirstName = "";
               this.CoBorrowerLastName = "";
               this.CoBorrowerMiddleName = "";
               this.CoBorrowerSuffix = "";

            

             
        }

    }




    // ABADDefaultItem

    public class AbadDefaultItem
    {
        public int AbadDefaultItemId { get; set; } // ABADDefaultItemID (Primary key)
        public int AbadSetId { get; set; } // ABADSetID
        public int DisplayOrder { get; set; } // DisplayOrder
        public string ProviderAndService { get; set; } // ProviderAndService (length: 200)
        public string Charges { get; set; } // Charges (length: 200)
        public bool RequiredUse { get; set; } // RequiredUse


        public AbadDefaultItem()
        {
            AbadSetId = 0;
            DisplayOrder = 0;
            RequiredUse = false;
        }
    }

    // ABADSet

    public class AbadSet
    {
        public int AbadSetId { get; set; } // ABADSetID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Name { get; set; } // Name (length: 50)
        public int AbadOption { get; set; } // ABADOption
        public string RelationshipWith { get; set; } // RelationshipWith (length: 500)
        public string NatureOfRelationship { get; set; } // NatureOfRelationship (length: 500)


        public AbadSet()
        {
            DisplayOrder = 0;
            AbadOption = 0;
        }
    }

    // AdditionalParty

    public class AdditionalParty
    {
        public int AdditionalPartyId { get; set; } // AdditionalPartyID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int DisplayOrder { get; set; } // DisplayOrder
        public int AdditionalPartyType { get; set; } // AdditionalPartyType
        public string FirstName { get; set; } // FirstName (length: 50)
        public string MiddleName { get; set; } // MiddleName (length: 50)
        public string LastName { get; set; } // LastName (length: 50)
        public bool IsNonPersonEntity { get; set; } // IsNonPersonEntity
        public string NonPersonEntityName { get; set; } // NonPersonEntityName (length: 100)
        public string PoaFirstName { get; set; } // POAFirstName (length: 50)
        public string PoaMiddleName { get; set; } // POAMiddleName (length: 50)
        public string PoaLastName { get; set; } // POALastName (length: 50)
        public string PoaSigningCapacity { get; set; } // POASigningCapacity (length: 100)
        public string Generation { get; set; } // Generation (length: 10)
        public string PoaGeneration { get; set; } // POAGeneration (length: 10)
        public string NonPersonEntitySigner { get; set; } // NonPersonEntitySigner (length: 100)
        public string CreditAliases { get; set; } // CreditAliases (length: 500)
        public short TrustNo { get; set; } // TrustNo
        public System.Guid? SyncGuid { get; set; } // SyncGuid
        public string Street { get; set; } // Street (length: 50)
        public string City { get; set; } // City (length: 50)
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 9)
        public string Ssn { get; set; } // SSN (length: 50)
        public System.DateTime? Dob { get; set; } // DOB
        public string WorkPhone { get; set; } // WorkPhone (length: 20)
        public string HomePhone { get; set; } // HomePhone (length: 50)
        public string MobilePhone { get; set; } // MobilePhone (length: 20)
        public string Fax { get; set; } // Fax (length: 50)
        public string Email { get; set; } // Email (length: 50)
        public string MobilePhoneSmsGateway { get; set; } // MobilePhoneSMSGateway (length: 40)
        public int PrintSigLineOnClosingDisclosureOv { get; set; } // PrintSigLineOnClosingDisclosureOV
        public short CdSignatureMethod { get; set; } // CDSignatureMethod
        public string CdSignatureMethodOtherDesc { get; set; } // CDSignatureMethodOtherDesc (length: 35)


        public AdditionalParty()
        {
            FileDataId = 0;
            DisplayOrder = 0;
            AdditionalPartyType = 0;
            IsNonPersonEntity = false;
            TrustNo = 0;
            PrintSigLineOnClosingDisclosureOv = 0;
            CdSignatureMethod = 0;
        }
    }

    // AdjustmentTemplate

    public class AdjustmentTemplate
    {
        public int AdjustmentTemplateId { get; set; } // AdjustmentTemplateID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Description { get; set; } // Description (length: 100)
        public decimal? PricePercent { get; set; } // PricePercent
        public decimal? IntRatePercent { get; set; } // IntRatePercent
        public decimal? FeeAmount { get; set; } // FeeAmount
        public decimal? MarginPercent { get; set; } // MarginPercent
        public int PriceAdjustmentType { get; set; } // PriceAdjustmentType


        public AdjustmentTemplate()
        {
            DisplayOrder = 0;
            PriceAdjustmentType = 0;
        }
    }

    // The table 'AmortizationType' is not usable by entity framework because it
    // does not have a primary key. It is listed here for completeness.
    // AmortizationType

    public class AmortizationType
    {
        public int? AmortizationTypeId { get; set; } // AmortizationTypeId
        public string AmortizationTypeDesc { get; set; } // AmortizationTypeDesc (length: 160)
    }

    // AnnotationDefault

    public class AnnotationDefault
    {
        public int AnnotationDefaultId { get; set; } // AnnotationDefaultID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Code { get; set; } // Code (length: 20)
        public string Name { get; set; } // Name (length: 300)
        public int Type { get; set; } // Type
        public int FontColorArgb { get; set; } // FontColorARGB
        public string FontName { get; set; } // FontName (length: 50)
        public int FontStyle { get; set; } // FontStyle
        public decimal FontSize { get; set; } // FontSize
        public int StampType { get; set; } // StampType
        public int BorderColorArgb { get; set; } // BorderColorARGB
        public int BackgroundColorArgb { get; set; } // BackgroundColorARGB
        public bool BurnedOnSave { get; set; } // BurnedOnSave
        public long VisibleRoles { get; set; } // VisibleRoles
        public long EditableRoles { get; set; } // EditableRoles
        public bool OnlyCreatorCanModify { get; set; } // OnlyCreatorCanModify
        public bool ApplyChangesToAll { get; set; } // ApplyChangesToAll
        public bool Disabled { get; set; } // Disabled
        public string StampText { get; set; } // StampText (length: 50)


        public AnnotationDefault()
        {
            DisplayOrder = 0;
            Type = 0;
            FontColorArgb = 0;
            FontStyle = 0;
            FontSize = 0m;
            StampType = 0;
            BorderColorArgb = 0;
            BackgroundColorArgb = 0;
            BurnedOnSave = false;
            VisibleRoles = 0;
            EditableRoles = 0;
            OnlyCreatorCanModify = false;
            ApplyChangesToAll = false;
            Disabled = false;
        }
    }

    // APORRate

    public class AporRate
    {
        public int AporRateId { get; set; } // APORRateID (Primary key)
        public System.DateTime? AporDate { get; set; } // APORDate
        public short AporAmortType { get; set; } // APORAmortType
        public decimal? Rate1 { get; set; } // Rate1
        public decimal? Rate2 { get; set; } // Rate2
        public decimal? Rate3 { get; set; } // Rate3
        public decimal? Rate4 { get; set; } // Rate4
        public decimal? Rate5 { get; set; } // Rate5
        public decimal? Rate6 { get; set; } // Rate6
        public decimal? Rate7 { get; set; } // Rate7
        public decimal? Rate8 { get; set; } // Rate8
        public decimal? Rate9 { get; set; } // Rate9
        public decimal? Rate10 { get; set; } // Rate10
        public decimal? Rate11 { get; set; } // Rate11
        public decimal? Rate12 { get; set; } // Rate12
        public decimal? Rate13 { get; set; } // Rate13
        public decimal? Rate14 { get; set; } // Rate14
        public decimal? Rate15 { get; set; } // Rate15
        public decimal? Rate16 { get; set; } // Rate16
        public decimal? Rate17 { get; set; } // Rate17
        public decimal? Rate18 { get; set; } // Rate18
        public decimal? Rate19 { get; set; } // Rate19
        public decimal? Rate20 { get; set; } // Rate20
        public decimal? Rate21 { get; set; } // Rate21
        public decimal? Rate22 { get; set; } // Rate22
        public decimal? Rate23 { get; set; } // Rate23
        public decimal? Rate24 { get; set; } // Rate24
        public decimal? Rate25 { get; set; } // Rate25
        public decimal? Rate26 { get; set; } // Rate26
        public decimal? Rate27 { get; set; } // Rate27
        public decimal? Rate28 { get; set; } // Rate28
        public decimal? Rate29 { get; set; } // Rate29
        public decimal? Rate30 { get; set; } // Rate30
        public decimal? Rate31 { get; set; } // Rate31
        public decimal? Rate32 { get; set; } // Rate32
        public decimal? Rate33 { get; set; } // Rate33
        public decimal? Rate34 { get; set; } // Rate34
        public decimal? Rate35 { get; set; } // Rate35
        public decimal? Rate36 { get; set; } // Rate36
        public decimal? Rate37 { get; set; } // Rate37
        public decimal? Rate38 { get; set; } // Rate38
        public decimal? Rate39 { get; set; } // Rate39
        public decimal? Rate40 { get; set; } // Rate40
        public decimal? Rate41 { get; set; } // Rate41
        public decimal? Rate42 { get; set; } // Rate42
        public decimal? Rate43 { get; set; } // Rate43
        public decimal? Rate44 { get; set; } // Rate44
        public decimal? Rate45 { get; set; } // Rate45
        public decimal? Rate46 { get; set; } // Rate46
        public decimal? Rate47 { get; set; } // Rate47
        public decimal? Rate48 { get; set; } // Rate48
        public decimal? Rate49 { get; set; } // Rate49
        public decimal? Rate50 { get; set; } // Rate50


        public AporRate()
        {
            AporAmortType = 0;
        }
    }

    // Application
    
    public class Application
    {
        public ByteBorrower Borrower { get; set; }

        public ByteBorrower CoBorrower { get; set; }
        
        public int ApplicationId { get; set; } // ApplicationID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int DisplayOrder { get; set; } // DisplayOrder
        public int? BorrowerId { get; set; } // BorrowerID
        public int? CoBorrowerId { get; set; } // CoBorrowerID
        public short ApplicationMethod { get; set; } // ApplicationMethod
        public bool OtherIncome { get; set; } // OtherIncome
        public bool IncomeSpouse { get; set; } // IncomeSpouse
        public string CreditRefNo { get; set; } // CreditRefNo (length: 50)
        public string AutoDesc1 { get; set; } // AutoDesc1 (length: 50)
        public decimal? AutoValue1 { get; set; } // AutoValue1
        public string AutoDesc2 { get; set; } // AutoDesc2 (length: 50)
        public decimal? AutoValue2 { get; set; } // AutoValue2
        public string AutoDesc3 { get; set; } // AutoDesc3 (length: 50)
        public decimal? AutoValue3 { get; set; } // AutoValue3
        public short OtherExpenseType { get; set; } // OtherExpenseType
        public string OtherExpenseOwedTo { get; set; } // OtherExpenseOwedTo (length: 50)
        public decimal? OtherExpenseAmount { get; set; } // OtherExpenseAmount
        public string JobExpenseDesc1 { get; set; } // JobExpenseDesc1 (length: 50)
        public decimal? JobExpenseAmount1 { get; set; } // JobExpenseAmount1
        public string JobExpenseDesc2 { get; set; } // JobExpenseDesc2 (length: 50)
        public decimal? JobExpenseAmount2 { get; set; } // JobExpenseAmount2
        public string OtherAssetDesc1 { get; set; } // OtherAssetDesc1 (length: 50)
        public decimal? OtherAssetValue1 { get; set; } // OtherAssetValue1
        public string OtherAssetDesc2 { get; set; } // OtherAssetDesc2 (length: 50)
        public decimal? OtherAssetValue2 { get; set; } // OtherAssetValue2
        public string OtherAssetDesc3 { get; set; } // OtherAssetDesc3 (length: 50)
        public decimal? OtherAssetValue3 { get; set; } // OtherAssetValue3
        public string OtherAssetDesc4 { get; set; } // OtherAssetDesc4 (length: 50)
        public decimal? OtherAssetValue4 { get; set; } // OtherAssetValue4
        public decimal? NetWorthOfBusiness { get; set; } // NetWorthOfBusiness
        public decimal? RetirementFunds { get; set; } // RetirementFunds
        public decimal? LifeInsFaceValue { get; set; } // LifeInsFaceValue
        public decimal? LifeInsCashValue { get; set; } // LifeInsCashValue
        public string StockBondDesc1 { get; set; } // StockBondDesc1 (length: 50)
        public decimal? StockBondValue1 { get; set; } // StockBondValue1
        public short StockBondType1 { get; set; } // StockBondType1
        public string StockBondDesc2 { get; set; } // StockBondDesc2 (length: 50)
        public decimal? StockBondValue2 { get; set; } // StockBondValue2
        public short StockBondType2 { get; set; } // StockBondType2
        public string StockBondDesc3 { get; set; } // StockBondDesc3 (length: 50)
        public decimal? StockBondValue3 { get; set; } // StockBondValue3
        public short StockBondType3 { get; set; } // StockBondType3
        public short StatementsCompleted { get; set; } // StatementsCompleted
        public decimal? IncomeBaseOv { get; set; } // IncomeBaseOV
        public decimal? IncomeOvertimeOv { get; set; } // IncomeOvertimeOV
        public decimal? IncomeBonusOv { get; set; } // IncomeBonusOV
        public decimal? IncomeCommissionOv { get; set; } // IncomeCommissionOV
        public decimal? IncomeDivIntOv { get; set; } // IncomeDivIntOV
        public decimal? IncomeNetRentalOv { get; set; } // IncomeNetRentalOV
        public decimal? IncomeOther1Ov { get; set; } // IncomeOther1OV
        public decimal? IncomeOther2Ov { get; set; } // IncomeOther2OV
        public decimal? IncomeTotalOv { get; set; } // IncomeTotalOV
        public decimal? PresentRent { get; set; } // PresentRent
        public decimal? PresentFirstMortgage { get; set; } // PresentFirstMortgage
        public decimal? PresentOtherFiPi { get; set; } // PresentOtherFiPI
        public decimal? PresentHazardIns { get; set; } // PresentHazardIns
        public decimal? PresentTaxes { get; set; } // PresentTaxes
        public decimal? PresentMi { get; set; } // PresentMI
        public decimal? PresentHod { get; set; } // PresentHOD
        public decimal? PresentOtherHousingExp { get; set; } // PresentOtherHousingExp
        public bool CashDepositIncludeInNetWorth { get; set; } // CashDepositIncludeInNetWorth
        public decimal? NetReoPaymentsOv { get; set; } // NetREOPaymentsOV
        public short EquifaxIndicator { get; set; } // EquifaxIndicator
        public short ExperianIndicator { get; set; } // ExperianIndicator
        public short TransUnionIndicator { get; set; } // TransUnionIndicator
        public string Page4Memo { get; set; } // Page4Memo (length: 5000)
        public decimal? IncomeBase { get; set; } // _IncomeBase
        public decimal? IncomeOvertime { get; set; } // _IncomeOvertime
        public decimal? IncomeBonus { get; set; } // _IncomeBonus
        public decimal? IncomeCommission { get; set; } // _IncomeCommission
        public decimal? IncomeDivInt { get; set; } // _IncomeDivInt
        public decimal? IncomeOther1 { get; set; } // _IncomeOther1
        public decimal? IncomeOther2 { get; set; } // _IncomeOther2
        public decimal? IncomeNetRental { get; set; } // _IncomeNetRental
        public decimal? IncomeTotal { get; set; } // _IncomeTotal
        public decimal? PresentPiti { get; set; } // _PresentPITI
        public decimal? ReoMarketValue { get; set; } // _REOMarketValue
        public decimal? LiquidAssetsLessCashDeposit { get; set; } // _LiquidAssetsLessCashDeposit
        public decimal? LiquidAssets { get; set; } // _LiquidAssets
        public decimal? TotalAssets { get; set; } // _TotalAssets
        public decimal? TotalDebtPayments { get; set; } // _TotalDebtPayments
        public decimal? TotalDebtBalance { get; set; } // _TotalDebtBalance
        public decimal? NetWorth { get; set; } // _NetWorth
        public decimal? NetReoPayments { get; set; } // _NetREOPayments
        public decimal? AutoExpense { get; set; } // AutoExpense
        public decimal? HealthInsuranceExpense { get; set; } // HealthInsuranceExpense
        public decimal? MedicalExpense { get; set; } // MedicalExpense
        public decimal? FoodExpense { get; set; } // FoodExpense
        public decimal? UtilitiesExpense { get; set; } // UtilitiesExpense
        public decimal? PropertyMaintenanceExpense { get; set; } // PropertyMaintenanceExpense
        public decimal? LifeInsuranceExpense { get; set; } // LifeInsuranceExpense
        public decimal? DependentCareExpense { get; set; } // DependentCareExpense
        public string OtherExpenseQmatrNotes { get; set; } // OtherExpenseQMATRNotes (length: 2147483647)
        public string JobExpense1QmatrNotes { get; set; } // JobExpense1QMATRNotes (length: 2147483647)
        public string JobExpense2QmatrNotes { get; set; } // JobExpense2QMATRNotes (length: 2147483647)
        public System.Guid? SyncGuid { get; set; } // SyncGuid
        public decimal? PresentSuppPropIns { get; set; } // PresentSuppPropIns


        public Application()
        {
            FileDataId = 0;
            DisplayOrder = 0;
            ApplicationMethod = 0;
            OtherIncome = false;
            IncomeSpouse = false;
            OtherExpenseType = 0;
            StockBondType1 = 0;
            StockBondType2 = 0;
            StockBondType3 = 0;
            StatementsCompleted = 0;
            CashDepositIncludeInNetWorth = false;
            EquifaxIndicator = 0;
            ExperianIndicator = 0;
            TransUnionIndicator = 0;
        }





        public void Update(
                                  decimal? lifeInsuranceEstimatedMonthlyAmount,
                                  decimal? propertyInfoHoaDues)
        {

            this.LifeInsCashValue = lifeInsuranceEstimatedMonthlyAmount;
            this.ApplicationMethod = (short)4;
            //Home Due Amount
            this.PresentHod = propertyInfoHoaDues/12;





        } 
        
    }

    
    public class ByteBorrower
    {
         public int FileDataID { get; set; }

         public int? BorrowerID { get; set; }

        public Borrower Borrower { get; set; }

         public List<Residence> Residences { get; set; }

         public List<Employer> Employers { get; set; }

         public List<Income> Incomes { get; set; }

         public List<Asset> Assets { get; set; }

         public List<Reo> REOs { get; set; }

         public List<Debt> Debts { get; set; }


        //
        //public List<object> CreditAliases { get; set; }
        //
        //public List<object> Expenses { get; set; }
        //
        //public List<object> Gifts { get; set; }
        public static ByteBorrower Create(ActionModels.LoanFile.Borrower rmBorrower,
                                          ThirdPartyCodeList thirdPartyCodeList,
                                          int byteFileDataId)
        {
            var byteBorrower = new ByteBorrower();

            byteBorrower.Borrower = Borrower.Create(rmBorrower, thirdPartyCodeList, byteFileDataId);
            byteBorrower.BorrowerID = 0;
            byteBorrower.FileDataID = byteFileDataId;
            byteBorrower.Residences = new List<Residence>();
            //byteBorrower.Residences.Add(new Residence());
            //byteBorrower.Residences[0].FileDataId = byteFileDataId;
            //byteBorrower.Residences[0].Current = true;
            

            return byteBorrower;
        }





        public static void Update(ByteBorrower byteBorrower,
                                   ActionModels.LoanFile.Borrower rmBorrower,
                                  ThirdPartyCodeList thirdPartyCodeList)
        {

            Borrower.Update(byteBorrower.Borrower, rmBorrower, thirdPartyCodeList);
        }
    }
    // ARMIndex

    public class ArmIndex
    {
        public int ArmIndexId { get; set; } // ARMIndexID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public int ArmIndexType { get; set; } // ARMIndexType
        public decimal ArmIndexValue { get; set; } // ARMIndexValue
        public System.DateTime? ArmIndexForWeekEnding { get; set; } // ARMIndexForWeekEnding


        public ArmIndex()
        {
            DisplayOrder = 0;
            ArmIndexType = 0;
            ArmIndexValue = 0m;
        }
    }

    // Asset

    public class Asset
    {
        public int AssetId { get; set; } // AssetID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int? BorrowerId { get; set; } // BorrowerID
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Name { get; set; } // Name (length: 50)
        public string Attn { get; set; } // Attn (length: 50)
        public string Street { get; set; } // Street (length: 50)
        public string City { get; set; } // City (length: 50)
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 9)
        public short AccountType1 { get; set; } // AccountType1
        public string AccountNo1 { get; set; } // AccountNo1 (length: 50)
        public decimal? AccountBalance1 { get; set; } // AccountBalance1
        public short AccountType2 { get; set; } // AccountType2
        public string AccountNo2 { get; set; } // AccountNo2 (length: 50)
        public decimal? AccountBalance2 { get; set; } // AccountBalance2
        public short AccountType3 { get; set; } // AccountType3
        public string AccountNo3 { get; set; } // AccountNo3 (length: 50)
        public decimal? AccountBalance3 { get; set; } // AccountBalance3
        public short AccountType4 { get; set; } // AccountType4
        public string AccountNo4 { get; set; } // AccountNo4 (length: 50)
        public decimal? AccountBalance4 { get; set; } // AccountBalance4
        public string Notes { get; set; } // Notes (length: 500)
        public string Fax { get; set; } // Fax (length: 50)
        public System.Guid? SyncGuid { get; set; } // SyncGuid
        public string AccountOtherDesc { get; set; } // AccountOtherDesc (length: 80)
        public int AccountHeldByType { get; set; } // AccountHeldByType


        public Asset()
        {
            FileDataId = 0;
            DisplayOrder = 0;
            AccountType1 = 0;
            AccountType2 = 0;
            AccountType3 = 0;
            AccountType4 = 0;
            AccountHeldByType = 0;
        }


        public static List<Asset> Create(ActionModels.LoanFile.Borrower rmBorrower,
                                         ThirdPartyCodeList thirdPartyCodeList,
                                         Application application,
                                         int fileDataId)
        {
            var assets = new List<Asset>();
            decimal sumOfAccount = 0;

            foreach (var borrowerAccountBinder in rmBorrower.BorrowerAccountBinders)
            {
                var borrowerAccount = borrowerAccountBinder.BorrowerAccount;

                //var assetType = GetByteProValue("AccountType", borrowerAccountBinder.AccountTypeId);
                var assetType = thirdPartyCodeList.GetByteProValue("AccountType",
                                                                   borrowerAccount.AccountTypeId);
                int assetIndex = assetType.FindEnumIndex(typeof(AccountTypeEnum));


                string accountTitle = accountTitle = $"{rmBorrower.LoanContact.Suffix} {rmBorrower.LoanContact.FirstName} {rmBorrower.LoanContact.LastName} ";





                //if (!attachedBorrowerAccounts.Any(accountId => accountId == borrowerAccountBinder.Id))
                //{
                //attachedBorrowerAccounts.Add(borrowerAccountBinder.Id);
                if (assetIndex == (int)AccountTypeEnum.NetEquity)
                {
                    application.StockBondDesc1 = "Net Equity";
                    application.StockBondValue1 = borrowerAccount.Balance ?? null;
                }
                else
                {
                    assets.Add(new Asset
                    {
                        FileDataId = fileDataId,
                        AccountType1 = (short)assetIndex,
                        Name = borrowerAccount.Name,
                        AccountNo1 = borrowerAccount.AccountNumber,
                        AccountBalance1 = borrowerAccount.Balance ?? null,

                    });
                }

                //}


            }

            return assets;
        }
    }

    // AuditLogDataMod

    public class AuditLogDataMod
    {
        public long AuditLogDataModId { get; set; } // AuditLogDataModID (Primary key)
        public int AuditLogEventId { get; set; } // AuditLogEventID
        public short ModType { get; set; } // ModType
        public string TableName { get; set; } // TableName (length: 50)
        public string FieldName { get; set; } // FieldName (length: 50)
        public int PrimaryKeyId { get; set; } // PrimaryKeyID
        public string NewValue { get; set; } // NewValue (length: 7000)
    }

    // AuditLogEvent

    public class AuditLogEvent
    {
        public int AuditLogEventId { get; set; } // AuditLogEventID (Primary key)
        public System.DateTime EventDate { get; set; } // EventDate
        public string UserName { get; set; } // UserName (length: 50)
        public short AuditLogEventType { get; set; } // AuditLogEventType
        public string SourceFileName { get; set; } // SourceFileName (length: 50)
        public string TargetFileName { get; set; } // TargetFileName (length: 300)
        public int? DataStoreType { get; set; } // DataStoreType
        public int? FileDataId { get; set; } // FileDataID


        public AuditLogEvent()
        {
            EventDate = System.DateTime.Now;
        }
    }

    // AUSRun

    public class AusRun
    {
        public int AusRunId { get; set; } // AUSRunID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public string UserName { get; set; } // UserName (length: 50)
        public int AusSystem { get; set; } // AUSSystem
        public string AusSystemOther { get; set; } // AUSSystemOther (length: 255)
        public int AusResult { get; set; } // AUSResult
        public string AusResultOther { get; set; } // AUSResultOther (length: 255)
        public System.DateTime? DateRun { get; set; } // DateRun
        public int MortgageType { get; set; } // MortgageType


        public AusRun()
        {
            FileDataId = 0;
            AusSystem = 0;
            AusResult = 0;
            MortgageType = 0;
        }
    }

    // AutoLoginToken

    public class AutoLoginToken
    {
        public string Token { get; set; } // Token (Primary key) (length: 88)
        public string UserName { get; set; } // UserName (length: 50)
        public System.DateTime DateRequested { get; set; } // DateRequested


        public AutoLoginToken()
        {
            DateRequested = System.DateTime.Now;
        }
    }

    // BarcodePattern

    public class BarcodePattern
    {
        public int BarcodePatternId { get; set; } // BarcodePatternID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Name { get; set; } // Name (length: 20)
        public int BarcodeType { get; set; } // BarcodeType
        public string Delimiter { get; set; } // Delimiter (length: 2)
        public string Prefix { get; set; } // Prefix (length: 10)
        public int SearchAreaAlignment { get; set; } // SearchAreaAlignment
        public decimal SearchAreaWidth { get; set; } // SearchAreaWidth
        public decimal SearchAreaHeight { get; set; } // SearchAreaHeight
        public int Field1Type { get; set; } // Field1Type
        public int Field2Type { get; set; } // Field2Type
        public int Field3Type { get; set; } // Field3Type
        public int Field4Type { get; set; } // Field4Type
        public int Field5Type { get; set; } // Field5Type
        public int Field6Type { get; set; } // Field6Type
        public int BarcodePatternUsage { get; set; } // BarcodePatternUsage
        public string Suffix { get; set; } // Suffix (length: 10)
        public string CustomField { get; set; } // CustomField (length: 100)


        public BarcodePattern()
        {
            DisplayOrder = 0;
            BarcodeType = 0;
            SearchAreaAlignment = 0;
            SearchAreaWidth = 0m;
            SearchAreaHeight = 0m;
            Field1Type = 0;
            Field2Type = 0;
            Field3Type = 0;
            Field4Type = 0;
            Field5Type = 0;
            Field6Type = 0;
            BarcodePatternUsage = 0;
        }
    }

    // Borrower

    public class Borrower
    {
        public int BorrowerId { get; set; } // BorrowerID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public string FirstName { get; set; } // FirstName (length: 50)
        public string MiddleName { get; set; } // MiddleName (length: 50)
        public string LastName { get; set; } // LastName (length: 50)
        public string Generation { get; set; } // Generation (length: 10)
        public string NickName { get; set; } // NickName (length: 50)
        public string Ssn { get; set; } // SSN (length: 50)
        public string HomePhone { get; set; } // HomePhone (length: 50)
        public string MobilePhone { get; set; } // MobilePhone (length: 20)
        public string Fax { get; set; } // Fax (length: 50)
        public int? Age { get; set; } // Age
        public System.DateTime? Dob { get; set; } // DOB
        public short Ethnicity { get; set; } // Ethnicity
        public bool GovDoNotWishToFurnish { get; set; } // GovDoNotWishToFurnish
        public bool RaceNotApplicable { get; set; } // RaceNotApplicable
        public bool RaceNotProvided { get; set; } // RaceNotProvided
        public bool RaceAmericanIndian { get; set; } // RaceAmericanIndian
        public bool RaceAsian { get; set; } // RaceAsian
        public bool RaceBlack { get; set; } // RaceBlack
        public bool RacePacificIslander { get; set; } // RacePacificIslander
        public bool RaceWhite { get; set; } // RaceWhite
        public short Gender { get; set; } // Gender
        public int? YearsSchool { get; set; } // YearsSchool
        public short MaritalStatus { get; set; } // MaritalStatus
        public int? NoDeps { get; set; } // NoDeps
        public string DepsAges { get; set; } // DepsAges (length: 40)
        public string Email { get; set; } // Email (length: 50)
        public System.DateTime? DateSigned1003 { get; set; } // DateSigned1003
        public bool OmitFromTitle { get; set; } // OmitFromTitle
        public string FnmaCreditRefNo { get; set; } // FNMACreditRefNo (length: 50)
        public string VaServiceNo { get; set; } // VAServiceNo (length: 50)
        public string VaClaimFolderNo { get; set; } // VAClaimFolderNo (length: 50)
        public string Caivrs { get; set; } // CAIVRS (length: 50)
        public string Ldp { get; set; } // LDP (length: 50)
        public short OutstandingJudgements { get; set; } // OutstandingJudgements
        public short Bankruptcy { get; set; } // Bankruptcy
        public short PropertyForeclosed { get; set; } // PropertyForeclosed
        public short PartyToLawsuit { get; set; } // PartyToLawsuit
        public short LoanForeclosed { get; set; } // LoanForeclosed
        public short DelinquentFederalDebt { get; set; } // DelinquentFederalDebt
        public short AlimonyObligation { get; set; } // AlimonyObligation
        public short DownPaymentBorrowed { get; set; } // DownPaymentBorrowed
        public short EndorserOnNote { get; set; } // EndorserOnNote
        public short OccupyAsPrimaryRes { get; set; } // OccupyAsPrimaryRes
        public short OwnershipInterest { get; set; } // OwnershipInterest
        public short PropertyType { get; set; } // PropertyType
        public short TitleHeld { get; set; } // TitleHeld
        public decimal? IncomeNetRentalOv { get; set; } // IncomeNetRentalOV
        public decimal? IncomeOther1Ov { get; set; } // IncomeOther1OV
        public decimal? IncomeOther2Ov { get; set; } // IncomeOther2OV
        public decimal? IncomeTotalOv { get; set; } // IncomeTotalOV
        public string MailingStreet { get; set; } // MailingStreet (length: 50)
        public string MailingCity { get; set; } // MailingCity (length: 50)
        public string MailingState { get; set; } // MailingState (length: 2)
        public string MailingZip { get; set; } // MailingZip (length: 9)
        public string MailingCountry { get; set; } // MailingCountry (length: 50)
        public string IdentityDocType { get; set; } // IdentityDocType (length: 50)
        public string IdentityDocNo { get; set; } // IdentityDocNo (length: 50)
        public string IdentityDocPlaceOfIssuance { get; set; } // IdentityDocPlaceOfIssuance (length: 50)
        public System.DateTime? IdentityDocDateOfIssuance { get; set; } // IdentityDocDateOfIssuance
        public System.DateTime? IdentityDocExpDate { get; set; } // IdentityDocExpDate
        public bool OfacScanComplete { get; set; } // OFACScanComplete
        public string IdentityComments { get; set; } // IdentityComments (length: 300)
        public decimal? VaDeductionFedTax { get; set; } // VADeductionFedTax
        public decimal? VaDeductionStateTax { get; set; } // VADeductionStateTax
        public decimal? VaDeductionRetirement { get; set; } // VADeductionRetirement
        public decimal? VaDeductionOther { get; set; } // VADeductionOther
        public short IsVeteran { get; set; } // IsVeteran
        public int? ExperianScore { get; set; } // ExperianScore
        public string ExperianModel { get; set; } // ExperianModel (length: 50)
        public string ExperianFactors { get; set; } // ExperianFactors (length: 1000)
        public string ExperianPin { get; set; } // ExperianPIN (length: 10)
        public int? TransUnionScore { get; set; } // TransUnionScore
        public string TransUnionModel { get; set; } // TransUnionModel (length: 50)
        public string TransUnionFactors { get; set; } // TransUnionFactors (length: 1000)
        public string TransUnionPin { get; set; } // TransUnionPIN (length: 10)
        public int? EquifaxScore { get; set; } // EquifaxScore
        public string EquifaxModel { get; set; } // EquifaxModel (length: 50)
        public string EquifaxFactors { get; set; } // EquifaxFactors (length: 1000)
        public string EquifaxPin { get; set; } // EquifaxPIN (length: 10)
        public int? CreditScoreLow { get; set; } // CreditScoreLow
        public int? CreditScoreHigh { get; set; } // CreditScoreHigh
        public int? CreditScoreAverage { get; set; } // CreditScoreAverage
        public decimal? IncomeNetRental { get; set; } // _IncomeNetRental
        public decimal? IncomeOther1 { get; set; } // _IncomeOther1
        public decimal? IncomeOther2 { get; set; } // _IncomeOther2
        public decimal? IncomeTotal { get; set; } // _IncomeTotal
        public int? CreditScoreMedian { get; set; } // _CreditScoreMedian
        public bool IsNonPersonEntity { get; set; } // IsNonPersonEntity
        public string FmacCreditRefNo { get; set; } // FMACCreditRefNo (length: 50)
        public short HasLdp { get; set; } // HasLDP
        public int HampHardshipIncomeReduced { get; set; } // HAMPHardshipIncomeReduced
        public int HampHardshipCircumstancesChanged { get; set; } // HAMPHardshipCircumstancesChanged
        public int HampHardshipExpensesIncreased { get; set; } // HAMPHardshipExpensesIncreased
        public int HampHardshipCashReservesInsufficient { get; set; } // HAMPHardshipCashReservesInsufficient
        public int HampHardshipDebtPaymentsExcessive { get; set; } // HAMPHardshipDebtPaymentsExcessive
        public int HampHardshipOther { get; set; } // HAMPHardshipOther
        public int CitizenResidencyType { get; set; } // CitizenResidencyType
        public short UsCitizen { get; set; } // _USCitizen
        public short ResidentAlien { get; set; } // _ResidentAlien
        public int? EquifaxModelRangeLow { get; set; } // EquifaxModelRangeLow
        public int? EquifaxModelRangeHigh { get; set; } // EquifaxModelRangeHigh
        public int? ExperianModelRangeLow { get; set; } // ExperianModelRangeLow
        public int? ExperianModelRangeHigh { get; set; } // ExperianModelRangeHigh
        public int? TransUnionModelRangeLow { get; set; } // TransUnionModelRangeLow
        public int? TransUnionModelRangeHigh { get; set; } // TransUnionModelRangeHigh
        public decimal? EquifaxCreditScoreRank { get; set; } // EquifaxCreditScoreRank
        public decimal? ExperianCreditScoreRank { get; set; } // ExperianCreditScoreRank
        public decimal? TransUnionCreditScoreRank { get; set; } // TransUnionCreditScoreRank
        public int CreditDenialCreditBureauFlags { get; set; } // CreditDenialCreditBureauFlags
        public string CreditDenialOtherReasons { get; set; } // CreditDenialOtherReasons (length: 2147483647)
        public int CreditDenialCreditScoreBureauOv { get; set; } // CreditDenialCreditScoreBureauOV
        public int CreditDenialCreditScoreUsedOv { get; set; } // CreditDenialCreditScoreUsedOV
        public short CounselingConfirmationType { get; set; } // CounselingConfirmationType
        public short CounselingFormatType { get; set; } // CounselingFormatType
        public string MobilePhoneSmsGateway { get; set; } // MobilePhoneSMSGateway (length: 40)
        public short LegalEntityType { get; set; } // LegalEntityType
        public bool NonTraditionalCreditUsed { get; set; } // NonTraditionalCreditUsed
        public bool PostClosingMailingOverride { get; set; } // PostClosingMailingOverride
        public string PostClosingMailingStreet { get; set; } // PostClosingMailingStreet (length: 100)
        public string PostClosingMailingCity { get; set; } // PostClosingMailingCity (length: 50)
        public string PostClosingMailingState { get; set; } // PostClosingMailingState (length: 2)
        public string PostClosingMailingZip { get; set; } // PostClosingMailingZip (length: 9)
        public string PostClosingMailingCountryCode { get; set; } // PostClosingMailingCountryCode (length: 2)
        public int TaxpayerIdentifierType { get; set; } // TaxpayerIdentifierType
        public bool FirstTimeHomebuyer { get; set; } // FirstTimeHomebuyer
        public string CaivrsInfo { get; set; } // CAIVRSInfo (length: 2147483647)
        public string PoaFirstName { get; set; } // POAFirstName (length: 50)
        public string PoaMiddleName { get; set; } // POAMiddleName (length: 50)
        public string PoaLastName { get; set; } // POALastName (length: 50)
        public string PoaGeneration { get; set; } // POAGeneration (length: 10)
        public string PoaSigningCapacity { get; set; } // POASigningCapacity (length: 50)
        public string NonPersonEntitySigner { get; set; } // NonPersonEntitySigner (length: 250)
        public System.Guid? SyncGuid { get; set; } // SyncGuid
        public int ForeclosureExplanation { get; set; } // ForeclosureExplanation
        public short PrevFhaMort { get; set; } // PrevFHAMort
        public short PrevFhaMortToBeSold { get; set; } // PrevFHAMortToBeSold
        public decimal? PrevFhaMortSalesPrice { get; set; } // PrevFHAMortSalesPrice
        public decimal? PrevFhaMortOrigLoan { get; set; } // PrevFHAMortOrigLoan
        public string PrevFhaMortStreet { get; set; } // PrevFHAMortStreet (length: 50)
        public string PrevFhaMortCity { get; set; } // PrevFHAMortCity (length: 50)
        public string PrevFhaMortState { get; set; } // PrevFHAMortState (length: 2)
        public string PrevFhaMortZip { get; set; } // PrevFHAMortZip (length: 9)
        public short FinIntInSubdivision { get; set; } // FinIntInSubdivision
        public string SubdivisionDetails { get; set; } // SubdivisionDetails (length: 200)
        public short OwnMoreThanFourDwellings { get; set; } // OwnMoreThanFourDwellings
        public short EverHadVaLoan { get; set; } // EverHadVALoan
        public short VaOccupancyType { get; set; } // VAOccupancyType
        public short PriceExceedingValueAwareness { get; set; } // PriceExceedingValueAwareness
        public bool OkToEDisclose { get; set; } // OKToEDisclose
        public bool OkToPullCredit { get; set; } // OKToPullCredit
        public short CdSignatureMethod { get; set; } // CDSignatureMethod
        public int IsCoveredByMilitaryLendingAct { get; set; } // IsCoveredByMilitaryLendingAct
        public int SsaMatchResult { get; set; } // SSAMatchResult
        public string SsaCheckedSsn { get; set; } // SSACheckedSSN (length: 9)
        public string EthnicityOtherHispanicOrLatinoDesc { get; set; } // EthnicityOtherHispanicOrLatinoDesc (length: 35)
        public string RaceAmericanIndianTribe { get; set; } // RaceAmericanIndianTribe (length: 35)
        public string RaceOtherAsianDesc { get; set; } // RaceOtherAsianDesc (length: 35)
        public string RaceOtherPacificIslanderDesc { get; set; } // RaceOtherPacificIslanderDesc (length: 35)
        public int DemographicInfoProvidedMethod { get; set; } // DemographicInfoProvidedMethod
        public int Race2 { get; set; } // Race2
        public int Ethnicity2 { get; set; } // Ethnicity2
        public int Gender2 { get; set; } // Gender2
        public int Race2CompletionMethod { get; set; } // Race2CompletionMethod
        public int Ethnicity2CompletionMethod { get; set; } // Ethnicity2CompletionMethod
        public int Gender2CompletionMethod { get; set; } // Gender2CompletionMethod
        public bool GmiNotApplicable { get; set; } // GMINotApplicable
        public string CdSignatureMethodOtherDesc { get; set; } // CDSignatureMethodOtherDesc (length: 35)
        public int HasMilitaryService { get; set; } // HasMilitaryService
        public int IsMilitarySurvivingSpouse { get; set; } // IsMilitarySurvivingSpouse
        public System.DateTime? MilitaryServiceExpirationDate { get; set; } // MilitaryServiceExpirationDate
        public int LanguagePreference { get; set; } // LanguagePreference
        public string LanguageOtherDesc { get; set; } // LanguageOtherDesc (length: 35)
        public byte UseUnmarriedAddendumOv { get; set; } // UseUnmarriedAddendumOV
        public byte HasDomesticRelationship { get; set; } // HasDomesticRelationship
        public byte DomesticRelationshipType { get; set; } // DomesticRelationshipType
        public string DomesticRelationshipOtherDesc { get; set; } // DomesticRelationshipOtherDesc (length: 80)
        public string DomesticRelationshipState { get; set; } // DomesticRelationshipState (length: 2)
        public byte SpecialBorrowerSellerRelationship { get; set; } // SpecialBorrowerSellerRelationship
        public decimal? UndisclosedBorrowerFundsAmount { get; set; } // UndisclosedBorrowerFundsAmount
        public byte UndisclosedMortgageApplication { get; set; } // UndisclosedMortgageApplication
        public byte UndisclosedCreditApplication { get; set; } // UndisclosedCreditApplication
        public byte PropertyProposedCleanEnergyLien { get; set; } // PropertyProposedCleanEnergyLien
        public byte PriorPropertyShortSaleCompleted { get; set; } // PriorPropertyShortSaleCompleted
        public byte BankruptcyChapterType { get; set; } // BankruptcyChapterType
        public bool FormerResidencesDnaDesired { get; set; } // FormerResidencesDNADesired
        public bool MailingAddressDnaDesired { get; set; } // MailingAddressDNADesired
        public bool PrimaryEmployerDnaDesired { get; set; } // PrimaryEmployerDNADesired
        public bool SecondaryEmployersDnaDesired { get; set; } // SecondaryEmployersDNADesired
        public bool FormerEmployersDnaDesired { get; set; } // FormerEmployersDNADesired
        public bool OtherIncomeDnaDesired { get; set; } // OtherIncomeDNADesired
        public bool OtherAssetsDnaDesired { get; set; } // OtherAssetsDNADesired
        public bool DebtsDnaDesired { get; set; } // DebtsDNADesired
        public bool ExpensesDnaDesired { get; set; } // ExpensesDNADesired
        public bool DoNotOwnAnyRealEstateDesired { get; set; } // DoNotOwnAnyRealEstateDesired
        public bool GiftsDnaDesired { get; set; } // GiftsDNADesired
        public bool AdditionalReOsDnaDesired { get; set; } // AdditionalREOsDNADesired
        public int ConveyedTitleInLieuOfForeclosure { get; set; } // ConveyedTitleInLieuOfForeclosure
        public byte HasCompletedEducation { get; set; } // HasCompletedEducation
        public byte EducationFormat { get; set; } // EducationFormat
        public string EducationAgencyId { get; set; } // EducationAgencyID (length: 5)
        public string EducationAgencyName { get; set; } // EducationAgencyName (length: 2147483647)
        public System.DateTime? EducationCompletionDate { get; set; } // EducationCompletionDate
        public byte HasCompletedCounseling { get; set; } // HasCompletedCounseling
        public byte CounselingFormat { get; set; } // CounselingFormat
        public string CounselingAgencyId { get; set; } // CounselingAgencyID (length: 5)
        public string CounselingAgencyName { get; set; } // CounselingAgencyName (length: 2147483647)
        public System.DateTime? CounselingCompletionDate { get; set; } // CounselingCompletionDate
        public string UrlaAdditionalInfo { get; set; } // URLAAdditionalInfo (length: 2147483647)
        public byte MailingStreetContainsUnitNumberOv { get; set; } // MailingStreetContainsUnitNumberOV
        public byte IsMilitaryActiveDuty { get; set; } // IsMilitaryActiveDuty
        public byte IsMilitaryRetired { get; set; } // IsMilitaryRetired
        public byte IsMilitaryReservesOrNationalGuard { get; set; } // IsMilitaryReservesOrNationalGuard
        public int IsPurpleHeartRecipient { get; set; } // IsPurpleHeartRecipient
        public string WorkPersonalPhone { get; set; } // WorkPersonalPhone (length: 20)


        public Borrower()
        {
            FileDataId = 0;
            Ethnicity = 0;
            GovDoNotWishToFurnish = false;
            RaceNotApplicable = false;
            RaceNotProvided = false;
            RaceAmericanIndian = false;
            RaceAsian = false;
            RaceBlack = false;
            RacePacificIslander = false;
            RaceWhite = false;
            Gender = 0;
            MaritalStatus = 0;
            OmitFromTitle = false;
            OutstandingJudgements = 0;
            Bankruptcy = 0;
            PropertyForeclosed = 0;
            PartyToLawsuit = 0;
            LoanForeclosed = 0;
            DelinquentFederalDebt = 0;
            AlimonyObligation = 0;
            DownPaymentBorrowed = 0;
            EndorserOnNote = 0;
            OccupyAsPrimaryRes = 0;
            OwnershipInterest = 0;
            PropertyType = 0;
            TitleHeld = 0;
            OfacScanComplete = false;
            IsVeteran = 0;
            IsNonPersonEntity = false;
            HasLdp = 0;
            HampHardshipIncomeReduced = 0;
            HampHardshipCircumstancesChanged = 0;
            HampHardshipExpensesIncreased = 0;
            HampHardshipCashReservesInsufficient = 0;
            HampHardshipDebtPaymentsExcessive = 0;
            HampHardshipOther = 0;
            CitizenResidencyType = 0;
            UsCitizen = 0;
            ResidentAlien = 0;
            CreditDenialCreditBureauFlags = 0;
            CreditDenialCreditScoreBureauOv = 0;
            CreditDenialCreditScoreUsedOv = 0;
            CounselingConfirmationType = 0;
            CounselingFormatType = 0;
            LegalEntityType = 0;
            NonTraditionalCreditUsed = false;
            PostClosingMailingOverride = false;
            TaxpayerIdentifierType = 0;
            FirstTimeHomebuyer = false;
            ForeclosureExplanation = 0;
            PrevFhaMort = 0;
            PrevFhaMortToBeSold = 0;
            FinIntInSubdivision = 0;
            OwnMoreThanFourDwellings = 0;
            EverHadVaLoan = 0;
            VaOccupancyType = 0;
            PriceExceedingValueAwareness = 0;
            OkToEDisclose = false;
            OkToPullCredit = false;
            CdSignatureMethod = 0;
            IsCoveredByMilitaryLendingAct = 0;
            SsaMatchResult = 0;
            DemographicInfoProvidedMethod = 0;
            Race2 = 0;
            Ethnicity2 = 0;
            Gender2 = 0;
            Race2CompletionMethod = 0;
            Ethnicity2CompletionMethod = 0;
            Gender2CompletionMethod = 0;
            GmiNotApplicable = false;
            HasMilitaryService = 0;
            IsMilitarySurvivingSpouse = 0;
            LanguagePreference = 0;
            UseUnmarriedAddendumOv = 0;
            HasDomesticRelationship = 0;
            DomesticRelationshipType = 0;
            SpecialBorrowerSellerRelationship = 0;
            UndisclosedMortgageApplication = 0;
            UndisclosedCreditApplication = 0;
            PropertyProposedCleanEnergyLien = 0;
            PriorPropertyShortSaleCompleted = 0;
            BankruptcyChapterType = 0;
            FormerResidencesDnaDesired = false;
            MailingAddressDnaDesired = false;
            PrimaryEmployerDnaDesired = false;
            SecondaryEmployersDnaDesired = false;
            FormerEmployersDnaDesired = false;
            OtherIncomeDnaDesired = false;
            OtherAssetsDnaDesired = false;
            DebtsDnaDesired = false;
            ExpensesDnaDesired = false;
            DoNotOwnAnyRealEstateDesired = false;
            GiftsDnaDesired = false;
            AdditionalReOsDnaDesired = false;
            ConveyedTitleInLieuOfForeclosure = 0;
            HasCompletedEducation = 0;
            EducationFormat = 0;
            HasCompletedCounseling = 0;
            CounselingFormat = 0;
            MailingStreetContainsUnitNumberOv = 0;
            IsMilitaryActiveDuty = 0;
            IsMilitaryRetired = 0;
            IsMilitaryReservesOrNationalGuard = 0;
            IsPurpleHeartRecipient = 0;
        }


        public static Borrower Create(ActionModels.LoanFile.Borrower rmBorrower,
                                      ThirdPartyCodeList thirdPartyCodeList, int byteFileDataId)
        {

            var bor = new Borrower();
            bor.FileDataId = byteFileDataId;
            bor.FirstName = rmBorrower.LoanContact.FirstName;
            bor.LastName = rmBorrower.LoanContact.LastName;
            bor.MiddleName = rmBorrower.LoanContact.MiddleName;
            bor.Generation = rmBorrower.LoanContact.Suffix;

            bor.Dob = rmBorrower.LoanContact.DobUtc;
            bor.HomePhone = rmBorrower.LoanContact.HomePhone;
            bor.MobilePhone = rmBorrower.LoanContact.CellPhone;
            bor.Email = rmBorrower.LoanContact.EmailAddress;

            bor.MaritalStatus = (short)thirdPartyCodeList.GetByteProValue("MaritalStatus",
                                                                           rmBorrower.LoanContact.MaritalStatusId).FindEnumIndex(typeof(MaritalStatusEnum));
            
            bor.NoDeps = rmBorrower.NoOfDependent;
            bor.DepsAges = rmBorrower.DependentAge;

            

             

            return bor;


        }


        public static Borrower Update(Borrower borrower,
                                      ActionModels.LoanFile.Borrower rmBorrower,
                                      ThirdPartyCodeList thirdPartyCodeList)
        {
            borrower.FirstName = rmBorrower.LoanContact.FirstName;
            borrower.LastName = rmBorrower.LoanContact.LastName;
            borrower.MiddleName = rmBorrower.LoanContact.MiddleName;
            borrower.Generation = rmBorrower.LoanContact.Suffix;

            borrower.Dob = rmBorrower.LoanContact.DobUtc;
            borrower.HomePhone = rmBorrower.LoanContact.HomePhone;
            borrower.MobilePhone = rmBorrower.LoanContact.CellPhone;
            borrower.Email = rmBorrower.LoanContact.EmailAddress;
            borrower.MaritalStatus = (short)thirdPartyCodeList.GetByteProValue("MaritalStatus",
                                                                                    rmBorrower.LoanContact.MaritalStatusId).FindEnumIndex(typeof(MaritalStatusEnum));
            borrower.NoDeps = rmBorrower.NoOfDependent;
            borrower.DepsAges = rmBorrower.DependentAge;

            return borrower;
        }
        

        public void SetDeclaration(ActionModels.LoanFile.Borrower rmBorrower,
                                          ThirdPartyCodeList thirdPartyCodeList)
        {



            var LoanForeclosureOrJudgementIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 36)?.QuestionResponse.AnswerText;
            var BankruptcyIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 37)?.QuestionResponse.AnswerText;
            var PropertyForeclosedPastSevenYearsIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 38)?.QuestionResponse.AnswerText;
            var PartyToLawsuitIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 39)?.QuestionResponse.AnswerText;
            var OutstandingJudgementsIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 40)?.QuestionResponse.AnswerText;
            var PresentlyDelinquentIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 41)?.QuestionResponse.AnswerText;
            var BorrowedDownPaymentIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 42)?.QuestionResponse.AnswerText;
            var CoMakerEndorserOfNoteIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 43)?.QuestionResponse.AnswerText;
            var DeclarationsJIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 44)?.QuestionResponse.AnswerText;
            var IntentToOccupyIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 45)?.QuestionResponse.AnswerText;
            var AlimonyChildSupportObligationIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 46)?.QuestionResponse.AnswerText;
            var HomeownerPastThreeYearsIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 47)?.QuestionResponse.AnswerText;
            var PriorPropertyUsageType = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 49)?.QuestionResponse.AnswerText;
            var PriorPropertyTitleType = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 50)?.QuestionResponse.AnswerText;
            var DeclarationsKIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 54)?.QuestionResponse.AnswerText;


            this.OutstandingJudgements = (short)(OutstandingJudgementsIndicator == "1" ? 1 : 2);
            this.Bankruptcy = (short)(BankruptcyIndicator == "1" ? 1 : 2);
            this.PartyToLawsuit = (short)(PartyToLawsuitIndicator == "1" ? 1 : 2);
            this.PropertyForeclosed = (short)(PartyToLawsuitIndicator == "1" ? 1 : 2);
            this.LoanForeclosed = (short)(LoanForeclosureOrJudgementIndicator == "1" ? 1 : 2);
            this.AlimonyObligation = (short)(AlimonyChildSupportObligationIndicator == "1" ? 1 : 2);
            this.DownPaymentBorrowed = (short)(BorrowedDownPaymentIndicator == "1" ? 1 : 2);
            this.UndisclosedBorrowerFundsAmount = rmBorrower.AssetBorrowerBinders.FirstOrDefault()?.BorrowerAsset.Value;
            this.EndorserOnNote = (short)(CoMakerEndorserOfNoteIndicator == "1" ? 1 : 2);
            this.OwnershipInterest = (short)(HomeownerPastThreeYearsIndicator == "1" ? 1 : 2);
            this.DelinquentFederalDebt = (short)(PresentlyDelinquentIndicator == "1" ? 1 : 2);
            this.OccupyAsPrimaryRes = (short)(IntentToOccupyIndicator == "1" ? 1 : 2);

            int citizenshipType = 5;
            int isUsCitizen = (DeclarationsJIndicator == "1" || rmBorrower.LoanContact.ResidencyStateId == (int)ResidencyStateEnum.UsCitizen) ? 1 : 0; // US Citizen
            int isPermanentResident = (DeclarationsKIndicator == "1" || rmBorrower.LoanContact.ResidencyStateId == (int)ResidencyStateEnum.PermanentResident) ? 2 : 0; // Permanent Resident Alien
            if (isUsCitizen == 1)
            {
                citizenshipType = isUsCitizen;
            }
            if (isPermanentResident == 2)
            {
                citizenshipType = isPermanentResident;
            }
            this.CitizenResidencyType = citizenshipType;


            //todo hammad plz fix below lines
            if (!string.IsNullOrEmpty(PriorPropertyUsageType))
            {
                var propertyUsageIndex = (short)thirdPartyCodeList.GetByteProValue("BorPriorPropertyUsage", Convert.ToInt32(PriorPropertyUsageType)).FindEnumIndex(typeof(BorPropertyTypeEnum));
                this.PropertyType = propertyUsageIndex;
            }

            if (!string.IsNullOrEmpty(PriorPropertyTitleType))
            {
                var titleheldIndex = (short)thirdPartyCodeList.GetByteProValue("TitleHeldWith", Convert.ToInt32(PriorPropertyTitleType)).FindEnumIndex(typeof(TitleHeldEnum));
                this.TitleHeld = titleheldIndex;
            }





        }

     
        public   void SetGovernmentQuestion(  ActionModels.LoanFile.Borrower rmBorrower)
        {
            var borrowerLoanContact = rmBorrower.LoanContact;
            // Ethnicity
            IEnumerable<IGrouping<int, LoanContactEthnicityBinder>> loanContactEthnicityBinders = borrowerLoanContact.LoanContactEthnicityBinders.GroupBy(loanContactEthnicityBinder => loanContactEthnicityBinder.EthnicityId);
            foreach (var loanContactEthnicityBinder in loanContactEthnicityBinders)
            {
                switch (loanContactEthnicityBinder.Key)
                {
                    case (int)EthnicityEnum.HispanicOrLatino:
                        // borrower.IsEthnicityBasedOnVisual = ethnicityBinder.Ethnicity.Name;
                        this.Ethnicity2 += 2;
                        foreach (LoanContactEthnicityBinder contactEthnicityBinder in loanContactEthnicityBinder)
                        {
                            if (contactEthnicityBinder.Ethnicity != null)
                            {
                                if (contactEthnicityBinder.EthnicityDetailId != null)
                                {
                                    switch (contactEthnicityBinder.EthnicityDetailId)
                                    {
                                        case (int)EthnicityDetailEnum.Mexican:
                                            this.Ethnicity2 += 8;
                                            break;
                                        case (int)EthnicityDetailEnum.PuertoRican:
                                            this.Ethnicity2 += 16;
                                            break;
                                        case (int)EthnicityDetailEnum.Cuban:
                                            this.Ethnicity2 += 32;
                                            break;
                                        case (int)EthnicityDetailEnum.OtherHispanicOrLatino:
                                            this.Ethnicity2 += 64;
                                            break;
                                        default:
                                            break;
                                    }
                                }

                            }
                        }

                        break;
                    case (int)EthnicityEnum.NotHispanicOrLatino:
                        //borrower.IsEthnicityBasedOnVisual = ethnicityBinder.Ethnicity.Name;
                        this.Ethnicity2 += 4;
                        break;
                    case (int)EthnicityEnum.DoNotWishToProvideThisInformation:
                        //borrower.IsEthnicityBasedOnVisual = ethnicityBinder.Ethnicity.Name;
                        this.Ethnicity2 += 1;
                        break;

                }




            }

            // Race
            IEnumerable<IGrouping<int, LoanContactRaceBinder>> loanContactRaceBinders = borrowerLoanContact.LoanContactRaceBinders.GroupBy(dd => dd.RaceId);
            foreach (var contactRaceBinders in loanContactRaceBinders)
            {
                switch (contactRaceBinders.Key)
                {
                    case (int)RaceEnum.AmericanIndianOrAlaskaNative:
                        //borrower.IsRaceBasedOnVisual = raceBinder.Race.Name;
                        this.Race2 += 2;
                        break;
                    case (int)RaceEnum.Asian:
                        //borrower.IsRaceBasedOnVisual = raceBinder.Race.Name;
                        this.Race2 += 4;
                        foreach (LoanContactRaceBinder raceBinder in contactRaceBinders)
                        {
                            if (raceBinder.Race != null)
                            {
                                if (raceBinder.RaceDetailId != null)
                                {
                                    switch (raceBinder.RaceDetailId)
                                    {
                                        case (int)RaceDetailEnum.AsianIndian:
                                            this.Race2 += 64;
                                            break;
                                        case (int)RaceDetailEnum.Chinese:
                                            this.Race2 += 128;
                                            break;
                                        case (int)RaceDetailEnum.Filipino:
                                            this.Race2 += 256;
                                            break;
                                        case (int)RaceDetailEnum.Japanese:
                                            this.Race2 += 512;
                                            break;
                                        case (int)RaceDetailEnum.Korean:
                                            this.Race2 += 1024;
                                            break;
                                        case (int)RaceDetailEnum.Vietnamese:
                                            this.Race2 += 2048;
                                            break;
                                        case (int)RaceDetailEnum.OtherAsian:
                                            this.Race2 += 4096;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }

                        break;
                    case (int)RaceEnum.BlackOrAfricanAmerican:
                        //borrower.IsRaceBasedOnVisual = raceBinder.Race.Name;
                        this.Race2 += 8;
                        break;
                    case (int)RaceEnum.NativeHawaiianOrOtherPacificIslander:
                        //borrower.IsRaceBasedOnVisual = raceBinder.Race.Name;
                        this.Race2 += 16;
                        foreach (LoanContactRaceBinder raceBinder in contactRaceBinders)
                        {
                            if (raceBinder.Race != null)
                            {
                                if (raceBinder.RaceDetailId != null)
                                {
                                    switch (raceBinder.RaceDetailId)
                                    {
                                        case (int)RaceDetailEnum.NativeHawaiian:
                                            this.Race2 += 8192;
                                            break;
                                        case (int)RaceDetailEnum.GuamanianOrChamorro:
                                            this.Race2 += 16384;
                                            break;
                                        case (int)RaceDetailEnum.Samoan:
                                            this.Race2 += 32768;
                                            break;
                                        case (int)RaceDetailEnum.OtherPacificIslander:
                                            this.Race2 += 65536;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }

                        }



                        break;
                    case (int)RaceEnum.White:
                        //borrower.IsRaceBasedOnVisual = raceBinder.Race.Name;
                        this.Race2 += 32;
                        break;
                    case (int)RaceEnum.DoNotWishToProvideThisInformation:
                        //borrower.IsRaceBasedOnVisual = raceBinder.Race.Name;
                        this.Race2 += 1;
                        break;
                }



            }

            // Gender
            if (borrowerLoanContact.Gender != null)
            {
                switch (borrowerLoanContact.GenderId)
                {
                    case (int)GenderEnum.Female:
                        //borrower.IsSexBasedOnVisual = borrowerLoanContact.Gender.Name;
                        this.Gender2 += 2;
                        break;
                    case (int)GenderEnum.Male:
                        //borrower.IsSexBasedOnVisual = borrowerLoanContact.Gender.Name;
                        this.Gender2 += 4;
                        break;
                    case (int)GenderEnum.Do_Not_Wish:
                        this.Gender2 += 1;
                        break;
                    default:
                        break;
                }

            }



        }
    }

    // BorrowerLogin

    public class BorrowerLogin
    {
        public int BorrowerId { get; set; } // BorrowerID (Primary key)
        public string UserName { get; set; } // UserName (length: 50)
        public bool ChangePasswordAtLogon { get; set; } // ChangePasswordAtLogon
        public System.DateTime PasswordExpirationDate { get; set; } // PasswordExpirationDate
        public string EncryptedPassword { get; set; } // EncryptedPassword (length: 100)
        public bool LockedOut { get; set; } // LockedOut


        public BorrowerLogin()
        {
            ChangePasswordAtLogon = false;
            PasswordExpirationDate = System.DateTime.Now;
            LockedOut = false;
        }
    }

    // BorrowerSession

    public class BorrowerSession
    {
        public string AspNetSessionId { get; set; } // ASPNetSessionID (Primary key) (length: 32)
        public int? BorrowerId { get; set; } // BorrowerID
        public System.DateTime LastActivity { get; set; } // LastActivity


        public BorrowerSession()
        {
            LastActivity = System.DateTime.Now;
        }
    }

    // Bundle

    public class Bundle
    {
        public int BundleId { get; set; } // BundleID (Primary key)
        public string Name { get; set; } // Name (length: 50)
        public int? DocumentStackId { get; set; } // DocumentStackID
        public int DocumentFormat { get; set; } // DocumentFormat
        public int PackageFormat { get; set; } // PackageFormat
        public int DeliveryMethod { get; set; } // DeliveryMethod
        public string DeliveryMethodOtherName { get; set; } // DeliveryMethodOtherName (length: 50)
        public string Url { get; set; } // URL (length: 200)
        public string UserName { get; set; } // UserName (length: 50)
        public string HostName { get; set; } // HostName (length: 50)
        public int Port { get; set; } // Port
        public string UploadFolder { get; set; } // UploadFolder (length: 200)
        public int SftpVersion { get; set; } // SFTPVersion
        public int SftpAuthenticationType { get; set; } // SFTPAuthenticationType
        public int SftpTransferBlockSize { get; set; } // SFTPTransferBlockSize
        public string SftpPrivateKeyData { get; set; } // SFTPPrivateKeyData (length: 4096)
        public string ServerFolderName { get; set; } // ServerFolderName (length: 200)
        public string OutputFileNameFormat { get; set; } // OutputFileNameFormat (length: 200)
        public string InvestorCode { get; set; } // InvestorCode (length: 50)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Notes { get; set; } // Notes (length: 2147483647)
        public string OtherDeliveryMethodProperties { get; set; } // OtherDeliveryMethodProperties (length: 1000)
        public string ClientCode { get; set; } // ClientCode (length: 50)
        public string EncryptedPassword { get; set; } // EncryptedPassword (length: 100)


        public Bundle()
        {
            DocumentFormat = 0;
            PackageFormat = 0;
            DeliveryMethod = 0;
            Port = 0;
            SftpVersion = 0;
            SftpAuthenticationType = 0;
            SftpTransferBlockSize = 0;
            DisplayOrder = 0;
        }
    }

    // BusinessDayDefaults

    public class BusinessDayDefault
    {
        public int BusinessDayDefaultsId { get; set; } // BusinessDayDefaultsID (Primary key)
        public int ExcludedHolidays { get; set; } // ExcludedHolidays
        public bool ObserveSaturdayHolidaysOnFriday { get; set; } // ObserveSaturdayHolidaysOnFriday
        public bool DayAfterThanksgivingIsHoliday { get; set; } // DayAfterThanksgivingIsHoliday
        public bool SaturdaysAreBusinessDays { get; set; } // SaturdaysAreBusinessDays
        public bool DoNotObserveSundayHolidaysOnMonday { get; set; } // DoNotObserveSundayHolidaysOnMonday


        public BusinessDayDefault()
        {
            ExcludedHolidays = 0;
            ObserveSaturdayHolidaysOnFriday = false;
            DayAfterThanksgivingIsHoliday = false;
            SaturdaysAreBusinessDays = false;
            DoNotObserveSundayHolidaysOnMonday = false;
        }
    }

    // ByteDBProperties

    public class ByteDbProperty
    {
        public int BytePropertyId { get; set; } // BytePropertyID (Primary key)
        public string Name { get; set; } // Name (length: 50)
        public string Value { get; set; } // Value (length: 50)
    }

    // ByteMacro

    public class ByteMacro
    {
        public int ByteMacroId { get; set; } // ByteMacroID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Name { get; set; } // Name (length: 100)
        public bool Disabled { get; set; } // Disabled
        public int TriggeringEvent { get; set; } // TriggeringEvent
        public string Script { get; set; } // Script (length: 2147483647)
        public bool RunAsAdmin { get; set; } // RunAsAdmin
        public int Environments { get; set; } // Environments


        public ByteMacro()
        {
            DisplayOrder = 0;
            Disabled = false;
            TriggeringEvent = 0;
            RunAsAdmin = false;
            Environments = 0;
        }
    }

    // CA883

    public class Ca883
    {
        public int Ca883Id { get; set; } // CA883ID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public decimal? BrokerFee { get; set; } // BrokerFee
        public short HasAdditionalComp { get; set; } // HasAdditionalComp
        public decimal? AdditionalComp { get; set; } // AdditionalComp
        public decimal? CreditDisabilityInsAmount { get; set; } // CreditDisabilityInsAmount
        public short PrepayPenaltyOption { get; set; } // PrepayPenaltyOption
        public short PrepayPenaltyPrincipalOption { get; set; } // PrepayPenaltyPrincipalOption
        public int? PrepayPenaltyMonths { get; set; } // PrepayPenaltyMonths
        public short BrokerFundedOption { get; set; } // BrokerFundedOption
        public bool OmitLandAndAlts { get; set; } // OmitLandAndAlts
        public bool OmitMipff { get; set; } // OmitMIPFF
        public bool OmitSubFi { get; set; } // OmitSubFi
        public bool OmitPaidBySeller { get; set; } // OmitPaidBySeller
        public bool OmitOtherCredits { get; set; } // OmitOtherCredits
        public decimal? OtherDeduction { get; set; } // OtherDeduction
        public string OtherDeductionText { get; set; } // OtherDeductionText (length: 50)
        public string PrepayPenaltyOtherDescription { get; set; } // PrepayPenaltyOtherDescription (length: 240)
        public bool HasLimitedDocumentation { get; set; } // HasLimitedDocumentation
        public int? PrepayPenaltyTerm { get; set; } // PrepayPenaltyTerm
        public decimal? PrepayPenaltyMaxAmount { get; set; } // PrepayPenaltyMaxAmount


        public Ca883()
        {
            FileDataId = 0;
            HasAdditionalComp = 0;
            PrepayPenaltyOption = 0;
            PrepayPenaltyPrincipalOption = 0;
            BrokerFundedOption = 0;
            OmitLandAndAlts = false;
            OmitMipff = false;
            OmitSubFi = false;
            OmitPaidBySeller = false;
            OmitOtherCredits = false;
            HasLimitedDocumentation = false;
        }
    }

    // The table 'Category' is not usable by entity framework because it
    // does not have a primary key. It is listed here for completeness.
    // Category

    public class Category
    {
        public int? CategoryId { get; set; } // CategoryId
        public string CategoryDesc { get; set; } // CategoryDesc (length: 160)
    }

    // CCDLine

    public class CcdLine
    {
        public int CcdLineId { get; set; } // CCDLineID (Primary key)
        public int CcdSetId { get; set; } // CCDSetID
        public short HudLineNo { get; set; } // HUDLineNo
        public short PpfcOption { get; set; } // PPFCOption
        public string Name { get; set; } // Name (length: 50)
        public short EstimateMethod { get; set; } // EstimateMethod
        public decimal? FlatFee { get; set; } // FlatFee
        public decimal? PercentFee { get; set; } // PercentFee
        public decimal? FormulaPurFee { get; set; } // FormulaPurFee
        public decimal? FormulaPurPricePerc { get; set; } // FormulaPurPricePerc
        public decimal? FormulaPurLoanPerc { get; set; } // FormulaPurLoanPerc
        public decimal? FormulaRefiFee { get; set; } // FormulaRefiFee
        public decimal? FormulaRefiPricePerc { get; set; } // FormulaRefiPricePerc
        public decimal? FormulaRefiLoanPerc { get; set; } // FormulaRefiLoanPerc
        public short TablePurFeeBasis { get; set; } // TablePurFeeBasis
        public short TableRefiFeeBasis { get; set; } // TableRefiFeeBasis
        public bool CopyEstimateToGfe { get; set; } // CopyEstimateToGFE
        public bool CopyEstimateToComp { get; set; } // CopyEstimateToComp
        public short ClosingCostTypeOv { get; set; } // ClosingCostTypeOV
        public bool AutoUpdate { get; set; } // AutoUpdate


        public CcdLine()
        {
            CcdSetId = 0;
            HudLineNo = 0;
            PpfcOption = 0;
            EstimateMethod = 0;
            TablePurFeeBasis = 0;
            TableRefiFeeBasis = 0;
            CopyEstimateToGfe = false;
            CopyEstimateToComp = false;
            ClosingCostTypeOv = 0;
            AutoUpdate = false;
        }
    }

    // CCDSet

    public class CcdSet
    {
        public int CcdSetId { get; set; } // CCDSetID (Primary key)
        public string Name { get; set; } // Name (length: 50)
        public int MortgageTypeFilter { get; set; } // MortgageTypeFilter
        public int LoanPurposeFilter { get; set; } // LoanPurposeFilter
        public string OrganizationCodeFilter { get; set; } // OrganizationCodeFilter (length: 50)
        public string LoanProgramFilter { get; set; } // LoanProgramFilter (length: 2147483647)
        public string StateFilter { get; set; } // StateFilter (length: 2)
        public string CountyFilter { get; set; } // CountyFilter (length: 50)
        public int LoanProductTypeFilter { get; set; } // LoanProductTypeFilter
        public decimal? LoCompensationPerc { get; set; } // LOCompensationPerc
        public decimal? LoCompensationFlatFee { get; set; } // LOCompensationFlatFee
        public int DisplayOrder { get; set; } // DisplayOrder


        public CcdSet()
        {
            MortgageTypeFilter = 0;
            LoanPurposeFilter = 0;
            LoanProductTypeFilter = 0;
            DisplayOrder = 0;
        }
    }

    // CCDTableTier

    public class CcdTableTier
    {
        public int CcdTableTierId { get; set; } // CCDTableTierID (Primary key)
        public int CcdSetId { get; set; } // CCDSetID
        public int CcdLineId { get; set; } // CCDLineID
        public decimal TierLevel { get; set; } // TierLevel
        public decimal? RefiFee { get; set; } // RefiFee
        public decimal? PurFee { get; set; } // PurFee
        public decimal? RefiPerc { get; set; } // RefiPerc
        public decimal? PurPerc { get; set; } // PurPerc


        public CcdTableTier()
        {
            CcdSetId = 0;
            CcdLineId = 0;
            TierLevel = 0m;
        }
    }

    // CCSetupData

    public class CcSetupData
    {
        public int CcSetupDataId { get; set; } // CCSetupDataID (Primary key)
        public short BuydownLineNo { get; set; } // BuydownLineNo
        public short OrigFeeBasisConv { get; set; } // OrigFeeBasisConv
        public short OrigFeeBasisFha { get; set; } // OrigFeeBasisFHA
        public short OrigFeeBasisVa { get; set; } // OrigFeeBasisVA
        public short OrigFeeBasisRhs { get; set; } // OrigFeeBasisRHS
        public short OrigFeeBasisOther { get; set; } // OrigFeeBasisOther
        public short BrokerFeeBasisConv { get; set; } // BrokerFeeBasisConv
        public short BrokerFeeBasisFha { get; set; } // BrokerFeeBasisFHA
        public short BrokerFeeBasisVa { get; set; } // BrokerFeeBasisVA
        public short BrokerFeeBasisRhs { get; set; } // BrokerFeeBasisRHS
        public short BrokerFeeBasisOther { get; set; } // BrokerFeeBasisOther


        public CcSetupData()
        {
            BuydownLineNo = 0;
            OrigFeeBasisConv = 0;
            OrigFeeBasisFha = 0;
            OrigFeeBasisVa = 0;
            OrigFeeBasisRhs = 0;
            OrigFeeBasisOther = 0;
            BrokerFeeBasisConv = 0;
            BrokerFeeBasisFha = 0;
            BrokerFeeBasisVa = 0;
            BrokerFeeBasisRhs = 0;
            BrokerFeeBasisOther = 0;
        }
    }

    // CCSetupLine

    public class CcSetupLine
    {
        public int CcSetupLineId { get; set; } // CCSetupLineID (Primary key)
        public short HudLineNo { get; set; } // HUDLineNo
        public string Name { get; set; } // Name (length: 50)
        public bool LockName { get; set; } // LockName
        public bool Ppfc { get; set; } // PPFC
        public bool LockPpfc { get; set; } // LockPPFC
        public bool NonRefundable { get; set; } // NonRefundable
        public short ClosingCostType { get; set; } // ClosingCostType
        public short LockClosingCostTypeThreeState { get; set; } // LockClosingCostTypeThreeState
        public int HudLineNo2010 { get; set; } // HUDLineNo2010
        public int GfeBlock { get; set; } // GFEBlock
        public int LockGfeBlockThreeState { get; set; } // LockGFEBlockThreeState
        public string Name2010Ov { get; set; } // Name2010OV (length: 50)
        public int LockHudLineNo2010ThreeState { get; set; } // LockHUDLineNo2010ThreeState
        public int ResponsiblePartyType { get; set; } // ResponsiblePartyType
        public int LockResponsiblePartyTypeThreeState { get; set; } // LockResponsiblePartyTypeThreeState
        public int PaidToType { get; set; } // PaidToType
        public int LockPaidToTypeThreeState { get; set; } // LockPaidToTypeThreeState
        public bool Poc { get; set; } // POC
        public bool NetFromWire { get; set; } // NetFromWire
        public string GlCode { get; set; } // GLCode (length: 20)
        public int Status { get; set; } // Status
        public int PointsAndFeesDirective { get; set; } // PointsAndFeesDirective
        public int TridBlock { get; set; } // TRIDBlock
        public int LockTridBlockThreeState { get; set; } // LockTRIDBlockThreeState
        public bool IsOptional { get; set; } // IsOptional
        public int LockIsOptionalThreeState { get; set; } // LockIsOptionalThreeState
        public int TridBlockApplication { get; set; } // TRIDBlockApplication
        public int NameApplication { get; set; } // NameApplication
        public string Notes { get; set; } // Notes (length: 1000)


        public CcSetupLine()
        {
            HudLineNo = 0;
            LockName = false;
            Ppfc = false;
            LockPpfc = false;
            NonRefundable = false;
            ClosingCostType = 0;
            LockClosingCostTypeThreeState = 0;
            HudLineNo2010 = 0;
            GfeBlock = 0;
            LockGfeBlockThreeState = 0;
            LockHudLineNo2010ThreeState = 0;
            ResponsiblePartyType = 0;
            LockResponsiblePartyTypeThreeState = 0;
            PaidToType = 0;
            LockPaidToTypeThreeState = 0;
            Poc = false;
            NetFromWire = false;
            Status = 0;
            PointsAndFeesDirective = 0;
            TridBlock = 0;
            LockTridBlockThreeState = 0;
            IsOptional = false;
            LockIsOptionalThreeState = 0;
            TridBlockApplication = 0;
            NameApplication = 0;
        }
    }

    // Closing

    public class Closing
    {
        public int ClosingId { get; set; } // ClosingID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public string NotaryName { get; set; } // NotaryName (length: 50)
        public string PropertyTaxDesc { get; set; } // PropertyTaxDesc (length: 50)
        public string PropertyTaxYearDesc { get; set; } // PropertyTaxYearDesc (length: 20)
        public string TrustName { get; set; } // TrustName (length: 300)
        public short TrustType { get; set; } // TrustType
        public System.DateTime? TrustEstablishedDate { get; set; } // TrustEstablishedDate
        public string TrustState { get; set; } // TrustState (length: 2)
        public string VestingDescription { get; set; } // VestingDescription (length: 500)
        public string TrustTypeOtherDescription { get; set; } // TrustTypeOtherDescription (length: 50)
        public string TrustName2 { get; set; } // TrustName2 (length: 300)
        public short TrustType2 { get; set; } // TrustType2
        public string TrustTypeOtherDescription2 { get; set; } // TrustTypeOtherDescription2 (length: 50)
        public System.DateTime? TrustEstablishedDate2 { get; set; } // TrustEstablishedDate2
        public string TrustState2 { get; set; } // TrustState2 (length: 2)
        public int LoanProceedsTo { get; set; } // LoanProceedsTo
        public int RefiOriginalCreditorIncrease { get; set; } // RefiOriginalCreditorIncrease
        public short CdSplitDisclosureIndicator { get; set; } // CDSplitDisclosureIndicator
        public int? CdMainEmbeddedDocId { get; set; } // CDMainEmbeddedDocID
        public int? CdSellerEmbeddedDocId { get; set; } // CDSellerEmbeddedDocID
        public int TexasA6Status { get; set; } // TexasA6Status


        public Closing()
        {
            FileDataId = 0;
            TrustType = 0;
            TrustType2 = 0;
            LoanProceedsTo = 0;
            RefiOriginalCreditorIncrease = 0;
            CdSplitDisclosureIndicator = 0;
            TexasA6Status = 0;
        }
    }

    // ClosingAdjustment

    public class ClosingAdjustment
    {
        public int ClosingAdjustmentId { get; set; } // ClosingAdjustmentID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int DisplayOrder { get; set; } // DisplayOrder
        public int Section { get; set; } // Section
        public int ItemType { get; set; } // ItemType
        public string DescriptionOv { get; set; } // DescriptionOV (length: 50)
        public bool Poc { get; set; } // POC
        public decimal? Amount { get; set; } // Amount
        public string PaidByName { get; set; } // PaidByName (length: 100)
        public int PaidByType { get; set; } // PaidByType
        public bool PaidByNonPersonEntity { get; set; } // PaidByNonPersonEntity
        public int SubSection { get; set; } // SubSection
        public decimal? PrincipalAmountOfLoan { get; set; } // PrincipalAmountOfLoan
        public int? LineNumber { get; set; } // LineNumber


        public ClosingAdjustment()
        {
            FileDataId = 0;
            DisplayOrder = 0;
            Section = 0;
            ItemType = 0;
            Poc = false;
            PaidByType = 0;
            PaidByNonPersonEntity = false;
            SubSection = 0;
        }
    }

    // ClosingCost

    public class ClosingCost
    {
        public int Ccid { get; set; } // CCID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int? LoanId { get; set; } // LoanID
        public short HudLineNo { get; set; } // HUDLineNo
        public bool Ppfc { get; set; } // PPFC
        public decimal? Points { get; set; } // Points
        public string Name { get; set; } // Name (length: 50)
        public decimal? BorrowerAmount { get; set; } // BorrowerAmount
        public decimal? SellerAmount { get; set; } // SellerAmount
        public bool Poc { get; set; } // POC
        public bool IsPaidToBroker { get; set; } // IsPaidToBroker
        public decimal? PaidToBrokerSplit { get; set; } // PaidToBrokerSplit
        public bool NotCounted { get; set; } // NotCounted
        public string PaidToName { get; set; } // PaidToName (length: 50)
        public short ClosingCostType { get; set; } // ClosingCostType
        public decimal? TotalAmount { get; set; } // _TotalAmount
        public decimal? PaidToBroker { get; set; } // _PaidToBroker
        public decimal? PaidToOthers { get; set; } // _PaidToOthers
        public int PaidByOtherType { get; set; } // PaidByOtherType
        public decimal? GfeDisclosedAmount { get; set; } // GFEDisclosedAmount
        public bool ProviderChosenByBorrower { get; set; } // ProviderChosenByBorrower
        public int HudLineNo2010 { get; set; } // HUDLineNo2010
        public int GfeBlock { get; set; } // GFEBlock
        public int ResponsiblePartyType { get; set; } // ResponsiblePartyType
        public int PaidToType { get; set; } // PaidToType
        public bool NetFromWire { get; set; } // NetFromWire
        public string GlCode { get; set; } // _GLCode (length: 20)
        public bool Financed { get; set; } // Financed
        public decimal? PointsAndFeesAmountOv { get; set; } // PointsAndFeesAmountOV
        public int TridBlock { get; set; } // TRIDBlock
        public int IsOptionalOv { get; set; } // IsOptionalOV
        public decimal? GfeBaselineAmount { get; set; } // GFEBaselineAmount
        public decimal? BorrowerPocAmountOv { get; set; } // BorrowerPOCAmountOV
        public decimal? SellerPocAmountOv { get; set; } // SellerPOCAmountOV


        public ClosingCost()
        {
            FileDataId = 0;
            HudLineNo = 0;
            Ppfc = false;
            Poc = false;
            IsPaidToBroker = false;
            NotCounted = false;
            ClosingCostType = 0;
            PaidByOtherType = 0;
            ProviderChosenByBorrower = false;
            HudLineNo2010 = 0;
            GfeBlock = 0;
            ResponsiblePartyType = 0;
            PaidToType = 0;
            NetFromWire = false;
            Financed = false;
            TridBlock = 0;
            IsOptionalOv = 0;
        }
    }

    // ClosingPayoff

    public class ClosingPayoff
    {
        public int ClosingPayoffId { get; set; } // ClosingPayoffID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int DisplayOrder { get; set; } // DisplayOrder
        public int LiabilityType { get; set; } // LiabilityType
        public string LiabilityTypeOtherDesc { get; set; } // LiabilityTypeOtherDesc (length: 50)
        public string DescriptionOv { get; set; } // DescriptionOV (length: 100)
        public string HeldByName { get; set; } // HeldByName (length: 100)
        public decimal? PayoffAmount { get; set; } // PayoffAmount
        public decimal? PrepaymentPenaltyAmount { get; set; } // PrepaymentPenaltyAmount
        public int IsSecuredBySubProp { get; set; } // IsSecuredBySubProp
        public int ClosingPayoffType { get; set; } // ClosingPayoffType
        public int ClosingAdjustmentItemEnum { get; set; } // ClosingAdjustmentItemEnum
        public string PaidBy { get; set; } // PaidBy (length: 100)
        public bool PaidByNonPersonEntity { get; set; } // PaidByNonPersonEntity
        public bool IncludePppInPointsAndFees { get; set; } // IncludePPPInPointsAndFees
        public bool Poc { get; set; } // POC
        public decimal? PrincipalAmountOfLoan { get; set; } // PrincipalAmountOfLoan


        public ClosingPayoff()
        {
            FileDataId = 0;
            DisplayOrder = 0;
            LiabilityType = 0;
            IsSecuredBySubProp = 0;
            ClosingPayoffType = 0;
            ClosingAdjustmentItemEnum = 0;
            PaidByNonPersonEntity = false;
            IncludePppInPointsAndFees = false;
            Poc = false;
        }
    }

    // ClosingProration

    public class ClosingProration
    {
        public int ClosingProrationId { get; set; } // ClosingProrationID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int DisplayOrder { get; set; } // DisplayOrder
        public int Section { get; set; } // Section
        public int ItemType { get; set; } // ItemType
        public string DescriptionOv { get; set; } // DescriptionOV (length: 50)
        public bool Poc { get; set; } // POC
        public decimal? Amount { get; set; } // Amount
        public System.DateTime? PaidFromDate { get; set; } // PaidFromDate
        public System.DateTime? PaidToDate { get; set; } // PaidToDate
        public int? LineNumber { get; set; } // LineNumber


        public ClosingProration()
        {
            FileDataId = 0;
            DisplayOrder = 0;
            Section = 0;
            ItemType = 0;
            Poc = false;
        }
    }

    // COCLogEntry

    public class CocLogEntry
    {
        public int CocLogEntryId { get; set; } // COCLogEntryID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int DisplayOrder { get; set; } // DisplayOrder
        public System.DateTime? EntryDate { get; set; } // EntryDate
        public string UserName { get; set; } // UserName (length: 50)
        public string FeeDetails { get; set; } // FeeDetails (length: 2147483647)
        public string Notes { get; set; } // Notes (length: 2147483647)
        public string CocReasonName { get; set; } // COCReasonName (length: 200)
        public int Category { get; set; } // Category
        public string CocReasonCode { get; set; } // COCReasonCode (length: 50)
        public System.DateTime? ChangeDate { get; set; } // ChangeDate
        public int? DisclosureLogEntryId { get; set; } // DisclosureLogEntryID


        public CocLogEntry()
        {
            FileDataId = 0;
            DisplayOrder = 0;
            Category = 0;
        }
    }

    // COCReason

    public class CocReason
    {
        public int CocReasonId { get; set; } // COCReasonID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Name { get; set; } // Name (length: 200)
        public bool RestrictFees { get; set; } // RestrictFees
        public string InternalNotes { get; set; } // InternalNotes (length: 500)
        public int Category { get; set; } // Category
        public string Code { get; set; } // Code (length: 50)
        public int NotesOption { get; set; } // NotesOption
        public bool AutoSelectFees { get; set; } // AutoSelectFees


        public CocReason()
        {
            DisplayOrder = 0;
            RestrictFees = false;
            Category = 0;
            NotesOption = 0;
            AutoSelectFees = false;
        }
    }

    // COCReasonFee

    public class CocReasonFee
    {
        public int CocReasonFeeId { get; set; } // COCReasonFeeID (Primary key)
        public int CocReasonId { get; set; } // COCReasonID
        public int DisplayOrder { get; set; } // DisplayOrder
        public int UpdatableFeeType { get; set; } // UpdatableFeeType
        public int ClosingCostHudLineNo { get; set; } // ClosingCostHUDLineNo
        public int PrepaidItemType { get; set; } // PrepaidItemType


        public CocReasonFee()
        {
            CocReasonId = 0;
            DisplayOrder = 0;
            UpdatableFeeType = 0;
            ClosingCostHudLineNo = 0;
            PrepaidItemType = 0;
        }
    }

    // CompPlan

    public class CompPlan
    {
        public int CompPlanId { get; set; } // CompPlanID (Primary key)
        public int CompPlanType { get; set; } // CompPlanType
        public string CompPlanCode { get; set; } // CompPlanCode (length: 50)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Notes { get; set; } // Notes (length: 2147483647)


        public CompPlan()
        {
            CompPlanType = 0;
            DisplayOrder = 0;
        }
    }

    // CompPlanCustomField

    public class CompPlanCustomField
    {
        public int CompPlanCustomFieldId { get; set; } // CompPlanCustomFieldID (Primary key)
        public int CompPlanId { get; set; } // CompPlanID
        public string Name { get; set; } // Name (length: 50)
        public string Value { get; set; } // Value (length: 2147483647)


        public CompPlanCustomField()
        {
            CompPlanId = 0;
        }
    }

    // CompPlanCustomFieldDef

    public class CompPlanCustomFieldDef
    {
        public int CompPlanCustomFieldDefId { get; set; } // CompPlanCustomFieldDefID (Primary key)
        public int CompPlanType { get; set; } // CompPlanType
        public string Name { get; set; } // Name (length: 50)
        public int DataType { get; set; } // DataType
        public int DisplayOrder { get; set; } // DisplayOrder


        public CompPlanCustomFieldDef()
        {
            CompPlanType = 0;
            DataType = 0;
            DisplayOrder = 0;
        }
    }

    // Condition

    public class Condition
    {
        public int ConditionId { get; set; } // ConditionID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int DisplayOrder { get; set; } // DisplayOrder
        public int? ConditionNo { get; set; } // ConditionNo
        public string Description { get; set; } // _Description (length: 1500)
        public int ConditionStage { get; set; } // ConditionStage
        public bool IsInternal { get; set; } // IsInternal
        public int FollowUpFlag { get; set; } // FollowUpFlag
        public string ReceivedBy { get; set; } // ReceivedBy (length: 50)
        public int? ReceivedTaskId { get; set; } // ReceivedTaskID
        public string SubmittedBy { get; set; } // SubmittedBy (length: 50)
        public int? SubmittedTaskId { get; set; } // SubmittedTaskID
        public string ClearedBy { get; set; } // ClearedBy (length: 50)
        public int? ClearedTaskId { get; set; } // ClearedTaskID
        public System.DateTime? FollowUpDate { get; set; } // FollowUpDate
        public bool IsAdHoc { get; set; } // IsAdHoc
        public string ConditionClassCode { get; set; } // ConditionClassCode (length: 20)
        public string DescriptionTemplate { get; set; } // DescriptionTemplate (length: 1500)
        public short ConditionDescriptionType { get; set; } // ConditionDescriptionType
        public string ConditionTypeCode { get; set; } // ConditionTypeCode (length: 20)
        public string ConditionTypeAndNo { get; set; } // _ConditionTypeAndNo (length: 30)
        public string ExceptionRequestedBy { get; set; } // ExceptionRequestedBy (length: 50)
        public System.DateTime? DateExceptionRequested { get; set; } // DateExceptionRequested
        public int ExceptionStatus { get; set; } // ExceptionStatus
        public int ResponsibleParty { get; set; } // ResponsibleParty
        public System.DateTime? RequestedDate { get; set; } // RequestedDate
        public string RequestedBy { get; set; } // RequestedBy (length: 50)
        public int? RequestedTaskId { get; set; } // RequestedTaskID
        public System.DateTime? ReceivedDate { get; set; } // ReceivedDate
        public System.DateTime? SubmittedDate { get; set; } // SubmittedDate
        public System.DateTime? ClearedDate { get; set; } // ClearedDate
        public string Notes { get; set; } // Notes (length: 2147483647)
        public System.DateTime? RecordingDate { get; set; } // RecordingDate
        public System.DateTime? ExpirationDate { get; set; } // ExpirationDate
        public bool NeededFromBorrower { get; set; } // NeededFromBorrower


        public Condition()
        {
            FileDataId = 0;
            DisplayOrder = 0;
            ConditionStage = 0;
            IsInternal = false;
            FollowUpFlag = 0;
            IsAdHoc = false;
            ConditionDescriptionType = 0;
            ExceptionStatus = 0;
            ResponsibleParty = 0;
            NeededFromBorrower = false;
        }
    }

    // ConditionClass

    public class ConditionClass
    {
        public int ConditionClassId { get; set; } // ConditionClassID (Primary key)
        public string ConditionClassCode { get; set; } // ConditionClassCode (length: 20)
        public int AllowableStages { get; set; } // AllowableStages
        public int DisplayOrder { get; set; } // DisplayOrder


        public ConditionClass()
        {
            AllowableStages = 0;
            DisplayOrder = 0;
        }
    }

    // ConditionFilterClause

    public class ConditionFilterClause
    {
        public int ConditionFilterClauseId { get; set; } // ConditionFilterClauseID (Primary key)
        public int ConditionTemplateId { get; set; } // ConditionTemplateID
        public int DisplayOrder { get; set; } // DisplayOrder
        public int GroupingLevel { get; set; } // GroupingLevel
        public int FilterClauseAndOrType { get; set; } // FilterClauseAndOrType
        public string ObjectName { get; set; } // ObjectName (length: 100)
        public string PropertyName { get; set; } // PropertyName (length: 50)
        public int FilterOperator { get; set; } // FilterOperator
        public string FilterValue { get; set; } // FilterValue (length: 1000)


        public ConditionFilterClause()
        {
            ConditionTemplateId = 0;
            DisplayOrder = 0;
            GroupingLevel = 0;
            FilterClauseAndOrType = 0;
            FilterOperator = 0;
        }
    }

    // ConditionStageInfo

    public class ConditionStageInfo
    {
        public int ConditionStageInfoId { get; set; } // ConditionStageInfoID (Primary key)
        public int ConditionStage { get; set; } // ConditionStage
        public string NameOv { get; set; } // NameOV (length: 50)
        public int NeededItemType { get; set; } // NeededItemType


        public ConditionStageInfo()
        {
            ConditionStage = 0;
            NeededItemType = 0;
        }
    }

    // ConditionTemplate

    public class ConditionTemplate
    {
        public int ConditionTemplateId { get; set; } // ConditionTemplateID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public int? ConditionNo { get; set; } // ConditionNo
        public string DescriptionTemplate { get; set; } // DescriptionTemplate (length: 2147483647)
        public int ConditionStage { get; set; } // ConditionStage
        public bool IsInternal { get; set; } // IsInternal
        public bool IsStandard { get; set; } // IsStandard
        public string ConditionClassCode { get; set; } // ConditionClassCode (length: 20)
        public short ConditionDescriptionType { get; set; } // ConditionDescriptionType
        public string ConditionTypeCode { get; set; } // ConditionTypeCode (length: 20)
        public int ResponsibleParty { get; set; } // ResponsibleParty
        public bool NeededFromBorrower { get; set; } // NeededFromBorrower


        public ConditionTemplate()
        {
            DisplayOrder = 0;
            ConditionStage = 0;
            IsInternal = false;
            IsStandard = false;
            ConditionDescriptionType = 0;
            ResponsibleParty = 0;
            NeededFromBorrower = false;
        }
    }

    // ConditionType

    public class ConditionType
    {
        public int ConditionTypeId { get; set; } // ConditionTypeID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string ConditionTypeCode { get; set; } // ConditionTypeCode (length: 20)
        public int StartingConditionNo { get; set; } // StartingConditionNo


        public ConditionType()
        {
            DisplayOrder = 0;
            StartingConditionNo = 0;
        }
    }

    // ConsumerPortalContent

    public class ConsumerPortalContent
    {
        public int ConsumerPortalContentId { get; set; } // ConsumerPortalContentID (Primary key)
        public int PortalCustomContentType { get; set; } // PortalCustomContentType
        public string TextContent { get; set; } // TextContent (length: 2147483647)
        public byte[] ImageContent { get; set; } // ImageContent (length: 2147483647)


        public ConsumerPortalContent()
        {
            PortalCustomContentType = 0;
        }
    }

    // Contact

    public class Contact
    {
        public int ContactId { get; set; } // ContactID (Primary key)
        public short CategoryId1 { get; set; } // CategoryID1
        public short CategoryId2 { get; set; } // CategoryID2
        public short CategoryId3 { get; set; } // CategoryID3
        public string FirstName { get; set; } // FirstName (length: 50)
        public string MiddleName { get; set; } // MiddleName (length: 50)
        public string LastName { get; set; } // LastName (length: 50)
        public string Title { get; set; } // Title (length: 50)
        public string Company { get; set; } // Company (length: 100)
        public string Street { get; set; } // Street (length: 50)
        public string City { get; set; } // City (length: 50)
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 9)
        public string EMail { get; set; } // EMail (length: 250)
        public string WorkPhone { get; set; } // WorkPhone (length: 20)
        public string HomePhone { get; set; } // HomePhone (length: 20)
        public string MobilePhone { get; set; } // MobilePhone (length: 20)
        public string OtherPhone { get; set; } // OtherPhone (length: 20)
        public string Pager { get; set; } // Pager (length: 20)
        public string Fax { get; set; } // Fax (length: 20)
        public string LicenseNo { get; set; } // LicenseNo (length: 50)
        public string ChumsNo { get; set; } // CHUMSNo (length: 50)
        public string OriginatorId { get; set; } // OriginatorID (length: 50)
        public string BranchId { get; set; } // BranchID (length: 20)
        public string Notes { get; set; } // Notes (length: 2000)
        public short ContactSharingLevel { get; set; } // ContactSharingLevel
        public int OrganizationId { get; set; } // OrganizationID
        public string UserName { get; set; } // UserName (length: 50)
        public string ContactNmlsid { get; set; } // ContactNMLSID (length: 50)
        public string CompanyNmlsid { get; set; } // CompanyNMLSID (length: 50)
        public string CompanyEin { get; set; } // CompanyEIN (length: 9)
        public string MobilePhoneSmsGateway { get; set; } // MobilePhoneSMSGateway (length: 40)
        public bool Disabled { get; set; } // Disabled
        public string CompanyLicenseNo { get; set; } // CompanyLicenseNo (length: 50)
        public string WirePrimaryBankName { get; set; } // WirePrimaryBankName (length: 50)
        public string WirePrimaryBankCity { get; set; } // WirePrimaryBankCity (length: 50)
        public string WirePrimaryBankState { get; set; } // WirePrimaryBankState (length: 2)
        public string WirePrimaryAbaNo { get; set; } // WirePrimaryABANo (length: 9)
        public string WirePrimaryAccountNo { get; set; } // WirePrimaryAccountNo (length: 22)
        public string WireFctBankName { get; set; } // WireFCTBankName (length: 50)
        public string WireFctBankCity { get; set; } // WireFCTBankCity (length: 50)
        public string WireFctBankState { get; set; } // WireFCTBankState (length: 2)
        public string WireFctabaNo { get; set; } // WireFCTABANo (length: 9)
        public string WireFctAccountNo { get; set; } // WireFCTAccountNo (length: 22)
        public string TitleUnderwriter { get; set; } // TitleUnderwriter (length: 25)
        public string WirePrimaryBankStreet { get; set; } // WirePrimaryBankStreet (length: 50)
        public string WireFctBankStreet { get; set; } // WireFCTBankStreet (length: 50)
        public string WirePrimaryBankZip { get; set; } // WirePrimaryBankZip (length: 90)
        public string WireFctBankZip { get; set; } // WireFCTBankZip (length: 9)
        public System.DateTime? EAndOPolicyExpirationDate { get; set; } // EAndOPolicyExpirationDate
        public string LicensingAgencyCode { get; set; } // LicensingAgencyCode (length: 20)
        public string EMail2 { get; set; } // EMail2 (length: 250)
        public string EMail3 { get; set; } // EMail3 (length: 250)


        public Contact()
        {
            CategoryId1 = 0;
            CategoryId2 = 0;
            CategoryId3 = 0;
            ContactSharingLevel = 0;
            OrganizationId = 0;
            Disabled = false;
        }
    }

    // ContactCategory

    public class ContactCategory
    {
        public int ContactCategoryId { get; set; } // ContactCategoryID (Primary key)
        public int ContactCategory_ { get; set; } // ContactCategory
        public short ContactCategorySharingLevel { get; set; } // ContactCategorySharingLevel
        public bool ResponsiblePartyForConditions { get; set; } // ResponsiblePartyForConditions
        public bool CopyNotesToParty { get; set; } // CopyNotesToParty
        public int SyncData { get; set; } // SyncData
        public string NameOv { get; set; } // NameOV (length: 50)


        public ContactCategory()
        {
            ContactCategory_ = 0;
            ContactCategorySharingLevel = 0;
            ResponsiblePartyForConditions = false;
            CopyNotesToParty = false;
            SyncData = 0;
        }
    }

    // ContactOrganization

    public class ContactOrganization
    {
        public int ContactId { get; set; } // ContactID (Primary key)
        public int OrganizationId { get; set; } // OrganizationID (Primary key)
    }

    // Conversation

    public class Conversation
    {
        public int ConversationId { get; set; } // ConversationID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public System.DateTime? ConversationTime { get; set; } // ConversationTime
        public string TalkedTo { get; set; } // TalkedTo (length: 50)
        public short FollowUpFlag { get; set; } // FollowUpFlag
        public string Notes { get; set; } // Notes (length: 2147483647)
        public short AlertFlag { get; set; } // AlertFlag
        public long AlertUserRoles { get; set; } // AlertUserRoles
        public System.DateTime? FollowUpDate { get; set; } // FollowUpDate


        public Conversation()
        {
            FileDataId = 0;
            FollowUpFlag = 0;
            AlertFlag = 0;
            AlertUserRoles = 0;
        }
    }

    // CounselingAgencyDef

    public class CounselingAgencyDef
    {
        public int CounselingAgencyDefId { get; set; } // CounselingAgencyDefID (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Street1 { get; set; } // Street1 (length: 60)
        public string Street2 { get; set; } // Street2 (length: 60)
        public string City { get; set; } // City (length: 50)
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 9)
        public string Phone { get; set; } // Phone (length: 16)
        public string Url { get; set; } // URL (length: 120)
        public string Services { get; set; } // Services (length: 200)
        public string Languages { get; set; } // Languages (length: 200)
        public string Email { get; set; } // Email (length: 100)
        public decimal? Latitude { get; set; } // Latitude
        public decimal? Longitude { get; set; } // Longitude
    }

    // CounselingLanguage

    public class CounselingLanguage
    {
        public int CounselingLanguageId { get; set; } // CounselingLanguageID (Primary key)
        public string HudCode { get; set; } // HUDCode (length: 4)
        public string Language { get; set; } // Language (length: 50)
    }

    // CounselingService

    public class CounselingService
    {
        public int CounselingServiceId { get; set; } // CounselingServiceID (Primary key)
        public string HudCode { get; set; } // HUDCode (length: 4)
        public string Service { get; set; } // Service (length: 100)
    }

    // County

    public class County
    {
        public int CountyId { get; set; } // CountyID (Primary key)
        public int CountySetId { get; set; } // CountySetID
        public string Name { get; set; } // Name (length: 50)
        public int CountyCode { get; set; } // CountyCode
        public decimal? MaxOneFamily { get; set; } // MaxOneFamily
        public decimal? MaxTwoFamily { get; set; } // MaxTwoFamily
        public decimal? MaxThreeFamily { get; set; } // MaxThreeFamily
        public decimal? MaxFourFamily { get; set; } // MaxFourFamily


        public County()
        {
            CountySetId = 0;
            CountyCode = 0;
        }
    }

    // CountySet

    public class CountySet
    {
        public int CountySetId { get; set; } // CountySetID (Primary key)
        public string StateAbbr { get; set; } // StateAbbr (length: 2)
    }

    // CreditAgency

    public class CreditAgency
    {
        public int CreditAgencyId { get; set; } // CreditAgencyId (Primary key)
        public int CreditAgencyCode { get; set; } // CreditAgencyCode
        public string CreditAgencyName { get; set; } // CreditAgencyName (length: 50)


        public CreditAgency()
        {
            CreditAgencyCode = 0;
        }
    }

    // CreditAlias

    public class CreditAlia
    {
        public int CreditAliasId { get; set; } // CreditAliasID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int? BorrowerId { get; set; } // BorrowerID
        public string FirstName { get; set; } // FirstName (length: 50)
        public string MiddleName { get; set; } // MiddleName (length: 50)
        public string LastName { get; set; } // LastName (length: 50)
        public string CreditorName { get; set; } // CreditorName (length: 50)
        public string AccountNo { get; set; } // AccountNo (length: 50)
        public System.Guid? SyncGuid { get; set; } // SyncGuid
        public int CreditAliasType { get; set; } // CreditAliasType
        public string Generation { get; set; } // Generation (length: 10)


        public CreditAlia()
        {
            FileDataId = 0;
            CreditAliasType = 0;
        }
    }

    // CreditDenial

    public class CreditDenial
    {
        public int CreditDenialId { get; set; } // CreditDenialID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public bool CreditNoCreditFile { get; set; } // CreditNoCreditFile
        public bool CreditNumberOfReferences { get; set; } // CreditNumberOfReferences
        public bool CreditInsufficientFiles { get; set; } // CreditInsufficientFiles
        public bool CreditLimitedExperience { get; set; } // CreditLimitedExperience
        public bool CreditUnableToVerifyReferences { get; set; } // CreditUnableToVerifyReferences
        public bool CreditGarnishment { get; set; } // CreditGarnishment
        public bool CreditJudgment { get; set; } // CreditJudgment
        public bool CreditExcessiveObligation { get; set; } // CreditExcessiveObligation
        public bool CreditPaymentRecordPreviousMtg { get; set; } // CreditPaymentRecordPreviousMtg
        public bool CreditLackOfCashReserves { get; set; } // CreditLackOfCashReserves
        public bool CreditDelinquentObligationOthers { get; set; } // CreditDelinquentObligationOthers
        public bool CreditBankruptcy { get; set; } // CreditBankruptcy
        public bool CreditTypeOfReference { get; set; } // CreditTypeOfReference
        public bool CreditPoorPerformanceUs { get; set; } // CreditPoorPerformanceUs
        public bool EmpUnableToVerify { get; set; } // EmpUnableToVerify
        public bool EmpLength { get; set; } // EmpLength
        public bool EmpTempOrIrregular { get; set; } // EmpTempOrIrregular
        public bool IncInsufficientForAmount { get; set; } // IncInsufficientForAmount
        public bool IncUnableToVerify { get; set; } // IncUnableToVerify
        public bool IncExcessiveObligations { get; set; } // IncExcessiveObligations
        public bool ResTemporary { get; set; } // ResTemporary
        public bool ResLength { get; set; } // ResLength
        public bool ResUnableToVerify { get; set; } // ResUnableToVerify
        public bool DeniedHud { get; set; } // DeniedHUD
        public bool DeniedVa { get; set; } // DeniedVa
        public bool DeniedFannie { get; set; } // DeniedFannie
        public bool DeniedFreddie { get; set; } // DeniedFreddie
        public bool DeniedOther { get; set; } // DeniedOther
        public string DeniedOtherDesc { get; set; } // DeniedOtherDesc (length: 50)
        public bool OtherInsufficientFundsToClose { get; set; } // OtherInsufficientFundsToClose
        public bool OtherCreditApplicationIncomplete { get; set; } // OtherCreditApplicationIncomplete
        public bool OtherValueOrTypeOfCollateral { get; set; } // OtherValueOrTypeOfCollateral
        public bool OtherUnacceptableProperty { get; set; } // OtherUnacceptableProperty
        public bool OtherInsufficientDataProperty { get; set; } // OtherInsufficientDataProperty
        public bool OtherUnacceptableAppraisal { get; set; } // OtherUnacceptableAppraisal
        public bool OtherUnacceptalbeLeasehold { get; set; } // OtherUnacceptalbeLeasehold
        public bool OtherTermsAndConditions { get; set; } // OtherTermsAndConditions
        public bool OtherSpecify { get; set; } // OtherSpecify
        public string OtherDescription { get; set; } // OtherDescription (length: 200)
        public string ActionTakenDescription { get; set; } // ActionTakenDescription (length: 300)
        public string AccountDescription { get; set; } // AccountDescription (length: 150)
        public string CompanyName { get; set; } // CompanyName (length: 50)
        public string CompanyStreet { get; set; } // CompanyStreet (length: 50)
        public string CompanyCity { get; set; } // CompanyCity (length: 50)
        public string CompanyState { get; set; } // CompanyState (length: 2)
        public string CompanyZip { get; set; } // CompanyZip (length: 9)
        public string CompanyPhone { get; set; } // CompanyPhone (length: 20)
        public bool CreditDecision1 { get; set; } // CreditDecision1
        public bool CreditDecision2 { get; set; } // CreditDecision2
        public short NoticeDeliveryMethod { get; set; } // NoticeDeliveryMethod
        public string PreparedBy { get; set; } // PreparedBy (length: 50)
        public bool Equifax { get; set; } // Equifax
        public bool Experian { get; set; } // Experian
        public bool TransUnion { get; set; } // TransUnion
        public bool CreditNumberOfInquiries { get; set; } // CreditNumberOfInquiries


        public CreditDenial()
        {
            FileDataId = 0;
            CreditNoCreditFile = false;
            CreditNumberOfReferences = false;
            CreditInsufficientFiles = false;
            CreditLimitedExperience = false;
            CreditUnableToVerifyReferences = false;
            CreditGarnishment = false;
            CreditJudgment = false;
            CreditExcessiveObligation = false;
            CreditPaymentRecordPreviousMtg = false;
            CreditLackOfCashReserves = false;
            CreditDelinquentObligationOthers = false;
            CreditBankruptcy = false;
            CreditTypeOfReference = false;
            CreditPoorPerformanceUs = false;
            EmpUnableToVerify = false;
            EmpLength = false;
            EmpTempOrIrregular = false;
            IncInsufficientForAmount = false;
            IncUnableToVerify = false;
            IncExcessiveObligations = false;
            ResTemporary = false;
            ResLength = false;
            ResUnableToVerify = false;
            DeniedHud = false;
            DeniedVa = false;
            DeniedFannie = false;
            DeniedFreddie = false;
            DeniedOther = false;
            OtherInsufficientFundsToClose = false;
            OtherCreditApplicationIncomplete = false;
            OtherValueOrTypeOfCollateral = false;
            OtherUnacceptableProperty = false;
            OtherInsufficientDataProperty = false;
            OtherUnacceptableAppraisal = false;
            OtherUnacceptalbeLeasehold = false;
            OtherTermsAndConditions = false;
            OtherSpecify = false;
            CreditDecision1 = false;
            CreditDecision2 = false;
            NoticeDeliveryMethod = 0;
            Equifax = false;
            Experian = false;
            TransUnion = false;
            CreditNumberOfInquiries = false;
        }
    }

    // CreditScoreModel

    public class CreditScoreModel
    {
        public int CreditScoreModelId { get; set; } // CreditScoreModelID (Primary key)
        public string ModelName { get; set; } // ModelName (length: 50)
        public int LowestPossibleScore { get; set; } // LowestPossibleScore
        public int HighestPossibleScore { get; set; } // HighestPossibleScore


        public CreditScoreModel()
        {
            LowestPossibleScore = 0;
            HighestPossibleScore = 0;
        }
    }

    // CreditScoreModelTier

    public class CreditScoreModelTier
    {
        public int CreditScoreModelTierId { get; set; } // CreditScoreModelTierID (Primary key)
        public int CreditScoreModelId { get; set; } // CreditScoreModelID
        public int DisplayOrder { get; set; } // DisplayOrder
        public int LowScore { get; set; } // LowScore
        public int HighScore { get; set; } // HighScore
        public decimal? PercConsumersInTier { get; set; } // PercConsumersInTier


        public CreditScoreModelTier()
        {
            CreditScoreModelId = 0;
            DisplayOrder = 0;
            LowScore = 0;
            HighScore = 0;
        }
    }

    // CustomARDocument

    public class CustomArDocument
    {
        public int CustomArDocumentId { get; set; } // CustomARDocumentID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Name { get; set; } // Name (length: 100)
        public byte[] SerializedArDocumentXml { get; set; } // SerializedARDocumentXML (length: 2147483647)
        public short GroupId { get; set; } // GroupID
        public short PaperSize { get; set; } // PaperSize
        public string FormNo { get; set; } // FormNo (length: 50)
        public string FilterState { get; set; } // FilterState (length: 2)
        public short FilterMortgageType { get; set; } // FilterMortgageType
        public short FilterLoanProductType { get; set; } // FilterLoanProductType
        public short FilterLoanPurposeType { get; set; } // FilterLoanPurposeType
        public bool ShowPageCounter { get; set; } // ShowPageCounter
        public string Description { get; set; } // Description (length: 200)
        public short PageLayout { get; set; } // PageLayout
        public decimal MarginLeft { get; set; } // MarginLeft
        public decimal MarginTop { get; set; } // MarginTop
        public decimal MarginRight { get; set; } // MarginRight
        public decimal MarginBottom { get; set; } // MarginBottom
        public int DesignGridWidth { get; set; } // DesignGridWidth
        public int DesignGridHeight { get; set; } // DesignGridHeight
        public System.Guid? CustomArDocumentGuid { get; set; } // CustomARDocumentGUID
        public bool IsFree { get; set; } // IsFree
        public int FilterOriginationChannel { get; set; } // FilterOriginationChannel


        public CustomArDocument()
        {
            DisplayOrder = 0;
            GroupId = 0;
            PaperSize = 0;
            FilterMortgageType = 0;
            FilterLoanProductType = 0;
            FilterLoanPurposeType = 0;
            ShowPageCounter = false;
            PageLayout = 0;
            MarginLeft = 0m;
            MarginTop = 0m;
            MarginRight = 0m;
            MarginBottom = 0m;
            DesignGridWidth = 0;
            DesignGridHeight = 0;
            CustomArDocumentGuid = System.Guid.NewGuid();
            IsFree = false;
            FilterOriginationChannel = 0;
        }
    }

    // CustomARDocumentField

    public class CustomArDocumentField
    {
        public int CustomArDocumentFieldId { get; set; } // CustomARDocumentFieldID (Primary key)
        public int CustomArDocumentId { get; set; } // CustomARDocumentID
        public string ReportFieldName { get; set; } // ReportFieldName (length: 2147483647)
        public string ControlName { get; set; } // ControlName (length: 100)


        public CustomArDocumentField()
        {
            CustomArDocumentId = 0;
        }
    }

    // CustomARDocumentScreen

    public class CustomArDocumentScreen
    {
        public int CustomArDocumentScreenId { get; set; } // CustomARDocumentScreenID (Primary key)
        public int CustomArDocumentId { get; set; } // CustomARDocumentID
        public string PageClass { get; set; } // PageClass (length: 100)


        public CustomArDocumentScreen()
        {
            CustomArDocumentId = 0;
        }
    }

    // CustomFields

    public class CustomField
    {
        public int CustomFieldsId { get; set; } // CustomFieldsID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public string Field01 { get; set; } // Field01 (length: 2147483647)
        public string Field02 { get; set; } // Field02 (length: 2147483647)
        public string Field03 { get; set; } // Field03 (length: 2147483647)
        public string Field04 { get; set; } // Field04 (length: 2147483647)
        public string Field05 { get; set; } // Field05 (length: 2147483647)
        public string Field06 { get; set; } // Field06 (length: 2147483647)
        public string Field07 { get; set; } // Field07 (length: 2147483647)
        public string Field08 { get; set; } // Field08 (length: 2147483647)
        public string Field09 { get; set; } // Field09 (length: 2147483647)
        public string Field10 { get; set; } // Field10 (length: 2147483647)
        public string Field11 { get; set; } // Field11 (length: 2147483647)
        public string Field12 { get; set; } // Field12 (length: 2147483647)
        public string Field13 { get; set; } // Field13 (length: 2147483647)
        public string Field14 { get; set; } // Field14 (length: 2147483647)
        public string Field15 { get; set; } // Field15 (length: 2147483647)
        public string Field16 { get; set; } // Field16 (length: 2147483647)
        public string Field17 { get; set; } // Field17 (length: 2147483647)
        public string Field18 { get; set; } // Field18 (length: 2147483647)
        public string Field19 { get; set; } // Field19 (length: 2147483647)
        public string Field20 { get; set; } // Field20 (length: 2147483647)
        public string Field21 { get; set; } // Field21 (length: 2147483647)
        public string Field22 { get; set; } // Field22 (length: 2147483647)
        public string Field23 { get; set; } // Field23 (length: 2147483647)
        public string Field24 { get; set; } // Field24 (length: 2147483647)
        public string Field25 { get; set; } // Field25 (length: 2147483647)
        public string Field26 { get; set; } // Field26 (length: 2147483647)
        public string Field27 { get; set; } // Field27 (length: 2147483647)
        public string Field28 { get; set; } // Field28 (length: 2147483647)
        public string Field29 { get; set; } // Field29 (length: 2147483647)
        public string Field30 { get; set; } // Field30 (length: 2147483647)
        public string Field31 { get; set; } // Field31 (length: 2147483647)
        public string Field32 { get; set; } // Field32 (length: 2147483647)
        public string Field33 { get; set; } // Field33 (length: 2147483647)
        public string Field34 { get; set; } // Field34 (length: 2147483647)
        public string Field35 { get; set; } // Field35 (length: 2147483647)
        public string Field36 { get; set; } // Field36 (length: 2147483647)
        public string Field37 { get; set; } // Field37 (length: 2147483647)
        public string Field38 { get; set; } // Field38 (length: 2147483647)
        public string Field39 { get; set; } // Field39 (length: 2147483647)
        public string Field40 { get; set; } // Field40 (length: 2147483647)
        public string Field41 { get; set; } // Field41 (length: 2147483647)
        public string Field42 { get; set; } // Field42 (length: 2147483647)
        public string Field43 { get; set; } // Field43 (length: 2147483647)
        public string Field44 { get; set; } // Field44 (length: 2147483647)
        public string Field45 { get; set; } // Field45 (length: 2147483647)
        public string Field46 { get; set; } // Field46 (length: 2147483647)
        public string Field47 { get; set; } // Field47 (length: 2147483647)
        public string Field48 { get; set; } // Field48 (length: 2147483647)
        public string Field49 { get; set; } // Field49 (length: 2147483647)
        public string Field50 { get; set; } // Field50 (length: 2147483647)
        public string Field51 { get; set; } // Field51 (length: 2147483647)
        public string Field52 { get; set; } // Field52 (length: 2147483647)
        public string Field53 { get; set; } // Field53 (length: 2147483647)
        public string Field54 { get; set; } // Field54 (length: 2147483647)
        public string Field55 { get; set; } // Field55 (length: 2147483647)
        public string Field56 { get; set; } // Field56 (length: 2147483647)
        public string Field57 { get; set; } // Field57 (length: 2147483647)
        public string Field58 { get; set; } // Field58 (length: 2147483647)
        public string Field59 { get; set; } // Field59 (length: 2147483647)
        public string Field60 { get; set; } // Field60 (length: 2147483647)
        public string Field61 { get; set; } // Field61 (length: 2147483647)
        public string Field62 { get; set; } // Field62 (length: 2147483647)
        public string Field63 { get; set; } // Field63 (length: 2147483647)
        public string Field64 { get; set; } // Field64 (length: 2147483647)
        public string Field65 { get; set; } // Field65 (length: 2147483647)
        public string Field66 { get; set; } // Field66 (length: 2147483647)
        public string Field67 { get; set; } // Field67 (length: 2147483647)
        public string Field68 { get; set; } // Field68 (length: 2147483647)
        public string Field69 { get; set; } // Field69 (length: 2147483647)
        public string Field70 { get; set; } // Field70 (length: 2147483647)
        public string Field71 { get; set; } // Field71 (length: 2147483647)
        public string Field72 { get; set; } // Field72 (length: 2147483647)
        public string Field73 { get; set; } // Field73 (length: 2147483647)
        public string Field74 { get; set; } // Field74 (length: 2147483647)
        public string Field75 { get; set; } // Field75 (length: 2147483647)
        public string Field76 { get; set; } // Field76 (length: 2147483647)
        public string Field77 { get; set; } // Field77 (length: 2147483647)
        public string Field78 { get; set; } // Field78 (length: 2147483647)
        public string Field79 { get; set; } // Field79 (length: 2147483647)
        public string Field80 { get; set; } // Field80 (length: 2147483647)
        public string Field81 { get; set; } // Field81 (length: 2147483647)
        public string Field82 { get; set; } // Field82 (length: 2147483647)
        public string Field83 { get; set; } // Field83 (length: 2147483647)
        public string Field84 { get; set; } // Field84 (length: 2147483647)
        public string Field85 { get; set; } // Field85 (length: 2147483647)
        public string Field86 { get; set; } // Field86 (length: 2147483647)
        public string Field87 { get; set; } // Field87 (length: 2147483647)
        public string Field88 { get; set; } // Field88 (length: 2147483647)
        public string Field89 { get; set; } // Field89 (length: 2147483647)
        public string Field90 { get; set; } // Field90 (length: 2147483647)
        public string Field91 { get; set; } // Field91 (length: 2147483647)
        public string Field92 { get; set; } // Field92 (length: 2147483647)
        public string Field93 { get; set; } // Field93 (length: 2147483647)
        public string Field94 { get; set; } // Field94 (length: 2147483647)
        public string Field95 { get; set; } // Field95 (length: 2147483647)
        public string Field96 { get; set; } // Field96 (length: 2147483647)
        public string Field97 { get; set; } // Field97 (length: 2147483647)
        public string Field98 { get; set; } // Field98 (length: 2147483647)
        public string Field99 { get; set; } // Field99 (length: 2147483647)
        public string Field100 { get; set; } // Field100 (length: 2147483647)
        public string Field101 { get; set; } // Field101 (length: 2147483647)
        public string Field102 { get; set; } // Field102 (length: 2147483647)
        public string Field103 { get; set; } // Field103 (length: 2147483647)
        public string Field104 { get; set; } // Field104 (length: 2147483647)
        public string Field105 { get; set; } // Field105 (length: 2147483647)
        public string Field106 { get; set; } // Field106 (length: 2147483647)
        public string Field107 { get; set; } // Field107 (length: 2147483647)
        public string Field108 { get; set; } // Field108 (length: 2147483647)
        public string Field109 { get; set; } // Field109 (length: 2147483647)
        public string Field110 { get; set; } // Field110 (length: 2147483647)
        public string Field111 { get; set; } // Field111 (length: 2147483647)
        public string Field112 { get; set; } // Field112 (length: 2147483647)
        public string Field113 { get; set; } // Field113 (length: 2147483647)
        public string Field114 { get; set; } // Field114 (length: 2147483647)
        public string Field115 { get; set; } // Field115 (length: 2147483647)
        public string Field116 { get; set; } // Field116 (length: 2147483647)
        public string Field117 { get; set; } // Field117 (length: 2147483647)
        public string Field118 { get; set; } // Field118 (length: 2147483647)
        public string Field119 { get; set; } // Field119 (length: 2147483647)
        public string Field120 { get; set; } // Field120 (length: 2147483647)
        public string Field121 { get; set; } // Field121 (length: 2147483647)
        public string Field122 { get; set; } // Field122 (length: 2147483647)
        public string Field123 { get; set; } // Field123 (length: 2147483647)
        public string Field124 { get; set; } // Field124 (length: 2147483647)
        public string Field125 { get; set; } // Field125 (length: 2147483647)
        public string Field126 { get; set; } // Field126 (length: 2147483647)
        public string Field127 { get; set; } // Field127 (length: 2147483647)
        public string Field128 { get; set; } // Field128 (length: 2147483647)
        public string Field129 { get; set; } // Field129 (length: 2147483647)
        public string Field130 { get; set; } // Field130 (length: 2147483647)
        public string Field131 { get; set; } // Field131 (length: 2147483647)
        public string Field132 { get; set; } // Field132 (length: 2147483647)
        public string Field133 { get; set; } // Field133 (length: 2147483647)
        public string Field134 { get; set; } // Field134 (length: 2147483647)
        public string Field135 { get; set; } // Field135 (length: 2147483647)
        public string Field136 { get; set; } // Field136 (length: 2147483647)
        public string Field137 { get; set; } // Field137 (length: 2147483647)
        public string Field138 { get; set; } // Field138 (length: 2147483647)
        public string Field139 { get; set; } // Field139 (length: 2147483647)
        public string Field140 { get; set; } // Field140 (length: 2147483647)
        public string Field141 { get; set; } // Field141 (length: 2147483647)
        public string Field142 { get; set; } // Field142 (length: 2147483647)
        public string Field143 { get; set; } // Field143 (length: 2147483647)
        public string Field144 { get; set; } // Field144 (length: 2147483647)
        public string Field145 { get; set; } // Field145 (length: 2147483647)
        public string Field146 { get; set; } // Field146 (length: 2147483647)
        public string Field147 { get; set; } // Field147 (length: 2147483647)
        public string Field148 { get; set; } // Field148 (length: 2147483647)
        public string Field149 { get; set; } // Field149 (length: 2147483647)
        public string Field150 { get; set; } // Field150 (length: 2147483647)
        public string Field151 { get; set; } // Field151 (length: 2147483647)
        public string Field152 { get; set; } // Field152 (length: 2147483647)
        public string Field153 { get; set; } // Field153 (length: 2147483647)
        public string Field154 { get; set; } // Field154 (length: 2147483647)
        public string Field155 { get; set; } // Field155 (length: 2147483647)
        public string Field156 { get; set; } // Field156 (length: 2147483647)
        public string Field157 { get; set; } // Field157 (length: 2147483647)
        public string Field158 { get; set; } // Field158 (length: 2147483647)
        public string Field159 { get; set; } // Field159 (length: 2147483647)
        public string Field160 { get; set; } // Field160 (length: 2147483647)
        public string Field161 { get; set; } // Field161 (length: 2147483647)
        public string Field162 { get; set; } // Field162 (length: 2147483647)
        public string Field163 { get; set; } // Field163 (length: 2147483647)
        public string Field164 { get; set; } // Field164 (length: 2147483647)
        public string Field165 { get; set; } // Field165 (length: 2147483647)
        public string Field166 { get; set; } // Field166 (length: 2147483647)
        public string Field167 { get; set; } // Field167 (length: 2147483647)
        public string Field168 { get; set; } // Field168 (length: 2147483647)
        public string Field169 { get; set; } // Field169 (length: 2147483647)
        public string Field170 { get; set; } // Field170 (length: 2147483647)
        public string Field171 { get; set; } // Field171 (length: 2147483647)
        public string Field172 { get; set; } // Field172 (length: 2147483647)
        public string Field173 { get; set; } // Field173 (length: 2147483647)
        public string Field174 { get; set; } // Field174 (length: 2147483647)
        public string Field175 { get; set; } // Field175 (length: 2147483647)
        public string Field176 { get; set; } // Field176 (length: 2147483647)
        public string Field177 { get; set; } // Field177 (length: 2147483647)
        public string Field178 { get; set; } // Field178 (length: 2147483647)
        public string Field179 { get; set; } // Field179 (length: 2147483647)
        public string Field180 { get; set; } // Field180 (length: 2147483647)
        public string Field181 { get; set; } // Field181 (length: 2147483647)
        public string Field182 { get; set; } // Field182 (length: 2147483647)
        public string Field183 { get; set; } // Field183 (length: 2147483647)
        public string Field184 { get; set; } // Field184 (length: 2147483647)
        public string Field185 { get; set; } // Field185 (length: 2147483647)
        public string Field186 { get; set; } // Field186 (length: 2147483647)
        public string Field187 { get; set; } // Field187 (length: 2147483647)
        public string Field188 { get; set; } // Field188 (length: 2147483647)
        public string Field189 { get; set; } // Field189 (length: 2147483647)
        public string Field190 { get; set; } // Field190 (length: 2147483647)
        public string Field191 { get; set; } // Field191 (length: 2147483647)
        public string Field192 { get; set; } // Field192 (length: 2147483647)
        public string Field193 { get; set; } // Field193 (length: 2147483647)
        public string Field194 { get; set; } // Field194 (length: 2147483647)
        public string Field195 { get; set; } // Field195 (length: 2147483647)
        public string Field196 { get; set; } // Field196 (length: 2147483647)
        public string Field197 { get; set; } // Field197 (length: 2147483647)
        public string Field198 { get; set; } // Field198 (length: 2147483647)
        public string Field199 { get; set; } // Field199 (length: 2147483647)
        public string Field200 { get; set; } // Field200 (length: 2147483647)
        public string Field201 { get; set; } // Field201 (length: 2147483647)
        public string Field202 { get; set; } // Field202 (length: 2147483647)
        public string Field203 { get; set; } // Field203 (length: 2147483647)
        public string Field204 { get; set; } // Field204 (length: 2147483647)
        public string Field205 { get; set; } // Field205 (length: 2147483647)
        public string Field206 { get; set; } // Field206 (length: 2147483647)
        public string Field207 { get; set; } // Field207 (length: 2147483647)
        public string Field208 { get; set; } // Field208 (length: 2147483647)
        public string Field209 { get; set; } // Field209 (length: 2147483647)
        public string Field210 { get; set; } // Field210 (length: 2147483647)
        public string Field211 { get; set; } // Field211 (length: 2147483647)
        public string Field212 { get; set; } // Field212 (length: 2147483647)
        public string Field213 { get; set; } // Field213 (length: 2147483647)
        public string Field214 { get; set; } // Field214 (length: 2147483647)
        public string Field215 { get; set; } // Field215 (length: 2147483647)
        public string Field216 { get; set; } // Field216 (length: 2147483647)
        public string Field217 { get; set; } // Field217 (length: 2147483647)
        public string Field218 { get; set; } // Field218 (length: 2147483647)
        public string Field219 { get; set; } // Field219 (length: 2147483647)
        public string Field220 { get; set; } // Field220 (length: 2147483647)
        public string Field221 { get; set; } // Field221 (length: 2147483647)
        public string Field222 { get; set; } // Field222 (length: 2147483647)
        public string Field223 { get; set; } // Field223 (length: 2147483647)
        public string Field224 { get; set; } // Field224 (length: 2147483647)
        public string Field225 { get; set; } // Field225 (length: 2147483647)
        public string Field226 { get; set; } // Field226 (length: 2147483647)
        public string Field227 { get; set; } // Field227 (length: 2147483647)
        public string Field228 { get; set; } // Field228 (length: 2147483647)
        public string Field229 { get; set; } // Field229 (length: 2147483647)
        public string Field230 { get; set; } // Field230 (length: 2147483647)
        public string Field231 { get; set; } // Field231 (length: 2147483647)
        public string Field232 { get; set; } // Field232 (length: 2147483647)
        public string Field233 { get; set; } // Field233 (length: 2147483647)
        public string Field234 { get; set; } // Field234 (length: 2147483647)
        public string Field235 { get; set; } // Field235 (length: 2147483647)
        public string Field236 { get; set; } // Field236 (length: 2147483647)
        public string Field237 { get; set; } // Field237 (length: 2147483647)
        public string Field238 { get; set; } // Field238 (length: 2147483647)
        public string Field239 { get; set; } // Field239 (length: 2147483647)
        public string Field240 { get; set; } // Field240 (length: 2147483647)
        public string Field241 { get; set; } // Field241 (length: 2147483647)
        public string Field242 { get; set; } // Field242 (length: 2147483647)
        public string Field243 { get; set; } // Field243 (length: 2147483647)
        public string Field244 { get; set; } // Field244 (length: 2147483647)
        public string Field245 { get; set; } // Field245 (length: 2147483647)
        public string Field246 { get; set; } // Field246 (length: 2147483647)
        public string Field247 { get; set; } // Field247 (length: 2147483647)
        public string Field248 { get; set; } // Field248 (length: 2147483647)
        public string Field249 { get; set; } // Field249 (length: 2147483647)
        public string Field250 { get; set; } // Field250 (length: 2147483647)
        public string Field251 { get; set; } // Field251 (length: 2147483647)
        public string Field252 { get; set; } // Field252 (length: 2147483647)
        public string Field253 { get; set; } // Field253 (length: 2147483647)
        public string Field254 { get; set; } // Field254 (length: 2147483647)
        public string Field255 { get; set; } // Field255 (length: 2147483647)
        public string Field256 { get; set; } // Field256 (length: 2147483647)
        public string Field257 { get; set; } // Field257 (length: 2147483647)
        public string Field258 { get; set; } // Field258 (length: 2147483647)
        public string Field259 { get; set; } // Field259 (length: 2147483647)
        public string Field260 { get; set; } // Field260 (length: 2147483647)
        public string Field261 { get; set; } // Field261 (length: 2147483647)
        public string Field262 { get; set; } // Field262 (length: 2147483647)
        public string Field263 { get; set; } // Field263 (length: 2147483647)
        public string Field264 { get; set; } // Field264 (length: 2147483647)
        public string Field265 { get; set; } // Field265 (length: 2147483647)
        public string Field266 { get; set; } // Field266 (length: 2147483647)
        public string Field267 { get; set; } // Field267 (length: 2147483647)
        public string Field268 { get; set; } // Field268 (length: 2147483647)
        public string Field269 { get; set; } // Field269 (length: 2147483647)
        public string Field270 { get; set; } // Field270 (length: 2147483647)
        public string Field271 { get; set; } // Field271 (length: 2147483647)
        public string Field272 { get; set; } // Field272 (length: 2147483647)
        public string Field273 { get; set; } // Field273 (length: 2147483647)
        public string Field274 { get; set; } // Field274 (length: 2147483647)
        public string Field275 { get; set; } // Field275 (length: 2147483647)
        public string Field276 { get; set; } // Field276 (length: 2147483647)
        public string Field277 { get; set; } // Field277 (length: 2147483647)
        public string Field278 { get; set; } // Field278 (length: 2147483647)
        public string Field279 { get; set; } // Field279 (length: 2147483647)
        public string Field280 { get; set; } // Field280 (length: 2147483647)
        public string Field281 { get; set; } // Field281 (length: 2147483647)
        public string Field282 { get; set; } // Field282 (length: 2147483647)
        public string Field283 { get; set; } // Field283 (length: 2147483647)
        public string Field284 { get; set; } // Field284 (length: 2147483647)
        public string Field285 { get; set; } // Field285 (length: 2147483647)
        public string Field286 { get; set; } // Field286 (length: 2147483647)
        public string Field287 { get; set; } // Field287 (length: 2147483647)
        public string Field288 { get; set; } // Field288 (length: 2147483647)
        public string Field289 { get; set; } // Field289 (length: 2147483647)
        public string Field290 { get; set; } // Field290 (length: 2147483647)
        public string Field291 { get; set; } // Field291 (length: 2147483647)
        public string Field292 { get; set; } // Field292 (length: 2147483647)
        public string Field293 { get; set; } // Field293 (length: 2147483647)
        public string Field294 { get; set; } // Field294 (length: 2147483647)
        public string Field295 { get; set; } // Field295 (length: 2147483647)
        public string Field296 { get; set; } // Field296 (length: 2147483647)
        public string Field297 { get; set; } // Field297 (length: 2147483647)
        public string Field298 { get; set; } // Field298 (length: 2147483647)
        public string Field299 { get; set; } // Field299 (length: 2147483647)
        public string Field300 { get; set; } // Field300 (length: 2147483647)
        public string Field301 { get; set; } // Field301 (length: 2147483647)
        public string Field302 { get; set; } // Field302 (length: 2147483647)
        public string Field303 { get; set; } // Field303 (length: 2147483647)
        public string Field304 { get; set; } // Field304 (length: 2147483647)
        public string Field305 { get; set; } // Field305 (length: 2147483647)
        public string Field306 { get; set; } // Field306 (length: 2147483647)
        public string Field307 { get; set; } // Field307 (length: 2147483647)
        public string Field308 { get; set; } // Field308 (length: 2147483647)
        public string Field309 { get; set; } // Field309 (length: 2147483647)
        public string Field310 { get; set; } // Field310 (length: 2147483647)
        public string Field311 { get; set; } // Field311 (length: 2147483647)
        public string Field312 { get; set; } // Field312 (length: 2147483647)
        public string Field313 { get; set; } // Field313 (length: 2147483647)
        public string Field314 { get; set; } // Field314 (length: 2147483647)
        public string Field315 { get; set; } // Field315 (length: 2147483647)
        public string Field316 { get; set; } // Field316 (length: 2147483647)
        public string Field317 { get; set; } // Field317 (length: 2147483647)
        public string Field318 { get; set; } // Field318 (length: 2147483647)
        public string Field319 { get; set; } // Field319 (length: 2147483647)
        public string Field320 { get; set; } // Field320 (length: 2147483647)
        public string Field321 { get; set; } // Field321 (length: 2147483647)
        public string Field322 { get; set; } // Field322 (length: 2147483647)
        public string Field323 { get; set; } // Field323 (length: 2147483647)
        public string Field324 { get; set; } // Field324 (length: 2147483647)
        public string Field325 { get; set; } // Field325 (length: 2147483647)
        public string Field326 { get; set; } // Field326 (length: 2147483647)
        public string Field327 { get; set; } // Field327 (length: 2147483647)
        public string Field328 { get; set; } // Field328 (length: 2147483647)
        public string Field329 { get; set; } // Field329 (length: 2147483647)
        public string Field330 { get; set; } // Field330 (length: 2147483647)
        public string Field331 { get; set; } // Field331 (length: 2147483647)
        public string Field332 { get; set; } // Field332 (length: 2147483647)
        public string Field333 { get; set; } // Field333 (length: 2147483647)
        public string Field334 { get; set; } // Field334 (length: 2147483647)
        public string Field335 { get; set; } // Field335 (length: 2147483647)
        public string Field336 { get; set; } // Field336 (length: 2147483647)
        public string Field337 { get; set; } // Field337 (length: 2147483647)
        public string Field338 { get; set; } // Field338 (length: 2147483647)
        public string Field339 { get; set; } // Field339 (length: 2147483647)
        public string Field340 { get; set; } // Field340 (length: 2147483647)
        public string Field341 { get; set; } // Field341 (length: 2147483647)
        public string Field342 { get; set; } // Field342 (length: 2147483647)
        public string Field343 { get; set; } // Field343 (length: 2147483647)
        public string Field344 { get; set; } // Field344 (length: 2147483647)
        public string Field345 { get; set; } // Field345 (length: 2147483647)
        public string Field346 { get; set; } // Field346 (length: 2147483647)
        public string Field347 { get; set; } // Field347 (length: 2147483647)
        public string Field348 { get; set; } // Field348 (length: 2147483647)
        public string Field349 { get; set; } // Field349 (length: 2147483647)
        public string Field350 { get; set; } // Field350 (length: 2147483647)
        public string Field351 { get; set; } // Field351 (length: 2147483647)
        public string Field352 { get; set; } // Field352 (length: 2147483647)
        public string Field353 { get; set; } // Field353 (length: 2147483647)
        public string Field354 { get; set; } // Field354 (length: 2147483647)
        public string Field355 { get; set; } // Field355 (length: 2147483647)
        public string Field356 { get; set; } // Field356 (length: 2147483647)
        public string Field357 { get; set; } // Field357 (length: 2147483647)
        public string Field358 { get; set; } // Field358 (length: 2147483647)
        public string Field359 { get; set; } // Field359 (length: 2147483647)
        public string Field360 { get; set; } // Field360 (length: 2147483647)
        public string Field361 { get; set; } // Field361 (length: 2147483647)
        public string Field362 { get; set; } // Field362 (length: 2147483647)
        public string Field363 { get; set; } // Field363 (length: 2147483647)
        public string Field364 { get; set; } // Field364 (length: 2147483647)
        public string Field365 { get; set; } // Field365 (length: 2147483647)
        public string Field366 { get; set; } // Field366 (length: 2147483647)
        public string Field367 { get; set; } // Field367 (length: 2147483647)
        public string Field368 { get; set; } // Field368 (length: 2147483647)
        public string Field369 { get; set; } // Field369 (length: 2147483647)
        public string Field370 { get; set; } // Field370 (length: 2147483647)
        public string Field371 { get; set; } // Field371 (length: 2147483647)
        public string Field372 { get; set; } // Field372 (length: 2147483647)
        public string Field373 { get; set; } // Field373 (length: 2147483647)
        public string Field374 { get; set; } // Field374 (length: 2147483647)
        public string Field375 { get; set; } // Field375 (length: 2147483647)
        public string Field376 { get; set; } // Field376 (length: 2147483647)
        public string Field377 { get; set; } // Field377 (length: 2147483647)
        public string Field378 { get; set; } // Field378 (length: 2147483647)
        public string Field379 { get; set; } // Field379 (length: 2147483647)
        public string Field380 { get; set; } // Field380 (length: 2147483647)
        public string Field381 { get; set; } // Field381 (length: 2147483647)
        public string Field382 { get; set; } // Field382 (length: 2147483647)
        public string Field383 { get; set; } // Field383 (length: 2147483647)
        public string Field384 { get; set; } // Field384 (length: 2147483647)
        public string Field385 { get; set; } // Field385 (length: 2147483647)
        public string Field386 { get; set; } // Field386 (length: 2147483647)
        public string Field387 { get; set; } // Field387 (length: 2147483647)
        public string Field388 { get; set; } // Field388 (length: 2147483647)
        public string Field389 { get; set; } // Field389 (length: 2147483647)
        public string Field390 { get; set; } // Field390 (length: 2147483647)
        public string Field391 { get; set; } // Field391 (length: 2147483647)
        public string Field392 { get; set; } // Field392 (length: 2147483647)
        public string Field393 { get; set; } // Field393 (length: 2147483647)
        public string Field394 { get; set; } // Field394 (length: 2147483647)
        public string Field395 { get; set; } // Field395 (length: 2147483647)
        public string Field396 { get; set; } // Field396 (length: 2147483647)
        public string Field397 { get; set; } // Field397 (length: 2147483647)
        public string Field398 { get; set; } // Field398 (length: 2147483647)
        public string Field399 { get; set; } // Field399 (length: 2147483647)
        public string Field400 { get; set; } // Field400 (length: 2147483647)
        public string Field401 { get; set; } // Field401 (length: 2147483647)
        public string Field402 { get; set; } // Field402 (length: 2147483647)
        public string Field403 { get; set; } // Field403 (length: 2147483647)
        public string Field404 { get; set; } // Field404 (length: 2147483647)
        public string Field405 { get; set; } // Field405 (length: 2147483647)
        public string Field406 { get; set; } // Field406 (length: 2147483647)
        public string Field407 { get; set; } // Field407 (length: 2147483647)
        public string Field408 { get; set; } // Field408 (length: 2147483647)
        public string Field409 { get; set; } // Field409 (length: 2147483647)
        public string Field410 { get; set; } // Field410 (length: 2147483647)
        public string Field411 { get; set; } // Field411 (length: 2147483647)
        public string Field412 { get; set; } // Field412 (length: 2147483647)
        public string Field413 { get; set; } // Field413 (length: 2147483647)
        public string Field414 { get; set; } // Field414 (length: 2147483647)
        public string Field415 { get; set; } // Field415 (length: 2147483647)
        public string Field416 { get; set; } // Field416 (length: 2147483647)
        public string Field417 { get; set; } // Field417 (length: 2147483647)
        public string Field418 { get; set; } // Field418 (length: 2147483647)
        public string Field419 { get; set; } // Field419 (length: 2147483647)
        public string Field420 { get; set; } // Field420 (length: 2147483647)
        public string Field421 { get; set; } // Field421 (length: 2147483647)
        public string Field422 { get; set; } // Field422 (length: 2147483647)
        public string Field423 { get; set; } // Field423 (length: 2147483647)
        public string Field424 { get; set; } // Field424 (length: 2147483647)
        public string Field425 { get; set; } // Field425 (length: 2147483647)
        public string Field426 { get; set; } // Field426 (length: 2147483647)
        public string Field427 { get; set; } // Field427 (length: 2147483647)
        public string Field428 { get; set; } // Field428 (length: 2147483647)
        public string Field429 { get; set; } // Field429 (length: 2147483647)
        public string Field430 { get; set; } // Field430 (length: 2147483647)
        public string Field431 { get; set; } // Field431 (length: 2147483647)
        public string Field432 { get; set; } // Field432 (length: 2147483647)
        public string Field433 { get; set; } // Field433 (length: 2147483647)
        public string Field434 { get; set; } // Field434 (length: 2147483647)
        public string Field435 { get; set; } // Field435 (length: 2147483647)
        public string Field436 { get; set; } // Field436 (length: 2147483647)
        public string Field437 { get; set; } // Field437 (length: 2147483647)
        public string Field438 { get; set; } // Field438 (length: 2147483647)
        public string Field439 { get; set; } // Field439 (length: 2147483647)
        public string Field440 { get; set; } // Field440 (length: 2147483647)
        public string Field441 { get; set; } // Field441 (length: 2147483647)
        public string Field442 { get; set; } // Field442 (length: 2147483647)
        public string Field443 { get; set; } // Field443 (length: 2147483647)
        public string Field444 { get; set; } // Field444 (length: 2147483647)
        public string Field445 { get; set; } // Field445 (length: 2147483647)
        public string Field446 { get; set; } // Field446 (length: 2147483647)
        public string Field447 { get; set; } // Field447 (length: 2147483647)
        public string Field448 { get; set; } // Field448 (length: 2147483647)
        public string Field449 { get; set; } // Field449 (length: 2147483647)
        public string Field450 { get; set; } // Field450 (length: 2147483647)
        public string Field451 { get; set; } // Field451 (length: 2147483647)
        public string Field452 { get; set; } // Field452 (length: 2147483647)
        public string Field453 { get; set; } // Field453 (length: 2147483647)
        public string Field454 { get; set; } // Field454 (length: 2147483647)
        public string Field455 { get; set; } // Field455 (length: 2147483647)
        public string Field456 { get; set; } // Field456 (length: 2147483647)
        public string Field457 { get; set; } // Field457 (length: 2147483647)
        public string Field458 { get; set; } // Field458 (length: 2147483647)
        public string Field459 { get; set; } // Field459 (length: 2147483647)
        public string Field460 { get; set; } // Field460 (length: 2147483647)
        public string Field461 { get; set; } // Field461 (length: 2147483647)
        public string Field462 { get; set; } // Field462 (length: 2147483647)
        public string Field463 { get; set; } // Field463 (length: 2147483647)
        public string Field464 { get; set; } // Field464 (length: 2147483647)
        public string Field465 { get; set; } // Field465 (length: 2147483647)
        public string Field466 { get; set; } // Field466 (length: 2147483647)
        public string Field467 { get; set; } // Field467 (length: 2147483647)
        public string Field468 { get; set; } // Field468 (length: 2147483647)
        public string Field469 { get; set; } // Field469 (length: 2147483647)
        public string Field470 { get; set; } // Field470 (length: 2147483647)
        public string Field471 { get; set; } // Field471 (length: 2147483647)
        public string Field472 { get; set; } // Field472 (length: 2147483647)
        public string Field473 { get; set; } // Field473 (length: 2147483647)
        public string Field474 { get; set; } // Field474 (length: 2147483647)
        public string Field475 { get; set; } // Field475 (length: 2147483647)
        public string Field476 { get; set; } // Field476 (length: 2147483647)
        public string Field477 { get; set; } // Field477 (length: 2147483647)
        public string Field478 { get; set; } // Field478 (length: 2147483647)
        public string Field479 { get; set; } // Field479 (length: 2147483647)
        public string Field480 { get; set; } // Field480 (length: 2147483647)
        public string Field481 { get; set; } // Field481 (length: 2147483647)
        public string Field482 { get; set; } // Field482 (length: 2147483647)
        public string Field483 { get; set; } // Field483 (length: 2147483647)
        public string Field484 { get; set; } // Field484 (length: 2147483647)
        public string Field485 { get; set; } // Field485 (length: 2147483647)
        public string Field486 { get; set; } // Field486 (length: 2147483647)
        public string Field487 { get; set; } // Field487 (length: 2147483647)
        public string Field488 { get; set; } // Field488 (length: 2147483647)
        public string Field489 { get; set; } // Field489 (length: 2147483647)
        public string Field490 { get; set; } // Field490 (length: 2147483647)
        public string Field491 { get; set; } // Field491 (length: 2147483647)
        public string Field492 { get; set; } // Field492 (length: 2147483647)
        public string Field493 { get; set; } // Field493 (length: 2147483647)
        public string Field494 { get; set; } // Field494 (length: 2147483647)
        public string Field495 { get; set; } // Field495 (length: 2147483647)
        public string Field496 { get; set; } // Field496 (length: 2147483647)
        public string Field497 { get; set; } // Field497 (length: 2147483647)
        public string Field498 { get; set; } // Field498 (length: 2147483647)
        public string Field499 { get; set; } // Field499 (length: 2147483647)
        public string Field500 { get; set; } // Field500 (length: 2147483647)


        public CustomField()
        {
            FileDataId = 0;
        }


        public static CustomField Create(LoanApplication loanApplication,
                                         int? byteFileFileDataId=null)
        {
            var customField = new CustomField();
          if (byteFileFileDataId.HasValue)  customField.FileDataId = byteFileFileDataId.Value;
            customField.Field01 = loanApplication.BusinessUnit.Name;
            customField.Field02 = loanApplication.Opportunity.BusinessUnit.LeadSource.Name;
            customField.Field03 = loanApplication.Opportunity.BusinessUnit.LeadSource.Id.ToString();
            customField.Field04 = loanApplication.Opportunity.Id.ToString();
            customField.Field05 = loanApplication.LoanGoal.Name;
            return customField;

        }
        public void Update(LoanApplication loanApplication)
        {

            this.Field01 = loanApplication.BusinessUnit.Name;
            this.Field02 = loanApplication.Opportunity.BusinessUnit.LeadSource.Name;
            this.Field03 = loanApplication.Opportunity.BusinessUnit.LeadSource.Id.ToString();
            this.Field04 = loanApplication.Opportunity.Id.ToString();


        }
    }

    // CustomPage

    public class CustomPage
    {
        public int CustomPageId { get; set; } // CustomPageID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Name { get; set; } // Name (length: 100)
        public byte[] SerializedPageXml { get; set; } // SerializedPageXML (length: 2147483647)
        public short DisplayType { get; set; } // DisplayType
        public string Description { get; set; } // Description (length: 200)
        public int DesignGridWidth { get; set; } // DesignGridWidth
        public int DesignGridHeight { get; set; } // DesignGridHeight
        public int ScreenGroup { get; set; } // ScreenGroup


        public CustomPage()
        {
            DisplayOrder = 0;
            DisplayType = 0;
            DesignGridWidth = 0;
            DesignGridHeight = 0;
            ScreenGroup = 0;
        }
    }

    // CustomPageField

    public class CustomPageField
    {
        public int CustomPageFieldId { get; set; } // CustomPageFieldID (Primary key)
        public int CustomPageId { get; set; } // CustomPageID
        public string ReportFieldName { get; set; } // ReportFieldName (length: 2147483647)
        public string ControlName { get; set; } // ControlName (length: 100)


        public CustomPageField()
        {
            CustomPageId = 0;
        }
    }

    // CustomPageLink

    public class CustomPageLink
    {
        public int CustomPageLinkId { get; set; } // CustomPageLinkID (Primary key)
        public int CustomPageId { get; set; } // CustomPageID
        public int DisplayOrder { get; set; } // DisplayOrder
        public string LinkedPageClassName { get; set; } // LinkedPageClassName (length: 100)
        public short LinkedPageLocation { get; set; } // LinkedPageLocation


        public CustomPageLink()
        {
            CustomPageId = 0;
            DisplayOrder = 0;
            LinkedPageLocation = 0;
        }
    }

    // CustomPagePrintItem

    public class CustomPagePrintItem
    {
        public int CustomPagePrintItemId { get; set; } // CustomPagePrintItemID (Primary key)
        public int CustomPageId { get; set; } // CustomPageID
        public int ReportType { get; set; } // ReportType
        public string ReportName { get; set; } // ReportName (length: 100)
        public System.Guid? ReportGuid { get; set; } // ReportGuid


        public CustomPagePrintItem()
        {
            CustomPageId = 0;
            ReportType = 0;
        }
    }

    // CustomValue

    public class CustomValue
    {
        public int CustomValueId { get; set; } // CustomValueID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public short CustomValueType { get; set; } // CustomValueType
        public string Name { get; set; } // Name (length: 100)
        public string Value { get; set; } // Value (length: 2000)


        public CustomValue()
        {
            FileDataId = 0;
            CustomValueType = 0;
        }
    }

    // CustomValueDef

    public class CustomValueDef
    {
        public int CustomValueDefId { get; set; } // CustomValueDefID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Name { get; set; } // Name (length: 50)
        public string Label { get; set; } // Label (length: 50)
        public short ListType { get; set; } // ListType
        public short ContactCategory { get; set; } // ContactCategory
        public short ContactField { get; set; } // ContactField
        public short DataType { get; set; } // DataType
        public bool IsCalculated { get; set; } // IsCalculated
        public string Formula { get; set; } // Formula (length: 6000)


        public CustomValueDef()
        {
            DisplayOrder = 0;
            ListType = 0;
            ContactCategory = 0;
            ContactField = 0;
            DataType = 0;
            IsCalculated = false;
        }
    }

    // CustomValueUserItem

    public class CustomValueUserItem
    {
        public int CustomValueUserItemId { get; set; } // CustomValueUserItemID (Primary key)
        public int CustomValueDefId { get; set; } // CustomValueDefID
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Text { get; set; } // Text (length: 200)


        public CustomValueUserItem()
        {
            CustomValueDefId = 0;
            DisplayOrder = 0;
        }
    }

    // DataStore

    public class DataStore
    {
        public int DataStoreId { get; set; } // DataStoreID (Primary key)
        public System.DateTime? RevisionDate { get; set; } // RevisionDate
    }

    // Dealer

    public class Dealer
    {
        public int DealerId { get; set; } // DealerID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string DealerCode { get; set; } // DealerCode (length: 50)
        public string DealerName { get; set; } // DealerName (length: 50)
        public string Street { get; set; } // Street (length: 50)
        public string City { get; set; } // City (length: 50)
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 9)
        public string ContactFirstName { get; set; } // ContactFirstName (length: 50)
        public string ContactMiddleName { get; set; } // ContactMiddleName (length: 50)
        public string ContactLastName { get; set; } // ContactLastName (length: 50)
        public string ContactTitle { get; set; } // ContactTitle (length: 50)
        public string ContactPhone { get; set; } // ContactPhone (length: 20)
        public string ContactEmail { get; set; } // ContactEmail (length: 100)
        public string BankName { get; set; } // BankName (length: 50)
        public string AbaNo { get; set; } // ABANo (length: 50)
        public string AccountNo { get; set; } // AccountNo (length: 50)
        public string Notes { get; set; } // Notes (length: 500)


        public Dealer()
        {
            DisplayOrder = 0;
        }
    }

    // Debt

    public class Debt
    {
        public int DebtId { get; set; } // DebtID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int? BorrowerId { get; set; } // BorrowerID
        public int DisplayOrder { get; set; } // DisplayOrder
        public int? Reoid { get; set; } // REOID
        public short DebtType { get; set; } // DebtType
        public string Name { get; set; } // Name (length: 50)
        public string Attn { get; set; } // Attn (length: 50)
        public string Street { get; set; } // Street (length: 50)
        public string City { get; set; } // City (length: 50)
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 9)
        public string AccountNo { get; set; } // AccountNo (length: 50)
        public decimal? MoPayment { get; set; } // MoPayment
        public int? PaymentsLeft { get; set; } // PaymentsLeft
        public string PaymentsLeftTextOv { get; set; } // PaymentsLeftTextOV (length: 50)
        public decimal? UnpaidBal { get; set; } // UnpaidBal
        public bool NotCounted { get; set; } // NotCounted
        public bool ToBePaidOff { get; set; } // ToBePaidOff
        public int? LienPosition { get; set; } // LienPosition
        public bool Resubordinated { get; set; } // Resubordinated
        public bool Omitted { get; set; } // Omitted
        public string Notes { get; set; } // Notes (length: 500)
        public bool IsLienOnSubProp { get; set; } // IsLienOnSubProp
        public decimal? TotalPaymentsOv { get; set; } // TotalPaymentsOV
        public string Fax { get; set; } // Fax (length: 50)
        public short SsOrDilAccepted { get; set; } // SSOrDILAccepted
        public bool ListedOnCreditReport { get; set; } // ListedOnCreditReport
        public string QmatrNotes { get; set; } // QMATRNotes (length: 500)
        public System.Guid? SyncGuid { get; set; } // SyncGuid
        public string OtherDesc { get; set; } // OtherDesc (length: 80)
        public byte MortgageType { get; set; } // MortgageType
        public decimal? HelocCreditLimit { get; set; } // HELOCCreditLimit
        public int AccountHeldByType { get; set; } // AccountHeldByType


        public Debt()
        {
            FileDataId = 0;
            DisplayOrder = 0;
            DebtType = 0;
            NotCounted = false;
            ToBePaidOff = false;
            Resubordinated = false;
            Omitted = false;
            IsLienOnSubProp = false;
            SsOrDilAccepted = 0;
            ListedOnCreditReport = false;
            MortgageType = 0;
            AccountHeldByType = 0;
        }


        public static List<Debt> Create(
                                        ActionModels.LoanFile.Borrower rmBorrower,
                                        int fileDataId,
                                        ThirdPartyCodeList thirdPartyCodeList)
        {
            var debts = new List<Debt>();
            Update(debts, rmBorrower,
              thirdPartyCodeList, fileDataId);

            return debts;
        }


        public static class EncompassConstants // todo change name
        {
            // * Liability Types
            public const int LeasePaymentId = 19; // Home Dues
            public const int InstallemntId = 18; // First Mortgage
            public const int MortgageLoanId = 20; // Second Mortgage
            public const int AlimonyId = 27; // Second Mortgage
        }


        public static void Update(List<Debt> debts,
                                  ActionModels.LoanFile.Borrower rmBorrower,
                                  ThirdPartyCodeList thirdPartyCodeList,
                                  int fileDataId)
        {

            LoanApplication loanApplication = rmBorrower.LoanApplication;

            if(rmBorrower.OwnTypeId == OwnTypeEnum.PrimaryContact.ToInt())
            {
                #region first mortgage

                var loanPurpose = thirdPartyCodeList.GetByteProValue("LoanPurpose", loanApplication.LoanPurposeId);

                if (loanPurpose != LoanPurposeEnum.Purchase.ToString())
                {
                    var firstMortgage = loanApplication.PropertyInfo.MortgageOnProperties.Single(mortgage => mortgage.IsFirstMortgage == true);

                    if (firstMortgage != null && firstMortgage.MonthlyPayment > 0)
                    {
                        var debtIndex = (short)thirdPartyCodeList.GetByteProValue("LiabilityType",
                                                                                   EncompassConstants.InstallemntId).FindEnumIndex(typeof(DebtTypeEnum));

                        debts.Add(new Debt
                        {
                            FileDataId = fileDataId,
                            //Id = "Liability/" + index,
                            //LiabilityIndex = index,
                            DebtType = debtIndex, // Liability Type
                                                  //Date = DateTime.Now, //??
                                                  //AccountIdentifier = "First Mortgage",
                            Name = "First Mortgage",
                            // Owner = borrowerOwner,
                            MoPayment = firstMortgage.MonthlyPayment,
                            UnpaidBal = firstMortgage.MortgageBalance,
                            //CreditLimit = loanDetailDb.FirstMortgageLimit != null ? Convert.ToDouble(loanDetailDb.FirstMortgageLimit) : (double?)null,
                            ToBePaidOff = false,
                            //UCDPayoffType = "FirstPositionMortgageLien", // Balance ??

                        });

                    }
                }

                #endregion


                #region second mortgage

                var secondMortgages = loanApplication.PropertyInfo.MortgageOnProperties.Where(mortgage => mortgage.IsFirstMortgage == false).ToList();// todo clearfication required
                foreach (var secondMortgage in secondMortgages)
                {
                    if (secondMortgage?.MonthlyPayment != null && secondMortgage?.MonthlyPayment > 0)
                    {
                        var debtIndex = (short)thirdPartyCodeList.GetByteProValue("LiabilityType",
                                                                                  EncompassConstants.MortgageLoanId).FindEnumIndex(typeof(DebtTypeEnum));

                        debts.Add(new Debt
                        {
                            FileDataId = fileDataId,
                            //Id = "Liability/" + index,
                            //LiabilityIndex = index,
                            DebtType = debtIndex, // Liability Type
                                                  //Date = DateTime.Now, //??
                                                  //AccountIdentifier = "Second Mortgage",
                            Name = "Second Mortgage",
                            //Owner = borrowerOwner,
                            MoPayment = secondMortgage.MonthlyPayment,
                            UnpaidBal = secondMortgage.MortgageBalance,
                            //CreditLimit = loanDetailDb.SecondMortgageLimit != null ? Convert.ToDouble(loanDetailDb.SecondMortgageLimit) : (double?)null,
                            ToBePaidOff = false,
                            //UCDPayoffType = "SecondPositionMortgageLien", // Balance ??

                        });
                    }
                }



                #endregion
            }



            #region alimony liabilities

            var rmBorrowerLiabilities = rmBorrower.BorrowerLiabilities.ToList();

            if (rmBorrowerLiabilities != null)
            {
                foreach (var rmBorrowerALimonyLiability in rmBorrowerLiabilities)
                {

                    var debtIndex = (short)thirdPartyCodeList.GetByteProValue("LiabilityType",
                                                                              rmBorrowerALimonyLiability.LiabilityTypeId).FindEnumIndex(typeof(DebtTypeEnum));

                    if (debtIndex == 0)
                    {
                        debtIndex = 9;//Other
                    }

                    debts.Add(new Debt
                    {
                        FileDataId = fileDataId,
                        //Id = "Liability/" + index,
                        //LiabilityIndex = index,
                        DebtType = debtIndex, // Liability Type
                                              //Date = DateTime.Now, //??
                        Name = rmBorrowerALimonyLiability.CompanyName,
                        OtherDesc = rmBorrowerALimonyLiability.CompanyName,
                        AccountNo = rmBorrowerALimonyLiability.AccountNumber,
                        //Owner = borrowerOwner,
                        MoPayment = rmBorrowerALimonyLiability.MonthlyPayment,
                        UnpaidBal = rmBorrowerALimonyLiability.MonthlyPayment * rmBorrowerALimonyLiability.RemainingMonth,
                        PaymentsLeft = rmBorrowerALimonyLiability.RemainingMonth,
                        ToBePaidOff = rmBorrowerALimonyLiability.WillBePaidByThisLoan ?? false
                        //UCDPayoffType = "Other", // Balance ??
                    }) ;
                }


            }
            #endregion






        }
    }


    // DisclosureLogEntry

    public class DisclosureLogEntry
    {
        public int DisclosureLogEntryId { get; set; } // DisclosureLogEntryID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int Form { get; set; } // Form
        public System.DateTime? DeliveryDate { get; set; } // DeliveryDate
        public int DeliveryMethod { get; set; } // DeliveryMethod
        public System.DateTime? ReceivedDate { get; set; } // ReceivedDate
        public decimal? Apr { get; set; } // APR
        public string Product { get; set; } // Product (length: 80)
        public decimal? FinanceCharge { get; set; } // FinanceCharge
        public decimal? PurPrice { get; set; } // PurPrice
        public decimal? LoanAmount { get; set; } // LoanAmount
        public decimal? IntRate { get; set; } // IntRate
        public decimal? TotalClosingCostsUnrounded { get; set; } // TotalClosingCostsUnrounded
        public decimal? TotalClosingCostsRounded { get; set; } // TotalClosingCostsRounded
        public decimal? ClosingCostsFinanced { get; set; } // ClosingCostsFinanced
        public decimal? FundsFromBor { get; set; } // FundsFromBor
        public decimal? Deposit { get; set; } // Deposit
        public decimal? FundsForBor { get; set; } // FundsForBor
        public decimal? SellerCredits { get; set; } // SellerCredits
        public decimal? AdjustmentAndOtherCredits { get; set; } // AdjustmentAndOtherCredits
        public decimal? PayoffsAndPayments { get; set; } // PayoffsAndPayments
        public decimal? LenderCredits { get; set; } // LenderCredits
        public bool AlternativeCashToCloseTableUsed { get; set; } // AlternativeCashToCloseTableUsed
        public decimal? TotalLoanCostsUnrounded { get; set; } // TotalLoanCostsUnrounded
        public decimal? TotalOtherCostsUnrounded { get; set; } // TotalOtherCostsUnrounded
        public int DisclosureRevisionType { get; set; } // DisclosureRevisionType
        public System.DateTime? DeliveryDateAndTime { get; set; } // DeliveryDateAndTime
        public string UserName { get; set; } // UserName (length: 50)
        public System.DateTime? ClosingCostsExpirationDate { get; set; } // ClosingCostsExpirationDate
        public string ClosingCostsExpirationTimeOfDay { get; set; } // ClosingCostsExpirationTimeOfDay (length: 20)
        public System.DateTime? LockExpirationDate { get; set; } // LockExpirationDate
        public string LockExpirationTimeOfDay { get; set; } // LockExpirationTimeOfDay (length: 20)
        public int HasPrepaymentPenalty { get; set; } // HasPrepaymentPenalty
        public int? DocPackageId { get; set; } // DocPackageID
        public decimal? ClosingCostsFinancedRounded { get; set; } // ClosingCostsFinancedRounded
        public decimal? OriginationCharges { get; set; } // OriginationCharges
        public decimal? DiscountPoints { get; set; } // DiscountPoints
        public System.DateTime? IssuedDate { get; set; } // IssuedDate
        public decimal? FundsFromBorRounded { get; set; } // FundsFromBorRounded
        public decimal? FundsForBorRounded { get; set; } // FundsForBorRounded
        public string LoanProgramName { get; set; } // LoanProgramName (length: 50)
        public string LoanProgramCode { get; set; } // LoanProgramCode (length: 25)

        public DisclosureLogEntry()
        {
            FileDataId = 0;
            Form = 0;
            DeliveryMethod = 0;
            AlternativeCashToCloseTableUsed = false;
            DisclosureRevisionType = 0;
            HasPrepaymentPenalty = 0;
        }
    }

    // DocPackage

    public class DocPackage
    {
        public int DocPackageId { get; set; } // DocPackageID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int VendorType { get; set; } // VendorType
        public string VendorPackageId { get; set; } // VendorPackageID (length: 50)
        public int Status { get; set; } // Status
        public System.DateTime CreatedDateTime { get; set; } // CreatedDateTime
        public System.DateTime? ExpirationDateTime { get; set; } // ExpirationDateTime
        public System.DateTime? CancelledDateTime { get; set; } // CancelledDateTime
        public System.DateTime? ComplianceDeliveryDeadline { get; set; } // ComplianceDeliveryDeadline
        public System.DateTime? EDeliveryDeadline { get; set; } // EDeliveryDeadline
        public int PackageContents { get; set; } // PackageContents
        public string PackageName { get; set; } // PackageName (length: 100)
        public string PackageTypeName { get; set; } // PackageTypeName (length: 50)
        public int DeliveryMethod { get; set; } // DeliveryMethod
        public bool DeliveryNotRequired { get; set; } // DeliveryNotRequired
        public System.DateTime? DeliveryDate { get; set; } // _DeliveryDate
        public System.DateTime? ReceivedDate { get; set; } // _ReceivedDate
        public string DocumentTypeCode { get; set; } // DocumentTypeCode (length: 20)
        public string SenderUserName { get; set; } // SenderUserName (length: 50)

        public DocPackage()
        {
            FileDataId = 0;
            VendorType = 0;
            Status = 0;
            PackageContents = 0;
            DeliveryMethod = 0;
            DeliveryNotRequired = false;
        }
    }

    // DocSigner

    public class DocSigner
    {
        public int DocSignerId { get; set; } // DocSignerID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int DocPackageId { get; set; } // DocPackageID
        public int? BorrowerId { get; set; } // BorrowerID
        public string FullName { get; set; } // FullName (length: 150)
        public string EMail { get; set; } // EMail (length: 100)
        public System.DateTime? ConsentedDateTime { get; set; } // ConsentedDateTime
        public System.DateTime? SignedDateTime { get; set; } // SignedDateTime
        public System.DateTime? HardCopyMailedDateTime { get; set; } // HardCopyMailedDateTime
        public string VendorSignerId { get; set; } // VendorSignerID (length: 50)
        public System.DateTime? ConsentRevokedDateTime { get; set; } // ConsentRevokedDateTime
        public System.DateTime? HardCopyReceivedDate { get; set; } // HardCopyReceivedDate
        public int PartyType { get; set; } // PartyType
        public int? AdditionalPartyId { get; set; } // AdditionalPartyID
        public bool ConsentPreviouslyObtained { get; set; } // ConsentPreviouslyObtained
        public bool IsLockedOut { get; set; } // IsLockedOut
        public bool InvitationEmailBounced { get; set; } // InvitationEmailBounced

        public DocSigner()
        {
            FileDataId = 0;
            DocPackageId = 0;
            PartyType = 0;
            ConsentPreviouslyObtained = false;
            IsLockedOut = false;
            InvitationEmailBounced = false;
        }
    }

    // DocumentCategory

    public class DocumentCategory
    {
        public int DocumentCategoryId { get; set; } // DocumentCategoryID (Primary key)
        public string DocumentCategoryCode { get; set; } // DocumentCategoryCode (length: 30)
        public int DisplayOrder { get; set; } // DisplayOrder

        public DocumentCategory()
        {
            DisplayOrder = 0;
        }
    }

    // DocumentProperty

    public class DocumentProperty
    {
        public int DocumentPropertyId { get; set; } // DocumentPropertyID (Primary key)
        public int ReportType { get; set; } // ReportType
        public string DocumentName { get; set; } // DocumentName (length: 100)
        public bool OmitLogo { get; set; } // OmitLogo
        public bool OmitLicenseInfo { get; set; } // OmitLicenseInfo
        public System.Guid? ReportGuid { get; set; } // ReportGUID
        public bool OmitFromStoredDocs { get; set; } // OmitFromStoredDocs
        public bool IncludeInStoredDocs { get; set; } // IncludeInStoredDocs
        public int PageNumber { get; set; } // PageNumber
        public int PageCount { get; set; } // PageCount
        public string DocumentTypeCode { get; set; } // DocumentTypeCode (length: 20)
        public bool OmitBarcode { get; set; } // OmitBarcode
        public int DocumentInfoBarLocationOv { get; set; } // DocumentInfoBarLocationOV
        public bool AllowSandboxPrinting { get; set; } // AllowSandboxPrinting

        public DocumentProperty()
        {
            ReportType = 0;
            OmitLogo = false;
            OmitLicenseInfo = false;
            OmitFromStoredDocs = false;
            IncludeInStoredDocs = false;
            PageNumber = 0;
            PageCount = 0;
            OmitBarcode = false;
            DocumentInfoBarLocationOv = 0;
            AllowSandboxPrinting = false;
        }
    }

    // DocumentStack

    public class DocumentStack
    {
        public int DocumentStackId { get; set; } // DocumentStackID (Primary key)
        public string Name { get; set; } // Name (length: 50)
        public int DisplayOrder { get; set; } // DisplayOrder
        public int StackingDocumentStatuses2 { get; set; } // StackingDocumentStatuses2

        public DocumentStack()
        {
            DisplayOrder = 0;
            StackingDocumentStatuses2 = 0;
        }
    }

    // DocumentStackDocumentType

    public class DocumentStackDocumentType
    {
        public int DocumentStackDocumentTypeId { get; set; } // DocumentStackDocumentTypeID (Primary key)
        public int DocumentStackId { get; set; } // DocumentStackID
        public int DisplayOrder { get; set; } // DisplayOrder
        public string DocumentTypeCode { get; set; } // DocumentTypeCode (length: 20)
        public int DocumentCategoryId { get; set; } // DocumentCategoryID
        public int DocumentStackItemType { get; set; } // DocumentStackItemType
        public string OutputFileName { get; set; } // OutputFileName (length: 100)

        public DocumentStackDocumentType()
        {
            DocumentStackId = 0;
            DisplayOrder = 0;
            DocumentCategoryId = 0;
            DocumentStackItemType = 0;
        }
    }

    // DocumentType

    public class DocumentType
    {
        public int DocumentTypeId { get; set; } // DocumentTypeID (Primary key)
        public int DocumentCategoryId { get; set; } // DocumentCategoryID
        public string Name { get; set; } // Name (length: 100)
        public int MismoDocumentType { get; set; } // MISMODocumentType
        public int MismoDocumentSubtype { get; set; } // MISMODocumentSubtype
        public bool Inactive { get; set; } // Inactive
        public string DocumentTypeCode { get; set; } // DocumentTypeCode (length: 20)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string DocPrepCodeOv { get; set; } // DocPrepCodeOV (length: 200)
        public int IndexingType { get; set; } // IndexingType
        public bool AllowOnlyOneActiveDoc { get; set; } // AllowOnlyOneActiveDoc
        public int ImageOptimizationLevel { get; set; } // ImageOptimizationLevel
        public string DocPrepCode2Ov { get; set; } // DocPrepCode2OV (length: 200)

        public DocumentType()
        {
            DocumentCategoryId = 0;
            MismoDocumentType = 0;
            MismoDocumentSubtype = 0;
            Inactive = false;
            DisplayOrder = 0;
            IndexingType = 0;
            AllowOnlyOneActiveDoc = false;
            ImageOptimizationLevel = 0;
        }
    }

    // DOT

    public class Dot
    {
        public int Dotid { get; set; } // DOTID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public decimal? RefiDebtsToBePaidOffOv { get; set; } // RefiDebtsToBePaidOffOV
        public decimal? EstPrepaidsOv { get; set; } // EstPrepaidsOV
        public decimal? EstClosingCostsOv { get; set; } // EstClosingCostsOV
        public decimal? MipffTotalOv { get; set; } // MIPFFTotalOV
        public decimal? DiscountOv { get; set; } // DiscountOV
        public decimal? TotalCostsOv { get; set; } // TotalCostsOV
        public decimal? ClosingCostsPaidBySellerOv { get; set; } // ClosingCostsPaidBySellerOV
        public decimal? CashFromToBorrowerOv { get; set; } // CashFromToBorrowerOV
        public decimal? AppDepositAmount { get; set; } // AppDepositAmount
        public string AppDepositHeldBy { get; set; } // AppDepositHeldBy (length: 50)
        public decimal? EarnestMoneyAmount { get; set; } // EarnestMoneyAmount
        public string EarnestMoneyHeldBy { get; set; } // EarnestMoneyHeldBy (length: 50)
        public bool ExcludeSubFi { get; set; } // ExcludeSubFi
        public decimal? OtherCreditAmount5 { get; set; } // _OtherCreditAmount5
        public string OtherCreditDesc5 { get; set; } // _OtherCreditDesc5 (length: 50)
        public string OtherCreditDesc6 { get; set; } // _OtherCreditDesc6 (length: 50)
        public decimal? OtherCreditAmount6 { get; set; } // _OtherCreditAmount6
        public decimal? LineA { get; set; } // _LineA
        public decimal? LineB { get; set; } // _LineB
        public decimal? LineC { get; set; } // _LineC
        public decimal? LineD { get; set; } // _LineD
        public decimal? LineE { get; set; } // _LineE
        public decimal? LineF { get; set; } // _LineF
        public decimal? LineG { get; set; } // _LineG
        public decimal? LineH { get; set; } // _LineH
        public decimal? LineI { get; set; } // _LineI
        public decimal? LineJ { get; set; } // _LineJ
        public decimal? LineK { get; set; } // _LineK
        public decimal? LineL1 { get; set; } // _LineL1
        public decimal? LineL2 { get; set; } // _LineL2
        public decimal? LineL3 { get; set; } // _LineL3
        public decimal? LineL4 { get; set; } // _LineL4
        public decimal? LineM { get; set; } // _LineM
        public decimal? LineN { get; set; } // _LineN
        public decimal? LineO { get; set; } // _LineO
        public decimal? LineP { get; set; } // _LineP
        public decimal? CashFromToBorrower { get; set; } // _CashFromToBorrower
        public decimal? LeCashToCloseAdjustmentAmount { get; set; } // LECashToCloseAdjustmentAmount
        public string LeCashToCloseAdjustmentDesc { get; set; } // LECashToCloseAdjustmentDesc (length: 50)
        public decimal? RefiSubPropLoansToBePaidOffOv { get; set; } // RefiSubPropLoansToBePaidOffOV
        public decimal? OtherDebtsToBePaidOffOv { get; set; } // OtherDebtsToBePaidOffOV
        public decimal? BorrowerClosingCostsOv { get; set; } // BorrowerClosingCostsOV
        public decimal? DiscountPointsOv { get; set; } // DiscountPointsOV

        public Dot()
        {
            FileDataId = 0;
            ExcludeSubFi = false;
        }
    }

    // EmbeddedDoc

    //public class EmbeddedDoc
    //{
    //    public int EmbeddedDocId { get; set; } // EmbeddedDocID (Primary key)
    //    public int FileDataId { get; set; } // FileDataID
    //    public int EmbeddedDocType { get; set; } // EmbeddedDocType
    //    public System.DateTime DateCreated { get; set; } // DateCreated
    //    public string Description { get; set; } // Description (length: 200)
    //    public string FileExtension { get; set; } // FileExtension (length: 10)
    //    public byte[] Data { get; set; } // Data (length: 2147483647)
    //    public string ReportType { get; set; } // ReportType (length: 25)
    //    public string NameOnReport { get; set; } // NameOnReport (length: 100)
    //    public bool Viewable { get; set; } // Viewable
    //    public string Ssn { get; set; } // ssn (length: 50)
    //    public string ThirdPartyId { get; set; } // ThirdPartyID (length: 50)
    //    public System.Guid? Guid { get; set; } // GUID
    //    public int DocStorageMethod { get; set; } // DocStorageMethod
    //    public int? NeededItemId { get; set; } // NeededItemID
    //    public short DocStorageSource { get; set; } // DocStorageSource
    //    public int DisplayOrder { get; set; } // DisplayOrder
    //    public int DocReportType { get; set; } // DocReportType
    //    public System.Guid? DocReportGuid { get; set; } // DocReportGUID
    //    public int? ConditionId { get; set; } // ConditionID
    //    public string DocumentTypeCode { get; set; } // DocumentTypeCode (length: 20)
    //    public string DocumentCategoryCode { get; set; } // DocumentCategoryCode (length: 30)
    //    public byte[] MetaData { get; set; } // MetaData (length: 2147483647)
    //    public bool Internal { get; set; } // Internal
    //    public int Status { get; set; } // Status
    //    public bool Outdated { get; set; } // Outdated
    //    public System.DateTime? ExpirationDate { get; set; } // ExpirationDate
    //    public int? DocPackageId { get; set; } // DocPackageID
    //    public string VersionInfo { get; set; } // VersionInfo (length: 50)
    //    public bool IsAutoIndexingRequired { get; set; } // IsAutoIndexingRequired

    //    public EmbeddedDoc()
    //    {
    //        FileDataId = 0;
    //        EmbeddedDocType = 0;
    //        Viewable = false;
    //        Guid = System.Guid.NewGuid();
    //        DocStorageMethod = 0;
    //        DocStorageSource = 0;
    //        DisplayOrder = 0;
    //        DocReportType = 0;
    //        Internal = false;
    //        Status = 0;
    //        Outdated = false;
    //        IsAutoIndexingRequired = false;
    //    }
    //}

    // Employer

    public class Employer
    {
        public int EmployerId { get; set; } // EmployerID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int? BorrowerId { get; set; } // BorrowerID
        public int DisplayOrder { get; set; } // DisplayOrder
        public short Status { get; set; } // Status
        public string Name { get; set; } // Name (length: 50)
        public string Attn { get; set; } // Attn (length: 50)
        public string Street { get; set; } // Street (length: 50)
        public string City { get; set; } // City (length: 50)
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 9)
        public bool SelfEmp { get; set; } // SelfEmp
        public string Position { get; set; } // Position (length: 50)
        public string TypeBus { get; set; } // TypeBus (length: 50)
        public string Phone { get; set; } // Phone (length: 20)
        public System.DateTime? DateFrom { get; set; } // DateFrom
        public System.DateTime? DateTo { get; set; } // DateTo
        public decimal? YearsOnJob { get; set; } // YearsOnJob
        public decimal? YearsInProf { get; set; } // YearsInProf
        public decimal? MoIncome { get; set; } // MoIncome
        public string Notes { get; set; } // Notes (length: 500)
        public string Fax { get; set; } // Fax (length: 50)
        public System.Guid? SyncGuid { get; set; } // SyncGuid
        public string VoeVendorId { get; set; } // VOEVendorID (length: 50)
        public string VoeSalaryId { get; set; } // VOESalaryID (length: 50)
        public int? TimeInLineOfWorkYears { get; set; } // TimeInLineOfWorkYears
        public int? TimeInLineOfWorkMonths { get; set; } // TimeInLineOfWorkMonths
        public int IsSpecialRelationship { get; set; } // IsSpecialRelationship
        public int OwnershipInterest { get; set; } // OwnershipInterest
        public bool IsForeignEmployment { get; set; } // IsForeignEmployment
        public bool IsSeasonalEmployment { get; set; } // IsSeasonalEmployment
        public int StreetContainsUnitNumberOv { get; set; } // StreetContainsUnitNumberOV
        public string Country { get; set; } // Country (length: 50)
        public string VoeEmployeeId { get; set; } // VOEEmployeeID (length: 50)
        public int RmEmploymentInfoid { get; set; }


        public Employer()
        {
            FileDataId = 0;
            DisplayOrder = 0;
            Status = 0;
            SelfEmp = false;
            IsSpecialRelationship = 0;
            OwnershipInterest = 0;
            IsForeignEmployment = false;
            IsSeasonalEmployment = false;
            StreetContainsUnitNumberOv = 0;
        }


        public static List<Employer> Create(ActionModels.LoanFile.Borrower rmBorrower,
                                            List<Employer> byteEmployers,
                                            int fileDataId = 0)
        {

            

            var currentEmploymentInfos = rmBorrower.EmploymentInfoes.Where(ei => ei.EndDate == null).OrderByDescending(ei => ei.StartDate).ToList();
            var previousEmploymentInfos = rmBorrower.EmploymentInfoes.Where(ei => ei.EndDate != null).ToList();

            for (var index = 0; index < currentEmploymentInfos.Count; index++)
            {
                var employmentInfo = currentEmploymentInfos[index];

                Employer employer;
                if (index==0)
                    employer = byteEmployers[index];
                else
                {
                    employer = new Employer();
                    byteEmployers.Add(employer);
                }

                employer.FileDataId = fileDataId;
                employer.City = employmentInfo.AddressInfo?.CityName;
                employer.Zip = employmentInfo.AddressInfo?.ZipCode;
                employer.State = employmentInfo.AddressInfo.State?.Abbreviation;
                employer.Street = $"{employmentInfo.AddressInfo?.StreetAddress} {employmentInfo.AddressInfo?.UnitNo}".Trim();
                employer.MoIncome = employmentInfo.MonthlyBaseIncome;
                employer.Name = employmentInfo.Name;
                employer.Phone = employmentInfo.Phone;
                employer.Position = employmentInfo.JobTitle;
                employer.DateFrom = employmentInfo.StartDate;

                var toDate = employmentInfo.EndDate ?? rmBorrower.LoanApplication?.CreatedOnUtc ?? DateTime.UtcNow;

                employer.YearsOnJob = (decimal?) Math.Abs((employmentInfo.StartDate.Value - toDate).TotalDays / 365);
                employer.SelfEmp = employmentInfo.IsSelfEmployed ?? false;
                employer.OwnershipInterest = employmentInfo.OwnershipPercentage != null ? (employmentInfo.OwnershipPercentage >= 25 ? 1 : 2) : 0;
                employer.IsSeasonalEmployment = (int) JobTypeEnum.Seasonal == (employmentInfo.JobTypeId ?? -1);
                employer.IsSpecialRelationship = (employmentInfo.IsEmployedByPartyInTransaction ?? false) ? 1 : 0;
                employer.RmEmploymentInfoid = employmentInfo.Id;
                employer.Status = (short)(index == 0 ? 2 : 1);
            }

            for (var index = 0; index < previousEmploymentInfos.Count; index++)
            {
                
                var employmentInfo = previousEmploymentInfos[index];
                var employer = new Employer();
                byteEmployers.Add(employer);

                var toDate = employmentInfo.EndDate ?? rmBorrower.LoanApplication?.CreatedOnUtc ?? DateTime.UtcNow;
                int ownership = employmentInfo.OwnershipPercentage != null ? (employmentInfo.OwnershipPercentage >= 25 ? 1 : 2) : 0;

                employer.FileDataId = fileDataId;
                employer.City = employmentInfo.AddressInfo?.CityName;
                employer.Zip = employmentInfo.AddressInfo?.ZipCode;
                employer.State = employmentInfo.AddressInfo?.State?.Abbreviation;
                employer.Street = $"{employmentInfo.AddressInfo?.StreetAddress} {employmentInfo.AddressInfo?.UnitNo}".Trim();
                employer.MoIncome = employmentInfo.MonthlyBaseIncome;
                employer.Name = employmentInfo.Name;
                employer.Phone = employmentInfo.Phone;
                employer.Position = employmentInfo.JobTitle;
                employer.YearsOnJob = (decimal?) Math.Abs((employmentInfo.StartDate.Value - toDate).TotalDays / 365);
                employer.Status = 0; // Based on Condition;
                employer.DateFrom = employmentInfo.StartDate;
                employer.DateTo = employmentInfo.EndDate;
                employer.SelfEmp = employmentInfo.IsSelfEmployed ?? false;
                employer.OwnershipInterest = ownership;
                employer.RmEmploymentInfoid = employmentInfo.Id;
                employer.Status = 0;

            }

            return byteEmployers;
        }


      
    }

    

    // ErnstEndorsement

    public class ErnstEndorsement
    {
        public int EndorsementId { get; set; } // EndorsementID (Primary key)
        public string EndorsementCode { get; set; } // EndorsementCode (length: 10)
        public string EndorsementName { get; set; } // EndorsementName (length: 100)
        public bool EndorsementSelected { get; set; } // EndorsementSelected

        public ErnstEndorsement()
        {
            EndorsementSelected = false;
        }
    }

    // ErnstFee

    public class ErnstFee
    {
        public int ErnstFeeId { get; set; } // ErnstFeeID (Primary key)
        public short HudLineNo { get; set; } // HUDLineNo
        public string ErnstFeeCode { get; set; } // ErnstFeeCode (length: 50)
        public string ErnstFeeName { get; set; } // ErnstFeeName (length: 100)
        public short EnrstFeeType { get; set; } // EnrstFeeType

        public ErnstFee()
        {
            HudLineNo = 0;
            EnrstFeeType = 0;
        }
    }

    // ErnstQuestion

    public class ErnstQuestion
    {
        public int QuestionId { get; set; } // QuestionID (Primary key)
        public string StateCode { get; set; } // StateCode (length: 2)
        public string QuestionNode { get; set; } // QuestionNode (length: 4)
        public int AnswerType { get; set; } // AnswerType
        public string DefaultFormula { get; set; } // DefaultFormula (length: 6000)
        public bool RequestTypeDeed { get; set; } // RequestTypeDeed
        public bool ReqeustTypeMort { get; set; } // ReqeustTypeMort
        public bool RequestTypeMortRefi { get; set; } // RequestTypeMortRefi
        public bool RequestTypeMod { get; set; } // RequestTypeMod
        public bool RequestTypeAssn { get; set; } // RequestTypeAssn
        public bool RequestTypeRel { get; set; } // RequestTypeRel
        public bool ReqeustTypeDeedAmend { get; set; } // ReqeustTypeDeedAmend
        public bool RequestTypeSubord { get; set; } // RequestTypeSubord
        public string Question { get; set; } // Question (length: 500)
        public string QuestionDescription { get; set; } // QuestionDescription (length: 2000)
        public string OverrideFormula { get; set; } // OverrideFormula (length: 6000)

        public ErnstQuestion()
        {
            AnswerType = 0;
            RequestTypeDeed = false;
            ReqeustTypeMort = false;
            RequestTypeMortRefi = false;
            RequestTypeMod = false;
            RequestTypeAssn = false;
            RequestTypeRel = false;
            ReqeustTypeDeedAmend = false;
            RequestTypeSubord = false;
        }
    }

    // Expense

    public class Expense
    {
        public int ExpenseId { get; set; } // ExpenseID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int BorrowerId { get; set; } // BorrowerID
        public int ExpenseType { get; set; } // ExpenseType
        public string OtherDesc { get; set; } // OtherDesc (length: 80)
        public decimal? MoPayment { get; set; } // MoPayment
        public int? PaymentsLeft { get; set; } // PaymentsLeft
        public string QmatrNotes { get; set; } // QMATRNotes (length: 500)
        public string Notes { get; set; } // Notes (length: 500)
        public int AccountHeldByType { get; set; } // AccountHeldByType

        public Expense()
        {
            FileDataId = 0;
            BorrowerId = 0;
            ExpenseType = 0;
            AccountHeldByType = 0;
        }
    }

    // ExtendedBooleanValue

    public class ExtendedBooleanValue
    {
        public int ExtendedBooleanValueId { get; set; } // ExtendedBooleanValueID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public string Name { get; set; } // Name (length: 50)
        public bool Value { get; set; } // Value

        public ExtendedBooleanValue()
        {
            FileDataId = 0;
            Value = false;
        }
    }

    // ExtendedDateValue

    public class ExtendedDateValue
    {
        public int ExtendedDateValueId { get; set; } // ExtendedDateValueID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public string Name { get; set; } // Name (length: 50)
        public System.DateTime? Value { get; set; } // Value

        public ExtendedDateValue()
        {
            FileDataId = 0;
        }
    }

    // ExtendedDecimalValue

    public class ExtendedDecimalValue
    {
        public int ExtendedDecimalValueId { get; set; } // ExtendedDecimalValueID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public string Name { get; set; } // Name (length: 50)
        public decimal? Value { get; set; } // Value

        public ExtendedDecimalValue()
        {
            FileDataId = 0;
        }
    }

    // ExtendedFieldDef

    public class ExtendedFieldDef
    {
        public int ExtendedFieldDefId { get; set; } // ExtendedFieldDefID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Name { get; set; } // Name (length: 50)
        public string Label { get; set; } // Label (length: 200)
        public short ListType { get; set; } // ListType
        public short ContactCategory { get; set; } // ContactCategory
        public short ContactField { get; set; } // ContactField
        public short DataType { get; set; } // DataType
        public bool IsCalculated { get; set; } // IsCalculated
        public string Formula { get; set; } // Formula (length: 6000)
        public bool MultiLine { get; set; } // MultiLine
        public int Decimals { get; set; } // Decimals
        public int MaxTextLength { get; set; } // MaxTextLength

        public ExtendedFieldDef()
        {
            DisplayOrder = 0;
            ListType = 0;
            ContactCategory = 0;
            ContactField = 0;
            DataType = 0;
            IsCalculated = false;
            MultiLine = false;
            Decimals = 0;
            MaxTextLength = 0;
        }
    }

    // ExtendedFieldUserItem

    public class ExtendedFieldUserItem
    {
        public int ExtendedFieldUserItemId { get; set; } // ExtendedFieldUserItemID (Primary key)
        public int ExtendedFieldDefId { get; set; } // ExtendedFieldDefID
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Text { get; set; } // Text (length: 200)

        public ExtendedFieldUserItem()
        {
            ExtendedFieldDefId = 0;
            DisplayOrder = 0;
        }
    }

    // ExtendedTextValue

    public class ExtendedTextValue
    {
        public int ExtendedTextValueId { get; set; } // ExtendedTextValueID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public string Name { get; set; } // Name (length: 50)
        public string Value { get; set; } // Value (length: 800)

        public ExtendedTextValue()
        {
            FileDataId = 0;
        }
    }

    // Fannie

    public class Fannie
    {
        public int FannieId { get; set; } // FannieID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public string CaseFileId { get; set; } // CaseFileID (length: 30)
        public short CommunityLendingProduct { get; set; } // CommunityLendingProduct
        public short CommSeconds { get; set; } // CommSeconds
        public short NeighborsElig { get; set; } // NeighborsElig
        public string Msa { get; set; } // MSA (length: 40)
        public string Recommendation { get; set; } // Recommendation (length: 50)
        public string InstitutionId { get; set; } // InstitutionID (length: 6)
        public short CommunitySecondsRepaymentSchedule { get; set; } // CommunitySecondsRepaymentSchedule
        public System.DateTime? UnderwritingRunDate { get; set; } // UnderwritingRunDate
        public string SellerNumber { get; set; } // SellerNumber (length: 50)
        public int CreditAgencyCode { get; set; } // CreditAgencyCode
        public bool RefiOfConstructionLoan { get; set; } // RefiOfConstructionLoan
        public int HomebuyerEducationType { get; set; } // HomebuyerEducationType
        public int ProductDescription { get; set; } // ProductDescription
        public decimal? EnergyImpAmount { get; set; } // EnergyImpAmount
        public decimal? PaceLoanPayoffAmount { get; set; } // PACELoanPayoffAmount
        public string ProductDescriptionOther { get; set; } // ProductDescriptionOther (length: 50)
        public bool IsSellerProvidingBelowMarketSubFi { get; set; } // IsSellerProvidingBelowMarketSubFi
        public int SubmissionType { get; set; } // SubmissionType
        public string LenderInstitutionIdentifier { get; set; } // LenderInstitutionIdentifier (length: 20)

        public Fannie()
        {
            FileDataId = 0;
            CommunityLendingProduct = 0;
            CommSeconds = 0;
            NeighborsElig = 0;
            CommunitySecondsRepaymentSchedule = 0;
            CreditAgencyCode = 0;
            RefiOfConstructionLoan = false;
            HomebuyerEducationType = 0;
            ProductDescription = 0;
            IsSellerProvidingBelowMarketSubFi = false;
            SubmissionType = 0;
        }
    }

    // FannieARM

    public class FannieArm
    {
        public int FannieArmid { get; set; } // FannieARMID (Primary key)
        public string Plan { get; set; } // Plan (length: 20)
        public string Description { get; set; } // Description (length: 255)
    }

    // Favorite

    public class Favorite
    {
        public int FavoriteId { get; set; } // FavoriteID (Primary key)
        public string PageName { get; set; } // PageName (length: 50)
        public int DisplayOrder { get; set; } // DisplayOrder
        public int UserId { get; set; } // UserID
        public int Location { get; set; } // Location

        public Favorite()
        {
            DisplayOrder = 0;
            UserId = 0;
            Location = 0;
        }
    }

    // FeeLogEntry

    public class FeeLogEntry
    {
        public int FeeLogEntryId { get; set; } // FeeLogEntryID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int? CocLogEntryId { get; set; } // COCLogEntryID
        public int LoanId { get; set; } // LoanID
        public int FeeType { get; set; } // FeeType
        public int HudccLineNo { get; set; } // HUDCCLineNo
        public int PrepaidItemType { get; set; } // PrepaidItemType
        public decimal? Points { get; set; } // Points
        public decimal? BorrowerAmount { get; set; } // BorrowerAmount
        public decimal? SellerAmount { get; set; } // SellerAmount
        public bool Poc { get; set; } // POC
        public decimal? BorrowerPocAmountOv { get; set; } // BorrowerPOCAmountOV
        public decimal? SellerPocAmountOv { get; set; } // SellerPOCAmountOV
        public int PaidByOtherType { get; set; } // PaidByOtherType
        public decimal? PeriodicPaymentAmount { get; set; } // PeriodicPaymentAmount
        public int? CountOfPeriods { get; set; } // CountOfPeriods
        public int ResponsiblePartyType { get; set; } // ResponsiblePartyType
        public int FeeLogEntryType { get; set; } // FeeLogEntryType
        public decimal? FinancedAmount { get; set; } // FinancedAmount
        public decimal? LumpSumAmount { get; set; } // LumpSumAmount
        public decimal? ItemizedAmount { get; set; } // ItemizedAmount

        public FeeLogEntry()
        {
            FileDataId = 0;
            LoanId = 0;
            FeeType = 0;
            HudccLineNo = 0;
            PrepaidItemType = 0;
            Poc = false;
            PaidByOtherType = 0;
            ResponsiblePartyType = 0;
            FeeLogEntryType = 0;
        }
    }

    // FHA

    public class Fha
    {
        public int Fhaid { get; set; } // FHAID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public decimal? MipRefund { get; set; } // MIPRefund
        public decimal? EnergyImp { get; set; } // EnergyImp
        public string SectionOfActOv { get; set; } // SectionOfActOV (length: 50)
        public decimal? ReasonableValue { get; set; } // ReasonableValue
        public short FhaLoanPurpose { get; set; } // FHALoanPurpose
        public string CreditDataAgent { get; set; } // CreditDataAgent (length: 250)
        public short ReasonableValueType { get; set; } // ReasonableValueType
        public short ReceivedLeadPaintNotice { get; set; } // ReceivedLeadPaintNotice
        public short ApprovedOrModified { get; set; } // ApprovedOrModified
        public decimal? ModifiedLoanWith { get; set; } // ModifiedLoanWith
        public decimal? ModifiedIntRate { get; set; } // ModifiedIntRate
        public int? ModifiedTerm { get; set; } // ModifiedTerm
        public decimal? ModifiedPayment { get; set; } // ModifiedPayment
        public decimal? ModifiedUfmip { get; set; } // ModifiedUFMIP
        public decimal? ModifiedMiPayment { get; set; } // ModifiedMIPayment
        public int? ModifiedMiTerm { get; set; } // ModifiedMITerm
        public bool ProposedConstInCompliance { get; set; } // ProposedConstInCompliance
        public bool NewConstComplete { get; set; } // NewConstComplete
        public bool BuildersWarrantyReq { get; set; } // BuildersWarrantyReq
        public bool PropertyHas10YearWarranty { get; set; } // PropertyHas10YearWarranty
        public bool CodeInspectionReq { get; set; } // CodeInspectionReq
        public bool OoNotRequired { get; set; } // OONotRequired
        public bool HighLtvForNonOoInMilitary { get; set; } // HighLTVForNonOOInMilitary
        public bool OtherCondition { get; set; } // OtherCondition
        public string OtherConditionDesc { get; set; } // OtherConditionDesc (length: 1000)
        public short ScorecardRating { get; set; } // ScorecardRating
        public string MortgageeRep { get; set; } // MortgageeRep (length: 50)
        public string DeUnderwriter { get; set; } // DEUnderwriter (length: 50)
        public string Duchumsid { get; set; } // DUCHUMSID (length: 50)
        public short MortgageeHasFinancialRel { get; set; } // MortgageeHasFinancialRel
        public string PropertyJuris { get; set; } // PropertyJuris (length: 100)
        public short HasWaterSupplyOrSewageSys { get; set; } // HasWaterSupplyOrSewageSys
        public string Section221D2CodeLetter { get; set; } // Section221D2CodeLetter (length: 50)
        public decimal? Mcc { get; set; } // MCC
        public decimal? MaxLoanLtvFactorOv { get; set; } // MaxLoanLTVFactorOV
        public int? MiDurationMonthsOv { get; set; } // MIDurationMonthsOV
        public bool IsSponsoredOrigination { get; set; } // IsSponsoredOrigination
        public bool MaxLoanCanExceedCountyLimitMl201129 { get; set; } // MaxLoanCanExceedCountyLimitML2011_29
        public System.DateTime? OrigFhaEndorsementDate { get; set; } // OrigFHAEndorsementDate
        public bool AllConditionsSatisfied { get; set; } // AllConditionsSatisfied
        public long UrlaAddendumRoleOfOfficerOv { get; set; } // URLAAddendumRoleOfOfficerOV

        public Fha()
        {
            FileDataId = 0;
            FhaLoanPurpose = 0;
            ReasonableValueType = 0;
            ReceivedLeadPaintNotice = 0;
            ApprovedOrModified = 0;
            ProposedConstInCompliance = false;
            NewConstComplete = false;
            BuildersWarrantyReq = false;
            PropertyHas10YearWarranty = false;
            CodeInspectionReq = false;
            OoNotRequired = false;
            HighLtvForNonOoInMilitary = false;
            OtherCondition = false;
            ScorecardRating = 0;
            MortgageeHasFinancialRel = 0;
            HasWaterSupplyOrSewageSys = 0;
            IsSponsoredOrigination = false;
            MaxLoanCanExceedCountyLimitMl201129 = false;
            AllConditionsSatisfied = false;
            UrlaAddendumRoleOfOfficerOv = 0;
        }
    }

    // FHA203K

    public class Fha203K
    {
        public int Fha203Kid { get; set; } // FHA203KID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public short HudOwned { get; set; } // HUDOwned
        public short CommitmentStage { get; set; } // CommitmentStage
        public short OccupancyType { get; set; } // OccupancyType
        public System.DateTime? PurchaseDate { get; set; } // PurchaseDate
        public bool EscrowCommitment { get; set; } // EscrowCommitment
        public decimal? ExistingDebt { get; set; } // ExistingDebt
        public decimal? AsIsValue { get; set; } // AsIsValue
        public decimal? AfterImprovedValue { get; set; } // AfterImprovedValue
        public decimal? BorClosingCostsOv { get; set; } // BorClosingCostsOV
        public decimal? ContingencyPerc { get; set; } // ContingencyPerc
        public decimal? ContingencyPaidInCash { get; set; } // ContingencyPaidInCash
        public decimal? CostOfRepairs { get; set; } // CostOfRepairs
        public int? InspectionCount { get; set; } // InspectionCount
        public decimal? InspectionCost { get; set; } // InspectionCost
        public int? TitleUpdateCount { get; set; } // TitleUpdateCount
        public decimal? TitleUpdateCost { get; set; } // TitleUpdateCost
        public int? MonthsEscrowed { get; set; } // MonthsEscrowed
        public decimal? MortgagePayment { get; set; } // MortgagePayment
        public decimal? ArchAndEngFees { get; set; } // ArchAndEngFees
        public decimal? ConsultantFees { get; set; } // ConsultantFees
        public decimal? PermitsAndOtherFees { get; set; } // PermitsAndOtherFees
        public int? ReviewerMiles { get; set; } // ReviewerMiles
        public decimal? ReviewerCostPerMile { get; set; } // ReviewerCostPerMile
        public decimal? ReviewerOtherFee { get; set; } // ReviewerOtherFee
        public bool WaiveSuppOrigFee { get; set; } // WaiveSuppOrigFee
        public decimal? SuppOrigFeeOv { get; set; } // SuppOrigFeeOV
        public decimal? DiscoutOnRepairs { get; set; } // DiscoutOnRepairs
        public decimal? AllowableDownPayment { get; set; } // AllowableDownPayment
        public decimal? PurRequiredInvOv { get; set; } // PurRequiredInvOV
        public decimal? PurMaxMortOv { get; set; } // PurMaxMortOV
        public decimal? AllowableFinPrepaids { get; set; } // AllowableFinPrepaids
        public decimal? DiscountOnLoan { get; set; } // DiscountOnLoan
        public decimal? DiscountLoanBasis { get; set; } // DiscountLoanBasis
        public decimal? LineD4Ov { get; set; } // LineD4OV
        public decimal? LineE1Ov { get; set; } // LineE1OV
        public decimal? LineE2Ov { get; set; } // LineE2OV
        public decimal? TotalEscrowedFunds { get; set; } // TotalEscrowedFunds
        public string Remarks { get; set; } // Remarks (length: 1000)
        public bool ShowDiscountInDollars { get; set; } // ShowDiscountInDollars
        public string DeTitle { get; set; } // DETitle (length: 50)
        public bool StreamlinedK { get; set; } // StreamlinedK
        public decimal? FeasibilityStudyFee { get; set; } // FeasibilityStudyFee
        public decimal? PurInducements { get; set; } // PurInducements
        public decimal? LeadBasedPaintCredit { get; set; } // LeadBasedPaintCredit
        public decimal? MortgageLimit { get; set; } // MortgageLimit
        public decimal? InitialBaseMortgageOv { get; set; } // InitialBaseMortgageOV
        public decimal? SolarWindCost { get; set; } // SolarWindCost
        public decimal? MaterialCostForItemsOrderedPrepaid { get; set; } // MaterialCostForItemsOrderedPrepaid
        public decimal? MaterialCostForItemsOrderedNotPrepaid { get; set; } // MaterialCostForItemsOrderedNotPrepaid
        public int Fha203KType { get; set; } // FHA203KType
        public decimal? DrawArchAndEngFees { get; set; } // DrawArchAndEngFees
        public decimal? DrawPermitsAndOtherFees { get; set; } // DrawPermitsAndOtherFees
        public decimal? DrawSuppOrigFee { get; set; } // DrawSuppOrigFee
        public decimal? DrawDiscountPointsAndFees { get; set; } // DrawDiscountPointsAndFees
        public int PropertyAcquired { get; set; } // PropertyAcquired
        public decimal? InitialBaseMortgage { get; set; } // _InitialBaseMortgage
        public decimal? DiscountPointsAndFeesOv { get; set; } // DiscountPointsAndFeesOV
        public decimal? ContingencyTotalOv { get; set; } // ContingencyTotalOV

        public Fha203K()
        {
            FileDataId = 0;
            HudOwned = 0;
            CommitmentStage = 0;
            OccupancyType = 0;
            EscrowCommitment = false;
            WaiveSuppOrigFee = false;
            ShowDiscountInDollars = false;
            StreamlinedK = false;
            Fha203KType = 0;
            PropertyAcquired = 0;
        }
    }

    // FHAForms

    public class FhaForm
    {
        public int FhaFormsId { get; set; } // FHAFormsID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public short FairAnalysis { get; set; } // FairAnalysis
        public string FairAnalysisComments { get; set; } // FairAnalysisComments (length: 2147483647)
        public string QualityComments { get; set; } // QualityComments (length: 2147483647)
        public short ComparablesAcceptable { get; set; } // ComparablesAcceptable
        public string ComparablesComments { get; set; } // ComparablesComments (length: 2147483647)
        public short AdjustmentsAcceptable { get; set; } // AdjustmentsAcceptable
        public string AdjustmentsComments { get; set; } // AdjustmentsComments (length: 2147483647)
        public short ValueAcceptable { get; set; } // ValueAcceptable
        public short ValueShouldBeCorrected { get; set; } // ValueShouldBeCorrected
        public decimal? ValueForFhaPurposes { get; set; } // ValueForFHAPurposes
        public string ValueCorrectionComments { get; set; } // ValueCorrectionComments (length: 2147483647)
        public string RepairConditions { get; set; } // RepairConditions (length: 2147483647)
        public string OtherAppraisalComments { get; set; } // OtherAppraisalComments (length: 2147483647)
        public System.DateTime? ActionDate { get; set; } // ActionDate
        public string CommitmentIssuedBy { get; set; } // CommitmentIssuedBy (length: 100)
        public System.DateTime? CommitmentIssued { get; set; } // CommitmentIssued
        public System.DateTime? CommitmentExpires { get; set; } // CommitmentExpires
        public short IsEligibleForMaxFi { get; set; } // IsEligibleForMaxFi
        public bool HasAssuranceOfComp { get; set; } // HasAssuranceOfComp
        public decimal? AssuranceCompAmount { get; set; } // AssuranceCompAmount
        public bool HasAdditionaltemsAttached { get; set; } // HasAdditionaltemsAttached
        public string AdditionalConditions { get; set; } // AdditionalConditions (length: 400)
        public bool ConditionsOnBackApply { get; set; } // ConditionsOnBackApply

        public FhaForm()
        {
            FileDataId = 0;
            FairAnalysis = 0;
            ComparablesAcceptable = 0;
            AdjustmentsAcceptable = 0;
            ValueAcceptable = 0;
            ValueShouldBeCorrected = 0;
            IsEligibleForMaxFi = 0;
            HasAssuranceOfComp = false;
            HasAdditionaltemsAttached = false;
            ConditionsOnBackApply = false;
        }
    }

    // FHAHighCCState

    public class FhaHighCcState
    {
        public int FhaHighCcStateId { get; set; } // FHAHighCCStateID (Primary key)
        public string StateAbbr { get; set; } // StateAbbr (length: 2)
        public string StateName { get; set; } // StateName (length: 50)
        public int? StateId { get; set; } // StateID
        public bool IsHighCcState { get; set; } // IsHighCCState

        public FhaHighCcState()
        {
            IsHighCcState = false;
        }
    }

    // FHAInformedChoice

    public class FhaInformedChoice
    {
        public int FhaInformedChoiceId { get; set; } // FHAInformedChoiceID (Primary key)
        public System.DateTime? PrepDate { get; set; } // PrepDate
        public string Title1 { get; set; } // Title1 (length: 50)
        public decimal? PurPrice1 { get; set; } // PurPrice1
        public decimal? BaseLoan1 { get; set; } // BaseLoan1
        public decimal? LoanWith1 { get; set; } // LoanWith1
        public decimal? ClosingCosts1 { get; set; } // ClosingCosts1
        public decimal? Downpayment1 { get; set; } // Downpayment1
        public decimal? IntRate1 { get; set; } // IntRate1
        public decimal? Term1 { get; set; } // Term1
        public decimal? Pi1 { get; set; } // PI1
        public decimal? Ltv1 { get; set; } // LTV1
        public decimal? MiMo1 { get; set; } // MiMo1
        public string DurMi1 { get; set; } // DurMi1 (length: 50)
        public string UpfrontMip1 { get; set; } // UpfrontMIP1 (length: 50)
        public string Title2 { get; set; } // Title2 (length: 50)
        public decimal? PurPrice2 { get; set; } // PurPrice2
        public decimal? BaseLoan2 { get; set; } // BaseLoan2
        public decimal? LoanWith2 { get; set; } // LoanWith2
        public decimal? ClosingCosts2 { get; set; } // ClosingCosts2
        public decimal? Downpayment2 { get; set; } // Downpayment2
        public decimal? IntRate2 { get; set; } // IntRate2
        public decimal? Term2 { get; set; } // Term2
        public decimal? Pi2 { get; set; } // PI2
        public decimal? Ltv2 { get; set; } // LTV2
        public decimal? MiMo2 { get; set; } // MiMo2
        public string DurMi2 { get; set; } // DurMi2 (length: 50)
        public string UpfrontMip2 { get; set; } // UpfrontMIP2 (length: 50)
        public decimal? MipffPerc { get; set; } // MIPFFPerc
        public string TitleFhaov { get; set; } // TitleFHAOV (length: 50)
        public string TitleConvOv { get; set; } // TitleConvOV (length: 50)
        public decimal? MipffPercFhaov { get; set; } // MIPFFPercFHAOV
        public decimal? MipffPercConvOv { get; set; } // MIPFFPercConvOV
        public decimal? ClosingCostsFhaov { get; set; } // ClosingCostsFHAOV
        public decimal? ClosingCostsConvOv { get; set; } // ClosingCostsConvOV
        public decimal? IntRateFhaov { get; set; } // IntRateFHAOV
        public decimal? IntRateConvOv { get; set; } // IntRateConvOV
        public decimal? LoanToValueFhaov { get; set; } // LoanToValueFHAOV
        public decimal? LoanToValueConvOv { get; set; } // LoanToValueConvOV
        public decimal? MiPremiumFhaov { get; set; } // MIPremiumFHAOV
        public decimal? MiPremiumConvOv { get; set; } // MIPremiumConvOV
        public string MiDurationFhaov { get; set; } // MIDurationFHAOV (length: 50)
        public string MiDurationConvOv { get; set; } // MIDurationConvOV (length: 50)
        public System.DateTime? DatePreparedOv { get; set; } // DatePreparedOV
        public decimal? SalesPriceFhaov { get; set; } // SalesPriceFHAOV
        public decimal? SalesPriceConvOv { get; set; } // SalesPriceConvOV
        public int? TermInYearsFhaov { get; set; } // TermInYearsFHAOV
        public int? TermInYearsConvOv { get; set; } // TermInYearsConvOV
        public decimal? MiMoPremPercFhaov { get; set; } // MIMoPremPercFHAOV
        public decimal? MiMoPremPercConvOv { get; set; } // MIMoPremPercConvOV
        public decimal? DownPaymentFhaov { get; set; } // DownPaymentFHAOV
        public decimal? DownPaymentConvOv { get; set; } // DownPaymentConvOV
        public decimal? LoanWithFhaov { get; set; } // LoanWithFHAOV
        public decimal? LoanWithConvOv { get; set; } // LoanWithConvOV
        public decimal? PiPaymentFhaov { get; set; } // PIPaymentFHAOV
        public decimal? PiPaymentConvOv { get; set; } // PIPaymentConvOV
        public string UfmipDescFhaov { get; set; } // UFMIPDescFHAOV (length: 50)
        public string UfmipDescConvOv { get; set; } // UFMIPDescConvOV (length: 50)
    }

    // FHAMCAW

    public class Fhamcaw
    {
        public int Fhamcawid { get; set; } // FHAMCAWID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public short ConstructionType { get; set; } // ConstructionType
        public decimal? StatutoryInvReqOv { get; set; } // StatutoryInvReqOV
        public decimal? RequiredAdjustmentsOv { get; set; } // RequiredAdjustmentsOV
        public decimal? SalesConcessionsOv { get; set; } // SalesConcessionsOV
        public decimal? MortgageBasisOv { get; set; } // MortgageBasisOV
        public decimal? Refi10F1Ov { get; set; } // Refi10F1OV
        public decimal? Refi10F2Ov { get; set; } // Refi10F2OV
        public decimal? RefiRequiredInvestmentOv { get; set; } // RefiRequiredInvestmentOV
        public decimal? MinDownPaymentOv { get; set; } // MinDownPaymentOV
        public decimal? DiscountNotFinancedOv { get; set; } // DiscountNotFinancedOV
        public decimal? PrepaidsNotFinancedOv { get; set; } // PrepaidsNotFinancedOV
        public decimal? NonFinanceableRepairsOv { get; set; } // NonFinanceableRepairsOV
        public decimal? NonRealtyOv { get; set; } // NonRealtyOV
        public decimal? TotalCashToCloseOv { get; set; } // TotalCashToCloseOV
        public decimal? OtherCreditsOv { get; set; } // OtherCreditsOV
        public bool OtherCreditsCashOpt { get; set; } // OtherCreditsCashOpt
        public bool OtherCreditsOtherOpt { get; set; } // OtherCreditsOtherOpt
        public decimal? CashToCloseOv { get; set; } // CashToCloseOV
        public bool AmountToBePaidCashOpt { get; set; } // AmountToBePaidCashOpt
        public bool AmountToBePaidOtherOpt { get; set; } // AmountToBePaidOtherOpt
        public decimal? AvailableAssetsOv { get; set; } // AvailableAssetsOV
        public string SecondMortgageSource { get; set; } // SecondMortgageSource (length: 50)
        public decimal? SecondMortgageOv { get; set; } // SecondMortgageOV
        public decimal? BasePayBorOv { get; set; } // BasePayBorOV
        public decimal? OtherEarningsBorOv { get; set; } // OtherEarningsBorOV
        public decimal? BasePayCoBorOv { get; set; } // BasePayCoBorOV
        public decimal? OtherEarningsCoBorOv { get; set; } // OtherEarningsCoBorOV
        public decimal? NetRealEstateIncomeOv { get; set; } // NetRealEstateIncomeOV
        public decimal? GrossMonthlyIncomeOv { get; set; } // GrossMonthlyIncomeOV
        public decimal? Ltvov { get; set; } // LTVOV
        public decimal? FirstRatioOv { get; set; } // FirstRatioOV
        public decimal? SecondRatioOv { get; set; } // SecondRatioOV
        public short CreditCharacteristics { get; set; } // CreditCharacteristics
        public short AdequacyOfIncome { get; set; } // AdequacyOfIncome
        public short StabilityOfIncome { get; set; } // StabilityOfIncome
        public short AdequacyOfAssets { get; set; } // AdequacyOfAssets
        public short TotalCcOption { get; set; } // TotalCCOption
        public decimal? TotalCcOtherAmount { get; set; } // TotalCCOtherAmount
        public bool PurchasedWithinOneYear { get; set; } // PurchasedWithinOneYear
        public decimal? WeatherizationCosts { get; set; } // WeatherizationCosts
        public bool IncCcInExistingDebt { get; set; } // IncCCInExistingDebt
        public bool IncDpInExistingDebt { get; set; } // IncDPInExistingDebt
        public bool IncPpInExistingDebt { get; set; } // IncPPInExistingDebt
        public decimal? OtherExistingDebtAmount { get; set; } // OtherExistingDebtAmount
        public string Remarks { get; set; } // Remarks (length: 2147483647)
        public bool OrigCaseNoAssignedOnOrAfterJuly142008 { get; set; } // OrigCaseNoAssignedOnOrAfterJuly142008
        public decimal? NetRealEstateIncomeCoBorsOv { get; set; } // NetRealEstateIncomeCoBorsOV
        public short AmortTypeOv { get; set; } // AmortTypeOV
        public string SecondMortgageOtherDesc { get; set; } // SecondMortgageOtherDesc (length: 50)
        public short SecondMortgageSourceType { get; set; } // SecondMortgageSourceType
        public short Gift1SourceType { get; set; } // Gift1SourceType
        public string Gift1OtherDesc { get; set; } // Gift1OtherDesc (length: 50)
        public string Gift2Source { get; set; } // Gift2Source (length: 50)
        public short Gift2SourceType { get; set; } // Gift2SourceType
        public string Gift2OtherDesc { get; set; } // Gift2OtherDesc (length: 50)
        public decimal? Gift2Amount { get; set; } // Gift2Amount
        public short SellerFundedDap { get; set; } // SellerFundedDAP
        public decimal? Cltvov { get; set; } // CLTVOV
        public decimal? SellerContributionOv { get; set; } // SellerContributionOV
        public int? MonthsInReserveFunds { get; set; } // MonthsInReserveFunds
        public short ScoredByTotal { get; set; } // ScoredByTOTAL
        public short RiskClass { get; set; } // RiskClass
        public string AppraisalReviewerChumsNo { get; set; } // AppraisalReviewerCHUMSNo (length: 50)
        public short PropertyTypeOv { get; set; } // PropertyTypeOV
        public string SourceOfFunds { get; set; } // SourceOfFunds (length: 50)
        public bool EnergyEfficientMortgage { get; set; } // EnergyEfficientMortgage
        public bool BuildingOnOwnLand { get; set; } // BuildingOnOwnLand
        public bool LoanPurposeOther { get; set; } // LoanPurposeOther
        public string LoanPurposeOtherDesc { get; set; } // LoanPurposeOtherDesc (length: 50)
        public short IntRateBuydownOv { get; set; } // IntRateBuydownOV
        public int CaseNoAssignmentPeriod { get; set; } // CaseNoAssignmentPeriod
        public decimal? ClosingCostsAndPrepaidsPbaov { get; set; } // ClosingCostsAndPrepaidsPBAOV

        public Fhamcaw()
        {
            FileDataId = 0;
            ConstructionType = 0;
            OtherCreditsCashOpt = false;
            OtherCreditsOtherOpt = false;
            AmountToBePaidCashOpt = false;
            AmountToBePaidOtherOpt = false;
            CreditCharacteristics = 0;
            AdequacyOfIncome = 0;
            StabilityOfIncome = 0;
            AdequacyOfAssets = 0;
            TotalCcOption = 0;
            PurchasedWithinOneYear = false;
            IncCcInExistingDebt = false;
            IncDpInExistingDebt = false;
            IncPpInExistingDebt = false;
            OrigCaseNoAssignedOnOrAfterJuly142008 = false;
            AmortTypeOv = 0;
            SecondMortgageSourceType = 0;
            Gift1SourceType = 0;
            Gift2SourceType = 0;
            SellerFundedDap = 0;
            ScoredByTotal = 0;
            RiskClass = 0;
            PropertyTypeOv = 0;
            EnergyEfficientMortgage = false;
            BuildingOnOwnLand = false;
            LoanPurposeOther = false;
            IntRateBuydownOv = 0;
            CaseNoAssignmentPeriod = 0;
        }
    }

    // FHAMI

    public class Fhami
    {
        public int Fhamiid { get; set; } // FHAMIID (Primary key)
        public decimal? UpfrontTerm30 { get; set; } // UpfrontTerm30
        public decimal? UpfrontTerm15 { get; set; } // UpfrontTerm15
        public decimal? AnnualTerm30 { get; set; } // AnnualTerm30
        public decimal? AnnualTerm15Ltv90To100 { get; set; } // AnnualTerm15LTV90To100
        public decimal? AnnualTerm15Ltv0To90 { get; set; } // AnnualTerm15LTV0To90
        public decimal? RhsGuaranteeFeePercPur { get; set; } // RHSGuaranteeFeePercPur
        public decimal? RhsGuaranteeFeePercRefi { get; set; } // RHSGuaranteeFeePercRefi
    }

    // FieldHistoryEntry

    public class FieldHistoryEntry
    {
        public int FieldHistoryEntryId { get; set; } // FieldHistoryEntryID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int FieldHistoryEntryType { get; set; } // FieldHistoryEntryType
        public System.DateTime DateModified { get; set; } // DateModified
        public decimal? DecimalValue { get; set; } // DecimalValue
        public short FieldHistoryFieldType { get; set; } // FieldHistoryFieldType

        public FieldHistoryEntry()
        {
            FileDataId = 0;
            FieldHistoryEntryType = 0;
            FieldHistoryFieldType = 0;
        }
    }

    // FieldNote

    public class FieldNote
    {
        public int FieldNoteId { get; set; } // FieldNoteID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public string ObjectTableName { get; set; } // ObjectTableName (length: 50)
        public string FieldName { get; set; } // FieldName (length: 50)
        public int RecordBaseId { get; set; } // RecordBaseID
        public short FollowUpFlag { get; set; } // FollowUpFlag
        public System.DateTime? FollowUpDate { get; set; } // FollowUpDate
        public short AlertFlag { get; set; } // AlertFlag
        public long AlertUserRole { get; set; } // AlertUserRole
        public string Notes { get; set; } // Notes (length: 2147483647)

        public FieldNote()
        {
            FileDataId = 0;
            RecordBaseId = 0;
            FollowUpFlag = 0;
            AlertFlag = 0;
            AlertUserRole = 0;
        }
    }

    // FileData

    public class FileData
    {
        public int FileDataId { get; set; } // FileDataID (Primary key)
        public int? LoanId { get; set; } // LoanID
        public int OrganizationId { get; set; } // OrganizationID
        public string FileName { get; set; } // FileName (length: 50)
        public string FilePath { get; set; } // FilePath (length: 255)
        public short OccupancyType { get; set; } // OccupancyType
        public System.DateTime DateCreated { get; set; } // DateCreated
        public System.DateTime DateModified { get; set; } // DateModified
        public string AgencyCaseNo { get; set; } // AgencyCaseNo (length: 50)
        public string TitleNamesNonBorrowers { get; set; } // TitleNamesNonBorrowers (length: 200)
        public string DesiredCoName { get; set; } // DesiredCoName (length: 150)
        public string DesiredCoStreet1 { get; set; } // DesiredCoStreet1 (length: 50)
        public string DesiredCoStreet2 { get; set; } // DesiredCoStreet2 (length: 50)
        public string DesiredCoCity { get; set; } // DesiredCoCity (length: 50)
        public string DesiredCoState { get; set; } // DesiredCoState (length: 2)
        public string DesiredCoZip { get; set; } // DesiredCoZip (length: 9)
        public string DesiredCoPhone { get; set; } // DesiredCoPhone (length: 20)
        public string DesiredCoFax { get; set; } // DesiredCoFax (length: 20)
        public short DownPaymentType1 { get; set; } // DownPaymentType1
        public decimal? DownPaymentAmount1 { get; set; } // DownPaymentAmount1
        public short DownPaymentType2 { get; set; } // DownPaymentType2
        public decimal? DownPaymentAmount2 { get; set; } // DownPaymentAmount2
        public string DownPaymentDescOv { get; set; } // DownPaymentDescOV (length: 100)
        public string FhaLenderId { get; set; } // FHALenderId (length: 50)
        public string FhaSponsorId { get; set; } // FHASponsorId (length: 50)
        public decimal? SalesConcessions { get; set; } // SalesConcessions
        public decimal? GiftAmount { get; set; } // GiftAmount
        public string GiftSource { get; set; } // GiftSource (length: 50)
        public string GiftDonorName1 { get; set; } // GiftDonorName1 (length: 50)
        public string GiftDonorName2 { get; set; } // GiftDonorName2 (length: 50)
        public string GiftDonorStreet { get; set; } // GiftDonorStreet (length: 50)
        public string GiftDonorCity { get; set; } // GiftDonorCity (length: 50)
        public string GiftDonorState { get; set; } // GiftDonorState (length: 2)
        public string GiftDonorZip { get; set; } // GiftDonorZip (length: 9)
        public string GiftDonorPhone { get; set; } // GiftDonorPhone (length: 20)
        public string MinNumber { get; set; } // MINNumber (length: 20)
        public int? MinOrgId { get; set; } // MINOrgID
        public long? MinSequence { get; set; } // MINSequence
        public short WaiveEscrow { get; set; } // WaiveEscrow
        public short VaFundingFeeCategory { get; set; } // VAFundingFeeCategory
        public short FirstTimeHomeBuyer { get; set; } // FirstTimeHomeBuyer
        public int? TaxBracket { get; set; } // TaxBracket
        public short CommunityLending { get; set; } // CommunityLending
        public short HomeBuyerEducation { get; set; } // HomeBuyerEducation
        public short FirstMortgageHolder { get; set; } // FirstMortgageHolder
        public string LoanOfficerUserName { get; set; } // LoanOfficerUserName (length: 50)
        public short LoanOfficerAccess { get; set; } // LoanOfficerAccess
        public string LoanProcessorUserName { get; set; } // LoanProcessorUserName (length: 50)
        public short LoanProcessorAccess { get; set; } // LoanProcessorAccess
        public string OtherUserName { get; set; } // OtherUserName (length: 50)
        public short OtherUserAccess { get; set; } // OtherUserAccess
        public string OtherUser2Name { get; set; } // OtherUser2Name (length: 50)
        public short OtherUser2Access { get; set; } // OtherUser2Access
        public string OtherUser3Name { get; set; } // OtherUser3Name (length: 50)
        public short OtherUser3Access { get; set; } // OtherUser3Access
        public string OtherUser4Name { get; set; } // OtherUser4Name (length: 50)
        public short OtherUser4Access { get; set; } // OtherUser4Access
        public short DocumentationType { get; set; } // DocumentationType
        public bool UseCustomRepaymentWording { get; set; } // UseCustomRepaymentWording
        public string RepaymentWordingOv { get; set; } // RepaymentWordingOV (length: 500)
        public short EvidenceOfTitleOption { get; set; } // EvidenceOfTitleOption
        public string OtherEvidenceOfTitle { get; set; } // OtherEvidenceOfTitle (length: 50)
        public short CommitmentReturnOption { get; set; } // CommitmentReturnOption
        public int? CommitmentReturnDays { get; set; } // CommitmentReturnDays
        public string CommitmentReturnAddress { get; set; } // CommitmentReturnAddress (length: 250)
        public string XSiteLoanAppId { get; set; } // XSiteLoanAppID (length: 50)
        public bool Deleted { get; set; } // Deleted
        public int SubFolder { get; set; } // SubFolder
        public decimal? TrustAccountBalance { get; set; } // _TrustAccountBalance
        public decimal? TotalAllIncomes { get; set; } // _TotalAllIncomes
        public decimal? TotalAllDebtPayments { get; set; } // _TotalAllDebtPayments
        public decimal? TotalAllDebtBalances { get; set; } // _TotalAllDebtBalances
        public decimal? TotalAllLiquidAssets { get; set; } // _TotalAllLiquidAssets
        public string CosignerNames { get; set; } // _CosignerNames (length: 200)
        public string AllBorrowerNames { get; set; } // _AllBorrowerNames (length: 200)
        public System.DateTime? LoYellowAlertDate { get; set; } // _LOYellowAlertDate
        public System.DateTime? LoRedAlertDate { get; set; } // _LORedAlertDate
        public System.DateTime? LpYellowAlertDate { get; set; } // _LPYellowAlertDate
        public System.DateTime? LpRedAlertDate { get; set; } // _LPRedAlertDate
        public System.DateTime? OuYellowAlertDate { get; set; } // _OUYellowAlertDate
        public System.DateTime? OuRedAlertDate { get; set; } // _OURedAlertDate
        public System.DateTime? Ou2YellowAlertDate { get; set; } // _OU2YellowAlertDate
        public System.DateTime? Ou2RedAlertDate { get; set; } // _OU2RedAlertDate
        public System.DateTime? Ou3YellowAlertDate { get; set; } // _OU3YellowAlertDate
        public System.DateTime? Ou3RedAlertDate { get; set; } // _OU3RedAlertDate
        public System.DateTime? Ou4YellowAlertDate { get; set; } // _OU4YellowAlertDate
        public System.DateTime? Ou4RedAlertDate { get; set; } // _OU4RedAlertDate
        public bool OverrideInterviewer { get; set; } // OverrideInterviewer
        public int? DriveScore { get; set; } // DRIVEScore
        public string DriveStatus { get; set; } // DRIVEStatus (length: 50)
        public int? FileCreditScore { get; set; } // _FileCreditScore
        public string OrgNameOv { get; set; } // OrgNameOV (length: 150)
        public string OrgStreet1Ov { get; set; } // OrgStreet1OV (length: 50)
        public string OrgStreet2Ov { get; set; } // OrgStreet2OV (length: 50)
        public string OrgCityOv { get; set; } // OrgCityOV (length: 50)
        public string OrgStateOv { get; set; } // OrgStateOV (length: 2)
        public string OrgZipOv { get; set; } // OrgZipOV (length: 9)
        public string OrgPhoneOv { get; set; } // OrgPhoneOV (length: 20)
        public string OrgFaxOv { get; set; } // OrgFaxOV (length: 20)
        public string MortgageBotAccountId { get; set; } // MortgageBotAccountId (length: 50)
        public int SuperLoanType { get; set; } // SuperLoanType
        public string VaLenderId { get; set; } // VALenderID (length: 50)
        public string VaAgentId { get; set; } // VAAgentID (length: 50)
        public int GfeVersion { get; set; } // GFEVersion
        public int CompanyTypeOv { get; set; } // CompanyTypeOV
        public int OriginationChannel { get; set; } // OriginationChannel
        public int TilVersionOv { get; set; } // TILVersionOV
        public int LockRequestType { get; set; } // LockRequestType
        public System.DateTime? LockRequestTime { get; set; } // LockRequestTime
        public string LockRequestComments { get; set; } // LockRequestComments (length: 2147483647)
        public string CorvisaFileId1 { get; set; } // CorvisaFileID1 (length: 50)
        public string CorvisaFileId2 { get; set; } // CorvisaFileID2 (length: 50)
        public System.Guid? TradeGuid { get; set; } // TradeGUID
        public int? LockRequestExtensionDays { get; set; } // LockRequestExtensionDays
        public int? RhsCaseNoAssignmentPeriod { get; set; } // RHSCaseNoAssignmentPeriod
        public string ThirdPartyFileName { get; set; } // ThirdPartyFileName (length: 100)
        public string ThirdPartyFileNamePiggyback { get; set; } // ThirdPartyFileNamePiggyback (length: 100)
        public System.DateTime? ThirdPartyTransferDateFirst { get; set; } // ThirdPartyTransferDateFirst
        public System.DateTime? ThirdPartyTransferDateMostRecent { get; set; } // ThirdPartyTransferDateMostRecent
        public string OpenerUserName { get; set; } // OpenerUserName (length: 50)
        public string UnderwriterUserName { get; set; } // UnderwriterUserName (length: 50)
        public string DocDrawerUserName { get; set; } // DocDrawerUserName (length: 50)
        public string CloserUserName { get; set; } // CloserUserName (length: 50)
        public string QcUserName { get; set; } // QCUserName (length: 50)
        public string ComplianceUserName { get; set; } // ComplianceUserName (length: 50)
        public string ShipperUserName { get; set; } // ShipperUserName (length: 50)
        public string LockDeskUserName { get; set; } // LockDeskUserName (length: 50)
        public string AccountingUserName { get; set; } // AccountingUserName (length: 50)
        public string ServicingUserName { get; set; } // ServicingUserName (length: 50)
        public string InsuringUserName { get; set; } // InsuringUserName (length: 50)
        public string SecondaryUserName { get; set; } // SecondaryUserName (length: 50)
        public System.DateTime? OpenerYellowAlertDate { get; set; } // _OpenerYellowAlertDate
        public System.DateTime? OpenerRedAlertDate { get; set; } // _OpenerRedAlertDate
        public System.DateTime? UnderwriterYellowAlertDate { get; set; } // _UnderwriterYellowAlertDate
        public System.DateTime? UnderwriterRedAlertDate { get; set; } // _UnderwriterRedAlertDate
        public System.DateTime? DocDrawerYellowAlertDate { get; set; } // _DocDrawerYellowAlertDate
        public System.DateTime? DocDrawerRedAlertDate { get; set; } // _DocDrawerRedAlertDate
        public System.DateTime? CloserYellowAlertDate { get; set; } // _CloserYellowAlertDate
        public System.DateTime? CloserRedAlertDate { get; set; } // _CloserRedAlertDate
        public System.DateTime? QcYellowAlertDate { get; set; } // _QCYellowAlertDate
        public System.DateTime? QcRedAlertDate { get; set; } // _QCRedAlertDate
        public System.DateTime? ComplianceYellowAlertDate { get; set; } // _ComplianceYellowAlertDate
        public System.DateTime? ComplianceRedAlertDate { get; set; } // _ComplianceRedAlertDate
        public System.DateTime? ShipperYellowAlertDate { get; set; } // _ShipperYellowAlertDate
        public System.DateTime? ShipperRedAlertDate { get; set; } // _ShipperRedAlertDate
        public System.DateTime? LockDeskYellowAlertDate { get; set; } // _LockDeskYellowAlertDate
        public System.DateTime? LockDeskRedAlertDate { get; set; } // _LockDeskRedAlertDate
        public System.DateTime? AccountingYellowAlertDate { get; set; } // _AccountingYellowAlertDate
        public System.DateTime? AccountingRedAlertDate { get; set; } // _AccountingRedAlertDate
        public System.DateTime? ServicingYellowAlertDate { get; set; } // _ServicingYellowAlertDate
        public System.DateTime? ServicingRedAlertDate { get; set; } // _ServicingRedAlertDate
        public System.DateTime? InsuringYellowAlertDate { get; set; } // _InsuringYellowAlertDate
        public System.DateTime? InsuringRedAlertDate { get; set; } // _InsuringRedAlertDate
        public System.DateTime? SecondaryYellowAlertDate { get; set; } // _SecondaryYellowAlertDate
        public System.DateTime? SecondaryRedAlertDate { get; set; } // _SecondaryRedAlertDate
        public string GiftDonorAccountInstitution { get; set; } // GiftDonorAccountInstitution (length: 50)
        public string GiftDonorAccountNo { get; set; } // GiftDonorAccountNo (length: 30)
        public System.DateTime? GiftTransferDate { get; set; } // GiftTransferDate
        public bool GiftFundsProvidedAtClosing { get; set; } // GiftFundsProvidedAtClosing
        public string GiftDonorInstitutionAddress { get; set; } // GiftDonorInstitutionAddress (length: 100)
        public System.DateTime? AgencyCaseNoAssignmentDate { get; set; } // AgencyCaseNoAssignmentDate
        public int DocTypeEmployment { get; set; } // DocTypeEmployment
        public int DocTypeIncome { get; set; } // DocTypeIncome
        public int DocTypeAsset { get; set; } // DocTypeAsset
        public int? EstimatedCreditScore { get; set; } // EstimatedCreditScore
        public string OptimalBlueLoanIdentifier { get; set; } // OptimalBlueLoanIdentifier (length: 20)
        public bool SelfEmployed { get; set; } // _SelfEmployed
        public bool HasNonOccCoBorrower { get; set; } // _HasNonOccCoBorrower
        public string OtherUser5Name { get; set; } // OtherUser5Name (length: 50)
        public System.DateTime? Ou5YellowAlertDate { get; set; } // _OU5YellowAlertDate
        public System.DateTime? Ou5RedAlertDate { get; set; } // _OU5RedAlertDate
        public string OtherUser6Name { get; set; } // OtherUser6Name (length: 50)
        public System.DateTime? Ou6YellowAlertDate { get; set; } // _OU6YellowAlertDate
        public System.DateTime? Ou6RedAlertDate { get; set; } // _OU6RedAlertDate
        public string OtherUser7Name { get; set; } // OtherUser7Name (length: 50)
        public System.DateTime? Ou7YellowAlertDate { get; set; } // _OU7YellowAlertDate
        public System.DateTime? Ou7RedAlertDate { get; set; } // _OU7RedAlertDate
        public string OtherUser8Name { get; set; } // OtherUser8Name (length: 50)
        public System.DateTime? Ou8YellowAlertDate { get; set; } // _OU8YellowAlertDate
        public System.DateTime? Ou8RedAlertDate { get; set; } // _OU8RedAlertDate
        public string OtherUser9Name { get; set; } // OtherUser9Name (length: 50)
        public System.DateTime? Ou9YellowAlertDate { get; set; } // _OU9YellowAlertDate
        public System.DateTime? Ou9RedAlertDate { get; set; } // _OU9RedAlertDate
        public string OtherUser10Name { get; set; } // OtherUser10Name (length: 50)
        public System.DateTime? Ou10YellowAlertDate { get; set; } // _OU10YellowAlertDate
        public System.DateTime? Ou10RedAlertDate { get; set; } // _OU10RedAlertDate
        public string OtherUser11Name { get; set; } // OtherUser11Name (length: 50)
        public System.DateTime? Ou11YellowAlertDate { get; set; } // _OU11YellowAlertDate
        public System.DateTime? Ou11RedAlertDate { get; set; } // _OU11RedAlertDate
        public string OtherUser12Name { get; set; } // OtherUser12Name (length: 50)
        public System.DateTime? Ou12YellowAlertDate { get; set; } // _OU12YellowAlertDate
        public System.DateTime? Ou12RedAlertDate { get; set; } // _OU12RedAlertDate
        public int LoCompType { get; set; } // LOCompType
        public int EligibleForPurchaseByGseOv { get; set; } // EligibleForPurchaseByGSE_OV
        public string IncomeAndDebtQmatrNotes { get; set; } // IncomeAndDebtQMATRNotes (length: 2147483647)
        public string DocPrepAltLenderCode { get; set; } // DocPrepAltLenderCode (length: 10)
        public bool IsBusinessPurpose { get; set; } // IsBusinessPurpose
        public int EventInfo { get; set; } // EventInfo
        public bool IsFlpLoan { get; set; } // _IsFLPLoan
        public int SyncFileDataId { get; set; } // SyncFileDataID
        public int SyncType { get; set; } // SyncType
        public string SyncFileName { get; set; } // SyncFileName (length: 50)
        public decimal? SyncedFileDotCredits { get; set; } // SyncedFileDOTCredits
        public string MarksmanProspectId { get; set; } // MarksmanProspectID (length: 32)
        public int EscrowAbsenceReason { get; set; } // EscrowAbsenceReason
        public int PartialPaymentOption { get; set; } // PartialPaymentOption
        public int LiabilityAfterForeclosure { get; set; } // LiabilityAfterForeclosure
        public string TridLoanIdov { get; set; } // TRIDLoanIDOV (length: 30)
        public bool DoNotApplyTridRules { get; set; } // DoNotApplyTRIDRules
        public int ConstTilaCalcMethod { get; set; } // ConstTILACalcMethod
        public string OtherUser13Name { get; set; } // OtherUser13Name (length: 50)
        public System.DateTime? Ou13YellowAlertDate { get; set; } // _OU13YellowAlertDate
        public System.DateTime? Ou13RedAlertDate { get; set; } // _OU13RedAlertDate
        public string OtherUser14Name { get; set; } // OtherUser14Name (length: 50)
        public System.DateTime? Ou14YellowAlertDate { get; set; } // _OU14YellowAlertDate
        public System.DateTime? Ou14RedAlertDate { get; set; } // _OU14RedAlertDate
        public string OtherUser15Name { get; set; } // OtherUser15Name (length: 50)
        public System.DateTime? Ou15YellowAlertDate { get; set; } // _OU15YellowAlertDate
        public System.DateTime? Ou15RedAlertDate { get; set; } // _OU15RedAlertDate
        public string OtherUser16Name { get; set; } // OtherUser16Name (length: 50)
        public System.DateTime? Ou16YellowAlertDate { get; set; } // _OU16YellowAlertDate
        public System.DateTime? Ou16RedAlertDate { get; set; } // _OU16RedAlertDate
        public string OtherUser17Name { get; set; } // OtherUser17Name (length: 50)
        public System.DateTime? Ou17YellowAlertDate { get; set; } // _OU17YellowAlertDate
        public System.DateTime? Ou17RedAlertDate { get; set; } // _OU17RedAlertDate
        public string OtherUser18Name { get; set; } // OtherUser18Name (length: 50)
        public System.DateTime? Ou18YellowAlertDate { get; set; } // _OU18YellowAlertDate
        public System.DateTime? Ou18RedAlertDate { get; set; } // _OU18RedAlertDate
        public string OtherUser19Name { get; set; } // OtherUser19Name (length: 50)
        public System.DateTime? Ou19YellowAlertDate { get; set; } // _OU19YellowAlertDate
        public System.DateTime? Ou19RedAlertDate { get; set; } // _OU19RedAlertDate
        public string OtherUser20Name { get; set; } // OtherUser20Name (length: 50)
        public System.DateTime? Ou20YellowAlertDate { get; set; } // _OU20YellowAlertDate
        public System.DateTime? Ou20RedAlertDate { get; set; } // _OU20RedAlertDate
        public string OtherUser21Name { get; set; } // OtherUser21Name (length: 50)
        public System.DateTime? Ou21YellowAlertDate { get; set; } // _OU21YellowAlertDate
        public System.DateTime? Ou21RedAlertDate { get; set; } // _OU21RedAlertDate
        public string OtherUser22Name { get; set; } // OtherUser22Name (length: 50)
        public System.DateTime? Ou22YellowAlertDate { get; set; } // _OU22YellowAlertDate
        public System.DateTime? Ou22RedAlertDate { get; set; } // _OU22RedAlertDate
        public string OtherUser23Name { get; set; } // OtherUser23Name (length: 50)
        public System.DateTime? Ou23YellowAlertDate { get; set; } // _OU23YellowAlertDate
        public System.DateTime? Ou23RedAlertDate { get; set; } // _OU23RedAlertDate
        public string OtherUser24Name { get; set; } // OtherUser24Name (length: 50)
        public System.DateTime? Ou24YellowAlertDate { get; set; } // _OU24YellowAlertDate
        public System.DateTime? Ou24RedAlertDate { get; set; } // _OU24RedAlertDate
        public string OtherUser25Name { get; set; } // OtherUser25Name (length: 50)
        public System.DateTime? Ou25YellowAlertDate { get; set; } // _OU25YellowAlertDate
        public System.DateTime? Ou25RedAlertDate { get; set; } // _OU25RedAlertDate
        public int SyncFileDataId2 { get; set; } // SyncFileDataID2
        public string SyncFileName2 { get; set; } // SyncFileName2 (length: 50)
        public bool IsConsumerPortalLoan { get; set; } // _IsConsumerPortalLoan
        public string MarksmanLockId { get; set; } // MarksmanLockID (length: 32)
        public string Uli { get; set; } // ULI (length: 45)
        public bool OverrideUli { get; set; } // OverrideULI
        public int UrlaVersion { get; set; } // URLAVersion
        public int? CoreLogicFraudScore { get; set; } // CoreLogicFraudScore
        public string TitleCurrentlyHeldInNames { get; set; } // TitleCurrentlyHeldInNames (length: 2147483647)
        public string OriginalBorrowers { get; set; } // OriginalBorrowers (length: 2147483647)

        public FileData()
        {
            OrganizationId = 0;
            OccupancyType = 0;
            DownPaymentType1 = 0;
            DownPaymentType2 = 0;
            WaiveEscrow = 0;
            VaFundingFeeCategory = 0;
            FirstTimeHomeBuyer = 0;
            CommunityLending = 0;
            HomeBuyerEducation = 0;
            FirstMortgageHolder = 0;
            LoanOfficerAccess = 0;
            LoanProcessorAccess = 0;
            OtherUserAccess = 0;
            OtherUser2Access = 0;
            OtherUser3Access = 0;
            OtherUser4Access = 0;
            DocumentationType = 0;
            UseCustomRepaymentWording = false;
            EvidenceOfTitleOption = 0;
            CommitmentReturnOption = 0;
            Deleted = false;
            SubFolder = 0;
            OverrideInterviewer = false;
            SuperLoanType = 0;
            GfeVersion = 0;
            CompanyTypeOv = 0;
            OriginationChannel = 0;
            TilVersionOv = 0;
            LockRequestType = 0;
            GiftFundsProvidedAtClosing = false;
            DocTypeEmployment = 0;
            DocTypeIncome = 0;
            DocTypeAsset = 0;
            SelfEmployed = false;
            HasNonOccCoBorrower = false;
            LoCompType = 0;
            EligibleForPurchaseByGseOv = 0;
            IsBusinessPurpose = false;
            EventInfo = 0;
            IsFlpLoan = false;
            SyncFileDataId = 0;
            SyncType = 0;
            EscrowAbsenceReason = 0;
            PartialPaymentOption = 0;
            LiabilityAfterForeclosure = 0;
            DoNotApplyTridRules = false;
            ConstTilaCalcMethod = 0;
            SyncFileDataId2 = 0;
            IsConsumerPortalLoan = false;
            OverrideUli = false;
            UrlaVersion = 0;
        }


        public static FileData Create(LoanApplication loanApplication, ThirdPartyCodeList thirdPartyCodeList)
        {
            var fileData = new FileData();

            fileData.AgencyCaseNo = loanApplication.LoanNumber;
            fileData.OccupancyType = (short)thirdPartyCodeList.GetByteProValue("PropertyUsage",
                                                                               loanApplication.PropertyInfo.PropertyUsageId).FindEnumIndex(typeof(OccupancyTypeEnum));
            return fileData;
        }

        public  void Update(LoanApplication loanApplication, ThirdPartyCodeList thirdPartyCodeList)
        {

            this.AgencyCaseNo = loanApplication.LoanNumber;
            this.OccupancyType = (short)thirdPartyCodeList.GetByteProValue("PropertyUsage",
                                                                               loanApplication.PropertyInfo.PropertyUsageId).FindEnumIndex(typeof(OccupancyTypeEnum));
             
        }
    }

    // FileIdentifier

    public class FileIdentifier
    {
        public int FileIdentifierId { get; set; } // FileIdentifierID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int FileIdentifierType { get; set; } // FileIdentifierType
        public string Value { get; set; } // Value (length: 50)

        public FileIdentifier()
        {
            FileDataId = 0;
            FileIdentifierType = 0;
        }
    }

    // FMACCreditAffiliate

    public class FmacCreditAffiliate
    {
        public int CreditAffiliateId { get; set; } // CreditAffiliateId (Primary key)
        public string CreditAffiliateCode { get; set; } // CreditAffiliateCode (length: 50)
        public string CreditAffiliateName { get; set; } // CreditAffiliateName (length: 100)
        public int CreditAgencyId { get; set; } // CreditAgencyId

        public FmacCreditAffiliate()
        {
            CreditAgencyId = 0;
        }
    }

    // FMACCreditAgency

    public class FmacCreditAgency
    {
        public int CreditAgencyId { get; set; } // CreditAgencyId (Primary key)
        public string CreditAgencyCode { get; set; } // CreditAgencyCode (length: 50)
        public string CreditAgencyName { get; set; } // CreditAgencyName (length: 50)
    }

    // FolderInfo

    public class FolderInfo
    {
        public int FolderInfoId { get; set; } // FolderInfoID (Primary key)
        public bool RequirePassword { get; set; } // RequirePassword
        public string EncryptedAdminPassword { get; set; } // EncryptedAdminPassword (length: 20)
        public short DefaultsSharingOption { get; set; } // DefaultsSharingOption
        public short ContactsSharingOption { get; set; } // ContactsSharingOption
        public short TemplatesSharingOption { get; set; } // TemplatesSharingOption
        public bool DisableAuditLogging { get; set; } // DisableAuditLogging
        public int? ForceLoginAfterIdleXMinutes { get; set; } // ForceLoginAfterIdleXMinutes
        public decimal? ForceOfflineReconnectionAfterXDays { get; set; } // ForceOfflineReconnectionAfterXDays
        public bool LogDataModifications { get; set; } // LogDataModifications
        public bool RoleOtherVisible { get; set; } // RoleOtherVisible
        public string RoleOtherDisplayName { get; set; } // RoleOtherDisplayName (length: 30)
        public bool RoleOther2Visible { get; set; } // RoleOther2Visible
        public string RoleOther2DisplayName { get; set; } // RoleOther2DisplayName (length: 30)
        public bool RoleOther3Visible { get; set; } // RoleOther3Visible
        public string RoleOther3DisplayName { get; set; } // RoleOther3DisplayName (length: 30)
        public bool RoleOther4Visible { get; set; } // RoleOther4Visible
        public string RoleOther4DisplayName { get; set; } // RoleOther4DisplayName (length: 30)
        public bool OtherDate1Visible { get; set; } // OtherDate1Visible
        public string OtherDate1DisplayName { get; set; } // OtherDate1DisplayName (length: 25)
        public bool OtherDate2Visible { get; set; } // OtherDate2Visible
        public string OtherDate2DisplayName { get; set; } // OtherDate2DisplayName (length: 25)
        public bool OtherDate3Visible { get; set; } // OtherDate3Visible
        public string OtherDate3DisplayName { get; set; } // OtherDate3DisplayName (length: 25)
        public bool UseAutoFileNaming { get; set; } // UseAutoFileNaming
        public long AutoFileNameNextNum { get; set; } // AutoFileNameNextNum
        public int AutoFileNameDigits { get; set; } // AutoFileNameDigits
        public short AutoFileNameOrgPlacement { get; set; } // AutoFileNameOrgPlacement
        public string AutoFileNamePrefix { get; set; } // AutoFileNamePrefix (length: 5)
        public string AutoFileNameSuffix { get; set; } // AutoFileNameSuffix (length: 5)
        public bool AutoGenerateMinNumber { get; set; } // AutoGenerateMINNumber
        public string MinNumberOrgId { get; set; } // MINNumberOrgID (length: 7)
        public string MinNumberPrefix { get; set; } // MINNumberPrefix (length: 5)
        public short MinNumberMethod { get; set; } // MINNumberMethod
        public long MinNumberNextNumber { get; set; } // MINNumberNextNumber
        public bool DatabaseLocked { get; set; } // DatabaseLocked
        public string DesiredCoName { get; set; } // DesiredCoName (length: 150)
        public string DesiredCoStreet1 { get; set; } // DesiredCoStreet1 (length: 50)
        public string DesiredCoStreet2 { get; set; } // DesiredCoStreet2 (length: 50)
        public string DesiredCoCity { get; set; } // DesiredCoCity (length: 50)
        public string DesiredCoState { get; set; } // DesiredCoState (length: 2)
        public string DesiredCoZip { get; set; } // DesiredCoZip (length: 9)
        public string DesiredCoPhone { get; set; } // DesiredCoPhone (length: 20)
        public string DesiredCoFax { get; set; } // DesiredCoFax (length: 20)
        public int MinPasswordLength { get; set; } // MinPasswordLength
        public bool FlagBlueVisible { get; set; } // FlagBlueVisible
        public bool FlagGreenVisible { get; set; } // FlagGreenVisible
        public bool FlagOrangeVisible { get; set; } // FlagOrangeVisible
        public bool FlagPurpleVisible { get; set; } // FlagPurpleVisible
        public bool FlagRedVisible { get; set; } // FlagRedVisible
        public bool FlagYellowVisible { get; set; } // FlagYellowVisible
        public bool FlagNoneVisible { get; set; } // FlagNoneVisible
        public bool StoreContactsInSqlServer { get; set; } // StoreContactsInSQLServer
        public string DocStorageFolder { get; set; } // DocStorageFolder (length: 1000)
        public string DocStorageUserName { get; set; } // DocStorageUserName (length: 50)
        public string DocStorageDomain { get; set; } // DocStorageDomain (length: 50)
        public string DocStoragePasswordEncrypted { get; set; } // DocStoragePasswordEncrypted (length: 200)
        public bool UseWindowsAuthentication { get; set; } // UseWindowsAuthentication
        public string DefaultDomainName { get; set; } // DefaultDomainName (length: 260)
        public short LicenseTracking { get; set; } // LicenseTracking
        public bool CompanyNameOnDocs { get; set; } // CompanyNameOnDocs
        public bool CompanyLicNoOnDocs { get; set; } // CompanyLicNoOnDocs
        public bool CompanyNmlsidOnDocs { get; set; } // CompanyNMLSIDOnDocs
        public bool BranchLicNoOnDocs { get; set; } // BranchLicNoOnDocs
        public bool BranchNmlsidOnDocs { get; set; } // BranchNMLSIDOnDocs
        public bool LoanOfficerNameOnDocs { get; set; } // LoanOfficerNameOnDocs
        public bool LoanOfficerLicNoOnDocs { get; set; } // LoanOfficerLicNoOnDocs
        public bool LoanOfficerNmlsidOnDocs { get; set; } // LoanOfficerNMLSIDOnDocs
        public byte[] LogoImageData { get; set; } // LogoImageData (length: 2147483647)
        public decimal? LogoAreaMaxWidth { get; set; } // LogoAreaMaxWidth
        public int StoreDocsWhenPrintingOption { get; set; } // StoreDocsWhenPrintingOption
        public int PasswordComplexity { get; set; } // PasswordComplexity
        public bool StoreServiceOrderingDocsOnDisk { get; set; } // StoreServiceOrderingDocsOnDisk
        public int DocumentInfoBarLocation { get; set; } // DocumentInfoBarLocation
        public bool IncludeAutoStoredDocsOnStoredDocsScreen { get; set; } // IncludeAutoStoredDocsOnStoredDocsScreen
        public int CompPlanEffectiveDateType { get; set; } // CompPlanEffectiveDateType
        public bool AllowMultiUserFileEditing { get; set; } // AllowMultiUserFileEditing
        public int DefaultDocumentStatus { get; set; } // DefaultDocumentStatus
        public int NewFileStatusRetail { get; set; } // NewFileStatusRetail
        public int NewFileStatusBroker { get; set; } // NewFileStatusBroker
        public int NewFileStatusCorrespondent { get; set; } // NewFileStatusCorrespondent
        public bool ApplyImageOptimization { get; set; } // ApplyImageOptimization
        public long AccountExecRole { get; set; } // AccountExecRole
        public System.DateTime? ServicesRefreshTime { get; set; } // ServicesRefreshTime
        public int MaximumPasswordAge { get; set; } // MaximumPasswordAge
        public bool ImageOptLevel1ConvertToGrayscale { get; set; } // ImageOptLevel1ConvertToGrayscale
        public int ImageOptLevel1CompressionLevel { get; set; } // ImageOptLevel1CompressionLevel
        public int ImageOptLevel1Resolution { get; set; } // ImageOptLevel1Resolution
        public bool ImageOptLevel2ConvertToGrayscale { get; set; } // ImageOptLevel2ConvertToGrayscale
        public int ImageOptLevel2CompressionLevel { get; set; } // ImageOptLevel2CompressionLevel
        public int ImageOptLevel2Resolution { get; set; } // ImageOptLevel2Resolution
        public int NewFileStatusMiniCorr { get; set; } // NewFileStatusMiniCorr
        public string ReportingDbServerName { get; set; } // ReportingDBServerName (length: 50)
        public string ReportingDbDatabaseName { get; set; } // ReportingDBDatabaseName (length: 50)
        public int StackingDocumentStatuses { get; set; } // StackingDocumentStatuses
        public int DocStorageLoginMode { get; set; } // DocStorageLoginMode
        public int UliAutoGenerationOption { get; set; } // ULIAutoGenerationOption
        public int WebPortalSessionTimeoutInMinutes { get; set; } // WebPortalSessionTimeoutInMinutes
        public int WebPortalMinUntilTimeoutWarning { get; set; } // WebPortalMinUntilTimeoutWarning
        public bool LogNewFileChanges { get; set; } // LogNewFileChanges

        public FolderInfo()
        {
            RequirePassword = false;
            DefaultsSharingOption = 0;
            ContactsSharingOption = 0;
            TemplatesSharingOption = 0;
            DisableAuditLogging = false;
            LogDataModifications = false;
            RoleOtherVisible = false;
            RoleOther2Visible = false;
            RoleOther3Visible = false;
            RoleOther4Visible = false;
            OtherDate1Visible = false;
            OtherDate2Visible = false;
            OtherDate3Visible = false;
            UseAutoFileNaming = false;
            AutoFileNameNextNum = 0;
            AutoFileNameDigits = 0;
            AutoFileNameOrgPlacement = 0;
            AutoGenerateMinNumber = false;
            MinNumberMethod = 0;
            MinNumberNextNumber = 0;
            DatabaseLocked = false;
            MinPasswordLength = 0;
            FlagBlueVisible = false;
            FlagGreenVisible = false;
            FlagOrangeVisible = false;
            FlagPurpleVisible = false;
            FlagRedVisible = false;
            FlagYellowVisible = false;
            FlagNoneVisible = false;
            StoreContactsInSqlServer = false;
            UseWindowsAuthentication = false;
            LicenseTracking = 0;
            CompanyNameOnDocs = false;
            CompanyLicNoOnDocs = false;
            CompanyNmlsidOnDocs = false;
            BranchLicNoOnDocs = false;
            BranchNmlsidOnDocs = false;
            LoanOfficerNameOnDocs = false;
            LoanOfficerLicNoOnDocs = false;
            LoanOfficerNmlsidOnDocs = false;
            StoreDocsWhenPrintingOption = 0;
            PasswordComplexity = 0;
            StoreServiceOrderingDocsOnDisk = false;
            DocumentInfoBarLocation = 0;
            IncludeAutoStoredDocsOnStoredDocsScreen = false;
            CompPlanEffectiveDateType = 0;
            AllowMultiUserFileEditing = false;
            DefaultDocumentStatus = 0;
            NewFileStatusRetail = 0;
            NewFileStatusBroker = 0;
            NewFileStatusCorrespondent = 0;
            ApplyImageOptimization = false;
            AccountExecRole = 0;
            MaximumPasswordAge = 0;
            ImageOptLevel1ConvertToGrayscale = false;
            ImageOptLevel1CompressionLevel = 0;
            ImageOptLevel1Resolution = 0;
            ImageOptLevel2ConvertToGrayscale = false;
            ImageOptLevel2CompressionLevel = 0;
            ImageOptLevel2Resolution = 0;
            NewFileStatusMiniCorr = 0;
            StackingDocumentStatuses = 0;
            DocStorageLoginMode = 0;
            UliAutoGenerationOption = 0;
            WebPortalSessionTimeoutInMinutes = 0;
            WebPortalMinUntilTimeoutWarning = 0;
            LogNewFileChanges = false;
        }
    }

    // FollowUpFlagInfo

    public class FollowUpFlagInfo
    {
        public int FollowUpFlagInfoId { get; set; } // FollowUpFlagInfoID (Primary key)
        public int FollowUpFlagType { get; set; } // FollowUpFlagType
        public string RedFlagName { get; set; } // RedFlagName (length: 50)
        public string BlueFlagName { get; set; } // BlueFlagName (length: 50)
        public string YellowFlagName { get; set; } // YellowFlagName (length: 50)
        public string GreenFlagName { get; set; } // GreenFlagName (length: 50)
        public string OrangeFlagName { get; set; } // OrangeFlagName (length: 50)
        public string PurpleFlagName { get; set; } // PurpleFlagName (length: 50)
        public bool RedFlagHidden { get; set; } // RedFlagHidden
        public bool BlueFlagHidden { get; set; } // BlueFlagHidden
        public bool YellowFlagHidden { get; set; } // YellowFlagHidden
        public bool GreenFlagHidden { get; set; } // GreenFlagHidden
        public bool OrangeFlagHidden { get; set; } // OrangeFlagHidden
        public bool PurpleFlagHidden { get; set; } // PurpleFlagHidden

        public FollowUpFlagInfo()
        {
            FollowUpFlagType = 0;
            RedFlagHidden = false;
            BlueFlagHidden = false;
            YellowFlagHidden = false;
            GreenFlagHidden = false;
            OrangeFlagHidden = false;
            PurpleFlagHidden = false;
        }
    }

    // Freddie

    public class Freddie
    {
        public int FreddieId { get; set; } // FreddieID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public short AffordableProgramId { get; set; } // AffordableProgramId
        public short OfferingId { get; set; } // OfferingId
        public string OtherOfferingIdentifier { get; set; } // OtherOfferingIdentifier (length: 3)
        public short LoanDocumentationType { get; set; } // LoanDocumentationType
        public short BuildingStatusType { get; set; } // BuildingStatusType
        public short OriginationProcessingPoint { get; set; } // OriginationProcessingPoint
        public string LoanKeyNumber { get; set; } // LoanKeyNumber (length: 50)
        public string EvaluationStatusType { get; set; } // EvaluationStatusType (length: 50)
        public int? NumberOfSubmissions { get; set; } // NumberOfSubmissions
        public System.DateTime? EvaluationDate { get; set; } // EvaluationDate
        public string CreditRiskType { get; set; } // CreditRiskType (length: 50)
        public string RepositorySource { get; set; } // RepositorySource (length: 50)
        public string EvaluationType { get; set; } // EvaluationType (length: 50)
        public System.DateTime? SubmissionDate { get; set; } // SubmissionDate
        public string PurchaseEligibility { get; set; } // PurchaseEligibility (length: 50)
        public decimal? InitialLtv { get; set; } // InitialLTV
        public string TransactionNumber { get; set; } // TransactionNumber (length: 50)
        public decimal? InitialTltv { get; set; } // InitialTLTV
        public string DocumentClassification { get; set; } // DocumentClassification (length: 50)
        public string RiskGrade { get; set; } // RiskGrade (length: 10)
        public decimal? LossCoverageEstimate { get; set; } // LossCoverageEstimate
        public string MiDecisioin { get; set; } // MIDecisioin (length: 50)
        public int? CreditScore { get; set; } // CreditScore
        public string CreditRiskComment { get; set; } // CreditRiskComment (length: 50)
        public string RepositoryReason { get; set; } // RepositoryReason (length: 50)
        public short BuydownContributor { get; set; } // BuydownContributor
        public string SellerNumber { get; set; } // SellerNumber (length: 50)
        public string TpoNumber { get; set; } // TPONumber (length: 50)
        public string Notp { get; set; } // NOTP (length: 50)
        public short ConstructionPurpose { get; set; } // ConstructionPurpose
        public decimal? FreReserves { get; set; } // FREReserves
        public string LoanIdentifier { get; set; } // LoanIdentifier (length: 50)
        public string TransactionIdentifier { get; set; } // TransactionIdentifier (length: 50)
        public string LenderRegistrationNumber { get; set; } // LenderRegistrationNumber (length: 50)
        public string CreditReferenceNo { get; set; } // CreditReferenceNo (length: 50)
        public short NewConstruction { get; set; } // NewConstruction
        public string CpmProjectId { get; set; } // CPMProjectID (length: 50)
        public short PropertyCategoryType { get; set; } // PropertyCategoryType
        public short OwnershipType { get; set; } // OwnershipType
        public string OriginationProcessingPointOther { get; set; } // OriginationProcessingPointOther (length: 50)
        public string PropertyCategoryTypeOther { get; set; } // PropertyCategoryTypeOther (length: 50)
        public string OwnershipTypeOther { get; set; } // OwnershipTypeOther (length: 50)
        public string BuydownContributorOther { get; set; } // BuydownContributorOther (length: 50)
        public string BuildingStatusTypeOther { get; set; } // BuildingStatusTypeOther (length: 50)
        public bool OrderMergedCredit { get; set; } // OrderMergedCredit
        public decimal? FhaBorrowerClosingCosts { get; set; } // FHABorrowerClosingCosts
        public decimal? FhaFinancedDiscountPoints { get; set; } // FHAFinancedDiscountPoints
        public decimal? VaResidualIncome { get; set; } // VAResidualIncome
        public string CreditAgencyCode { get; set; } // CreditAgencyCode (length: 50)
        public string CreditAffiliateCode { get; set; } // CreditAffiliateCode (length: 50)
        public int? FreMonthsInReserveOv { get; set; } // FREMonthsInReserveOV
        public int AppraisalMethodType { get; set; } // AppraisalMethodType

        public Freddie()
        {
            FileDataId = 0;
            AffordableProgramId = 0;
            OfferingId = 0;
            LoanDocumentationType = 0;
            BuildingStatusType = 0;
            OriginationProcessingPoint = 0;
            BuydownContributor = 0;
            ConstructionPurpose = 0;
            NewConstruction = 0;
            PropertyCategoryType = 0;
            OwnershipType = 0;
            OrderMergedCredit = false;
            AppraisalMethodType = 0;
        }
    }

    // Gift

    public class Gift
    {
        public int GiftId { get; set; } // GiftID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int BorrowerId { get; set; } // BorrowerID
        public byte AssetType { get; set; } // AssetType
        public decimal? GiftAmount { get; set; } // GiftAmount
        public byte DepositedStatus { get; set; } // DepositedStatus
        public byte Source { get; set; } // Source
        public string DonorRelationshipToBorrowerDesc { get; set; } // DonorRelationshipToBorrowerDesc (length: 50)
        public string DonorName1 { get; set; } // DonorName1 (length: 50)
        public string DonorName2 { get; set; } // DonorName2 (length: 50)
        public string DonorStreet { get; set; } // DonorStreet (length: 50)
        public string DonorCity { get; set; } // DonorCity (length: 50)
        public string DonorState { get; set; } // DonorState (length: 2)
        public string DonorZip { get; set; } // DonorZip (length: 9)
        public string DonorPhone { get; set; } // DonorPhone (length: 20)
        public string DonorAccountInstitution { get; set; } // DonorAccountInstitution (length: 50)
        public string DonorInstitutionAddress { get; set; } // DonorInstitutionAddress (length: 100)
        public string DonorAccountNo { get; set; } // DonorAccountNo (length: 30)
        public byte FundsProvidedAtClosing { get; set; } // FundsProvidedAtClosing
        public System.DateTime? TransferDate { get; set; } // TransferDate
        public string SourceOtherDesc { get; set; } // SourceOtherDesc (length: 80)
        public int AccountHeldByType { get; set; } // AccountHeldByType

        public Gift()
        {
            FileDataId = 0;
            BorrowerId = 0;
            AssetType = 0;
            DepositedStatus = 0;
            Source = 0;
            FundsProvidedAtClosing = 0;
            AccountHeldByType = 0;
        }
    }

    // HAMP

    public class Hamp
    {
        public int Hampid { get; set; } // HAMPID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public string HardshipExplanation { get; set; } // HardshipExplanation (length: 2147483647)
        public int PreviouslyModifiedUnderHamp { get; set; } // PreviouslyModifiedUnderHAMP
        public System.DateTime? DateOfImminentDefault { get; set; } // DateOfImminentDefault
        public int Chapter7DischargeAndDidNotReaffirmMortgage { get; set; } // Chapter7DischargeAndDidNotReaffirmMortgage
        public int CurrentLoanHasEscrowAccount { get; set; } // CurrentLoanHasEscrowAccount
        public int ImminentDefaultDetermined { get; set; } // ImminentDefaultDetermined
        public string ImminentDefaultExplanation { get; set; } // ImminentDefaultExplanation (length: 2147483647)
        public System.DateTime? OriginalNoteDate { get; set; } // OriginalNoteDate
        public string ServicerLoanNumber { get; set; } // ServicerLoanNumber (length: 50)
        public decimal? EscrowShortageAmount { get; set; } // EscrowShortageAmount
        public System.DateTime? SolicitationDeadlineOv { get; set; } // SolicitationDeadlineOV
        public int IsMersMortgageeOfRecord { get; set; } // IsMERSMortgageeOfRecord
        public int IsMbsLoan { get; set; } // IsMBSLoan
        public System.DateTime? MbsRequestedRemovalDate { get; set; } // MBSRequestedRemovalDate
        public System.DateTime? MbsActualRemovalDate { get; set; } // MBSActualRemovalDate
        public decimal? OriginalIntRate { get; set; } // OriginalIntRate
        public int? OriginalRemainingTerm { get; set; } // OriginalRemainingTerm
        public int EligibleForHarp { get; set; } // EligibleForHARP
        public int OwnerOfMortgage { get; set; } // OwnerOfMortgage
        public int OtherHardshipApproved { get; set; } // OtherHardshipApproved
        public bool IncomeVerified { get; set; } // IncomeVerified
        public System.DateTime? DocRequestSentDate { get; set; } // DocRequestSentDate
        public System.DateTime? DocRequestDueByDate { get; set; } // DocRequestDueByDate
        public System.DateTime? PaymentPostedDate1 { get; set; } // PaymentPostedDate1
        public System.DateTime? PaymentPostedDate2 { get; set; } // PaymentPostedDate2
        public System.DateTime? PaymentPostedDate3 { get; set; } // PaymentPostedDate3
        public System.DateTime? PaymentPostedDate4 { get; set; } // PaymentPostedDate4
        public decimal? PaymentReceivedAmount1 { get; set; } // PaymentReceivedAmount1
        public decimal? PaymentReceivedAmount2 { get; set; } // PaymentReceivedAmount2
        public decimal? PaymentReceivedAmount3 { get; set; } // PaymentReceivedAmount3
        public decimal? PaymentReceivedAmount4 { get; set; } // PaymentReceivedAmount4
        public bool EscrowAccountEstablished { get; set; } // EscrowAccountEstablished
        public System.DateTime? NpvDate { get; set; } // NPVDate
        public decimal? NpvResultPreMod { get; set; } // NPVResultPreMod
        public decimal? NpvResultPostMod { get; set; } // NPVResultPostMod
        public decimal? IncomeUsedInTrialPlan { get; set; } // IncomeUsedInTrialPlan
        public int TrialPlanIncomeType { get; set; } // TrialPlanIncomeType
        public System.DateTime? TrialPlanSentDate { get; set; } // TrialPlanSentDate
        public System.DateTime? ModEffectiveDate { get; set; } // ModEffectiveDate
        public decimal? MarketSurveyRateOv { get; set; } // MarketSurveyRateOV
        public System.DateTime? ModAgreementPrepDate { get; set; } // ModAgreementPrepDate
        public System.DateTime? ModAgreementSentDate { get; set; } // ModAgreementSentDate
        public System.DateTime? ModAgreementDueDate { get; set; } // ModAgreementDueDate
        public System.DateTime? ModAgreementDueExtensionDate { get; set; } // ModAgreementDueExtensionDate
        public System.DateTime? ModAgreementReceivedDate { get; set; } // ModAgreementReceivedDate
        public System.DateTime? SchedModEffectiveDate { get; set; } // SchedModEffectiveDate
        public int ModRequiresRecordingOv { get; set; } // ModRequiresRecordingOV
        public System.DateTime? TrialPlanReceivedDate { get; set; } // TrialPlanReceivedDate
        public System.DateTime? DocRequestReceivedDate { get; set; } // DocRequestReceivedDate
        public System.DateTime? ValueAnalysisDate { get; set; } // ValueAnalysisDate
        public System.DateTime? CurrentLpiDate { get; set; } // CurrentLPIDate
        public decimal? UpbBeforeMod { get; set; } // UPBBeforeMod
        public decimal? OtherContributions { get; set; } // OtherContributions
        public decimal? AttorneyFeesNotInEscrow { get; set; } // AttorneyFeesNotInEscrow
        public decimal? EscrowShortageForAdvances { get; set; } // EscrowShortageForAdvances
        public decimal? OtherAdvances { get; set; } // OtherAdvances
        public decimal? BorrowerContributions { get; set; } // BorrowerContributions
        public decimal? InterestOwedOrPaymentNotReported { get; set; } // InterestOwedOrPaymentNotReported
        public decimal? PrincipalPaymentOrOwedNotReported { get; set; } // PrincipalPaymentOrOwedNotReported
        public decimal? DelinquentInterest { get; set; } // DelinquentInterest
        public decimal? ModServicingFeePercent { get; set; } // ModServicingFeePercent
        public System.DateTime? ModOfficerSignatureDate { get; set; } // ModOfficerSignatureDate
        public bool AllowCustomWaterfall { get; set; } // AllowCustomWaterfall
        public string CustomWaterfallName { get; set; } // CustomWaterfallName (length: 50)
        public string UnderlyingTrustId { get; set; } // UnderlyingTrustID (length: 9)
        public int ProgramType { get; set; } // ProgramType
        public System.DateTime? TrialPlanExecutionDateBor { get; set; } // TrialPlanExecutionDateBor
        public System.DateTime? TrialPlanExecutionDateServicer { get; set; } // TrialPlanExecutionDateServicer
        public System.DateTime? ModAgreementExecutionDateBor { get; set; } // ModAgreementExecutionDateBor
        public System.DateTime? ModAgreementExecutionDateServicer { get; set; } // ModAgreementExecutionDateServicer
        public int SubmissionStatus { get; set; } // SubmissionStatus
        public int HampMortgageType { get; set; } // HAMPMortgageType
        public int HampLienType { get; set; } // HAMPLienType
        public int? OriginalTerm { get; set; } // OriginalTerm
        public int? OriginalBalloonTerm { get; set; } // OriginalBalloonTerm
        public decimal? EscrowPaymentBeforeModOv { get; set; } // EscrowPaymentBeforeModOV
        public decimal? EscrowPaymentAfterModOv { get; set; } // EscrowPaymentAfterModOV
        public decimal? DisbursementForgiven { get; set; } // DisbursementForgiven
        public decimal? HousingExpenseBeforeModOv { get; set; } // HousingExpenseBeforeModOV
        public decimal? HousingExpenseAfterModOv { get; set; } // HousingExpenseAfterModOV
        public int ValuationAnalysisMethod { get; set; } // ValuationAnalysisMethod
        public int PropertyConditionCode { get; set; } // PropertyConditionCode
        public System.DateTime? LpiDateAfterMod { get; set; } // LPIDateAfterMod
        public decimal? PrincipalForgiveness { get; set; } // PrincipalForgiveness
        public int SubordinateLienPaydownCode { get; set; } // SubordinateLienPaydownCode
        public decimal? SubordinateLienPaydownAmount { get; set; } // SubordinateLienPaydownAmount
        public decimal? TrialPeriodPayment { get; set; } // TrialPeriodPayment
        public System.DateTime? PaymentPostedDate5 { get; set; } // PaymentPostedDate5
        public decimal? PaymentReceivedAmount5 { get; set; } // PaymentReceivedAmount5
        public decimal? EscrowCushionAmount { get; set; } // EscrowCushionAmount
        public int DelinquencyHardshipReason { get; set; } // DelinquencyHardshipReason
        public decimal? IntRateCapOv { get; set; } // IntRateCapOV
        public int OriginalProductType { get; set; } // OriginalProductType
        public int HampOccupancyCode { get; set; } // HAMPOccupancyCode
        public int RecordationRequired { get; set; } // RecordationRequired
        public decimal? OriginalPi { get; set; } // OriginalPI
        public bool ExtendTrialPeriodByOneMonth { get; set; } // ExtendTrialPeriodByOneMonth
        public int OtherHardshipRequired { get; set; } // OtherHardshipRequired
        public int EscrowAccountCannotBeEst { get; set; } // EscrowAccountCannotBeEst
        public int SubAgreementOrTitlePolRequired { get; set; } // SubAgreementOrTitlePolRequired
        public int NoteMayBeAssumed { get; set; } // NoteMayBeAssumed
        public System.DateTime? PaymentDueDate1 { get; set; } // PaymentDueDate1
        public System.DateTime? PaymentDueDate2 { get; set; } // PaymentDueDate2
        public System.DateTime? TrialPlanPrepDate { get; set; } // TrialPlanPrepDate
        public int OriginalLoanHasPrepayPenalty { get; set; } // OriginalLoanHasPrepayPenalty
        public System.DateTime? NpvDataCollectionDate { get; set; } // NPVDataCollectionDate
        public System.DateTime? FirstPaymentDateAtOrigination { get; set; } // FirstPaymentDateAtOrigination
        public decimal? LoanAmountAtOrigination { get; set; } // LoanAmountAtOrigination
        public decimal? IntRateAtOrigination { get; set; } // IntRateAtOrigination
        public decimal? OriginalPurPrice { get; set; } // OriginalPurPrice
        public decimal? AppraisedValueAtOrigination { get; set; } // AppraisedValueAtOrigination
        public int OriginalProductTypeNpv { get; set; } // OriginalProductTypeNPV
        public System.DateTime? ArmResetDate { get; set; } // ARMResetDate
        public decimal? FeesReimbursedByInvestor { get; set; } // FeesReimbursedByInvestor
        public decimal? MiPartialClaimAmount { get; set; } // MIPartialClaimAmount
        public System.DateTime? PaymentDueDate3 { get; set; } // _PaymentDueDate3
        public System.DateTime? PaymentDueDate4 { get; set; } // _PaymentDueDate4
        public System.DateTime? PaymentDueDate5 { get; set; } // _PaymentDueDate5
        public int TrialPeriodPaymentCountAsExtended { get; set; } // _TrialPeriodPaymentCountAsExtended
        public decimal? ModIntRateCap { get; set; } // _ModIntRateCap
        public System.DateTime? ModMaturityDate { get; set; } // _ModMaturityDate
        public bool ReportedTrial { get; set; } // ReportedTrial
        public bool ReportedModification { get; set; } // ReportedModification
        public bool ReportedPayment2 { get; set; } // ReportedPayment2
        public bool ReportedPayment3 { get; set; } // ReportedPayment3
        public bool ReportedPayment4 { get; set; } // ReportedPayment4
        public bool ReportedPayment5 { get; set; } // ReportedPayment5
        public bool ReportedBorrowerDisqualified { get; set; } // ReportedBorrowerDisqualified
        public bool ReportedForeclosureMit { get; set; } // ReportedForeclosureMit
        public bool ReportedCancel { get; set; } // ReportedCancel
        public System.DateTime? OriginalMaturityDate { get; set; } // OriginalMaturityDate
        public bool FirstPaymentDateNotFirstOfMonth { get; set; } // FirstPaymentDateNotFirstOfMonth
        public decimal? ArmResetIntRate { get; set; } // ARMResetIntRate
        public System.DateTime? ForeclosureReferralDate { get; set; } // ForeclosureReferralDate
        public System.DateTime? ProjectedForeclosureSaleDate { get; set; } // ProjectedForeclosureSaleDate
        public int PeriodCount { get; set; } // _PeriodCount
        public decimal IncentiveMonthly { get; set; } // _IncentiveMonthly
        public bool ExtendModEffectiveDateByOneMonth { get; set; } // ExtendModEffectiveDateByOneMonth
        public int NpvModelVersion { get; set; } // NPVModelVersion
        public int HampPropertyType { get; set; } // HAMPPropertyType
        public bool HmdaDataNotAvailable { get; set; } // HMDADataNotAvailable
        public int NpvModelType { get; set; } // NPVModelType
        public int HmdaConsentProvided { get; set; } // HMDAConsentProvided
        public int TrialNotApprovedReasonCode { get; set; } // TrialNotApprovedReasonCode
        public int TrialFalloutReasonCode { get; set; } // TrialFalloutReasonCode
        public int LoanDocsOmittedEscrowProvisions { get; set; } // LoanDocsOmittedEscrowProvisions
        public bool ReportedOfficialCorrection { get; set; } // ReportedOfficialCorrection
        public bool ReportedOfficialCancel { get; set; } // ReportedOfficialCancel
        public bool ReportedHmdaData { get; set; } // ReportedHMDAData
        public decimal? FhaPriorPartialClaimAmount { get; set; } // FHAPriorPartialClaimAmount
        public int AcceptedSolutionType { get; set; } // AcceptedSolutionType
        public decimal? AltUpbAfterMod { get; set; } // AltUPBAfterMod
        public decimal? AltIntRateAfterMod { get; set; } // AltIntRateAfterMod
        public int? AltTermAfterMod { get; set; } // AltTermAfterMod
        public decimal? AltForbearance { get; set; } // AltForbearance
        public decimal? AltPrincipalReduction { get; set; } // AltPrincipalReduction
        public int? MaxMonthsPastDueInPast12Months { get; set; } // MaxMonthsPastDueInPast12Months
        public decimal? NpvResultPreModPra { get; set; } // NPVResultPreModPRA
        public decimal? NpvResultPostModPra { get; set; } // NPVResultPostModPRA
        public System.DateTime? UpBorrowerRequestDate { get; set; } // UPBorrowerRequestDate
        public System.DateTime? UpForbearancePlanEffectiveDate { get; set; } // UPForbearancePlanEffectiveDate
        public System.DateTime? UpForbearancePlanNoticeSentDate { get; set; } // UPForbearancePlanNoticeSentDate
        public System.DateTime? UpForbearancePlanExpirationDate { get; set; } // UPForbearancePlanExpirationDate
        public int? UpForbearancePlanTerm { get; set; } // UPForbearancePlanTerm
        public decimal? UpPaymentAmount { get; set; } // UPPaymentAmount
        public string UpNotes { get; set; } // UPNotes (length: 2147483647)
        public int UpCompletedDisposition { get; set; } // UPCompletedDisposition
        public int UpInvestor { get; set; } // UPInvestor
        public int UpSource { get; set; } // UPSource
        public int UpStatus { get; set; } // UPStatus
        public System.DateTime? UpBenefitsStartDate { get; set; } // UPBenefitsStartDate
        public System.DateTime? UpBenefitsEndDate { get; set; } // UPBenefitsEndDate
        public System.DateTime? UpUnemploymentStartDate { get; set; } // UPUnemploymentStartDate
        public decimal? UpPaymentReductionPerc { get; set; } // _UPPaymentReductionPerc
        public System.DateTime? UpRequestConfirmationDate { get; set; } // UPRequestConfirmationDate
        public System.DateTime? UpEligibilityDeterminationDate { get; set; } // UPEligibilityDeterminationDate
        public System.DateTime? UpNonApprovalNoticeSentDate { get; set; } // UPNonApprovalNoticeSentDate
        public decimal? UpTotalMonthlyPayment { get; set; } // UPTotalMonthlyPayment
        public decimal? MinimumNetReturnAmount { get; set; } // MinimumNetReturnAmount
        public short MiWaiverCode { get; set; } // MIWaiverCode
        public System.DateTime? PropertyVacancyDate { get; set; } // PropertyVacancyDate
        public short SsCancellationReasonCode { get; set; } // SSCancellationReasonCode
        public short DilCancellationReasonCode { get; set; } // DILCancellationReasonCode
        public System.DateTime? TransactionClosingDate { get; set; } // TransactionClosingDate
        public System.DateTime? SsAgreementExpirationDate { get; set; } // SSAgreementExpirationDate
        public System.DateTime? DilAgreementExpirationDate { get; set; } // DILAgreementExpirationDate
        public System.DateTime? SsAgreementIssueDate { get; set; } // SSAgreementIssueDate
        public System.DateTime? DilAgreementIssueDate { get; set; } // DILAgreementIssueDate
        public short LoanDelinquencyType { get; set; } // LoanDelinquencyType
        public decimal? FinalUpbAmount { get; set; } // FinalUPBAmount
        public System.DateTime? SsBorExecutionDate { get; set; } // SSBorExecutionDate
        public System.DateTime? DilBorExecutionDate { get; set; } // DILBorExecutionDate
        public System.DateTime? SsOrDilReasonDate { get; set; } // SSOrDILReasonDate
        public short SsOrDilReasonCode { get; set; } // SSOrDILReasonCode
        public System.DateTime? SsAgreementDueDate { get; set; } // SSAgreementDueDate
        public bool HardshipAffidavitRequired { get; set; } // HardshipAffidavitRequired
        public decimal? PartialPaymentAmount { get; set; } // PartialPaymentAmount
        public System.DateTime? FirstPartialPaymentDueDate { get; set; } // FirstPartialPaymentDueDate
        public System.DateTime? AlternateRssaSentDate { get; set; } // AlternateRSSASentDate
        public System.DateTime? AlternateRssaDueDate { get; set; } // AlternateRSSADueDate
        public System.DateTime? HafaSolicitationSentDate { get; set; } // HAFASolicitationSentDate
        public System.DateTime? HafaEvaluationDate { get; set; } // HAFAEvaluationDate
        public string HafaEvaluationExplanation { get; set; } // HAFAEvaluationExplanation (length: 1000)
        public short ShortSaleStatus { get; set; } // ShortSaleStatus
        public short InvestorApprovalWithForeclosure { get; set; } // InvestorApprovalWithForeclosure
        public string HafaTitleNotes { get; set; } // HAFATitleNotes (length: 1000)
        public decimal? EstOtherAllowableCosts { get; set; } // EstOtherAllowableCosts
        public decimal? MarginForListPrice { get; set; } // MarginForListPrice
        public string ListPriceHistory { get; set; } // ListPriceHistory (length: 1000)
        public int? ExtensionPeriod { get; set; } // ExtensionPeriod
        public string ExtensionHistory { get; set; } // ExtensionHistory (length: 1000)
        public System.DateTime? RssaReceivedDate { get; set; } // RSSAReceivedDate
        public decimal? PurchaseOfferAmount { get; set; } // PurchaseOfferAmount
        public string PurchaseOfferHistory { get; set; } // PurchaseOfferHistory (length: 1000)
        public short PurchaseOfferApproved { get; set; } // PurchaseOfferApproved
        public bool D4LEvaluated { get; set; } // D4LEvaluated
        public bool D4LAccepted { get; set; } // D4LAccepted
        public bool D4LReleaseOfClaimsReceived { get; set; } // D4LReleaseOfClaimsReceived
        public decimal? SubLienReimbursementAmount { get; set; } // _SubLienReimbursementAmount
        public decimal? SubLienReimbursementAmountOv { get; set; } // SubLienReimbursementAmountOV
        public System.DateTime? SsCancellationDate { get; set; } // SSCancellationDate
        public bool RssaDeclinedDidNotComply { get; set; } // RSSADeclinedDidNotComply
        public string RssaDeclinedDidNotComplyText { get; set; } // RSSADeclinedDidNotComplyText (length: 500)
        public bool RssaDeclinedNotComplete { get; set; } // RSSADeclinedNotComplete
        public bool RssaDeclinedProceedsInsufficient { get; set; } // RSSADeclinedProceedsInsufficient
        public string RssaDeclinedOtherText { get; set; } // RSSADeclinedOtherText (length: 500)
        public bool RssaDeclinedOther { get; set; } // RSSADeclinedOther
        public bool RssaNotCompletedContract { get; set; } // RSSANotCompletedContract
        public bool RssaNotCompletedBuyersDoc { get; set; } // RSSANotCompletedBuyersDoc
        public bool RssaInsufficientSalesPrice { get; set; } // RSSAInsufficientSalesPrice
        public bool RssaInsufficientNetProceeds { get; set; } // RSSAInsufficientNetProceeds
        public bool RssaInsufficientExConcessions { get; set; } // RSSAInsufficientExConcessions
        public bool RssaInsufficientExCommisions { get; set; } // RSSAInsufficientExCommisions
        public bool RssaInsufficientExClosingCosts { get; set; } // RSSAInsufficientExClosingCosts
        public bool RssaInsufficientExSubLienObligations { get; set; } // RSSAInsufficientExSubLienObligations
        public System.DateTime? PartialPaymentStartDate { get; set; } // PartialPaymentStartDate
        public System.DateTime? DilCancelledDate { get; set; } // DILCancelledDate
        public System.DateTime? DilAgreementDueDate { get; set; } // DILAgreementDueDate
        public System.DateTime? MarketingPeriodExpirationDate { get; set; } // MarketingPeriodExpirationDate
        public decimal? PropertyListPrice { get; set; } // PropertyListPrice
        public decimal? TotalAllowableCostAmount { get; set; } // TotalAllowableCostAmount
        public decimal? SalesCommissionPerc { get; set; } // SalesCommissionPerc
        public decimal? SalesCommission { get; set; } // SalesCommission
        public short HampDelinquencyTypeCode { get; set; } // HAMPDelinquencyTypeCode
        public short ForbearancePlanType { get; set; } // ForbearancePlanType
        public short PrincipalReductionAlternativeCode { get; set; } // PrincipalReductionAlternativeCode
        public short RestrictionForAltWaterfallTypeCode { get; set; } // RestrictionForAltWaterfallTypeCode
        public short SupplementaryAssistCode { get; set; } // SupplementaryAssistCode
        public decimal? UpPaymentReductionAmount { get; set; } // _UPPaymentReductionAmount
        public string SsCancellationNotes { get; set; } // SSCancellationNotes (length: 1000)
        public System.DateTime? AlternateRssaExpirationDate { get; set; } // AlternateRSSAExpirationDate
        public decimal? ManpPerc { get; set; } // MANPPerc
        public short ManpBasis { get; set; } // MANPBasis
        public int FirstLienHelocWithRightToNewFunds { get; set; } // FirstLienHELOCWithRightToNewFunds
        public bool WeServiceSecondLien { get; set; } // WeServiceSecondLien

        public Hamp()
        {
            FileDataId = 0;
            PreviouslyModifiedUnderHamp = 0;
            Chapter7DischargeAndDidNotReaffirmMortgage = 0;
            CurrentLoanHasEscrowAccount = 0;
            ImminentDefaultDetermined = 0;
            IsMersMortgageeOfRecord = 0;
            IsMbsLoan = 0;
            EligibleForHarp = 0;
            OwnerOfMortgage = 0;
            OtherHardshipApproved = 0;
            IncomeVerified = false;
            EscrowAccountEstablished = false;
            TrialPlanIncomeType = 0;
            ModRequiresRecordingOv = 0;
            AllowCustomWaterfall = false;
            ProgramType = 0;
            SubmissionStatus = 0;
            HampMortgageType = 0;
            HampLienType = 0;
            ValuationAnalysisMethod = 0;
            PropertyConditionCode = 0;
            SubordinateLienPaydownCode = 0;
            DelinquencyHardshipReason = 0;
            OriginalProductType = 0;
            HampOccupancyCode = 0;
            RecordationRequired = 0;
            ExtendTrialPeriodByOneMonth = false;
            OtherHardshipRequired = 0;
            EscrowAccountCannotBeEst = 0;
            SubAgreementOrTitlePolRequired = 0;
            NoteMayBeAssumed = 0;
            OriginalLoanHasPrepayPenalty = 0;
            OriginalProductTypeNpv = 0;
            TrialPeriodPaymentCountAsExtended = 0;
            ReportedTrial = false;
            ReportedModification = false;
            ReportedPayment2 = false;
            ReportedPayment3 = false;
            ReportedPayment4 = false;
            ReportedPayment5 = false;
            ReportedBorrowerDisqualified = false;
            ReportedForeclosureMit = false;
            ReportedCancel = false;
            FirstPaymentDateNotFirstOfMonth = false;
            PeriodCount = 0;
            IncentiveMonthly = 0m;
            ExtendModEffectiveDateByOneMonth = false;
            NpvModelVersion = 0;
            HampPropertyType = 0;
            HmdaDataNotAvailable = false;
            NpvModelType = 0;
            HmdaConsentProvided = 0;
            TrialNotApprovedReasonCode = 0;
            TrialFalloutReasonCode = 0;
            LoanDocsOmittedEscrowProvisions = 0;
            ReportedOfficialCorrection = false;
            ReportedOfficialCancel = false;
            ReportedHmdaData = false;
            AcceptedSolutionType = 0;
            UpCompletedDisposition = 0;
            UpInvestor = 0;
            UpSource = 0;
            UpStatus = 0;
            MiWaiverCode = 0;
            SsCancellationReasonCode = 0;
            DilCancellationReasonCode = 0;
            LoanDelinquencyType = 0;
            SsOrDilReasonCode = 0;
            HardshipAffidavitRequired = false;
            ShortSaleStatus = 0;
            InvestorApprovalWithForeclosure = 0;
            PurchaseOfferApproved = 0;
            D4LEvaluated = false;
            D4LAccepted = false;
            D4LReleaseOfClaimsReceived = false;
            RssaDeclinedDidNotComply = false;
            RssaDeclinedNotComplete = false;
            RssaDeclinedProceedsInsufficient = false;
            RssaDeclinedOther = false;
            RssaNotCompletedContract = false;
            RssaNotCompletedBuyersDoc = false;
            RssaInsufficientSalesPrice = false;
            RssaInsufficientNetProceeds = false;
            RssaInsufficientExConcessions = false;
            RssaInsufficientExCommisions = false;
            RssaInsufficientExClosingCosts = false;
            RssaInsufficientExSubLienObligations = false;
            HampDelinquencyTypeCode = 0;
            ForbearancePlanType = 0;
            PrincipalReductionAlternativeCode = 0;
            RestrictionForAltWaterfallTypeCode = 0;
            SupplementaryAssistCode = 0;
            ManpBasis = 0;
            FirstLienHelocWithRightToNewFunds = 0;
            WeServiceSecondLien = false;
        }
    }

    // HAMPWaterfall

    public class HampWaterfall
    {
        public int HampWaterfallId { get; set; } // HAMPWaterfallID (Primary key)
        public string HampWaterfallName { get; set; } // HAMPWaterfallName (length: 50)
        public decimal? TargetDti { get; set; } // TargetDTI
        public decimal? IntRateCap { get; set; } // IntRateCap
        public int? ArmIntRateFixedFor { get; set; } // ARMIntRateFixedFor
        public int? ArmIntRateAdjustAt { get; set; } // ARMIntRateAdjustAt
        public decimal? ArmAdjustCapFirst { get; set; } // ARMAdjustCapFirst
        public decimal? ArmAdjustCapSubsequent { get; set; } // ARMAdjustCapSubsequent
    }

    // HAMPWaterfallStep

    public class HampWaterfallStep
    {
        public int HampWaterfallStepId { get; set; } // HAMPWaterfallStepID (Primary key)
        public int HampWaterfallId { get; set; } // HAMPWaterfallID
        public int WaterfallStepType { get; set; } // WaterfallStepType
        public int? MaxTerm { get; set; } // MaxTerm
        public int ForbearanceLimitType { get; set; } // ForbearanceLimitType
        public decimal? ForbearanceDollarLimit { get; set; } // ForbearanceDollarLimit
        public decimal? ForbearancePercentLimit { get; set; } // ForbearancePercentLimit
        public decimal? ForbearanceDollarIncrement { get; set; } // ForbearanceDollarIncrement
        public int DisplayOrder { get; set; } // DisplayOrder
        public decimal? IntRateFloor { get; set; } // IntRateFloor
        public decimal? IntRateIncrement { get; set; } // IntRateIncrement

        public HampWaterfallStep()
        {
            HampWaterfallId = 0;
            WaterfallStepType = 0;
            ForbearanceLimitType = 0;
            DisplayOrder = 0;
        }
    }

    // HELOCPeriod

    public class HelocPeriod
    {
        public int HelocPeriodId { get; set; } // HELOCPeriodID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int? LoanId { get; set; } // LoanID
        public short HelocPeriodType { get; set; } // HELOCPeriodType
        public int? Term { get; set; } // Term
        public decimal? Rate { get; set; } // Rate
        public decimal? PaymentPercent { get; set; } // PaymentPercent

        public HelocPeriod()
        {
            FileDataId = 0;
            HelocPeriodType = 0;
        }
    }

    // HMDA

    public class Hmda
    {
        public int Hmdaid { get; set; } // HMDAID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public short PropertyType { get; set; } // PropertyType
        public short ActionTaken { get; set; } // ActionTaken
        public System.DateTime? ActionDate { get; set; } // ActionDate
        public string CountyCode { get; set; } // CountyCode (length: 3)
        public string CensusTract { get; set; } // CensusTract (length: 7)
        public bool GrossAnnualIncomeNa { get; set; } // GrossAnnualIncomeNA
        public bool StateCodeNa { get; set; } // StateCodeNA
        public bool ApplicationDateNa { get; set; } // ApplicationDateNA
        public bool CountyCodeNa { get; set; } // CountyCodeNA
        public bool CensusTractNa { get; set; } // CensusTractNA
        public bool MsaNoNa { get; set; } // MSANoNA
        public short ApplicantEthnicity { get; set; } // _ApplicantEthnicity
        public short CoApplicantEthnicity { get; set; } // _CoApplicantEthnicity
        public short ApplicantRace1 { get; set; } // _ApplicantRace1
        public short ApplicantRace2 { get; set; } // _ApplicantRace2
        public short ApplicantRace3 { get; set; } // _ApplicantRace3
        public short ApplicantRace4 { get; set; } // _ApplicantRace4
        public short ApplicantRace5 { get; set; } // _ApplicantRace5
        public short CoApplicantRace1 { get; set; } // _CoApplicantRace1
        public short CoApplicantRace2 { get; set; } // _CoApplicantRace2
        public short CoApplicantRace3 { get; set; } // _CoApplicantRace3
        public short CoApplicantRace4 { get; set; } // _CoApplicantRace4
        public short CoApplicantRace5 { get; set; } // _CoApplicantRace5
        public short ApplicantSex { get; set; } // _ApplicantSex
        public short CoApplicantSex { get; set; } // _CoApplicantSex
        public short Occupancy { get; set; } // _Occupancy
        public string GrossAnnualIncomeInThousands { get; set; } // _GrossAnnualIncomeInThousands (length: 4)
        public string ApplicationDate { get; set; } // _ApplicationDate (length: 10)
        public string CountyCodeDisplay { get; set; } // _CountyCodeDisplay (length: 3)
        public string CensusTractDisplay { get; set; } // _CensusTractDisplay (length: 7)
        public string MsaNo { get; set; } // _MSANo (length: 5)
        public string StateCode { get; set; } // _StateCode (length: 2)
        public string DenialReasonOther { get; set; } // DenialReasonOther (length: 255)
        public int AusResult3 { get; set; } // AUSResult3
        public int AusResult4 { get; set; } // AUSResult4
        public int AusResult5 { get; set; } // AUSResult5
        public int AusSystem3 { get; set; } // AUSSystem3
        public int AusSystem4 { get; set; } // AUSSystem4
        public int AusSystem5 { get; set; } // AUSSystem5
        public string AusResultOtherDesc { get; set; } // AUSResultOtherDesc (length: 50)
        public int AusResult1 { get; set; } // AUSResult1
        public int AusResult2 { get; set; } // AUSResult2
        public int AusSystem1 { get; set; } // AUSSystem1
        public int AusSystem2 { get; set; } // AUSSystem2
        public string AusSystemOther { get; set; } // AUSSystemOther (length: 50)
        public bool PropertyValueNa { get; set; } // PropertyValueNA
        public int? Term { get; set; } // Term
        public bool TermNa { get; set; } // TermNA
        public decimal? TotalLoanCosts { get; set; } // TotalLoanCosts
        public bool TotalLoanCostsNa { get; set; } // TotalLoanCostsNA
        public decimal? TotalPointsAndFees { get; set; } // TotalPointsAndFees
        public bool TotalPointsAndFeesNa { get; set; } // TotalPointsAndFeesNA
        public decimal? OriginationCharges { get; set; } // OriginationCharges
        public bool OriginationChargesNa { get; set; } // OriginationChargesNA
        public decimal? DiscountPoints { get; set; } // DiscountPoints
        public bool DiscountPointsNa { get; set; } // DiscountPointsNA
        public decimal? LenderCredits { get; set; } // LenderCredits
        public bool LenderCreditsNa { get; set; } // LenderCreditsNA
        public decimal? InterestRate { get; set; } // InterestRate
        public bool InterestRateNa { get; set; } // InterestRateNA
        public decimal? DtiRatio { get; set; } // DTIRatio
        public bool DtiRatioNa { get; set; } // DTIRatioNA
        public decimal? Cltv { get; set; } // CLTV
        public bool CltvNa { get; set; } // CLTV_NA
        public int? IntroductoryRatePeriod { get; set; } // IntroductoryRatePeriod
        public bool IntroductoryRatePeriodNa { get; set; } // IntroductoryRatePeriodNA
        public bool HasBalloonPayment { get; set; } // HasBalloonPayment
        public bool HasInterestOnlyPayments { get; set; } // HasInterestOnlyPayments
        public bool HasOtherNonAmortizingFeature { get; set; } // HasOtherNonAmortizingFeature
        public string ApplicantGmiCombined { get; set; } // _ApplicantGMICombined (length: 250)
        public string CoApplicantGmiCombined { get; set; } // _CoApplicantGMICombined (length: 250)
        public int? ApplicantAge { get; set; } // ApplicantAge
        public int? CoApplicantAge { get; set; } // CoApplicantAge
        public int? ApplicantCreditScore { get; set; } // ApplicantCreditScore
        public int ApplicantCreditScoreModel { get; set; } // ApplicantCreditScoreModel
        public string ApplicantCreditScoreModelOther { get; set; } // ApplicantCreditScoreModelOther (length: 50)
        public int? CoApplicantCreditScore { get; set; } // CoApplicantCreditScore
        public int CoApplicantCreditScoreModel { get; set; } // CoApplicantCreditScoreModel
        public string CoApplicantCreditScoreModelOther { get; set; } // CoApplicantCreditScoreModelOther (length: 50)
        public decimal? PropertyValue { get; set; } // PropertyValue
        public bool MloNmlsidNa { get; set; } // MLO_NMLSID_NA
        public bool PrepaymentPenaltyNa { get; set; } // PrepaymentPenaltyNA
        public decimal? LoanAmount { get; set; } // LoanAmount
        public bool IsPartiallyExemptFromHmda { get; set; } // IsPartiallyExemptFromHMDA
        public byte InitiallyPayableToYourInstitutionOv { get; set; } // InitiallyPayableToYourInstitutionOV

        public Hmda()
        {
            FileDataId = 0;
            PropertyType = 0;
            ActionTaken = 0;
            GrossAnnualIncomeNa = false;
            StateCodeNa = false;
            ApplicationDateNa = false;
            CountyCodeNa = false;
            CensusTractNa = false;
            MsaNoNa = false;
            ApplicantEthnicity = 0;
            CoApplicantEthnicity = 0;
            ApplicantRace1 = 0;
            ApplicantRace2 = 0;
            ApplicantRace3 = 0;
            ApplicantRace4 = 0;
            ApplicantRace5 = 0;
            CoApplicantRace1 = 0;
            CoApplicantRace2 = 0;
            CoApplicantRace3 = 0;
            CoApplicantRace4 = 0;
            CoApplicantRace5 = 0;
            ApplicantSex = 0;
            CoApplicantSex = 0;
            Occupancy = 0;
            AusResult3 = 0;
            AusResult4 = 0;
            AusResult5 = 0;
            AusSystem3 = 0;
            AusSystem4 = 0;
            AusSystem5 = 0;
            AusResult1 = 0;
            AusResult2 = 0;
            AusSystem1 = 0;
            AusSystem2 = 0;
            PropertyValueNa = false;
            TermNa = false;
            TotalLoanCostsNa = false;
            TotalPointsAndFeesNa = false;
            OriginationChargesNa = false;
            DiscountPointsNa = false;
            LenderCreditsNa = false;
            InterestRateNa = false;
            DtiRatioNa = false;
            CltvNa = false;
            IntroductoryRatePeriodNa = false;
            HasBalloonPayment = false;
            HasInterestOnlyPayments = false;
            HasOtherNonAmortizingFeature = false;
            ApplicantCreditScoreModel = 0;
            CoApplicantCreditScoreModel = 0;
            MloNmlsidNa = false;
            PrepaymentPenaltyNa = false;
            IsPartiallyExemptFromHmda = false;
            InitiallyPayableToYourInstitutionOv = 0;
        }
    }

    // HMDASettings

    public class HmdaSetting
    {
        public int HmdaSettingsId { get; set; } // HMDASettingsID (Primary key)
        public bool PromptForData { get; set; } // PromptForData
        public short Agency { get; set; } // Agency
        public string RespondentId { get; set; } // RespondentID (length: 10)
        public string TaxId { get; set; } // TaxID (length: 9)
        public string Name { get; set; } // Name (length: 30)
        public string Street { get; set; } // Street (length: 40)
        public string City { get; set; } // City (length: 25)
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 9)
        public string Contact { get; set; } // Contact (length: 30)
        public string ContactPhone { get; set; } // ContactPhone (length: 20)
        public string ContactFax { get; set; } // ContactFax (length: 20)
        public string ContactEMail { get; set; } // ContactEMail (length: 45)
        public string ParentName { get; set; } // ParentName (length: 30)
        public string ParentStreet { get; set; } // ParentStreet (length: 40)
        public string ParentCity { get; set; } // ParentCity (length: 25)
        public string ParentState { get; set; } // ParentState (length: 2)
        public string ParentZip { get; set; } // ParentZip (length: 9)
        public string Lei { get; set; } // LEI (length: 20)

        public HmdaSetting()
        {
            PromptForData = false;
            Agency = 0;
        }
    }

    // Holiday

    public class Holiday
    {
        public int HolidayId { get; set; } // HolidayID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Description { get; set; } // Description (length: 50)
        public System.DateTime? HolidayDate { get; set; } // HolidayDate

        public Holiday()
        {
            DisplayOrder = 0;
        }
    }

    // HUD1

    public class Hud1
    {
        public int Hud1Id { get; set; } // HUD1ID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int? LoanId { get; set; } // LoanID
        public string FileNo { get; set; } // FileNo (length: 50)
        public short MortgageType { get; set; } // MortgageType
        public string BuilderOrSellerTin { get; set; } // BuilderOrSellerTIN (length: 50)
        public string SettlementAgentTin { get; set; } // SettlementAgentTIN (length: 50)
        public string SettlementText { get; set; } // SettlementText (length: 500)
        public System.DateTime? InterimInterestDateFrom { get; set; } // InterimInterestDateFrom
        public System.DateTime? InterimInterestDateTo { get; set; } // InterimInterestDateTo
        public int? MiMonthsOv { get; set; } // MIMonthsOV
        public decimal? LenderCoverage { get; set; } // LenderCoverage
        public string Line905Desc { get; set; } // Line905Desc (length: 100)
        public decimal? OwnersCoverage { get; set; } // OwnersCoverage
        public string Line1501Name { get; set; } // Line1501Name (length: 50)
        public decimal? Line1501Amount { get; set; } // Line1501Amount
        public string Line1502Name { get; set; } // Line1502Name (length: 50)
        public decimal? Line1502Amount { get; set; } // Line1502Amount
        public string Line1503Name { get; set; } // Line1503Name (length: 50)
        public decimal? Line1503Amount { get; set; } // Line1503Amount
        public string Line1504Name { get; set; } // Line1504Name (length: 50)
        public decimal? Line1504Amount { get; set; } // Line1504Amount
        public string Line1505Name { get; set; } // Line1505Name (length: 50)
        public decimal? Line1505Amount { get; set; } // Line1505Amount
        public string Line1506Name { get; set; } // Line1506Name (length: 50)
        public decimal? Line1506Amount { get; set; } // Line1506Amount
        public string Line1507Name { get; set; } // Line1507Name (length: 50)
        public decimal? Line1507Amount { get; set; } // Line1507Amount
        public string Line1508Name { get; set; } // Line1508Name (length: 50)
        public decimal? Line1508Amount { get; set; } // Line1508Amount
        public string Line1509Name { get; set; } // Line1509Name (length: 50)
        public decimal? Line1509Amount { get; set; } // Line1509Amount
        public string Line1510Name { get; set; } // Line1510Name (length: 50)
        public decimal? Line1510Amount { get; set; } // Line1510Amount
        public string Line1511Name { get; set; } // Line1511Name (length: 50)
        public decimal? Line1511Amount { get; set; } // Line1511Amount
        public string Line1512Name { get; set; } // Line1512Name (length: 50)
        public decimal? Line1512Amount { get; set; } // Line1512Amount
        public string Line1513Name { get; set; } // Line1513Name (length: 50)
        public decimal? Line1513Amount { get; set; } // Line1513Amount
        public string Line1514Name { get; set; } // Line1514Name (length: 50)
        public decimal? Line1514Amount { get; set; } // Line1514Amount
        public string Line1515Name { get; set; } // Line1515Name (length: 50)
        public decimal? Line1515Amount { get; set; } // Line1515Amount
        public short LoanAmountOption { get; set; } // LoanAmountOption
        public decimal? CashFromBorrower { get; set; } // CashFromBorrower
        public int? Line1008MonthsInReserve { get; set; } // Line1008MonthsInReserve
        public decimal? Line1008Payment { get; set; } // Line1008Payment
        public string Line1107ItemNumbers { get; set; } // Line1107ItemNumbers (length: 50)
        public string Line1108ItemNumbers { get; set; } // Line1108ItemNumbers (length: 50)
        public decimal? Line102Amount { get; set; } // Line102Amount
        public string Line104Desc { get; set; } // Line104Desc (length: 50)
        public decimal? Line104Amount { get; set; } // Line104Amount
        public string Line105Desc { get; set; } // Line105Desc (length: 50)
        public decimal? Line105Amount { get; set; } // Line105Amount
        public System.DateTime? Line106DateFrom { get; set; } // Line106DateFrom
        public System.DateTime? Line106DateTo { get; set; } // Line106DateTo
        public decimal? Line106Amount { get; set; } // Line106Amount
        public System.DateTime? Line107DateFrom { get; set; } // Line107DateFrom
        public System.DateTime? Line107DateTo { get; set; } // Line107DateTo
        public decimal? Line107Amount { get; set; } // Line107Amount
        public System.DateTime? Line108DateFrom { get; set; } // Line108DateFrom
        public System.DateTime? Line108DateTo { get; set; } // Line108DateTo
        public decimal? Line108Amount { get; set; } // Line108Amount
        public string Line109Desc { get; set; } // Line109Desc (length: 50)
        public decimal? Line109Amount { get; set; } // Line109Amount
        public string Line110Desc { get; set; } // Line110Desc (length: 50)
        public decimal? Line110Amount { get; set; } // Line110Amount
        public string Line111Desc { get; set; } // Line111Desc (length: 50)
        public decimal? Line111Amount { get; set; } // Line111Amount
        public string Line112Desc { get; set; } // Line112Desc (length: 50)
        public decimal? Line112Amount { get; set; } // Line112Amount
        public decimal? TotalFromBorrowerOv { get; set; } // TotalFromBorrowerOV
        public decimal? Line201Amount { get; set; } // Line201Amount
        public decimal? Line202Amount { get; set; } // Line202Amount
        public decimal? Line203Amount { get; set; } // Line203Amount
        public string Line204Desc { get; set; } // Line204Desc (length: 50)
        public decimal? Line204Amount { get; set; } // Line204Amount
        public bool Line204PaidByOthers { get; set; } // Line204PaidByOthers
        public string Line205Desc { get; set; } // Line205Desc (length: 50)
        public decimal? Line205Amount { get; set; } // Line205Amount
        public bool Line205PaidByOthers { get; set; } // Line205PaidByOthers
        public string Line206Desc { get; set; } // Line206Desc (length: 50)
        public decimal? Line206Amount { get; set; } // Line206Amount
        public bool Line206PaidByOthers { get; set; } // Line206PaidByOthers
        public string Line207Desc { get; set; } // Line207Desc (length: 50)
        public decimal? Line207Amount { get; set; } // Line207Amount
        public bool Line207PaidByOthers { get; set; } // Line207PaidByOthers
        public string Line208Desc { get; set; } // Line208Desc (length: 50)
        public decimal? Line208Amount { get; set; } // Line208Amount
        public bool Line208PaidByOthers { get; set; } // Line208PaidByOthers
        public string Line209Desc { get; set; } // Line209Desc (length: 50)
        public decimal? Line209Amount { get; set; } // Line209Amount
        public bool Line209PaidByOthers { get; set; } // Line209PaidByOthers
        public System.DateTime? Line210DateFrom { get; set; } // Line210DateFrom
        public System.DateTime? Line210DateTo { get; set; } // Line210DateTo
        public decimal? Line210Amount { get; set; } // Line210Amount
        public System.DateTime? Line211DateFrom { get; set; } // Line211DateFrom
        public System.DateTime? Line211DateTo { get; set; } // Line211DateTo
        public decimal? Line211Amount { get; set; } // Line211Amount
        public System.DateTime? Line212DateFrom { get; set; } // Line212DateFrom
        public System.DateTime? Line212DateTo { get; set; } // Line212DateTo
        public decimal? Line212Amount { get; set; } // Line212Amount
        public string Line213Desc { get; set; } // Line213Desc (length: 50)
        public decimal? Line213Amount { get; set; } // Line213Amount
        public string Line214Desc { get; set; } // Line214Desc (length: 50)
        public decimal? Line214Amount { get; set; } // Line214Amount
        public string Line215Desc { get; set; } // Line215Desc (length: 50)
        public decimal? Line215Amount { get; set; } // Line215Amount
        public string Line216Desc { get; set; } // Line216Desc (length: 50)
        public decimal? Line216Amount { get; set; } // Line216Amount
        public string Line217Desc { get; set; } // Line217Desc (length: 50)
        public decimal? Line217Amount { get; set; } // Line217Amount
        public string Line218Desc { get; set; } // Line218Desc (length: 50)
        public decimal? Line218Amount { get; set; } // Line218Amount
        public string Line219Desc { get; set; } // Line219Desc (length: 50)
        public decimal? Line219Amount { get; set; } // Line219Amount
        public decimal? TotalByBorrowerOv { get; set; } // TotalByBorrowerOV
        public decimal? TotalSettlementBorrowerOv { get; set; } // TotalSettlementBorrowerOV
        public decimal? Line402Amount { get; set; } // Line402Amount
        public string Line403Desc { get; set; } // Line403Desc (length: 50)
        public decimal? Line403Amount { get; set; } // Line403Amount
        public string Line404Desc { get; set; } // Line404Desc (length: 50)
        public decimal? Line404Amount { get; set; } // Line404Amount
        public string Line405Desc { get; set; } // Line405Desc (length: 50)
        public decimal? Line405Amount { get; set; } // Line405Amount
        public System.DateTime? Line406DateFrom { get; set; } // Line406DateFrom
        public System.DateTime? Line406DateTo { get; set; } // Line406DateTo
        public decimal? Line406Amount { get; set; } // Line406Amount
        public System.DateTime? Line407DateFrom { get; set; } // Line407DateFrom
        public System.DateTime? Line407DateTo { get; set; } // Line407DateTo
        public decimal? Line407Amount { get; set; } // Line407Amount
        public System.DateTime? Line408DateFrom { get; set; } // Line408DateFrom
        public System.DateTime? Line408DateTo { get; set; } // Line408DateTo
        public decimal? Line408Amount { get; set; } // Line408Amount
        public string Line409Desc { get; set; } // Line409Desc (length: 50)
        public decimal? Line409Amount { get; set; } // Line409Amount
        public string Line410Desc { get; set; } // Line410Desc (length: 50)
        public decimal? Line410Amount { get; set; } // Line410Amount
        public string Line411Desc { get; set; } // Line411Desc (length: 50)
        public decimal? Line411Amount { get; set; } // Line411Amount
        public string Line412Desc { get; set; } // Line412Desc (length: 50)
        public decimal? Line412Amount { get; set; } // Line412Amount
        public decimal? TotalFromSellerOv { get; set; } // TotalFromSellerOV
        public decimal? Line501Amount { get; set; } // Line501Amount
        public decimal? Line503Amount { get; set; } // Line503Amount
        public decimal? Line504Amount { get; set; } // Line504Amount
        public decimal? Line505Amount { get; set; } // Line505Amount
        public string Line506Desc { get; set; } // Line506Desc (length: 50)
        public decimal? Line506Amount { get; set; } // Line506Amount
        public string Line507Desc { get; set; } // Line507Desc (length: 50)
        public decimal? Line507Amount { get; set; } // Line507Amount
        public string Line508Desc { get; set; } // Line508Desc (length: 50)
        public decimal? Line508Amount { get; set; } // Line508Amount
        public string Line509Desc { get; set; } // Line509Desc (length: 50)
        public decimal? Line509Amount { get; set; } // Line509Amount
        public System.DateTime? Line510DateFrom { get; set; } // Line510DateFrom
        public System.DateTime? Line510DateTo { get; set; } // Line510DateTo
        public decimal? Line510Amount { get; set; } // Line510Amount
        public System.DateTime? Line511DateFrom { get; set; } // Line511DateFrom
        public System.DateTime? Line511DateTo { get; set; } // Line511DateTo
        public decimal? Line511Amount { get; set; } // Line511Amount
        public System.DateTime? Line512DateFrom { get; set; } // Line512DateFrom
        public System.DateTime? Line512DateTo { get; set; } // Line512DateTo
        public decimal? Line512Amount { get; set; } // Line512Amount
        public string Line513Desc { get; set; } // Line513Desc (length: 50)
        public decimal? Line513Amount { get; set; } // Line513Amount
        public string Line514Desc { get; set; } // Line514Desc (length: 50)
        public decimal? Line514Amount { get; set; } // Line514Amount
        public string Line515Desc { get; set; } // Line515Desc (length: 50)
        public decimal? Line515Amount { get; set; } // Line515Amount
        public string Line516Desc { get; set; } // Line516Desc (length: 50)
        public decimal? Line516Amount { get; set; } // Line516Amount
        public string Line517Desc { get; set; } // Line517Desc (length: 50)
        public decimal? Line517Amount { get; set; } // Line517Amount
        public string Line518Desc { get; set; } // Line518Desc (length: 50)
        public decimal? Line518Amount { get; set; } // Line518Amount
        public string Line519Desc { get; set; } // Line519Desc (length: 50)
        public decimal? Line519Amount { get; set; } // Line519Amount
        public decimal? TotalReductionsSellerOv { get; set; } // TotalReductionsSellerOV
        public decimal? TotalSettlementSellerOv { get; set; } // TotalSettlementSellerOV
        public decimal? Line700Perc { get; set; } // Line700Perc
        public decimal? Line700Perc2 { get; set; } // Line700Perc2
        public decimal? Line700Perc3 { get; set; } // Line700Perc3
        public string Line700PercText { get; set; } // Line700PercText (length: 20)
        public decimal? Line700BaseLoanTier1 { get; set; } // Line700BaseLoanTier1
        public decimal? Line700BaseLoanTier2 { get; set; } // Line700BaseLoanTier2
        public decimal? Line700TotalOv { get; set; } // Line700TotalOV
        public decimal? Line701Amount { get; set; } // Line701Amount
        public string Line701PaidTo { get; set; } // Line701PaidTo (length: 100)
        public decimal? Line702Amount { get; set; } // Line702Amount
        public string Line702PaidTo { get; set; } // Line702PaidTo (length: 100)
        public decimal? Line703Pba { get; set; } // Line703PBA
        public decimal? Line703Pbs { get; set; } // Line703PBS
        public string Line704Desc { get; set; } // Line704Desc (length: 100)
        public decimal? Line704Pba { get; set; } // Line704PBA
        public decimal? Line704Pbs { get; set; } // Line704PBS
        public decimal? TotalBorrowerChargesOv { get; set; } // TotalBorrowerChargesOV
        public decimal? TotalSellerChargesOv { get; set; } // TotalSellerChargesOV
        public decimal? AgentPortionOfTitlePrem { get; set; } // AgentPortionOfTitlePrem
        public decimal? UnderwriterPortionOfTitlePrem { get; set; } // UnderwriterPortionOfTitlePrem
        public string TitleCompanyNameOv { get; set; } // TitleCompanyNameOV (length: 50)
        public bool OmitLenderPaidFee { get; set; } // OmitLenderPaidFee
        public string Line102NameOv { get; set; } // Line102NameOV (length: 50)
        public decimal? SellerCredit { get; set; } // SellerCredit
        public decimal? PersonalPropertyAmount { get; set; } // PersonalPropertyAmount
        public decimal? AssumedLoanAmount { get; set; } // AssumedLoanAmount
        public decimal? SellerPayoffOfFirstMort { get; set; } // SellerPayoffOfFirstMort
        public decimal? SellerPayoffOfSecondMort { get; set; } // SellerPayoffOfSecondMort
        public string DebtsToBePaidOffDescOv { get; set; } // DebtsToBePaidOffDescOV (length: 50)
        public decimal? ExcessDeposit { get; set; } // ExcessDeposit
        public decimal? CureOv { get; set; } // CureOV
        public string CustomaryRecitals { get; set; } // CustomaryRecitals (length: 2147483647)
        public bool SeeAdditionalPagesBorTrans { get; set; } // SeeAdditionalPagesBorTrans
        public bool SeeAdditionalPagesSellerTrans { get; set; } // SeeAdditionalPagesSellerTrans
        public decimal? Cure { get; set; } // Cure
        public int CureTestResult { get; set; } // CureTestResult
        public int TridPayoffSourceForCd { get; set; } // TRIDPayoffSourceForCD
        public bool CdOmitSellerInfo { get; set; } // CDOmitSellerInfo
        public decimal? SellerCreditDueFromSellerOv { get; set; } // SellerCreditDueFromSellerOV

        public Hud1()
        {
            FileDataId = 0;
            MortgageType = 0;
            LoanAmountOption = 0;
            Line204PaidByOthers = false;
            Line205PaidByOthers = false;
            Line206PaidByOthers = false;
            Line207PaidByOthers = false;
            Line208PaidByOthers = false;
            Line209PaidByOthers = false;
            OmitLenderPaidFee = false;
            SeeAdditionalPagesBorTrans = false;
            SeeAdditionalPagesSellerTrans = false;
            CureTestResult = 0;
            TridPayoffSourceForCd = 0;
            CdOmitSellerInfo = false;
        }
    }

    // ImportTemplate

    public class ImportTemplate
    {
        public int ImportTemplateId { get; set; } // ImportTemplateID (Primary key)
        public string Name { get; set; } // Name (length: 50)
        public short Format { get; set; } // Format
        public string OtherDelimiter { get; set; } // OtherDelimiter (length: 1)
        public short TextQualifierOv { get; set; } // TextQualifierOV
        public bool ReadNewFileNameFromImportedData { get; set; } // ReadNewFileNameFromImportedData
        public short ImportAction { get; set; } // ImportAction
        public string SourceFileName { get; set; } // SourceFileName (length: 255)
        public bool HasHeaderRow { get; set; } // HasHeaderRow
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Worksheet { get; set; } // Worksheet (length: 50)

        public ImportTemplate()
        {
            Format = 0;
            TextQualifierOv = 0;
            ReadNewFileNameFromImportedData = false;
            ImportAction = 0;
            HasHeaderRow = false;
            DisplayOrder = 0;
        }
    }

    // ImportTemplateField

    public class ImportTemplateField
    {
        public int ImportTemplateFieldId { get; set; } // ImportTemplateFieldID (Primary key)
        public int? ImportTemplateId { get; set; } // ImportTemplateID
        public int DisplayOrder { get; set; } // DisplayOrder
        public string ObjectName { get; set; } // ObjectName (length: 50)
        public string PropertyName { get; set; } // PropertyName (length: 50)
        public bool SkipColumn { get; set; } // SkipColumn

        public ImportTemplateField()
        {
            DisplayOrder = 0;
            SkipColumn = false;
        }
    }

    // Income

    public class Income
    {
        public int IncomeId { get; set; } // IncomeID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int? BorrowerId { get; set; } // BorrowerID
        public int DisplayOrder { get; set; } // DisplayOrder
        public short IncomeType { get; set; } // IncomeType
        public decimal? Amount { get; set; } // Amount
        public string DescriptionOv { get; set; } // DescriptionOV (length: 70)
        public int IncomeFrequencyType { get; set; } // IncomeFrequencyType
        public decimal? IncomeRate { get; set; } // IncomeRate
        public decimal? HoursPerWeek { get; set; } // HoursPerWeek
        public string Notes { get; set; } // Notes (length: 500)
        public string QmatrNotes { get; set; } // QMATRNotes (length: 500)
        public string VariablePeriod1Desc { get; set; } // VariablePeriod1Desc (length: 25)
        public decimal? VariablePeriod1Income { get; set; } // VariablePeriod1Income
        public decimal? VariablePeriod1Months { get; set; } // VariablePeriod1Months
        public string VariablePeriod2Desc { get; set; } // VariablePeriod2Desc (length: 25)
        public decimal? VariablePeriod2Income { get; set; } // VariablePeriod2Income
        public decimal? VariablePeriod2Months { get; set; } // VariablePeriod2Months
        public string VariablePeriod3Desc { get; set; } // VariablePeriod3Desc (length: 25)
        public decimal? VariablePeriod3Income { get; set; } // VariablePeriod3Income
        public decimal? VariablePeriod3Months { get; set; } // VariablePeriod3Months
        public string CalcDescription { get; set; } // _CalcDescription (length: 50)
        public string RateDescription { get; set; } // _RateDescription (length: 70)
        public bool SelfEmploymentIncome { get; set; } // SelfEmploymentIncome
        public int RMEmploymentInfoId { get; set; }
        public System.Guid? SyncGuid { get; set; } // SyncGuid
        public int? EmployerId { get; set; } // EmployerID

        public Income()
        {
            FileDataId = 0;
            DisplayOrder = 0;
            IncomeType = 0;
            IncomeFrequencyType = 0;
            SelfEmploymentIncome = false;
        }


        //public static List<Income> Create()
        //{
        //    throw new NotImplementedException();
        //}

        public static List<Income> Create(
                                    ActionModels.LoanFile.Borrower borrower,
                                    int fileDataId,
                                    ThirdPartyCodeList thirdPartyCodeList)
        {
            var incomes = new List<Income>();
            Update(incomes, borrower, fileDataId, thirdPartyCodeList);
            return incomes;
        }

        


        private static void Update(List<Income> incomes,
                                   ActionModels.LoanFile.Borrower borrower,
                                   int fileDataId,
                                   ThirdPartyCodeList thirdPartyCodeList)
        {



            var currentEmploymentInfos = borrower.EmploymentInfoes.Where(ei => ei.EndDate == null);
            var previousEmploymentInfos = borrower.EmploymentInfoes.Where(ei => ei.EndDate != null);
            var otherIncomes = borrower.OtherIncomes;

            foreach (var employmentInfo in currentEmploymentInfos)
            {




                var toDate = employmentInfo.EndDate ?? borrower.LoanApplication?.CreatedOnUtc ?? DateTime.UtcNow;




                double fractionalYears = Math.Abs((employmentInfo.StartDate.Value - toDate).TotalDays / 365);




                // Base Income Total
                incomes.Add(new Income
                {
                    //EmployerID = employerId,
                    FileDataId = fileDataId,
                    Amount = employmentInfo.MonthlyBaseIncome,
                    DescriptionOv = "BaseIncome",
                    IncomeType = 1,
                    SelfEmploymentIncome = employmentInfo.IsSelfEmployed ?? false,
                    RMEmploymentInfoId = employmentInfo.Id


                });
                var totalOverTime = employmentInfo.OtherEmploymentIncomes.Where(x => x.OtherIncomeTypeId == OtherEmploymentIncomeTypeEnum.Overtime.ToInt()).Select(mo => mo.MonthlyIncome).FirstOrDefault();
                var totalBonus = employmentInfo.OtherEmploymentIncomes.Where(x => x.OtherIncomeTypeId == OtherEmploymentIncomeTypeEnum.Bonus.ToInt()).Select(mo => mo.MonthlyIncome).FirstOrDefault();
                var totalCommission = employmentInfo.OtherEmploymentIncomes.Where(x => x.OtherIncomeTypeId == OtherEmploymentIncomeTypeEnum.Commission.ToInt()).Select(mo => mo.MonthlyIncome).FirstOrDefault();
                // Over Time
                incomes.Add(new Income
                {
                    //EmployerID = employerId,
                    FileDataId = fileDataId,
                    Amount = (decimal?)(totalOverTime ?? null),
                    DescriptionOv = "Overtime",
                    IncomeType = 2,
                    SelfEmploymentIncome = employmentInfo.IsSelfEmployed ?? false,
                    RMEmploymentInfoId = employmentInfo.Id

                });

                // Bonus
                incomes.Add(new Income
                {
                    //EmployerID = employerId,
                    FileDataId = fileDataId,
                    Amount = (decimal?)(totalBonus ?? null),
                    DescriptionOv = "Bonus",
                    IncomeType = 3,
                    SelfEmploymentIncome = employmentInfo.IsSelfEmployed ?? false,
                    RMEmploymentInfoId = employmentInfo.Id

                });

                // Commissions
                incomes.Add(new Income
                {
                    //EmployerID = employerId,
                    FileDataId = fileDataId,
                    Amount = (decimal?)(totalCommission ?? null),
                    DescriptionOv = "Commission",
                    IncomeType = 4,
                    SelfEmploymentIncome = employmentInfo.IsSelfEmployed ?? false,
                    RMEmploymentInfoId = employmentInfo.Id

                });


            }


            foreach (var otherIncome in otherIncomes)
            {
                //string incomeTypeName = GetByteProValue("IncomeType", otIncome.IncomeTypeId);
                //int incomeTypeIndex = FindIndex(typeof(IncomeType), incomeTypeName);

                var incomeTypeName = thirdPartyCodeList.GetByteProValue("IncomeType",
                                                                        otherIncome.IncomeTypeId);
                var incomeTypeIndex = (short)incomeTypeName.FindEnumIndex(typeof(IncomeTypeEnum));


                incomes.Add(new Income
                {
                    //EmployerID = employerId,
                    FileDataId = fileDataId,
                    Amount = otherIncome.MonthlyAmount ?? null,
                    DescriptionOv = incomeTypeName,
                    IncomeType = incomeTypeIndex,


                });
                //index++;
            }



        }
    }

    // Investor

    public class Investor
    {
        public int InvestorId { get; set; } // InvestorID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public System.Guid? Guid { get; set; } // Guid
        public string Name { get; set; } // Name (length: 50)
        public string Code { get; set; } // Code (length: 50)
        public string Notes { get; set; } // Notes (length: 2147483647)
        public string AccountRep1FirstName { get; set; } // AccountRep1FirstName (length: 50)
        public string AccountRep1MiddleName { get; set; } // AccountRep1MiddleName (length: 50)
        public string AccountRep1LastName { get; set; } // AccountRep1LastName (length: 50)
        public string AccountRep1Title { get; set; } // AccountRep1Title (length: 50)
        public string AccountRep1Phone { get; set; } // AccountRep1Phone (length: 20)
        public string AccountRep1Email { get; set; } // AccountRep1Email (length: 100)
        public string WebSiteUrl { get; set; } // WebSiteURL (length: 100)
        public string AccountRep2FirstName { get; set; } // AccountRep2FirstName (length: 50)
        public string AccountRep2MiddleName { get; set; } // AccountRep2MiddleName (length: 50)
        public string AccountRep2LastName { get; set; } // AccountRep2LastName (length: 50)
        public string AccountRep2Title { get; set; } // AccountRep2Title (length: 50)
        public string AccountRep2Phone { get; set; } // AccountRep2Phone (length: 20)
        public string AccountRep2Email { get; set; } // AccountRep2Email (length: 100)
        public string LockDeskPhone { get; set; } // LockDeskPhone (length: 20)
        public string ShippingStreet { get; set; } // ShippingStreet (length: 50)
        public string ShippingCity { get; set; } // ShippingCity (length: 50)
        public string ShippingState { get; set; } // ShippingState (length: 2)
        public string ShippingZip { get; set; } // ShippingZip (length: 9)
        public string PpeInvestorCode { get; set; } // PPEInvestorCode (length: 50)
        public string MersOrgId { get; set; } // MERSOrgID (length: 7)
        public string SellerNo { get; set; } // SellerNo (length: 20)
        public string ShippingName { get; set; } // ShippingName (length: 100)
        public string ShippingPhone { get; set; } // ShippingPhone (length: 20)
        public string ShippingContact { get; set; } // ShippingContact (length: 50)
        public string PaymentName { get; set; } // PaymentName (length: 100)
        public string PaymentStreet { get; set; } // PaymentStreet (length: 50)
        public string PaymentCity { get; set; } // PaymentCity (length: 50)
        public string PaymentState { get; set; } // PaymentState (length: 2)
        public string PaymentZip { get; set; } // PaymentZip (length: 9)
        public string PaymentPhone { get; set; } // PaymentPhone (length: 20)
        public string PaymentContact { get; set; } // PaymentContact (length: 50)
        public string LossPayeeName { get; set; } // LossPayeeName (length: 100)
        public string LossPayeeStreet { get; set; } // LossPayeeStreet (length: 50)
        public string LossPayeeCity { get; set; } // LossPayeeCity (length: 50)
        public string LossPayeeState { get; set; } // LossPayeeState (length: 2)
        public string LossPayeeZip { get; set; } // LossPayeeZip (length: 9)
        public string LossPayeePhone { get; set; } // LossPayeePhone (length: 20)
        public string LossPayeeContact { get; set; } // LossPayeeContact (length: 50)
        public string ServicerBusinessContact { get; set; } // ServicerBusinessContact (length: 50)
        public string ServicerBusinessStreet { get; set; } // ServicerBusinessStreet (length: 50)
        public string ServicerBusinessCity { get; set; } // ServicerBusinessCity (length: 50)
        public string ServicerBusinessState { get; set; } // ServicerBusinessState (length: 2)
        public string ServicerBusinessZip { get; set; } // ServicerBusinessZip (length: 9)
        public string PaymentPhoneHours { get; set; } // PaymentPhoneHours (length: 150)
        public string PaymentPhoneContact { get; set; } // PaymentPhoneContact (length: 30)
        public string ServicerQwrContact { get; set; } // ServicerQWRContact (length: 50)
        public string ServicerQwrStreet { get; set; } // ServicerQWRStreet (length: 50)
        public string ServicerQwrCity { get; set; } // ServicerQWRCity (length: 50)
        public string ServicerQwrState { get; set; } // ServicerQWRState (length: 2)
        public string ServicerQwrZip { get; set; } // ServicerQWRZip (length: 9)
        public int InvestorType { get; set; } // InvestorType
        public string DocPrepCode { get; set; } // DocPrepCode (length: 50)
        public int HmdaTypeOfPurchaser { get; set; } // HMDATypeOfPurchaser
        public string MarksmanCode { get; set; } // MarksmanCode (length: 50)
        public int NmlsInvestorType { get; set; } // NMLSInvestorType
        public string DocPrepCode2 { get; set; } // DocPrepCode2 (length: 50)
        public bool IsSubServicer { get; set; } // IsSubServicer
        public string InvestorsServicerMersOrgId { get; set; } // InvestorsServicerMERSOrgID (length: 7)
        public int ServicingOption { get; set; } // ServicingOption
        public bool Disabled { get; set; } // Disabled

        public Investor()
        {
            DisplayOrder = 0;
            Guid = System.Guid.NewGuid();
            InvestorType = 0;
            HmdaTypeOfPurchaser = -1;
            NmlsInvestorType = 0;
            IsSubServicer = false;
            ServicingOption = 0;
            Disabled = false;
        }
    }

    // IRS4506TDefault

    public class Irs4506TDefault
    {
        public int Irs4506TDefaultId { get; set; } // IRS4506TDefaultID (Primary key)
        public string DefaultName { get; set; } // DefaultName (length: 50)
        public string ThirdPartyName { get; set; } // ThirdPartyName (length: 100)
        public string ThirdPartyStreet { get; set; } // ThirdPartyStreet (length: 50)
        public string ThirdPartyCity { get; set; } // ThirdPartyCity (length: 50)
        public string ThirdPartyState { get; set; } // ThirdPartyState (length: 2)
        public string ThirdPartyZip { get; set; } // ThirdPartyZip (length: 9)
        public string ThirdPartyPhone { get; set; } // ThirdPartyPhone (length: 20)
        public string TaxFormRequested { get; set; } // TaxFormRequested (length: 20)
        public bool ReturnTranscript { get; set; } // ReturnTranscript
        public bool AccountTranscript { get; set; } // AccountTranscript
        public bool RecordOfAccount { get; set; } // RecordOfAccount
        public bool VerificationOfNonFiling { get; set; } // VerificationOfNonFiling
        public bool FormW2EtcTranscript { get; set; } // FormW2EtcTranscript
        public string TaxPeriod1 { get; set; } // TaxPeriod1 (length: 10)
        public string TaxPeriod2 { get; set; } // TaxPeriod2 (length: 10)
        public string TaxPeriod3 { get; set; } // TaxPeriod3 (length: 10)
        public string TaxPeriod4 { get; set; } // TaxPeriod4 (length: 10)

        public Irs4506TDefault()
        {
            ReturnTranscript = false;
            AccountTranscript = false;
            RecordOfAccount = false;
            VerificationOfNonFiling = false;
            FormW2EtcTranscript = false;
        }
    }

    // IRSForm1098

    public class IrsForm1098
    {
        public int IrsForm1098Id { get; set; } // IRSForm1098ID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public short AddressOption { get; set; } // AddressOption
        public string AccountNumber { get; set; } // AccountNumber (length: 50)
        public decimal? MortgageInterest { get; set; } // MortgageInterest
        public decimal? Points { get; set; } // Points
        public decimal? Refund { get; set; } // Refund
        public decimal? Mip { get; set; } // MIP
        public decimal? OutstandingPrincipalOnJanFirst { get; set; } // OutstandingPrincipalOnJanFirst

        public IrsForm1098()
        {
            FileDataId = 0;
            AddressOption = 0;
        }
    }

    // IRSForm4506

    public class IrsForm4506
    {
        public int IrsForm4506Id { get; set; } // IRSForm4506ID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int? BorrowerId { get; set; } // BorrowerID
        public short FilingStatus { get; set; } // FilingStatus
        public string LastReturnStreet { get; set; } // LastReturnStreet (length: 50)
        public string LastReturnCity { get; set; } // LastReturnCity (length: 50)
        public string LastReturnState { get; set; } // LastReturnState (length: 2)
        public string LastReturnZip { get; set; } // LastReturnZip (length: 9)
        public string ThirdPartyName { get; set; } // ThirdPartyName (length: 100)
        public string ThirdPartyStreet { get; set; } // ThirdPartyStreet (length: 50)
        public string ThirdPartyCity { get; set; } // ThirdPartyCity (length: 50)
        public string ThirdPartyState { get; set; } // ThirdPartyState (length: 2)
        public string ThirdPartyZip { get; set; } // ThirdPartyZip (length: 9)
        public string ThirdPartyPhone { get; set; } // ThirdPartyPhone (length: 20)
        public string TaxFormRequested { get; set; } // TaxFormRequested (length: 20)
        public string TaxPeriod1 { get; set; } // TaxPeriod1 (length: 10)
        public string TaxPeriod2 { get; set; } // TaxPeriod2 (length: 10)
        public string TaxPeriod3 { get; set; } // TaxPeriod3 (length: 10)
        public string TaxPeriod4 { get; set; } // TaxPeriod4 (length: 10)
        public bool ReturnTranscript { get; set; } // ReturnTranscript
        public bool AccountTranscript { get; set; } // AccountTranscript
        public bool RecordOfAccount { get; set; } // RecordOfAccount
        public bool VerificationOfNonFiling { get; set; } // VerificationOfNonFiling
        public bool FormW2EtcTranscript { get; set; } // FormW2EtcTranscript
        public string PhoneOv { get; set; } // PhoneOV (length: 10)
        public bool OverrideReturnName { get; set; } // OverrideReturnName
        public bool OverrideSpouse { get; set; } // OverrideSpouse
        public bool OverrideCurrentName { get; set; } // OverrideCurrentName
        public bool OverrideCurrentAddress { get; set; } // OverrideCurrentAddress
        public string ReturnFirstNameOv { get; set; } // ReturnFirstNameOV (length: 50)
        public string ReturnMiddleNameOv { get; set; } // ReturnMiddleNameOV (length: 50)
        public string ReturnLastNameOv { get; set; } // ReturnLastNameOV (length: 50)
        public string ReturnSuffixOv { get; set; } // ReturnSuffixOV (length: 10)
        public string ReturnSsnov { get; set; } // ReturnSSNOV (length: 20)
        public string SpouseFirstNameOv { get; set; } // SpouseFirstNameOV (length: 50)
        public string SpouseMiddleNameOv { get; set; } // SpouseMiddleNameOV (length: 50)
        public string SpouseLastNameOv { get; set; } // SpouseLastNameOV (length: 50)
        public string SpouseSuffixOv { get; set; } // SpouseSuffixOV (length: 10)
        public string SpouseSsnov { get; set; } // SpouseSSNOV (length: 20)
        public string CurrentFirstNameOv { get; set; } // CurrentFirstNameOV (length: 50)
        public string CurrentMiddleNameOv { get; set; } // CurrentMiddleNameOV (length: 50)
        public string CurrentLastNameOv { get; set; } // CurrentLastNameOV (length: 50)
        public string CurrentSuffixOv { get; set; } // CurrentSuffixOV (length: 10)
        public string CurrentStreetOv { get; set; } // CurrentStreetOV (length: 50)
        public string CurrentCityOv { get; set; } // CurrentCityOV (length: 50)
        public string CurrentStateOv { get; set; } // CurrentStateOV (length: 2)
        public string CurrentZipOv { get; set; } // CurrentZipOV (length: 9)
        public string DvOrderId1 { get; set; } // DVOrderId1 (length: 50)
        public int TrvForm1 { get; set; } // TRVForm1
        public int TrvForm2 { get; set; } // TRVForm2
        public int TrvForm3 { get; set; } // TRVForm3
        public int? TrvForm1Year1 { get; set; } // TRVForm1Year1
        public int? TrvForm1Year2 { get; set; } // TRVForm1Year2
        public int? TrvForm1Year3 { get; set; } // TRVForm1Year3
        public int? TrvForm1Year4 { get; set; } // TRVForm1Year4
        public int? TrvForm2Year1 { get; set; } // TRVForm2Year1
        public int? TrvForm2Year2 { get; set; } // TRVForm2Year2
        public int? TrvForm2Year3 { get; set; } // TRVForm2Year3
        public int? TrvForm2Year4 { get; set; } // TRVForm2Year4
        public int? TrvForm3Year1 { get; set; } // TRVForm3Year1
        public int? TrvForm3Year2 { get; set; } // TRVForm3Year2
        public int? TrvForm3Year3 { get; set; } // TRVForm3Year3
        public int? TrvForm3Year4 { get; set; } // TRVForm3Year4
        public string DvOrderId2 { get; set; } // DVOrderId2 (length: 50)
        public string DvOrderId3 { get; set; } // DVOrderId3 (length: 50)
        public bool IdentityTheft { get; set; } // IdentityTheft
        public System.Guid? IrsForm4506Guid { get; set; } // IRSForm4506Guid

        public IrsForm4506()
        {
            FileDataId = 0;
            FilingStatus = 0;
            ReturnTranscript = false;
            AccountTranscript = false;
            RecordOfAccount = false;
            VerificationOfNonFiling = false;
            FormW2EtcTranscript = false;
            OverrideReturnName = false;
            OverrideSpouse = false;
            OverrideCurrentName = false;
            OverrideCurrentAddress = false;
            TrvForm1 = 0;
            TrvForm2 = 0;
            TrvForm3 = 0;
            IdentityTheft = false;
        }
    }

    // LicensedCompany

    public class LicensedCompany
    {
        public int LicensedCompanyId { get; set; } // LicensedCompanyID (Primary key)
        public string Name { get; set; } // Name (length: 100)
        public string Street1 { get; set; } // Street1 (length: 100)
        public string Street2 { get; set; } // Street2 (length: 100)
        public string City { get; set; } // City (length: 100)
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 9)
        public string Phone { get; set; } // Phone (length: 10)
        public string Fax { get; set; } // Fax (length: 10)
        public int CompanyId { get; set; } // CompanyID
        public string UserNo { get; set; } // UserNo (length: 15)
        public int CheckSum { get; set; } // CheckSum

        public LicensedCompany()
        {
            CompanyId = 0;
            CheckSum = 0;
        }
    }

    // LicensedProduct

    public class LicensedProduct
    {
        public int LicensedProductId { get; set; } // LicensedProductID (Primary key)
        public short LicensedProductType { get; set; } // LicensedProductType
        public System.DateTime SupportExpirationDate { get; set; } // SupportExpirationDate
        public bool Trial { get; set; } // Trial
        public System.DateTime DieDate { get; set; } // DieDate
        public int NoUsers { get; set; } // NoUsers
        public int Options { get; set; } // Options
        public int CheckSum { get; set; } // CheckSum

        public LicensedProduct()
        {
            LicensedProductType = 0;
            Trial = false;
            NoUsers = 0;
            Options = 0;
            CheckSum = 0;
        }
    }

    // LicensingAgency

    public class LicensingAgency
    {
        public int LicensingAgencyId { get; set; } // LicensingAgencyID (Primary key)
        public string Code { get; set; } // Code (length: 20)
        public int LicensingAgencyType { get; set; } // LicensingAgencyType
        public string State { get; set; } // State (length: 2)
        public string Name { get; set; } // Name (length: 100)
        public string Url { get; set; } // URL (length: 100)
        public bool AutoPopulate { get; set; } // AutoPopulate
        public int DisplayOrder { get; set; } // DisplayOrder

        public LicensingAgency()
        {
            LicensingAgencyType = 0;
            AutoPopulate = false;
            DisplayOrder = 0;
        }
    }

    // Loan

    public class Loan
    {
        public int LoanId { get; set; } // LoanID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int DisplayOrder { get; set; } // DisplayOrder
        public int? LinkedLoanId { get; set; } // LinkedLoanID
        public bool SellerBreakDown { get; set; } // SellerBreakDown
        public short LoanPurpose { get; set; } // LoanPurpose
        public string LoanPurposeOther { get; set; } // LoanPurposeOther (length: 50)
        public bool TreatConstAsRefi { get; set; } // TreatConstAsRefi
        public string RefiPurpNameOv { get; set; } // RefiPurpNameOV (length: 50)
        public short RefiPurpAu { get; set; } // RefiPurpAU
        public short RefiTypeFha { get; set; } // RefiTypeFHA
        public short RefiTypeVa { get; set; } // RefiTypeVA
        public short AmortizationType { get; set; } // AmortizationType
        public string AmortizationTypeDescOv { get; set; } // AmortizationTypeDescOV (length: 50)
        public string LoanProgramName { get; set; } // LoanProgramName (length: 100)
        public decimal? PurPrice { get; set; } // PurPrice
        public decimal? BaseLoan { get; set; } // BaseLoan
        public decimal? LoanWith { get; set; } // LoanWith
        public decimal? MipffPerc { get; set; } // MIPFFPerc
        public decimal? MipffPaidInCash { get; set; } // MIPFFPaidInCash
        public decimal? MipffPaidInCashPbsDesired { get; set; } // MIPFFPaidInCashPBSDesired
        public short MortgageType { get; set; } // MortgageType
        public string MortgageTypeOther { get; set; } // MortgageTypeOther (length: 50)
        public short BuyDowns { get; set; } // BuyDowns
        public int? BalloonTerm { get; set; } // BalloonTerm
        public int? Term { get; set; } // Term
        public decimal? IntRate { get; set; } // IntRate
        public decimal? QualRateOv { get; set; } // QualRateOV
        public int? InterestOnlyPeriod { get; set; } // InterestOnlyPeriod
        public decimal? Piov { get; set; } // PIOV
        public decimal? PropOther { get; set; } // PropOther
        public short SubFiType { get; set; } // SubFiType
        public bool SubFiIntOnly { get; set; } // SubFiIntOnly
        public decimal? SubFiBaseLoan { get; set; } // SubFiBaseLoan
        public decimal? SubFiIntRate { get; set; } // SubFiIntRate
        public int? SubFiTerm { get; set; } // SubFiTerm
        public decimal? SubFiPiov { get; set; } // SubFiPIOV
        public int? MaxFirstRatio { get; set; } // MaxFirstRatio
        public int? MaxSecondRatio { get; set; } // MaxSecondRatio
        public int? DesiredLtv { get; set; } // DesiredLTV
        public string ArmIndexNameOv { get; set; } // ARMIndexNameOV (length: 100)
        public short ArmIndexType { get; set; } // ARMIndexType
        public string ArmIndexPublished { get; set; } // ARMIndexPublished (length: 200)
        public decimal? ArmIndexValue { get; set; } // ARMIndexValue
        public System.DateTime? ArmIndexValueForWeekEnding { get; set; } // ARMIndexValueForWeekEnding
        public decimal? ArmMargin { get; set; } // ARMMargin
        public decimal? ArmAdjustCapFirst { get; set; } // ARMAdjustCapFirst
        public decimal? ArmAdjustCapSubsequent { get; set; } // ARMAdjustCapSubsequent
        public decimal? ArmFloor { get; set; } // ARMFloor
        public decimal? ArmLifeCap { get; set; } // ARMLifeCap
        public short ArmRounding { get; set; } // ARMRounding
        public string ArmInterestRateChangesDescOv { get; set; } // ARMInterestRateChangesDescOV (length: 2147483647)
        public string ArmPaymentChangesDescOv { get; set; } // ARMPaymentChangesDescOV (length: 2147483647)
        public decimal? ArmMaxBalance { get; set; } // ARMMaxBalance
        public decimal? ArmPaymentCap { get; set; } // ARMPaymentCap
        public bool ArmConversionOption { get; set; } // ARMConversionOption
        public string ArmNegAmDesc { get; set; } // ARMNegAmDesc (length: 100)
        public int? ArmDaysNotice { get; set; } // ARMDaysNotice
        public int? ArmCommonRenewal { get; set; } // ARMCommonRenewal
        public bool ArmNoAdjCap { get; set; } // ARMNoAdjCap
        public int? ArmIntRateFixedFor { get; set; } // ARMIntRateFixedFor
        public int? ArmIntRateAdjustAt { get; set; } // ARMIntRateAdjustAt
        public string ArmAddFeatures { get; set; } // ARMAddFeatures (length: 500)
        public bool ArmCarryover { get; set; } // ARMCarryover
        public string ArmCarryoverExample { get; set; } // ARMCarryoverExample (length: 2147483647)
        public string ArmConversionCond { get; set; } // ARMConversionCond (length: 2147483647)
        public string ArmConversionOn { get; set; } // ARMConversionOn (length: 50)
        public string ArmConversionPeriod { get; set; } // ARMConversionPeriod (length: 2147483647)
        public string ArmConversionRate { get; set; } // ARMConversionRate (length: 2147483647)
        public bool ArmHasNegAmFeature { get; set; } // ARMHasNegAmFeature
        public int? ArmPaymentAdjustPeriod { get; set; } // ARMPaymentAdjustPeriod
        public int? ArmRecastPeriod { get; set; } // ARMRecastPeriod
        public decimal? ConstIntRate { get; set; } // ConstIntRate
        public int? ConstPeriod { get; set; } // ConstPeriod
        public bool CpIncludeConstPeriodInTerm { get; set; } // CPIncludeConstPeriodInTerm
        public bool CpCalcAdjFromDayOne { get; set; } // CPCalcAdjFromDayOne
        public bool CpCollectNoIntDuringCp { get; set; } // CPCollectNoIntDuringCP
        public string CpAdditionalTilText { get; set; } // CPAdditionalTILText (length: 2147483647)
        public bool IsBiWeekly { get; set; } // IsBiWeekly
        public short LoanProductType { get; set; } // LoanProductType
        public decimal? GpmStartRate { get; set; } // GPMStartRate
        public short FhaProgram { get; set; } // FHAProgram
        public decimal? HelocMaxBalance { get; set; } // HELOCMaxBalance
        public decimal? MiCoveragePerc { get; set; } // MiCoveragePerc
        public decimal? Apr { get; set; } // APR
        public decimal? DesiredCcWithoutDpAndBdpbs { get; set; } // DesiredCCWithoutDPAndBDPBS
        public decimal? DesiredDiscountPointsPbs { get; set; } // DesiredDiscountPointsPBS
        public decimal? DesiredBuydownFundsPbs { get; set; } // DesiredBuydownFundsPBS
        public decimal? DesiredPpWithout902And905Pbs { get; set; } // DesiredPPWithout902And905PBS
        public string PrepayPenaltyMemo { get; set; } // PrepayPenaltyMemo (length: 2147483647)
        public decimal? HazCoverageAmount { get; set; } // HazCoverageAmount
        public int? InterimInterestDays { get; set; } // InterimInterestDays
        public decimal? InterimInterestPbsDesired { get; set; } // InterimInterestPBSDesired
        public decimal? InterimInterestOv { get; set; } // InterimInterestOV
        public decimal? DailyInterestOv { get; set; } // DailyInterestOV
        public short MiMethod { get; set; } // MIMethod
        public int? MiltvCutoffOv { get; set; } // MILTVCutoffOV
        public int? MiLastMonth { get; set; } // MILastMonth
        public decimal? MiAnnualPremPerc { get; set; } // MIAnnualPremPerc
        public decimal? MiAnnualPrem { get; set; } // MIAnnualPrem
        public decimal? MiMoPremPerc { get; set; } // MIMoPremPerc
        public decimal? MiMoPremPerc2 { get; set; } // MIMoPremPerc2
        public short MiRefundedOrCredited { get; set; } // MIRefundedOrCredited
        public decimal? Line1008AmountOv { get; set; } // Line1008AmountOV
        public bool Line1007IncludeInPiti { get; set; } // Line1007IncludeInPITI
        public string Line1008Name { get; set; } // Line1008Name (length: 50)
        public decimal? Line1008AmountPbs { get; set; } // Line1008AmountPBS
        public bool GfePrintProvidedByBroker { get; set; } // GFEPrintProvidedByBroker
        public bool GfePrintYspText { get; set; } // GFEPrintYSPText
        public bool GfePrintProvider { get; set; } // GFEPrintProvider
        public short YspWordingOption { get; set; } // YSPWordingOption
        public string YspWordingOther { get; set; } // YSPWordingOther (length: 250)
        public short YspValueOption { get; set; } // YSPValueOption
        public decimal? YspDollarLow { get; set; } // YSPDollarLow
        public decimal? YspDollarHigh { get; set; } // YSPDollarHigh
        public decimal? YspPercLow { get; set; } // YSPPercLow
        public decimal? YspPercHigh { get; set; } // YSPPercHigh
        public decimal? CompDebtsToBePaidOffOv { get; set; } // CompDebtsToBePaidOffOV
        public short TilStatus { get; set; } // TILStatus
        public bool DemandFeature { get; set; } // DemandFeature
        public string DemandFeatureDesc { get; set; } // DemandFeatureDesc (length: 255)
        public bool VariableRateFeature { get; set; } // VariableRateFeature
        public bool CollateralSecurity { get; set; } // CollateralSecurity
        public bool DepositAccounts { get; set; } // DepositAccounts
        public short AssumptionOption { get; set; } // AssumptionOption
        public decimal? FilingRecFees { get; set; } // FilingRecFees
        public bool HazardInsRequired { get; set; } // HazardInsRequired
        public short TilHazardInsOption { get; set; } // TILHazardInsOption
        public short HazardInsAvailFromLender { get; set; } // HazardInsAvailFromLender
        public decimal? HazardInsCost { get; set; } // HazardInsCost
        public int? HazardInsTerm { get; set; } // HazardInsTerm
        public int? LateChargeDays { get; set; } // LateChargeDays
        public decimal? LateChargePerc { get; set; } // LateChargePerc
        public short LateChargeBasis { get; set; } // LateChargeBasis
        public short LateChargeWording { get; set; } // LateChargeWording
        public string LateChargeCustomDesc { get; set; } // LateChargeCustomDesc (length: 255)
        public bool AprDoesNotIncReqDep { get; set; } // APRDoesNotIncReqDep
        public short PrepaymentPenaltyOption { get; set; } // PrepaymentPenaltyOption
        public short RefundFinanceCharge { get; set; } // RefundFinanceCharge
        public int? LockDays { get; set; } // LockDays
        public short RepaymentType { get; set; } // RepaymentType
        public string CcEstimateName { get; set; } // CCEstimateName (length: 50)
        public short InterimIntDaysPerYearOv { get; set; } // InterimIntDaysPerYearOV
        public short BdInterimIntCalcMethodOv { get; set; } // BDInterimIntCalcMethodOV
        public short InterimIntDecimalsOv { get; set; } // InterimIntDecimalsOV
        public System.DateTime? MaturityDate { get; set; } // MaturityDate
        public decimal? Ltv { get; set; } // _LTV
        public decimal? Cltv { get; set; } // _CLTV
        public decimal? Hcltv { get; set; } // _HCLTV
        public decimal? Piti { get; set; } // _PITI
        public decimal Profit { get; set; } // _Profit
        public decimal? ProfitPercent { get; set; } // _ProfitPercent
        public decimal ClosingCostsAllTotal { get; set; } // _ClosingCostsAllTotal
        public decimal ClosingCostsAllPbs { get; set; } // _ClosingCostsAllPBS
        public decimal ClosingCostsAllPba { get; set; } // _ClosingCostsAllPBA
        public decimal PrepaidsWith902And905Total { get; set; } // _PrepaidsWith902And905Total
        public decimal PrepaidsWith902And905Pbs { get; set; } // _PrepaidsWith902And905PBS
        public decimal PrepaidsWith902And905Pba { get; set; } // _PrepaidsWith902And905PBA
        public decimal? ProposedFirstMortPayment { get; set; } // _ProposedFirstMortPayment
        public decimal? ProposedOtherFiPayment { get; set; } // _ProposedOtherFiPayment
        public decimal? ProposedHazPayment { get; set; } // _ProposedHazPayment
        public decimal? ProposedPropTaxesPayment { get; set; } // _ProposedPropTaxesPayment
        public decimal? ProposedMiPayment { get; set; } // _ProposedMIPayment
        public decimal? ProposedHodPayment { get; set; } // _ProposedHODPayment
        public decimal? ProposedOtherPayment { get; set; } // _ProposedOtherPayment
        public decimal? Pi { get; set; } // _PI
        public decimal? FirstRatio { get; set; } // _FirstRatio
        public decimal? SecondRatio { get; set; } // _SecondRatio
        public decimal? GapRatio { get; set; } // _GapRatio
        public short LienPosition { get; set; } // _LienPosition
        public decimal? DailyInterest { get; set; } // _DailyInterest
        public decimal? InterimInterest { get; set; } // _InterimInterest
        public decimal? TaxesAndInsurance { get; set; } // _TaxesAndInsurance
        public string LoanProgramCode { get; set; } // LoanProgramCode (length: 50)
        public int? ArmPaymentAdjustPeriodInitial { get; set; } // ARMPaymentAdjustPeriodInitial
        public decimal? ArmNegAmStartRateOv { get; set; } // ARMNegAmStartRateOV
        public System.DateTime? LockStartDate { get; set; } // LockStartDate
        public System.DateTime? LockExpirationDate { get; set; } // LockExpirationDate
        public bool LockFloating { get; set; } // LockFloating
        public short LockGuaranteedBy { get; set; } // LockGuaranteedBy
        public decimal? LockFeePercent { get; set; } // LockFeePercent
        public decimal? LockFeeAmount { get; set; } // LockFeeAmount
        public System.DateTime? FirstPaymentDate { get; set; } // FirstPaymentDate
        public string LenderCaseNo { get; set; } // LenderCaseNo (length: 50)
        public decimal? YspProfit { get; set; } // YSPProfit
        public short HmdaLoanPurpose { get; set; } // HMDALoanPurpose
        public short HmdaPreapproval { get; set; } // HMDAPreapproval
        public short HmdaTypeOfPurchaser { get; set; } // HMDATypeOfPurchaser
        public short HmdahoepaStatus { get; set; } // HMDAHOEPAStatus
        public decimal? HmdaRateSpread { get; set; } // HMDARateSpread
        public bool HmdaRateSpreadNa { get; set; } // HMDARateSpreadNA
        public short HmdaDenialReason1 { get; set; } // HMDADenialReason1
        public short HmdaDenialReason2 { get; set; } // HMDADenialReason2
        public short HmdaDenialReason3 { get; set; } // HMDADenialReason3
        public short HmdaLienStatus { get; set; } // HMDALienStatus
        public short HmdaLoanType { get; set; } // _HMDALoanType
        public string LoanAmountInThousands { get; set; } // _LoanAmountInThousands (length: 8)
        public int? PrepayPenaltyTerm { get; set; } // PrepayPenaltyTerm
        public short QualIntOnlyAtAmortizingPaymentOv { get; set; } // QualIntOnlyAtAmortizingPaymentOV
        public decimal? Line1008Amount { get; set; } // _Line1008Amount
        public decimal? Line1008AmountPba { get; set; } // _Line1008AmountPBA
        public decimal? Ca885FixedIntRate { get; set; } // CA885FixedIntRate
        public decimal? Ca885IntOnlyFirst5IntRate { get; set; } // CA885IntOnlyFirst5IntRate
        public decimal? Ca885FiveOneArmIntRate { get; set; } // CA885FiveOneARMIntRate
        public decimal? Ca885IntOnlyFirst5ArmIntRate { get; set; } // CA885IntOnlyFirst5ARMIntRate
        public decimal? Ca885OptionPaymentIntRate1 { get; set; } // CA885OptionPaymentIntRate1
        public decimal? Ca885OptionPaymentIntRate2To60 { get; set; } // CA885OptionPaymentIntRate2To60
        public string Ca885ProposedLoanExplanation { get; set; } // CA885ProposedLoanExplanation (length: 100)
        public string Ca885Years1To5ExplanationOv { get; set; } // CA885Years1To5ExplanationOV (length: 35)
        public string Ca885Year6Explanation { get; set; } // CA885Year6Explanation (length: 35)
        public string Ca885Year6With2PercRiseExplanation { get; set; } // CA885Year6With2PercRiseExplanation (length: 35)
        public string Ca885Year6With5PercRiseExplanation { get; set; } // CA885Year6With5PercRiseExplanation (length: 35)
        public decimal? Ca885PaymentYears1To5Ov { get; set; } // CA885PaymentYears1To5OV
        public decimal? Ca885PaymentYear6Ov { get; set; } // CA885PaymentYear6OV
        public decimal? Ca885PaymentYear6With2PercRiseOv { get; set; } // CA885PaymentYear6With2PercRiseOV
        public decimal? Ca885PaymentYear6With5PercRiseOv { get; set; } // CA885PaymentYear6With5PercRiseOV
        public decimal? Ca885BalanceYear5Ov { get; set; } // CA885BalanceYear5OV
        public int InterimInterestPaidByOtherType { get; set; } // InterimInterestPaidByOtherType
        public int MipffPaidInCashPaidByOtherType { get; set; } // MIPFFPaidInCashPaidByOtherType
        public int Line1008PaidByOtherType { get; set; } // Line1008PaidByOtherType
        public string Ca885CertifiedBy { get; set; } // CA885CertifiedBy (length: 50)
        public bool IsClosingDocsLoan { get; set; } // _IsClosingDocsLoan
        public bool IsLoanOfRecord { get; set; } // _IsLoanOfRecord
        public string RateSheetId { get; set; } // RateSheetID (length: 50)
        public decimal? BuyPriceBase { get; set; } // BuyPriceBase
        public decimal? BuyPriceAdjustments { get; set; } // _BuyPriceAdjustments
        public decimal? BuyPriceNet { get; set; } // _BuyPriceNet
        public string BuyCommitmentNo { get; set; } // BuyCommitmentNo (length: 50)
        public decimal? ForbearanceAmount { get; set; } // ForbearanceAmount
        public decimal? FhaMaxLoanLimit { get; set; } // _FHAMaxLoanLimit
        public short EntityOnTilOption { get; set; } // EntityOnTILOption
        public decimal? MostRecentlyDisclosedApr { get; set; } // MostRecentlyDisclosedAPR
        public decimal? AprVariance { get; set; } // _APRVariance
        public System.DateTime? GfeIntRateAvailThroughDate { get; set; } // GFEIntRateAvailThroughDate
        public System.DateTime? GfeChargesAvailThroughDate { get; set; } // GFEChargesAvailThroughDate
        public int? GfeLockDaysBeforeSettlement { get; set; } // GFELockDaysBeforeSettlement
        public bool GfeShowTradeoffOptions { get; set; } // GFEShowTradeoffOptions
        public decimal? GfeTradeoffIntRate2 { get; set; } // GFETradeoffIntRate2
        public decimal? GfeTradeoffIntRate3 { get; set; } // GFETradeoffIntRate3
        public decimal? GfePrepaymentPenaltyMax { get; set; } // GFEPrepaymentPenaltyMax
        public bool GfeDelivered { get; set; } // GFEDelivered
        public decimal? GfeInitialLoanAmountOv { get; set; } // GFEInitialLoanAmountOV
        public decimal? GfepimiInitialAmountOv { get; set; } // GFEPIMIInitialAmountOV
        public short GfepimiIncludesPrincipalOv { get; set; } // GFEPIMIIncludesPrincipalOV
        public short GfepimiIncludesInterestOv { get; set; } // GFEPIMIIncludesInterestOV
        public short GfepimiIncludesMiov { get; set; } // GFEPIMIIncludesMIOV
        public short GfeIntRateCanRiseOv { get; set; } // GFEIntRateCanRiseOV
        public short GfeBalanceCanRiseOv { get; set; } // GFEBalanceCanRiseOV
        public short GfepimiCanRiseOv { get; set; } // GFEPIMICanRiseOV
        public string GfeIntRateFirstChangeIntervalDescOv { get; set; } // GFEIntRateFirstChangeIntervalDescOV (length: 50)
        public string GfeIntRateFirstChangeDateDescOv { get; set; } // GFEIntRateFirstChangeDateDescOV (length: 50)
        public string GfeIntRateNextChangeIntervalDescOv { get; set; } // GFEIntRateNextChangeIntervalDescOV (length: 50)
        public decimal? GfeIntRateAdjCapOv { get; set; } // GFEIntRateAdjCapOV
        public decimal? GfeIntRateMinOv { get; set; } // GFEIntRateMinOV
        public decimal? GfeIntRateMaxOv { get; set; } // GFEIntRateMaxOV
        public decimal? GfeMaxBalanceOv { get; set; } // GFEMaxBalanceOV
        public string GfepimiFirstIncreaseIntervalDescOv { get; set; } // GFEPIMIFirstIncreaseIntervalDescOV (length: 50)
        public string GfepimiFirstIncreaseDateDescOv { get; set; } // GFEPIMIFirstIncreaseDateDescOV (length: 50)
        public decimal? GfepimiFirstIncreaseAmountOv { get; set; } // GFEPIMIFirstIncreaseAmountOV
        public decimal? GfepimiMaxAmountOv { get; set; } // GFEPIMIMaxAmountOV
        public decimal? GfeBalloonPaymentOv { get; set; } // GFEBalloonPaymentOV
        public decimal? GfeTradeoffCostChangePerc2 { get; set; } // GFETradeoffCostChangePerc2
        public decimal? GfeTradeoffCostChangePerc3 { get; set; } // GFETradeoffCostChangePerc3
        public decimal? GfeTradeoffCostChangeAmount2Ov { get; set; } // GFETradeoffCostChangeAmount2OV
        public decimal? GfeTradeoffCostChangeAmount3Ov { get; set; } // GFETradeoffCostChangeAmount3OV
        public short GfeEscrowAllPropTaxesOv { get; set; } // GFEEscrowAllPropTaxesOV
        public short GfeEscrowAllInsuranceOv { get; set; } // GFEEscrowAllInsuranceOV
        public short GfeEscrowOtherOv { get; set; } // GFEEscrowOtherOV
        public string GfeEscrowOtherDescOv { get; set; } // GFEEscrowOtherDescOV (length: 50)
        public bool NoCostLoan { get; set; } // NoCostLoan
        public bool GfeIntRateAvailThroughDateNa { get; set; } // GFEIntRateAvailThroughDateNA
        public bool GfeLockDaysNa { get; set; } // GFELockDaysNA
        public bool GfeLockDaysBeforeSettlementNa { get; set; } // GFELockDaysBeforeSettlementNA
        public decimal? InterimIntGfeDisclosedAmount { get; set; } // InterimIntGFEDisclosedAmount
        public decimal? MipffgfeDisclosedAmount { get; set; } // MIPFFGFEDisclosedAmount
        public decimal? EscrowDepositGfeDisclosedAmount { get; set; } // EscrowDepositGFEDisclosedAmount
        public decimal? YspAmount1 { get; set; } // YSPAmount1
        public decimal? YspAmount2 { get; set; } // YSPAmount2
        public decimal? YspGfeDisclosedAmount1 { get; set; } // YSP_GFEDisclosedAmount1
        public decimal? YspGfeDisclosedAmount2 { get; set; } // YSP_GFEDisclosedAmount2
        public string ChangedCircumstanceExplanation { get; set; } // ChangedCircumstanceExplanation (length: 2147483647)
        public decimal? NoCostLenderCreditGfeDisclosedAmount { get; set; } // NoCostLenderCreditGFEDisclosedAmount
        public short GfeOwnersTitleNaWhenZeroOv { get; set; } // GFEOwnersTitleNAWhenZeroOV
        public decimal? PaidToBrokerAdjustment { get; set; } // PaidToBrokerAdjustment
        public decimal? PaidToLenderAdjustment { get; set; } // PaidToLenderAdjustment
        public decimal? PaidToInvestorAdjustment { get; set; } // PaidToInvestorAdjustment
        public decimal? PaidToBrokerTotal { get; set; } // _PaidToBrokerTotal
        public decimal? PaidToLenderTotal { get; set; } // _PaidToLenderTotal
        public decimal? PaidToInvestorTotal { get; set; } // _PaidToInvestorTotal
        public int IsHpml { get; set; } // _IsHPML
        public bool IsActiveOrLinkedLoan { get; set; } // _IsActiveOrLinkedLoan
        public short GfeZeroCreditOrChargeOption { get; set; } // GFEZeroCreditOrChargeOption
        public decimal GfeBlockOurOriginationCharge { get; set; } // _GFEBlockOurOriginationCharge
        public decimal GfeBlockCreditOrChargeForIntRate { get; set; } // _GFEBlockCreditOrChargeForIntRate
        public decimal GfeBlockServicesLenderSelected { get; set; } // _GFEBlockServicesLenderSelected
        public decimal GfeBlockTitleServices { get; set; } // _GFEBlockTitleServices
        public decimal GfeBlockOwnersTitle { get; set; } // _GFEBlockOwnersTitle
        public decimal GfeBlockServicesYouCanShopFor { get; set; } // _GFEBlockServicesYouCanShopFor
        public decimal GfeBlockRecordingCharges { get; set; } // _GFEBlockRecordingCharges
        public decimal GfeBlockTransferTaxes { get; set; } // _GFEBlockTransferTaxes
        public decimal? GfeBlockEscrowDeposit { get; set; } // _GFEBlockEscrowDeposit
        public decimal? GfeBlockInterimInt { get; set; } // _GFEBlockInterimInt
        public decimal GfeBlockInsurancePremium { get; set; } // _GFEBlockInsurancePremium
        public decimal GfeAdjustedOriginationCharge { get; set; } // _GFEAdjustedOriginationCharge
        public decimal GfeSettlementChargesAllOther { get; set; } // _GFESettlementChargesAllOther
        public decimal GfeSettlementChargesTotal { get; set; } // _GFESettlementChargesTotal
        public decimal Hud1OurOriginationCharge { get; set; } // _HUD1OurOriginationCharge
        public decimal Hud1CreditOrChargeForIntRate { get; set; } // _HUD1CreditOrChargeForIntRate
        public decimal Hud1AdjustedOriginationCharge { get; set; } // _HUD1AdjustedOriginationCharge
        public decimal? YspPerc1 { get; set; } // YSPPerc1
        public decimal? YspPerc2 { get; set; } // YSPPerc2
        public decimal? DailyInterestGfeDisclosed { get; set; } // DailyInterestGFEDisclosed
        public int? InterimInterestDaysGfeDisclosed { get; set; } // InterimInterestDaysGFEDisclosed
        public int? GfeLockToSettlementDays { get; set; } // GFELockToSettlementDays
        public int? GfeTermOv { get; set; } // GFETermOV
        public short GfeHasBalloonPaymentOv { get; set; } // GFEHasBalloonPaymentOV
        public decimal? FinanceCharge { get; set; } // FinanceCharge
        public decimal? TotalOfPayments { get; set; } // TotalOfPayments
        public int? LockExtension1Days { get; set; } // LockExtension1Days
        public int? LockExtension2Days { get; set; } // LockExtension2Days
        public decimal? BuySrp { get; set; } // BuySRP
        public decimal? RefinanceCashOutAmount { get; set; } // RefinanceCashOutAmount
        public decimal? HelocAnnualFee { get; set; } // HELOCAnnualFee
        public decimal? ConstIntReserves { get; set; } // ConstIntReserves
        public int? HelocDrawAccessTerm { get; set; } // HELOCDrawAccessTerm
        public bool PortfolioRefi { get; set; } // PortfolioRefi
        public decimal? HelocTerminationFee { get; set; } // HELOCTerminationFee
        public string GfeIntRateAvailableThroughDateExtra { get; set; } // GFEIntRateAvailableThroughDateExtra (length: 30)
        public string BuyPricedInvestor { get; set; } // BuyPricedInvestor (length: 50)
        public decimal? BuyProfitNet { get; set; } // _BuyProfitNet
        public System.DateTime? UnextendedLockExpDate { get; set; } // UnextendedLockExpDate
        public System.DateTime? PpeLockExpDate { get; set; } // PPELockExpDate
        public System.DateTime? PpeTimePriced { get; set; } // PPETimePriced
        public int? LockExtension3Days { get; set; } // LockExtension3Days
        public int? PricedLockDays { get; set; } // PricedLockDays
        public decimal? ApprovedRate { get; set; } // ApprovedRate
        public System.Guid? LoanGuid { get; set; } // LoanGUID
        public int InterimIntDayCountCalcMethodOv { get; set; } // InterimIntDayCountCalcMethodOV
        public bool IsNegAmLoanUnderTila { get; set; } // IsNegAmLoanUnderTILA
        public bool IsPreferredRateLoan { get; set; } // IsPreferredRateLoan
        public decimal? PreferredRateDiscount { get; set; } // PreferredRateDiscount
        public int? PreferredRateFixedForMonths { get; set; } // PreferredRateFixedForMonths
        public int SharedEquityOrAppreciationOption { get; set; } // SharedEquityOrAppreciationOption
        public string AntiSteeringCreditor { get; set; } // AntiSteeringCreditor (length: 50)
        public decimal? AntiSteeringPointsAndFeesOv { get; set; } // AntiSteeringPointsAndFeesOV
        public int ReverseMortgageType { get; set; } // ReverseMortgageType
        public int ReverseMortgageLoanPurpose { get; set; } // ReverseMortgageLoanPurpose
        public decimal? McrFeesCollected { get; set; } // _MCRFeesCollected
        public decimal? McrFeesCollectedOv { get; set; } // MCRFeesCollectedOV
        public bool ExcludeFromHmda { get; set; } // ExcludeFromHMDA
        public bool ExcludeFromNmlsCallReport { get; set; } // ExcludeFromNMLSCallReport
        public int? HelocRepaymentTerm { get; set; } // HELOCRepaymentTerm
        public decimal? HelocMinDrawAmount { get; set; } // HELOCMinDrawAmount
        public bool ExcludeFromRegulatorConnect { get; set; } // ExcludeFromRegulatorConnect
        public System.DateTime? LenderRegisteredDate { get; set; } // LenderRegisteredDate
        public int? ArmLookbackDays { get; set; } // ARMLookbackDays
        public bool IsRelocationLoan { get; set; } // IsRelocationLoan
        public bool BalloonResetIndicator { get; set; } // BalloonResetIndicator
        public decimal? LenderPaidMiInterestRateAdjustmentPercent { get; set; } // LenderPaidMIInterestRateAdjustmentPercent
        public int? ExtendableBalloonMaxTerm { get; set; } // ExtendableBalloonMaxTerm
        public decimal? CreditSaleDownPaymentOv { get; set; } // CreditSaleDownPaymentOV
        public decimal? DotOtherCreditAmount3 { get; set; } // DOTOtherCreditAmount3
        public decimal? DotOtherCreditAmount4 { get; set; } // DOTOtherCreditAmount4
        public string DotOtherCreditDescOv3 { get; set; } // DOTOtherCreditDescOV3 (length: 50)
        public string DotOtherCreditDescOv4 { get; set; } // DOTOtherCreditDescOV4 (length: 50)
        public short DotOtherCreditType3 { get; set; } // DOTOtherCreditType3
        public short DotOtherCreditType4 { get; set; } // DOTOtherCreditType4
        public bool GfeLenderCreditLumpSumIncludedInDot { get; set; } // GFELenderCreditLumpSumIncludedInDOT
        public decimal? Haz2CoverageAmount { get; set; } // Haz2CoverageAmount
        public decimal? DotLenderCreditAmount { get; set; } // _DOTLenderCreditAmount
        public int IsJumbo { get; set; } // IsJumbo
        public bool IsAltAOrSubprime { get; set; } // IsAltAOrSubprime
        public bool IsOtherMortgageTypeGovLoan { get; set; } // IsOtherMortgageTypeGovLoan
        public int NmlsInvestorTypeOv { get; set; } // NMLSInvestorTypeOV
        public decimal? BuySrpAdjustments { get; set; } // _BuySRPAdjustments
        public decimal? BuySrpNet { get; set; } // _BuySRPNet
        public decimal? UndiscountedIntRate { get; set; } // UndiscountedIntRate
        public string PpePricedProductName { get; set; } // PPEPricedProductName (length: 150)
        public short BonaFideDiscountPointsIndicator { get; set; } // BonaFideDiscountPointsIndicator
        public System.DateTime? LockCanceledDate { get; set; } // LockCanceledDate
        public string FannieArmPlanNo { get; set; } // FannieARMPlanNo (length: 10)
        public int ArmQualRateOption { get; set; } // ARMQualRateOption
        public int AtrAssessmentMethod { get; set; } // ATRAssessmentMethod
        public int QmTestResult { get; set; } // QMTestResult
        public decimal? UndiscountedPrice { get; set; } // UndiscountedPrice
        public decimal? BrokerCompAtTimeRateSetOv { get; set; } // BrokerCompAtTimeRateSetOV
        public decimal? ArmIndexValueAtTimeRateSetOv { get; set; } // ARMIndexValueAtTimeRateSetOV
        public int HcmTestResult { get; set; } // HCMTestResult
        public decimal? PointsAndFees { get; set; } // PointsAndFees
        public decimal? HcmApr { get; set; } // HCM_APR
        public int IsJumboForHpmlOv { get; set; } // IsJumboForHPML_OV
        public decimal? BonaFideDiscountPointsPercOv { get; set; } // BonaFideDiscountPointsPercOV
        public decimal? QualRate { get; set; } // _QualRate
        public System.DateTime? RegulatoryLockDateOv { get; set; } // RegulatoryLockDateOV
        public bool ArmIndexValueIsLocked { get; set; } // ARMIndexValueIsLocked
        public decimal? OccupantFirstRatio { get; set; } // _OccupantFirstRatio
        public decimal? OccupantSecondRatio { get; set; } // _OccupantSecondRatio
        public string LockExpDateTimeOfDayOv { get; set; } // LockExpDateTimeOfDayOV (length: 20)
        public decimal? LenderCreditGfeDisclosedAmount { get; set; } // LenderCreditGFEDisclosedAmount
        public bool UseAlternativeCashToCloseTable { get; set; } // UseAlternativeCashToCloseTable
        public int IntOnlyQualPaymentCalcOption { get; set; } // IntOnlyQualPaymentCalcOption
        public int TridLoanPurposeOv { get; set; } // TRIDLoanPurposeOV
        public bool IsGrantLoan { get; set; } // IsGrantLoan
        public bool Line1003ExcludeFromPiti { get; set; } // Line1003ExcludeFromPITI
        public int LockExpDateTimeZoneCityOv { get; set; } // LockExpDateTimeZoneCityOV
        public decimal? ConstInitialDraw { get; set; } // ConstInitialDraw
        public bool CpCollectMiDuringConstPeriod { get; set; } // CPCollectMIDuringConstPeriod
        public int IsRescindableOv { get; set; } // IsRescindableOV
        public int ConstructionPurpose { get; set; } // ConstructionPurpose
        public bool IsRescindable { get; set; } // _IsRescindable
        public decimal? MilitaryApr { get; set; } // MilitaryAPR
        public int MilitaryAprTestResult { get; set; } // _MilitaryAPRTestResult
        public int HmdaDenialReason4 { get; set; } // HMDADenialReason4
        public int HmdaLoanPurpose2 { get; set; } // HMDALoanPurpose2
        public int MipffFinancingCalcOption { get; set; } // MIPFFFinancingCalcOption
        public int ArmConversionStatus { get; set; } // ARMConversionStatus
        public byte RefiTypeUrlaov { get; set; } // RefiTypeURLAOV
        public byte RefiProgramOv { get; set; } // RefiProgramOV
        public string RefiProgramOther { get; set; } // RefiProgramOther (length: 20)

        public Loan()
        {
            FileDataId = 0;
            DisplayOrder = 0;
            SellerBreakDown = false;
            LoanPurpose = 0;
            TreatConstAsRefi = false;
            RefiPurpAu = 0;
            RefiTypeFha = 0;
            RefiTypeVa = 0;
            AmortizationType = 0;
            MortgageType = 0;
            BuyDowns = 0;
            SubFiType = 0;
            SubFiIntOnly = false;
            ArmIndexType = 0;
            ArmRounding = 0;
            ArmConversionOption = false;
            ArmNoAdjCap = false;
            ArmCarryover = false;
            ArmHasNegAmFeature = false;
            CpIncludeConstPeriodInTerm = false;
            CpCalcAdjFromDayOne = false;
            CpCollectNoIntDuringCp = false;
            IsBiWeekly = false;
            LoanProductType = 0;
            FhaProgram = 0;
            MiMethod = 0;
            MiRefundedOrCredited = 0;
            Line1007IncludeInPiti = false;
            GfePrintProvidedByBroker = false;
            GfePrintYspText = false;
            GfePrintProvider = false;
            YspWordingOption = 0;
            YspValueOption = 0;
            TilStatus = 0;
            DemandFeature = false;
            VariableRateFeature = false;
            CollateralSecurity = false;
            DepositAccounts = false;
            AssumptionOption = 0;
            HazardInsRequired = false;
            TilHazardInsOption = 0;
            HazardInsAvailFromLender = 0;
            LateChargeBasis = 0;
            LateChargeWording = 0;
            AprDoesNotIncReqDep = false;
            PrepaymentPenaltyOption = 0;
            RefundFinanceCharge = 0;
            RepaymentType = 0;
            InterimIntDaysPerYearOv = 0;
            BdInterimIntCalcMethodOv = 0;
            InterimIntDecimalsOv = 0;
            Profit = 0m;
            ClosingCostsAllTotal = 0m;
            ClosingCostsAllPbs = 0m;
            ClosingCostsAllPba = 0m;
            PrepaidsWith902And905Total = 0m;
            PrepaidsWith902And905Pbs = 0m;
            PrepaidsWith902And905Pba = 0m;
            LienPosition = 0;
            LockFloating = false;
            LockGuaranteedBy = 0;
            HmdaLoanPurpose = 0;
            HmdaPreapproval = 0;
            HmdaTypeOfPurchaser = 0;
            HmdahoepaStatus = 0;
            HmdaRateSpreadNa = false;
            HmdaDenialReason1 = 0;
            HmdaDenialReason2 = 0;
            HmdaDenialReason3 = 0;
            HmdaLienStatus = 0;
            HmdaLoanType = 0;
            QualIntOnlyAtAmortizingPaymentOv = 0;
            InterimInterestPaidByOtherType = 0;
            MipffPaidInCashPaidByOtherType = 0;
            Line1008PaidByOtherType = 0;
            IsClosingDocsLoan = false;
            IsLoanOfRecord = false;
            EntityOnTilOption = 0;
            GfeShowTradeoffOptions = false;
            GfeDelivered = false;
            GfepimiIncludesPrincipalOv = 0;
            GfepimiIncludesInterestOv = 0;
            GfepimiIncludesMiov = 0;
            GfeIntRateCanRiseOv = 0;
            GfeBalanceCanRiseOv = 0;
            GfepimiCanRiseOv = 0;
            GfeEscrowAllPropTaxesOv = 0;
            GfeEscrowAllInsuranceOv = 0;
            GfeEscrowOtherOv = 0;
            NoCostLoan = false;
            GfeIntRateAvailThroughDateNa = false;
            GfeLockDaysNa = false;
            GfeLockDaysBeforeSettlementNa = false;
            GfeOwnersTitleNaWhenZeroOv = 0;
            IsHpml = 0;
            IsActiveOrLinkedLoan = false;
            GfeZeroCreditOrChargeOption = 0;
            GfeBlockOurOriginationCharge = 0m;
            GfeBlockCreditOrChargeForIntRate = 0m;
            GfeBlockServicesLenderSelected = 0m;
            GfeBlockTitleServices = 0m;
            GfeBlockOwnersTitle = 0m;
            GfeBlockServicesYouCanShopFor = 0m;
            GfeBlockRecordingCharges = 0m;
            GfeBlockTransferTaxes = 0m;
            GfeBlockInsurancePremium = 0m;
            GfeAdjustedOriginationCharge = 0m;
            GfeSettlementChargesAllOther = 0m;
            GfeSettlementChargesTotal = 0m;
            Hud1OurOriginationCharge = 0m;
            Hud1CreditOrChargeForIntRate = 0m;
            Hud1AdjustedOriginationCharge = 0m;
            GfeHasBalloonPaymentOv = 0;
            PortfolioRefi = false;
            LoanGuid = System.Guid.NewGuid();
            InterimIntDayCountCalcMethodOv = 0;
            IsNegAmLoanUnderTila = false;
            IsPreferredRateLoan = false;
            SharedEquityOrAppreciationOption = 0;
            ReverseMortgageType = 0;
            ReverseMortgageLoanPurpose = 0;
            ExcludeFromHmda = false;
            ExcludeFromNmlsCallReport = false;
            ExcludeFromRegulatorConnect = false;
            IsRelocationLoan = false;
            BalloonResetIndicator = false;
            DotOtherCreditType3 = 0;
            DotOtherCreditType4 = 0;
            GfeLenderCreditLumpSumIncludedInDot = false;
            IsJumbo = 0;
            IsAltAOrSubprime = false;
            IsOtherMortgageTypeGovLoan = false;
            NmlsInvestorTypeOv = 0;
            BonaFideDiscountPointsIndicator = 0;
            ArmQualRateOption = 0;
            AtrAssessmentMethod = 0;
            QmTestResult = 0;
            HcmTestResult = 0;
            IsJumboForHpmlOv = 0;
            ArmIndexValueIsLocked = false;
            UseAlternativeCashToCloseTable = false;
            IntOnlyQualPaymentCalcOption = 0;
            TridLoanPurposeOv = 0;
            IsGrantLoan = false;
            Line1003ExcludeFromPiti = false;
            LockExpDateTimeZoneCityOv = 0;
            CpCollectMiDuringConstPeriod = false;
            IsRescindableOv = 0;
            ConstructionPurpose = 0;
            IsRescindable = false;
            MilitaryAprTestResult = 0;
            HmdaDenialReason4 = 0;
            HmdaLoanPurpose2 = 0;
            MipffFinancingCalcOption = 0;
            ArmConversionStatus = 0;
            RefiTypeUrlaov = 0;
            RefiProgramOv = 0;
        }


        public static Loan Create(LoanApplication loanApplication,
                                  LoanRequest loanRequest, ThirdPartyCodeList thirdPartyCodeList)
        {

            var loan = new Loan();
            loan.BaseLoan = loanApplication.LoanAmount;
            loan.PurPrice = loanApplication.PropertyInfo.PropertyValue;
            loan.SubFiBaseLoan = loanRequest.MortgageToBeSubordinate;


            loan.AmortizationType = (short)thirdPartyCodeList.GetByteProValue("AmortizationType",
                                                                              loanApplication.ProductAmortizationTypeId).FindEnumIndex(typeof(AmortizationTypeEnum));

            loan.MortgageType = (short)thirdPartyCodeList.GetByteProValue("MortgageType",
                                                                          loanApplication.LoanTypeId).FindEnumIndex(typeof(MortgageTypeEnum));
            loan.LoanPurpose = (short)thirdPartyCodeList.GetByteProValue("LoanPurpose",
                                                                         loanApplication.LoanPurposeId).FindEnumIndex(typeof(LoanPurposeEnum));
            loan.RefinanceCashOutAmount = loanApplication.CashOutAmount;

            return loan;
        }

        public  void Update(LoanApplication loanApplication,
                            LoanRequest loanRequest, ThirdPartyCodeList thirdPartyCodeList
                                  )
        {


            
            this.BaseLoan = loanApplication.LoanAmount;
            this.PurPrice = loanApplication.PropertyInfo.PropertyValue;
            this.SubFiBaseLoan = loanRequest.MortgageToBeSubordinate;
            this.AmortizationType = (short)thirdPartyCodeList.GetByteProValue("AmortizationType",
                                                                              loanApplication.ProductAmortizationTypeId).FindEnumIndex(typeof(AmortizationTypeEnum));
            this.MortgageType = (short)thirdPartyCodeList.GetByteProValue("MortgageType",
                                                                          loanApplication.LoanTypeId).FindEnumIndex(typeof(MortgageTypeEnum));
            this.LoanPurpose = (short)thirdPartyCodeList.GetByteProValue("LoanPurpose",
                                                                         loanApplication.LoanPurposeId).FindEnumIndex(typeof(LoanPurposeEnum));
            this.RefinanceCashOutAmount = loanApplication.CashOutAmount;

            

        }
    }

    // LoanPayment

    public class LoanPayment
    {
        public int LoanPaymentId { get; set; } // LoanPaymentID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int DisplayOrder { get; set; } // DisplayOrder
        public System.DateTime? DueDate { get; set; } // DueDate
        public decimal? AmountDue { get; set; } // AmountDue
        public System.DateTime? ReceivedDate { get; set; } // ReceivedDate
        public System.DateTime? DepositedDate { get; set; } // DepositedDate
        public decimal? AmountPaid { get; set; } // AmountPaid
        public string CheckNo { get; set; } // CheckNo (length: 50)
        public decimal? CreditedPrincipal { get; set; } // CreditedPrincipal
        public decimal? CreditedInterest { get; set; } // CreditedInterest
        public decimal? CreditedEscrowFunds { get; set; } // CreditedEscrowFunds
        public decimal? CreditedBuydownFunds { get; set; } // CreditedBuydownFunds
        public decimal? CreditedLateFees { get; set; } // CreditedLateFees
        public decimal? CreditedTotal { get; set; } // _CreditedTotal
        public string Notes { get; set; } // Notes (length: 2147483647)
        public short LoanPaymentType { get; set; } // LoanPaymentType
        public decimal? CreditedMi { get; set; } // CreditedMI
        public string PayorPayee { get; set; } // PayorPayee (length: 50)
        public decimal? CreditedEscrowExclMi { get; set; } // CreditedEscrowExclMI

        public LoanPayment()
        {
            FileDataId = 0;
            DisplayOrder = 0;
            LoanPaymentType = 0;
        }
    }

    // LoanProgram

    public class LoanProgram
    {
        public int LoanProgramId { get; set; } // LoanProgramID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string LoanProgramName { get; set; } // LoanProgramName (length: 100)
        public short MortgageType { get; set; } // MortgageType
        public string MortgageTypeOther { get; set; } // MortgageTypeOther (length: 50)
        public short FhaProgram { get; set; } // FHAProgram
        public short BuyDowns { get; set; } // BuyDowns
        public int? Term { get; set; } // Term
        public int? BalloonTerm { get; set; } // BalloonTerm
        public decimal? IntRate { get; set; } // IntRate
        public decimal? QualRateOv { get; set; } // QualRateOV
        public bool IsBiWeekly { get; set; } // IsBiWeekly
        public int? InterestOnlyPeriod { get; set; } // InterestOnlyPeriod
        public short LoanProductType { get; set; } // LoanProductType
        public string ArmIndexNameOv { get; set; } // ARMIndexNameOV (length: 100)
        public short ArmIndexType { get; set; } // ARMIndexType
        public string ArmIndexPublished { get; set; } // ARMIndexPublished (length: 200)
        public decimal? ArmIndexValue { get; set; } // ARMIndexValue
        public System.DateTime? ArmIndexValueForWeekEnding { get; set; } // ARMIndexValueForWeekEnding
        public decimal? ArmMargin { get; set; } // ARMMargin
        public bool ArmNoAdjCap { get; set; } // ARMNoAdjCap
        public decimal? ArmAdjustCapFirst { get; set; } // ARMAdjustCapFirst
        public decimal? ArmAdjustCapSubsequent { get; set; } // ARMAdjustCapSubsequent
        public decimal? ArmFloor { get; set; } // ARMFloor
        public decimal? ArmLifeCap { get; set; } // ARMLifeCap
        public short ArmRounding { get; set; } // ARMRounding
        public string ArmInterestRateChangesDescOv { get; set; } // ARMInterestRateChangesDescOV (length: 2147483647)
        public string ArmPaymentChangesDescOv { get; set; } // ARMPaymentChangesDescOV (length: 2147483647)
        public decimal? ArmMaxBalance { get; set; } // ARMMaxBalance
        public decimal? ArmPaymentCap { get; set; } // ARMPaymentCap
        public bool ArmConversionOption { get; set; } // ARMConversionOption
        public string ArmNegAmDesc { get; set; } // ARMNegAmDesc (length: 100)
        public int? ArmDaysNotice { get; set; } // ARMDaysNotice
        public int? ArmCommonRenewal { get; set; } // ARMCommonRenewal
        public int? ArmIntRateFixedFor { get; set; } // ARMIntRateFixedFor
        public int? ArmIntRateAdjustAt { get; set; } // ARMIntRateAdjustAt
        public string ArmAddFeatures { get; set; } // ARMAddFeatures (length: 500)
        public bool ArmCarryover { get; set; } // ARMCarryover
        public string ArmCarryoverExample { get; set; } // ARMCarryoverExample (length: 2147483647)
        public string ArmConversionCond { get; set; } // ARMConversionCond (length: 2147483647)
        public string ArmConversionOn { get; set; } // ARMConversionOn (length: 50)
        public string ArmConversionPeriod { get; set; } // ARMConversionPeriod (length: 2147483647)
        public string ArmConversionRate { get; set; } // ARMConversionRate (length: 2147483647)
        public bool ArmHasNegAmFeature { get; set; } // ARMHasNegAmFeature
        public int? ArmPaymentAdjustPeriod { get; set; } // ARMPaymentAdjustPeriod
        public int? ArmRecastPeriod { get; set; } // ARMRecastPeriod
        public string PrepayPenaltyMemo { get; set; } // PrepayPenaltyMemo (length: 2147483647)
        public bool DemandFeature { get; set; } // DemandFeature
        public string DemandFeatureDesc { get; set; } // DemandFeatureDesc (length: 255)
        public bool VariableRateFeature { get; set; } // VariableRateFeature
        public bool CopySecurityFields { get; set; } // CopySecurityFields
        public bool CollateralSecurity { get; set; } // CollateralSecurity
        public bool DepositAccounts { get; set; } // DepositAccounts
        public short AssumptionOption { get; set; } // AssumptionOption
        public decimal? FilingRecFees { get; set; } // FilingRecFees
        public bool CopyHazardFields { get; set; } // CopyHazardFields
        public bool HazardInsRequired { get; set; } // HazardInsRequired
        public short TilHazardInsOption { get; set; } // TILHazardInsOption
        public short HazardInsAvailFromLender { get; set; } // HazardInsAvailFromLender
        public bool CopyLateChargeFields { get; set; } // CopyLateChargeFields
        public int? LateChargeDays { get; set; } // LateChargeDays
        public decimal? LateChargePerc { get; set; } // LateChargePerc
        public short LateChargeBasis { get; set; } // LateChargeBasis
        public short LateChargeWording { get; set; } // LateChargeWording
        public string LateChargeCustomDesc { get; set; } // LateChargeCustomDesc (length: 255)
        public short PrepaymentPenaltyOption { get; set; } // PrepaymentPenaltyOption
        public short RefundFinanceCharge { get; set; } // RefundFinanceCharge
        public int? MaxFirstRatio { get; set; } // MaxFirstRatio
        public int? MaxSecondRatio { get; set; } // MaxSecondRatio
        public string LoanProgramCode { get; set; } // LoanProgramCode (length: 50)
        public int MinCreditScore { get; set; } // MinCreditScore
        public decimal? MaxLtv { get; set; } // MaxLTV
        public decimal? MaxCltv { get; set; } // MaxCLTV
        public decimal? MaxLoanAmount { get; set; } // MaxLoanAmount
        public int OccupancyTypeFlags { get; set; } // OccupancyTypeFlags
        public int LoanPurposeFlags { get; set; } // LoanPurposeFlags
        public int SubjectPropertyFlags { get; set; } // SubjectPropertyFlags
        public int? ArmPaymentAdjustPeriodInitial { get; set; } // ARMPaymentAdjustPeriodInitial
        public decimal? ArmNegAmStartRateOv { get; set; } // ARMNegAmStartRateOV
        public string Ca885LoanExplanation { get; set; } // CA885LoanExplanation (length: 100)
        public string DocPrepCode { get; set; } // DocPrepCode (length: 50)
        public bool IsNegAmLoanUnderTila { get; set; } // IsNegAmLoanUnderTILA
        public int LtvRoundingMethod { get; set; } // LTVRoundingMethod
        public int? ArmLookbackDays { get; set; } // ARMLookbackDays
        public string PpeLoanProgramCode { get; set; } // PPELoanProgramCode (length: 100)
        public int LtvCalcMethod { get; set; } // LTVCalcMethod
        public int IsJumbo { get; set; } // IsJumbo
        public string FannieArmPlanNo { get; set; } // FannieARMPlanNo (length: 10)
        public int RepaymentType { get; set; } // RepaymentType
        public int AtrAssessmentMethod { get; set; } // ATRAssessmentMethod
        public bool Disabled { get; set; } // Disabled
        public int OriginationChannels { get; set; } // OriginationChannels
        public bool RestrictOriginationChannels { get; set; } // RestrictOriginationChannels
        public string MarksmanCode { get; set; } // MarksmanCode (length: 50)
        public string InvestorCode { get; set; } // InvestorCode (length: 50)
        public int? MiCoverageDefaultId { get; set; } // MICoverageDefaultID
        public int SimpleInterestType { get; set; } // SimpleInterestType
        public string DocPrepCode2 { get; set; } // DocPrepCode2 (length: 50)
        public bool IsNotMersLoan { get; set; } // IsNotMERSLoan
        public int InterestCalculationBasis { get; set; } // InterestCalculationBasis

        public LoanProgram()
        {
            DisplayOrder = 0;
            MortgageType = 0;
            FhaProgram = 0;
            BuyDowns = 0;
            IsBiWeekly = false;
            LoanProductType = 0;
            ArmIndexType = 0;
            ArmNoAdjCap = false;
            ArmRounding = 0;
            ArmConversionOption = false;
            ArmCarryover = false;
            ArmHasNegAmFeature = false;
            DemandFeature = false;
            VariableRateFeature = false;
            CopySecurityFields = false;
            CollateralSecurity = false;
            DepositAccounts = false;
            AssumptionOption = 0;
            CopyHazardFields = false;
            HazardInsRequired = false;
            TilHazardInsOption = 0;
            HazardInsAvailFromLender = 0;
            CopyLateChargeFields = false;
            LateChargeBasis = 0;
            LateChargeWording = 0;
            PrepaymentPenaltyOption = 0;
            RefundFinanceCharge = 0;
            MinCreditScore = 0;
            OccupancyTypeFlags = 0;
            LoanPurposeFlags = 0;
            SubjectPropertyFlags = 0;
            IsNegAmLoanUnderTila = false;
            LtvRoundingMethod = 0;
            LtvCalcMethod = 0;
            IsJumbo = 0;
            RepaymentType = 0;
            AtrAssessmentMethod = 0;
            Disabled = false;
            OriginationChannels = 0;
            RestrictOriginationChannels = false;
            SimpleInterestType = 0;
            IsNotMersLoan = false;
            InterestCalculationBasis = 0;
        }
    }

    // The table 'LoanPurpose' is not usable by entity framework because it
    // does not have a primary key. It is listed here for completeness.
    // LoanPurpose

    public class LoanPurpose
    {
        public int? LoanPurposeId { get; set; } // LoanPurposeId
        public string LoanPurposeDesc { get; set; } // LoanPurposeDesc (length: 160)
    }

    // The table 'LoanStatus' is not usable by entity framework because it
    // does not have a primary key. It is listed here for completeness.
    // LoanStatus

    public class LoanStatu
    {
        public int? LoanStatusId { get; set; } // LoanStatusId
        public string LoanStatusDesc { get; set; } // LoanStatusDesc (length: 160)
    }

    // The table 'LoanStatusDescription' is not usable by entity framework because it
    // does not have a primary key. It is listed here for completeness.
    // LoanStatusDescription

    public class LoanStatusDescription
    {
        public int? LoanStatusId { get; set; } // LoanStatusId
        public string LoanStatusDesc { get; set; } // LoanStatusDesc (length: 80)
    }

    // LoanStatusItem

    public class LoanStatusItem
    {
        public int LoanStatusItemId { get; set; } // LoanStatusItemID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public short LoanStatus { get; set; } // LoanStatus
        public string NameOv { get; set; } // NameOV (length: 50)
        public bool IsVisible { get; set; } // IsVisible
        public string ConsumerPortalNameOv { get; set; } // ConsumerPortalNameOV (length: 50)

        public LoanStatusItem()
        {
            DisplayOrder = 0;
            LoanStatus = 0;
            IsVisible = false;
        }
    }

    // LockHistory

    public class LockHistory
    {
        public int LoanLockRequestId { get; set; } // LoanLockRequestID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public short LockRequestType { get; set; } // LockRequestType
        public short LockRequestStatus { get; set; } // LockRequestStatus
        public int? LockDays { get; set; } // LockDays
        public int? LockExtension1Days { get; set; } // LockExtension1Days
        public int? LockExtension2Days { get; set; } // LockExtension2Days
        public decimal? IntRate { get; set; } // IntRate
        public decimal? BuyPriceNet { get; set; } // BuyPriceNet
        public string Notes { get; set; } // Notes (length: 2147483647)
        public string LoanProgramCode { get; set; } // LoanProgramCode (length: 50)
        public System.DateTime? LockRequestTime { get; set; } // LockRequestTime
        public System.DateTime? ProcessedTime { get; set; } // ProcessedTime
        public System.DateTime? LockExpirationDate { get; set; } // LockExpirationDate
        public decimal? LoanAmount { get; set; } // LoanAmount
        public int MortgageType { get; set; } // MortgageType
        public int? LockExtension3Days { get; set; } // LockExtension3Days
        public decimal? BaseLoan { get; set; } // BaseLoan
        public int? CreditScore { get; set; } // CreditScore
        public decimal? Ltv { get; set; } // LTV
        public decimal? Cltv { get; set; } // CLTV
        public decimal? Hcltv { get; set; } // HCLTV
        public decimal? SecondRatio { get; set; } // SecondRatio
        public int WaiveEscrow { get; set; } // WaiveEscrow
        public int OccupancyType { get; set; } // OccupancyType
        public int LoanPurpose { get; set; } // LoanPurpose
        public int RefiType { get; set; } // RefiType
        public decimal? BuyPriceBase { get; set; } // BuyPriceBase
        public decimal? BuySrp { get; set; } // BuySRP
        public decimal? BuyPriceAdjustments { get; set; } // BuyPriceAdjustments
        public int PropertyType { get; set; } // PropertyType
        public string SubPropStreet { get; set; } // SubPropStreet (length: 50)
        public string SubPropCity { get; set; } // SubPropCity (length: 50)
        public string SubPropState { get; set; } // SubPropState (length: 2)
        public string SubPropZip { get; set; } // SubPropZip (length: 9)
        public string SubPropCounty { get; set; } // SubPropCounty (length: 50)
        public int? SubPropNoUnits { get; set; } // SubPropNoUnits
        public int? SubPropStories { get; set; } // SubPropStories
        public string BorFirstName { get; set; } // BorFirstName (length: 50)
        public string BorLastName { get; set; } // BorLastName (length: 50)
        public decimal? AppraisedValue { get; set; } // AppraisedValue
        public decimal? SubFiBaseLoan { get; set; } // SubFiBaseLoan
        public int FirstTimeHomeBuyer { get; set; } // FirstTimeHomeBuyer
        public bool SelfEmployed { get; set; } // SelfEmployed
        public decimal? CashFromToBorrower { get; set; } // CashFromToBorrower
        public int? MonthsInReserve { get; set; } // MonthsInReserve
        public bool HasNonOccCoBorrower { get; set; } // HasNonOccCoBorrower
        public string UserName { get; set; } // UserName (length: 50)
        public System.DateTime? LockCanceledDate { get; set; } // LockCanceledDate
        public decimal? ArmMargin { get; set; } // ARMMargin
        public decimal? RefiCashOutAmount { get; set; } // RefiCashOutAmount
        public int? Term { get; set; } // Term
        public string PricedInvestor { get; set; } // PricedInvestor (length: 50)
        public string CoBorFirstName { get; set; } // CoBorFirstName (length: 50)
        public string CoBorLastName { get; set; } // CoBorLastName (length: 50)
        public int LoanProductType { get; set; } // LoanProductType
        public decimal? PurPrice { get; set; } // PurPrice
        public string LoanProgramName { get; set; } // LoanProgramName (length: 50)
        public System.DateTime? LockStartDate { get; set; } // LockStartDate
        public int RiskAssessmentMethod { get; set; } // RiskAssessmentMethod
        public string BuyCommitmentNo { get; set; } // BuyCommitmentNo (length: 50)
        public decimal? DiscountPoints { get; set; } // DiscountPoints

        public LockHistory()
        {
            FileDataId = 0;
            LockRequestType = 0;
            LockRequestStatus = 0;
            MortgageType = 0;
            WaiveEscrow = 0;
            OccupancyType = 0;
            LoanPurpose = 0;
            RefiType = 0;
            PropertyType = 0;
            FirstTimeHomeBuyer = 0;
            SelfEmployed = false;
            HasNonOccCoBorrower = false;
            LoanProductType = 0;
            RiskAssessmentMethod = 0;
        }
    }

    // The table 'LockRequestType' is not usable by entity framework because it
    // does not have a primary key. It is listed here for completeness.
    // LockRequestType

    public class LockRequestType
    {
        public int? LockRequestTypeId { get; set; } // LockRequestTypeId
        public string LockRequestTypeDesc { get; set; } // LockRequestTypeDesc (length: 160)
    }

    // MannerTitleHeld

    public class MannerTitleHeld
    {
        public int MannerTitleHeldId { get; set; } // MannerTitleHeldID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Text { get; set; } // Text (length: 100)

        public MannerTitleHeld()
        {
            DisplayOrder = 0;
        }
    }

    // ManRep

    public class ManRep
    {
        public int ManRepId { get; set; } // ManRepID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public short OutputType { get; set; } // OutputType
        public short FileSelectionType { get; set; } // FileSelectionType
        public string OutputFileName { get; set; } // OutputFileName (length: 255)
        public string WordDocFileName { get; set; } // WordDocFileName (length: 500)
        public string Title { get; set; } // Title (length: 255)
        public bool Legal { get; set; } // Legal
        public bool WordWrap { get; set; } // WordWrap
        public bool PromptToOverwrite { get; set; } // PromptToOverwrite
        public short Action { get; set; } // Action
        public string CommandLine { get; set; } // CommandLine (length: 255)
        public string FileName { get; set; } // FileName (length: 100)
        public string OriginalFileName { get; set; } // OriginalFileName (length: 100)
        public int SetNo { get; set; } // SetNo
        public bool UseOrigMmNames { get; set; } // UseOrigMMNames
        public byte[] EmbeddedReport { get; set; } // EmbeddedReport (length: 2147483647)
        public bool UseEmbeddedReport { get; set; } // UseEmbeddedReport
        public short EmbeddedReportType { get; set; } // EmbeddedReportType
        public int SpreadsheetStartingRow { get; set; } // SpreadsheetStartingRow
        public string SpreadsheetWorksheet { get; set; } // SpreadsheetWorksheet (length: 100)
        public string Category { get; set; } // Category (length: 100)
        public short PageLayout { get; set; } // PageLayout
        public short DisplayType { get; set; } // DisplayType
        public short LoanReportingOption { get; set; } // LoanReportingOption
        public bool FitToPage { get; set; } // FitToPage
        public short FontSize { get; set; } // FontSize
        public bool IncludesDetailTable { get; set; } // IncludesDetailTable
        public short DetailGridDataSource { get; set; } // DetailGridDataSource
        public bool ShowFilesWithNoDetailRows { get; set; } // ShowFilesWithNoDetailRows
        public bool PrintEachFileOnSeparateLine { get; set; } // PrintEachFileOnSeparateLine
        public int? ChartValueFieldId { get; set; } // ChartValueFieldID
        public int? ChartCategoryFieldId { get; set; } // ChartCategoryFieldID
        public int? ChartSeriesFieldId { get; set; } // ChartSeriesFieldID
        public short ChartValueFunction { get; set; } // ChartValueFunction
        public short ChartCategoryDateGrouping { get; set; } // ChartCategoryDateGrouping
        public short ChartSeriesDateGrouping { get; set; } // ChartSeriesDateGrouping
        public bool IncludeExcludedFiles { get; set; } // IncludeExcludedFiles
        public bool ChartHideMarkerLabels { get; set; } // ChartHideMarkerLabels
        public bool ChartShowGridLines { get; set; } // ChartShowGridLines
        public bool ChartHideLegend { get; set; } // ChartHideLegend
        public System.DateTime? RevisionDate { get; set; } // RevisionDate
        public System.Guid? ManRepGuid { get; set; } // ManRepGUID
        public short DetailGridStyle { get; set; } // DetailGridStyle
        public bool IsDataSource { get; set; } // IsDataSource
        public bool ExcludeHeader { get; set; } // ExcludeHeader
        public string FontDescription { get; set; } // FontDescription (length: 100)
        public int MaxItems { get; set; } // MaxItems
        public int SqlSubFolderId { get; set; } // SQLSubFolderID
        public bool IncludeOnlyDeletedFiles { get; set; } // IncludeOnlyDeletedFiles
        public int PipelineViewPosition { get; set; } // PipelineViewPosition
        public int PipelineType { get; set; } // PipelineType
        public int DatasetType { get; set; } // DatasetType
        public string CustomJoins { get; set; } // CustomJoins (length: 2147483647)
        public bool UseReportingDb { get; set; } // UseReportingDB
        public System.DateTime? LoanWithHistoryDate { get; set; } // LoanWithHistoryDate

        public ManRep()
        {
            DisplayOrder = 0;
            OutputType = 0;
            FileSelectionType = 0;
            Legal = false;
            WordWrap = false;
            PromptToOverwrite = false;
            Action = 0;
            SetNo = 0;
            UseOrigMmNames = false;
            UseEmbeddedReport = false;
            EmbeddedReportType = 0;
            SpreadsheetStartingRow = 0;
            PageLayout = 0;
            DisplayType = 0;
            LoanReportingOption = 0;
            FitToPage = false;
            FontSize = 0;
            IncludesDetailTable = false;
            DetailGridDataSource = 0;
            ShowFilesWithNoDetailRows = false;
            PrintEachFileOnSeparateLine = false;
            ChartValueFunction = 0;
            ChartCategoryDateGrouping = 0;
            ChartSeriesDateGrouping = 0;
            IncludeExcludedFiles = false;
            ChartHideMarkerLabels = false;
            ChartShowGridLines = false;
            ChartHideLegend = false;
            ManRepGuid = System.Guid.NewGuid();
            DetailGridStyle = 0;
            IsDataSource = false;
            ExcludeHeader = false;
            MaxItems = 0;
            SqlSubFolderId = 0;
            IncludeOnlyDeletedFiles = false;
            PipelineViewPosition = 0;
            PipelineType = 0;
            DatasetType = 0;
            UseReportingDb = false;
        }
    }

    // ManRepField

    public class ManRepField
    {
        public int ManRepFieldId { get; set; } // ManRepFieldID (Primary key)
        public int ManRepId { get; set; } // ManRepID
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool UseEnumValue { get; set; } // UseEnumValue
        public string ObjectName { get; set; } // ObjectName (length: 100)
        public string PropertyName { get; set; } // PropertyName (length: 50)
        public string FormatOv { get; set; } // FormatOV (length: 50)
        public string CaptionOv { get; set; } // CaptionOV (length: 50)
        public bool ExcludeField { get; set; } // ExcludeField
        public short Sorting { get; set; } // Sorting
        public short Grouping { get; set; } // Grouping
        public short FilterType { get; set; } // FilterType
        public decimal? FilterCurrency1 { get; set; } // FilterCurrency1
        public decimal? FilterCurrency2 { get; set; } // FilterCurrency2
        public System.DateTime? FilterDate1 { get; set; } // FilterDate1
        public System.DateTime? FilterDate2 { get; set; } // FilterDate2
        public int? FilterInteger1 { get; set; } // FilterInteger1
        public int? FilterInteger2 { get; set; } // FilterInteger2
        public string FilterString { get; set; } // FilterString (length: 1000)
        public bool PromptAtRuntime { get; set; } // PromptAtRuntime
        public int? SpreadsheetColumnIndexOv { get; set; } // SpreadsheetColumnIndexOV
        public int SummaryType { get; set; } // SummaryType
        public bool OmitDuplicates { get; set; } // OmitDuplicates
        public decimal? Width { get; set; } // Width
        public string FilterCustom { get; set; } // FilterCustom (length: 2147483647)
        public int? CollectionIndex { get; set; } // CollectionIndex
        public string CustomFormula { get; set; } // CustomFormula (length: 2147483647)
        public int ManRepCustomFormulaType { get; set; } // ManRepCustomFormulaType
        public string JScriptFormula { get; set; } // JScriptFormula (length: 2147483647)

        public ManRepField()
        {
            ManRepId = 0;
            DisplayOrder = 0;
            UseEnumValue = false;
            ExcludeField = false;
            Sorting = 0;
            Grouping = 0;
            FilterType = 0;
            PromptAtRuntime = false;
            SummaryType = 0;
            OmitDuplicates = false;
            ManRepCustomFormulaType = 0;
        }
    }

    // ManRepImportFieldMap

    public class ManRepImportFieldMap
    {
        public int ManRepImportFieldMapId { get; set; } // ManRepImportFieldMapID (Primary key)
        public int ClassicFieldNo { get; set; } // ClassicFieldNo
        public string BpPropertyName { get; set; } // BP_PropertyName (length: 50)
        public string BpObjectName { get; set; } // BP_ObjectName (length: 50)

        public ManRepImportFieldMap()
        {
            ClassicFieldNo = 0;
        }
    }

    // MarketSurveyRate

    public class MarketSurveyRate
    {
        public int MarketSurveyRateId { get; set; } // MarketSurveyRateID (Primary key)
        public System.DateTime? DateRangeStart { get; set; } // DateRangeStart
        public System.DateTime? DateRangeEnd { get; set; } // DateRangeEnd
        public decimal AverageRate { get; set; } // AverageRate

        public MarketSurveyRate()
        {
            AverageRate = 0m;
        }
    }

    // Markup

    public class Markup
    {
        public int MarkupId { get; set; } // MarkupID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public string Description { get; set; } // Description (length: 100)
        public decimal? MarkupPercent { get; set; } // MarkupPercent
        public decimal? MarkupAmount { get; set; } // MarkupAmount

        public Markup()
        {
            FileDataId = 0;
        }
    }

    // MaxLoanAmounts

    public class MaxLoanAmount
    {
        public int MaxLoanAmountsId { get; set; } // MaxLoanAmountsID (Primary key)
        public decimal? VaMaxOneFamily { get; set; } // VAMaxOneFamily
        public decimal? VaMaxTwoFamily { get; set; } // VAMaxTwoFamily
        public decimal? VaMaxThreeFamily { get; set; } // VAMaxThreeFamily
        public decimal? VaMaxFourFamily { get; set; } // VAMaxFourFamily
        public decimal? ConvMaxOneFamily { get; set; } // ConvMaxOneFamily
        public decimal? ConvMaxTwoFamily { get; set; } // ConvMaxTwoFamily
        public decimal? ConvMaxThreeFamily { get; set; } // ConvMaxThreeFamily
        public decimal? ConvMaxFourFamily { get; set; } // ConvMaxFourFamily
    }

    // MICoverageDefault

    public class MiCoverageDefault
    {
        public int MiCoverageDefaultId { get; set; } // MICoverageDefaultID (Primary key)
        public string Code { get; set; } // Code (length: 20)
        public bool IsDefault { get; set; } // IsDefault
        public int? CoverageLtv80To85 { get; set; } // CoverageLTV80To85
        public int? CoverageLtv85To90 { get; set; } // CoverageLTV85To90
        public int? CoverageLtv90To95 { get; set; } // CoverageLTV90To95
        public int? CoverageLtv95To97 { get; set; } // CoverageLTV95To97
        public int? CoverageLtv97To100 { get; set; } // CoverageLTV97To100

        public MiCoverageDefault()
        {
            IsDefault = false;
        }
    }

    // MIMethodEstimate

    public class MiMethodEstimate
    {
        public int MiMethodEstimateId { get; set; } // MIMethodEstimateID (Primary key)
        public short MiMethod { get; set; } // MIMethod
        public decimal? AdjSecondHome { get; set; } // AdjSecondHome
        public decimal? AdjInvestmentProperty { get; set; } // AdjInvestmentProperty
        public decimal? AdjRateTermRefi { get; set; } // AdjRateTermRefi
        public decimal? AdjCashOutRefi { get; set; } // AdjCashOutRefi
        public decimal? MinimumRate { get; set; } // MinimumRate

        public MiMethodEstimate()
        {
            MiMethod = 0;
        }
    }

    // MIRateEstimate

    public class MiRateEstimate
    {
        public int MiRateEstimateId { get; set; } // MIRateEstimateID (Primary key)
        public short MiRateType { get; set; } // MIRateType
        public short MiLoanType { get; set; } // MILoanType
        public decimal? Rate8085 { get; set; } // Rate80_85
        public decimal? Rate8590 { get; set; } // Rate85_90
        public decimal? Rate9095 { get; set; } // Rate90_95
        public decimal? Rate9597 { get; set; } // Rate95_97
        public decimal? Rate97100 { get; set; } // Rate97_100
        public decimal? Rate100Plus { get; set; } // Rate100_Plus
        public decimal? RateYears11ToTerm { get; set; } // RateYears11ToTerm

        public MiRateEstimate()
        {
            MiRateType = 0;
            MiLoanType = 0;
        }
    }

    // MiscForms

    public class MiscForm
    {
        public int MiscFormsId { get; set; } // MiscFormsID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public short MbcBrokerRepresents { get; set; } // MBCBrokerRepresents
        public short MbcShopOption { get; set; } // MBCShopOption
        public int? MbcNumberOfLenders { get; set; } // MBCNumberOfLenders
        public string MbcCompAmount { get; set; } // MBCCompAmount (length: 50)
        public string MbcCompNoGreaterThan { get; set; } // MBCCompNoGreaterThan (length: 50)
        public string MbcPoints { get; set; } // MBCPoints (length: 50)
        public string MbcBorFee { get; set; } // MBCBorFee (length: 50)
        public string MbcLenderFee { get; set; } // MBCLenderFee (length: 50)
        public decimal? FlbaNetProceedsOv { get; set; } // FLBANetProceedsOV
        public decimal? FlbaBrokerageFeeOv { get; set; } // FLBABrokerageFeeOV
        public decimal? FlbaAppDepositOv { get; set; } // FLBAAppDepositOV
        public decimal? FlbaThirdPartyFee { get; set; } // FLBAThirdPartyFee
        public bool FlbaIsAppDepositRefundable { get; set; } // FLBAIsAppDepositRefundable
        public string MaNoteExpDate { get; set; } // MANoteExpDate (length: 50)
        public decimal? MaAppFee { get; set; } // MAAppFee
        public string FlecBrokerName { get; set; } // FLECBrokerName (length: 50)
        public string NyAdvisedDesc1Ov { get; set; } // NYAdvisedDesc1OV (length: 200)
        public string NyAdvisedDesc2Ov { get; set; } // NYAdvisedDesc2OV (length: 200)
        public string NyProcessingAssistantTextOv { get; set; } // NYProcessingAssistantTextOV (length: 200)
        public bool NyLenderFees { get; set; } // NYLenderFees
        public string NyLenderFeesPercent { get; set; } // NYLenderFeesPercent (length: 10)
        public string NyLenderFeesDollar { get; set; } // NYLenderFeesDollar (length: 10)
        public string NyLenderFeesPoints { get; set; } // NYLenderFeesPoints (length: 10)
        public bool NyFeeUnkown { get; set; } // NYFeeUnkown
        public string NyFeeUnknownPoints { get; set; } // NYFeeUnknownPoints (length: 10)
        public bool NyFeesFromLoan { get; set; } // NYFeesFromLoan
        public string NyFeesFromLoanPercent { get; set; } // NYFeesFromLoanPercent (length: 10)
        public string NyFeesFromLoanDollar { get; set; } // NYFeesFromLoanDollar (length: 10)
        public bool NyFeesDirect { get; set; } // NYFeesDirect
        public string NyFeesDirectCommitment { get; set; } // NYFeesDirectCommitment (length: 10)
        public string NyFeesDirectClosing { get; set; } // NYFeesDirectClosing (length: 10)
        public string NyFeesDirectPercent { get; set; } // NYFeesDirectPercent (length: 10)
        public string NyFeesDirectDollar { get; set; } // NYFeesDirectDollar (length: 10)
        public decimal? NyApplicationFeeOv { get; set; } // NYApplicationFeeOV
        public decimal? NyAppraisalFeeOv { get; set; } // NYAppraisalFeeOV
        public decimal? NyCreditFeeOv { get; set; } // NYCreditFeeOV
        public string NyApplicationFeeRefundableOv { get; set; } // NYApplicationFeeRefundableOV (length: 300)
        public string NyRefundDesc1Ov { get; set; } // NYRefundDesc1OV (length: 200)
        public string NyRefundDesc2Ov { get; set; } // NYRefundDesc2OV (length: 200)
        public decimal? NyProcFeeOv { get; set; } // NYProcFeeOV
        public string NyFeeDivisionName1 { get; set; } // NYFeeDivisionName1 (length: 100)
        public string NyFeeDivisionName2 { get; set; } // NYFeeDivisionName2 (length: 100)
        public string NyFeeDivisionFee1 { get; set; } // NYFeeDivisionFee1 (length: 10)
        public string NyFeeDivisionFee2 { get; set; } // NYFeeDivisionFee2 (length: 10)
        public string NyFeeDivisionFee3 { get; set; } // NYFeeDivisionFee3 (length: 10)
        public string NyFeeDivisionFee4 { get; set; } // NYFeeDivisionFee4 (length: 10)
        public string NyContactName { get; set; } // NYContactName (length: 50)
        public string NyContactPhone { get; set; } // NYContactPhone (length: 50)
        public string NyContactPhoneCollect { get; set; } // NYContactPhoneCollect (length: 50)
        public string NyContactPhoneTollFree { get; set; } // NYContactPhoneTollFree (length: 50)
        public string NyDispositionStatusOv { get; set; } // NYDispositionStatusOV (length: 50)
        public System.DateTime? NyDispositionDateOv { get; set; } // NYDispositionDateOV
        public decimal? NyBrokerFeeAmountLender { get; set; } // NYBrokerFeeAmountLender
        public decimal? NyBrokerFeeAmountBorrower { get; set; } // NYBrokerFeeAmountBorrower
        public decimal? NyBrokerFeeAmountOther1 { get; set; } // NYBrokerFeeAmountOther1
        public decimal? NyBrokerFeeAmountOther2 { get; set; } // NYBrokerFeeAmountOther2
        public string NyBrokerFeeDescOther1 { get; set; } // NYBrokerFeeDescOther1 (length: 50)
        public string NyBrokerFeeDescOther2 { get; set; } // NYBrokerFeeDescOther2 (length: 50)
        public string Ssa89AgentOv { get; set; } // SSA89AgentOV (length: 500)
        public string FlInfoDisclosureBranchPhoneOv { get; set; } // FLInfoDisclosureBranchPhoneOV (length: 16)
        public int Ssa89CompanyOption { get; set; } // SSA89CompanyOption

        public MiscForm()
        {
            FileDataId = 0;
            MbcBrokerRepresents = 0;
            MbcShopOption = 0;
            FlbaIsAppDepositRefundable = false;
            NyLenderFees = false;
            NyFeeUnkown = false;
            NyFeesFromLoan = false;
            NyFeesDirect = false;
            Ssa89CompanyOption = 0;
        }
    }

    // The table 'MortgageType' is not usable by entity framework because it
    // does not have a primary key. It is listed here for completeness.
    // MortgageType

    public class MortgageType
    {
        public int? MortgageTypeId { get; set; } // MortgageTypeId
        public string MortgageTypeDesc { get; set; } // MortgageTypeDesc (length: 160)
    }

    // MSACounty

    public class MsaCounty
    {
        public int MsaCountyId { get; set; } // MSACountyID (Primary key)
        public int MsaCountySetId { get; set; } // MSACountySetID
        public string Name { get; set; } // Name (length: 50)
        public decimal? MedianIncome { get; set; } // MedianIncome
        public decimal? AdjustmentFactor { get; set; } // AdjustmentFactor

        public MsaCounty()
        {
            MsaCountySetId = 0;
        }
    }

    // MSACountySet

    public class MsaCountySet
    {
        public int MsaCountySetId { get; set; } // MSACountySetID (Primary key)
        public string StateAbbr { get; set; } // StateAbbr (length: 2)
    }

    // NeededItem

    public class NeededItem
    {
        public int NeededItemId { get; set; } // NeededItemID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int DisplayOrder { get; set; } // DisplayOrder
        public short TaskType { get; set; } // TaskType
        public short NeededItemType { get; set; } // NeededItemType
        public string Description { get; set; } // Description (length: 1500)
        public bool NeededFromBorrower { get; set; } // NeededFromBorrower
        public System.DateTime? DateOrdered { get; set; } // DateOrdered
        public System.DateTime? DateVerified { get; set; } // DateVerified
        public int? DateDueDays { get; set; } // DateDueDays
        public System.DateTime? DateDue { get; set; } // DateDue
        public string OrderedBy { get; set; } // OrderedBy (length: 50)
        public string VerifiedBy { get; set; } // VerifiedBy (length: 50)
        public int? DateExpirationDays { get; set; } // DateExpirationDays
        public System.DateTime? DateExpiration { get; set; } // DateExpiration
        public short FollowUpFlag { get; set; } // FollowUpFlag
        public int? ResidenceId { get; set; } // ResidenceID
        public int? EmployerId { get; set; } // EmployerID
        public int? AssetId { get; set; } // AssetID
        public int? DebtId { get; set; } // DebtID
        public short DeliveryMethod { get; set; } // DeliveryMethod
        public string NeededItemMatrixName { get; set; } // NeededItemMatrixName (length: 50)
        public System.DateTime? DateSubmitted { get; set; } // DateSubmitted
        public string SubmittedBy { get; set; } // SubmittedBy (length: 50)
        public System.DateTime? DateCleared { get; set; } // DateCleared
        public string ClearedBy { get; set; } // ClearedBy (length: 50)
        public long AssignedTo { get; set; } // AssignedTo
        public System.DateTime? FollowUpDate { get; set; } // FollowUpDate
        public int? OrderedTaskId { get; set; } // OrderedTaskID
        public int? ReceivedTaskId { get; set; } // ReceivedTaskID
        public int? SubmittedTaskId { get; set; } // SubmittedTaskID
        public int? ClearedTaskId { get; set; } // ClearedTaskID

        public NeededItem()
        {
            FileDataId = 0;
            DisplayOrder = 0;
            TaskType = 0;
            NeededItemType = 0;
            NeededFromBorrower = false;
            FollowUpFlag = 0;
            DeliveryMethod = 0;
            AssignedTo = 0;
        }
    }

    // NeededItemDefault

    public class NeededItemDefault
    {
        public int NeededItemDefaultId { get; set; } // NeededItemDefaultID (Primary key)
        public int NeededItemSetId { get; set; } // NeededItemSetID
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Description { get; set; } // Description (length: 250)
        public bool NeededFromBorrower { get; set; } // NeededFromBorrower
        public short NeededItemType { get; set; } // NeededItemType
        public int? DateDueDays { get; set; } // DateDueDays
        public int? DateExpirationDays { get; set; } // DateExpirationDays
        public int? RedAlertDays { get; set; } // RedAlertDays
        public int? YellowAlertDays { get; set; } // YellowAlertDays
        public string NeededItemMatrixName { get; set; } // NeededItemMatrixName (length: 50)
        public long AssignedTo { get; set; } // AssignedTo
        public short FollowUpFlag { get; set; } // FollowUpFlag

        public NeededItemDefault()
        {
            NeededItemSetId = 0;
            DisplayOrder = 0;
            NeededFromBorrower = false;
            NeededItemType = 0;
            AssignedTo = 0;
            FollowUpFlag = 0;
        }
    }

    // NeededItemMatrix

    public class NeededItemMatrix
    {
        public int NeededItemMatrixId { get; set; } // NeededItemMatrixID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Name { get; set; } // Name (length: 50)

        public NeededItemMatrix()
        {
            DisplayOrder = 0;
        }
    }

    // NeededItemMatrixItem

    public class NeededItemMatrixItem
    {
        public int NeededItemMatrixItemId { get; set; } // NeededItemMatrixItemID (Primary key)
        public int NeededItemMatrixId { get; set; } // NeededItemMatrixID
        public int SecurityProfileId { get; set; } // SecurityProfileID
        public bool CanEditDescription { get; set; } // CanEditDescription
        public bool CanEditBorrower { get; set; } // CanEditBorrower
        public bool CanEditTaskType { get; set; } // CanEditTaskType
        public bool CanEditDateOrdered { get; set; } // CanEditDateOrdered
        public bool CanEditDateDue { get; set; } // CanEditDateDue
        public bool CanEditDateVerified { get; set; } // CanEditDateVerified
        public bool CanEditDateSubmitted { get; set; } // CanEditDateSubmitted
        public bool CanEditDateCleared { get; set; } // CanEditDateCleared
        public bool CanEditDateExpires { get; set; } // CanEditDateExpires
        public bool CanEditFollowupFlag { get; set; } // CanEditFollowupFlag
        public bool CanEditAssignedTo { get; set; } // CanEditAssignedTo
        public bool CanDelete { get; set; } // CanDelete
        public bool CanEditDocuments { get; set; } // CanEditDocuments

        public NeededItemMatrixItem()
        {
            NeededItemMatrixId = 0;
            SecurityProfileId = 0;
            CanEditDescription = false;
            CanEditBorrower = false;
            CanEditTaskType = false;
            CanEditDateOrdered = false;
            CanEditDateDue = false;
            CanEditDateVerified = false;
            CanEditDateSubmitted = false;
            CanEditDateCleared = false;
            CanEditDateExpires = false;
            CanEditFollowupFlag = false;
            CanEditAssignedTo = false;
            CanDelete = false;
            CanEditDocuments = false;
        }
    }

    // NeededItemPermission

    public class NeededItemPermission
    {
        public int NeededItemPermissionId { get; set; } // NeededItemPermissionID (Primary key)
        public int DefaultNeededItemMatrixId { get; set; } // DefaultNeededItemMatrixID
        public short NeededItemType { get; set; } // NeededItemType

        public NeededItemPermission()
        {
            DefaultNeededItemMatrixId = 0;
            NeededItemType = 0;
        }
    }

    // NeededItemPermissionItem

    public class NeededItemPermissionItem
    {
        public int NeededItemPermissionItemId { get; set; } // NeededItemPermissionItemID (Primary key)
        public int NeededItemPermissionId { get; set; } // NeededItemPermissionID
        public int SecurityProfileId { get; set; } // SecurityProfileID
        public bool CanCreateFromScratch { get; set; } // CanCreateFromScratch
        public bool CanCreateFromList { get; set; } // CanCreateFromList
        public bool CanModifyPermissions { get; set; } // CanModifyPermissions

        public NeededItemPermissionItem()
        {
            NeededItemPermissionId = 0;
            SecurityProfileId = 0;
            CanCreateFromScratch = false;
            CanCreateFromList = false;
            CanModifyPermissions = false;
        }
    }

    // NeededItemSet

    public class NeededItemSet
    {
        public int NeededItemSetId { get; set; } // NeededItemSetID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Name { get; set; } // Name (length: 50)

        public NeededItemSet()
        {
            DisplayOrder = 0;
        }
    }

    // NeededItemStage

    public class NeededItemStage
    {
        public int NeededItemStageId { get; set; } // NeededItemStageID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public short NeededItemType { get; set; } // NeededItemType
        public string NameOv { get; set; } // NameOV (length: 50)
        public bool IsVisible { get; set; } // IsVisible

        public NeededItemStage()
        {
            DisplayOrder = 0;
            NeededItemType = 0;
            IsVisible = false;
        }
    }

    // NYAppLogFee

    public class NyAppLogFee
    {
        public int NyAppLogFeeId { get; set; } // NYAppLogFeeID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public short FeeType { get; set; } // FeeType
        public string Purpose { get; set; } // Purpose (length: 50)
        public decimal? FeeAmount { get; set; } // FeeAmount
        public System.DateTime? FeeDate { get; set; } // FeeDate
        public string Description { get; set; } // Description (length: 400)
        public int DisplayOrder { get; set; } // DisplayOrder

        public NyAppLogFee()
        {
            FileDataId = 0;
            FeeType = 0;
            DisplayOrder = 0;
        }
    }

    // ObjectPermission

    public class ObjectPermission
    {
        public int ObjectPermissionId { get; set; } // ObjectPermissionID (Primary key)
        public short ObjectCategory { get; set; } // ObjectCategory
        public string ObjectName { get; set; } // ObjectName (length: 100)
        public int? SecurityProfileId { get; set; } // SecurityProfileID
        public int ObjectPermissionType { get; set; } // ObjectPermissionType
        public long StatusBits { get; set; } // StatusBits
        public bool DisabledIfLocked { get; set; } // DisabledIfLocked
        public bool DisabledIfLockRequested { get; set; } // DisabledIfLockRequested
        public bool DisabledIfGfeDelivered { get; set; } // DisabledIfGFEDelivered

        public ObjectPermission()
        {
            ObjectCategory = 0;
            ObjectPermissionType = 0;
            StatusBits = 0;
            DisabledIfLocked = false;
            DisabledIfLockRequested = false;
            DisabledIfGfeDelivered = false;
        }
    }

    // Organization

    public class Organization
    {
        public int OrganizationId { get; set; } // OrganizationID (Primary key)
        public int? ParentOrganizationId { get; set; } // ParentOrganizationID
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Code { get; set; } // Code (length: 30)
        public string Name { get; set; } // Name (length: 100)
        public string CostCenter { get; set; } // CostCenter (length: 10)
        public bool PreventLoanAssignment { get; set; } // PreventLoanAssignment
        public string Street1 { get; set; } // Street1 (length: 100)
        public string Street2 { get; set; } // Street2 (length: 100)
        public string City { get; set; } // City (length: 50)
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 9)
        public string Phone { get; set; } // Phone (length: 10)
        public string Fax { get; set; } // Fax (length: 10)
        public short FileNamingOption { get; set; } // FileNamingOption
        public long AutoFileNameNextNum { get; set; } // AutoFileNameNextNum
        public int AutoFileNameDigits { get; set; } // AutoFileNameDigits
        public short AutoFileNameOrgPlacement { get; set; } // AutoFileNameOrgPlacement
        public string AutoFileNamePrefix { get; set; } // AutoFileNamePrefix (length: 5)
        public string AutoFileNameSuffix { get; set; } // AutoFileNameSuffix (length: 5)
        public short MinNumberOption { get; set; } // MINNumberOption
        public string MinNumberOrgId { get; set; } // MINNumberOrgID (length: 7)
        public string MinNumberPrefix { get; set; } // MINNumberPrefix (length: 5)
        public short MinNumberMethod { get; set; } // MINNumberMethod
        public long MinNumberNextNumber { get; set; } // MINNumberNextNumber
        public string GseServicerNo { get; set; } // GSEServicerNo (length: 30)
        public string HampServicerNo { get; set; } // HAMPServicerNo (length: 30)
        public int? HampDiscountRatePremium { get; set; } // HAMPDiscountRatePremium
        public string NmlsCompanyId { get; set; } // NMLSCompanyID (length: 50)
        public string NmlsBranchId { get; set; } // NMLSBranchID (length: 50)
        public string CompanyEin { get; set; } // CompanyEIN (length: 9)
        public string HampRegistrationNumber { get; set; } // HAMPRegistrationNumber (length: 15)
        public string HampServicerLoginName { get; set; } // HAMPServicerLoginName (length: 50)
        public string FhaLenderId { get; set; } // FHALenderID (length: 50)
        public string VaLenderId { get; set; } // VALenderID (length: 50)
        public string PpeUserName { get; set; } // PPEUserName (length: 50)
        public bool Disabled { get; set; } // Disabled
        public short OrganizationType { get; set; } // OrganizationType
        public string FreddieTpoNumber { get; set; } // FreddieTPONumber (length: 20)
        public string FreddieTpoPasswordEncrypted { get; set; } // FreddieTPOPasswordEncrypted (length: 50)
        public bool UnauthorizedForConv { get; set; } // UnauthorizedForConv
        public bool UnauthorizedForFha { get; set; } // UnauthorizedForFHA
        public bool UnauthorizedForVa { get; set; } // UnauthorizedForVA
        public bool UnauthorizedForRhs { get; set; } // UnauthorizedForRHS
        public bool UnauthorizedForOtherMortType { get; set; } // UnauthorizedForOtherMortType
        public decimal? DelegationLimitConv { get; set; } // DelegationLimitConv
        public decimal? DelegationLimitFha { get; set; } // DelegationLimitFHA
        public decimal? DelegationLimitVa { get; set; } // DelegationLimitVA
        public decimal? DelegationLimitRhs { get; set; } // DelegationLimitRHS
        public decimal? DelegationLimitOtherMortType { get; set; } // DelegationLimitOtherMortType
        public string CustomData { get; set; } // CustomData (length: 500)
        public string Notes { get; set; } // Notes (length: 2147483647)
        public string AccountExecUserName { get; set; } // AccountExecUserName (length: 50)
        public string FannieSellerNo { get; set; } // FannieSellerNo (length: 10)
        public int LogoOption { get; set; } // LogoOption
        public byte[] LogoImageData { get; set; } // LogoImageData (length: 2147483647)
        public decimal? LogoAreaMaxWidthCustom { get; set; } // LogoAreaMaxWidthCustom
        public int Status { get; set; } // Status
        public System.DateTime? StatusDate { get; set; } // StatusDate
        public int BusinessType { get; set; } // BusinessType
        public string LegalName { get; set; } // LegalName (length: 150)
        public string OwnerOfRecord { get; set; } // OwnerOfRecord (length: 100)
        public System.DateTime? VaApprovedDate { get; set; } // VAApprovedDate
        public System.DateTime? FhaApprovedDate { get; set; } // FHAApprovedDate
        public System.DateTime? RhsApprovedDate { get; set; } // RHSApprovedDate
        public System.DateTime? EAndOPolicyExpirationDate { get; set; } // EAndOPolicyExpirationDate
        public System.DateTime? FinancialsExpirationDate { get; set; } // FinancialsExpirationDate
        public string Lei { get; set; } // LEI (length: 20)
        public int UliAssignmentOption { get; set; } // ULIAssignmentOption

        public Organization()
        {
            DisplayOrder = 0;
            PreventLoanAssignment = false;
            FileNamingOption = 0;
            AutoFileNameNextNum = 0;
            AutoFileNameDigits = 0;
            AutoFileNameOrgPlacement = 0;
            MinNumberOption = 0;
            MinNumberMethod = 0;
            MinNumberNextNumber = 0;
            Disabled = false;
            OrganizationType = 0;
            UnauthorizedForConv = false;
            UnauthorizedForFha = false;
            UnauthorizedForVa = false;
            UnauthorizedForRhs = false;
            UnauthorizedForOtherMortType = false;
            LogoOption = 0;
            Status = 0;
            BusinessType = 0;
            UliAssignmentOption = 0;
        }
    }

    // OrganizationStateLicense

    public class OrganizationStateLicense
    {
        public int OrganizationStateLicenseId { get; set; } // OrganizationStateLicenseID (Primary key)
        public int OrganizationId { get; set; } // OrganizationID
        public string State { get; set; } // State (length: 2)
        public short OrganizationStateLicenseType { get; set; } // OrganizationStateLicenseType
        public string LicenseNo { get; set; } // LicenseNo (length: 50)
        public System.DateTime? ExpirationDate { get; set; } // ExpirationDate
        public System.DateTime? StartDate { get; set; } // StartDate

        public OrganizationStateLicense()
        {
            OrganizationId = 0;
            OrganizationStateLicenseType = 0;
        }
    }

    // OrgCompPlan

    public class OrgCompPlan
    {
        public int OrgCompPlanId { get; set; } // OrgCompPlanID (Primary key)
        public int OrganizationId { get; set; } // OrganizationID
        public int CompPlanId { get; set; } // CompPlanID
        public System.DateTime? StartDate { get; set; } // StartDate
        public System.DateTime? EndDate { get; set; } // EndDate

        public OrgCompPlan()
        {
            OrganizationId = 0;
            CompPlanId = 0;
        }
    }

    // OSOResult

    public class OsoResult
    {
        public int OsoResultId { get; set; } // OSOResultID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public string Description { get; set; } // Description (length: 4000)
        public System.DateTime ResultDate { get; set; } // ResultDate
        public string VendorCode { get; set; } // VendorCode (length: 20)

        public OsoResult()
        {
            FileDataId = 0;
        }
    }

    // PartnerCustomField

    public class PartnerCustomField
    {
        public int PartnerCustomFieldId { get; set; } // PartnerCustomFieldID (Primary key)
        public int PartnerSystemType { get; set; } // PartnerSystemType
        public string PartnerFieldName { get; set; } // PartnerFieldName (length: 100)
        public string FieldName { get; set; } // FieldName (length: 200)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string DataFormat { get; set; } // DataFormat (length: 50)

        public PartnerCustomField()
        {
            PartnerSystemType = 0;
            DisplayOrder = 0;
        }
    }

    // Party

    public class Party
    {
        public int PartyId { get; set; } // PartyID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public short CategoryId { get; set; } // CategoryID
        public string FirstName { get; set; } // FirstName (length: 50)
        public string MiddleName { get; set; } // MiddleName (length: 50)
        public string LastName { get; set; } // LastName (length: 50)
        public string Title { get; set; } // Title (length: 50)
        public string Company { get; set; } // Company (length: 100)
        public string Street { get; set; } // Street (length: 50)
        public string City { get; set; } // City (length: 50)
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 9)
        public string EMail { get; set; } // EMail (length: 250)
        public string WorkPhone { get; set; } // WorkPhone (length: 20)
        public string HomePhone { get; set; } // HomePhone (length: 20)
        public string MobilePhone { get; set; } // MobilePhone (length: 20)
        public string OtherPhone { get; set; } // OtherPhone (length: 20)
        public string Pager { get; set; } // Pager (length: 20)
        public string Fax { get; set; } // Fax (length: 20)
        public string LicenseNo { get; set; } // LicenseNo (length: 50)
        public string ChumsNo { get; set; } // CHUMSNo (length: 50)
        public string FhaOrigOrSponsorId { get; set; } // FHAOrigOrSponsorID (length: 50)
        public string BranchId { get; set; } // BranchID (length: 20)
        public string Notes { get; set; } // Notes (length: 2000)
        public string ContactNmlsid { get; set; } // ContactNMLSID (length: 50)
        public string CompanyNmlsid { get; set; } // CompanyNMLSID (length: 50)
        public bool LockToUser { get; set; } // LockToUser
        public string CompanyEin { get; set; } // CompanyEIN (length: 9)
        public string MobilePhoneSmsGateway { get; set; } // MobilePhoneSMSGateway (length: 40)
        public string CompanyLicenseNo { get; set; } // CompanyLicenseNo (length: 50)
        public string WirePrimaryBankName { get; set; } // WirePrimaryBankName (length: 50)
        public string WirePrimaryBankCity { get; set; } // WirePrimaryBankCity (length: 50)
        public string WirePrimaryBankState { get; set; } // WirePrimaryBankState (length: 2)
        public string WirePrimaryAbaNo { get; set; } // WirePrimaryABANo (length: 9)
        public string WirePrimaryAccountNo { get; set; } // WirePrimaryAccountNo (length: 22)
        public string WireFctBankName { get; set; } // WireFCTBankName (length: 50)
        public string WireFctBankCity { get; set; } // WireFCTBankCity (length: 50)
        public string WireFctBankState { get; set; } // WireFCTBankState (length: 2)
        public string WireFctabaNo { get; set; } // WireFCTABANo (length: 9)
        public string WireFctAccountNo { get; set; } // WireFCTAccountNo (length: 22)
        public string WirePrimaryBankStreet { get; set; } // WirePrimaryBankStreet (length: 50)
        public string WireFctBankStreet { get; set; } // WireFCTBankStreet (length: 50)
        public string WirePrimaryBankZip { get; set; } // WirePrimaryBankZip (length: 9)
        public string WireFctBankZip { get; set; } // WireFCTBankZip (length: 9)
        public bool SyncData { get; set; } // SyncData
        public System.DateTime? EAndOPolicyExpirationDate { get; set; } // EAndOPolicyExpirationDate
        public string LicensingAgencyCode { get; set; } // LicensingAgencyCode (length: 20)
        public string EMail2 { get; set; } // EMail2 (length: 250)
        public string EMail3 { get; set; } // EMail3 (length: 250)

        public Party()
        {
            FileDataId = 0;
            CategoryId = 0;
            LockToUser = false;
            SyncData = false;
        }


        public static Party Create(LoanApplication loanApplication)
        {

            return Create(loanApplication.Opportunity.Owner);

        }
        public void Update(Employee owner)
        {

            this.FirstName = owner.Contact.Preferred;
            this.ContactNmlsid = owner.NmlsNo;
            this.WorkPhone = owner.EmployeePhoneBinders.FirstOrDefault().CompanyPhoneInfo.Phone; //todo;
            this.EMail = owner.EmployeeBusinessUnitEmails.FirstOrDefault().EmailAccount.Email;

        }

        public static Party Create(Employee owner)
        {
            var party = new Party();
            party.FirstName = owner.Contact.Preferred;
            party.ContactNmlsid = owner.NmlsNo;
            party.WorkPhone = owner.EmployeePhoneBinders.FirstOrDefault().CompanyPhoneInfo.Phone; //todo;
            party.EMail = owner.EmployeeBusinessUnitEmails.FirstOrDefault().EmailAccount.Email;
            return party;
        }
    }

    // PartyMisc

    public class PartyMisc
    {
        public int PartyMiscId { get; set; } // PartyMiscID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public string HazPolicyNo { get; set; } // HazPolicyNo (length: 50)
        public System.DateTime? HazEffectiveDate { get; set; } // HazEffectiveDate
        public System.DateTime? HazRenewalDate { get; set; } // HazRenewalDate
        public bool HazInsTypeEarthquake { get; set; } // HazInsTypeEarthquake
        public bool HazInsTypeFlood { get; set; } // HazInsTypeFlood
        public bool HazInsTypeHazard { get; set; } // HazInsTypeHazard
        public bool HazInsTypeWindStorm { get; set; } // HazInsTypeWindStorm
        public short HazInsEscrowed { get; set; } // HazInsEscrowed
        public string EscrowAccountNo { get; set; } // EscrowAccountNo (length: 50)
        public bool WaiveEscrowAmended { get; set; } // WaiveEscrowAmended
        public short PropTaxesEscrowed { get; set; } // PropTaxesEscrowed
        public string FloodAccountNo { get; set; } // FloodAccountNo (length: 50)
        public string MiCertificateNo { get; set; } // MICertificateNo (length: 50)
        public int? MiPerCov { get; set; } // MIPerCov
        public string TitleAccountNo { get; set; } // TitleAccountNo (length: 50)
        public string MortgageeClause { get; set; } // MortgageeClause (length: 100)
        public string LenderVesting { get; set; } // LenderVesting (length: 50)
        public string LenderGovLaws { get; set; } // LenderGovLaws (length: 50)
        public short FloodInsRequired { get; set; } // FloodInsRequired
        public short FloodInsNfipType { get; set; } // FloodInsNFIPType
        public string FloodInsZone { get; set; } // FloodInsZone (length: 6)
        public System.DateTime? FloodInsApplicationDate { get; set; } // FloodInsApplicationDate
        public string FloodInsCommunity { get; set; } // FloodInsCommunity (length: 50)
        public string FloodInsInfoObtainedFrom { get; set; } // FloodInsInfoObtainedFrom (length: 50)
        public System.DateTime? FloodInsNfipMapPanelDate { get; set; } // FloodInsNFIPMapPanelDate
        public System.DateTime? FloodInsDeterminationDate { get; set; } // FloodInsDeterminationDate
        public decimal? ReferralFee { get; set; } // ReferralFee
        public short TitleOrderSurvey { get; set; } // TitleOrderSurvey
        public short TitleOrderPayoffs { get; set; } // TitleOrderPayoffs
        public bool ShowFirstAndSecondOnTitleOrder { get; set; } // ShowFirstAndSecondOnTitleOrder
        public decimal? FloodCoverageAmount { get; set; } // FloodCoverageAmount
        public string TitleUnderwriter { get; set; } // TitleUnderwriter (length: 50)
        public System.DateTime? LoanOfficerLicenseExpDate { get; set; } // _LoanOfficerLicenseExpDate
        public string CompanyLicenseNoOv { get; set; } // CompanyLicenseNoOV (length: 50)
        public string CompanyNmlsidov { get; set; } // CompanyNMLSIDOV (length: 50)
        public string BranchLicenseNoOv { get; set; } // BranchLicenseNoOV (length: 50)
        public string BranchNmlsidov { get; set; } // BranchNMLSIDOV (length: 50)
        public string CompanyLicenseNo { get; set; } // _CompanyLicenseNo (length: 50)
        public string CompanyNmlsid { get; set; } // _CompanyNMLSID (length: 50)
        public System.DateTime? CompanyLicenseExpirationDate { get; set; } // _CompanyLicenseExpirationDate
        public string BranchLicenseNo { get; set; } // _BranchLicenseNo (length: 50)
        public string BranchNmlsid { get; set; } // _BranchNMLSID (length: 50)
        public System.DateTime? BranchLicenseExpirationDate { get; set; } // _BranchLicenseExpirationDate
        public string SupervisoryAppraiserLicenseNumber { get; set; } // SupervisoryAppraiserLicenseNumber (length: 50)
        public string CompanyEinov { get; set; } // CompanyEINOV (length: 9)
        public string CompanyEin { get; set; } // _CompanyEIN (length: 9)
        public string InvestorCode { get; set; } // InvestorCode (length: 50)
        public short MiCompanyNameType { get; set; } // MICompanyNameType
        public string Haz2PolicyNo { get; set; } // Haz2PolicyNo (length: 50)
        public System.DateTime? Haz2EffectiveDate { get; set; } // Haz2EffectiveDate
        public System.DateTime? Haz2RenewalDate { get; set; } // Haz2RenewalDate
        public int Haz2InsType { get; set; } // Haz2InsType
        public string FloodInsCounty { get; set; } // FloodInsCounty (length: 50)
        public string FloodInsCommunityNo { get; set; } // FloodInsCommunityNo (length: 6)
        public string NfipMapIdentifier { get; set; } // NFIPMapIdentifier (length: 11)
        public System.DateTime? NfipLetterOfMapDate { get; set; } // NFIPLetterOfMapDate
        public int NfipMapIndicator { get; set; } // NFIPMapIndicator
        public bool FloodInsIsInProtectedArea { get; set; } // FloodInsIsInProtectedArea
        public System.DateTime? FloodInsProtectedAreaDesigDate { get; set; } // FloodInsProtectedAreaDesigDate
        public int FloodInsIsLifeOfLoan { get; set; } // FloodInsIsLifeOfLoan
        public string FloodCertificationIdentifier { get; set; } // FloodCertificationIdentifier (length: 50)
        public System.DateTime? NfipCommunityParticipationStartDate { get; set; } // NFIPCommunityParticipationStartDate
        public int NfipFloodDataRevisionType { get; set; } // NFIPFloodDataRevisionType
        public System.DateTime? TitleReportDate { get; set; } // TitleReportDate
        public string TitleReportItems { get; set; } // TitleReportItems (length: 50)
        public string TitleReportEndorsements { get; set; } // TitleReportEndorsements (length: 50)
        public bool BuilderOrSellerIsNonPersonEntity { get; set; } // BuilderOrSellerIsNonPersonEntity
        public string CreditAltLenderCaseNo { get; set; } // CreditAltLenderCaseNo (length: 50)
        public int ClosingAgentType { get; set; } // ClosingAgentType
        public string ServicerCode { get; set; } // ServicerCode (length: 50)
        public string WireSpecialInstructions { get; set; } // WireSpecialInstructions (length: 500)
        public decimal? TrusteeFeePercent { get; set; } // TrusteeFeePercent
        public short Seller1CdSignatureMethod { get; set; } // Seller1CDSignatureMethod
        public System.DateTime? LoanOfficerLicenseStartDate { get; set; } // _LoanOfficerLicenseStartDate
        public System.DateTime? CompanyLicenseStartDate { get; set; } // _CompanyLicenseStartDate
        public System.DateTime? BranchLicenseStartDate { get; set; } // _BranchLicenseStartDate
        public string Seller1CdSignatureMethodOtherDesc { get; set; } // Seller1CDSignatureMethodOtherDesc (length: 35)
        public int MiUnderwritingType { get; set; } // MIUnderwritingType
        public string MiSpecialProgramCode { get; set; } // MISpecialProgramCode (length: 50)
        public System.DateTime? FloodEffectiveDate { get; set; } // FloodEffectiveDate
        public System.DateTime? FloodRenewalDate { get; set; } // FloodRenewalDate
        public int? EducationBorrowerId { get; set; } // EducationBorrowerID
        public int? CounselingBorrowerId { get; set; } // CounselingBorrowerID

        public PartyMisc()
        {
            FileDataId = 0;
            HazInsTypeEarthquake = false;
            HazInsTypeFlood = false;
            HazInsTypeHazard = false;
            HazInsTypeWindStorm = false;
            HazInsEscrowed = 0;
            WaiveEscrowAmended = false;
            PropTaxesEscrowed = 0;
            FloodInsRequired = 0;
            FloodInsNfipType = 0;
            TitleOrderSurvey = 0;
            TitleOrderPayoffs = 0;
            ShowFirstAndSecondOnTitleOrder = false;
            MiCompanyNameType = 0;
            Haz2InsType = 0;
            NfipMapIndicator = 0;
            FloodInsIsInProtectedArea = false;
            FloodInsIsLifeOfLoan = 0;
            NfipFloodDataRevisionType = 0;
            BuilderOrSellerIsNonPersonEntity = false;
            ClosingAgentType = 0;
            Seller1CdSignatureMethod = 0;
            MiUnderwritingType = 0;
        }
    }

    // PartyPickerField

    public class PartyPickerField
    {
        public int PartyPickerFieldId { get; set; } // PartyPickerFieldID (Primary key)
        public int CustomPageId { get; set; } // CustomPageID
        public string ControlName { get; set; } // ControlName (length: 100)
        public short PartyCat { get; set; } // PartyCat
        public string PartyField { get; set; } // PartyField (length: 50)
        public string DisplayFields { get; set; } // DisplayFields (length: 500)

        public PartyPickerField()
        {
            CustomPageId = 0;
            PartyCat = 0;
        }
    }

    // PPELoanProgramMap

    public class PpeLoanProgramMap
    {
        public int PpeLoanProgramMapId { get; set; } // PPELoanProgramMapID (Primary key)
        public string PpeLoanProgramCode { get; set; } // PPELoanProgramCode (length: 100)
        public int LoanProgramId { get; set; } // LoanProgramID

        public PpeLoanProgramMap()
        {
            LoanProgramId = 0;
        }
    }

    // PPEstimate

    public class PpEstimate
    {
        public int PpEstimateId { get; set; } // PPEstimateID (Primary key)
        public decimal? HazPricePerc { get; set; } // HazPricePerc
        public int? HazMoAdv { get; set; } // HazMoAdv
        public short HazDisbursementSched { get; set; } // HazDisbursementSched
        public decimal? FloodPricePerc { get; set; } // FloodPricePerc
        public int? FloodMoAdv { get; set; } // FloodMoAdv
        public short FloodDisbursementSched { get; set; } // FloodDisbursementSched
        public string Line1003Name { get; set; } // Line1003Name (length: 50)
        public decimal? Line1003PurPricePerc { get; set; } // Line1003PurPricePerc
        public decimal? Line1003PurLoanPerc { get; set; } // Line1003PurLoanPerc
        public decimal? Line1003RefiPricePerc { get; set; } // Line1003RefiPricePerc
        public decimal? Line1003RefiLoanPerc { get; set; } // Line1003RefiLoanPerc
        public short Line1003DisbursementSched { get; set; } // Line1003DisbursementSched
        public short Line1003DisbursementStart { get; set; } // Line1003DisbursementStart
        public string Line1003DisbursementPeriods { get; set; } // Line1003DisbursementPeriods (length: 12)
        public string Line1007Name { get; set; } // Line1007Name (length: 50)
        public decimal? Line1007PurPricePerc { get; set; } // Line1007PurPricePerc
        public decimal? Line1007PurLoanPerc { get; set; } // Line1007PurLoanPerc
        public decimal? Line1007RefiPricePerc { get; set; } // Line1007RefiPricePerc
        public decimal? Line1007RefiLoanPerc { get; set; } // Line1007RefiLoanPerc
        public short Line1007DisbursementSched { get; set; } // Line1007DisbursementSched
        public short Line1007DisbursementStart { get; set; } // Line1007DisbursementStart
        public string Line1007DisbursementPeriods { get; set; } // Line1007DisbursementPeriods (length: 12)
        public decimal? PropTaxesPricePerc { get; set; } // PropTaxesPricePerc
        public short PropTaxesDisbursementSched { get; set; } // PropTaxesDisbursementSched
        public short PropTaxesDisbursementStart { get; set; } // PropTaxesDisbursementStart
        public string PropTaxesDisbursementPeriods { get; set; } // PropTaxesDisbursementPeriods (length: 12)
        public int Line1003MismoPrepaidItemType { get; set; } // Line1003MISMOPrepaidItemType
        public int Line1007MismoPrepaidItemType { get; set; } // Line1007MISMOPrepaidItemType
        public bool HazPremiumNetFromWire { get; set; } // HazPremiumNetFromWire
        public bool FloodPremiumNetFromWire { get; set; } // FloodPremiumNetFromWire

        public PpEstimate()
        {
            HazDisbursementSched = 0;
            FloodDisbursementSched = 0;
            Line1003DisbursementSched = 0;
            Line1003DisbursementStart = 0;
            Line1007DisbursementSched = 0;
            Line1007DisbursementStart = 0;
            PropTaxesDisbursementSched = 0;
            PropTaxesDisbursementStart = 0;
            Line1003MismoPrepaidItemType = 0;
            Line1007MismoPrepaidItemType = 0;
            HazPremiumNetFromWire = false;
            FloodPremiumNetFromWire = false;
        }
    }

    // PrepaidItem

    public class PrepaidItem
    {
        public int PrepaidItemId { get; set; } // PrepaidItemID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int? LoanId { get; set; } // LoanID
        public short PrepaidItemType { get; set; } // PrepaidItemType
        public string NameOv { get; set; } // NameOV (length: 50)
        public decimal? Payment { get; set; } // Payment
        public int? MonthsInReserve { get; set; } // MonthsInReserve
        public int? MonthsInAdvance { get; set; } // MonthsInAdvance
        public bool PremiumPoc { get; set; } // PremiumPOC
        public bool ReservesPoc { get; set; } // ReservesPOC
        public bool IncludeInPiti { get; set; } // IncludeInPITI
        public decimal? ReservesPbsDesired { get; set; } // ReservesPBSDesired
        public decimal? PremiumPbsDesired { get; set; } // PremiumPBSDesired
        public short DisbursementSched { get; set; } // DisbursementSched
        public short DisbursementStartPur { get; set; } // DisbursementStartPur
        public short DisbursementStartRefi { get; set; } // DisbursementStartRefi
        public string DisbursementPeriods { get; set; } // DisbursementPeriods (length: 26)
        public int? CushionOv { get; set; } // CushionOV
        public decimal? PrequalPaymentOv { get; set; } // PrequalPaymentOV
        public decimal? ReservesOv { get; set; } // ReservesOV
        public decimal? PremiumOv { get; set; } // PremiumOV
        public decimal? ReservesPba { get; set; } // _ReservesPBA
        public decimal? ReservesPbs { get; set; } // _ReservesPBS
        public decimal? Premium { get; set; } // _Premium
        public decimal? PremiumPba { get; set; } // _PremiumPBA
        public decimal? PremiumPbs { get; set; } // _PremiumPBS
        public int Cushion { get; set; } // _Cushion
        public decimal? CushionDollar { get; set; } // _CushionDollar
        public int ReservesPaidByOtherType { get; set; } // ReservesPaidByOtherType
        public int PremiumPaidByOtherType { get; set; } // PremiumPaidByOtherType
        public int? DisbursementStartYear { get; set; } // DisbursementStartYear
        public int MismoPrepaidItemTypeOv { get; set; } // MISMOPrepaidItemTypeOV
        public decimal? PremiumGfeDisclosedAmount { get; set; } // PremiumGFEDisclosedAmount
        public bool PremiumNetFromWire { get; set; } // PremiumNetFromWire
        public string QmatrNotes { get; set; } // QMATRNotes (length: 500)
        public decimal? PremiumPointsAndFeesAmountOv { get; set; } // PremiumPointsAndFeesAmountOV
        public decimal? ReservesPointsAndFeesAmountOv { get; set; } // ReservesPointsAndFeesAmountOV
        public int PremiumPaidToType { get; set; } // PremiumPaidToType
        public decimal? PeriodicPaymentAmount { get; set; } // PeriodicPaymentAmount
        public decimal? YearlyPayment { get; set; } // _YearlyPayment
        public string PaidToNameOv { get; set; } // PaidToNameOV (length: 50)
        public decimal? ReservesGfeDisclosedAmount { get; set; } // ReservesGFEDisclosedAmount
        public string DisbursementList { get; set; } // DisbursementList (length: 2147483647)

        public PrepaidItem()
        {
            FileDataId = 0;
            PrepaidItemType = 0;
            PremiumPoc = false;
            ReservesPoc = false;
            IncludeInPiti = false;
            DisbursementSched = 0;
            DisbursementStartPur = 0;
            DisbursementStartRefi = 0;
            Cushion = 0;
            ReservesPaidByOtherType = 0;
            PremiumPaidByOtherType = 0;
            MismoPrepaidItemTypeOv = 0;
            PremiumNetFromWire = false;
            PremiumPaidToType = 0;
        }


        public static PrepaidItem Create(in decimal propertyInsuranceAnnuallyPayment,
                                         int itemType,int? byteFileDataId = null)
        {
            var prepaidItem = new PrepaidItem();
            if (byteFileDataId.HasValue) prepaidItem.FileDataId = byteFileDataId.Value;
            prepaidItem.Payment = propertyInsuranceAnnuallyPayment/12;
            prepaidItem.PrepaidItemType = Convert.ToInt16(itemType);
            return prepaidItem;
        }
    }

    // PrepayPenalty

    public class PrepayPenalty
    {
        public int PrepayPenaltyId { get; set; } // PrepayPenaltyID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Name { get; set; } // Name (length: 50)
        public string Text { get; set; } // Text (length: 2500)
        public int? PrepayPenaltyTerm { get; set; } // PrepayPenaltyTerm

        public PrepayPenalty()
        {
            DisplayOrder = 0;
        }
    }

    // PriceAdjustment

    public class PriceAdjustment
    {
        public int PriceAdjustmentId { get; set; } // PriceAdjustmentID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int? LoanId { get; set; } // LoanID
        public string Description { get; set; } // Description (length: 500)
        public int Side { get; set; } // Side
        public decimal? PricePercent { get; set; } // PricePercent
        public decimal? IntRatePercent { get; set; } // IntRatePercent
        public decimal? FeeAmount { get; set; } // FeeAmount
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool PpeGenerated { get; set; } // PPEGenerated
        public decimal? MarginPercent { get; set; } // MarginPercent
        public string Notes { get; set; } // Notes (length: 500)
        public short PriceAdjustmentType { get; set; } // PriceAdjustmentType

        public PriceAdjustment()
        {
            FileDataId = 0;
            Side = 0;
            DisplayOrder = 0;
            PpeGenerated = false;
            PriceAdjustmentType = 0;
        }
    }

    // PrintLogItem

    public class PrintLogItem
    {
        public int PrintLogId { get; set; } // PrintLogID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public System.DateTime PrintDate { get; set; } // PrintDate
        public string UserName { get; set; } // UserName (length: 50)
        public short DocumentType { get; set; } // DocumentType
        public short PrintOutputFormat { get; set; } // PrintOutputFormat
        public int ReportType { get; set; } // ReportType
        public string DocumentName { get; set; } // DocumentName (length: 100)

        public PrintLogItem()
        {
            FileDataId = 0;
            DocumentType = 0;
            PrintOutputFormat = 0;
            ReportType = 0;
        }
    }

    // PrintSet

    public class PrintSet
    {
        public int PrintSetId { get; set; } // PrintSetID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Name { get; set; } // Name (length: 50)

        public PrintSet()
        {
            DisplayOrder = 0;
        }
    }

    // PrintSetItem

    public class PrintSetItem
    {
        public int PrintSetItemId { get; set; } // PrintSetItemID (Primary key)
        public int PrintSetId { get; set; } // PrintSetID
        public int DisplayOrder { get; set; } // DisplayOrder
        public short PrintSetItemType { get; set; } // PrintSetItemType
        public int ReportType { get; set; } // ReportType
        public string ManRepName { get; set; } // ManRepName (length: 100)
        public System.Guid? ReportGuid { get; set; } // ReportGUID

        public PrintSetItem()
        {
            PrintSetId = 0;
            DisplayOrder = 0;
            PrintSetItemType = 0;
            ReportType = 0;
        }
    }

    // PrivacyNoticeDefault

    public class PrivacyNoticeDefault
    {
        public int PrivacyNoticeDefaultId { get; set; } // PrivacyNoticeDefaultID (Primary key)
        public string DateRevised { get; set; } // DateRevised (length: 20)
        public string NameInTitle { get; set; } // NameInTitle (length: 100)
        public string NameInColumnHeader { get; set; } // NameInColumnHeader (length: 100)
        public string NameElsewhere { get; set; } // NameElsewhere (length: 100)
        public int CustomerType { get; set; } // CustomerType
        public int InformationType1 { get; set; } // InformationType1
        public int InformationType2 { get; set; } // InformationType2
        public int InformationType3 { get; set; } // InformationType3
        public int InformationType4 { get; set; } // InformationType4
        public int InformationType5 { get; set; } // InformationType5
        public int ReasonEverydayBusiness { get; set; } // ReasonEverydayBusiness
        public int ReasonMarketing { get; set; } // ReasonMarketing
        public int ReasonJointMarketing { get; set; } // ReasonJointMarketing
        public int ReasonAffiliatesTransactions { get; set; } // ReasonAffiliatesTransactions
        public int ReasonAffiliatesCreditworthiness { get; set; } // ReasonAffiliatesCreditworthiness
        public int ReasonAffiliatesMarketing { get; set; } // ReasonAffiliatesMarketing
        public int ReasonNonaffiliatesMarketing { get; set; } // ReasonNonaffiliatesMarketing
        public bool IncludeReasonAffiliatesMarketing { get; set; } // IncludeReasonAffiliatesMarketing
        public string OptOutContactPhoneDesc { get; set; } // OptOutContactPhoneDesc (length: 200)
        public string OptOutContactWebSiteDesc { get; set; } // OptOutContactWebSiteDesc (length: 200)
        public int BeginSharingDays { get; set; } // BeginSharingDays
        public int QuestionsOption { get; set; } // QuestionsOption
        public string QuestionsOtherContactInfo { get; set; } // QuestionsOtherContactInfo (length: 400)
        public string WhoIsProvidingNotice { get; set; } // WhoIsProvidingNotice (length: 400)
        public string ProtectInfoAdditionalInfo { get; set; } // ProtectInfoAdditionalInfo (length: 400)
        public int CollectionMethod1 { get; set; } // CollectionMethod1
        public int CollectionMethod2 { get; set; } // CollectionMethod2
        public int CollectionMethod3 { get; set; } // CollectionMethod3
        public int CollectionMethod4 { get; set; } // CollectionMethod4
        public int CollectionMethod5 { get; set; } // CollectionMethod5
        public bool SeeBelowForMoreRightsUnderStateLaw { get; set; } // SeeBelowForMoreRightsUnderStateLaw
        public int JointAccountSharingOption { get; set; } // JointAccountSharingOption
        public int AffiliatesOption { get; set; } // AffiliatesOption
        public string AffiliatesAdditionalInfo { get; set; } // AffiliatesAdditionalInfo (length: 400)
        public int NonaffiliatesOption { get; set; } // NonaffiliatesOption
        public string NonaffiliatesAdditionalInfo { get; set; } // NonaffiliatesAdditionalInfo (length: 400)
        public int JointMarketingOption { get; set; } // JointMarketingOption
        public string JointMarketingAdditionalInfo { get; set; } // JointMarketingAdditionalInfo (length: 400)
        public string OtherImportantInfo { get; set; } // OtherImportantInfo (length: 1000)
        public bool IncludeAcknowledgment { get; set; } // IncludeAcknowledgment

        public PrivacyNoticeDefault()
        {
            CustomerType = 0;
            InformationType1 = 0;
            InformationType2 = 0;
            InformationType3 = 0;
            InformationType4 = 0;
            InformationType5 = 0;
            ReasonEverydayBusiness = 0;
            ReasonMarketing = 0;
            ReasonJointMarketing = 0;
            ReasonAffiliatesTransactions = 0;
            ReasonAffiliatesCreditworthiness = 0;
            ReasonAffiliatesMarketing = 0;
            ReasonNonaffiliatesMarketing = 0;
            IncludeReasonAffiliatesMarketing = false;
            BeginSharingDays = 0;
            QuestionsOption = 0;
            CollectionMethod1 = 0;
            CollectionMethod2 = 0;
            CollectionMethod3 = 0;
            CollectionMethod4 = 0;
            CollectionMethod5 = 0;
            SeeBelowForMoreRightsUnderStateLaw = false;
            JointAccountSharingOption = 0;
            AffiliatesOption = 0;
            NonaffiliatesOption = 0;
            JointMarketingOption = 0;
            IncludeAcknowledgment = false;
        }
    }

    // PropertyTypeCustom

    public class PropertyTypeCustom
    {
        public int PropertyTypeCustomId { get; set; } // PropertyTypeCustomID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Text { get; set; } // Text (length: 50)

        public PropertyTypeCustom()
        {
            DisplayOrder = 0;
        }
    }

    // PurchaseCredit

    public class PurchaseCredit
    {
        public int PurchaseCreditId { get; set; } // PurchaseCreditID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int DisplayOrder { get; set; } // DisplayOrder
        public byte PurchaseCreditType { get; set; } // PurchaseCreditType
        public byte Source { get; set; } // Source
        public decimal? Amount { get; set; } // Amount
        public string DescriptionOv { get; set; } // DescriptionOV (length: 50)
        public bool OmitFromOtherAssets { get; set; } // OmitFromOtherAssets

        public PurchaseCredit()
        {
            FileDataId = 0;
            DisplayOrder = 0;
            PurchaseCreditType = 0;
            Source = 0;
            OmitFromOtherAssets = false;
        }
    }

    // The table 'RefiPurpAU' is not usable by entity framework because it
    // does not have a primary key. It is listed here for completeness.
    // RefiPurpAU

    public class RefiPurpAu
    {
        public int? RefiPurpAuId { get; set; } // RefiPurpAUId
        public string RefiPurpAuDesc { get; set; } // RefiPurpAUDesc (length: 160)
    }

    // RelatedLoan

    public class RelatedLoan
    {
        public int RelatedLoanId { get; set; } // RelatedLoanID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public short LienPriorityType { get; set; } // LienPriorityType
        public short AmortizationType { get; set; } // AmortizationType
        public short MortgageType { get; set; } // MortgageType
        public System.DateTime? NoteDate { get; set; } // NoteDate
        public decimal? NoteAmount { get; set; } // NoteAmount
        public int? LoanMaturityPeriodCount { get; set; } // LoanMaturityPeriodCount
        public System.DateTime? ScheduledFirstPaymentDate { get; set; } // ScheduledFirstPaymentDate
        public decimal? UnpaidBalance { get; set; } // UnpaidBalance
        public decimal? HelocBalance { get; set; } // HELOCBalance
        public decimal? HelocMaxBalance { get; set; } // HELOCMaxBalance
        public string AmortizationTypeOtherDesc { get; set; } // AmortizationTypeOtherDesc (length: 50)
        public string MortgageTypeOtherDesc { get; set; } // MortgageTypeOtherDesc (length: 50)
        public int? BalloonTerm { get; set; } // BalloonTerm
        public bool IsConcurrentlyClosing { get; set; } // IsConcurrentlyClosing
        public bool IsTiedToLinkedLoan { get; set; } // IsTiedToLinkedLoan
        public int LoanAffordableIndicator { get; set; } // LoanAffordableIndicator
        public bool IsHeloc { get; set; } // IsHELOC
        public string CreditorName { get; set; } // CreditorName (length: 100)
        public bool CreditorIsAnIndividual { get; set; } // CreditorIsAnIndividual
        public byte FundsSourceType { get; set; } // FundsSourceType
        public bool IsPaymentDeferredForFirstFiveYears { get; set; } // IsPaymentDeferredForFirstFiveYears
        public decimal? MonthlyPayment { get; set; } // MonthlyPayment
        public decimal? BalanceAtClosing { get; set; } // BalanceAtClosing

        public RelatedLoan()
        {
            FileDataId = 0;
            LienPriorityType = 0;
            AmortizationType = 0;
            MortgageType = 0;
            IsConcurrentlyClosing = false;
            IsTiedToLinkedLoan = false;
            LoanAffordableIndicator = 0;
            IsHeloc = false;
            CreditorIsAnIndividual = false;
            FundsSourceType = 0;
            IsPaymentDeferredForFirstFiveYears = false;
        }
    }

    // REO

    public class Reo
    {
        public int Reoid { get; set; } // REOID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int? BorrowerId { get; set; } // BorrowerID
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Street { get; set; } // Street (length: 50)
        public string City { get; set; } // City (length: 50)
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 9)
        public short ReoStatus { get; set; } // REOStatus
        public short ReoType { get; set; } // REOType
        public decimal? MarketValue { get; set; } // MarketValue
        public decimal? GrossRentalIncome { get; set; } // GrossRentalIncome
        public decimal? Taxes { get; set; } // Taxes
        public decimal? NetRentalIncomeOv { get; set; } // NetRentalIncomeOV
        public decimal? VacancyFactorOv { get; set; } // VacancyFactorOV
        public bool IsSubjectProperty { get; set; } // IsSubjectProperty
        public bool IsCurrentResidence { get; set; } // IsCurrentResidence
        public decimal? Pitiov { get; set; } // PITIOV
        public bool TiIncludedInMortgage { get; set; } // TIIncludedInMortgage
        public bool VaPurchasedOrRefinancedWithVaLoan { get; set; } // VAPurchasedOrRefinancedWithVALoan
        public System.DateTime? VaLoanDate { get; set; } // VALoanDate
        public short VaEntitlementRestoration { get; set; } // VAEntitlementRestoration
        public System.Guid? SyncGuid { get; set; } // SyncGuid
        public bool MortgagesDnaDesired { get; set; } // MortgagesDNADesired
        public byte StreetContainsUnitNumberOv { get; set; } // StreetContainsUnitNumberOV
        public int AccountHeldByType { get; set; } // AccountHeldByType
        public int? NoUnits { get; set; } // NoUnits
        public int CurrentUsageType { get; set; } // CurrentUsageType
        public int IntendedUsageType { get; set; } // IntendedUsageType
        public string Country { get; set; } // Country (length: 50)

        public Reo()
        {
            FileDataId = 0;
            DisplayOrder = 0;
            ReoStatus = 0;
            ReoType = 0;
            IsSubjectProperty = false;
            IsCurrentResidence = false;
            TiIncludedInMortgage = false;
            VaPurchasedOrRefinancedWithVaLoan = false;
            VaEntitlementRestoration = 0;
            MortgagesDnaDesired = false;
            StreetContainsUnitNumberOv = 0;
            AccountHeldByType = 0;
            CurrentUsageType = 0;
            IntendedUsageType = 0;
        }


        public static List<Reo> Create(ActionModels.LoanFile.Borrower rmBorrower,
                                       int fileDataId,
                                       ThirdPartyCodeList thirdPartyCodeList
                                       )
        {
            var properties = new List<Reo>();


            foreach (var borrowerProperty in rmBorrower.BorrowerProperties)
            {
                var propertyInfo = borrowerProperty.PropertyInfo;

                if (propertyInfo.AddressInfo != null)
                {
                    var propertyUsage = thirdPartyCodeList.GetByteProValue("PropertyUsage", propertyInfo.PropertyUsageId);
                    var currentUsageIndex = propertyUsage.FindEnumIndex(typeof(OccupancyTypeEnum));

                    var reoType = thirdPartyCodeList.GetByteProValue("REOType", propertyInfo.PropertyTypeId);
                    var reoIndex = reoType.FindEnumIndex(typeof(REOTypeEnum));

                    var propertyStatus = propertyInfo.PropertyStatus?.Replace(" ", "");
                    int reoStatusIndex = 0;
                    var isCurrentResidence = false;
                    if (propertyStatus.HasValue())
                    {
                        if (!string.IsNullOrEmpty(propertyStatus) && propertyStatus.Contains("Rental"))
                        {
                            reoStatusIndex = (int)REOStatusEnum.Rental;
                        }
                        else
                        {
                            reoStatusIndex = propertyStatus.FindEnumIndex(typeof(REOStatusEnum));
                        }

                    }
                    else
                    {
                        if (propertyInfo.IntentToSellPriorToPurchase == true)
                        {
                            reoStatusIndex = (int)REOStatusEnum.PendingSale;
                        }
                        else
                        {
                            isCurrentResidence = rmBorrower.BorrowerResidences.Any(br => br.LoanAddressId == propertyInfo.AddressInfo.Id);
                            if (isCurrentResidence)
                            {
                                reoStatusIndex = (int)REOStatusEnum.Retained;
                            }
                            else
                            {
                                reoStatusIndex = (int)REOStatusEnum.NotAssigned;
                            }
                        }
                    }

                    properties.Add(new Reo
                    {
                        IsCurrentResidence =isCurrentResidence,
                        FileDataId = fileDataId,
                        Zip = propertyInfo.AddressInfo.ZipCode,
                        City = propertyInfo.AddressInfo.CityName,
                        State = propertyInfo.AddressInfo.State.Abbreviation,
                        Street = $"{propertyInfo.AddressInfo.StreetAddress} {propertyInfo.AddressInfo.UnitNo}".Trim(),
                        CurrentUsageType = currentUsageIndex,
                        ReoType = (short)reoIndex,
                        IsSubjectProperty = false,
                        MarketValue = propertyInfo.PropertyValue,
                        ReoStatus = (short)reoStatusIndex,
                        GrossRentalIncome = propertyInfo.RentalIncome,
                        Taxes = propertyInfo.MonthlyDue,

                    });

                }
            }

            return properties;
        }
    }

    // RequestLog

    public class RequestLog
    {
        public int RequestLogId { get; set; } // RequestLogID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int RequestLogType { get; set; } // RequestLogType
        public System.DateTime? RequestDate { get; set; } // RequestDate
        public string Data { get; set; } // Data (length: 2147483647)
        public string UserName { get; set; } // UserName (length: 50)
        public string BatchCode { get; set; } // BatchCode (length: 20)
        public string Version { get; set; } // Version (length: 20)

        public RequestLog()
        {
            FileDataId = 0;
            RequestLogType = 0;
        }
    }

    // RequiredProvider

    public class RequiredProvider
    {
        public int RequiredProviderId { get; set; } // RequiredProviderID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Company { get; set; } // Company (length: 50)
        public string Street { get; set; } // Street (length: 50)
        public string City { get; set; } // City (length: 50)
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 9)
        public string WorkPhone { get; set; } // WorkPhone (length: 20)
        public string NatureRel { get; set; } // NatureRel (length: 100)
        public string HudLineNo { get; set; } // HUDLineNo (length: 10)

        public RequiredProvider()
        {
            FileDataId = 0;
            DisplayOrder = 0;
        }
    }

    // RequiredProviderDefault

    public class RequiredProviderDefault
    {
        public int RequiredProviderDefaultId { get; set; } // RequiredProviderDefaultID (Primary key)
        public int RequiredProviderSetId { get; set; } // RequiredProviderSetID
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Company { get; set; } // Company (length: 50)
        public string Street { get; set; } // Street (length: 50)
        public string City { get; set; } // City (length: 50)
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 9)
        public string WorkPhone { get; set; } // WorkPhone (length: 20)
        public string NatureRel { get; set; } // NatureRel (length: 50)
        public string HudLineNo { get; set; } // HUDLineNo (length: 10)

        public RequiredProviderDefault()
        {
            RequiredProviderSetId = 0;
            DisplayOrder = 0;
        }
    }

    // RequiredProviderSet

    public class RequiredProviderSet
    {
        public int RequiredProviderSetId { get; set; } // RequiredProviderSetID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Name { get; set; } // Name (length: 50)

        public RequiredProviderSet()
        {
            DisplayOrder = 0;
        }
    }

    // ReserveEstimateCounty

    public class ReserveEstimateCounty
    {
        public int ReserveEstimateCountyId { get; set; } // ReserveEstimateCountyID (Primary key)
        public int ReserveLineNo { get; set; } // ReserveLineNo
        public string State { get; set; } // State (length: 2)
        public string County { get; set; } // County (length: 100)
        public decimal? PricePerc { get; set; } // PricePerc
        public bool OverrideDisbursement { get; set; } // OverrideDisbursement
        public short DisbursementSched { get; set; } // DisbursementSched
        public short DisbursementStart { get; set; } // DisbursementStart
        public string DisbursementPeriods { get; set; } // DisbursementPeriods (length: 12)

        public ReserveEstimateCounty()
        {
            ReserveLineNo = 0;
            OverrideDisbursement = false;
            DisbursementSched = 0;
            DisbursementStart = 0;
        }
    }

    // ReserveEstimateState

    public class ReserveEstimateState
    {
        public int ReserveEstimateStateId { get; set; } // ReserveEstimateStateID (Primary key)
        public int ReserveLineNo { get; set; } // ReserveLineNo
        public string State { get; set; } // State (length: 2)
        public decimal? PricePerc { get; set; } // PricePerc
        public short DisbursementSched { get; set; } // DisbursementSched
        public short DisbursementStart { get; set; } // DisbursementStart
        public string DisbursementPeriods { get; set; } // DisbursementPeriods (length: 12)

        public ReserveEstimateState()
        {
            ReserveLineNo = 0;
            DisbursementSched = 0;
            DisbursementStart = 0;
        }
    }

    // Residence

    public class Residence
    {
        public int ResidenceId { get; set; } // ResidenceID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int? BorrowerId { get; set; } // BorrowerID
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool Current { get; set; } // Current
        public string Street { get; set; } // Street (length: 50)
        public string City { get; set; } // City (length: 50)
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 9)
        public short LivingStatus { get; set; } // LivingStatus
        public int? NoYears { get; set; } // NoYears
        public int? NoMonths { get; set; } // NoMonths
        public string LlName { get; set; } // LLName (length: 50)
        public string LlAttn { get; set; } // LLAttn (length: 50)
        public string LlStreet { get; set; } // LLStreet (length: 50)
        public string LlCity { get; set; } // LLCity (length: 50)
        public string LlState { get; set; } // LLState (length: 2)
        public string LlZip { get; set; } // LLZip (length: 9)
        public string LlPhone { get; set; } // LLPhone (length: 20)
        public string Notes { get; set; } // Notes (length: 500)
        public string LlFax { get; set; } // LLFax (length: 50)
        public System.Guid? SyncGuid { get; set; } // SyncGuid
        public string Country { get; set; } // Country (length: 50)
        public decimal? MonthlyRent { get; set; } // MonthlyRent
        public byte StreetContainsUnitNumberOv { get; set; } // StreetContainsUnitNumberOV

        public Residence()
        {
            FileDataId = 0;
            DisplayOrder = 0;
            Current = false;
            LivingStatus = 0;
            StreetContainsUnitNumberOv = 0;
        }


        public static List<Residence> Create(List<BorrowerResidence> borrowerResidences)
        {
            var residences = new List<Residence>();

            Update(ref residences, borrowerResidences);

            return residences;


        }

        


        private static void Update(ref List<Residence> residences, List<BorrowerResidence> borrowerResidences)
        {



            foreach (var rmBorrowerResidence in borrowerResidences)
            {
                var residence = new Residence();
                residence.City = rmBorrowerResidence.LoanAddress.CityName;
                residence.State = rmBorrowerResidence.LoanAddress.State.Name;
                residence.Country = rmBorrowerResidence.LoanAddress.County.Name;
                //residence.Street = GetCombinedStreetAddress(residence.LoanAddress.StreetAddress, residence.LoanAddress.UnitNo);
                residence.Street = $"{rmBorrowerResidence.LoanAddress.StreetAddress} {rmBorrowerResidence.LoanAddress.UnitNo}".Trim();
                residence.Zip = rmBorrowerResidence.LoanAddress.ZipCode;
                //RequestDate = residence.ToDate ?? DateTime.Now,
                residence.Current = false; // todo how to get current
                                           //ApplicantType = borrowerOwner,
                var toDate = rmBorrowerResidence.ToDate == null ? DateTime.Now : rmBorrowerResidence.ToDate.Value;
                DateTimeExtensions.ElapsedTimeSpan elapsedTime = rmBorrowerResidence.FromDate.Value.ElapsedTime(toDate);
                residence.LivingStatus = (short)((rmBorrowerResidence.OwnershipTypeId == null || rmBorrowerResidence.OwnershipTypeId == (int)OwnershipTypeEnum.Other) ? 0 : rmBorrowerResidence.OwnershipTypeId == (int)OwnershipTypeEnum.Own ? 1 : 2);
                residence.NoMonths = elapsedTime.months;
                residence.NoYears = elapsedTime.years;
                //MailingAddressIndicator = false,
                residence.MonthlyRent = rmBorrowerResidence.MonthlyRent;
                residences.Add(residence);
            }


        }



        public void Update(BorrowerResidence rmBorrowerResidence,bool current,
                           int fileDataId)
        {



             
                
                this.City = rmBorrowerResidence.LoanAddress.CityName;
                this.State = rmBorrowerResidence.LoanAddress.StateName;
                this.Country = rmBorrowerResidence.LoanAddress.CountyName;
                //residence.Street = GetCombinedStreetAddress(residence.LoanAddress.StreetAddress, residence.LoanAddress.UnitNo);
                this.Street = $"{rmBorrowerResidence.LoanAddress.StreetAddress} {rmBorrowerResidence.LoanAddress.UnitNo}".Trim();
                this.Zip = rmBorrowerResidence.LoanAddress.ZipCode;
                //RequestDate = residence.ToDate ?? DateTime.Now,
                this.Current = current; // todo how to get current
                //ApplicantType = borrowerOwner,
                var toDate = rmBorrowerResidence.ToDate == null ? DateTime.Now : rmBorrowerResidence.ToDate.Value;
                DateTimeExtensions.ElapsedTimeSpan elapsedTime = rmBorrowerResidence.FromDate.Value.ElapsedTime(toDate);
                this.LivingStatus = (short)((rmBorrowerResidence.OwnershipTypeId == null || rmBorrowerResidence.OwnershipTypeId == (int)OwnershipTypeEnum.Other) ? 0 : rmBorrowerResidence.OwnershipTypeId == (int)OwnershipTypeEnum.Own ? 1 : 2);
                this.NoMonths = elapsedTime.months;
                this.NoYears = elapsedTime.years;
                //MailingAddressIndicator = false,
                this.MonthlyRent = rmBorrowerResidence.MonthlyRent;
                FileDataId = fileDataId;




        }
    }

    // ResourceLock

    public class ResourceLock
    {
        public short ResourceType { get; set; } // ResourceType (Primary key)
        public int ResourceId { get; set; } // ResourceID (Primary key)
        public byte[] SessionId { get; set; } // SessionID (length: 88)
        public System.DateTime? LockedAtTime { get; set; } // LockedAtTime
    }

    // RHSFeePeriod

    public class RhsFeePeriod
    {
        public int RhsFeePeriodId { get; set; } // RHSFeePeriodID (Primary key)
        public int RhsCaseNoAssignmentPeriod { get; set; } // RHSCaseNoAssignmentPeriod
        public System.DateTime? StartDate { get; set; } // StartDate
        public System.DateTime? EndDate { get; set; } // EndDate
        public decimal? GuaranteeFeePurchase { get; set; } // GuaranteeFeePurchase
        public decimal? GuaranteeFeeRefinance { get; set; } // GuaranteeFeeRefinance
        public decimal? AnnualFeePurchase { get; set; } // AnnualFeePurchase
        public decimal? AnnualFeeRefinance { get; set; } // AnnualFeeRefinance

        public RhsFeePeriod()
        {
            RhsCaseNoAssignmentPeriod = 0;
        }
    }

    // SalesTools

    public class SalesTool
    {
        public int SalesToolsId { get; set; } // SalesToolsID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public short ComparisonLoanPurp { get; set; } // ComparisonLoanPurp
        public int? ComparisonId1 { get; set; } // ComparisonID1
        public int? ComparisonId2 { get; set; } // ComparisonID2
        public int? ComparisonId3 { get; set; } // ComparisonID3
        public int? PrequalId1 { get; set; } // PrequalID1
        public int? PrequalId2 { get; set; } // PrequalID2
        public int? PrequalId3 { get; set; } // PrequalID3
        public decimal? IncomeOv { get; set; } // IncomeOV
        public decimal? DebtsOv { get; set; } // DebtsOV
        public decimal? DesiredPiti { get; set; } // DesiredPITI
        public string ComparisonComments { get; set; } // ComparisonComments (length: 1000)
        public string OpenHousePropertyDesc { get; set; } // OpenHousePropertyDesc (length: 1000)
        public string OpenHouseComments { get; set; } // OpenHouseComments (length: 1000)
        public string OpenHousePicture { get; set; } // OpenHousePicture (length: 250)
        public short OpenHousePartyCat { get; set; } // OpenHousePartyCat
        public decimal? PlannerPiCurrent { get; set; } // PlannerPICurrent
        public decimal? PlannerPiProposed { get; set; } // PlannerPIProposed
        public decimal? PlannerClosingCosts { get; set; } // PlannerClosingCosts
        public bool CalcFlood { get; set; } // CalcFlood
        public bool ShowTotalSavings { get; set; } // ShowTotalSavings
        public decimal? CashToBorrower { get; set; } // CashToBorrower
        public string DebtConsolidationComments { get; set; } // DebtConsolidationComments (length: 1000)
        public bool ShowPiOnly { get; set; } // ShowPIOnly
        public bool ComparisonShowApr { get; set; } // ComparisonShowAPR
        public bool OpenHouseShowApr { get; set; } // OpenHouseShowAPR
        public bool DebtConsolidationShowApr { get; set; } // DebtConsolidationShowAPR
        public string OpenHousePictureAgent { get; set; } // OpenHousePictureAgent (length: 250)
        public string OpenHousePictureLo { get; set; } // OpenHousePictureLO (length: 250)
        public int? AntiSteeringId1 { get; set; } // AntiSteeringID1
        public int? AntiSteeringId2 { get; set; } // AntiSteeringID2
        public int? AntiSteeringId3 { get; set; } // AntiSteeringID3
        public int? AntiSteeringId4 { get; set; } // AntiSteeringID4
        public string AntiSteeringChoiceExplanation { get; set; } // AntiSteeringChoiceExplanation (length: 2147483647)
        public string AntiSteeringOption4Title { get; set; } // AntiSteeringOption4Title (length: 50)
        public int AntiSteeringSelection { get; set; } // AntiSteeringSelection

        public SalesTool()
        {
            FileDataId = 0;
            ComparisonLoanPurp = 0;
            OpenHousePartyCat = 0;
            CalcFlood = false;
            ShowTotalSavings = false;
            ShowPiOnly = false;
            ComparisonShowApr = false;
            OpenHouseShowApr = false;
            DebtConsolidationShowApr = false;
            AntiSteeringSelection = 0;
        }
    }

    // SampleDataView

    public class SampleDataView
    {
        public int FileDataId { get; set; } // FileDataID (Primary key)
        public string FirstName { get; set; } // FirstName (length: 50)
        public string LastName { get; set; } // LastName (length: 50)
        public int LockRequestType { get; set; } // LockRequestType (Primary key)
        public System.DateTime? LockRequestTime { get; set; } // LockRequestTime
        public System.DateTime? LockStartDate { get; set; } // LockStartDate
        public System.DateTime? LockExpirationDate { get; set; } // LockExpirationDate
        public decimal? LoanWith { get; set; } // LoanWith
        public decimal? IntRate { get; set; } // IntRate
        public short MortgageType { get; set; } // MortgageType (Primary key)
        public short LoanPurpose { get; set; } // LoanPurpose (Primary key)
        public short RefiPurpAu { get; set; } // RefiPurpAU (Primary key)
        public string LoanOfficerUserName { get; set; } // LoanOfficerUserName (length: 50)
        public string LoanProcessorUserName { get; set; } // LoanProcessorUserName (length: 50)
        public string Company { get; set; } // Company (length: 100)
        public short LoanStatus { get; set; } // LoanStatus (Primary key)
        public string Street { get; set; } // Street (length: 50)
        public string City { get; set; } // City (length: 50)
        public string State { get; set; } // State (length: 2)
    }

    // ScreenDefault

    public class ScreenDefault
    {
        public int ScreenDefaultId { get; set; } // ScreenDefaultID (Primary key)
        public string PageName { get; set; } // PageName (length: 100)
        public int ScreenGroup { get; set; } // ScreenGroup

        public ScreenDefault()
        {
            ScreenGroup = 0;
        }
    }

    // Secondary

    public class Secondary
    {
        public int SecondaryId { get; set; } // SecondaryID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public decimal? PaUnpaidPrincipalBalance { get; set; } // PAUnpaidPrincipalBalance
        public decimal? PaNetPrice { get; set; } // PANetPrice
        public decimal? PaPremiumOrDiscountOv { get; set; } // PAPremiumOrDiscountOV
        public decimal? PaPremiumOrDiscount { get; set; } // _PAPremiumOrDiscount
        public decimal? PaSrpPerc { get; set; } // PA_SRPPerc
        public decimal? PaSrpAmountOv { get; set; } // PA_SRPAmountOV
        public decimal? PaSrpAmount { get; set; } // _PA_SRPAmount
        public decimal? PaEscrowBalance { get; set; } // PAEscrowBalance
        public int? PaInterestDays { get; set; } // PAInterestDays
        public decimal? PaInterestAmount { get; set; } // PAInterestAmount
        public decimal? PaBuydownFunds { get; set; } // PABuydownFunds
        public decimal? PaOtherFees { get; set; } // PAOtherFees
        public decimal? PaRollFee { get; set; } // PARollFee
        public string PaNotes { get; set; } // PANotes (length: 2147483647)
        public decimal? PaWireAmount { get; set; } // _PAWireAmount
        public string FundingWireAdjustmentDesc1 { get; set; } // FundingWireAdjustmentDesc1 (length: 50)
        public decimal? FundingWireAdjustmentAmount1 { get; set; } // FundingWireAdjustmentAmount1
        public string FundingWireAdjustmentDesc2 { get; set; } // FundingWireAdjustmentDesc2 (length: 50)
        public decimal? FundingWireAdjustmentAmount2 { get; set; } // FundingWireAdjustmentAmount2
        public decimal? FundingWireAmount { get; set; } // FundingWireAmount
        public decimal? WarehouseAdvanceAmount { get; set; } // WarehouseAdvanceAmount
        public decimal? WarehouseHaircut { get; set; } // _WarehouseHaircut
        public string WarehouseLoanNo { get; set; } // WarehouseLoanNo (length: 50)
        public bool Hedged { get; set; } // Hedged
        public System.DateTime? InvestorFirstPaymentDate { get; set; } // InvestorFirstPaymentDate
        public string InvestorCommitmentNo { get; set; } // InvestorCommitmentNo (length: 50)
        public int InvestorCommitmentType { get; set; } // InvestorCommitmentType
        public System.DateTime? InvestorCommitmentDate { get; set; } // InvestorCommitmentDate
        public System.DateTime? InvestorCommitmentExpirationDate { get; set; } // InvestorCommitmentExpirationDate
        public int? InvestorLockDays { get; set; } // InvestorLockDays
        public int? InvestorLockExtension1Days { get; set; } // InvestorLockExtension1Days
        public int? InvestorLockExtension2Days { get; set; } // InvestorLockExtension2Days
        public string InvestorRateSheetId { get; set; } // InvestorRateSheetID (length: 50)
        public decimal? InvestorPriceBase { get; set; } // InvestorPriceBase
        public decimal? InvestorSrp { get; set; } // InvestorSRP
        public decimal? InvestorPriceAdjustments { get; set; } // _InvestorPriceAdjustments
        public decimal? InvestorPriceNet { get; set; } // _InvestorPriceNet
        public decimal? PricingGainPercent { get; set; } // _PricingGainPercent
        public System.DateTime? InvestorRegisteredDate { get; set; } // InvestorRegisteredDate
        public decimal? InvestorIntRate { get; set; } // InvestorIntRate
        public string InvestorLoanProgramCode { get; set; } // InvestorLoanProgramCode (length: 150)
        public string UwNotes { get; set; } // UWNotes (length: 2147483647)
        public string MandatoryInvestor { get; set; } // MandatoryInvestor (length: 50)
        public string ExcludedInvestor { get; set; } // ExcludedInvestor (length: 50)
        public string PricedInvestor { get; set; } // PricedInvestor (length: 50)
        public decimal? InvestorProfitNet { get; set; } // _InvestorProfitNet
        public decimal? PricingGainAmount { get; set; } // _PricingGainAmount
        public int? InvestorLockExtension3Days { get; set; } // InvestorLockExtension3Days
        public decimal? CreditedPrincipal { get; set; } // _CreditedPrincipal
        public decimal? CreditedInterest { get; set; } // _CreditedInterest
        public decimal? CreditedEscrowFunds { get; set; } // _CreditedEscrowFunds
        public decimal? CreditedBuydownFunds { get; set; } // _CreditedBuydownFunds
        public decimal? CreditedLateFees { get; set; } // _CreditedLateFees
        public decimal? CreditedTotal { get; set; } // _CreditedTotal
        public decimal? PaidAmountTotal { get; set; } // _PaidAmountTotal
        public string WarehouseLenderCode { get; set; } // WarehouseLenderCode (length: 50)
        public string OtherAusCaseNo { get; set; } // OtherAUSCaseNo (length: 50)
        public System.DateTime? OtherAusSubmissionDate { get; set; } // OtherAUSSubmissionDate
        public string OtherAusRecommendation { get; set; } // OtherAUSRecommendation (length: 50)
        public int FundingType { get; set; } // FundingType
        public bool AppDepositIsNettedFromWire { get; set; } // AppDepositIsNettedFromWire
        public System.DateTime? NextRateAdjustmentEffectiveDate { get; set; } // NextRateAdjustmentEffectiveDate
        public int InvestorCollateralProgram { get; set; } // InvestorCollateralProgram
        public short InvestorRemittanceType { get; set; } // InvestorRemittanceType
        public string LoanComments { get; set; } // LoanComments (length: 2147483647)
        public decimal? ArmCurrentIntRate { get; set; } // ARMCurrentIntRate
        public short GseLoanProgramIdentifier { get; set; } // GSELoanProgramIdentifier
        public decimal? AggregateLoanCurtailmentAmountOv { get; set; } // AggregateLoanCurtailmentAmountOV
        public short GseRefinanceProgramType { get; set; } // GSERefinanceProgramType
        public int? DeliquentPaymentsOverPast12MonthsCount { get; set; } // DeliquentPaymentsOverPast12MonthsCount
        public short CreditScoreImpairmentType { get; set; } // CreditScoreImpairmentType
        public decimal? ArmCurrentPiPayment { get; set; } // ARMCurrentPIPayment
        public short UlddDestination { get; set; } // ULDDDestination
        public int ArmIndexSource { get; set; } // ARMIndexSource
        public int FannieArmIndexSource { get; set; } // FannieARMIndexSource
        public short AusRecommendationOv { get; set; } // AUSRecommendationOV
        public string SellerLoanIdentifier { get; set; } // SellerLoanIdentifier (length: 30)
        public string DocumentCustodianIdentifier { get; set; } // DocumentCustodianIdentifier (length: 50)
        public string ServicerIdentifier { get; set; } // ServicerIdentifier (length: 50)
        public short ConstructionMethod { get; set; } // ConstructionMethod
        public decimal? UlddUpbAmountOv { get; set; } // ULDD_UPBAmountOV
        public decimal? LoanAcquisitionScheduledUpbAmount { get; set; } // LoanAcquisitionScheduledUPBAmount
        public decimal? DownPaymentAmount1 { get; set; } // DownPaymentAmount1
        public int DownPaymentSource1 { get; set; } // DownPaymentSource1
        public int DownPaymentType1 { get; set; } // DownPaymentType1
        public decimal? DownPaymentAmount2 { get; set; } // DownPaymentAmount2
        public int DownPaymentSource2 { get; set; } // DownPaymentSource2
        public int DownPaymentType2 { get; set; } // DownPaymentType2
        public decimal? DownPaymentAmount3 { get; set; } // DownPaymentAmount3
        public int DownPaymentSource3 { get; set; } // DownPaymentSource3
        public int DownPaymentType3 { get; set; } // DownPaymentType3
        public decimal? DownPaymentAmount4 { get; set; } // DownPaymentAmount4
        public int DownPaymentSource4 { get; set; } // DownPaymentSource4
        public int DownPaymentType4 { get; set; } // DownPaymentType4
        public short SecondMortgageType { get; set; } // SecondMortgageType
        public short CcFundsType1 { get; set; } // CCFundsType1
        public short CcSourceType1 { get; set; } // CCSourceType1
        public decimal? CcFundAmount1 { get; set; } // CCFundAmount1
        public short CcFundsType2 { get; set; } // CCFundsType2
        public short CcSourceType2 { get; set; } // CCSourceType2
        public decimal? CcFundAmount2 { get; set; } // CCFundAmount2
        public short CcFundsType3 { get; set; } // CCFundsType3
        public short CcSourceType3 { get; set; } // CCSourceType3
        public decimal? CcFundAmount3 { get; set; } // CCFundAmount3
        public short CcFundsType4 { get; set; } // CCFundsType4
        public short CcSourceType4 { get; set; } // CCSourceType4
        public decimal? CcFundAmount4 { get; set; } // CCFundAmount4
        public bool GuaranteeFeeAddOnIndicator { get; set; } // GuaranteeFeeAddOnIndicator
        public decimal? LoanBuyupBuydownBasisPointNumber { get; set; } // LoanBuyupBuydownBasisPointNumber
        public short LoanBuyupBuydownType { get; set; } // LoanBuyupBuydownType
        public short ConstToPermClosingType { get; set; } // ConstToPermClosingType
        public System.DateTime? ConstToPermFirstPaymentDueDate { get; set; } // ConstToPermFirstPaymentDueDate
        public bool ConstructionLoanIndicator { get; set; } // ConstructionLoanIndicator
        public short BuydownContributor { get; set; } // BuydownContributor
        public short ProjectAttachmentTypeOv { get; set; } // ProjectAttachmentTypeOV
        public short AttachmentTypeOv { get; set; } // AttachmentTypeOV
        public short ProjectClassificationOv { get; set; } // ProjectClassificationOV
        public string FanniePayeeIdentifierOv { get; set; } // FanniePayeeIdentifierOV (length: 9)
        public short ConstToPermClosingFeatureType { get; set; } // ConstToPermClosingFeatureType
        public short UlddSectionOfActTypeOv { get; set; } // ULDDSectionOfActTypeOV
        public System.DateTime? LastPaidInstallmentDueDateOv { get; set; } // LastPaidInstallmentDueDateOV
        public decimal? LtvBaseOv { get; set; } // LTVBaseOV
        public decimal? Ltvov { get; set; } // LTVOV
        public decimal? Cltvov { get; set; } // CLTVOV
        public decimal? Hcltvov { get; set; } // HCLTVOV
        public decimal? OfcEscrowFunds { get; set; } // OFCEscrowFunds
        public decimal? OfcBuydown { get; set; } // OFCBuydown
        public decimal? OfcAdvancedPitiPayment { get; set; } // OFCAdvancedPITIPayment
        public decimal? OfcPrincipalCurtailment { get; set; } // OFCPrincipalCurtailment
        public short MiAbsenceReason { get; set; } // MIAbsenceReason
        public int? LastUsedAdHocConditionNo { get; set; } // LastUsedAdHocConditionNo
        public int ServicingOption { get; set; } // ServicingOption
        public decimal? InvestorArmMargin { get; set; } // InvestorARMMargin
        public decimal? InvestorSrpAdjustments { get; set; } // _InvestorSRPAdjustments
        public decimal? InvestorSrpNet { get; set; } // _InvestorSRPNet
        public decimal? UnpaidPrincipalBalance { get; set; } // _UnpaidPrincipalBalance
        public bool InterimInterestNotNetFunded { get; set; } // InterimInterestNotNetFunded
        public string InvestorPpePricedProductName { get; set; } // InvestorPPEPricedProductName (length: 150)
        public System.DateTime? InvestorPpeTimePriced { get; set; } // InvestorPPETimePriced
        public decimal? MarginTotal { get; set; } // MarginTotal
        public decimal EscrowBalanceAtClosing { get; set; } // _EscrowBalanceAtClosing
        public decimal EscrowBalanceCurrent { get; set; } // _EscrowBalanceCurrent
        public decimal BuydownFundsAtClosing { get; set; } // _BuydownFundsAtClosing
        public decimal BuydownFundsCurrent { get; set; } // _BuydownFundsCurrent
        public System.DateTime? HedgeCancellationDate { get; set; } // HedgeCancellationDate
        public decimal? PricingGainAmountPercent { get; set; } // _PricingGainAmountPercent
        public string NotaryCounty { get; set; } // NotaryCounty (length: 50)
        public decimal? MarginTotalPerc { get; set; } // MarginTotalPerc
        public decimal? WarehouseAdvancePerc { get; set; } // WarehouseAdvancePerc
        public decimal? CreditedMi { get; set; } // _CreditedMI
        public decimal MiCurrent { get; set; } // _MICurrent
        public decimal? ServicingPrincipalAmountTransferred { get; set; } // ServicingPrincipalAmountTransferred
        public decimal? ServicingEscrowAmountTransferred { get; set; } // ServicingEscrowAmountTransferred
        public decimal? ServicingBuydownAmountTransferred { get; set; } // ServicingBuydownAmountTransferred
        public string ServicerLoanNumber { get; set; } // ServicerLoanNumber (length: 30)
        public decimal? WarehouseInterestRate { get; set; } // WarehouseInterestRate
        public decimal? WarehouseInterestAmount { get; set; } // _WarehouseInterestAmount
        public string WarehouseOtherFeeName { get; set; } // WarehouseOtherFeeName (length: 25)
        public decimal? WarehouseOtherFeeAmount { get; set; } // WarehouseOtherFeeAmount
        public System.DateTime? WarehouseAdvanceDate { get; set; } // WarehouseAdvanceDate
        public System.DateTime? WarehousePayoffDate { get; set; } // WarehousePayoffDate
        public string WarehouseBatchNo { get; set; } // WarehouseBatchNo (length: 20)
        public bool EscrowDepositNotNetFunded { get; set; } // EscrowDepositNotNetFunded
        public bool MipffNotNetFunded { get; set; } // MIPFFNotNetFunded
        public decimal? InsuranceFeeAmount { get; set; } // InsuranceFeeAmount
        public string PmiCheckNo { get; set; } // PMICheckNo (length: 22)
        public int? TotalMortgagedProperties { get; set; } // TotalMortgagedProperties
        public decimal? DeclaredSecondRatio { get; set; } // DeclaredSecondRatio
        public int? DeclaredMonthsInReserve { get; set; } // DeclaredMonthsInReserve
        public decimal? DeclaredCashOutAmount { get; set; } // DeclaredCashOutAmount
        public bool DeclaredNonOccCobor { get; set; } // DeclaredNonOccCobor
        public int WarehouseAdvanceCalcMethod { get; set; } // WarehouseAdvanceCalcMethod
        public decimal? FinalApr { get; set; } // FinalAPR
        public decimal? FinalFinanceCharge { get; set; } // FinalFinanceCharge
        public bool LumpSumLenderCreditIsNettedFromWire { get; set; } // LumpSumLenderCreditIsNettedFromWire
        public bool CdCureIsNettedFromWire { get; set; } // CDCureIsNettedFromWire
        public System.DateTime? RepriceNeededDateAndTime { get; set; } // RepriceNeededDateAndTime
        public decimal? TotalLiabilitiesMonthlyPaymentAmountOv { get; set; } // TotalLiabilitiesMonthlyPaymentAmountOV
        public decimal? TotalMonthlyProposedHousingExpenseAmountOv { get; set; } // TotalMonthlyProposedHousingExpenseAmountOV
        public int LoanAffordableIndicatorOv { get; set; } // LoanAffordableIndicatorOV
        public bool LoanAffordableIndicator { get; set; } // _LoanAffordableIndicator
        public bool PortfolioRefiPrepaymentPenaltyPaid { get; set; } // PortfolioRefiPrepaymentPenaltyPaid
        public int IncomeVerified { get; set; } // IncomeVerified
        public decimal? UpbPurchased { get; set; } // UPBPurchased
        public int? MonthsSincePreviousRefi { get; set; } // MonthsSincePreviousRefi
        public int IsExistingHcl { get; set; } // IsExistingHCL
        public int ENoteIndicator { get; set; } // ENoteIndicator
        public string FannieWarehouseIdentifierOv { get; set; } // FannieWarehouseIdentifierOV (length: 50)
        public string FreddieWarehouseIdentifierOv { get; set; } // FreddieWarehouseIdentifierOV (length: 50)
        public System.DateTime? PricingLastUpated { get; set; } // PricingLastUpated
        public System.DateTime? InvestorPricingLastUpated { get; set; } // InvestorPricingLastUpated
        public decimal? FinalTotalOfPayments { get; set; } // FinalTotalOfPayments
        public decimal? AmountDueToBrokerAtFundingOv { get; set; } // AmountDueToBrokerAtFundingOV
        public decimal? EscrowBalanceAtDelivery { get; set; } // EscrowBalanceAtDelivery

        public Secondary()
        {
            FileDataId = 0;
            Hedged = false;
            InvestorCommitmentType = 0;
            FundingType = 0;
            AppDepositIsNettedFromWire = false;
            InvestorCollateralProgram = 0;
            InvestorRemittanceType = 0;
            GseLoanProgramIdentifier = 0;
            GseRefinanceProgramType = 0;
            CreditScoreImpairmentType = 0;
            UlddDestination = 0;
            ArmIndexSource = 0;
            FannieArmIndexSource = 0;
            AusRecommendationOv = 0;
            ConstructionMethod = 0;
            DownPaymentSource1 = 0;
            DownPaymentType1 = 0;
            DownPaymentSource2 = 0;
            DownPaymentType2 = 0;
            DownPaymentSource3 = 0;
            DownPaymentType3 = 0;
            DownPaymentSource4 = 0;
            DownPaymentType4 = 0;
            SecondMortgageType = 0;
            CcFundsType1 = 0;
            CcSourceType1 = 0;
            CcFundsType2 = 0;
            CcSourceType2 = 0;
            CcFundsType3 = 0;
            CcSourceType3 = 0;
            CcFundsType4 = 0;
            CcSourceType4 = 0;
            GuaranteeFeeAddOnIndicator = false;
            LoanBuyupBuydownType = 0;
            ConstToPermClosingType = 0;
            ConstructionLoanIndicator = false;
            BuydownContributor = 0;
            ProjectAttachmentTypeOv = 0;
            AttachmentTypeOv = 0;
            ProjectClassificationOv = 0;
            ConstToPermClosingFeatureType = 0;
            UlddSectionOfActTypeOv = 0;
            MiAbsenceReason = 0;
            ServicingOption = 0;
            InterimInterestNotNetFunded = false;
            EscrowBalanceAtClosing = 0m;
            EscrowBalanceCurrent = 0m;
            BuydownFundsAtClosing = 0m;
            BuydownFundsCurrent = 0m;
            MiCurrent = 0m;
            EscrowDepositNotNetFunded = false;
            MipffNotNetFunded = false;
            DeclaredNonOccCobor = false;
            WarehouseAdvanceCalcMethod = 0;
            LumpSumLenderCreditIsNettedFromWire = false;
            CdCureIsNettedFromWire = false;
            LoanAffordableIndicatorOv = 0;
            LoanAffordableIndicator = false;
            PortfolioRefiPrepaymentPenaltyPaid = false;
            IncomeVerified = 0;
            IsExistingHcl = 0;
            ENoteIndicator = 0;
        }
    }

    // SecurityProfile

    public class SecurityProfile
    {
        public int SecurityProfileId { get; set; } // SecurityProfileID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Name { get; set; } // Name (length: 50)
        public long Role { get; set; } // Role
        public bool RightViewAllFiles { get; set; } // RightViewAllFiles
        public bool RightDeleteFiles { get; set; } // RightDeleteFiles
        public bool RightRenameFiles { get; set; } // RightRenameFiles
        public bool RightCopyFiles { get; set; } // RightCopyFiles
        public bool RightSecurityManager { get; set; } // RightSecurityManager
        public bool RightEditCardex { get; set; } // RightEditCardex
        public bool RightChangeFileOrganization { get; set; } // RightChangeFileOrganization
        public bool RightCreateFiles { get; set; } // RightCreateFiles
        public bool RightEditAllSharedDefaults { get; set; } // RightEditAllSharedDefaults
        public bool RightEditDefaultPolicies { get; set; } // RightEditDefaultPolicies
        public bool RightBreakFileLocks { get; set; } // RightBreakFileLocks
        public bool RestrictStatusChanges { get; set; } // RestrictStatusChanges
        public bool RestrictEditPermissionsByStatus { get; set; } // RestrictEditPermissionsByStatus
        public short CreditOrderingPermissions { get; set; } // CreditOrderingPermissions
        public short Access { get; set; } // Access
        public bool RightUndeleteFiles { get; set; } // RightUndeleteFiles
        public short AuditLogPermissions { get; set; } // AuditLogPermissions
        public bool RightCreateTemplateFiles { get; set; } // RightCreateTemplateFiles
        public bool RightOfflineAccess { get; set; } // RightOfflineAccess
        public bool RightChangePassword { get; set; } // RightChangePassword
        public bool RightPurgeFiles { get; set; } // RightPurgeFiles
        public bool RightBulkAssignment { get; set; } // RightBulkAssignment
        public bool IsPrivateProfile { get; set; } // IsPrivateProfile
        public short LoanProgramSelectionOption { get; set; } // LoanProgramSelectionOption
        public short TilDefaultSelectionOption { get; set; } // TILDefaultSelectionOption
        public bool RightCreateFileWithoutTemplate { get; set; } // RightCreateFileWithoutTemplate
        public bool PreventWordDocPreviewing { get; set; } // PreventWordDocPreviewing
        public bool PreventSandboxMode { get; set; } // PreventSandboxMode
        public bool ForceOfflineLoanImport { get; set; } // ForceOfflineLoanImport
        public int TaskPermissions { get; set; } // TaskPermissions
        public bool LimitSecurityManagerPages { get; set; } // LimitSecurityManagerPages
        public int SecurityManagerPages { get; set; } // SecurityManagerPages
        public bool LimitBulkAssignment { get; set; } // LimitBulkAssignment
        public int BulkAssignmentPermissions { get; set; } // BulkAssignmentPermissions
        public int LogFilePermissions { get; set; } // LogFilePermissions
        public bool RightApi { get; set; } // RightAPI

        public SecurityProfile()
        {
            DisplayOrder = 0;
            Role = 0;
            RightViewAllFiles = false;
            RightDeleteFiles = false;
            RightRenameFiles = false;
            RightCopyFiles = false;
            RightSecurityManager = false;
            RightEditCardex = false;
            RightChangeFileOrganization = false;
            RightCreateFiles = false;
            RightEditAllSharedDefaults = false;
            RightEditDefaultPolicies = false;
            RightBreakFileLocks = false;
            RestrictStatusChanges = false;
            RestrictEditPermissionsByStatus = false;
            CreditOrderingPermissions = 0;
            Access = 0;
            RightUndeleteFiles = false;
            AuditLogPermissions = 0;
            RightCreateTemplateFiles = false;
            RightOfflineAccess = false;
            RightChangePassword = false;
            RightPurgeFiles = false;
            RightBulkAssignment = false;
            IsPrivateProfile = false;
            LoanProgramSelectionOption = 0;
            TilDefaultSelectionOption = 0;
            RightCreateFileWithoutTemplate = false;
            PreventWordDocPreviewing = false;
            PreventSandboxMode = false;
            ForceOfflineLoanImport = false;
            TaskPermissions = 0;
            LimitSecurityManagerPages = false;
            SecurityManagerPages = 0;
            LimitBulkAssignment = false;
            BulkAssignmentPermissions = 0;
            LogFilePermissions = 0;
            RightApi = false;
        }
    }

    // SecurityProfileContactCategory

    public class SecurityProfileContactCategory
    {
        public int SecurityProfileContactCategoryId { get; set; } // SecurityProfileContactCategoryID (Primary key)
        public int SecurityProfileId { get; set; } // SecurityProfileID
        public int ContactCategory { get; set; } // ContactCategory
        public bool Enabled { get; set; } // Enabled

        public SecurityProfileContactCategory()
        {
            SecurityProfileId = 0;
            ContactCategory = 0;
            Enabled = false;
        }
    }

    // SecurityProfileDataStore

    public class SecurityProfileDataStore
    {
        public int SecurityProfileDataStoreId { get; set; } // SecurityProfileDataStoreID (Primary key)
        public int SecurityProfileId { get; set; } // SecurityProfileID
        public int DataStoreType { get; set; } // DataStoreType

        public SecurityProfileDataStore()
        {
            SecurityProfileId = 0;
            DataStoreType = 0;
        }
    }

    // SelfEmpInc

    public class SelfEmpInc
    {
        public int SelfEmpIncId { get; set; } // SelfEmpIncID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int? BorrowerId { get; set; } // BorrowerID
        public int DisplayOrder { get; set; } // DisplayOrder
        public decimal? YtdTotalAddBack { get; set; } // YTDTotalAddBack
        public decimal? YtdPercOwnership { get; set; } // YTDPercOwnership
        public decimal? YtdTotalNetProfit { get; set; } // YTDTotalNetProfit
        public decimal? YtdSalaryAndDraws { get; set; } // YTDSalaryAndDraws
        public string Form1040OtherIncDesc { get; set; } // Form1040OtherIncDesc (length: 50)
        public bool HasInterestInBus { get; set; } // HasInterestInBus
        public bool IsEmpByFamily { get; set; } // IsEmpByFamily
        public bool IsPaidCommission { get; set; } // IsPaidCommission
        public bool HasRentalProp { get; set; } // HasRentalProp
        public bool HasVariableInc { get; set; } // HasVariableInc
        public bool CanDocInc { get; set; } // CanDocInc
        public bool HasAdequateLiquidity { get; set; } // HasAdequateLiquidity
        public bool HasPositiveSales { get; set; } // HasPositiveSales
        public string BusinessName { get; set; } // BusinessName (length: 100)

        public SelfEmpInc()
        {
            FileDataId = 0;
            DisplayOrder = 0;
            HasInterestInBus = false;
            IsEmpByFamily = false;
            IsPaidCommission = false;
            HasRentalProp = false;
            HasVariableInc = false;
            CanDocInc = false;
            HasAdequateLiquidity = false;
            HasPositiveSales = false;
        }
    }

    // SelfEmpIncYear

    public class SelfEmpIncYear
    {
        public int SelfEmpIncYearId { get; set; } // SelfEmpIncYearID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int? SelfEmpIncId { get; set; } // SelfEmpIncID
        public int DisplayOrder { get; set; } // DisplayOrder
        public int? Year { get; set; } // Year
        public int? Months { get; set; } // Months
        public decimal? Form1040TotalIncome { get; set; } // Form1040TotalIncome
        public decimal? Form1040Wages { get; set; } // Form1040Wages
        public decimal? Form1040TaxExemptInt { get; set; } // Form1040TaxExemptInt
        public decimal? Form1040Refunds { get; set; } // Form1040Refunds
        public decimal? Form1040NonrecAlimony { get; set; } // Form1040NonrecAlimony
        public decimal? Form1040SchedDInc { get; set; } // Form1040SchedDInc
        public decimal? Form1040PensionDist { get; set; } // Form1040PensionDist
        public decimal? Form1040SchedEInc { get; set; } // Form1040SchedEInc
        public decimal? Form1040NonrecUnemp { get; set; } // Form1040NonrecUnemp
        public decimal? Form1040SocialSec { get; set; } // Form1040SocialSec
        public decimal? Form1040NonrecOtherInc { get; set; } // Form1040NonrecOtherInc
        public decimal? Form1040OtherInc { get; set; } // Form1040OtherInc
        public decimal? Form2106Exp { get; set; } // Form2106Exp
        public decimal? Form2106Dep { get; set; } // Form2106Dep
        public decimal? SchedBNonrecInt { get; set; } // SchedBNonrecInt
        public decimal? SchedBNonrecDiv { get; set; } // SchedBNonrecDiv
        public decimal? SchedCNonrecOther { get; set; } // SchedCNonrecOther
        public decimal? SchedCDepletion { get; set; } // SchedCDepletion
        public decimal? SchedCDepreciation { get; set; } // SchedCDepreciation
        public decimal? SchedCMeals { get; set; } // SchedCMeals
        public decimal? SchedCBusUseOfHome { get; set; } // SchedCBusUseOfHome
        public decimal? SchedCAmort { get; set; } // SchedCAmort
        public decimal? SchedDCapGains { get; set; } // SchedDCapGains
        public decimal? Form4787CapGains { get; set; } // Form4787CapGains
        public decimal? Form6252Principal { get; set; } // Form6252Principal
        public decimal? SchedERent { get; set; } // SchedERent
        public decimal? SchedEExp { get; set; } // SchedEExp
        public decimal? SchedEAmort { get; set; } // SchedEAmort
        public decimal? SchedEIns { get; set; } // SchedEIns
        public decimal? SchedFCoop { get; set; } // SchedFCoop
        public decimal? SchedFOther { get; set; } // SchedFOther
        public decimal? SchedFDepreciation { get; set; } // SchedFDepreciation
        public decimal? SchedFDepletion { get; set; } // SchedFDepletion
        public decimal? SchedFBusUseOfHome { get; set; } // SchedFBusUseOfHome
        public decimal? ParterK1OrdInc { get; set; } // ParterK1OrdInc
        public decimal? ParterK1NetInc { get; set; } // ParterK1NetInc
        public decimal? ParterK1Payments { get; set; } // ParterK1Payments
        public decimal? SCorpK1OrdInc { get; set; } // SCorpK1OrdInc
        public decimal? SCorpK1NetInc { get; set; } // SCorpK1NetInc
        public decimal? Form1065Pass { get; set; } // Form1065Pass
        public decimal? Form1065Other { get; set; } // Form1065Other
        public decimal? Form1065Depreciation { get; set; } // Form1065Depreciation
        public decimal? Form1065Depletion { get; set; } // Form1065Depletion
        public decimal? Form1065Amort { get; set; } // Form1065Amort
        public decimal? Form1065Notes { get; set; } // Form1065Notes
        public decimal? Form1065Meals { get; set; } // Form1065Meals
        public decimal? Form1065Percent { get; set; } // Form1065Percent
        public decimal? Form1120SOther { get; set; } // Form1120SOther
        public decimal? Form1120SDepreciation { get; set; } // Form1120SDepreciation
        public decimal? Form1120SDepletion { get; set; } // Form1120SDepletion
        public decimal? Form1120SAmort { get; set; } // Form1120SAmort
        public decimal? Form1120SNotes { get; set; } // Form1120SNotes
        public decimal? Form1120SMeals { get; set; } // Form1120SMeals
        public decimal? Form1120SPercent { get; set; } // Form1120SPercent
        public decimal? Form1120Inc { get; set; } // Form1120Inc
        public decimal? Form1120Tax { get; set; } // Form1120Tax
        public decimal? Form1120Gains { get; set; } // Form1120Gains
        public decimal? Form1120Other { get; set; } // Form1120Other
        public decimal? Form1120Depreciation { get; set; } // Form1120Depreciation
        public decimal? Form1120Depletion { get; set; } // Form1120Depletion
        public decimal? Form1120Amort { get; set; } // Form1120Amort
        public decimal? Form1120OpLoss { get; set; } // Form1120OpLoss
        public decimal? Form1120Notes { get; set; } // Form1120Notes
        public decimal? Form1120Meals { get; set; } // Form1120Meals
        public decimal? Form1120Percent { get; set; } // Form1120Percent
        public decimal? Form1120DivPaid { get; set; } // Form1120DivPaid
        public decimal? Form1040IraDist { get; set; } // Form1040IRADist
        public decimal? SchedEDepletion { get; set; } // SchedEDepletion
        public decimal? PartnerK1Dist { get; set; } // PartnerK1Dist
        public decimal? SCorpK1Dist { get; set; } // SCorpK1Dist
        public decimal? Form1065OrdInc { get; set; } // Form1065OrdInc
        public decimal? Form1065Dist { get; set; } // Form1065Dist
        public decimal? Form1065Payments { get; set; } // Form1065Payments
        public decimal? Form1120SOrdInc { get; set; } // Form1120SOrdInc
        public decimal? Form1120SDist { get; set; } // Form1120SDist
        public decimal? Form1040OtherGains { get; set; } // Form1040OtherGains
        public decimal? SchedCProfitLoss { get; set; } // SchedCProfitLoss
        public decimal? SchedFProfitLoss { get; set; } // SchedFProfitLoss

        public SelfEmpIncYear()
        {
            FileDataId = 0;
            DisplayOrder = 0;
        }
    }

    // ServiceOrder

    public class ServiceOrder
    {
        public int ServiceOrderId { get; set; } // ServiceOrderID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int VendorType { get; set; } // VendorType
        public string VendorTypeOtherName { get; set; } // VendorTypeOtherName (length: 35)
        public string ReferenceNumber { get; set; } // ReferenceNumber (length: 50)
        public int? BorrowerId { get; set; } // BorrowerID
        public string OrderNumber { get; set; } // OrderNumber (length: 50)
        public int VerificationType { get; set; } // VerificationType

        public ServiceOrder()
        {
            FileDataId = 0;
            VendorType = 0;
            VerificationType = 0;
        }
    }

    // The table 'ServicesInfo' is not usable by entity framework because it
    // does not have a primary key. It is listed here for completeness.
    // ServicesInfo

    public class ServicesInfo
    {
        public string RefreshToken { get; set; } // RefreshToken (length: 50)
        public System.DateTime? RefreshTime { get; set; } // RefreshTime
    }

    // Session

    public class Session
    {
        public byte[] SessionId { get; set; } // SessionID (Primary key) (length: 88)
        public System.DateTime StartDate { get; set; } // StartDate
        public System.DateTime? EndDate { get; set; } // EndDate
        public int UserOrgProfileId { get; set; } // UserOrgProfileID
        public string AspNetSessionId { get; set; } // ASPNetSessionID (length: 32)

        public Session()
        {
            StartDate = System.DateTime.Now;
        }
    }

    // Setting

    public class Setting
    {
        public int UserId { get; set; } // UserID (Primary key)
        public string Name { get; set; } // Name (Primary key) (length: 50)
        public string Value { get; set; } // Value (length: 5000)
    }

    // SettingDefault

    public class SettingDefault
    {
        public string Name { get; set; } // Name (Primary key) (length: 50)
        public string Value { get; set; } // Value (length: 5000)
    }

    // SettingPolicy

    public class SettingPolicy
    {
        public string Name { get; set; } // Name (Primary key) (length: 50)
        public short PolicyOption { get; set; } // PolicyOption
    }

    // SettingPolicyDefault

    public class SettingPolicyDefault
    {
        public string Name { get; set; } // Name (Primary key) (length: 50)
        public short PolicyOption { get; set; } // PolicyOption
    }

    // SettingUpdated

    public class SettingUpdated
    {
        public int UserId { get; set; } // UserID (Primary key)
        public System.DateTime DateUpdated { get; set; } // DateUpdated
    }

    // ShoppableProvider

    public class ShoppableProvider
    {
        public int ShoppableProviderId { get; set; } // ShoppableProviderID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int DisplayOrder { get; set; } // DisplayOrder
        public string ServicesProvided { get; set; } // ServicesProvided (length: 100)
        public string Company { get; set; } // Company (length: 100)
        public string Street { get; set; } // Street (length: 50)
        public string City { get; set; } // City (length: 50)
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 9)
        public string WorkPhone { get; set; } // WorkPhone (length: 20)
        public string Email { get; set; } // Email (length: 250)
        public int HudccLineNo { get; set; } // HUDCCLineNo
        public string FirstName { get; set; } // FirstName (length: 50)
        public string MiddleName { get; set; } // MiddleName (length: 50)
        public string LastName { get; set; } // LastName (length: 50)

        public ShoppableProvider()
        {
            FileDataId = 0;
            DisplayOrder = 0;
            HudccLineNo = 0;
        }
    }

    // ShoppableProviderDefault

    public class ShoppableProviderDefault
    {
        public int ShoppableProviderDefaultId { get; set; } // ShoppableProviderDefaultID (Primary key)
        public int ShoppableProviderSetId { get; set; } // ShoppableProviderSetID
        public int DisplayOrder { get; set; } // DisplayOrder
        public string ServicesProvided { get; set; } // ServicesProvided (length: 100)
        public string Company { get; set; } // Company (length: 100)
        public string Street { get; set; } // Street (length: 50)
        public string City { get; set; } // City (length: 50)
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 9)
        public string WorkPhone { get; set; } // WorkPhone (length: 20)
        public string Email { get; set; } // Email (length: 250)
        public int HudccLineNo { get; set; } // HUDCCLineNo
        public string FirstName { get; set; } // FirstName (length: 50)
        public string MiddleName { get; set; } // MiddleName (length: 50)
        public string LastName { get; set; } // LastName (length: 50)

        public ShoppableProviderDefault()
        {
            ShoppableProviderSetId = 0;
            DisplayOrder = 0;
            HudccLineNo = 0;
        }
    }

    // ShoppableProviderSet

    public class ShoppableProviderSet
    {
        public int ShoppableProviderSetId { get; set; } // ShoppableProviderSetID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Name { get; set; } // Name (length: 50)

        public ShoppableProviderSet()
        {
            DisplayOrder = 0;
        }
    }

    // Snapshot

    public class Snapshot
    {
        public int SnapshotId { get; set; } // SnapshotID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public System.DateTime? SnapshotDateAndTime { get; set; } // SnapshotDateAndTime
        public int OldStatus { get; set; } // OldStatus
        public int NewStatus { get; set; } // NewStatus
        public int SnapshotType { get; set; } // SnapshotType

        public Snapshot()
        {
            FileDataId = 0;
            OldStatus = 0;
            NewStatus = 0;
            SnapshotType = 0;
        }
    }

    // SnapshotFieldDef

    public class SnapshotFieldDef
    {
        public int SnapshotFieldDefId { get; set; } // SnapshotFieldDefID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public int SnapshotType { get; set; } // SnapshotType
        public string ObjectName { get; set; } // ObjectName (length: 50)
        public string FieldName { get; set; } // FieldName (length: 50)
        public string CaptionOv { get; set; } // CaptionOV (length: 50)

        public SnapshotFieldDef()
        {
            DisplayOrder = 0;
            SnapshotType = 0;
        }
    }

    // SnapshotFieldValue

    public class SnapshotFieldValue
    {
        public int SnapshotFieldValueId { get; set; } // SnapshotFieldValueID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int SnapshotId { get; set; } // SnapshotID
        public string TableAndFieldName { get; set; } // TableAndFieldName (length: 100)
        public string FieldValue { get; set; } // FieldValue (length: 7000)

        public SnapshotFieldValue()
        {
            FileDataId = 0;
            SnapshotId = 0;
        }
    }

    // SQLWhiteListStatement

    public class SqlWhiteListStatement
    {
        public int SqlWhiteListStatementId { get; set; } // SQLWhiteListStatementID (Primary key)
        public System.DateTime DateModified { get; set; } // DateModified
        public int Method { get; set; } // Method
        public string SqlStatement { get; set; } // SQLStatement
    }

    // Status

    public class Status
    {
        public int StatusId { get; set; } // StatusID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public short LoanStatus { get; set; } // LoanStatus
        public System.DateTime? LeadDate { get; set; } // LeadDate
        public System.DateTime? ApplicationDate { get; set; } // ApplicationDate
        public short FollowUpFlag { get; set; } // FollowUpFlag
        public System.DateTime? PrequalDate { get; set; } // PrequalDate
        public System.DateTime? CreditOnlyDate { get; set; } // CreditOnlyDate
        public System.DateTime? InProcessingDate { get; set; } // InProcessingDate
        public System.DateTime? SubmittedDate { get; set; } // SubmittedDate
        public System.DateTime? ApprovedDate { get; set; } // ApprovedDate
        public System.DateTime? ResubmittedDate { get; set; } // ResubmittedDate
        public System.DateTime? DeclinedDate { get; set; } // DeclinedDate
        public System.DateTime? InClosingDate { get; set; } // InClosingDate
        public System.DateTime? ClosedDate { get; set; } // ClosedDate
        public System.DateTime? CanceledDate { get; set; } // CanceledDate
        public System.DateTime? SchedClosingDate { get; set; } // SchedClosingDate
        public System.DateTime? SchedApprovalDate { get; set; } // SchedApprovalDate
        public System.DateTime? SigningDate { get; set; } // SigningDate
        public short GfeDeliveryMethod { get; set; } // GFEDeliveryMethod
        public System.DateTime? GfeDeliveryDate { get; set; } // GFEDeliveryDate
        public System.DateTime? GfeRevisionDate { get; set; } // GFERevisionDate
        public System.DateTime? FundingDate { get; set; } // FundingDate
        public System.DateTime? PurchaseContractDate { get; set; } // PurchaseContractDate
        public bool ExcludeFromManagementReports { get; set; } // ExcludeFromManagementReports
        public System.DateTime? FollowUpDate { get; set; } // FollowUpDate
        public System.DateTime? CreditFirstIssuedDate { get; set; } // CreditFirstIssuedDate
        public System.DateTime? SuspendedDate { get; set; } // SuspendedDate
        public System.DateTime? DocsSignedDate { get; set; } // DocsSignedDate
        public System.DateTime? SchedFundingDate { get; set; } // SchedFundingDate
        public System.DateTime? CustomStatus1Date { get; set; } // CustomStatus1Date
        public System.DateTime? CustomStatus2Date { get; set; } // CustomStatus2Date
        public System.DateTime? CustomStatus3Date { get; set; } // CustomStatus3Date
        public System.DateTime? CustomStatus4Date { get; set; } // CustomStatus4Date
        public System.DateTime? CustomStatus5Date { get; set; } // CustomStatus5Date
        public System.DateTime? CustomStatus6Date { get; set; } // CustomStatus6Date
        public System.DateTime? OtherDate1 { get; set; } // OtherDate1
        public System.DateTime? OtherDate2 { get; set; } // OtherDate2
        public System.DateTime? OtherDate3 { get; set; } // OtherDate3
        public string Notes { get; set; } // Notes (length: 2147483647)
        public System.DateTime? LoanPurchasedDate { get; set; } // LoanPurchasedDate
        public int GfeRevisionMethod { get; set; } // GFERevisionMethod
        public bool DisclosureWaitingPeriodWaived { get; set; } // DisclosureWaitingPeriodWaived
        public bool RedisclosureWaitingPeriodWaived { get; set; } // RedisclosureWaitingPeriodWaived
        public System.DateTime? TilRevisionDate { get; set; } // TILRevisionDate
        public int TilRevisionMethod { get; set; } // TILRevisionMethod
        public System.DateTime? RecissionDate { get; set; } // RecissionDate
        public System.DateTime? InvestorDueDate { get; set; } // InvestorDueDate
        public System.DateTime? ShipByDate { get; set; } // ShipByDate
        public System.DateTime? ClearToCloseDate { get; set; } // ClearToCloseDate
        public System.DateTime? ShippedDate { get; set; } // ShippedDate
        public System.DateTime? CollateralSentDate { get; set; } // CollateralSentDate
        public System.DateTime? DocsSentDate { get; set; } // DocsSentDate
        public System.DateTime? NoteDate { get; set; } // NoteDate
        public System.DateTime? NmlsActionDate { get; set; } // _NMLSActionDate
        public System.DateTime? StatusDate { get; set; } // _StatusDate
        public int PredProtectResult { get; set; } // PredProtectResult
        public int ComplianceEaseRiskIndicator { get; set; } // ComplianceEaseRiskIndicator
        public System.DateTime? ComplianceCheckRunDate { get; set; } // ComplianceCheckRunDate
        public bool ComplianceCheckOverride { get; set; } // ComplianceCheckOverride
        public System.DateTime? CustomStatus7Date { get; set; } // CustomStatus7Date
        public System.DateTime? CustomStatus8Date { get; set; } // CustomStatus8Date
        public System.DateTime? CustomStatus9Date { get; set; } // CustomStatus9Date
        public System.DateTime? CustomStatus10Date { get; set; } // CustomStatus10Date
        public System.DateTime? CustomStatus11Date { get; set; } // CustomStatus11Date
        public System.DateTime? AppraisalCompleted { get; set; } // AppraisalCompleted
        public System.DateTime? AppraisalDelivered { get; set; } // AppraisalDelivered
        public int AppraisalDeliveryMethod { get; set; } // AppraisalDeliveryMethod
        public System.DateTime? Appraisal2Completed { get; set; } // Appraisal2Completed
        public System.DateTime? Appraisal2Delivered { get; set; } // Appraisal2Delivered
        public int Appraisal2DeliveryMethod { get; set; } // Appraisal2DeliveryMethod
        public System.DateTime? AppraisalReceivedByBorrower { get; set; } // AppraisalReceivedByBorrower
        public System.DateTime? Appraisal2ReceivedByBorrower { get; set; } // Appraisal2ReceivedByBorrower
        public System.DateTime? AppraisalTimingWaiverDate { get; set; } // AppraisalTimingWaiverDate
        public System.DateTime? DocsBackDate { get; set; } // DocsBackDate
        public System.DateTime? DocsImagedDate { get; set; } // DocsImagedDate
        public System.DateTime? InvestorPackagedDate { get; set; } // InvestorPackagedDate
        public System.DateTime? InvestorShippedDate { get; set; } // InvestorShippedDate
        public System.DateTime? InvestorSuspendedDate { get; set; } // InvestorSuspendedDate
        public System.DateTime? InvestorClearedDate { get; set; } // InvestorClearedDate
        public System.DateTime? InsurancePackagedDate { get; set; } // InsurancePackagedDate
        public System.DateTime? InsuranceShippedDate { get; set; } // InsuranceShippedDate
        public System.DateTime? InsuranceRejectedDate { get; set; } // InsuranceRejectedDate
        public System.DateTime? InsuranceResubmittedDate { get; set; } // InsuranceResubmittedDate
        public System.DateTime? InsuranceObtainedDate { get; set; } // InsuranceObtainedDate
        public System.DateTime? InsuranceRhsRequestMailedDate { get; set; } // InsuranceRHSRequestMailedDate
        public System.DateTime? InsuranceRhsResponseDate { get; set; } // InsuranceRHSResponseDate
        public System.DateTime? InsuranceFeeDeliveredDate { get; set; } // InsuranceFeeDeliveredDate
        public System.DateTime? InsuranceMicLgcToInvestorDate { get; set; } // InsuranceMIC_LGCToInvestorDate
        public System.DateTime? ServicingFirstPaymentDate { get; set; } // ServicingFirstPaymentDate
        public System.DateTime? ServicingDataSentDate { get; set; } // ServicingDataSentDate
        public System.DateTime? ServicingPackageSentDate { get; set; } // ServicingPackageSentDate
        public System.DateTime? ServicingAuditDate { get; set; } // ServicingAuditDate
        public string ServicingAuditUserName { get; set; } // ServicingAuditUserName (length: 50)
        public System.DateTime? CorrPurchaseByDate { get; set; } // CorrPurchaseByDate
        public System.DateTime? CorrOriginalNoteReceivedDate { get; set; } // CorrOriginalNoteReceivedDate
        public System.DateTime? InsuringReceivedDate { get; set; } // InsuringReceivedDate
        public System.DateTime? InsuranceReviewReqDate { get; set; } // InsuranceReviewReqDate
        public System.DateTime? MortRecChangeDate { get; set; } // MortRecChangeDate
        public System.DateTime? CaseTransferReqDate { get; set; } // CaseTransferReqDate
        public System.DateTime? CaseTransferCompDate { get; set; } // CaseTransferCompDate
        public string InsuringNotes { get; set; } // InsuringNotes (length: 200)
        public System.DateTime? ShippingReceivedDate { get; set; } // ShippingReceivedDate
        public System.DateTime? CollateralPackSentDate { get; set; } // CollateralPackSentDate
        public System.DateTime? FinalHudToInvestorDate { get; set; } // FinalHUDToInvestorDate
        public System.DateTime? NoteShipmentReqDate { get; set; } // NoteShipmentReqDate
        public System.DateTime? NoteBackToWarehouseDate { get; set; } // NoteBackToWarehouseDate
        public string ShippingNotes { get; set; } // ShippingNotes (length: 200)
        public System.DateTime? PmiCertificateActivationDate { get; set; } // PMICertificateActivationDate
        public System.DateTime? PreapprovalApplicationDate { get; set; } // PreapprovalApplicationDate
        public System.DateTime? NmlsApplicationDate { get; set; } // _NMLSApplicationDate
        public bool CdWaitingPeriodWaived { get; set; } // CDWaitingPeriodWaived
        public System.DateTime? IntentToProceedDate { get; set; } // IntentToProceedDate
        public System.DateTime? ClosingCostsExpirationDate { get; set; } // ClosingCostsExpirationDate
        public string ClosingCostsExpirationTimeOfDayOv { get; set; } // ClosingCostsExpirationTimeOfDayOV (length: 20)
        public int CdDateIssuedOption { get; set; } // CDDateIssuedOption
        public System.DateTime? CdDateIssuedOtherDate { get; set; } // CDDateIssuedOtherDate
        public bool HasSixAppDataPoints { get; set; } // _HasSixAppDataPoints
        public int ApplicationTestResult { get; set; } // _ApplicationTestResult
        public int FeeReconciliationTestResult { get; set; } // FeeReconciliationTestResult
        public int TridDisclosureTestResult { get; set; } // TRIDDisclosureTestResult
        public int LeDisclosureTestResult { get; set; } // LEDisclosureTestResult
        public int CdDisclosureTestResult { get; set; } // CDDisclosureTestResult
        public int ClosingCostsExpirationTimeZoneCityOv { get; set; } // ClosingCostsExpirationTimeZoneCityOV
        public System.DateTime? CustomStatus12Date { get; set; } // CustomStatus12Date
        public System.DateTime? CustomStatus13Date { get; set; } // CustomStatus13Date
        public System.DateTime? CustomStatus14Date { get; set; } // CustomStatus14Date
        public System.DateTime? CustomStatus15Date { get; set; } // CustomStatus15Date
        public System.DateTime? CustomStatus16Date { get; set; } // CustomStatus16Date
        public System.DateTime? CustomStatus17Date { get; set; } // CustomStatus17Date
        public System.DateTime? CustomStatus18Date { get; set; } // CustomStatus18Date
        public System.DateTime? CustomStatus19Date { get; set; } // CustomStatus19Date
        public System.DateTime? CustomStatus20Date { get; set; } // CustomStatus20Date
        public System.DateTime? CustomStatus21Date { get; set; } // CustomStatus21Date
        public System.DateTime? CustomStatus22Date { get; set; } // CustomStatus22Date
        public System.DateTime? CustomStatus23Date { get; set; } // CustomStatus23Date
        public System.DateTime? CustomStatus24Date { get; set; } // CustomStatus24Date
        public System.DateTime? CustomStatus25Date { get; set; } // CustomStatus25Date
        public System.DateTime? CustomStatus26Date { get; set; } // CustomStatus26Date
        public System.DateTime? CustomStatus27Date { get; set; } // CustomStatus27Date
        public System.DateTime? CustomStatus28Date { get; set; } // CustomStatus28Date
        public System.DateTime? CustomStatus29Date { get; set; } // CustomStatus29Date
        public System.DateTime? CustomStatus30Date { get; set; } // CustomStatus30Date
        public bool SigningAppointmentConfirmed { get; set; } // SigningAppointmentConfirmed
        public int SigningAppointmentLocationType { get; set; } // SigningAppointmentLocationType
        public string SigningAppointmentTime { get; set; } // SigningAppointmentTime (length: 15)
        public System.DateTime? SigningAppointmentDate { get; set; } // SigningAppointmentDate
        public System.DateTime? LastStatusChangeDateTime { get; set; } // LastStatusChangeDateTime
        public int ESignTestResult { get; set; } // _ESignTestResult
        public System.DateTime? MersRegistrationDate { get; set; } // MERSRegistrationDate
        public System.DateTime? MersTransferDate { get; set; } // MERSTransferDate
        public string MersCurrentInvestorOrgId { get; set; } // MERSCurrentInvestorOrgID (length: 7)
        public string MersCurrentServicerOrgId { get; set; } // MERSCurrentServicerOrgID (length: 7)
        public string MersCurrentSubservicerOrgId { get; set; } // MERSCurrentSubservicerOrgID (length: 7)
        public int? InterimIntAdditionalConsentedDays { get; set; } // InterimIntAdditionalConsentedDays
        public int NmlsApplicationDateCalcOption { get; set; } // NMLSApplicationDateCalcOption

        public Status()
        {
            FileDataId = 0;
            LoanStatus = 0;
            FollowUpFlag = 0;
            GfeDeliveryMethod = 0;
            ExcludeFromManagementReports = false;
            GfeRevisionMethod = 0;
            DisclosureWaitingPeriodWaived = false;
            RedisclosureWaitingPeriodWaived = false;
            TilRevisionMethod = 0;
            PredProtectResult = 0;
            ComplianceEaseRiskIndicator = 0;
            ComplianceCheckOverride = false;
            AppraisalDeliveryMethod = 0;
            Appraisal2DeliveryMethod = 0;
            CdWaitingPeriodWaived = false;
            CdDateIssuedOption = 0;
            HasSixAppDataPoints = false;
            ApplicationTestResult = 0;
            FeeReconciliationTestResult = 0;
            TridDisclosureTestResult = 0;
            LeDisclosureTestResult = 0;
            CdDisclosureTestResult = 0;
            ClosingCostsExpirationTimeZoneCityOv = 0;
            SigningAppointmentConfirmed = false;
            SigningAppointmentLocationType = 0;
            ESignTestResult = 0;
            NmlsApplicationDateCalcOption = 0;
        }


        public static Status Create(LoanApplication loanApplication)
        {
            var status = new Status();
            status.ApplicationDate = DateTime.UtcNow;
            status.SchedClosingDate = loanApplication.ExpectedClosingDate;
            return status;
        }
        public void Update(LoanApplication loanApplication)
        {

            this.ApplicationDate = DateTime.UtcNow;
            this.SchedClosingDate = loanApplication.ExpectedClosingDate;

        }
    }

    // StatusAlertDefault

    public class StatusAlertDefault
    {
        public int AlertDefaultId { get; set; } // AlertDefaultID (Primary key)
        public int? LockExpirationRedAlertDays { get; set; } // LockExpirationRedAlertDays
        public int? LockExpirationYellowAlertDays { get; set; } // LockExpirationYellowAlertDays
        public long LockExpirationAlertUserRoles { get; set; } // LockExpirationAlertUserRoles
        public int? ApprovalDateRedAlertDays { get; set; } // ApprovalDateRedAlertDays
        public int? ApprovalDateYellowAlertDays { get; set; } // ApprovalDateYellowAlertDays
        public long ApprovalDateAlertUserRoles { get; set; } // ApprovalDateAlertUserRoles
        public int? ClosingDateRedAlertDays { get; set; } // ClosingDateRedAlertDays
        public int? ClosingDateYellowAlertDays { get; set; } // ClosingDateYellowAlertDays
        public long ClosingDateAlertUserRoles { get; set; } // ClosingDateAlertUserRoles
        public long ApprovalDateStatusBits { get; set; } // ApprovalDateStatusBits
        public long LockExpirationStatusBits { get; set; } // LockExpirationStatusBits
        public long ClosingDateStatusBits { get; set; } // ClosingDateStatusBits
        public long NeededItemStatusBits { get; set; } // NeededItemStatusBits

        public StatusAlertDefault()
        {
            LockExpirationAlertUserRoles = 0;
            ApprovalDateAlertUserRoles = 0;
            ClosingDateAlertUserRoles = 0;
            ApprovalDateStatusBits = 0;
            LockExpirationStatusBits = 0;
            ClosingDateStatusBits = 0;
            NeededItemStatusBits = 0;
        }
    }

    // StatusAssignmentItem

    public class StatusAssignmentItem
    {
        public int StatusAssignmentItemId { get; set; } // StatusAssignmentItemID (Primary key)
        public int SecurityProfileId { get; set; } // SecurityProfileID
        public int DisplayOrder { get; set; } // DisplayOrder
        public int FromStatus { get; set; } // FromStatus
        public int ToStatus { get; set; } // ToStatus

        public StatusAssignmentItem()
        {
            SecurityProfileId = 0;
            DisplayOrder = 0;
            FromStatus = 0;
            ToStatus = 0;
        }
    }

    // SubProp

    public class SubProp
    {
        public int SubPropId { get; set; } // SubPropID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public string Street { get; set; } // Street (length: 50)
        public string City { get; set; } // City (length: 50)
        public string County { get; set; } // County (length: 50)
        public int? CountyCode { get; set; } // CountyCode
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 9)
        public decimal? AppraisedValue { get; set; } // AppraisedValue
        public short PropertyType { get; set; } // PropertyType
        public int? NoUnits { get; set; } // NoUnits
        public string LegalDesc { get; set; } // LegalDesc (length: 5000)
        public bool MetesAndBounds { get; set; } // MetesAndBounds
        public int? YearBuilt { get; set; } // YearBuilt
        public decimal? FirstMortPi { get; set; } // FirstMortPI
        public decimal? FirstMortBalance { get; set; } // FirstMortBalance
        public decimal? SecondMortPi { get; set; } // SecondMortPI
        public decimal? SecondMortBalance { get; set; } // SecondMortBalance
        public decimal? GrossRentalIncome { get; set; } // GrossRentalIncome
        public decimal? VacancyFactorOv { get; set; } // VacancyFactorOV
        public bool ReservesRequired { get; set; } // ReservesRequired
        public int? CYearLotAcq { get; set; } // CYearLotAcq
        public decimal? COrigCost { get; set; } // COrigCost
        public decimal? CAmtExLiens { get; set; } // CAmtExLiens
        public decimal? CPresValLot { get; set; } // CPresValLot
        public decimal? CImprvCost { get; set; } // CImprvCost
        public int? RYearLotAcq { get; set; } // RYearLotAcq
        public decimal? ROrigCost { get; set; } // ROrigCost
        public decimal? RAmtExLiens { get; set; } // RAmtExLiens
        public short ImprvMade { get; set; } // ImprvMade
        public string ImprvDesc { get; set; } // ImprvDesc (length: 50)
        public decimal? ImprvCost { get; set; } // ImprvCost
        public string MannerTitleHeld { get; set; } // MannerTitleHeld (length: 100)
        public short EstHeld { get; set; } // EstHeld
        public System.DateTime? EstLeaseHoldEx { get; set; } // EstLeaseHoldEx
        public string Msa { get; set; } // MSA (length: 5)
        public decimal? NetCashFlowOv { get; set; } // NetCashFlowOV
        public decimal? AltImpRep { get; set; } // AltImpRep
        public decimal? LandValue { get; set; } // LandValue
        public bool IsPud { get; set; } // IsPUD
        public short PropertyClass { get; set; } // PropertyClass
        public string ProjectName { get; set; } // ProjectName (length: 60)
        public int? SquareFeet { get; set; } // SquareFeet
        public decimal? PropertyAge { get; set; } // PropertyAge
        public int? TotalRooms { get; set; } // TotalRooms
        public decimal? Bathrooms { get; set; } // Bathrooms
        public int? Bedrooms { get; set; } // Bedrooms
        public decimal? FhavaUnpaidBalance { get; set; } // FHAVAUnpaidBalance
        public int? RemainingEconomicLife { get; set; } // RemainingEconomicLife
        public string PropertyTypeCustom { get; set; } // PropertyTypeCustom (length: 50)
        public string AssessorsParcelNo { get; set; } // AssessorsParcelNo (length: 50)
        public System.DateTime? PriorSaleDate { get; set; } // PriorSaleDate
        public decimal? PriorSaleAmount { get; set; } // PriorSaleAmount
        public decimal? FirstMortOrigAmount { get; set; } // FirstMortOrigAmount
        public System.DateTime? DateLandAcquired { get; set; } // DateLandAcquired
        public decimal? LandPurchasePrice { get; set; } // LandPurchasePrice
        public bool LandAcquiredNotByPurchase { get; set; } // LandAcquiredNotByPurchase
        public decimal? AvmConfidenceScore { get; set; } // AVMConfidenceScore
        public System.DateTime? AvmDeterminationDate { get; set; } // AVMDeterminationDate
        public int? Stories { get; set; } // Stories
        public int WarrantableCondo { get; set; } // WarrantableCondo
        public bool PropertyTbd { get; set; } // PropertyTBD
        public bool SpecialFloodHazardArea { get; set; } // SpecialFloodHazardArea
        public short ProjectStatusType { get; set; } // ProjectStatusType
        public short ProjectDesignType { get; set; } // ProjectDesignType
        public int? ProjectDwellingUnitCount { get; set; } // ProjectDwellingUnitCount
        public int? ProjectDwellingUnitsSoldCount { get; set; } // ProjectDwellingUnitsSoldCount
        public System.DateTime? PropertyValuationEffectiveDate { get; set; } // PropertyValuationEffectiveDate
        public short PropertyValuationMethod { get; set; } // PropertyValuationMethod
        public short AvmModelType { get; set; } // AVMModelType
        public string PropertyValuationUcdpDocumentIdentifier { get; set; } // PropertyValuationUCDPDocumentIdentifier (length: 20)
        public int? BedroomsUnit1 { get; set; } // BedroomsUnit1
        public int? BedroomsUnit2 { get; set; } // BedroomsUnit2
        public int? BedroomsUnit3 { get; set; } // BedroomsUnit3
        public int? BedroomsUnit4 { get; set; } // BedroomsUnit4
        public decimal? GrossRentUnit1 { get; set; } // GrossRentUnit1
        public decimal? GrossRentUnit2 { get; set; } // GrossRentUnit2
        public decimal? GrossRentUnit3 { get; set; } // GrossRentUnit3
        public decimal? GrossRentUnit4 { get; set; } // GrossRentUnit4
        public string OriginalLoanGseIdentifier { get; set; } // OriginalLoanGSEIdentifier (length: 20)
        public short OriginalLoanOwner { get; set; } // OriginalLoanOwner
        public decimal? AssessedValue { get; set; } // AssessedValue
        public bool CreditSaleIndicator { get; set; } // CreditSaleIndicator
        public int UcdpFindingsStatusFannie { get; set; } // UCDPFindingsStatusFannie
        public int UcdpFindingsStatusFreddie { get; set; } // UCDPFindingsStatusFreddie
        public string LegalDescShort { get; set; } // LegalDescShort (length: 2147483647)
        public bool PartialSfha { get; set; } // PartialSFHA
        public string FirstMortQmatrNotes { get; set; } // FirstMortQMATRNotes (length: 2147483647)
        public string SecondMortQmatrNotes { get; set; } // SecondMortQMATRNotes (length: 2147483647)
        public int UlddPropertyValuationForm { get; set; } // ULDDPropertyValuationForm
        public short UlddManufacturedWidthType { get; set; } // ULDDManufacturedWidthType
        public string ParsedHouseNumber { get; set; } // ParsedHouseNumber (length: 20)
        public string ParsedDirectionPrefix { get; set; } // ParsedDirectionPrefix (length: 10)
        public string ParsedStreetName { get; set; } // ParsedStreetName (length: 50)
        public string ParsedStreetSuffix { get; set; } // ParsedStreetSuffix (length: 20)
        public string ParsedDirectionSuffix { get; set; } // ParsedDirectionSuffix (length: 10)
        public string ParsedUnitNumber { get; set; } // ParsedUnitNumber (length: 20)
        public int DelayedSettlementDueToConstruction { get; set; } // DelayedSettlementDueToConstruction
        public int AppraisedValueStatusOv { get; set; } // AppraisedValueStatusOV
        public int LandLoanStatus { get; set; } // LandLoanStatus
        public int TridAltImpRepOption { get; set; } // TRIDAltImpRepOption
        public int? PartialSfhaStructureCount { get; set; } // PartialSFHAStructureCount
        public string PreviousLoanNumber { get; set; } // PreviousLoanNumber (length: 50)
        public int FreAppraisalFormType { get; set; } // FREAppraisalFormType
        public string FreAppraisalFormTypeOther { get; set; } // FREAppraisalFormTypeOther (length: 50)
        public int ManufacturedHomeLandPropertyInterest { get; set; } // ManufacturedHomeLandPropertyInterest
        public int? MultifamilyAffordableUnitsCount { get; set; } // MultifamilyAffordableUnitsCount
        public int ManufacturedHomeSecuredPropertyType { get; set; } // ManufacturedHomeSecuredPropertyType
        public bool IsChattelLoan { get; set; } // IsChattelLoan
        public bool PropertyHasNoAddress { get; set; } // PropertyHasNoAddress
        public string Lot { get; set; } // Lot (length: 50)
        public string Block { get; set; } // Block (length: 50)
        public decimal? ManufacturedHomeWidth { get; set; } // ManufacturedHomeWidth
        public decimal? ManufacturedHomeLength { get; set; } // ManufacturedHomeLength
        public int ManufacturedHomeAttachedToFoundation { get; set; } // ManufacturedHomeAttachedToFoundation
        public int ManufacturedHomeCondition { get; set; } // ManufacturedHomeCondition
        public string ManufacturedHomeHudCertLabelId1 { get; set; } // ManufacturedHomeHUDCertLabelID1 (length: 12)
        public string ManufacturedHomeHudCertLabelId2 { get; set; } // ManufacturedHomeHUDCertLabelID2 (length: 12)
        public string ManufacturedHomeHudCertLabelId3 { get; set; } // ManufacturedHomeHUDCertLabelID3 (length: 12)
        public string ManufacturedHomeMake { get; set; } // ManufacturedHomeMake (length: 30)
        public string ManufacturedHomeModel { get; set; } // ManufacturedHomeModel (length: 30)
        public string ManufacturedHomeSerialNo { get; set; } // ManufacturedHomeSerialNo (length: 30)
        public bool HasHomesteadExemption { get; set; } // HasHomesteadExemption
        public byte IsMixedUseProperty { get; set; } // IsMixedUseProperty
        public bool NetCashFlowDnaDesired { get; set; } // NetCashFlowDNADesired
        public bool OtherLoansDnaDesired { get; set; } // OtherLoansDNADesired
        public bool IsConversionOfLandContract { get; set; } // IsConversionOfLandContract
        public bool IsRenovation { get; set; } // IsRenovation
        public bool HasCleanEnergyLien { get; set; } // HasCleanEnergyLien
        public System.DateTime? LotAcquiredDate { get; set; } // LotAcquiredDate
        public int IndianCountryLandTenure { get; set; } // IndianCountryLandTenure
        public byte StreetContainsUnitNumberOv { get; set; } // StreetContainsUnitNumberOV
        public byte LandValueType { get; set; } // LandValueType
        public string PropertyDataId { get; set; } // PropertyDataID (length: 50)

        public SubProp()
        {
            FileDataId = 0;
            PropertyType = 0;
            MetesAndBounds = false;
            ReservesRequired = false;
            ImprvMade = 0;
            EstHeld = 0;
            IsPud = false;
            PropertyClass = 0;
            LandAcquiredNotByPurchase = false;
            WarrantableCondo = 0;
            PropertyTbd = false;
            SpecialFloodHazardArea = false;
            ProjectStatusType = 0;
            ProjectDesignType = 0;
            PropertyValuationMethod = 0;
            AvmModelType = 0;
            OriginalLoanOwner = 0;
            CreditSaleIndicator = false;
            UcdpFindingsStatusFannie = 0;
            UcdpFindingsStatusFreddie = 0;
            PartialSfha = false;
            UlddPropertyValuationForm = 0;
            UlddManufacturedWidthType = 0;
            DelayedSettlementDueToConstruction = 0;
            AppraisedValueStatusOv = 0;
            LandLoanStatus = 0;
            TridAltImpRepOption = 0;
            FreAppraisalFormType = 0;
            ManufacturedHomeLandPropertyInterest = 0;
            ManufacturedHomeSecuredPropertyType = 0;
            IsChattelLoan = false;
            PropertyHasNoAddress = false;
            ManufacturedHomeAttachedToFoundation = 0;
            ManufacturedHomeCondition = 0;
            HasHomesteadExemption = false;
            IsMixedUseProperty = 0;
            NetCashFlowDnaDesired = false;
            OtherLoansDnaDesired = false;
            IsConversionOfLandContract = false;
            IsRenovation = false;
            HasCleanEnergyLien = false;
            IndianCountryLandTenure = 0;
            StreetContainsUnitNumberOv = 0;
            LandValueType = 0;
        }


        public static SubProp Create(LoanApplication loanApplication, ThirdPartyCodeList thirdPartyCodeList)
        {
            var subProp = new SubProp();


            subProp.City = loanApplication.PropertyInfo.AddressInfo.CityName;
            subProp.State = loanApplication.PropertyInfo.AddressInfo.State.Abbreviation;
            subProp.County = loanApplication.PropertyInfo.AddressInfo.CountyName;
            subProp.Street = loanApplication.PropertyInfo.AddressInfo.StreetAddress;
            subProp.NoUnits = thirdPartyCodeList.GetByteProValue("PropertyInfo_NoUnit", loanApplication.PropertyInfo.PropertyTypeId).ToInt();
            subProp.Zip = loanApplication.PropertyInfo.AddressInfo.ZipCode;
            subProp.RAmtExLiens = loanApplication.PropertyInfo.MortgageOnProperties.Sum(mortgageOnProperty => mortgageOnProperty.MortgageBalance);
            subProp.LotAcquiredDate = loanApplication.PropertyInfo.DateAcquired;


            if (loanApplication.LoanPurposeId == (int)Enums.Rainmaker.LoanPurposeEnum.Refinance || loanApplication.LoanPurposeId == (int)Enums.Rainmaker.LoanPurposeEnum.CashOut)
            {
                subProp.ROrigCost = loanApplication.PropertyInfo.OriginalPurchasePrice;
                subProp.RYearLotAcq = loanApplication.PropertyInfo.DateAcquired.Value.Year;
            }

            subProp.PropertyType = (short)thirdPartyCodeList.GetByteProValue("PropertyType",
                                                                      loanApplication.PropertyInfo.PropertyTypeId).FindEnumIndex(typeof(PropertyTypeEnum));

            return subProp;

        }
        public void Update(LoanApplication loanApplication, ThirdPartyCodeList thirdPartyCodeList)
        {
            this.City = loanApplication.PropertyInfo.AddressInfo.CityName;
            this.State = loanApplication.PropertyInfo.AddressInfo.State.Abbreviation;
            this.County = loanApplication.PropertyInfo.AddressInfo.CountyName;
            this.Street = loanApplication.PropertyInfo.AddressInfo.StreetAddress;
            this.NoUnits = thirdPartyCodeList.GetByteProValue("PropertyInfo_NoUnit", loanApplication.PropertyInfo.PropertyTypeId).ToInt();
            this.Zip = loanApplication.PropertyInfo.AddressInfo.ZipCode;
            this.RAmtExLiens = loanApplication.PropertyInfo.MortgageOnProperties.Sum(mortgageOnProperty => mortgageOnProperty.MortgageBalance);
            this.LotAcquiredDate = loanApplication.PropertyInfo.DateAcquired;


            if (loanApplication.LoanPurposeId == (int)Enums.Rainmaker.LoanPurposeEnum.Refinance || loanApplication.LoanPurposeId == (int)Enums.Rainmaker.LoanPurposeEnum.CashOut)
            {
                this.ROrigCost = loanApplication.PropertyInfo.OriginalPurchasePrice;
                this.RYearLotAcq = loanApplication.PropertyInfo.DateAcquired.Value.Year;
            }

            this.PropertyType = (short)thirdPartyCodeList.GetByteProValue("PropertyType",
                                                                             loanApplication.PropertyInfo.PropertyTypeId).FindEnumIndex(typeof(PropertyTypeEnum));

            

        }
    }

    // Task

    public class Task
    {
        public int TaskId { get; set; } // TaskID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public System.DateTime DateCreated { get; set; } // DateCreated
        public System.DateTime? DateDue { get; set; } // DateDue
        public string Description { get; set; } // Description (length: 200)
        public System.DateTime? DateCompleted { get; set; } // DateCompleted
        public string CompletedBy { get; set; } // CompletedBy (length: 50)
        public string Notes { get; set; } // Notes (length: 2147483647)
        public long AssignedUserRole { get; set; } // AssignedUserRole
        public string CreatedBy { get; set; } // CreatedBy (length: 50)
        public long VisibleToUserRoles { get; set; } // VisibleToUserRoles
        public bool SendNotification { get; set; } // SendNotification
        public long NotificationUserRoles { get; set; } // NotificationUserRoles
        public string NotificationOtherEmail { get; set; } // NotificationOtherEmail (length: 200)
        public string NotificationMessage { get; set; } // NotificationMessage (length: 2147483647)
        public int NotificationParties { get; set; } // NotificationParties
        public long CreatedByUserRole { get; set; } // CreatedByUserRole
        public int TaskSecurity { get; set; } // TaskSecurity
        public string NotificationMessageBorrower { get; set; } // NotificationMessageBorrower (length: 500)
        public int TaskPriority { get; set; } // TaskPriority
        public int? TaskTemplateId { get; set; } // TaskTemplateID
        public bool OverrideDescription { get; set; } // OverrideDescription

        public Task()
        {
            FileDataId = 0;
            AssignedUserRole = 0;
            VisibleToUserRoles = 0;
            SendNotification = false;
            NotificationUserRoles = 0;
            NotificationParties = 0;
            CreatedByUserRole = 0;
            TaskSecurity = 0;
            TaskPriority = 0;
            OverrideDescription = false;
        }
    }

    // TaskDefault

    public class TaskDefault
    {
        public int TaskDefaultId { get; set; } // TaskDefaultID (Primary key)
        public short TaskType { get; set; } // TaskType
        public short NeededItemType { get; set; } // NeededItemType
        public int? DateDueDays { get; set; } // DateDueDays
        public int? DateExpirationDays { get; set; } // DateExpirationDays
        public int? DueDaysRedAlert { get; set; } // DueDaysRedAlert
        public int? DueDaysYellowAlert { get; set; } // DueDaysYellowAlert
        public long DueDaysAlertUserRoles { get; set; } // DueDaysAlertUserRoles
        public int? ExpireDaysRedAlert { get; set; } // ExpireDaysRedAlert
        public int? ExpireDaysYellowAlert { get; set; } // ExpireDaysYellowAlert
        public long ExpireDaysAlertUserRoles { get; set; } // ExpireDaysAlertUserRoles
        public string NeededItemMatrixName { get; set; } // NeededItemMatrixName (length: 50)
        public long AssignedTo { get; set; } // AssignedTo
        public short FollowUpFlag { get; set; } // FollowUpFlag

        public TaskDefault()
        {
            TaskType = 0;
            NeededItemType = 0;
            DueDaysAlertUserRoles = 0;
            ExpireDaysAlertUserRoles = 0;
            AssignedTo = 0;
            FollowUpFlag = 0;
        }
    }

    // TaskTemplate

    public class TaskTemplate
    {
        public int TaskTemplateId { get; set; } // TaskTemplateID (Primary key)
        public string Description { get; set; } // Description (length: 200)
        public long AssignedUserRole { get; set; } // AssignedUserRole
        public long VisibleToUserRoles { get; set; } // VisibleToUserRoles
        public bool SendNotification { get; set; } // SendNotification
        public long NotificationUserRoles { get; set; } // NotificationUserRoles
        public string NotificationOtherEmail { get; set; } // NotificationOtherEmail (length: 200)
        public string NotificationMessage { get; set; } // NotificationMessage (length: 500)
        public int NotificationParties { get; set; } // NotificationParties
        public int TaskDueOption { get; set; } // TaskDueOption
        public int? DueDateDays { get; set; } // DueDateDays
        public int TaskSecurity { get; set; } // TaskSecurity
        public bool SendTaskCreationNotification { get; set; } // SendTaskCreationNotification
        public int DisplayOrder { get; set; } // DisplayOrder
        public int TaskTemplateType { get; set; } // TaskTemplateType
        public int NeededItemTaskTemplateUse { get; set; } // NeededItemTaskTemplateUse
        public int NeededItemTaskType { get; set; } // NeededItemTaskType
        public int NeededItemType { get; set; } // NeededItemType
        public string NeededItemDescriptions { get; set; } // NeededItemDescriptions (length: 2147483647)
        public int TriggeringLoanStatus { get; set; } // TriggeringLoanStatus
        public string NotificationMessageBorrower { get; set; } // NotificationMessageBorrower (length: 500)
        public int TaskPriority { get; set; } // TaskPriority
        public int ConditionTaskTemplateUse { get; set; } // ConditionTaskTemplateUse
        public int ConditionStage { get; set; } // ConditionStage
        public string ConditionNo { get; set; } // ConditionNo (length: 1000)
        public string Notes { get; set; } // Notes (length: 2147483647)
        public string TaskTemplateIDs { get; set; } // TaskTemplateIDs (length: 2147483647)
        public bool OverrideDescription { get; set; } // OverrideDescription

        public TaskTemplate()
        {
            AssignedUserRole = 0;
            VisibleToUserRoles = 0;
            SendNotification = false;
            NotificationUserRoles = 0;
            NotificationParties = 0;
            TaskDueOption = 0;
            TaskSecurity = 0;
            SendTaskCreationNotification = false;
            DisplayOrder = 0;
            TaskTemplateType = 0;
            NeededItemTaskTemplateUse = 0;
            NeededItemTaskType = 0;
            NeededItemType = 0;
            TriggeringLoanStatus = 0;
            TaskPriority = 0;
            ConditionTaskTemplateUse = 0;
            ConditionStage = 0;
            OverrideDescription = false;
        }
    }

    // TaskTemplateOrganization

    public class TaskTemplateOrganization
    {
        public int TaskTemplateOrganizationId { get; set; } // TaskTemplateOrganizationID (Primary key)
        public int TaskTemplateId { get; set; } // TaskTemplateID
        public int OrganizationId { get; set; } // OrganizationID

        public TaskTemplateOrganization()
        {
            TaskTemplateId = 0;
            OrganizationId = 0;
        }
    }

    // _tblUserSetting

    public class TblUserSetting
    {
        public int Id { get; set; } // ID (Primary key)
        public string UserId { get; set; } // UserID (length: 100)
        public string Report { get; set; } // Report (length: 100)
        public string Setting { get; set; } // Setting (length: 100)
        public string Val { get; set; } // Val
    }

    // TeamRelationship

    public class TeamRelationship
    {
        public int TeamRelationshipId { get; set; } // TeamRelationshipID (Primary key)
        public int UserId { get; set; } // UserID
        public int TeamMemberUserId { get; set; } // TeamMemberUserID
        public long TeamMemberUserRole { get; set; } // TeamMemberUserRole
        public int TeamMemberAssignmentOption { get; set; } // TeamMemberAssignmentOption

        public TeamRelationship()
        {
            UserId = 0;
            TeamMemberUserId = 0;
            TeamMemberUserRole = 0;
            TeamMemberAssignmentOption = 0;
        }
    }

    // TILDefault

    public class TilDefault
    {
        public int TilDefaultId { get; set; } // TILDefaultID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Name { get; set; } // Name (length: 50)
        public short TilStatus { get; set; } // TILStatus
        public bool DemandFeature { get; set; } // DemandFeature
        public string DemandFeatureDesc { get; set; } // DemandFeatureDesc (length: 255)
        public bool VariableRateFeature { get; set; } // VariableRateFeature
        public bool CollateralSecurity { get; set; } // CollateralSecurity
        public bool DepositAccounts { get; set; } // DepositAccounts
        public short AssumptionOption { get; set; } // AssumptionOption
        public decimal? FilingRecFees { get; set; } // FilingRecFees
        public bool HazardInsRequired { get; set; } // HazardInsRequired
        public short TilHazardInsOption { get; set; } // TILHazardInsOption
        public decimal? HazCoverageAmount { get; set; } // HazCoverageAmount
        public short HazardInsAvailFromLender { get; set; } // HazardInsAvailFromLender
        public decimal? HazardInsCost { get; set; } // HazardInsCost
        public int? HazardInsTerm { get; set; } // HazardInsTerm
        public int? LateChargeDays { get; set; } // LateChargeDays
        public decimal? LateChargePerc { get; set; } // LateChargePerc
        public short LateChargeBasis { get; set; } // LateChargeBasis
        public short LateChargeWording { get; set; } // LateChargeWording
        public string LateChargeCustomDesc { get; set; } // LateChargeCustomDesc (length: 255)
        public bool AprDoesNotIncReqDep { get; set; } // APRDoesNotIncReqDep
        public short PrepaymentPenaltyOption { get; set; } // PrepaymentPenaltyOption
        public short RefundFinanceCharge { get; set; } // RefundFinanceCharge

        public TilDefault()
        {
            DisplayOrder = 0;
            TilStatus = 0;
            DemandFeature = false;
            VariableRateFeature = false;
            CollateralSecurity = false;
            DepositAccounts = false;
            AssumptionOption = 0;
            HazardInsRequired = false;
            TilHazardInsOption = 0;
            HazardInsAvailFromLender = 0;
            LateChargeBasis = 0;
            LateChargeWording = 0;
            AprDoesNotIncReqDep = false;
            PrepaymentPenaltyOption = 0;
            RefundFinanceCharge = 0;
        }
    }

    // Trade

    public class Trade
    {
        public int TradeId { get; set; } // TradeID (Primary key)
        public System.Guid? TradeGuid { get; set; } // TradeGUID
        public string TradeNo { get; set; } // TradeNo (length: 50)
        public string InvestorCommitmentNo { get; set; } // InvestorCommitmentNo (length: 50)
        public decimal? OriginalTradeAmount { get; set; } // OriginalTradeAmount
        public decimal? CurrentTradeAmount { get; set; } // _CurrentTradeAmount
        public decimal? TolerancePerc { get; set; } // TolerancePerc
        public decimal? MaxOverdeliveryAmount { get; set; } // MaxOverdeliveryAmount
        public System.DateTime? CommitmentDate { get; set; } // CommitmentDate
        public System.DateTime? ExpirationDate { get; set; } // ExpirationDate
        public int PoolMonth { get; set; } // PoolMonth
        public int? PoolYear { get; set; } // PoolYear
        public int TradeStatus { get; set; } // TradeStatus
        public string Notes { get; set; } // Notes (length: 2147483647)
        public decimal? NoteRateLow { get; set; } // NoteRateLow
        public decimal? NoteRateHigh { get; set; } // NoteRateHigh
        public int? TermLow { get; set; } // TermLow
        public int? TermHigh { get; set; } // TermHigh
        public int AmortType { get; set; } // AmortType
        public string LoanProgramName { get; set; } // LoanProgramName (length: 50)
        public decimal? CouponRate { get; set; } // CouponRate
        public int CommitmentType { get; set; } // CommitmentType
        public decimal? ToleranceLowAmountOv { get; set; } // ToleranceLowAmountOV
        public decimal? ToleranceHighAmountOv { get; set; } // ToleranceHighAmountOV
        public string MasterCommitmentNo { get; set; } // MasterCommitmentNo (length: 50)
        public short FollowUpFlag { get; set; } // FollowUpFlag
        public System.DateTime? InvestorDueDate { get; set; } // InvestorDueDate
        public System.DateTime? ShipByDate { get; set; } // ShipByDate
        public string InvestorCode { get; set; } // InvestorCode (length: 50)
        public string PoolIdentifier { get; set; } // PoolIdentifier (length: 20)
        public string PoolIdentifierSuffix { get; set; } // PoolIdentifierSuffix (length: 10)
        public string SellerNo { get; set; } // SellerNo (length: 50)
        public string ServicerNo { get; set; } // ServicerNo (length: 50)
        public int MortgageType { get; set; } // MortgageType
        public System.DateTime? PoolIssueDate { get; set; } // PoolIssueDate
        public System.DateTime? SecurityTradeBookEntryDate { get; set; } // SecurityTradeBookEntryDate
        public decimal? PassThroughRate { get; set; } // PassThroughRate
        public string FannieArmPlanNo { get; set; } // FannieARMPlanNo (length: 10)
        public int? RemittanceDay { get; set; } // RemittanceDay
        public int PoolStructureType { get; set; } // PoolStructureType
        public string InvestorFeature1 { get; set; } // InvestorFeature1 (length: 3)
        public string InvestorFeature2 { get; set; } // InvestorFeature2 (length: 3)
        public string InvestorFeature3 { get; set; } // InvestorFeature3 (length: 3)
        public int InterestOnlyIndicator { get; set; } // InterestOnlyIndicator
        public int BalloonIndicator { get; set; } // BalloonIndicator
        public int AssumabilityIndicator { get; set; } // AssumabilityIndicator
        public int AccrualRateMethod { get; set; } // AccrualRateMethod
        public int? ArmLookbackDays { get; set; } // ARMLookbackDays
        public decimal? ArmMargin { get; set; } // ARMMargin
        public decimal? FixedServicingFeePercent { get; set; } // FixedServicingFeePercent
        public decimal? MinimumAccrualRate { get; set; } // MinimumAccrualRate
        public decimal? MaximumAccrualRate { get; set; } // MaximumAccrualRate
        public int ArmRounding { get; set; } // ARMRounding
        public int LoanDefaultLossPartyType { get; set; } // LoanDefaultLossPartyType
        public int ReoMarketingPartyType { get; set; } // REOMarketingPartyType
        public decimal? BaseGuarantyFeePercent { get; set; } // BaseGuarantyFeePercent
        public decimal? GuarantyFeeAfterApm { get; set; } // GuarantyFeeAfterAPM
        public int InvestorRemittanceType { get; set; } // InvestorRemittanceType
        public string DocumentCustodianIdentifier { get; set; } // DocumentCustodianIdentifier (length: 50)
        public string HedgeNo { get; set; } // HedgeNo (length: 50)
        public decimal? SecurityPrice { get; set; } // SecurityPrice
        public string DealerCode { get; set; } // DealerCode (length: 50)
        public int SecurityType { get; set; } // SecurityType
        public string SecurityTypeOther { get; set; } // SecurityTypeOther (length: 10)
        public int TransactionType { get; set; } // TransactionType
        public System.DateTime? NotificationDate { get; set; } // NotificationDate
        public System.DateTime? SettlementDate { get; set; } // SettlementDate
        public decimal? OptionPremiumPerc { get; set; } // OptionPremiumPerc
        public string OptionProduct { get; set; } // OptionProduct (length: 50)

        public Trade()
        {
            TradeGuid = System.Guid.NewGuid();
            PoolMonth = 0;
            TradeStatus = 0;
            AmortType = 0;
            CommitmentType = 0;
            FollowUpFlag = 0;
            MortgageType = 0;
            PoolStructureType = 0;
            InterestOnlyIndicator = 0;
            BalloonIndicator = 0;
            AssumabilityIndicator = 0;
            AccrualRateMethod = 0;
            ArmRounding = 0;
            LoanDefaultLossPartyType = 0;
            ReoMarketingPartyType = 0;
            InvestorRemittanceType = 0;
            SecurityType = 0;
            TransactionType = 0;
        }
    }

    // TradeDeliveryAdjustment

    public class TradeDeliveryAdjustment
    {
        public int TradeDeliveryAdjustmentId { get; set; } // TradeDeliveryAdjustmentID (Primary key)
        public int TradeId { get; set; } // TradeID
        public string Description { get; set; } // Description (length: 50)
        public System.DateTime? DeliveryDate { get; set; } // DeliveryDate
        public decimal? AdjustmentPrice { get; set; } // AdjustmentPrice
        public int DisplayOrder { get; set; } // DisplayOrder

        public TradeDeliveryAdjustment()
        {
            TradeId = 0;
            DisplayOrder = 0;
        }
    }

    // TradeDocument

    public class TradeDocument
    {
        public int TradeDocumentId { get; set; } // TradeDocumentID (Primary key)
        public int TradeId { get; set; } // TradeID
        public int DisplayOrder { get; set; } // DisplayOrder
        public string Description { get; set; } // Description (length: 255)
        public string FileExtension { get; set; } // FileExtension (length: 10)
        public System.DateTime DateCreated { get; set; } // DateCreated
        public System.Guid? Guid { get; set; } // GUID
        public byte[] Data { get; set; } // Data (length: 2147483647)

        public TradeDocument()
        {
            TradeId = 0;
            DisplayOrder = 0;
            Guid = System.Guid.NewGuid();
        }
    }

    // TradeFee

    public class TradeFee
    {
        public int TradeFeeId { get; set; } // TradeFeeID (Primary key)
        public int TradeId { get; set; } // TradeID
        public System.DateTime? FeeDate { get; set; } // FeeDate
        public int FeeType { get; set; } // FeeType
        public decimal? FeePerc { get; set; } // FeePerc
        public decimal? FeeAmount { get; set; } // FeeAmount
        public string Notes { get; set; } // Notes (length: 1000)
        public decimal? PairOffAmount { get; set; } // PairOffAmount

        public TradeFee()
        {
            TradeId = 0;
            FeeType = 0;
        }
    }

    // TradePrice

    public class TradePrice
    {
        public int TradePriceId { get; set; } // TradePriceID (Primary key)
        public int TradeId { get; set; } // TradeID
        public decimal? NoteRate { get; set; } // NoteRate
        public decimal? BasePrice { get; set; } // BasePrice

        public TradePrice()
        {
            TradeId = 0;
        }
    }

    // Transmittal

    public class Transmittal
    {
        public int TransmittalId { get; set; } // TransmittalID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public short IsPropType1UnitOv { get; set; } // IsPropType1UnitOV
        public short IsPropType2To4UnitsOv { get; set; } // IsPropType2To4UnitsOV
        public short IsPropTypeCondoOv { get; set; } // IsPropTypeCondoOV
        public short IsPropTypePudov { get; set; } // IsPropTypePUDOV
        public short IsPropTypeCoopOv { get; set; } // IsPropTypeCoopOV
        public short IsPropTypeManuOv { get; set; } // IsPropTypeManuOV
        public short IsPropTypeSinglewideOv { get; set; } // IsPropTypeSinglewideOV
        public short IsPropTypeMultiwideOv { get; set; } // IsPropTypeMultiwideOV
        public short AmortTypeOv { get; set; } // AmortTypeOV
        public string AmortTypeDescOv { get; set; } // AmortTypeDescOV (length: 50)
        public short LoanPurposeOv { get; set; } // LoanPurposeOV
        public short OriginatorType { get; set; } // OriginatorType
        public string BrokerName { get; set; } // BrokerName (length: 100)
        public decimal? GrossIncomeBorOv { get; set; } // GrossIncomeBorOV
        public decimal? GrossIncomeCoBorsOv { get; set; } // GrossIncomeCoBorsOV
        public decimal? GrossIncomeTotalOv { get; set; } // GrossIncomeTotalOV
        public decimal? OtherIncomeBorOv { get; set; } // OtherIncomeBorOV
        public decimal? OtherIncomeCoBorsOv { get; set; } // OtherIncomeCoBorsOV
        public decimal? OtherIncomeTotalOv { get; set; } // OtherIncomeTotalOV
        public decimal? CashFlowIncomeBorOv { get; set; } // CashFlowIncomeBorOV
        public decimal? CashFlowIncomeCoBorsOv { get; set; } // CashFlowIncomeCoBorsOV
        public decimal? CashFlowIncomeTotalOv { get; set; } // CashFlowIncomeTotalOV
        public decimal? TotalIncomeBorOv { get; set; } // TotalIncomeBorOV
        public decimal? TotalIncomeCoBorsOv { get; set; } // TotalIncomeCoBorsOV
        public decimal? TotalIncomeTotalOv { get; set; } // TotalIncomeTotalOV
        public decimal? FirstRatioOv { get; set; } // FirstRatioOV
        public decimal? SecondRatioOv { get; set; } // SecondRatioOV
        public decimal? GapRatioOv { get; set; } // GapRatioOV
        public decimal? Ltvov { get; set; } // LTVOV
        public decimal? Cltvov { get; set; } // CLTVOV
        public decimal? Hcltvov { get; set; } // HCLTVOV
        public short AppraisalType { get; set; } // AppraisalType
        public string AppraisalFormNo { get; set; } // AppraisalFormNo (length: 50)
        public short RiskAssessmentMethod { get; set; } // RiskAssessmentMethod
        public string RiskAssessmentMethodOther { get; set; } // RiskAssessmentMethodOther (length: 50)
        public string AusRecommendation { get; set; } // AUSRecommendation (length: 50)
        public string AusFileId { get; set; } // AUSFileID (length: 50)
        public string DocumentClassification { get; set; } // DocumentClassification (length: 50)
        public int? RepCreditScore { get; set; } // RepCreditScore
        public decimal? PrimaryResFirstMortPiov { get; set; } // PrimaryResFirstMortPIOV
        public decimal? PrimaryResSecondMortPiov { get; set; } // PrimaryResSecondMortPIOV
        public decimal? PrimaryResHazOv { get; set; } // PrimaryResHazOV
        public decimal? PrimaryResPropTaxesOv { get; set; } // PrimaryResPropTaxesOV
        public decimal? PrimaryResMiov { get; set; } // PrimaryResMIOV
        public decimal? PrimaryResHodov { get; set; } // PrimaryResHODOV
        public decimal? PrimaryResLeaseOv { get; set; } // PrimaryResLeaseOV
        public decimal? PrimaryResOtherHousingExpOv { get; set; } // PrimaryResOtherHousingExpOV
        public decimal? PrimaryResPitiov { get; set; } // PrimaryResPITIOV
        public decimal? SubPropNetCashFlowNegOv { get; set; } // SubPropNetCashFlowNegOV
        public decimal? AllOtherPaymentsOv { get; set; } // AllOtherPaymentsOV
        public decimal? TotalAllPaymentsOv { get; set; } // TotalAllPaymentsOV
        public decimal? FundsReqToCloseOv { get; set; } // FundsReqToCloseOV
        public decimal? VerifiedAssetsOv { get; set; } // VerifiedAssetsOV
        public int? MonthsInReserveOv { get; set; } // MonthsInReserveOV
        public decimal? SalesConcessionsPercOv { get; set; } // SalesConcessionsPercOV
        public string UwCom { get; set; } // UWCom (length: 2147483647)
        public short QualRateOption { get; set; } // QualRateOption
        public string SellerNo { get; set; } // SellerNo (length: 50)
        public string InvestorLoanNo { get; set; } // InvestorLoanNo (length: 50)
        public string SellerLoanNo { get; set; } // SellerLoanNo (length: 50)
        public string MasterCommitmentNo { get; set; } // MasterCommitmentNo (length: 50)
        public string ContractNo { get; set; } // ContractNo (length: 50)
        public System.DateTime? ContractSignatureDate { get; set; } // ContractSignatureDate
        public int? RequiredMonthsInResOv { get; set; } // RequiredMonthsInResOV
        public string SpecialFeatureCode01 { get; set; } // SpecialFeatureCode01 (length: 3)
        public string SpecialFeatureCode02 { get; set; } // SpecialFeatureCode02 (length: 3)
        public string SpecialFeatureCode03 { get; set; } // SpecialFeatureCode03 (length: 3)
        public string SpecialFeatureCode04 { get; set; } // SpecialFeatureCode04 (length: 3)
        public string SpecialFeatureCode05 { get; set; } // SpecialFeatureCode05 (length: 3)
        public string SpecialFeatureCode06 { get; set; } // SpecialFeatureCode06 (length: 3)
        public string SpecialFeatureCode07 { get; set; } // SpecialFeatureCode07 (length: 3)
        public string SpecialFeatureCode08 { get; set; } // SpecialFeatureCode08 (length: 3)
        public string SpecialFeatureCode09 { get; set; } // SpecialFeatureCode09 (length: 3)
        public string SpecialFeatureCode10 { get; set; } // SpecialFeatureCode10 (length: 3)
        public int? MonthsInReserve { get; set; } // _MonthsInReserve
        public bool UseCommentsAddendum { get; set; } // UseCommentsAddendum
        public decimal? InterestedPartyContributions { get; set; } // InterestedPartyContributions
        public decimal? IncomeNetRentalOv { get; set; } // IncomeNetRentalOV
        public decimal? SubPropFirstMortPiov { get; set; } // SubPropFirstMortPIOV
        public decimal? SubPropOtherFinancingPiov { get; set; } // SubPropOtherFinancingPIOV
        public decimal? SubPropOtherPaymentOv { get; set; } // SubPropOtherPaymentOV

        public Transmittal()
        {
            FileDataId = 0;
            IsPropType1UnitOv = 0;
            IsPropType2To4UnitsOv = 0;
            IsPropTypeCondoOv = 0;
            IsPropTypePudov = 0;
            IsPropTypeCoopOv = 0;
            IsPropTypeManuOv = 0;
            IsPropTypeSinglewideOv = 0;
            IsPropTypeMultiwideOv = 0;
            AmortTypeOv = 0;
            LoanPurposeOv = 0;
            OriginatorType = 0;
            AppraisalType = 0;
            RiskAssessmentMethod = 0;
            QualRateOption = 0;
            UseCommentsAddendum = false;
        }
    }

    // TrustAccountItem

    public class TrustAccountItem
    {
        public int TrustAccountItemId { get; set; } // TrustAccountItemID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int? CheckNo { get; set; } // CheckNo
        public System.DateTime? CheckDate { get; set; } // CheckDate
        public string Description { get; set; } // Description (length: 100)
        public decimal? Payment { get; set; } // Payment
        public decimal? Deposit { get; set; } // Deposit

        public TrustAccountItem()
        {
            FileDataId = 0;
        }
    }

    // TXForms

    public class TxForm
    {
        public int TxFormsId { get; set; } // TXFormsID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public string BrokerOrLo { get; set; } // BrokerOrLO (length: 150)
        public bool SubmitToLender { get; set; } // SubmitToLender
        public bool IndependentContractor { get; set; } // IndependentContractor
        public bool ActingAsFollows { get; set; } // ActingAsFollows
        public string ActingAsFollowsText { get; set; } // ActingAsFollowsText (length: 255)
        public bool RetailPrice { get; set; } // RetailPrice
        public bool WholesaleOptions { get; set; } // WholesaleOptions
        public decimal? FeeAmount { get; set; } // FeeAmount
        public decimal? ApplicationFee { get; set; } // ApplicationFee
        public decimal? ProcessingFee { get; set; } // ProcessingFee
        public decimal? AppraisalFee { get; set; } // AppraisalFee
        public decimal? CreditReportFee { get; set; } // CreditReportFee
        public decimal? AuFee { get; set; } // AUFee
        public string OtherFee1Desc { get; set; } // OtherFee1Desc (length: 50)
        public decimal? OtherFee1Amount { get; set; } // OtherFee1Amount
        public string OtherFee2Desc { get; set; } // OtherFee2Desc (length: 50)
        public decimal? OtherFee2Amount { get; set; } // OtherFee2Amount
        public decimal? NonRefundableAmount { get; set; } // NonRefundableAmount
        public string LicensedEntity { get; set; } // LicensedEntity (length: 150)
        public string LicenseNo { get; set; } // LicenseNo (length: 50)
        public bool PricingBasedOnCustomFlag { get; set; } // PricingBasedOnCustomFlag
        public string PricingBasedOnCustomDesc { get; set; } // PricingBasedOnCustomDesc (length: 200)
        public bool EstFeesShownOnGfeFlag { get; set; } // EstFeesShownOnGFEFlag

        public TxForm()
        {
            FileDataId = 0;
            SubmitToLender = false;
            IndependentContractor = false;
            ActingAsFollows = false;
            RetailPrice = false;
            WholesaleOptions = false;
            PricingBasedOnCustomFlag = false;
            EstFeesShownOnGfeFlag = false;
        }
    }

    // UpdateAction

    public class UpdateAction
    {
        public string UpdateActionName { get; set; } // UpdateActionName (Primary key) (length: 50)
    }

    // User

    public class User
    {
        public int UserId { get; set; } // UserID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string UserName { get; set; } // UserName (length: 50)
        public bool Disabled { get; set; } // Disabled
        public string EncryptedPassword { get; set; } // EncryptedPassword (length: 88)
        public string FirstName { get; set; } // FirstName (length: 50)
        public string MiddleName { get; set; } // MiddleName (length: 50)
        public string LastName { get; set; } // LastName (length: 50)
        public string Title { get; set; } // Title (length: 50)
        public string EMail { get; set; } // EMail (length: 250)
        public string WorkPhone { get; set; } // WorkPhone (length: 20)
        public string HomePhone { get; set; } // HomePhone (length: 20)
        public string MobilePhone { get; set; } // MobilePhone (length: 20)
        public string OtherPhone { get; set; } // OtherPhone (length: 20)
        public string Pager { get; set; } // Pager (length: 20)
        public string Fax { get; set; } // Fax (length: 20)
        public string LicenseNo { get; set; } // LicenseNo (length: 50)
        public int OtherFlags { get; set; } // OtherFlags
        public string DomainNameOv { get; set; } // DomainNameOV (length: 260)
        public string Nmlsid { get; set; } // NMLSID (length: 50)
        public string MobilePhoneSmsGateway { get; set; } // MobilePhoneSMSGateway (length: 40)
        public string PpeUserName { get; set; } // PPEUserName (length: 50)
        public string CustomData { get; set; } // CustomData (length: 500)
        public bool ChangePasswordAtLogon { get; set; } // ChangePasswordAtLogon
        public string Notes { get; set; } // Notes (length: 2147483647)
        public bool PasswordNeverExpires { get; set; } // PasswordNeverExpires
        public System.DateTime? StartDate { get; set; } // StartDate
        public System.DateTime? TerminationDate { get; set; } // TerminationDate
        public bool OverrideWindowsAuthToUseStandardAuth { get; set; } // OverrideWindowsAuthToUseStandardAuth

        public User()
        {
            DisplayOrder = 0;
            Disabled = false;
            OtherFlags = 0;
            ChangePasswordAtLogon = false;
            PasswordNeverExpires = false;
            OverrideWindowsAuthToUseStandardAuth = false;
        }
    }

    // UserCompPlan

    public class UserCompPlan
    {
        public int UserCompPlanId { get; set; } // UserCompPlanID (Primary key)
        public int UserId { get; set; } // UserID
        public int CompPlanId { get; set; } // CompPlanID
        public System.DateTime? StartDate { get; set; } // StartDate
        public System.DateTime? EndDate { get; set; } // EndDate

        public UserCompPlan()
        {
            UserId = 0;
            CompPlanId = 0;
        }
    }

    // UserImage

    public class UserImage
    {
        public int UserImageId { get; set; } // UserImageID (Primary key)
        public int UserId { get; set; } // UserID
        public int ImageType { get; set; } // ImageType
        public byte[] ImageData { get; set; } // ImageData (length: 2147483647)

        public UserImage()
        {
            UserId = 0;
            ImageType = 0;
        }
    }

    // UserLicensedProduct

    public class UserLicensedProduct
    {
        public int UserLicensedProductId { get; set; } // UserLicensedProductID (Primary key)
        public int UserId { get; set; } // UserID
        public int LicensedProductId { get; set; } // LicensedProductID

        public UserLicensedProduct()
        {
            UserId = 0;
            LicensedProductId = 0;
        }
    }

    // UserOrgProfile

    public class UserOrgProfile
    {
        public int UserOrgProfileId { get; set; } // UserOrgProfileID (Primary key)
        public int UserId { get; set; } // UserID
        public int OrganizationId { get; set; } // OrganizationID
        public int SecurityProfileId { get; set; } // SecurityProfileID

        public UserOrgProfile()
        {
            UserId = 0;
            OrganizationId = 0;
            SecurityProfileId = 0;
        }
    }

    // UserPasswordHistory

    public class UserPasswordHistory
    {
        public int UserPasswordHistoryId { get; set; } // UserPasswordHistoryID (Primary key)
        public int? UserId { get; set; } // UserID
        public string EncryptedPassword { get; set; } // EncryptedPassword (length: 88)
        public System.DateTime? DateCreated { get; set; } // DateCreated
    }

    // UserRoleItem

    public class UserRoleItem
    {
        public int UserRoleItemId { get; set; } // UserRoleItemID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public long Role { get; set; } // Role
        public string NameOv { get; set; } // NameOV (length: 50)
        public bool Visible { get; set; } // Visible
        public bool VisibleInPipeline { get; set; } // VisibleInPipeline
        public string Abbreviation { get; set; } // Abbreviation (length: 4)

        public UserRoleItem()
        {
            DisplayOrder = 0;
            Role = 0;
            Visible = false;
            VisibleInPipeline = false;
        }
    }

    // UserStateLicense

    public class UserStateLicense
    {
        public int UserStateLicenseId { get; set; } // UserStateLicenseID (Primary key)
        public int UserId { get; set; } // UserID
        public string State { get; set; } // State (length: 2)
        public string LicenseNo { get; set; } // LicenseNo (length: 50)
        public System.DateTime? ExpirationDate { get; set; } // ExpirationDate
        public System.DateTime? StartDate { get; set; } // StartDate

        public UserStateLicense()
        {
            UserId = 0;
        }
    }

    // VA

    public class Va
    {
        public int Vaid { get; set; } // VAID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public int? VeteranBorrowerId { get; set; } // VeteranBorrowerID
        public short VaTitleVested { get; set; } // VATitleVested
        public string VaTitleVestedOtherDesc { get; set; } // VATitleVestedOtherDesc (length: 30)
        public System.DateTime? DateOfApproval { get; set; } // DateOfApproval
        public System.DateTime? DateApprovalExpires { get; set; } // DateApprovalExpires
        public short HasVaDebt { get; set; } // HasVADebt
        public decimal? CashFromVeteran { get; set; } // CashFromVeteran
        public decimal? IrrrccPlusPpov { get; set; } // IRRRCCPlusPPOV
        public decimal? Maintenance { get; set; } // Maintenance
        public decimal? Utilities { get; set; } // Utilities
        public decimal? DownPaymentOv { get; set; } // DownPaymentOV
        public decimal? LiquidAssetsOv { get; set; } // LiquidAssetsOV
        public short UtilitiesIncludedInPresPiti { get; set; } // UtilitiesIncludedInPresPITI
        public decimal? SpecialAssessments { get; set; } // SpecialAssessments
        public short PastCreditRecord { get; set; } // PastCreditRecord
        public short MeetCreditStandards { get; set; } // MeetCreditStandards
        public string LoanAnalysisRemarks { get; set; } // LoanAnalysisRemarks (length: 1000)
        public string Line36OtherDeductionsDesc { get; set; } // Line36OtherDeductionsDesc (length: 50)
        public string Line39OtherIncomesDesc { get; set; } // Line39OtherIncomesDesc (length: 50)
        public decimal? Line44SupportBalanceOv { get; set; } // Line44SupportBalanceOV
        public short HaveFiledClaim { get; set; } // HaveFiledClaim
        public string CertMailingStreet { get; set; } // CertMailingStreet (length: 50)
        public string CertMailingCity { get; set; } // CertMailingCity (length: 50)
        public string CertMailingState { get; set; } // CertMailingState (length: 2)
        public string CertMailingZip { get; set; } // CertMailingZip (length: 10)
        public short VeteranHasDisability { get; set; } // VeteranHasDisability
        public System.DateTime? LoanDisburementReportDate { get; set; } // LoanDisburementReportDate
        public string RelativeName { get; set; } // RelativeName (length: 100)
        public string RelativeStreet { get; set; } // RelativeStreet (length: 50)
        public string RelativeCity { get; set; } // RelativeCity (length: 50)
        public string RelativeState { get; set; } // RelativeState (length: 2)
        public string RelativeZip { get; set; } // RelativeZip (length: 10)
        public string RelativePhone { get; set; } // RelativePhone (length: 20)
        public short IssuanceOfEvidence { get; set; } // IssuanceOfEvidence
        public System.DateTime? FullyPaidOutDate { get; set; } // FullyPaidOutDate
        public string LienTypeOther { get; set; } // LienTypeOther (length: 50)
        public decimal? UnpaidSpecialAssessments { get; set; } // UnpaidSpecialAssessments
        public decimal? AnnualMaintenanceAssessment { get; set; } // AnnualMaintenanceAssessment
        public string NonRealtyAcquired { get; set; } // NonRealtyAcquired (length: 200)
        public string AdditionalSecurity { get; set; } // AdditionalSecurity (length: 200)
        public short WithheldAmountDepositType { get; set; } // WithheldAmountDepositType
        public bool ConstructionCompletedProperly { get; set; } // ConstructionCompletedProperly
        public string ApprovedUnderwriter { get; set; } // ApprovedUnderwriter (length: 150)
        public bool VetHasNotBeenDischarged { get; set; } // VetHasNotBeenDischarged
        public bool OmitGovInfoFromLoanDisb { get; set; } // OmitGovInfoFromLoanDisb
        public decimal? ApproxAnnualAssessmentOv { get; set; } // ApproxAnnualAssessmentOV
        public short LienTypeOv { get; set; } // LienTypeOV
        public decimal? AmountWithheldFromProceeds { get; set; } // AmountWithheldFromProceeds
        public decimal? ApproxAnnualRealEstateTaxesOv { get; set; } // ApproxAnnualRealEstateTaxesOV
        public decimal? PreviousLoanAmount { get; set; } // PreviousLoanAmount
        public int? PreviousTerm { get; set; } // PreviousTerm
        public decimal? PreviousMonthlyPi { get; set; } // PreviousMonthlyPI
        public decimal? PreviousIntRate { get; set; } // PreviousIntRate
        public short ShowLendersCert { get; set; } // ShowLendersCert
        public string PreviousVaLoanNo { get; set; } // PreviousVALoanNo (length: 50)
        public System.DateTime? PrevLoanClosed { get; set; } // PrevLoanClosed
        public decimal? PreviousMonthlyPiti { get; set; } // PreviousMonthlyPITI
        public int ActiveDutyDayFollowingSeperation { get; set; } // ActiveDutyDayFollowingSeperation
        public string VeteranStatusFileRef { get; set; } // VeteranStatusFileRef (length: 200)
        public bool ServedUnderAnotherName { get; set; } // ServedUnderAnotherName
        public string OtherNamesUsedDuringMilitaryService { get; set; } // OtherNamesUsedDuringMilitaryService (length: 200)
        public bool BorrowerHadPreviousVaLoan { get; set; } // BorrowerHadPreviousVALoan
        public int PrevLoanMoreThan30DaysLateInPast6Mo { get; set; } // PrevLoanMoreThan30DaysLateInPast6Mo
        public int RecommendedAction { get; set; } // RecommendedAction
        public int FinalAction { get; set; } // FinalAction
        public decimal? CrvValue { get; set; } // CRVValue
        public System.DateTime? CrvExpirationDate { get; set; } // CRVExpirationDate
        public int? CrvEconomicLife { get; set; } // CRVEconomicLife
        public byte PreviousLoanType { get; set; } // PreviousLoanType
        public string PreviousLoanTypeOtherDesc { get; set; } // PreviousLoanTypeOtherDesc (length: 50)
        public decimal? PreviousTotalOfPiAndMiPayments { get; set; } // PreviousTotalOfPIAndMIPayments
        public bool NtbEliminatesMi { get; set; } // NTBEliminatesMI
        public bool NtbIncreasesResidualIncome { get; set; } // NTBIncreasesResidualIncome
        public bool NtbRefinancesConstLoan { get; set; } // NTBRefinancesConstLoan
        public int VaCashOutRefiType { get; set; } // VACashOutRefiType

        public Va()
        {
            FileDataId = 0;
            VaTitleVested = 0;
            HasVaDebt = 0;
            UtilitiesIncludedInPresPiti = 0;
            PastCreditRecord = 0;
            MeetCreditStandards = 0;
            HaveFiledClaim = 0;
            VeteranHasDisability = 0;
            IssuanceOfEvidence = 0;
            WithheldAmountDepositType = 0;
            ConstructionCompletedProperly = false;
            VetHasNotBeenDischarged = false;
            OmitGovInfoFromLoanDisb = false;
            LienTypeOv = 0;
            ShowLendersCert = 0;
            ActiveDutyDayFollowingSeperation = 0;
            ServedUnderAnotherName = false;
            BorrowerHadPreviousVaLoan = false;
            PrevLoanMoreThan30DaysLateInPast6Mo = 0;
            RecommendedAction = 0;
            FinalAction = 0;
            PreviousLoanType = 0;
            NtbEliminatesMi = false;
            NtbIncreasesResidualIncome = false;
            NtbRefinancesConstLoan = false;
            VaCashOutRefiType = 0;
        }
    }

    // VAAuthorizedAgent

    public class VaAuthorizedAgent
    {
        public int VaAuthorizedAgentId { get; set; } // VAAuthorizedAgentID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public string VaAuthorizedAgentName { get; set; } // VAAuthorizedAgentName (length: 100)
        public string Street { get; set; } // Street (length: 50)
        public string City { get; set; } // City (length: 50)
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 10)
        public string VaFunction { get; set; } // VAFunction (length: 100)

        public VaAuthorizedAgent()
        {
            FileDataId = 0;
        }
    }

    // VAFundingFee

    public class VaFundingFee
    {
        public int VaFundingFeeId { get; set; } // VAFundingFeeID (Primary key)
        public decimal? PurRegularFirstUse100 { get; set; } // PurRegularFirstUse100
        public decimal? PurRegularFirstUse95 { get; set; } // PurRegularFirstUse95
        public decimal? PurRegularFirstUse90 { get; set; } // PurRegularFirstUse90
        public decimal? PurRegularNextUse100 { get; set; } // PurRegularNextUse100
        public decimal? PurRegularNextUse95 { get; set; } // PurRegularNextUse95
        public decimal? PurRegularNextUse90 { get; set; } // PurRegularNextUse90
        public decimal? PurResNgFirstUse100 { get; set; } // PurResNGFirstUse100
        public decimal? PurResNgFirstUse95 { get; set; } // PurResNGFirstUse95
        public decimal? PurResNgFirstUse90 { get; set; } // PurResNGFirstUse90
        public decimal? PurResNgNextUse100 { get; set; } // PurResNGNextUse100
        public decimal? PurResNgNextUse95 { get; set; } // PurResNGNextUse95
        public decimal? PurResNgNextUse90 { get; set; } // PurResNGNextUse90
        public decimal? RefiRegularFirstUse { get; set; } // RefiRegularFirstUse
        public decimal? RefiRegularNextUse { get; set; } // RefiRegularNextUse
        public decimal? RefiResNgFirstUse { get; set; } // RefiResNGFirstUse
        public decimal? RefiResNgNextUse { get; set; } // RefiResNGNextUse
        public decimal? Irr { get; set; } // IRR
        public decimal? Manufactured { get; set; } // Manufactured
        public decimal? LoanAssumptions { get; set; } // LoanAssumptions
        public decimal? RefiRegularFirstUse100 { get; set; } // RefiRegularFirstUse100
        public decimal? RefiRegularNextUse100 { get; set; } // RefiRegularNextUse100
        public decimal? RefiResNgFirstUse100 { get; set; } // RefiResNGFirstUse100
        public decimal? RefiResNgNextUse100 { get; set; } // RefiResNGNextUse100
    }

    // ValidationRule

    public class ValidationRule
    {
        public int ValidationRuleId { get; set; } // ValidationRuleID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public string ErrorMessage { get; set; } // ErrorMessage (length: 1000)
        public string Condition { get; set; } // Condition (length: 6000)
        public short ValidationRuleType { get; set; } // ValidationRuleType
        public bool Disabled { get; set; } // Disabled
        public bool AppliesToAllStatuses { get; set; } // AppliesToAllStatuses
        public long StatusFlags { get; set; } // StatusFlags
        public short DocumentRuleActionType { get; set; } // DocumentRuleActionType
        public short LoanReportingOption { get; set; } // LoanReportingOption
        public string ScreenToInvoke { get; set; } // ScreenToInvoke (length: 100)
        public string Code { get; set; } // Code (length: 20)
        public int ValidationRuleConditionType { get; set; } // ValidationRuleConditionType
        public string JScriptCondition { get; set; } // JScriptCondition (length: 2147483647)

        public ValidationRule()
        {
            DisplayOrder = 0;
            ValidationRuleType = 0;
            Disabled = false;
            AppliesToAllStatuses = false;
            StatusFlags = 0;
            DocumentRuleActionType = 0;
            LoanReportingOption = 0;
            ValidationRuleConditionType = 0;
        }
    }

    // ValidationRuleDocument

    public class ValidationRuleDocument
    {
        public int ValidationRuleDocumentId { get; set; } // ValidationRuleDocumentID (Primary key)
        public int ValidationRuleId { get; set; } // ValidationRuleID
        public int DisplayOrder { get; set; } // DisplayOrder
        public short DocumentType { get; set; } // DocumentType
        public int ReportType { get; set; } // ReportType
        public string DocumentName { get; set; } // DocumentName (length: 100)
        public System.Guid? ReportGuid { get; set; } // ReportGUID

        public ValidationRuleDocument()
        {
            ValidationRuleId = 0;
            DisplayOrder = 0;
            DocumentType = 0;
            ReportType = 0;
        }
    }

    // ValidationRuleScreen

    public class ValidationRuleScreen
    {
        public int ValidationRuleScreenId { get; set; } // ValidationRuleScreenID (Primary key)
        public int ValidationRuleId { get; set; } // ValidationRuleID
        public int DisplayOrder { get; set; } // DisplayOrder
        public string PageClass { get; set; } // PageClass (length: 100)

        public ValidationRuleScreen()
        {
            ValidationRuleId = 0;
            DisplayOrder = 0;
        }
    }

    // VALoanSummary

    public class VaLoanSummary
    {
        public int VaLoanSummaryId { get; set; } // VALoanSummaryID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public string EntitlementCode { get; set; } // EntitlementCode (length: 50)
        public decimal? EntitlementAmount { get; set; } // EntitlementAmount
        public short VaServiceBranch { get; set; } // VAServiceBranch
        public short VaMilitaryStatus { get; set; } // VAMilitaryStatus
        public short VaLoanProcedure { get; set; } // VALoanProcedure
        public short VaLoanPurpose { get; set; } // VALoanPurpose
        public short VaLoanCode { get; set; } // VALoanCode
        public short VaMortgageType { get; set; } // VAMortgageType
        public short VaHybridArmType { get; set; } // VAHybridARMType
        public short VaOwnershipType { get; set; } // VAOwnershipType
        public decimal? EnergyImpAmount { get; set; } // EnergyImpAmount
        public bool EnergyImpNone { get; set; } // EnergyImpNone
        public bool EnergyImpSolar { get; set; } // EnergyImpSolar
        public bool EnergyImpReplacement { get; set; } // EnergyImpReplacement
        public bool EnergyImpAddition { get; set; } // EnergyImpAddition
        public bool EnergyImpInsulation { get; set; } // EnergyImpInsulation
        public bool EnergyImpOther { get; set; } // EnergyImpOther
        public short VaPropertyType { get; set; } // VAPropertyType
        public short VaAppraisalType { get; set; } // VAAppraisalType
        public short VaStructureType { get; set; } // VAStructureType
        public short VaPropertyDesignation { get; set; } // VAPropertyDesignation
        public string McrvNo { get; set; } // MCRVNo (length: 50)
        public short VaManufacturedHomeType { get; set; } // VAManufacturedHomeType
        public string LenderSarid { get; set; } // LenderSARID (length: 50)
        public System.DateTime? SarIssueDate { get; set; } // SARIssueDate
        public short AdjustedBySar { get; set; } // AdjustedBySAR
        public short ProcessedWithAus { get; set; } // ProcessedWithAUS
        public short VaausSystem { get; set; } // VAAUSSystem
        public short VaRiskClassification { get; set; } // VARiskClassification
        public int? VaMedianCreditScore { get; set; } // VAMedianCreditScore
        public decimal? ResidualIncomeGuideline { get; set; } // ResidualIncomeGuideline
        public short ConsiderSpouseIncome { get; set; } // ConsiderSpouseIncome
        public decimal? SpouseIncome { get; set; } // SpouseIncome
        public short TotalDiscountOption { get; set; } // TotalDiscountOption
        public short VeteranDiscountOption { get; set; } // VeteranDiscountOption
        public short VaFundingFeeExempt { get; set; } // VAFundingFeeExempt
        public string OriginalVaLoanNo { get; set; } // OriginalVALoanNo (length: 50)
        public decimal? OriginalLoanAmount { get; set; } // OriginalLoanAmount
        public decimal? OriginalIntRate { get; set; } // OriginalIntRate
        public string VaLoanSummaryRemarks { get; set; } // VALoanSummaryRemarks (length: 1000)
        public decimal? VaDiscountPoints { get; set; } // VADiscountPoints
        public decimal? VaDiscountAmount { get; set; } // VADiscountAmount
        public decimal? VaDiscountPointVeteran { get; set; } // VADiscountPointVeteran
        public decimal? VaDiscountAmountVeteran { get; set; } // VADiscountAmountVeteran
        public short VaPriorLoanType { get; set; } // VAPriorLoanType

        public VaLoanSummary()
        {
            FileDataId = 0;
            VaServiceBranch = 0;
            VaMilitaryStatus = 0;
            VaLoanProcedure = 0;
            VaLoanPurpose = 0;
            VaLoanCode = 0;
            VaMortgageType = 0;
            VaHybridArmType = 0;
            VaOwnershipType = 0;
            EnergyImpNone = false;
            EnergyImpSolar = false;
            EnergyImpReplacement = false;
            EnergyImpAddition = false;
            EnergyImpInsulation = false;
            EnergyImpOther = false;
            VaPropertyType = 0;
            VaAppraisalType = 0;
            VaStructureType = 0;
            VaPropertyDesignation = 0;
            VaManufacturedHomeType = 0;
            AdjustedBySar = 0;
            ProcessedWithAus = 0;
            VaausSystem = 0;
            VaRiskClassification = 0;
            ConsiderSpouseIncome = 0;
            TotalDiscountOption = 0;
            VeteranDiscountOption = 0;
            VaFundingFeeExempt = 0;
            VaPriorLoanType = 0;
        }
    }

    // VAPreviousLoan

    public class VaPreviousLoan
    {
        public int VaPreviousLoanId { get; set; } // VAPreviousLoanID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public short VaPreviousLoanType { get; set; } // VAPreviousLoanType
        public string Street { get; set; } // Street (length: 50)
        public string City { get; set; } // City (length: 50)
        public string State { get; set; } // State (length: 2)
        public string Zip { get; set; } // Zip (length: 10)
        public System.DateTime? DateOfLoan { get; set; } // DateOfLoan
        public short PropertyStillOwned { get; set; } // PropertyStillOwned
        public System.DateTime? DateOfSale { get; set; } // DateOfSale
        public string VaLoanNo { get; set; } // VALoanNo (length: 50)

        public VaPreviousLoan()
        {
            FileDataId = 0;
            VaPreviousLoanType = 0;
            PropertyStillOwned = 0;
        }
    }

    // VAServiceData

    public class VaServiceData
    {
        public int VaServiceDataId { get; set; } // VAServiceDataID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public System.DateTime? ActiveServiceDateFrom { get; set; } // ActiveServiceDateFrom
        public System.DateTime? ActiveServiceDateTo { get; set; } // ActiveServiceDateTo
        public string VeteransName { get; set; } // VeteransName (length: 100)
        public string ServiceNo { get; set; } // ServiceNo (length: 50)
        public string ServiceBranch { get; set; } // ServiceBranch (length: 50)
        public short MilitaryServiceType { get; set; } // MilitaryServiceType
        public short MilitaryOfficerOrEnlisted { get; set; } // MilitaryOfficerOrEnlisted

        public VaServiceData()
        {
            FileDataId = 0;
            MilitaryServiceType = 0;
            MilitaryOfficerOrEnlisted = 0;
        }
    }

    // VAValue

    public class VaValue
    {
        public int VaValueId { get; set; } // VAValueID (Primary key)
        public int FileDataId { get; set; } // FileDataID
        public string TitleLimits { get; set; } // TitleLimits (length: 190)
        public string FirmName { get; set; } // FirmName (length: 50)
        public string FirmStreet { get; set; } // FirmStreet (length: 50)
        public string FirmCityStateZip { get; set; } // FirmCityStateZip (length: 50)
        public string FirmComments { get; set; } // FirmComments (length: 200)
        public string LotDimensions { get; set; } // LotDimensions (length: 50)
        public int? LotSqFootage { get; set; } // LotSqFootage
        public decimal? LotAcreage { get; set; } // LotAcreage
        public bool IsIrregular { get; set; } // IsIrregular
        public bool IsAcres { get; set; } // IsAcres
        public short ElectricUtil { get; set; } // ElectricUtil
        public short GasUtil { get; set; } // GasUtil
        public short WaterUtil { get; set; } // WaterUtil
        public short SewerUtil { get; set; } // SewerUtil
        public bool HasRange { get; set; } // HasRange
        public bool HasRefrigerator { get; set; } // HasRefrigerator
        public bool HasDishwasher { get; set; } // HasDishwasher
        public bool HasWasher { get; set; } // HasWasher
        public bool HasDryer { get; set; } // HasDryer
        public bool HasDisposal { get; set; } // HasDisposal
        public bool HasVentFan { get; set; } // HasVentFan
        public bool HasCarpet { get; set; } // HasCarpet
        public bool HasOtherEquip { get; set; } // HasOtherEquip
        public string OtherEquipDesc { get; set; } // OtherEquipDesc (length: 30)
        public short BuildingStatus { get; set; } // BuildingStatus
        public short BuildingType { get; set; } // BuildingType
        public short FactoryFab { get; set; } // FactoryFab
        public int? NoBuildings { get; set; } // NoBuildings
        public int? NoLivingUnits { get; set; } // NoLivingUnits
        public short StreetAccess { get; set; } // StreetAccess
        public short StreetMaint { get; set; } // StreetMaint
        public short Warranty { get; set; } // Warranty
        public string WarrantyProgram { get; set; } // WarrantyProgram (length: 50)
        public string WarrantyExpires { get; set; } // WarrantyExpires (length: 20)
        public string ConstCompleted { get; set; } // ConstCompleted (length: 20)
        public string Owner { get; set; } // Owner (length: 50)
        public short PropertyOccupancy { get; set; } // PropertyOccupancy
        public decimal? Rent { get; set; } // Rent
        public string OccupantName { get; set; } // OccupantName (length: 30)
        public string OccupantPhone { get; set; } // OccupantPhone (length: 10)
        public string BrokerName { get; set; } // BrokerName (length: 30)
        public string BrokerPhone { get; set; } // BrokerPhone (length: 20)
        public string InspectionDate { get; set; } // InspectionDate (length: 20)
        public bool CanInspectAm { get; set; } // CanInspectAM
        public bool CanInspectPm { get; set; } // CanInspectPM
        public string KeysAt { get; set; } // KeysAt (length: 50)
        public string InstNo { get; set; } // InstNo (length: 50)
        public short InspectionBy { get; set; } // InspectionBy
        public short Plans { get; set; } // Plans
        public string PlansCaseNo { get; set; } // PlansCaseNo (length: 50)
        public string Comments { get; set; } // Comments (length: 1000)
        public decimal? Taxes { get; set; } // Taxes
        public short MineralRightsReserved { get; set; } // MineralRightsReserved
        public string MineralRightsExpl { get; set; } // MineralRightsExpl (length: 50)
        public short LeaseType { get; set; } // LeaseType
        public string LeaseExpires { get; set; } // LeaseExpires (length: 20)
        public decimal? GroundRent { get; set; } // GroundRent
        public short PurLotSep { get; set; } // PurLotSep
        public short ContractAttached { get; set; } // ContractAttached
        public string ContractNo { get; set; } // ContractNo (length: 50)
        public short UseLoanValue { get; set; } // UseLoanValue
        public short Signature { get; set; } // Signature
        public string RequestorTitle { get; set; } // RequestorTitle (length: 50)
        public string RequestorPhone { get; set; } // RequestorPhone (length: 20)
        public string AssignmentDate { get; set; } // AssignmentDate (length: 20)
        public bool OmitBuilder { get; set; } // OmitBuilder
        public string FirmEmail { get; set; } // FirmEmail (length: 250)
        public string PointOfContactInfo { get; set; } // PointOfContactInfo (length: 2147483647)
        public string BuilderVaid { get; set; } // BuilderVAID (length: 50)

        public VaValue()
        {
            FileDataId = 0;
            IsIrregular = false;
            IsAcres = false;
            ElectricUtil = 0;
            GasUtil = 0;
            WaterUtil = 0;
            SewerUtil = 0;
            HasRange = false;
            HasRefrigerator = false;
            HasDishwasher = false;
            HasWasher = false;
            HasDryer = false;
            HasDisposal = false;
            HasVentFan = false;
            HasCarpet = false;
            HasOtherEquip = false;
            BuildingStatus = 0;
            BuildingType = 0;
            FactoryFab = 0;
            StreetAccess = 0;
            StreetMaint = 0;
            Warranty = 0;
            PropertyOccupancy = 0;
            CanInspectAm = false;
            CanInspectPm = false;
            InspectionBy = 0;
            Plans = 0;
            MineralRightsReserved = 0;
            LeaseType = 0;
            PurLotSep = 0;
            ContractAttached = 0;
            UseLoanValue = 0;
            Signature = 0;
            OmitBuilder = false;
        }
    }

    // WarehouseLender

    public class WarehouseLender
    {
        public int WarehouseLenderId { get; set; } // WarehouseLenderID (Primary key)
        public int DisplayOrder { get; set; } // DisplayOrder
        public System.Guid? Guid { get; set; } // Guid
        public string Name { get; set; } // Name (length: 50)
        public string Code { get; set; } // Code (length: 50)
        public string Notes { get; set; } // Notes (length: 2147483647)
        public string FanniePayeeIdentifier { get; set; } // FanniePayeeIdentifier (length: 9)
        public decimal? AdvanceAmountPerc { get; set; } // AdvanceAmountPerc
        public decimal? LineBalance { get; set; } // LineBalance
        public string AccountNo { get; set; } // AccountNo (length: 22)
        public int WarehouseAdvanceCalcMethod { get; set; } // WarehouseAdvanceCalcMethod
        public string FannieWarehouseIdentifier { get; set; } // FannieWarehouseIdentifier (length: 50)
        public string FreddieWarehouseIdentifier { get; set; } // FreddieWarehouseIdentifier (length: 50)
        public string MersOrgId { get; set; } // MERSOrgID (length: 7)

        public WarehouseLender()
        {
            DisplayOrder = 0;
            Guid = System.Guid.NewGuid();
            WarehouseAdvanceCalcMethod = 0;
        }
    }

    // ZipCode

    public class ZipCode
    {
        public int ZipCodeId { get; set; } // ZipCodeID (Primary key)
        public string ZipCode_ { get; set; } // ZipCode (length: 9)
        public string State { get; set; } // State (length: 2)
        public string City { get; set; } // City (length: 50)
        public string County { get; set; } // County (length: 50)
        public decimal? Latitude { get; set; } // Latitude
        public decimal? Longitude { get; set; } // Longitude
    }

    #endregion

    #region BytePro Enums

    public enum AmortizationTypeEnum
    {
        NotAssigned,
        Fixed,
        ARM,
        GPM,
        Other
    }
    public enum MortgageTypeEnum
    {
        NotAssigned,
        VA,
        FHA,
        Conventional,
        RHS,
        Other,
        HELOC,
        StateAgency,
        LocalAgency
    }
    public enum LoanPurposeEnum
    {
        NotAssigned,
        Purchase,
        Refinance,
        Construction,
        ConstructionPerm,
        Second,
        Third,
        PurchaseMoneySecond,
        Other,
        PurchaseMoneyThird,
        RefiSecond,
        RefiThird
    }
    public enum OccupancyTypeEnum
    {
        NotAssigned,
        PrimaryResidence,
        SecondaryResidence,
        InvestmentProperty
    }
    public enum MaritalStatusEnum
    {
        NotAssigned,
        Married,
        Separated,
        Unmarried
    }
    public enum BorPropertyTypeEnum
    {
        NotAssigned,
        PRPrincipalResidence,
        SHSecondHome,
        IPInvestmentProperty,
        SRFHASecondaryResidence
    }
    public enum TitleHeldEnum
    {
        NotAssigned,
        SSolely,
        SPJointlyWithSpouse,
        OJointlyWithAnotherPerson
    }
    public enum DebtTypeEnum
    {
        NotAssigned,
        Revolving,
        Installment,
        Mortgage,
        HELOC,
        Liens,
        LeasePayments,
        Open,
        Taxes,
        Other,
        TaxLien


    }
    public enum OtherExpenseTypeEnum
    {
        NotAssigned,
        Alimony,
        ChildSupport,
        SeparateMaintenance,
        OtherExpense
    }
    public enum AccountTypeEnum
    {
        NotAssigned,
        Savings,
        Checking,
        CashDepositOnSalesContract,
        GiftNotDeposited,
        CertificateOfDeposit,
        MoneyMarketFund,
        MutualFunds,
        Stocks,
        Bonds,
        SecuredBorrowedFundsNotDeposited,
        BridgeLoanNotDeposited,
        RetirementFunds,
        NetWorthOfBusinessOwned,
        TrustFunds,
        OtherNonLiquidAsset,
        OtherLiquidAsset,
        NetProceedsFromSaleOfRealEstate,
        NetEquity,
        CashOnHand,
        GiftOfEquity,
        IndividualDevelopmentAccount,
        LifeInsuranceCashValue,
        ProceedsFromSaleOfNonRealEstateAsset,
        SecuredBorrowedFunds,
        StockOptions,
        UnsecuredBorrowedFunds
    }
    public enum IncomeTypeEnum
    {
        NotAssigned,
        BaseIncome,
        Overtime,
        Bonus,
        Commission,
        DividendInterest,
        NetRentalIncome,
        AlimonyChildSupport,
        AutomobileExpenseAccount,
        FosterCare,
        NotesReceivableInstallment,
        Pension,
        SocialSecurity,
        SubjectPropertyNetCashFlow,
        Trust,
        Unemployment,
        PublicAssistance,
        VABenefitsNonEducational,
        MortgageDifferential,
        MilitaryBasePay,
        MilitaryRationsAllowance,
        MilitaryFlightPay,
        MilitaryHazardPay,
        MilitaryClothesAllowance,
        MilitaryQuartersAllowance,
        MilitaryPropPay,
        MilitaryOverseasPay,
        MilitaryCombatPay,
        MilitaryVariableHousingAllowance,
        MortgageCreditCertificate,
        TrailingCoBorrowerIncome,
        Other,
        BoarderIncome,
        CapitalGains,
        EmploymentRelatedAssets,
        ForeignIncome,
        RoyaltyPayment,
        SeasonalIncome,
        TemporaryLeave,
        TipIncome,
        Section8,
        NonBorHouseholdIncome,
        AccessoryUnitIncome,
        Alimony,
        ChildSupport,
        ContractBasis,
        DefinedContributionPlan,
        Disability,
        HousingAllowance,
        SeparateMaintenance
    }
    public enum REOTypeEnum
    {
        NotAssigned,
        SingleFamily,
        Condominium,
        Townhouse,
        Cooperative,
        TwoToFourUnitProperty,
        MultifamilyMoreThanFourUnits,
        ManufacturedMobileHome,
        CommercialNonResidential,
        MixedUseResidential,
        Farm,
        HomeAndBusinessCombined,
        Land
    }
    public enum REOStatusEnum
    {
        NotAssigned,
        Sold,
        PendingSale,
        Rental,
        Retained
    }
    public enum LivingStatusEnum
    {
        NotAssigned,
        Own,
        Rent,
        LivingRentFree
    }
    public enum PropertyTypeEnum
    {
        NotAssigned,
        Detached,
        Attached,
        Condominium,
        HighRiseCondo,
        DetachedCondo,
        PUD,
        Cooperative,
        Manufactured,
        Manufactured_Condo_PUD_COOP,
        ManufacturedSinglewide,
        ManufacturedMultiwide,
        Other,
        VacantLand,
        ManufacturedMHAdvantage
    }

    #endregion
}



