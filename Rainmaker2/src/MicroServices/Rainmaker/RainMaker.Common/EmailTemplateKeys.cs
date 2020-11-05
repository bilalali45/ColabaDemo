using RainMaker.Entity.Models;
using System.Collections.Generic;
using System.Linq;

namespace RainMaker.Common
{
    public enum FillKey
    {
        Date,
        DateTime,
        EmailViewLink,
        TrackingLogo,
        OpportunityPrimaryContactPreferredName,
        OpportunityPrimaryContactNickName,
        OpportunityPrimaryContactFirstName,
        OpportunityPrimaryContactLastName,
        OpportunityPrimaryContactEmailAddress,
        OpportunityStatusLog,
        OpportunityStatusDate,
        OpportunityId,
        PropertyState,
        PropertyStateAbberviated,
        PropertyCounty,
        LoanPurpose,
        LoanRequestSource,
        LoanAmount,
        PropertyValue,
        CashoutAmount,
        DownPayment,
        DownPaymentPercent,
        PropertyType,
        PropertyUsage,
        CreditScore,
        LockPeriodDays,
        EscrowWaiver,
        SecondLienBalance,
        ResidencyType,
        FirstTimeHomeBuyer,
        SelfEmployed,
        OwnRentalProperties,
        DtiHousing,
        DtiTotal,
        LastSearchDate,
        LoanGoal,
        StatusCause,
        IsValidLoanRequest,
        PropertyCity,
        PropertyZipCode,
        FirstMortgageBalance,
        SecondMortgageBalance,
        //EmployeeName,
        //CustomerName,
        UnsubscribeLink,
        UnsubscribePageUrl,

        Ltv,
        Cltv,
        BranchNmlsNo,
        BusinessUnitName,
        BusinessUnitShortName,
        BusinessUnitAbbreviatedName,
        BusinessUnitPhone,
        BusinessUnitOtherPhone,
        BusinessUnitWebSiteUrl,
        PlainBusinessUnitWebSiteUrl,
        BusinessUnitLogoUrl,
        Impound,
        EscrowsRequired,
        DirectLender,
        //SecondLienLine,
        CreditScoreName,
        //ProposalForLoanRequest,
        EmailSubject,
        EmailDomainUrl,
        FromEmail,
        EmailTag,
        EmailBody,
        LoanApplicationId,
        TenantId,
        ErrorUrl,
        ErrorCode,
        OpportunitySecondaryContactPreferredName,
        OpportunitySecondaryContactNickName,
        OpportunitySecondaryContactFirstName,
        OpportunitySecondaryContactLastName,
        OpportunityEmployeeFirstName,
        OpportunityEmployeeLastName,
        OpportunityEmployeeNickName,
        OpportunityEmployeePreferredName,
        OpportunityEmployeeScheduleUrl,
        OpportunityEmployeeNmlsNo,
        OpportunityEmployeeOfficeDirectPhone,
        OpportunityEmployeeOfficeCellPhone,
        OpportunityEmployeeCellPhone,
        OpportunityAssignEmployeeNmlsNo,

        BusinessUnitFetchRatesEmail,
        LoOrBusinessUnitFetchRatesEmail,
        BusinessUnitFetchRatesNoFeeEmail,
        LoOrBusinessUnitFetchRatesNoFeeEmail,

        BusinessUnitEmailRateStackEmail,
        BusinessUnitEmailClosingCostEmail,
        BusinessUnitEmailCompareEmail,

        LoOrBusinessUnitAllRateStackEmail,
        LoOrBusinessUnitRateStackEmail,
        LoOrBusinessUnitClosingCostEmailEmail,
        LoOrBusinessUnitCompareEmailEmail,

        RealTimeRateSheetUrl,
        RateSheetUrl,
        ClosingCostUrl,
        ComparePageUrl,

        DesireRatesOptions,

        BusinessUnitLoanApplicationLink,
        BusinessUnitLoanApplicationLoginLink,
        LoOrBusinessUnitLoanApplicationLink,
        LoOrBusinessUnitLoanApplicationLoginLink,
        LoProfileLink,
        LoProfileUrl,
        LoEmailAddress,
        QuoteRateId,
        QuoteId,

        CustomEmailHeader,
        CustomEmailFooter,
        EmailTemplateId,

        HomePage,
        GiftPage,

        ResetPasswordLink,
        RegisterPasswordLink,
        LoginLink,
        ActivityDate,
        OpportunityLinkUrl,
        LoanApplicationLinkUrl,
        LoanApplicationPropertyAddress,
        LoanApplicationLoanAmount,
        LoanApplicationPurpose,
        LoanApplicationLandingPageLink,
        DefaultRatePageLink,
        LoanApplicationCreateDate,
        DailyRates,
        ByteFileName,
        ByteStatusId
    }

    public enum Questions
    {
        FirstTimeHomeBuyer = 1,
        SelfEmployed = 2
    }
    public class EmailTemplateKeys
    {


        private readonly static IDictionary<FillKey, EmailTemplateKey> Keys = new Dictionary<FillKey, EmailTemplateKey>
        {
        {FillKey.Date,   new EmailTemplateKey{KeyName = "Date" ,Symbol = "###Date###",Description = "Current Date",IsIndependentSystemKey=true}},
        {FillKey.DateTime,   new EmailTemplateKey{KeyName = "Date Time",Symbol = "###DateTime###",Description = "Current Date Time",IsIndependentSystemKey=true}},
        {FillKey.EmailViewLink,   new EmailTemplateKey{KeyName = "Email View Link" ,Symbol = "###ViewLink###",Description = "Link for viewing email online",IsIndependentSystemKey=true}},
        {FillKey.UnsubscribeLink,   new EmailTemplateKey{KeyName = "Un-subscribe Email Link with Tag" ,Symbol = "###UnsubscribeLink###",Description = "Link for un-subscribe email with Tag",IsIndependentSystemKey=true}},
        {FillKey.UnsubscribePageUrl,   new EmailTemplateKey{KeyName = "Unsubscribe Page Url" ,Symbol = "###UnsubscribePageUrl###",Description = "Unsubscribe page url",IsIndependentSystemKey=true}},
        {FillKey.TrackingLogo,   new EmailTemplateKey{KeyName = "Email Tracking",Symbol = "###TrackingLog###",Description = "Key for enabling tracking of email",IsIndependentSystemKey=true}},
        {FillKey.HomePage,   new EmailTemplateKey{KeyName = "Home Page Tracking Url",Symbol = "###TrackingHomeUrl###",Description = "Key for tracking Home Page url",IsIndependentSystemKey=true}},
        {FillKey.GiftPage,   new EmailTemplateKey{KeyName = "Gift Page Tracking Url",Symbol = "###TrackingGiftUrl###",Description = "Key for tracking Gift Page url",IsIndependentSystemKey=true}},

        {FillKey.OpportunityPrimaryContactPreferredName,   new EmailTemplateKey{KeyName = "Primary Customer Preferred Name",Symbol = "###PrimaryPreferredName###",Description = "Opportunity Primary Customer Contact Preferred Name",IsIndependentSystemKey=true}},
        {FillKey.OpportunityPrimaryContactNickName,   new EmailTemplateKey{KeyName = "Primary Customer Nick Name",Symbol = "###PrimaryNickName###",Description = "Opportunity Primary Customer Contact Nick Name",IsIndependentSystemKey=true}},
        {FillKey.OpportunityPrimaryContactFirstName,   new EmailTemplateKey{KeyName = "Primary Customer First Name",Symbol = "###PrimaryFirstName###",Description = "Opportunity Primary Customer Contact First Name",IsIndependentSystemKey=true}},
        {FillKey.OpportunityPrimaryContactLastName,   new EmailTemplateKey{KeyName = "Primary Customer Last Name",Symbol = "###PrimaryLastName###",Description = "Opportunity Primary Customer Contact Last Name",IsIndependentSystemKey=true}},
        {FillKey.OpportunityPrimaryContactEmailAddress, new EmailTemplateKey{KeyName = "Customer Email Address",Symbol = "###PrimaryEmailAddress###",Description = "Customer Email Address",IsIndependentSystemKey=true}},
        {FillKey.OpportunitySecondaryContactPreferredName,   new EmailTemplateKey{KeyName = "Secondary Customer Preferred Name",Symbol = "###SecondaryPreferredName###",Description = "Opportunity Secondary Customer Contact Preferred Name",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.OpportunitySecondaryContactNickName,   new EmailTemplateKey{KeyName = "Secondary Customer Nick Name",Symbol = "###SecondaryNickName###",Description = "Opportunity Secondary Customer Contact Nick Name",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.OpportunitySecondaryContactFirstName,   new EmailTemplateKey{KeyName = "Secondary Customer First Name",Symbol = "###SecondaryFirstName###",Description = "Opportunity Secondary Customer Contact First Name",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.OpportunitySecondaryContactLastName,   new EmailTemplateKey{KeyName = "Secondary Customer Last Name",Symbol = "###SecondaryLastName###",Description = "Opportunity Secondary Customer Contact Last Name",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},

        {FillKey.EmailSubject,   new EmailTemplateKey{KeyName = "Email Subject",Symbol = "###EmailSubject###",Description = "Email Subject",IsIndependentSystemKey=false}},
        {FillKey.EmailDomainUrl,   new EmailTemplateKey{KeyName = "Email Domain Url",Symbol = "###EmailDomainURL###",Description = "Email Domain url",IsIndependentSystemKey=false}},
        {FillKey.FromEmail,   new EmailTemplateKey{KeyName = "FromEmail",Symbol = "###FromEmail###",Description = "From Email",IsIndependentSystemKey=false}},
        {FillKey.EmailBody,   new EmailTemplateKey{KeyName = "Email Body",Symbol = "###EmailBody###",Description = "Email Body",IsIndependentSystemKey=false}},
        //{FillKey.EmailTag,   new EmailTemplateKey{KeyName = "EmailTag",Symbol = "###EmailTag###",Description = "Email Tag",IsIndependentSystemKey=false}},
        {FillKey.EmailTag,   new EmailTemplateKey{KeyName = "EmailTag",Symbol = "###EmailTag###",Description = "Email Tag",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.LoanApplicationId,   new EmailTemplateKey{KeyName = "LoanApplicationId",Symbol = "###LoanApplicationId###",Description = "Loan Application Id",IsIndependentSystemKey=false}},
        {FillKey.TenantId,   new EmailTemplateKey{KeyName = "TenantId",Symbol = "###TenantId###",Description = "Tenant Id",IsIndependentSystemKey=false}},
        {FillKey.ErrorCode,   new EmailTemplateKey{KeyName = "ErrorCode",Symbol = "###ErrorCode###",Description = "Error Code",IsIndependentSystemKey=false}},
        {FillKey.ErrorUrl,   new EmailTemplateKey{KeyName = "ErrorUrl",Symbol = "###ErrorUrl###",Description = "Error Url",IsIndependentSystemKey=false}},
        {FillKey.ByteFileName,   new EmailTemplateKey{KeyName = "ByteFileName", Symbol = "###ByteFileName###",Description = "Byte File Name" ,IsIndependentSystemKey = false}},
        {FillKey.ByteStatusId,   new EmailTemplateKey{KeyName = "ByteStatusId", Symbol = "###ByteStatusId###",Description = "Byte Status Id" ,IsIndependentSystemKey = false}},

        //{FillKey.ProposalForLoanRequest,   new EmailTemplateKey{KeyName = "Loan Request Proposal",Symbol = "###LoanRequestProposal###",Description = "Opportunity Loan Request Proposal",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.OpportunityId,   new EmailTemplateKey{KeyName = "Opportunity Id",Symbol = "###OpportunityId###",Description = "Opportunity Id",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.LoanApplicationCreateDate,   new EmailTemplateKey{KeyName = "Loan Application Create Date",Symbol = "###LoanApplicationCreateDate###",Description = "Loan Application Create Date",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        { FillKey.OpportunityStatusLog,   new EmailTemplateKey{KeyName = "Opportunity Status",Symbol = "###OpportunityStatus###",Description = "Opportunity Status",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.OpportunityStatusDate,   new EmailTemplateKey{KeyName = "Opportunity Status Log Date",Symbol = "###OpportunityStatusDate###",Description = "Opportunity Status Log Date",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.PropertyState,   new EmailTemplateKey{KeyName = "Property State",Symbol = "###PropertyState###",Description = "Property State",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.PropertyStateAbberviated,   new EmailTemplateKey{KeyName = "Property State Abbreviation",Symbol = "###PropertyStateAbberviated###",Description = "Property State Abbreviation",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.PropertyCounty,   new EmailTemplateKey{KeyName = "Property County",Symbol = "###PropertyCounty###",Description = "Property County",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.LoanPurpose,   new EmailTemplateKey{KeyName = "Loan Purpose",Symbol = "###LoanPurpose###",Description = "Loan Purpose",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.LoanRequestSource,   new EmailTemplateKey{KeyName = "LoanRequest Source",Symbol = "###LoanRequestSource###",Description = "Opportunity LoanRequest Source",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.LoanAmount,   new EmailTemplateKey{KeyName = "Loan Amount",Symbol = "###LoanAmount###",Description = "Opportunity LoanRequest Loan Amount",EntityRefType =Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.CashoutAmount,   new EmailTemplateKey{KeyName = "Cash Out Amount",Symbol = "###CashOutAmount###",Description = "Cash out Amount",EntityRefType =Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.DownPayment,   new EmailTemplateKey{KeyName = "Down Payment Amount",Symbol = "###DownPayment###",Description = "Down Payment Amount",EntityRefType =Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.DownPaymentPercent,   new EmailTemplateKey{KeyName = "Down Payment Percentage",Symbol = "###DownPaymentPercent###",Description = "Down Payment in percentage",EntityRefType =Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.PropertyValue,   new EmailTemplateKey{KeyName = "Property Value",Symbol = "###PropertyValue###",Description = "Property Value",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.PropertyType,   new EmailTemplateKey{KeyName = "Property Type",Symbol = "###PropertyType###",Description = "Property Type",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.PropertyUsage,   new EmailTemplateKey{KeyName = "Property Usage",Symbol = "###PropertyUsage###",Description = "Property Usage",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.CreditScore,   new EmailTemplateKey{KeyName = "Credit Score",Symbol = "###CreditScore###",Description = "Credit Score",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.CreditScoreName,   new EmailTemplateKey{KeyName = "Credit Score Name",Symbol = "###CreditScoreName###",Description = "Credit Score Name",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.LockPeriodDays,   new EmailTemplateKey{KeyName = "Lock Period Days",Symbol = "###LockPeriodDays###",Description = "Lock Period Days",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.EscrowWaiver,   new EmailTemplateKey{KeyName = "Escrow Waiver",Symbol = "###EscrowWaiver###",Description = "Escrow Waiver",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.SecondLienBalance,   new EmailTemplateKey{KeyName = "Second Lien Balance",Symbol = "###SecondLienBalance###",Description = "Second Lien Balance",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.OpportunityEmployeeScheduleUrl,   new EmailTemplateKey{KeyName = "Employee Schedule Url",Symbol = "###EmployeeScheduleUrl###",Description = "Employee Schedule Url",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.FirstMortgageBalance,   new EmailTemplateKey{KeyName = "1st Mortgage Balance",Symbol = "###FirstMortgageBalance###",Description = "1st Mortgage Balance",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.SecondMortgageBalance,   new EmailTemplateKey{KeyName = "2nd Mortgage Balance",Symbol = "###SecondMortgageBalance###",Description = "2nd Mortgage Balance",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.ResidencyType,   new EmailTemplateKey{KeyName = "Residency Type",Symbol = "###ResidencyType###",Description = "Residency Type",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.FirstTimeHomeBuyer,   new EmailTemplateKey{KeyName = "First Time Home Buyer",Symbol = "###FirstTimeHomeBuyer###",Description = "First Time Home Buyer",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.SelfEmployed,   new EmailTemplateKey{KeyName = "Self Employed",Symbol = "###SelfEmployed###",Description = "Self Employed",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.OwnRentalProperties,   new EmailTemplateKey{KeyName = "Own Rental Properties",Symbol = "###OwnRentalProperties###",Description = "Own Rental Properties",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.DtiHousing,   new EmailTemplateKey{KeyName = "DTI Housing",Symbol = "###DTIHousing###",Description = "DTI Housing",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.DtiTotal,   new EmailTemplateKey{KeyName = "DTI Total",Symbol = "###DTITotal###",Description = "DTI Total",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.LastSearchDate,   new EmailTemplateKey{KeyName = "Last Search Date",Symbol = "###LastSearchDate###",Description = "Last Search Date",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.LoanGoal,   new EmailTemplateKey{KeyName = "Loan Goal",Symbol = "###LoanGoal###",Description = "Loan Goal",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.StatusCause,   new EmailTemplateKey{KeyName = "Status Cause",Symbol = "###StatusCause###",Description = "Status Cause",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.IsValidLoanRequest,   new EmailTemplateKey{KeyName = "Is Valid LoanRequest",Symbol = "###IsValidLoanRequest###",Description = "Is Valid LoanRequest",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.PropertyCity,   new EmailTemplateKey{KeyName = "Property City",Symbol = "###PropertyCity###",Description = "Property City",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.PropertyZipCode,   new EmailTemplateKey{KeyName = "Property ZipCode",Symbol = "###PropertyZipCode###",Description = "Property Zip Code",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.Ltv,   new EmailTemplateKey{KeyName = "LTV",Symbol = "###LTV###",Description = "Loan Request LTV",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.Cltv,   new EmailTemplateKey{KeyName = "CLTV",Symbol = "###CLTV###",Description = "Loan Request CLTV",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.Impound,   new EmailTemplateKey{KeyName = "ESCROWS impound with or without",Symbol = "###IMPOUND###",Description = "ESCROWS impound with or without",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.EscrowsRequired,   new EmailTemplateKey{KeyName = "ESCROWS required or not required",Symbol = "###ESCROWSREQUIRED###",Description = "ESCROWS required or not required",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.DirectLender,   new EmailTemplateKey{KeyName = "DIRECT LENDER line",Symbol = "###DIRECTLENDER###",Description = "DIRECT LENDER line",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        //{FillKey.SecondLienLine,   new EmailTemplateKey{KeyName = "SECOND LIEN LINE",Symbol = "###SecondLienLine###",Description = "SECOND LIEN LINE",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.QuoteRateId,   new EmailTemplateKey{KeyName = "Price Id",Symbol = "###QuoteRateId###",Description = "Price Id in which customer interested",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.QuoteId,   new EmailTemplateKey{KeyName = "Quote Id",Symbol = "###QuoteId###",Description = "Latest Quote Result Id",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},

        { FillKey.OpportunityEmployeeFirstName,   new EmailTemplateKey{KeyName = "Opportunity Employee First Name",Symbol = "###OpportunityEmployeeFirstName###",Description = "Opportunity Employee First Name",IsIndependentSystemKey=true}},
        {FillKey.OpportunityEmployeeLastName,   new EmailTemplateKey{KeyName = "Opportunity Employee Last Name",Symbol = "###OpportunityEmployeeLastName###",Description = "Opportunity Employee Last Name",IsIndependentSystemKey=true}},
        {FillKey.OpportunityEmployeeNickName,   new EmailTemplateKey{KeyName = "Opportunity Employee Nick Name",Symbol = "###OpportunityEmployeeNickName###",Description = "Opportunity Employee Nick Name",IsIndependentSystemKey=true}},
        {FillKey.OpportunityEmployeePreferredName,   new EmailTemplateKey{KeyName = "Opportunity Employee Preferred Name",Symbol = "###OpportunityEmployeePreferredName###",Description = "Opportunity Employee Preferred Name",IsIndependentSystemKey=true}},
        {FillKey.OpportunityEmployeeNmlsNo,   new EmailTemplateKey{KeyName = "Opportunity Employee Nmls No Only",Symbol = "###OpportunityEmployeeNmlsNo###",Description = "Employee Nmls No Only",IsIndependentSystemKey=true}},
        {FillKey.OpportunityEmployeeOfficeDirectPhone,   new EmailTemplateKey{KeyName = "Opportunity Employee Office Direct Phone No",Symbol = "###OpportunityEmployeeOfficeDirectPhone###",Description = "Employee Office Direct Phone No",IsIndependentSystemKey=true}},
        {FillKey.OpportunityEmployeeOfficeCellPhone,   new EmailTemplateKey{KeyName = "Opportunity Employee Office Cell No",Symbol = "###OpportunityEmployeeOfficeCellPhone###",Description = "Employee Office Cell No",IsIndependentSystemKey=true}},
        {FillKey.OpportunityEmployeeCellPhone,   new EmailTemplateKey{KeyName = "Opportunity Employee Cell Phone",Symbol = "###OpportunityEmployeeCellPhone###",Description = "Employee Cell Phone",IsIndependentSystemKey=true}},
        {FillKey.OpportunityAssignEmployeeNmlsNo,   new EmailTemplateKey{KeyName = "Opportunity Employee Nmls No With Label",Symbol = "###OpportunityAssignEmployeeNmlsNo###",Description = "Employee Nmls No With Label",IsIndependentSystemKey=true}},

        {FillKey.BranchNmlsNo,   new EmailTemplateKey{KeyName = "Branch Nmls No",Symbol = "###BranchNmlsNo###",Description = "BranchNmls No",IsIndependentSystemKey=true}},
        {FillKey.BusinessUnitName,   new EmailTemplateKey{KeyName = "Business Unit Name",Symbol = "###BusinessUnitName###",Description = "Opportunity Business Unit Name",IsIndependentSystemKey=true}},
        {FillKey.BusinessUnitShortName,   new EmailTemplateKey{KeyName = "Business Unit Short Name",Symbol = "###BUShortName###",Description = "Business Short Unit Name",IsIndependentSystemKey=true}},
        {FillKey.BusinessUnitAbbreviatedName,   new EmailTemplateKey{KeyName = "Business Unit Abbreviated Name",Symbol = "###BUAbbreviatedName###",Description = "Business Unit Abbreviated Name",IsIndependentSystemKey=true}},
        { FillKey.BusinessUnitPhone,   new EmailTemplateKey{KeyName = "Business Unit Toll Free Number",Symbol = "###BusinessUnitPhone###",Description = "Opportunity Business Unit Toll Free Number",IsIndependentSystemKey=true}},
        {FillKey.BusinessUnitOtherPhone,   new EmailTemplateKey{KeyName = "Business Unit Other Phone",Symbol = "###BusinessUnitOtherPhone###",Description = "Opportunity Business Unit other Phone",IsIndependentSystemKey=true}},
        {FillKey.BusinessUnitWebSiteUrl,   new EmailTemplateKey{KeyName = "Business Unit WebSite Url",Symbol = "###BusinessUnitWebSiteUrl###",Description = "Insert business unit website url",IsIndependentSystemKey=true}},
        {FillKey.PlainBusinessUnitWebSiteUrl,   new EmailTemplateKey{KeyName = "Plain Business Unit WebSite Url",Symbol = "###PlainBusinessUnitWebSiteUrl###",Description = "Insert business unit website url with no tracking",IsIndependentSystemKey=true}},
        {FillKey.BusinessUnitLogoUrl,   new EmailTemplateKey{KeyName = "Business Unit Logo Url",Symbol = "###BusinessUnitLogoUrl###",Description = "Insert business unit logo url",IsIndependentSystemKey=true}},
        {FillKey.BusinessUnitLoanApplicationLink,   new EmailTemplateKey{KeyName = "Business Unit Loan Application Signup Link",Symbol = "###BusinessUnitLoanApplicationLink###",Description = "Business Unit Loan Application Signup Link",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.BusinessUnitLoanApplicationLoginLink,   new EmailTemplateKey{KeyName = "Business Unit Loan Application Login Link",Symbol = "###BusinessUnitLoanApplicationLoginLink###",Description = "Business Unit Loan Application Login Link",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},

        { FillKey.BusinessUnitFetchRatesEmail,   new EmailTemplateKey{KeyName = "Business Unit - Fetch New Rates",Symbol = "###BusinessUnitFetchRates###",Description = "Fetch New Rates and Email With Business Unit Values",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.LoOrBusinessUnitFetchRatesEmail,   new EmailTemplateKey{KeyName = "LO or BU - Fetch New Rates",Symbol = "###LoOrBusinessUnitFetchRatesEmail###",Description = "Fetch New Rates and Email With LO or Business Unit Values",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.BusinessUnitFetchRatesNoFeeEmail,   new EmailTemplateKey{KeyName = "Business Unit - Fetch New Rates (Single Rate)",Symbol = "###BusinessUnitFetchRatesNoFee###",Description = "Fetch New Rates and Email With Business Unit Values (Single Rate & With No Fee Column)",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.LoOrBusinessUnitFetchRatesNoFeeEmail,   new EmailTemplateKey{KeyName = "LO or BU - Fetch New Rates (Single Rate)",Symbol = "###LoOrBusinessUnitFetchRatesEmailNoFee###",Description = "Fetch New Rates and Email With LO or Business Unit Values (Single Rate & With No Fee Column) ",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},

        {FillKey.BusinessUnitEmailRateStackEmail,   new EmailTemplateKey{KeyName = "Business Unit - Rate Stack",Symbol = "###BusinessUnitEmailRateStack###",Description = "Send Rate Stack List With Business Unit Values",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.BusinessUnitEmailClosingCostEmail,   new EmailTemplateKey{KeyName = "Business Unit - Closing Cost",Symbol = "###BusinessUnitEmailClosingCost###",Description = "Closing Cost With Business Unit Values",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.BusinessUnitEmailCompareEmail,   new EmailTemplateKey{KeyName = "Business Unit - Compare Rates",Symbol = "###BusinessUnitEmailCompare###",Description = "Compare Rates With Business Unit Values",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},

        {FillKey.LoOrBusinessUnitAllRateStackEmail,   new EmailTemplateKey{KeyName = "LO or BU - Rate Stack",Symbol = "###LoOrBusinessUnitAllRateStack###",Description = "Send all rates on fething rates from customer site result page",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.LoOrBusinessUnitRateStackEmail,   new EmailTemplateKey{KeyName = "LO or BU - Rate Stack",Symbol = "###LoOrBusinessUnitRateStack###",Description = "Send rates list via campaign With LO or Business Unit Values",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.LoOrBusinessUnitClosingCostEmailEmail,   new EmailTemplateKey{KeyName = "LO or BU - Closing Cost",Symbol = "###LoOrBusinessUnitClosingCost###",Description = "Closing Cost With LO or Business Unit Values",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.LoOrBusinessUnitCompareEmailEmail,   new EmailTemplateKey{KeyName = "LO or BU - Compare Rates",Symbol = "###LoOrBusinessUnitCompare###",Description = "Compare Rates With LO or Business Unit Values",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},

        {FillKey.RateSheetUrl,   new EmailTemplateKey{KeyName = "View Rate Sheet Url",Symbol = "###RateSheetUrl###",Description = "Insert View Rate Sheet Url Only",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.ClosingCostUrl,   new EmailTemplateKey{KeyName = "View Closing Cost Url",Symbol = "###ClosingCostUrl###",Description = "Insert View Closing Cost Url Only",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.ComparePageUrl,   new EmailTemplateKey{KeyName = "View Compare Rates Url",Symbol = "###ComparePageUrl###",Description = "Insert View Compare Rates Url Only",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.RealTimeRateSheetUrl,   new EmailTemplateKey{KeyName = "Real Time Rate Sheet Url",Symbol = "###RealTimeRateSheetUrl###",Description = "Fetch new rate and display on result page",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.DesireRatesOptions,   new EmailTemplateKey{KeyName = "Desire Rates",Symbol = "###DesireRatesOptions###",Description = "Fetch new rate and send customer desire rates only",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},


        {FillKey.LoOrBusinessUnitLoanApplicationLink,   new EmailTemplateKey{KeyName = "Loan Officer Or BU Loan Application Signup Link",Symbol = "###LoanOfficerLoanApplicationLink###",Description = "Loan Officer or Business Unit Loan Application Signup Link",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.LoOrBusinessUnitLoanApplicationLoginLink,   new EmailTemplateKey{KeyName = "Loan Officer Or BU Loan Application Login Link",Symbol = "###LoanOfficerLoanApplicationLoginLink###",Description = "Loan Officer or Business Unit Loan Application Login Link",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.LoProfileLink,   new EmailTemplateKey{KeyName = "Loan Officer Profile Page With Link Tag",Symbol = "###LoProfileLink###",Description = "Loan Officer Profile Page With Link Tag",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.LoProfileUrl,   new EmailTemplateKey{KeyName = "Loan Officer Profile Page Link",Symbol = "###LoProfileUrl###",Description = "Loan Officer Profile Page Link",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},
        {FillKey.LoEmailAddress,   new EmailTemplateKey{KeyName = "Loan Officer Email Address",Symbol = "###LoEmailAddress###",Description = "Loan Officer Email Address",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},

        {FillKey.CustomEmailHeader, new EmailTemplateKey{KeyName = "Custom Email Header",Symbol = "###CustomEmailHeader###",Description = "Insert customer header text in email",IsIndependentSystemKey=true}},
        {FillKey.CustomEmailFooter, new EmailTemplateKey{KeyName = "Custom Email Footer",Symbol = "###CustomEmailFooter###",Description = "Insert customer footer text in email",IsIndependentSystemKey=true}},
        {FillKey.EmailTemplateId, new EmailTemplateKey{KeyName = "Email Template Id",Symbol = "###EmailTemplateId###",Description = "Insert email template idin email",IsIndependentSystemKey=true}},

        {FillKey.DefaultRatePageLink, new EmailTemplateKey{KeyName = "Default Rate Page Link",Symbol = "###DefaultRatePageLink###",Description = "Default Rate Page Link",IsIndependentSystemKey=true}},
        {FillKey.ResetPasswordLink, new EmailTemplateKey{KeyName = "Reset Password Link",Symbol = "###ResetPasswordLink###",Description = "Password reset link",IsIndependentSystemKey=true}},
        {FillKey.RegisterPasswordLink, new EmailTemplateKey{KeyName = "Co-Borrower Register Password Link",Symbol = "###RegisterPasswordLink###",Description = "Co-Borrower register password link",IsIndependentSystemKey=true}},
        {FillKey.LoanApplicationLandingPageLink, new EmailTemplateKey{KeyName = "Loan Application Landing Page Link",Symbol = "###LoanApplicationLandingPageLink###",Description = "Loan Application Landing Page Link",IsIndependentSystemKey=true}},
        { FillKey.LoginLink, new EmailTemplateKey{KeyName = "Account Login Link",Symbol = "###LoginLink###",Description = "Account Login Link",EntityRefType = Constants.GetEntityType(typeof(Customer)),IsIndependentSystemKey=false}},
        {FillKey.ActivityDate, new EmailTemplateKey{KeyName = "Activity Date",Symbol = "###ActivityDate###",Description = "Activity Date",EntityRefType = Constants.GetEntityType(typeof(Customer)),IsIndependentSystemKey=false}},

        {FillKey.OpportunityLinkUrl, new EmailTemplateKey{KeyName = "Opportunity Edit Link",Symbol = "###OpportunityLinkUrl###",Description = "Lead Edit Link for LO",IsIndependentSystemKey=true}},
        {FillKey.LoanApplicationLinkUrl, new EmailTemplateKey{KeyName = "Loan Application Edit Link",Symbol = "###LoanApplicationLinkUrl###",Description = "Loan Application Edit Link for LO",IsIndependentSystemKey=true}},
        {FillKey.LoanApplicationPurpose, new EmailTemplateKey{KeyName = "Loan Purpose",Symbol = "###LoanApplicationPurpose###",Description = "Loan Purpose",EntityRefType = Constants.GetEntityType(typeof(Customer)),IsIndependentSystemKey=false}},
        {FillKey.LoanApplicationPropertyAddress, new EmailTemplateKey{KeyName = "Property Address",Symbol = "###LoanApplicationPropertyAddress###",Description = "Property Address",EntityRefType = Constants.GetEntityType(typeof(Customer)),IsIndependentSystemKey=false}},
        {FillKey.LoanApplicationLoanAmount, new EmailTemplateKey{KeyName = "Loan Amount",Symbol = "###LoanApplicationLoanAmount###",Description = "Activity Time",EntityRefType = Constants.GetEntityType(typeof(Customer)),IsIndependentSystemKey=false}},

        {FillKey.DailyRates,   new EmailTemplateKey{KeyName = "Daily Rates",Symbol = "###DailyRatesEmail###",Description = "Fetch new rate and send customer desire rates and recomended only",EntityRefType = Constants.GetEntityType(typeof(Opportunity)),IsIndependentSystemKey=false}},

        };


        public static string GetKeySymbol(FillKey key)
        {
            var tem = Keys[key];
            return tem != null ? tem.Symbol : key.ToString();
        }
        public static IEnumerable<KeyValuePair<FillKey, EmailTemplateKey>> GetValueWithKeys(int entityRefTye)
        {
            return Keys.Where(s => s.Value.EntityRefType == entityRefTye || s.Value.IsIndependentSystemKey);
        }
        public static IEnumerable<EmailTemplateKey> GetKeys(int entityRefTye)
        {
            return Keys.Values.Where(s => s.EntityRefType == entityRefTye || s.IsIndependentSystemKey);
        }

        public static IEnumerable<KeyValuePair<FillKey, EmailTemplateKey>> GetIndependentSystemKeyKeys()
        {
            return Keys.Where(s => s.Value.IsIndependentSystemKey);
        }

    }

    public class EmailTemplateKey
    {
        public string KeyName { get; set; }
        public string Description { get; set; }
        public string Symbol { get; set; }
        public int EntityRefType { get; set; }
        public TemplateCommand? GeneratingCommand { get; set; }
        public bool IsIndependentSystemKey { get; set; }

    }

    public class ProductRateTemplateKey
    {
        public string KeyName { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public int FieldTypeId { get; set; }

    }

    public enum TemplateCommand
    {

    }

    public enum DynamicHtmlTypes
    {
        DefaultNone = 0,
        PrintRates = 1,
        PrintCompare = 2,
        PrintCosts = 3,

        BusinessUnitEmailRateStackEmail = 4,
        BusinessUnitEmailCompareEmail = 5,
        BusinessUnitEmailClosingCostEmail = 6,

        LoOrBusinessUnitRateStackEmail = 7,
        LoOrBusinessUnitCompareEmail = 8,
        LoOrBusinessUnitClosingCostEmail = 9,

        BusinessUnitFetchRatesEmail = 10,
        LoOrBusinessUnitFetchRatesEmail = 11,
        BusinessUnitFetchRatesNoFeeEmail = 12,
        LoOrBusinessUnitFetchRatesNoFeeEmail = 13,
        DesireRateEmail = 14,
        DailyRateEmail = 15,
        LoanEstimate = 16,

        ProposalTemplate = 100,
    }
}
