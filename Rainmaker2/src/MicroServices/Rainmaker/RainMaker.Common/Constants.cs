using RainMaker.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RainMaker.Common
{
    public class AjaxResponse
    {
        public int Status { get; set; }
        public IList<string> Message { get; set; }
        public Dictionary<string, string> ValidationMessages { get; set; }
        public object Data { get; set; }

    }

    public class AjaxMultipleResponse
    {
        public int Status { get; set; }
        public IList<string> Message { get; set; }
        public object DataFirst { get; set; }
        public object DataSecond { get; set; }
        public object DataThird { get; set; }

    }

    public class AjaxResponseList
    {
        public int Status { get; set; }
        public IList<string> Message { get; set; }
        public IList<object> Data { get; set; }

    }

    public class SystemCmsPage
    {
        public int SystemPageId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string SystemName { get; set; }
    }

    public enum BlockGroupType
    {
        [Description("-------")]
        None = 0,
        [Description("Closing Cost")]
        ClosingCost = 1,
        [Description("Other Costs/(Credits)")]
        DebitsNCredits = 2,
        [Description("Prepaid & Escrow")]
        PrepaidNEscrow = 3,
        [Description("Cash At Closing")]
        CashAtClosing = 4,
        [Description("Mortgage Payment")]
        MortgagePayment = 5,
        [Description("Taxes & Inusrance")]
        TaxesInsurance = 6
    }

    //public static class CampaignRuleFields
    //{
    //    public const string OpportunityStatusDatetime = "OpportunityStatusDatetime";
    //}
    public enum PaidByType
    {
        [Description("Borrower")]
        Borrower = 1,
        [Description("Seller")]
        Seller = 2,
        [Description("Other")]
        Other = 3,
    }

    public enum CmsChangeFrequency
    {
        [Description("Always")]
        always = 1,
        [Description("Hourly")]
        hourly = 2,
        [Description("Daily")]
        daily = 3,
        [Description("Weekly")]
        weekly = 4,
        [Description("Monthly")]
        monthly = 5,
        [Description("Yearly")]
        yearly = 6,
        [Description("Never")]
        never = 7
    }

    public enum PasswordLogType
    {
        [Description("Register")]
        Register = 1,
        [Description("Manage")]
        Manage = 2,
        [Description("Forgot")]
        Forgot = 3,
        [Description("Other")]
        Other = 4,
    }


    public static class Constants
    {
        public static FeeBlockType[] ClosingCostBlocks = new[] { FeeBlockType.GuaranteedLenderFees, FeeBlockType.LenderThirdPartyServiceFees, FeeBlockType.BuyerThirdPartyServiceFees, FeeBlockType.GovernmentRecordingFees, FeeBlockType.GovernmentTaxes, FeeBlockType.CostsAndCredits, FeeBlockType.CreditPointFeeBlock };
        public static FeeBlockType[] CWClosingCostBlocks = new[] { FeeBlockType.GuaranteedLenderFees, FeeBlockType.LenderThirdPartyServiceFees, FeeBlockType.BuyerThirdPartyServiceFees, FeeBlockType.GovernmentRecordingFees, FeeBlockType.GovernmentTaxes, FeeBlockType.CostsAndCredits, FeeBlockType.CreditPointFeeBlock };

        public static SystemCmsPage[] SystemCmsPages = new[] { new SystemCmsPage { SystemPageId = 1, Name = "Our Process", Url = "/OurProcess", SystemName = "OurProcess" },
            new SystemCmsPage { SystemPageId = 2, Name = "Contact Us", Url = "/Contact-Us", SystemName = "ContactUs" },
        new SystemCmsPage { SystemPageId = 3, Name = "InTheNews", Url = "/InTheNews", SystemName = "InTheNews" },
        new SystemCmsPage { SystemPageId = 4, Name = "LoanPrograms", Url = "/LoanPrograms", SystemName = "LoanPrograms" },
        new SystemCmsPage { SystemPageId = 5, Name = "Careers", Url = "/Careers", SystemName = "Careers" },
        new SystemCmsPage { SystemPageId = 6, Name = "AboutUs", Url = "/About-Us", SystemName = "AboutUs" },
        new SystemCmsPage { SystemPageId = 7, Name = "Loan Application Landing Page", Url = "/LoanApplication", SystemName = "LoanApplication" },
        new SystemCmsPage { SystemPageId = 8, Name = "Register", Url = "/Account/Register", SystemName = "Register" },
        new SystemCmsPage { SystemPageId = 9, Name = "Login", Url = "/Account/Login", SystemName = "Login" },
        new SystemCmsPage { SystemPageId = 10, Name = "Password Reset Page", Url = "/Account/SendResetPasswordRequest", SystemName = "PasswordReset" },
        new SystemCmsPage { SystemPageId = 11, Name = "Purchase", Url = "/Purchase", SystemName = "Purchase" },
        new SystemCmsPage { SystemPageId = 12, Name = "Refinance", Url = "/Refinance", SystemName = "Refinance" },
        new SystemCmsPage { SystemPageId = 13, Name = "Loan Finder", Url = "/Mortgage-Rates", SystemName = "LoanFinder" },
        new SystemCmsPage { SystemPageId = 14, Name = "Rates", Url = "/Mortgage-Rates/Rates", SystemName = "Rates" },

        new SystemCmsPage { SystemPageId = 15, Name = "Purchase Contract", Url = "/Purchase/Contract", SystemName = "PurchaseContract" },
        new SystemCmsPage { SystemPageId = 16, Name = "Purchase Preapproval", Url = "/Purchase/Preapproval", SystemName = "PurchasePreapproval" },
        new SystemCmsPage { SystemPageId = 17, Name = "Purchase Researching", Url = "/Purchase/Researching", SystemName = "PurchaseResearching" },
        new SystemCmsPage { SystemPageId = 18, Name = "Refinance Lower Payments", Url = "/Refinance/LowerPayments", SystemName = "RefinanceLowerPayments" },
        new SystemCmsPage { SystemPageId = 19, Name = "Refinance Cash Out", Url = "/Refinance/Cash-Out", SystemName = "RefinanceCashOut" },
        new SystemCmsPage { SystemPageId = 20, Name = "Refinance Debt Consolidation", Url = "/Refinance/Debt-Consolidation", SystemName = "RefinanceDebtConsolidation" },
            new SystemCmsPage { SystemPageId = 21, Name = "Reviews", Url = "/Reviews", SystemName = "Reviews" }
        };


        private static readonly Dictionary<Type, int> EntityList = new Dictionary<Type, int>()
        {
{typeof(CompanyPhoneInfo), 1},
{typeof(Department), 2},
{typeof(LoanGoal), 3},
{typeof(LoanPurpose), 4},
{typeof(Language), 5},
{typeof(GenericAttribute), 6},
{typeof(ReviewSiteAccount), 7},
{typeof(ZipCode), 8},
{typeof(BankRateInstance), 9},
{typeof(AddressInfo), 10},
{typeof(Acl), 11},
{typeof(PaidBy), 12},
{typeof(EmailAccount), 13},
{typeof(SecondLienType), 14},
{typeof(BankRateArchive), 15},
{typeof(PropertyUsage), 16},
{typeof(Customer), 17},
{typeof(LoanApplication), 18},
{typeof(BankRatePricingRequest), 19},
{typeof(ProfitTable), 20},
{typeof(SetupTable), 21},
{typeof(ProductQualifier), 22},
{typeof(AdsPageLocation), 23},
{typeof(RateArchive), 24},
{typeof(OfficeMetroBinder), 25},
{typeof(BusinessUnit), 26},
{typeof(CmsPage), 27},
{typeof(LogItem), 28},
{typeof(ContactInfo), 29},
{typeof(Country), 30},
{typeof(EmailLog), 31},
{typeof(BankRateParameter), 32},
{typeof(Formula), 33},
{typeof(FeeDetailPaidBy), 34},
{typeof(AdsPromotion), 35},
{typeof(Region), 36},
{typeof(ProductAmortizationType), 37},
{typeof(Contact), 38},
{typeof(ReviewPosted), 39},
{typeof(InvestorProduct), 40},
{typeof(RangeSet), 41},
{typeof(QuestionType), 42},
{typeof(TemplateForm), 43},
{typeof(Scheduler), 44},
{typeof(Employee), 45},
{typeof(Team), 46},
{typeof(EmailAttachmentsLog), 47},
{typeof(Note), 48},
{typeof(AdsSize), 49},
{typeof(Borrower), 50},
{typeof(Vendor), 51},
{typeof(EmployeeLicense), 52},
{typeof(Question), 53},
{typeof(EmailTracking), 54},
{typeof(RateServiceParameter), 55},
{typeof(Setting), 56},
{typeof(CustomerType), 57},
{typeof(AdsType), 58},
{typeof(Subscription), 59},
{typeof(Investor), 60},
{typeof(LoanRequest), 61},
{typeof(BenchMark), 62},
{typeof(ProductClass), 63},
{typeof(QuestionGroup), 64},
{typeof(BenchMarkRate), 65},
{typeof(LeadSourceType), 66},
{typeof(QueuedEmail), 67},
{typeof(WorkQueue), 68},
{typeof(NoteDetail), 69},
{typeof(CurrentRate), 70},
{typeof(Visitor), 71},
{typeof(SessionLogDetail), 72},
{typeof(OpportunityFee), 73},
{typeof(LeadSource), 74},
{typeof(TemplateFormPlot), 75},
{typeof(QuestionSection), 76},
{typeof(LoanIndexType), 77},
{typeof(Subordinate), 78},
{typeof(EmploymentInfo), 79},
{typeof(SubscriptionSection), 80},
{typeof(ReviewContact), 81},
{typeof(WorkQueueTracking), 82},
{typeof(FileAttachment), 83},
{typeof(WorkFlow), 84},
{typeof(SessionLog), 85},
{typeof(PrepayPenalty), 86},
{typeof(Branch), 87},
{typeof(Activity), 88},
{typeof(AuditTrail), 89},
{typeof(ReviewProperty), 90},
{typeof(BankRateStage), 91},
{typeof(ContactAddress), 92},
{typeof(BankRateServiceStage), 93},
{typeof(LocaleStringResource), 94},
{typeof(FeeBlock), 95},
{typeof(Campaign), 96},
{typeof(LoanType), 97},
{typeof(SystemEvent), 98},
{typeof(ReviewComment), 99},
{typeof(EscrowEntityType), 100},
{typeof(ActivityType), 101},
{typeof(VendorType), 102},
{typeof(QuestionOption), 103},
{typeof(PropertyTax), 104},
{typeof(BankRatePoint), 105},
{typeof(TemplateType), 106},
{typeof(CreditScore), 107},
{typeof(ReviewSite), 108},
{typeof(BankRateRequest), 109},
{typeof(RainMaker.Entity.Models.TimeZone), 110},
{typeof(MessageOnRule), 111},
{typeof(State), 112},
{typeof(ProductBestEx), 113},
{typeof(UserPermission), 114},
{typeof(BankRateTier), 115},
{typeof(FeeType), 116},
{typeof(TemplateFormSection), 117},
{typeof(OpportunityFeeDetailPaidBy), 118},
{typeof(City), 119},
{typeof(LoanDocType), 120},
{typeof(AdsGeoLocation), 121},
{typeof(OpportunityFeePaidBy), 122},
{typeof(Fee), 123},
{typeof(ProductFamily), 124},
{typeof(MessageTemplate), 125},
{typeof(StatusCause), 126},
{typeof(ProductType), 127},
{typeof(Position), 128},
{typeof(OpportunityStatusLog), 129},
{typeof(Rule), 130},
{typeof(LoanRequestDetail), 131},
{typeof(PmiCompany), 132},
{typeof(Sitemap), 133},
{typeof(FeeDetail), 134},
{typeof(StatusList), 135},
{typeof(UserRole), 136},
{typeof(PropertyType), 137},
{typeof(SystemEventLog), 138},
{typeof(Template), 139},
{typeof(Agency), 140},
{typeof(LoanLockPeriod), 141},
{typeof(ProductAd), 142},
{typeof(OpportunityFeeDetail), 143},
{typeof(AdsSource), 144},
{typeof(Adjustment), 145},
{typeof(LockDaysOnRule), 146},
{typeof(SettingGroup), 147},
{typeof(Opportunity), 148},
{typeof(AusProcessingType), 149},
{typeof(OwnType), 150},
{typeof(UserProfile), 151},
{typeof(TriggerValue), 152},
{typeof(County), 153},
{typeof(NoteSubject), 154},
{typeof(CampaignTrigger), 155},
{typeof(Product), 156},
{typeof(SubscriptionGroup), 157},
{typeof(ResidencyType), 158},
{typeof(UserResetPasswordKey), 159},
{typeof(QuoteResult), 160},
{typeof(LoanToValue), 161},
{typeof(BankRateProduct), 162},
{typeof(BlacklistIp), 163},
{typeof(OfficeHoliday), 164},
{typeof(FollowUp), 165},
{typeof(LeadType), 166},
{typeof(LockStatusList), 167},
{typeof(LockStatusCause), 168},
{typeof(OpportunityLockStatusLog), 169},
{typeof(FollowUpPurpose), 170},
{typeof(FollowUpActivity), 171},
{typeof(FeeCategory), 172},
{typeof(NoteTopic), 173},
{typeof(DebtToIncomeRatio), 174},
{typeof(CountyType), 175},
{typeof(ThirdPartyStatusList), 176},
{typeof(ContactPhoneInfo), 177},
{typeof(ContactEmailInfo), 178},
{typeof(Gender), 179},
{typeof(IncomeType), 180},
{typeof(TitleHeldWith), 181},
{typeof(SupportPaymentType), 182},
{typeof(Race), 183},
{typeof(JobType), 184},
{typeof(OwnershipType), 185},
{typeof(OtherEmploymentIncomeType), 186},
{typeof(MaritalStatusList), 187},
{typeof(LiabilityType), 188},
{typeof(InformationMedium), 189},
{typeof(TitleEstate), 190},
{typeof(FamilyRelationType), 191},
{typeof(Notification), 192},
{typeof(LoanContact), 193},
{typeof(Ethnicity), 194},
{typeof(FollowUpPriority), 195},
{typeof(MbsSecurity), 196},
{typeof(MbsRateArchive), 197},
{typeof(MbsRate), 198},
{typeof(ScheduleActivity), 199},
{typeof(HolidayType), 200},
{typeof(MilitaryStatusList), 201},
{typeof(MilitaryBranch), 202},
{typeof(DemographicMedium), 203},
{typeof(ResidencyState), 204},
{typeof(LoanDocumentType), 205},
{typeof(LoanDocumentSubType), 206},
{typeof(MilitaryAffiliation), 207},
{typeof(VaOccupancy), 208},
{typeof(LoanDocumentStatusList), 209},
{typeof(TitleLandTenure), 210},
{typeof(LoanDocument), 211},
{typeof(EthnicityDetail), 212},
{typeof(MortgageEducationType), 213},
{typeof(BankRuptcy), 214},
{typeof(ProjectType), 215},
{typeof(AccountType), 216},
{typeof(LoanPurposeProgram), 217},
{typeof(MaritalStatusType), 218},
{typeof(TitleManner), 219},
{typeof(RaceDetail), 220},
{typeof(ConsentType), 221},
{typeof(TransactionInfo), 222},
{typeof(TitleTrustInfo), 223},
{typeof(EducationFormat), 224},
{typeof(GiftSource), 225},
{typeof(AssetType), 226},
{typeof(DefaultLoanParameter), 229},
{typeof(UserResetPasswordLog), 230},
        };

        public static int GetEntityType(Type t)
        {
            int result = -1;
            if (EntityList.ContainsKey(t))
            {
                result = EntityList[t];
            }
            else
            {
                // throw new Exception("Entity Not Registered in Entity List");
            }
            return result;
        }
        public const int CompainRules = 91;

        //scheduler id used for trigger base lead assignment schedule
        public const int TriggerLeadAssignScheduleId = 2;

        // Status Constraints from Status table added by Asghar

        public static string CompareCookieName = "RSCompareCookie";
        public static string CompareLoanIdCookieName = "RSCompareLoanId";

        public const string ContactInfoOffice = "OFFICE";
        public const string Employee = "EMPLOYEE";
        public const string InActiveSymbol = "##";
        public const string InActiveString = " [Inactive]";
        public const string Referrer = "AHCLending";

        //number of retries for sending email if sending fails.
        public const int ReTries = 2;

        //Resource Constant

        public const string ValidPasswordMatch = "common.error.passwordmatch"; // For Confirm Password Fields

        public const string ValidTwoLetterIsoCode = "common.error.twoisocode"; // Two Letter Iso Code for Country

        public const string ValidThreeLetterIsoCode = "common.error.threeisocode"; // Three Letter Iso Code for Country

        public const string ValidStringLength = "common.error.stringlength"; // Length check just for View

        public const string ValidStringLengthTwoHundred = "common.error.stringlength200"; // Length check just for View

        public const string ValidLengthCheck = "common.error.lengthcheck"; // Length check just for model

        public const string ValidNotEmpty = "common.error.noempty"; // For Required


        public const string ValidIsEmail = "common.error.email"; // For Email Message
        //public const string ValidIsEmailRex = @"^\w+([-.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public const string ValidIsEmailRex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public const string ValidIsEmailRexBoot = @"^\w+([-.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public const string ValidPopupEmailRex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        public const string ValidIsMultiEmailRexBoot = @"(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*"; // Bootstrap email regex

        public const string ValidIMail = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";



        public const string ValidIsPropertyAddress = "common.error.propertyaddress";

        public const string ValidNoHtml = "common.error.nohtml"; // For Not allowing HTML

        //validate phone length
        public const string ValidIsPhoneRexBoot = @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}";
        public const string ValidIsPhoneRex = @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}";

        //validate us phone numbers
        public const string ValidUSPhoneRexBoot = @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$";
        public const string ValidUSPhoneRex = @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$";

        public const string ValidIsPhone = "common.error.phone"; // For Phone Message
        public const string PasswordLength = "common.error.passwordlength"; // For Phone Message

        public const string ValidNotUnique = "common.error.notunique"; // For Unique Value

        public const string ValidAlreadyExists = "common.error.exists"; // For existing values

        public const string ValidAlreadyAssigned = "common.error.assign"; // For existing values

        public const string AtleastOnePrimary = "common.error.oneprimary"; // For primary values

        public const string ValidPhoneAlreadyExists = "common.error.PhoneExists";//phone number already exist in current contact

        public const string ValidDateTime = "common.error.datetime"; // For Date Time Value

        public const string ValidAccessDenied = "common.error.accessdenied"; // Access Denied

        public const string ValidMaxLengthCheck = "common.error.maxlengthcheck"; // 100 Maxlength of Fields

        public const string ValidMaxEmailLengthCheck = "common.error.MaxEmailLengthCheck"; // 150 Max length for email field


        public const string ValidIntLengthCheck = "common.error.intlength"; // 2 to 9 Int length of Fields
        public const string ValidIntLengthCheckForYear = "common.error.intyearlength"; // 4 Int length of Fields
        public const string ValidIntLengthCheckForYearForTwoYears = "common.error.intyearlengthfortwoyears"; // 2 Int length of Fields
        public const string ValidIntOneLengthCheck = "common.error.intOnelength"; // 1 to 9 Int length of Fields
        public const string ValidDecimalLengthCheck = "Common.Error.DecimalLength"; // 1 to 17 decimal length of Fields
        public const string ValidEscrowAmountLengthCheck = "Common.Error.EscrowAmount"; // 1 to 17 decimal length of Fields

        public const string ValidIntLengthCheckStreet = "common.error.intlengthStreet"; // 2 to 199 Int length of Fields

        public const string ValidInvalidExpression = "common.error.invalidexpression"; // invalidexpression in Fields 

        public const string ValidGreaterThanZero = "common.error.greaterthanzero"; // Greater than Zero in Fields 

        public const string ValidIsCron = "common.error.croncheck"; // Cron check in Fields 

        public const string ValidNameCheck = "common.error.namecheck"; // Name check in Fields 

        public const string ValidFileCheck = "common.error.filecheck"; // File check check in Fields 

        public const string ValidIsPass = "common.error.pass"; // For Password Fields 

        public const string ValidIsConfirmPassword = "Common.Error.ConfirmPassword"; // Confirm Password Fields 

        public const string ValidIsFirstName = "common.error.firstname"; // First Name Fields  Regex Constants.ValidIsAlphabetRexBoot

        public const string ValidIsLastName = "common.error.lastname"; // Last Name in Fields  

        public const string ValidIsZipCode = "common.error.zipcode"; // Zip Code in Fields 
        public const string ValidNotZipCode = "common.error.invalidzipcode"; // Zip Code in Fields 
        public const string ValidSelectList = "common.error.selectlist"; // For all checkboxes and radio buttons and dropdown
        public const string ValidSelectListRex = "([a-z0-9A-Z+&%$#@(){}* ,.'-]+)";
        public const string ValidSelectListRexBoot = "^[a-z0-9A-Z+&%$#@(){}* ,.'-]+$";

        public const string ValidAlphaNumSpecial = "Common.Error.IsAlphanumericSpecial"; // For Alphabets , Numbers  and .-'$&#  in field
        public const string ValidAlphaNumSpecialRex = "([a-z0-9A-Z+&%$#,.'-]+)";
        public const string ValidAlphaNumSpecialRexBoot = "^[a-z0-9A-Z+&%$#,.'-]+$";

        public const string ValidAlphaNumSpecialUnderscoreHyphen = "Common.Error.IsAlphaNumSpecialUnderscoreHyphen"; // For Alphabets , Numbers,underscore & Hyphen  in field
        public const string ValidAlphaNumSpecialUnderscoreHyphenRex = "([a-z0-9A-Z_-]+)";
        public const string ValidAlphaNumSpecialUnderscoreHyphenRexBoot = "^[a-z0-9A-Z_-]+$";


        public const string ValidAlphaNumSpecialSpace = "common.error.isalphaspecialspace"; // For Alphabets , Numbers ,Space and .-'$&#  in field
        public const string ValidAlphaNumSpecialSpaceRex = "([a-z0-9A-Z +&%$#,.'-]+)";
        public const string ValidAlphaNumSpecialSpaceRexBoot = "^[a-z0-9A-Z +&%$#,.'-]+$";

        public const string ValidSmtpProtocol = "common.error.issmtpprotocol"; //SMTP 
        public const string ValidSmtpProtocolRex = @"[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(/\S*)?$";

        public const string ValidNotNumSpecial = "common.error.isalphaspace"; // For Alphabets and Space in field
        public const string ValidNotNumSpecialRex = "([a-zA-Z ]+)";
        public const string ValidNotNumSpecialRexBoot = "^[a-zA-Z ]+$";

        public const string ValidNotSpecialSpace = "common.error.isalphanumeric"; // Alphabet and Numeric
        public const string ValidNotSpecialSpaceRex = "([a-zA-Z0-9]+)";
        public const string ValidNotSpecialSpaceRexBoot = "^[a-z0-9A-Z]+$";

        public const string ValidAlphaNumericDotCharacters = "common.error.isalphanumericwithdot"; // Alphabet and Numeric and dot
        public const string ValidAlphaNumericDotCharactersRex = "([a-zA-Z0-9.]+)";
        public const string ValidAlphaNumericDotCharactersRexBoot = "^[a-z0-9A-Z.]+$";

        public const string InvalidAliasMessage = "Common.Error.AlphanumericSpace"; //For Alphabets Numbers and Space in field
        public const string ValidAliasRex = "([a-z0-9A-Z ]+)";
        public const string ValidAliasRexBoot = "^[a-z0-9A-Z ]+$";

        public const string ValidNotSpecial = "Common.Error.AlphanumericSpace";
        public const string ValidNotSpecialRex = "([a-z0-9A-Z -/.]+)";
        public const string ValidNotSpecialRexBoot = "^[a-z0-9A-Z -/.]+$";

        public const string ValidIsNumber = "common.error.isnumber";  // For Numbers
        public const string ValidIsNumberRex = "([0-9]+)";
        public const string ValidIsNumberRexBoot = "^[0-9]+$";

        public const string ValidIsDecimal = "common.error.isdecimal";  // For Numbers

        public const string ValidIsUrlRex = @"^(http|ftp|https|www)://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?$";
        public const string ValidIsUrl = "common.error.isurl";

        public const string ValidIsAlphabet = "common.error.isalphabet"; // For Alphabats 
        public const string ValidIsAlphabetRex = "([a-zA-Z ]+)";
        public const string ValidIsAlphabetRexBoot = "^[a-zA-Z ]+$";

        public const string ValidDescription = "common.error.description"; // For all Description Fields
        public const string ValidDescriptionRex = "([a-z0-9A-Z+&%$#@(){}* ,.'-_/]+)";
        public const string ValidDescriptionRexBoot = "^[a-z0-9A-Z+&%$#@(){}* ,.'-_/]+$";

        //public const string ValidStringResource = "common.error.StringResourceName"; // For all Description Fields
        //public const string ValidStringResourceRex = "([a-zA-Z0-9._]+)";
        //public const string ValidStringResourceRexBoot = "^[a-zA-Z0-9._]+$";

        //public const string ValidNameWithHCA = "common.error.nameWithHCA"; // 
        //public const string ValidNameWithHCARex = "([a-z0-9A-Z+&%$#@(){}* ,.'-_/]+)";
        //public const string ValidNameWithHCARexBoot = "^[a-z0-9A-Z+&%$#@(){}* ,.'-_/]+$";

        //public const string ValidNameWithHCANum = "common.error.nameWithHCANum"; // 
        //public const string ValidNameWithHCANumRex = "([a-z0-9A-Z+&%$#@(){}* ,.'-_/]+)";
        //public const string ValidNameWithHCANumRexBoot = "^[a-z0-9A-Z+&%$#@(){}* ,.'-_/]+$";

        public const string ValidEmailNotPrimary = "common.error.primaryemailreq"; // email is not primary in contact
        public const string ValidPhoneNotPrimary = "common.error.primaryphonereq"; // phone is not primary in contact
        public const string RequiredField = "common.error.RequiredField"; // This field is required

        public const string ValidFirstNLastName = "common.error.FirstNLastName"; // allow Alphabets, Space and .- Use where take input for first/last/middle/nick name
        public const string ValidFirstNLastNameRex = "([a-zA-Z .'-]+)";
        public const string ValidFirstNLastNameRexBoot = "^[a-zA-Z .'-]+$";

        public const string ValidAllowAllCharacter = "common.error.InvalidCharacters"; // Allow all characters
        public const string ValidAllowAllCharacterRex = "([a-z0-9A-Z+&%$#@(){}* ,.'-_~`!|\"/]+)";
        public const string ValidAllowAllCharacterRexBoot = "^[a-z0-9A-Z+&%$#@(){}* ,.'-_~`!|\"/]+$";

        public const string ValidAlphaNumHyphen = "common.error.IsValidAlphaNumHyphen"; // Allow alphabets  numeric & hyphen
        public const string ValidAlphaNumHyphenRex = "^[a-z0-9A-Z-]+$";



        public const string ValidUserName = "common.error.ValidateUserName"; // allow Alphabets & number and @.-_ Use in forms like register/signup etc.
        public const string ValidUserNameRex = "([a-zA-Z0-9@.\\-_]+)";
        public const string ValidUserNameRexBoot = "^[a-z0-9A-Z@.\\-_]+$";

        public const string ValidMaxCharacterOnly = "common.error.MaxCharacter"; // message for validate max character with no minimum character

        public const string ValidNotRange = "common.error.OverlappingRange"; // For Overlapping Range

        public const string ValidTime = "^(?:(?:0?[0-9]|1[0-2]):[0-5][0-9]\\s?(?:(?:[Aa]|[Pp])[Mm])?|(?:1[3-9]|2[0-3]):[0-5][0-9])$";
        public const string ValidTimeErroMessage = "common.error.InvalidTime";

        public const string ValidCronExpression = "common.error.InValidCronExpression";

        public const string AdsSourceMesasgeError = "common.AdsourceMessage.NotUnique"; // For Unique Value

        public const int SystemUserId = 1;

        public const string PrimaryCustomer = "PRIMARYCUSTOMER";
        public const int PrimaryCustomerValue = 1;
        public const int SecondaryCustomerValue = 2;
        public const string SecondaryCustomer1 = "SECONDARYCUSTOMER1";
        public const string SecondaryCustomer2 = "SECONDARYCUSTOMER2";
        public const string SecondaryCustomer3 = "SECONDARYCUSTOMER3";
        public const string OpportunityOriginatorCsr = "OPPORTUNITYORIGINATORCSR";
        public const string OpportunityBranchEmail = "OpportunityBranchEmail";
        public const string OpportunityBusinessChannelEmail = "OpportunityBusinessChannelEmail";
        public const string DefaultSettingEmail = "DefaultSettingEmail";
        public const string RememberMe = "RsRememberMe";
        public const int DefaultState = 0;
        public const string AllActiveStates = "ALLACTIVESTATES";
        public const string AllActiveStatesDropDown = "AllActiveStatesDropDown";
        public const string AllActiveCountiesDropDown = "AllActiveCountiesDropDown";
        public const string EntityTypeCache = "EntityTypeCache";
        public const string AllCounties = "ALLCOUNTIES";
        public const string AllCities = "ALLCITIES";
        public const string AllStates = "ALLSTATES";
        public const string BlockIpList = "BLOCKIPLIST";
        public const string TemplateUserResetPassword = "UserResetPassword";
        public const int TopCreditScoreId = 780;
        public const int LowestCreditScoreId = 620;
        public const int BankRateLowestCreditScoreId = 600;
        public const int LendingTreeThirdPartyId = 3;
        // public const int LendingTreeLeadSourceId = 2;
        public const int ZillowThirdPartyId = 2;
        public const int BankRateId = 4;
        public const string BusinessUnitPhone = "BusinessUnitPhone";
        public const string BranchPhone = "BranchPhone";
        public const string BranchName = "BranchName";
        public const string HomeAnimationText = "HomeAnimationText";
        public const string BusinessUnitName = "BusinessUnitName";
        public const string BranchAddress = "BranchAddress";
        public const string BranchNmls = "BranchNmls";
        public const string BusinessUnitUanPhone = "BusinessUnitUANPhone";
        public const string BusinessUnitUrl = "BusinessUnitUrl";
        public const string CityStateZipCode = "CityStateZipCode";
        // public const int ZillowLeadSourceId= 6;
        public const int DefaultScheduleId = 1;

        // Alt Tags String Resources
        public const string LogoFooterAltText = "LogoFooterAltText";
        public const string LogoMobileAltText = "LogoMobileAltText";
        public const string LogoAltText = "LogoAltText";

        public const string RateAssumptions = "Page.RateAssumptions";
        public const string CompanyInfo = "CompanyInfo";
        public const string Title = "Title";
        public const string FooterText = "FooterText";
        public const string FooterTextLo = "FooterTextLO";

        public const string EncryptionKey = "r@in$0f7";
        public const string Testimonials = "Testimonials";
        public const string TodaysRates = "TodaysRates";

        //Customer site memory cache constants
        public const string CacheDefaultLeadSource = "CacheDefaultLeadSource";
        public const string CacheDefaultLoanPurpose = "CacheDefaultLoanPurpose";

        public const string Error404 = "Error-404";
        public const string ErrorGeneral = "Error-500";
        public const string DynamicPageNotFound = "DynamicPageNotFound";
        public const string LenderAddress = "LenderAddress";
        public const string LeadGenQuestion = "LeadGenQuestion";

        public const string LeadGenPhoneError = "LeadGen.Error.Phone";
        public const string LeadGenPropertyValueError = "LeadGen.Error.PropertyPrice";
        public const string LeadGenPurchasePriceError = "LeadGen.Error.PurchasePrice";
        public const string LeadGenFirstMortgageError = "LeadGen.Error.FirstMortgagePrice";
        public const string LeadGenCashoutPriceError = "LeadGen.Error.CashoutPrice";
        public const string LeadGenLienPriceError = "LeadGen.Error.LienBalancePrice";
        public const string LeadGenSessionExpire = "LeadGen.Error.SessionExpire";
        public const string BankRateBranchName = "BankRateBranchName";
        public const string BusinessUnitEmail = "BusinessUnitEmail";
        public const string ShowMoreRatesError = "Error.ShowMoreRates";

        //meta tags for pages start
        //explore loan option
        public const string PageExploreMetaTitle = "Page.Explore.Meta.Title";
        public const string PageExploreMetaDescription = "Page.Explore.Meta.Description";
        public const string PageExploreMetaKeywords = "Page.Explore.Meta.Keywords";
        public const string PageExploreTitle = "Page.Explore.Title";

        //explorer loan rates option
        public const string PageRateOptionMetaTitle = "Page.RateOption.Meta.Title";
        public const string PageRateOptionMetaDescription = "Page.RateOption.Meta.Description";
        public const string PageRateOptionMetaKeywords = "Page.RateOption.Meta.Keywords";
        public const string PageRateOptionTitle = "Page.RateOption.Title";

        //product rates programs
        //public const string PageRateProgramMetaTitle = "Page.RateProgram.Meta.Title";
        //public const string PageRateProgramMetaDescription = "Page.RateProgram.Meta.Description";
        //public const string PageRateProgramMetaKeywords = "Page.RateProgram.Meta.Keywords";
        //public const string PageRateProgramTitle = "Page.RateProgram.Title";

        public const string PageRateProgramMetaTitle = "Page.{0}.Meta.Title";
        public const string PageRateProgramMetaDescription = "Page.{0}.Meta.Description";
        public const string PageRateProgramMetaKeywords = "Page.{0}.Meta.Keywords";
        public const string PageRateProgramTitle = "Page.{0}.Title";
        public const string PageRateProgramHeading = "Page.{0}.Heading";
        public const string PageRateProgramDescription = "Page.{0}.Description";

        //loan application langding page
        public const string PageLoanAppLandingMetaTitle = "Page.LoanAppLanding.Meta.Title";
        public const string PageLoanAppLandingMetaDescription = "Page.LoanAppLanding.Meta.Description";
        public const string PageLoanAppLandingMetaKeywords = "Page.LoanAppLanding.Meta.Keywords";
        public const string PageLoanAppLandingTitle = "Page.LoanAppLanding.Title";

        //loan application langding page - bankrate
        public const string PageLoanAppLandingBankMetaTitle = "Page.LoanAppLandingBank.Meta.Title";
        public const string PageLoanAppLandingBankMetaDescription = "Page.LoanAppLandingBank.Meta.Description";
        public const string PageLoanAppLandingBankMetaKeywords = "Page.LoanAppLandingBank.Meta.Keywords";
        public const string PageLoanAppLandingBankTitle = "Page.LoanAppLandingBank.Title";

        //Signup page
        public const string PageSignupMetaTitle = "Page.Signup.Meta.Title";
        public const string PageSignupMetaDescription = "Page.Signup.Meta.Description";
        public const string PageSignupMetaKeywords = "Page.Signup.Meta.Keywords";
        public const string PageSignupTitle = "Page.Signup.Title";

        //login page
        public const string PageLoginMetaTitle = "Page.Login.Meta.Title";
        public const string PageLoginMetaDescription = "Page.Login.Meta.Description";
        public const string PageLoginMetaKeywords = "Page.Login.Meta.Keywords";
        public const string PageLoginTitle = "Page.Login.Title";

        //forgot password page
        public const string PageForgotPassMetaTitle = "Page.ForgotPass.Meta.Title";
        public const string PageForgotPassMetaDescription = "Page.ForgotPass.Meta.Description";
        public const string PageForgotPassMetaKeywords = "Page.ForgotPass.Meta.Keywords";
        public const string PageForgotPassTitle = "Page.ForgotPass.Title";

        //lo profile page
        public const string PageLoProfileMetaTitle = "Page.LoProfile.Meta.Title";
        public const string PageLoProfileMetaDescription = "Page.LoProfile.Meta.Description";
        public const string PageLoProfileMetaKeywords = "Page.LoProfile.Meta.Keywords";
        public const string PageLoProfileTitle = "Page.LoProfile.Title";

        //loan finder purpose page
        public const string PageLeadGenMetaTitle = "Page.LeadGen.Meta.Title";
        public const string PageLeadGenMetaDescription = "Page.LeadGen.Meta.Description";
        public const string PageLeadGenMetaKeywords = "Page.LeadGen.Meta.Keywords";
        public const string PageLeadGenTitle = "Page.LeadGen.Title";

        //loan finder goal page - purchase
        public const string PageLeadGenGoalMetaTitle = "Page.LeadGenGoal.Meta.Title";
        public const string PageLeadGenGoalMetaDescription = "Page.LeadGenGoal.Meta.Description";
        public const string PageLeadGenGoalMetaKeywords = "Page.LeadGenGoal.Meta.Keywords";
        public const string PageLeadGenGoalTitle = "Page.LeadGenGoal.Title";

        //loan finder goal page - Refinance
        public const string PageLeadGenGoalRefMetaTitle = "Page.LeadGenRefGoal.Meta.Title";
        public const string PageLeadGenGoalRefMetaDescription = "Page.LeadGenRefGoal.Meta.Description";
        public const string PageLeadGenGoalRefMetaKeywords = "Page.LeadGenRefGoal.Meta.Keywords";
        public const string PageLeadGenGoalRefTitle = "Page.LeadGenRefGoal.Title";

        //result page
        public const string PageResultMetaTitle = "Page.Result.Meta.Title";
        public const string PageResultMetaDescription = "Page.Result.Meta.Description";
        public const string PageResultMetaKeywords = "Page.Result.Meta.Keywords";
        public const string PageResultTitle = "Page.Result.Title";

        //Home page
        public const string HomePageTitle = "Page.Home.Title";
        public const string MetaTitle = "Page.Home.Meta.Title";
        public const string MetaKeyWords = "Page.Home.Meta.Keywords";
        public const string MetaDescription = "Page.Home.Meta.Description";

        //meta tags for pages end

        public const string MortgageQuoteFooter = "MortgageQuote.Footer";
        public const int UserDefaultRoleId = 3;

        //review page heading and contents
        public const string TopHeading = "Reviews.Top.Heading";
        public const string TopDescription = "Reviews.Top.Description";
        public const string BottomDescription = "Reviews.Bottom.Description";
        public const string BottomHeading1 = "Reviews.Heading.Bottom1";
        public const string BottomHeading2 = "Reviews.Heading.Bottom2";

        //support role name
        public const string SupportTeamRoleName = "SupportTeam";

    }
    public static class AuthorizePermissions
    {
        public const string AdminLead = "AdminLead.Create";
        public const string ThirdPartyLeadInfo = "ThirdPartySsnAndPrice.View";
    }
    // Grid names shouldn't have any number or special character
    public class GridNames
    {
        public const string Region = "Region";
        public const string Employee = "Employee";
        public const string Country = "Country";
        public const string State = "State";
        public const string County = "County";
        public const string City = "City";
        public const string Campaign = "Campaign";
        public const string TemplateAttachment = "TemplateAttachment";
        public const string Template = "Template";
        public const string CampaignScheduler = "CampaignScheduler";
        public const string CampaignRules = "CampaignRules";
        public const string UserProfile = "UserProfile";
        public const string UserRole = "UserRole";
        public const string UserPermission = "UserPermission";
        public const string CmsPage = "CmsPage";
        public const string Setting = "Setting";
        public const string EmployeeProfile = "EmployeeProfile";
        public const string StringResource = "StringResource";
        public const string LeadGrid = "LeadGrid";
        public const string MergeLead = "MergeLead";
        public const string LoanPurpose = "LoanPurpose";
        public const string RecentLoanRequest = "RECENTQUOTE";
        public const string Customer = "Customer";
        public const string EmailTracking = "EmailTracking";
        public const string WorkQueueTracking = "WorkQueueTracking";
        public const string CampaignActivity = "CampaignActivity";
        public const string AuditTrail = "AuditTrail";
        public const string Status = "Status";
        public const string Team = "Team";
        public const string Rule = "Rule";
        public const string EmailAccount = "EmailAccount";
        public const string OppAssignmentLog = "OppAssignmentLog";
        public const string Fee = "Fee";
        public const string FeeDetails = "FeeDetails";
        public const string Formula = "Formula";
        public const string Branch = "Branch";
        public const string Product = "Product";
        public const string Acl = "ACL";
        public const string AllQuotes = "ALLQUOTES";
        public const string Position = "Position";
        public const string ReviewContact = "ReviewContact";
        public const string ReviewProperty = "ReviewProperty";
        public const string ReviewComment = "ReviewComment";
        public const string ReviewPosted = "ReviewPosted";
        public const string ReviewSite = "ReviewSite";
        public const string ReviewSiteAccount = "ReviewSiteAccount";
        public const string Review = "Review";
        public const string ProductType = "ProductType";
        public const string BusinessPartner = "BusinessPartner";
        public const string EscrowEntityType = "EscrowEntityType";
        public const string VendorType = "VendorType";
        public const string TemplateFormPlot = "TemplateFormPlot";
        public const string ProfitTable = "ProfitTable";


        public const string RuleMessage = "RuleMessage";
        public const string RateServiceParameter = "RateServiceParameter";
        public const string DefaultLoanRate = "DefaultLoanRate";
        public const string CurrentRate = "CurrentRate";
        public const string CurrentRateArchive = "CurrentRateArchive";

        public const string Adjustment = "Adjustment";
        public const string FeePaidByDefault = "FeePaidByDefault";
        public const string PropertyTax = "PropertyTax";
        public const string Insurance = "Insurance";
        public const string LoanApplication = "LoanApplication";

        public const string RangeSet = "RangeSet";
        public const string SecondLien = "SecondLien";
        public const string LeadSourceDetail = "LEADSOURCEDETAIL";
        public const string Department = "Department";
        public const string LoanGoal = "LoanGoal";
        public const string QuestionSection = "QuestionSection";
        public const string Question = "Question";
        public const string BestEx = "BestEx";
        public const string PropertyType = "PropertyType";
        public const string PropertyUsage = "PropertyUsage";
        public const string CreditRating = "CreditRating";
        public const string LockPeriod = "LockPeriod";
        public const string BusinessUnit = "BusinessUnit";
        public const string LoanType = "LoanType";
        public const string ProductClass = "ProductClass";
        public const string ProductQualifier = "ProductQualifier";
        public const string ProductFamily = "ProductFamily";
        public const string AmortizationType = "AmortizationType";
        public const string Agency = "Agency";
        public const string PrepayPenalty = "PrepayPenalty";
        public const string LoanIndexType = "LoanIndexType";
        public const string AusProcessingType = "AusProcessingType";
        public const string LeadSourceType = "LeadSourceType";
        public const string LeadSource = "LeadSource";
        public const string AdsPageLocation = "AdsPageLocation";
        public const string AdsType = "AdsType";
        public const string AdsSize = "AdsSize";
        public const string AdsPromotion = "AdsPromotion";
        public const string AdsSource = "AdsSource";
        public const string AdsGeoLocation = "AdsGeoLocation";
        public const string UserResetPasswordKey = "UserResetPasswordKey";
        public const string StatusCause = "StatusCause";
        public const string Investor = "Investor";
        public const string BenchMark = "BenchMark";
        public const string PendingOpportunity = "PendingOpportunity";
        public const string FollowUp = "FollowUp";
        public const string Five9LeadPosting = "Five9LeadPosting";
        public const string Five9LeadPostingLog = "Five9LeadPostingLog";
        public const string LeadType = "LeadType";
        public const string LockStatusList = "LockStatusList";
        public const string LockStatusCause = "LockStatusCause";
        public const string FeeCategory = "FeeCategory";
        public const string FollowUpPurpose = "FollowUpPurpose";
        public const string NoteTopic = "NoteTopic";
        public const string UnPickedLeads = "UnPickedLeads";
        public const string BankRateProduct = "BankRateProduct";
        public const string LoanToValue = "LoanToValue";
        public const string BankRateInstance = "BankRateInstance";
        public const string BankRatePoint = "BankRatePoint";
        public const string BankRateTier = "BankRateTier";
        public const string CreditScore = "CreditScore";
        public const string DebtToIncomeRatio = "DebtToIncomeRatio";
        public const string ThirdPartyLead = "ThirdPartyLead";
        public const string BlacklistIp = "BlacklistIp";
        public const string CompanyPhoneInfo = "CompanyPhoneInfo";
        public const string FollowUpPriority = "FollowUpPriority";
        public const string MbsRate = "MbsRate";
        public const string MbsRateArchive = "MbsRateArchive";
        public const string Subscription = "Subscription";
        public const string SubscriptionGroup = "SubscriptionGroup";
        public const string SubscriptionSection = "SubscriptionSection";
        public const string ScheduleActivityLog = "ScheduleActivityLog";
        public const string ScheduleActivity = "ScheduleActivity";
        public const string DesireRates = "DesireRates";
        public const string CampaignQueue = "CampaignQueue";
        public const string VisitorGrid = "VisitorGrid";
        public const string SessionLogGrid = "SessionLogGrid";
        public const string SessionLogDetailGrid = "SessionLogDetailGrid";
        public const string LoanRequestDetailGrid = "LoanRequestDetailGrid";
        public const string ResponseXmlGridViewer = "ResponseXmlGridViewer";
        public const string QuoteDetailGrid = "QuoteDetailGrid";
        public const string ZipCodeGrid = "ZipCodeGrid";
        public const string AdSourceMessageGrid = "AdSourceMessageGrid";
        public const string RateWidgetGrid = "RateWidgetGrid";
        public const string PromotionalProgramGrid = "PromotionalProgramGrid";
        public const string ProductCatalogGrid = "ProductCatalogGrid";
        public const string VortexConfigurationGrid = "VortexConfigurationGrid";
        public const string OfficeHolidayGrid = "OfficeHolidayGrid";
        public const string VortexUserSessionGrid = "VortexUserSessionGrid";
    }

    public static class SystemSettingKeys
    {
        // start pitch related settings
        public const string VortexServiceSettingIdForJobs = "VortexServiceSettingIdForJobs";
        public const string VortexPitchTimeout = "VortexPitchTimeout";
        public const string VortexServiceUrl = "VortexServiceUrl";
        public const string VortexPitchLogic = "VortexPitchLogic";
        public const string VortexAssignment = "VortexAssignment";
        public const string VortexBroadcastHopCount = "VortexBroadcastHopCount";
        public const string VortexPitchAssignmentExpiry = "VortexPitchAssignmentExpiry";
        // end pitch related settings

        public const string IsAuditEnabled = "AuditLogEnabledFlag";
        public const string SoftDelete = "SoftDeleteEnableFlag";
        public const string ManualFeeDefaultDays = "ManualFeeDefaultDays";
        //public const string CustomerDomainName = "CustomerDomainName";
        public const string AdminDomainUrl = "AdminDomainUrl";
        public const string WebApiUrl = "WebApiSiteUrl";
        public const string PasswordEncryptionKey = "PasswordEncryption";
        public const string SessionLogForAdminSite = "SessionLogForAdminSiteFlag";
        public const string SessionLogForCustomerSite = "SessionLogForCustomerSiteFlag";
        public const string SessionLogForThirdParty = "SessionLogForApiFlag";
        //public const string EmployeeMaxLeadQuota = "EmployeeMaxLeadQuota";
        //public const string EmployeePerDayLeadQuota = "EmployeePerDayLeadQuota";
        public const string EmployeeAutoLeadReassignMins = "AutoLeadReassignmentMinutes";
        public const string EmployeeAutoLeadReassignAllowed = "AutoLeadReassignmentFlag";
        //public const string EmployeeAutoLeadReassignLoginRequired = "EmployeeAutoLeadReassignLoginRequired";
        //public const string EmployeeManualLeadAssignmentAllowed = "EmployeeManualLeadAssignmentAllowed";
        public const string EmployeeMaxLeadAssignmentHopCount = "AutoLeadAssignmentHopCount";
        //public const string EmployeeAutoLeadAssignmentPerformanceRequired = "EmployeeAutoLeadAssignmentPerformanceRequired";
        public const string EmployeeAutoLeadAssignAllowed = "AutoLeadAssignmentFlag";
        //public const string EmployeeAutoLeadAssignmentServiceOn = "EmployeeAutoLeadAssignmentServiceOn";
        public const string RateUpdateServiceOn = "RateUpdateServiceOn";

        public const string LockPeriodMin = "LockPeriodMin";
        public const string LockPeriodMax = "LockPeriodMax";
        public const string TimeZone = "TimeZone";

        public const string DefaultSmtpHost = "DefaultSmtpHost";//"SMTPHost";
        public const string DefaultSslForEmail = "DefaultEmailSSL";//"UseSSLForEmail";
        public const string DefaultFromEmailAddress = "DefaultFromEmailAddress";//"FromEmailForCampaign";
        public const string DefaultEmailPassword = "DefaultEmailPassword";//"FromEmailPasswordForCampaign";
        public const string DefaultDisplayName = "DefaultEmailDisplayName";//"FromEmailForCampaignName";
        public const string DefaultSmtpPort = "DefaultSmtpPort";//"SMTPPort";
        public const string DefaultEmailUserName = "DefaultEmailUserName";//"TestEmail";
        public const string TestToEmailAddress = "TestToEmailAddress";
        public const string IsTestMode = "EmailTestModeFlag";

        public const string XDrivePath = "XDrivePath";
        public const string FtpHost = "FTPServer";
        public const string FtpUser = "FTPUser";
        public const string FtpPass = "FTPPass";
        public const string EmailViewTemplatePath = "EmailTemplateArchivePath";
        public const string EmailSavePath = "EmailArchivePath";
        public const string FtpTemplateAttachmentPath = "EmailTemplateRepositoryPath";
        public const string NumberOfArticlesPerPage = "NumberOfArticlesPerPage";
        public const string CmsHomePageFrequency = "SitemapHomePageFrequency";
        public const string CmsHomePagePriority = "SitemapHomePagePriority";
        //public const string OrganizationName = "OrganizationName";
        //public const string OrganizationAddress = "OrganizationAddress";
        //public const string OrganizationPhone = "OrganizationPhone";
        public const string GirdPageSize = "GridDefaultPageSize";
        public const string DefaultLockPeriodDays = "LockPeriodDefaultDays";

        public const string IdleSessionLimit = "IdleSessionLimit";
        public const string LogoffCountdown = "LogoffCountdown";
        public const string KeepAliveTime = "KeepAliveTime";

        /********* For Content of Customer Page *******************/
        public const string TopTitleImage = "TopTitleImage";
        public const string BottomTitleImage = "BottomTitleImage";
        public const string TopTitleOverlyImage = "TopTitleOverlyImage";
        public const string LandingRatingImage = "LandingRatingImage";
        public const string TopMobileTitleImage = "TopMobileTitleImage";
        public const string TopOverlayMobileTitleImage = "TopOverlayMobileTitleImage";


        //public const string TopMenuHtml = "TopMenuHtml";
        //ToDo: Need to get from company phone table
        //public const string TopPhone = "TopPhone";
        //public const string FooterHtml = "FooterHtml";
        public const string Adderss = "Address";
        public const string BUEmailLogo = "BUEmailLogo";
        public const string BUEmailUpdateButton = "BUEmailUpdateButton";
        public const string BUEmailOfferButton = "BUEmailOfferButton";
        public const string BUEmailIcon = "BUEmailIcon";
        public const string BUEmailViewClosingCostButton = "BUEmailViewClosingCostButton";

        public const string LoadingTextResult = "LoadingTextResult";
        public const string LoadingTextHome = "LoadingTextHome";

        //public const string Metatitle = "Metatitle";
        //public const string MetaKeyWords = "MetaKeyWords";
        //public const string MetaDescription = "MetaDescription";

        public const string EnabledAdrollTracking = "AdrollTrackingEnabledFlag";
        public const string EnabledGoogleAnalytic = "GoogleAnalyticEnabledFlag";
        public const string EnabledGoogleTagManager = "GoogleTagManagerFlag";
        public const string EnabledCrazyEgg = "CrazyEggEnabledFlag";

        public const string AdrollAdvId = "AdrollAdvId";
        public const string AdrollPixId = "AdrollPixId";
        public const string TrackingId = "GoogleTrackingId";
        public const string GoogleTagManagerId = "GoogleTagManagerId";
        public const string CrazyEggId = "CrazyEggId";


        public const string Favicon = "Favicon";
        //public const string RateLoaderImage = "RateLoaderImage";
        //public const string RateLoaderFlash = "RateLoaderFlash";
        /********* For Content of Customer Page *******************/
        /********* For Content of Customer AboutUs  ***************/
        // public const string AboutUsPhone = "AboutUsPhone";
        //public const string AboutUsMailForGeneralInq = "AboutUsMailForGeneralInq";

        /********* For Content of Customer AboutUs  *******************/

        /********* Email Title Image  *******************/
        //public const string EmailTitleImage = "EmailTitleImage";

        /********* Email Title Image  *******************/

        /********* Best Rate On  *******************/
        public const string BestRateOn = "BestRateOn";

        /********* Best Rate On  *******************/

        /********* FTP Folders  *******************/

        public const string LoanRequestFinalXmlFilePath = "QuoteXMLFilePath";
        public const string QuoteRequestTpXml = "QuoteRequestTpXmlPath";
        public const string QuoteResponseTpXml = "QuoteResponseTpXmlPath";
        public const string EmailTempAttachmentsFolderPath = "EmailTempAttachmentsFolderPath";
        public const string EmailTempAttachments = "EmailTempAttachmentPath";
        public const string EmailTemplatesAttachments = "EmailTemplatesAttachmentPath";
        public const string ApiRequestFilePath = "ApiRequestFilePath";
        public const string ApiMarksmanExportFilePath = "ApiMarksmanExportFilePath";
        public const string LendingTreeAck = "LendingTreeAckPath";
        public const string MarskmanResponse = "MarskmanResponse";
        public const string MbsRateUrl = "MbsRateURL";
        public const string MbsRateFileLocation = "MbsRateArchivePath";
        public const string FtpEmployeePhotoFolder = "EmployeePicturePath";
        public const string ApiBankRateFilePath = "ApiBankRateFilePath";
        public const string ApiLendingTreeFilePath = "ApiLendingTreeFilePath";
        public const string ApiZillowFilePath = "ApiZillowFilePath";
        public const string AmazonNotificationsPath = "AmazonNotificationsPath";


        /********* FTP Folders  *******************/

        /********* Twilio Constants  *******************/
        public const string TWILIO_ACCOUNT_SID = "TWILIO_ACCOUNT_SID";
        public const string TWILIO_AUTH_TOKEN = "TWILIO_AUTH_TOKEN";
        public const string TWILIO_CHAT_SERVICE_SID = "TWILIO_CHAT_SERVICE_SID";
        public const string TWILIO_WORKFLOW_SID = "TWILIO_WORKFLOW_SID";
        public const string TWILIO_WORKSPACE_SID = "TWILIO_WORKSPACE_SID";
        public const string TWILIO_APIKEY = "TWILIO_APIKEY";
        public const string TWILIO_API_SECRET = "TWILIO_API_SECRET";
        public const string TWILIO_PUSH_CREDENTIAL_SID = "TWILIO_PUSH_CREDENTIAL_SID";
        public const string TWILIO_SERVICE_SID = "TWILIO_SERVICE_SID";

        public const string VoiceMailDeletionGraceTimeInDays = "VortexVoiceMailDeletionGraceTimeInDays";
        public const string VoiceMailMaxLengthInSecs = "VortexVoiceMailMaxLengthInSecs";
        public const string InboundDialTimeOutInSecs = "VortexInboundDialTimeOutInSecs";
        public const string OutboundDialTimeOutInSecs = "VortexOutboundDialTimeOutInSecs";
        public const string DialTimeLimitInSecs = "VortexDialTimeLimitInSecs";
        public const string VortexPitchTimeoutInSecs = "VortexPitchTimeout";
        
        /********* Twilio Constants  *******************/

        //public const string TopOpportunityNotificationAlerts = "TopOpportunityNotificationAlerts";

        /********* Mortech  *******************/

        public const string MortechEmailAddress = "MortechPricingEmail";

        public const string MortechPricingStrategy = "MortechPricingStrategy";
        public const string MortechThreadCount = "MortechThreadCount";
        public const string MortechRequestUrl = "MortechRequestUrl";
        public const string MortechRequestId = "MortechRequestId";
        public const string MortechCustomerId = "MortechCustomerId";
        public const string MortechUserName = "MortechUserName";
        public const string MortechPassword = "MortechPassword";
        public const string MortechThirdPartyName = "MortechThirdPartyName";
        public const string MortechLicenseKey = "MortechLicenseKey";
        public const string MortechDisableEligibility = "MortechDisableEligibility";
        public const string MortechStrictEligibility = "MortechStrictEligibility";
        public const string MortechTargetPrice = "MortechTargetPrice";
        public const string MortechCoverageType = "MortechCoverageType";
        public const string MortechNoMi = "MortechNoMi";
        public const string MortechView = "MortechView";
        public const string MortechProductPerRequest = "MortechProductPerRequest";

        /********* For Loan Estimation *******************/
        //public const string LenderNmls = "LenderNMLS";
        //public const string LenderName = "LenderName";
        //public const string LenderAddress = "LenderAddress";
        public const string LenderEmail = "LenderEmail";
        public const string LenderSkype = "LenderSkype";
        // public const string LenderPhone = "LenderPhone";


        public const string LoggedPricingRequest = "PricingRequestLogFlag";
        public const string LoggedPricingRespose = "PricingResposeLogFlag";

        //public const string BenchMarkRateUpdateServiceOn = "BenchMarkRateUpdateServiceOn";
        public const string MinPointPrice = "BenchmarkPointPriceMin";
        public const string MaxPointPrice = "BenchmarkPointPriceMax";
        public const string Threshold = "BenchmarkThreshold";
        public const string HasProfit = "BenchmarkRateIncludingProfitFlag";
        public const string AutoClosingDateFlag = "AutoClosingDateFlag";

        /*** For Lead Assignment  Service ***/
        public const string LeadAssignmentLogic = "AutoLeadAssignmentStrategy";
        public const string ReAssignmentLogic = "ReAssignmentStrategy";
        public const string ReAssignmentStartEndIntervalMinutes = "ReAssignmentStartEndIntervalMinutes";
        public const string OfficeStartingHours = "OfficeStartingHours";
        public const string OfficeEndingHours = "OfficeEndingHours";
        public const string GridRefreshInterval = "GridRefreshInterval";
        public const string GridFilterInterval = "GridFilterInterval";

        //public const string MarketComparisonJson = "MarketComparisonJson";
        public const string CurrentRateType = "TodaysRateType";
        public const string RatingTopImages = "RatingTopImages";
        public const string LogoZillow = "LogoZillow";
        public const string LogoLendingTree = "LogoLendingTree";
        public const string RatingBottomImages = "RatingBottomImages";

        public const string Five9UserName = "Five9UserName";
        public const string Five9Password = "Five9Password";
        public const string EnabledFive9Posting = "Five9PostingEnabledFlag";
        public const string MaxPostingRetries = "Five9MaxPostingRetries";

        public const string MaxWorkQueueRetries = "MaxWorkQueueRetries";
        public const string WorkqueueRetryOffset = "WorkqueueRetryOffset-{0}";
        public const string SubsequentWorkqueueRetryOffset = "SubsequentWorkqueueRetryOffset";
        public const string DashboardChartDataInWeeks = "DashboardChartDataInWeeks";

        public const string CampaignDefaultOffsetToMinutes = "CampaignDefaultOffsetToMinutes";
        public const string LastVisitOffsetDays = "LastVisitOffsetDays";

        public const string EncompassApiClientId = "EncompassApiClientId";
        public const string EncompassApiClientSecretId = "EncompassApiClientSecretId";
        public const string EncompassInstanceId = "EncompassInstanceId";
        public const string EncompassUserName = "EncompassUserName";
        public const string EncompassPassword = "EncompassPassword";
        public const string EncompassLoanFolder = "EncompassLoanFolder";
        public const string EncompassLoanTemplatePath = "EncompassLoanTemplatePath";

        public const string ByteProUserName = "ByteProUserName";
        public const string ByteProPassword = "ByteProPassword";
        public const string ByteProApiUserName = "ByteProApiUserName";
        public const string ByteProApiPassword = "ByteProApiPassword";
        public const string ByteCompanyCode = "ByteCompanyCode";
        public const string ByteProUserNo = "ByteProUserNo";
        public const string ByteAuthKey = "ByteAuthKey";
        public const string ByteApiAuthKey = "ByteApiAuthKey";
        public const string ByteApiUrl = "ByteApiUrl";

        public const string MaxPointFeeValue = "MaxPointFee";
        public const string ForgotTokenExpiry = "ForgotTokenExpiry";
        public const string LeadGenErrorLocationLicense = "LeadGen.Error.LocationLicense";
        public const int CustomerEncryptedKey = 15095;

        public const string LoanApplicationStatus = "LoanApplicationStatus";
        public const string HistoryMonthLimit = "HistoryMonthLimit";
        public const string PitchTime = "PitchTime";
        public const string ReminderTime = "ReminderTime";

        public const string RatesFetchingInterval = "RatesFetchingInterval";

        public const string MaxCreditAmount = "MaxCreditAmount";
        public const string MaxCreditPercent = "MaxCreditPercent";
        public const string MaxPointFeeAmount = "MaxPointFeeAmount";
        public const string MaxPointFeePercent = "MaxPointFeePercent";

        public const string TwilioAccountSid = "TwilioAccountSid";
        public const string TwilioAuthToken = "TwilioAuthToken";
        public const string TwilioPhoneNumber = "TwilioPhoneNumber";
        public const string TwilioAppSid = "AppSid";
        public const string TwilioDomain = "TwilioDomain";

        public const string CustomSiteMapUrls = "CustomSiteMapUrls";
        public const string RobotFileString = "RobotsFile";

        public const string LenderFeePercent = "LenderFeePercent";
        public const string CushionAmount = "CushionAmount";
    }

    public static class CmsPages
    {
        public const string ContactUs = "ContactUs";
    }

    public static class HyperLinkUrl
    {
        public const string EmailTrackingUrl = "{0}/ELC/###EmailKey###/{1}/{2}?pageUrl={3}";
        public const string LoProfileUrl = "{0}/LO/{1}";
        public const string ResultPageUrl = "{0}/{4}?key={1}&opid={2}&loanRequestId={3}";
        public const string GiftPageUrl = "{0}/Thank-You-Gift";
        public const string AccountLoginLink = "{0}/Account/Login";
        public const string LoanApplicationLandingUrl = "{0}/LoanApplication";
    }

    public enum ContactInfoType
    {
        None = 0,
        [Description("Email")]
        Email = 1,
        [Description("Phone")]
        Phone = 2,
        [Description("Home")]
        Home = 3
    }

    public enum TemplateForms
    {
        [Description("Good Faith Estimate")]
        Gfe = 1,
        [Description("TIL")]
        Til = 2,
        [Description("Loan Estimate")]
        Le = 3,
        [Description("Closing Cost")]
        ClosingCost = 4,
        [Description("Mortgage Quote")]
        MortgageQuote = 5

    }

    public enum CustomerContactTypes
    {
        [Description("Not Set")]
        Notset = 0,
        [Description("Personal")]
        Personal = 1,
        [Description("Office")]
        Office = 2
    }


    public enum CmsPageType
    {
        [Description("CMS")]
        Cms = 1,
        [Description("Article")]
        Article = 2,
        [Description("Product Catalog")]
        ProductCatalog = 3
    }


    public enum PhoneTypes
    {
        [Description("Not Set")]
        Notset = 0,
        [Description("Cell")]
        Cell = 1,
        [Description("Home")]
        Home = 2,
        [Description("Work")]
        Work = 3,
        [Description("Work Cell")]
        WorkCell = 4
    }
    public enum PasswordEncryptionFormates { PlainText = 0, Encrypted = 1, Hashed = 2, }
    public enum SaveResultStatus { FailedDependency = 0, SuccessFull = 1, Failed = 2 }
    //public enum LoanPurposeType
    //{
    //    Purchase = 1,
    //    RefinanceLowerMyRatOrTerm = 2,
    //    RefinanceGetCashOrPayoff2ndMortgage = 3,
    //}
    public enum QueStatus
    {

        Created = 3,

        Scheduled = 5,

        Queued = 7,

        Running = 9,

        Retry = 10,

        Failed = 11,

        Completed = 13,

        FailOnException = 15,

        NoEmailSent = 17
    }

    public enum FeeBlockType
    {

        GuaranteedLenderFees = 1,
        LenderThirdPartyServiceFees = 2,
        GovernmentRecordingFees = 3,
        GovernmentTaxes = 4,
        CostsAndCredits = 5,
        PrePaid = 6,
        InitialEscrowDeposit = 7,
        BuyerThirdPartyServiceFees = 8,
        CreditPointFeeBlock = 9,
        CashFromBorrower = 100,
        MortgageNPayment = 101

    }

    public enum DynamicListCommand
    {
        CountyList = 3,
        StateList = 5,
        CountryList = 7,
        RegionList = 9,
        StatusList = 11,
        LoanPurposeList = 13,
        CityList = 15,
        LeadSourceList = 17,
        PropertyTypeList = 19,
        PropertyUsageList = 21,
        SecondLienTypeList = 23,
        ResidencyTypeList = 25,
        LoanGoalList = 27,
        CauseList = 29,
        ProductTypeList = 31,
        ProductList = 33,
        DefaultPmiCompany = 35,
        LoanDocTypeList = 37,
        InvestorList = 39,
        StatusTypeList = 41,
        StatusCategoryList = 43,
        LeadSourceTypeList = 45,
        EmployeeList = 47,
        TeamList = 49,
        QuestionList = 51,
        QuestionSectionList = 53,
        QuestionGroupList = 55,
        AdSourceList = 57,
        BranchList = 59,
        BusinessUnitList = 61,
        ProductFamilyList = 65,
        ProductLoanTypeList = 67,
        ProductQualifierList = 69,
        ProductClassList = 71,
        AusProcessingTypeList = 73,
        AmortizationTypeList = 75,
        AgencyList = 77,
        PrepayPenaltyList = 79,
        ProductBestExList = 81,
        LoanIndexTypeList = 83,
        PaidByList = 85,
        LeadCreatedFromList = 88,
        LeadTypeList = 89,
        LockStatusList = 90,
        LockStatusTypes = 91
    }

    public enum CampaignViews
    {
        CampaignForOpportunity = 3,
    }

    public enum RangeSetType
    {
        [Description("Fees")]
        Fees = 1,
        [Description("Profit Table")]
        ProfitTable = 2,
        [Description("Adjustment")]
        Adjustment = 3,
        PropertyTax = 4,
        [Description("Insurance")]
        Insurance = 5,

    }
    public enum CalculationOnType
    {
        [Description("Property")]
        Property = 1,
        [Description("LoamAmount")]
        LoamAmount = 2

    }

    public enum RadioButtonListType
    {
        Vertical = 1,
        Horizontal = 2
    }

    public enum CalcBasedOn
    {
        [Description("Property Value")]
        PropertyValue = 1,
        [Description("Loan Amount")]
        LoanAmount = 2,
    }


    public enum CalculationType
    {
        [Description("Fixed Value")]
        FixedValue = 1,
        //[Description("Per_Property_Value")]
        //PerPropertyValue = 2,
        [Description("Percentage")]
        PercentageValue = 3,
        //[Description("Per_LTV")]
        //PerLtv = 4,
        //[Description("Per_CLTV")]
        //PerCltv = 5,
        [Description("Formulas")]
        Formula = 6,

        [Description("Range Set")]
        RangeSet = 7,

    }

    public enum AdjustmentType
    {


        [Description("Price Adjustment")]
        PriceAdjustment = 1,
        [Description("Price Override")]
        PriceOverride = 2,
        //[Description("Rate Adjustment")]
        //RateAdjustment = 3,
        //[Description("Rate Override")]
        //RateOverride = 4,


    }

    public enum RoundType
    {
        [Description("Two Decimal")]
        RoundTo2Decimal = 1,
        [Description("Nearest Dollar")]
        RoundToNearestDollar = 2,
        [Description("Round Up")]
        RoundUp = 3,
        [Description("Round Down")]
        RoundDown = 4,

    }
    public enum RecurDuration
    {
        [Description("Daily")]
        Daily = 1,
        [Description("Weekly")]
        Weekly = 5,
        [Description("Monthly")]
        Monthly = 10,
        [Description("Yearly")]
        Yearly = 15,

        [Description("Life Time")]
        LifeTime = 20,
    }

    public enum ActivityActionType
    {
        [Description("Email")]
        Email = 1,
        [Description("SMS")]
        Sms = 2,
        //[Description("Email All Contacts Separately In To")]
        //EmailAllSeparately = 3,
        //[Description("Email To Primary Only")]
        //EmailToPrimaryOnly  = 4,
    }
    public enum SecondLienTypes
    {
        SecondMortgage = 1,
        HomeEquityLine = 2
    }

    public enum TemplateTypes
    {
        Email = 1,
        Sms = 2
    }

    public enum ActivityForType
    {
        Campaign = 1,
        RatesDirectEmail = 2,
        CompareDirectEmail = 3,
        ClosingDirectEmail = 4,
        CustomerForgotPassword = 5,
        EmailCustomerForLoanApplication = 6,
        EmailCustomerForNonLoanApplication = 7,
        EmailCustomerForLoanApplicationWithLo = 8,
        EmailCustomerForNonLoanApplicationWithLo = 9,
        EmailLoForLoanApplication = 10,
        EmailLoForNonLoanApplication = 11,
        PrimaryCustomerRegistration = 12,
        CoBorrowerRegistration = 13,
        PasswordChange = 14,
        LoanApplicationStart = 15,
        LoanApplicationSubmitted = 16,
        EmailLoOnLoanApplicationStart = 17,
        EmailLoOnLoanApplicationSubmitted = 18,
        LoanApplicationDocumentRequestActivity = 19,
        DocumentSyncFailureActivity = 20
    }


    public enum PreferredNameType
    {
        LastName = 1,
        FirstName = 2,
        NickName = 3,
        LastFirstName = 4,
        FirstLastName = 5,
        NickNameLastName = 6,
        PrefixLastName = 7
    }

    public enum RuleType
    {
        [Description("Opportunity Assignment")]
        OpportunityAssignment = 1,
        [Description("Fees Calculation")]
        FeesCalculation = 2,
        [Description("Lock Days")]
        LockDays = 3,
        [Description("LoanRequest Eligibility")]
        LoanRequestEligibility = 4,
        [Description("Web Content")]
        WebContent = 5,
        [Description("Profit Table")]
        ProfitTable = 6,
        [Description("Campaign")]
        Campaign = 7,
        [Description("Adjustment")]
        Adjustment = 8,
        [Description("Rate Filter")]
        RateFilter = 9,
        [Description("Fees Paid By")]
        FeesPaidBy = 10
    }
    public enum RuleMessageType
    {
        [Description("Eligibility Message")]
        EligibilityMessage = 1,
        [Description("Content Message")]
        ContentMessage = 2
    }

    public enum RuleMessageFor
    {
        [Description("Same for Employee and Customer")]
        Same = 1,
        [Description("Customer")]
        Customer = 2,
        [Description("Employee")]
        Employee = 3,
        [Description("Different for Employee and Customer")]
        BothDifferent = 4
    }
    public enum CommandParameters
    {
        OpportunityId,
        Opportunity,
        EmployeeId,
        Employee,
        CustomerId,
        Customer,
        ContactId,
        Contact,
        ContactInfoId,
        ContactInfo,
        QuoteResultId,
        PriceId,
        LoanRequestId,
        PricingDataset
    }

    public enum LockDayActionType
    {
        [Description("Override")]
        Override = 1,
        [Description("Adjustment")]
        Adjustment = 2
    }


    public enum FileFillCommands
    {
        [Description("Opportunity Welcome Email Command")]
        OpportunityWelcomeEmailCommand = 1,
        [Description("Close Loan Command")]
        CloseLoanCommand = 2,
        [Description("Disclosure Command")]
        DisclosureCommand = 3,
        [Description("Good Faith Estimate Command")]
        GoodFaithEstimateCommand = 4,
        [Description("TIL")]
        Til = 5,
        [Description("Loan Estimate Command")]
        LoanEstimate = 6,

    }
    public enum TrackingViewType
    {
        EmailClient = 1,
        OnlineEmailView = 2,
        OnlineAttachmentView = 3,
        EmailLinkClick = 4,
        Delivery = 5,
        Bounce = 6,
        SoftBounce = 7,
        Complaint = 8
    }
    public enum NotifyType
    {
        Success,
        Error
    }
    public enum FeesType
    {
        Fee = 1,
        ManualFee = 2,
        EscrowFee = 3,
        EscrowFeePrePaid = 4
    }
    public enum ControlTypeId
    {
        CheckBox = 1,
        RadioButon = 2,

    }
    public enum ValidityId
    {
        Unknown = 1,
        Yes = 2,
        No = 3,

    }
    public enum AjaxStatus
    {
        Success = 1,
        Error = 2,
        Validation = 3,
        Redirect = 4,
        SkipContact = 5
    }

    public enum SaveOption
    {
        Save = 1,
        SaveContinue = 2,
        SaveAddMore = 3,
        SaveRedirect = 4
    }


    public enum TriggerType
    {
        SendProposal = 1,
        SendQuoteResults = 2,
        StatusChangeTrigger = 3,
        SendContactUsEmail = 4,
        NewLeadCampaignTrigger = 5,
        RevisitingCustomerTrigger = 6,
        NewLeadWelcomeTrigger = 7,
        LeadPickedTrigger = 8,
        LoanApplicationNotify = 9,
        LoanApplicationSubmittedNotify = 10,
        LoanApplicationStartWithLO = 11,
        LoanApplicationSubmittedtWithLO = 12
    }

    //Workflow search on CategoryId used in status table
    public enum WorkFlowSearchStatusType
    {
        [Description("Floating")]
        Floating = 5,
        [Description("Lead")]
        Lead = 10,
        [Description("Closing")]
        LoanApplication = 15
    }


    //CategoryId used in status table
    public enum StatusCategoryEnum
    {
        [Description("Floating Status")]
        Floating = 1,
        [Description("Lead Status")]
        Lead = 2,
        [Description("Loan application status")]
        LoanApplication = 3
    }

    //TypeId used in status table
    public enum StatusTypeEnum
    {
        [Description("Active")]
        Active = 1,
        [Description("InActive")]
        InActive = 2
    }
    public enum EmailVerificationType
    {
        [Description("UnsubscribeCode")]
        UnsubscribeCode = 1,
        [Description("AccountActivation")]
        AccountActivation = 2
    }
    //OwnerType i.e ContactType
    public enum ContactType
    {
        [Description("Primary")]
        Primary = 1,
        [Description("Secondary")]
        Secondary = 2

    }
    public enum SearchTypes
    {
        [Description("Customer Id")]
        CustomerId = 1,
        /** Sadiq Ali said to remove *///
        //[Description("Customer ContactId")]
        //CustomerAccountId,

        //[Description("PropertyValue")]
        //PropertyValue,

        //[Description("LoanAmount")]
        //LoanAmount,

        //[Description("ZipCode")]
        //ZipCode,

        //[Description("State")]
        //State,
        [Description("Visitor Id")]
        VisitorId,
        [Description("OpportunityId")]
        OpportunityId,

        [Description("QuoteId")]
        QuoteId
    }
    public enum RateType
    {

        [Description("Lowest Rate")]
        LowestRate = 1,
        [Description("No Closing Cost")]
        NoClosingCost = 2,
        [Description("No Lender Fees")]
        NoLenderFees = 3,
        [Description("Actual No Closing Cost")]
        ActualNoClosingCost = 4,
        [Description("Zero Points")]
        ZeroPoints = 5

    }
    public enum RsDataType
    {
        Text = 1,
        Decimal = 2,
        Boolean = 3,
        StateSelect = 4,
        AddressType = 5,
        Password = 6
    }
    public enum SecurityCheckType
    {
        View = 1,
        Edit = 2,
        Delete = 3,
    }

    public enum QuestionTypes
    {
        Boolean = 1,
        Text = 2,
        Number = 3,
        SingleOption = 4,
        Multioptions = 5
    }
    public enum SettingGroups
    {
        CustomerHome = 3,

    }

    public enum CustomerSiteMenu
    {
        Other = 1,
        Header = 2,
        Footer = 3
    }

    public enum Cushion
    {
        E1 = 1,
        E2 = 2,
        E3 = 3,
        E4 = 4,
        E5 = 5,
        E6 = 6,
        E7 = 7,
        E8 = 8,
        E9 = 9,
        E10 = 10,
        E11 = 11,
        E12 = 12
    }

    public enum LayoutType
    {
        [Description("LayoutCmsDefault")]
        _LayoutCmsDefault = 1,
        //[Description("_LayoutCMSContactUs")]
        //_LayoutCmsContactUs = 2,
        [Description("_LayoutHomePage")]
        _Layout = 3,
        [Description("_LayoutCmsNavigation")]
        _LayoutCmsNavigation = 4

    }

    //Committed because an other already declared "LoanPurposeList"
    //public enum RequestType
    //{
    //    [Description("Refinance")]
    //    Refinance = 1,
    //    [Description("Purchase")]
    //    Purchase = 2,
    //    [Description("Cash out")]
    //    Cashout = 3 
    //}
    public enum Suffix
    {
        [Description("Sr")]
        Sr = 1,
        [Description("Jr")]
        Jr = 2
    }

    public enum PartnerType
    {
        [Description("No")]
        No = 1,
        [Description("Domestic Partner")]
        Domestic = 2,
        [Description("Spouse")]
        Spouse = 3,
        [Description("Other Borrower")]
        Other = 4
    }

    public enum MaritalStatus
    {
        [Description("Married")]
        Married = 1,
        [Description("Separated")]
        Separated = 2,
        [Description("Single")]
        Single = 3,
        [Description("Divorced")]
        Divorced = 4,
        [Description("Widowed")]
        Widowed = 5,
        [Description("Civil Union")]
        Civil_Union = 6,
        [Description("Domestic Partnership")]
        Domestic_Partnership = 7,
        [Description("Registered Reciprocal Beneficiary Relationship")]
        Registered_Reciprocal_Beneficiary_Relationship = 8

    }

    public enum Gender
    {
        [Description("Female")]
        Female = 1,
        [Description("Male")]
        Male = 2,
        [Description("Do Not Wish to Provide")]
        Do_Not_Wish = 3

    }

    public enum DeclarationQuestionEnum
    {
        // Are there any outstanding judgments against you?
        LoanForeclosureOrJudgementIndicator = 36,
        // Have you been declared bankrupt within the past 7 years?
        BankruptcyIndicator = 37,
        // Have you had property foreclosed upon or given title or deed in lieu thereof in the last 7 years?
        PropertyForeclosedPastSevenYearsIndicator = 38,
        // Are you a party to a lawsuit?
        PartyToLawsuitIndicator = 39,
        // Have you directly or indirectly been obligated on any loan which resulted in foreclosure, transfer of title in lieu of foreclosure, or judgment?
        OutstandingJudgementsIndicator = 40,
        // Are you presently delinquent or in default on any Federal debt or any other loan, mortgage, financial obligation, bond or loan guarantee?
        PresentlyDelinquentIndicator = 41,
        // Is any part of the down payment borrowed?
        BorrowedDownPaymentIndicator = 42,
        // Are you a co-maker or endorser on a note?
        CoMakerEndorserOfNoteIndicator = 43,
        // Are you a US citizen?
        DeclarationsJIndicator = 44,
        // Do you intend to occupy the property as your primary residence?
        IntentToOccupyIndicator = 45,
        // Are you obligated to pay alimony, child support, or separate maintenance?
        AlimonyChildSupportObligationIndicator = 46,
        // Have you had an ownership interest in a property in the last three years?
        HomeownerPastThreeYearsIndicator = 47,
        // What type of property did you own? Select the choice that fits best.
        PriorPropertyUsageType = 49,
        // What was your status on the title of that property?
        PriorPropertyTitleType = 50,
        // Are you a permanent resident alien?
        DeclarationsKIndicator = 54,
        // Are you holding a valid work visa?
        ValidWorkVisa = 57,
    }



    public enum EthnicityId
    {
        [Description("Hispanic")]
        Hispanic = 1,
        [Description("Latino")]
        Latino = 2
    }

    public enum ControlOnpage
    {
        //[Description("_loanRequest-CMSDefault")]
        //_loanRequestCMSDefault = 1,
        [Description("_ContactUs")]
        _ContactUs = 2,
        [Description("_BasicQuoteDynamic")]
        _BasicQuoteDynamic,
        [Description("_CompanyContactDetails")]
        _CompanyContactDetails,
        [Description("_ThanksAutoRedirect")]
        _ThanksAutoRedirect


    }

    public enum PropertyStatusListEnum
    {
        [Description("Sold")]
        Sold = 1,
        [Description("Pending Sale")]
        PendingSale = 2,
        [Description("Retain For Rental")]
        Retained = 3,
    }

    public enum StatusListEnum
    {
        [Description("Float")]
        Float = 1,
        [Description("New Lead")]
        NewLead = 2,
        [Description("System Duplicate Lead")]
        SystemDuplicate = 3,
        [Description("Duplicate Lead")]
        Duplicate = 4,
        [Description("Merge")]
        Merge = 5,
        [Description("Application Started")]
        ApplicationStarted = 12,
        [Description("Application Submitted")]
        ApplicationSubmitted = 13,
        [Description("Document Upload")]
        DocumentUpload = 50
    }
    public enum Months
    {
        [Description("January")]
        January = 1,
        [Description("February")]
        February = 2,
        [Description("March")]
        March = 3,
        [Description("April")]
        April = 4,
        [Description("May")]
        May = 5,
        [Description("June")]
        June = 6,
        [Description("July")]
        July = 7,
        [Description("August")]
        August = 8,
        [Description("September")]
        September = 9,
        [Description("October")]
        October = 10,
        [Description("November")]
        November = 11,
        [Description("December")]
        December = 12,

    }
    public enum PricingError
    {
        ServerNotAvailable = 1,
        GettingResponseError = 2,
        MalFormedXml = 3,
        XmlParssingError = 4,
        TimeOut = 5,
        GettingFeeError = 6
    }

    public enum QuestionResponseEnum
    {
        [Description("0")]
        False = 0,
        [Description("1")]
        True = 1

    }

    public enum LoanPurposeMode
    {
        Refinance = 0,
        Purchase = 1,
        Both = 2 //this will be used in create case to fill both
    }

    public enum LoanPurposeList
    {
        [Description("Geo Location")]
        GeoLocation = 0,
        [Description("Buying a Home")]
        Purchase = 1,
        [Description("Looking to Refinance")]
        Refinance = 2,
        CashOut = 3
    }
    public enum LoanPurposeListForResult
    {
        [Description("Purchase")]
        Purchase = 1,
        [Description("Refinance")]
        Refinance = 2,
        [Description("Cash Out")]
        CashOut = 3,
        [Description("Home Equity")]
        HomeEquity = 4
    }
    public enum ThirdParty
    {
        Rainsoft = 1,
        Zillow = 2,
        LendingTree = 3,
        BankRate = 4,
        Encompass = 5

    }

    public enum SubmitButtonMode
    {
        Cancel = 0,
        Save = 1,
        Get = 2,
        SaveAndClose = 3
    }

    public enum EscrowType
    {
        [Description("Insurance")]
        Insurance = 1,
        [Description("Taxes")]
        Taxes = 2
    }

    public enum FeeTypes
    {
        [Description("Guaranteed")]
        Guaranteed = 1,
        [Description("Estimated")]
        Estimated = 2
    }

    public enum RefinanceType //Its values are taken from data base, if changed in LoanPurpose database than it must be changed here.
    {
        Refinance = 2,
        CashOut = 3
    }
    public enum LeadCreatedFrom
    {
        [Description("Admin")]
        Manually = 1,
        [Description("Customer")]
        CompanyWeb = 2,
        [Description("3rd Party Api")]
        ThirdPartyApi = 3,
    }

    public enum TemplateLocation
    {
        [Description("Result Sheet Email")]
        ResultPage = 1,
        [Description("Closing Cost Email")]
        ClosingPage = 2,
        [Description("Compare Rate Email")]
        SystemTemplateforCompareEmail = 3
    }

    public enum DefaultEscrowItems
    {
        [Description("Property Taxes")]
        PropertyTaxes = 1,
        [Description("Home Owners Insurance")]
        HomeOwnersInsurance = 2
    }

    public enum LeadAssignmentStrategy
    {
        Strict = 1,
        LoadBalancing = 2
    }

    public enum PitchStrategy
    {
        Unicast = 1,
        Broadcast = 2
    }

    public enum ReAssignmentStrategy
    {
        Daily = 1,
        OfficeDays = 2
    }

    public enum Countries
    {
        Usa = 1
    }

    public enum FollowUpStatus
    {
        [Description("Open")]
        Open = 1,
        [Description("Closed")]
        Closed = 2
    }

    public enum RemindBefore
    {
        [Description("0 minute")]
        M0 = 0,
        [Description("5 minutes")]
        M5 = 5,
        [Description("10 minutes")]
        M10 = 10,
        [Description("15 minutes")]
        M15 = 15,
        [Description("30 minutes")]
        M30 = 30,
        [Description("1 hour")]
        H1 = 60,
        [Description("2 hours")]
        H2 = 120,
        [Description("3 hours")]
        H3 = 180,
        [Description("4 hours")]
        H4 = 240,
        [Description("5 hours")]
        H5 = 300,
        [Description("6 hours")]
        H6 = 360,
        [Description("7 hours")]
        H7 = 420,
        [Description("8 hours")]
        H8 = 480,
        [Description("9 hours")]
        H9 = 540,
        [Description("10 hours")]
        H10 = 600,
        [Description("11 hours")]
        H11 = 660,
        [Description("12 hours")]
        H12 = 720,
        [Description("1 day")]
        D1 = 1440,
        [Description("2 days")]
        D2 = 2880
    }

    //LockTypeId used in LockStatusList table
    public enum StatusLockType
    {
        [Description("Lock")]
        Lock = 1,
        [Description("Float")]
        Float = 0
    }

    public enum EnumLockStatusList
    {
        [Description("Float")]
        Float = 5
    }
    public enum PublishCalculationType
    {
        [Description("Fixed Value")]
        FixedValue = 1,
        [Description("Percentage")]
        PercentageValue = 3,
        [Description("Formulas")]
        Formula = 6,
        [Description("Range Set")]
        RangeSet = 7,

    }

    public enum DocumentTypeId
    {
        Pdf = 1,
        Html = 2,
    }

    public enum DocumentViewBy
    {
        Both = 0,
        Customer = 1,
        Employee = 2,

    }

    public enum ThirdPartyLeadSource
    {
        [Description("Zillow")]
        Zillow = 6,
        [Description("LendingTree")]
        LendingTree = 2,
        [Description("Bank Raterate Survey")]
        BankRate = 4

    }

    public enum LoanRequestType
    {
        Opportunity = 1,
        [Description("Rate Service Parameter")]
        RateServiceParameter = 2,
        BenchMark = 3,
        [Description("DefaultLoanRtae")]
        DefaultLoanRate = 4
    }

    public enum EscrowWaiver
    {
        [Description("Yes")]
        Yes = 0,
        [Description("No")]
        No = 1,
    }

    public enum EnumThirdPartyStatusList
    {
        Success = 1,
        Duplicate = 2,
        InvalidFormate = 3,
        SystemError = 4
    }

    public enum CompnayBranchPhoneType
    {
        [Description("Phone")]
        Phone = 1,
        [Description("Fax")]
        Fax = 2,
        [Description("Toll Free")]
        TollFree = 3
    }
    public enum CompnayBranchEmailType
    {
        [Description("Email")]
        Email = 1
    }

    public enum LoanTypes
    {
        [Description("Second Mortgage")]
        SecondMortgage = 1,
        [Description("Home Equity Line")]
        HomeEquityLine = 2

    }



    public enum ProductFamilyEnum
    {
        Conforming = 1
    }

    public enum NotificationMessageCriticalType
    {
        Normal = 1,
        LeadAssign = 2,
        WindowsService = 3
    }

    public enum TemplateUserType
    {
        [Description("Customer")]
        Customer = 1,
        [Description("Employee")]
        Employee = 2,
        [Description("Both Customer and Employee")]
        Both = 3
    }
    public enum SubscriptionTypes
    {
        [Description("All")]
        All = 1,
        [Description("Employee Only")]
        EmployeeOnly = 2,
    }

    public enum ServiceScheduleActivityType
    {
        [Description("Opportunity Assignment")]
        OpportunityAssignment = 1,
        [Description("Campaign")]
        Campaign = 2,
        [Description("Rate Update Serivce")]
        RateUpdate = 3,
        [Description("Bench Mark Serivce")]
        BenchMarkService = 4,
        [Description("Mbs Rate Serivce")]
        MbsRateService = 5,
        [Description("Five9 Batch Posting Service")]
        Five9BatchPostingService = 6,
        [Description("Reset Workqueue Service")]
        WorkqueueResetService = 7,
        [Description("Encompass Data Fetching")]
        EncompassDataFetching = 8,
        [Description("Encompass Data Fetching")]
        DefaultLoanRatesFetching = 9,
        [Description("Vortex Opportunity Assignment")]
        VortexOpportunityAssignment = 10
    }

    public enum CampaignDependOnType
    {
        [Description("Campaign")]
        Campaign = 1,
        [Description("Activity")]
        Activity = 2
    }

    public enum Five9PostingStatus
    {
        Success = 1,
        Fail = 2,
        NotEligible = 3,
        WebException = 4,
        Five9Exception = 5

    }

    public enum SystemQuestions
    {
        [Description("Is Military Personnel")]
        VaEligible = 3,
        RealEstateAgent = 35,
        FirstTimeBuyer = 1,
        NewlyConstructedProperty = 9
    }

    public enum LeadAssignmentFlag
    {
        Start = 1,
        Completed = 2
    }

    public enum ProductTemplateFieldType
    {
        LowestRateFixedRate = 1,
        NoClosingCostFixedRate = 2,
        NoLenderFeesFixedRate = 3,
        ActualNoClosingCostFixedRate = 4,
        LowestRateFixedApr = 5,
        NoClosingCostFixedApr = 6,
        NoLenderFeesFixedApr = 7,
        ActualNoClosingCostFixedApr = 8
    }

    public enum TrackingLinkEnum
    {
        Home = 1,
        RatesPage = 2,
        Unsubscribe = 3,
        LoProfile = 4,
        LoAppSignup = 5,//for lo loan application signup page
        LoAppLogin = 6,//for lo loan application login page
        LoanAppSignup = 7,//for BU loan application signup page
        LoanAppLogin = 8,//for BU loan application login page
        Gift = 9,
        AccountLoginLink = 10,
        PasswordResetLink = 11,
        LoanApplicationLandingPageLink = 12,
        ViewEmailPage = 13,
        GetRateLink = 14,
        CallScheduleUrl = 15
    }

    public enum WindowsServicesEnum
    {
        [Description("WorkQueue Service")]
        Workqueue = 1,
        [Description("Service Manager")]
        ServiceManager = 2,
        AllServices = 3
    }

    public enum LeadTypeSearchFilter
    {
        [Description("My Leads")]
        MyLeads = 1,
        [Description("Assigned")]
        Assigned = 2,
        [Description("All Leads")]
        AllLeads = 3,
        [Description("UnPikced")]
        UnPikced = 4,
        [Description("All UnAssigned")]
        UnAssigned = 5

    }
    public enum FloatingTypeSearchFilter
    {
        [Description("No Floats")]
        NoFloats = 1,
        [Description("My Floats")]
        MyFloats = 2,
        [Description("All Floats")]
        AllFloats = 3
    }

    public enum QutoeCreateBy
    {
        Customer = 1,
        Employee = 2,
        System = 3
    }
    public enum SystemTemplate
    {
        NoDesireRateFound = 83,
        NoPricingFound = 84
    }

    public enum CampaignQueueStatus
    {
        AddToQueue = 1,
        Success = 2,
        RuleFailure = 3
    }

    public enum SelectTableBaseOn
    {
        Rule = 1,
        Opportunity = 2
    }

    public enum LandingPageSteps
    {
        One = 1,
        Tow,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Eleven,
        Twelve
    }

    public enum ThirdPartyErrorCode
    {
        [Description("Invalid Lead Format")]
        InvalidLeadFormat = -1,

        [Description("System Error")]
        SystemError = -2,

        [Description("Account Inactive")]
        AccountInactive = -3,

        [Description("Success")]
        Success = 0,

        [Description("Invalid Coverage Match")]
        InvalidCoverage = 2,

        [Description("Duplicate Lead")]
        DuplicateLead = 3,

        [Description("input parameter buid is null")]
        BuidIsNull = 4,

        [Description("Data not found, request is empty.")]
        EmptyFile = -4,

    }

    public enum RateFilterEnum
    {
        [Description("Recommended Rates")]
        Recommended = 0,
        [Description("No Closing Cost")]
        NoClosingCost = 1,
        [Description("No or Low Lender Fee")]
        NoLenderFees = 2,
        [Description("No Points")]
        NoPoint = 3,
        [Description("Lowest Rates")]
        LowestRate = 4,

    }

    public enum TermTypeEnum
    {
        Fixed = 1,
        ARM = 2
    }

    public enum EscrowEntityTypeEnum
    {
        [Description("Property Taxes")]
        PropertyTaxes = 1,
        [Description("Home Owners Insurance")]
        HomeOwnersInsurance = 2,
        [Description("School Tax")]
        SchoolTax = 3,
        [Description("Flood Insurance")]
        FloodInsurance = 4
    }
    public enum FamilyRelationTypeEnum
    {
        [Description("Spouse")]
        Spouse = 1,
        [Description("Domestic Partner")]
        DomesticPartner = 2,
        [Description("Parent")]
        Parent = 3,
        [Description("Child")]
        Child = 4,
        [Description("Sibling")]
        Sibling = 5,
        [Description("Other")]
        Other = 6
    }

    public enum JobTypeEnum
    {
        [Description("Full-Time")]
        FullTime = 1,
        [Description("Part-Time")]
        PartTime = 2,
        [Description("Seasonal")]
        Seasonal = 3
    }

    public enum OwnershipTypeEnum
    {
        [Description("Own")]
        Own = 1,
        [Description("Rent")]
        Rent = 2,
        [Description("Others")]
        Other = 3
    }


    public enum OwnTypeEnum
    {
        [Description("Primary Contact")]
        PrimaryContact = 1,
        [Description("Secondary Contact")]
        SecondaryContact = 2,
    }

    public enum ResidencyTypeEnum
    {
        [Description("US Citizen")]
        UsCitizen = 1,
        [Description("Permanent Resident")]
        PermanentResident = 2,
        [Description("Valid work VISA (H1, L1 etc.)")]
        ValidworkVisa = 3,
        [Description("Valid EAD")]
        ValidEad = 4

    }
    public enum ResidencyStateEnum
    {
        [Description("US Citizen")]
        UsCitizen = 1,
        [Description("Permanent Resident")]
        PermanentResident = 2,
        [Description("Valid work VISA (H1, L1 etc.)")]
        ValidworkVisa = 3,
        [Description("Temporary workers (H -2A)")]
        Temporaryworkers = 4,
        [Description("Other")]
        Other = 5

    }

    public enum QuestionAnswerEnum
    {
        [Description("Yes")]
        Yes = 1,
        [Description("No")]
        No = 0,
    }
    public enum AssetTypeEnum
    {
        [Description("Earnest Money")]
        EarnestMoney = 1,
        [Description("Sale of Non-Real Estate Asset")]
        SaleofNonRealEstateAsset = 2,
        [Description("Sale of Real Estat Property")]
        SaleofRealEstatProperty = 3,
        [Description("Sweat Equity")] SweatEquity = 4,
        [Description("Employer Assistance")]
        EmployerAssistance = 5,
        [Description("Rent Credit")]
        RentCredit = 6,
        [Description("Secured Borrowed Funds")]
        SecuredBorrowedFunds = 7,
        [Description("Trade Equity")]
        TradeEquity = 8,
        [Description("Unsecured Borrowed Funds")]
        UnsecuredBorrowedFunds = 9,
        [Description("Other")]
        Other = 10,
        [Description("Cash Gift")]
        CashGift = 11,
        [Description("Gift of Equity")]
        GiftofEquity = 12,
        [Description("Grant")]
        Grant = 13,
    }
    public enum OtherEmploymentIncomeTypeEnum
    {
        [Description("Overtime")]
        Overtime = 1,
        [Description("Bonus")]
        Bonus = 2,
        [Description("Commission")]
        Commission = 3,
        [Description("Military Entitlements")]
        MilitaryEntitlements = 4,
        [Description("Other")]
        Other = 5,
    }
    public enum PropertyUsageEnum
    {
        [Description("Primary Residence")]
        PrimaryResidence = 1,
        [Description("Rental/Investment")]
        RentalInvestment = 2,
        [Description("Second Home")]
        SecondHome = 3
    }
    public enum PropertyTypeEnum
    {
        [Description("Single Family Detached")]
        SingleFamilyDetached = 1,
        [Description("Townhome")]
        Townhome = 2,
        [Description("Condominium")]
        Condominium = 3,
        [Description("2-Unit Building")]
        TwoUnitBuilding = 4,
        [Description("3-Unit Building")]
        ThreeUnitBuilding = 5,
        [Description("Manufactured Home")]
        ManufacturedHome = 6
    }

    public enum AddressPartialViewEnum
    {
        [Description("Google")]
        Google = 1,
        [Description("RainMaker")]
        RainMaker = 2,
    }
    public enum StringResourceTypeEnum
    {
        General = 1,
        AdSource = 2,
    }

    public enum MessageLocationEnum
    {
        [Description("Home Page Top")]
        HomePageTop = 1
    }

    public enum MortgageTypes
    {
        [Description("First Lien")]
        FirstLien = 0,
        [Description("Second Lien")]
        SecondLien = 1

    }

    public enum ScheduleActivityEnum
    {
        LeadAssignment = 1,
        Campaign = 2,
        RatesUpdate = 3,
        BenchMark = 4,
        MBSRate = 5,
        Five9 = 6,
        EncompassFetching = 8
    }

    public enum VisitorTypeEnum
    {
        Visitor = 1,
        Bot = 2,
        UnKnownBot = 3,
        NoTracking = 4,
        UnKnown = 5
    }

    public enum LoanGoalsEnum
    {
        ResearchingRates = 1,
        NeedPreapproval = 3,
        HomeUnderContract = 4,
        LowerMyRateTerm = 5,
        NeedCash = 6,
        DebtConsolidation = 7
    }

    public enum LoanMortgageTypesEnum
    {
        SingleMortgage = 1,
        MultipleMortgages = 2,
        MortgageFree = 3
    }

    public enum Services
    {
        Twilio = 1
    }

    public enum AuthProvider
    {
        RainMaker = 1,
        RainMakerWeb = 2,
        Google = 3,
        Facebook = 4,
        Twitter = 5
    }

    public enum SsoResponse
    {
        [Description("Success")]
        Success = 0,

        [Description("Error")]
        Error = 1,

        [Description("Account Inactive")]
        AccountInactive = 2,

        [Description("Data not found, request is empty.")]
        EmptyFile = 3,
    }

    public enum MortgagePageType
    {
        [Description("Mortgage Purpose Page")]
        MortgagePurposePage = 1,
        [Description("Mortgage Goal Page")]
        MortgageGoalPage = 2
    }

    public enum LoanPurposeType
    {
        [Description("Purchase")]
        Purchase = 1,
        [Description("Refinance")]
        Refinance = 2
    }

    public enum CmsPageFor
    {
        [Description("Static Page")]
        StaticPage = 1,
        [Description("Dynamic Page")]
        DynamicPage = 2,
        [Description("Product Catalog")]
        PrductCatalog = 3
    }

    public enum PickStatus
    {
        [Description("Picked")]
        Picked=1,
        [Description("Not Picked")]
        NotPicked=0
    }

    public enum VortexAgentStatus
    {
        [Description("Available")]
        Available =1,
        [Description("Unavailable")]
        Unavailable =2,
        [Description("Busy")]
        Busy =3,
        [Description("Break")]
        Break =4
    }

    public enum VortexSessionType
    {
        Workstation=1,
        Mobile=2
    }
}