













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // BusinessUnit

    public partial class BusinessUnit 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int? DefaultLeadSourceId { get; set; } // DefaultLeadSourceId
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted
        public int? EmailAccountId { get; set; } // EmailAccountId
        public int? TimeZoneId { get; set; } // TimeZoneId
        public string WebUrl { get; set; } // WebUrl (length: 150)
        public string LoanUrl { get; set; } // LoanUrl (length: 500)
        public string LoanLoginUrl { get; set; } // LoanLoginUrl (length: 500)
        public string TpId { get; set; } // TpId (length: 500)
        public string ShortName { get; set; } // ShortName (length: 150)
        public string AbbreviatedName { get; set; } // AbbreviatedName (length: 150)
        public string ScheduleUrl { get; set; } // ScheduleUrl (length: 500)
        public string Logo { get; set; } // Logo (length: 50)
        public string Banner { get; set; } // Logo (length: 50)
        public string FavIcon { get; set; } // Logo (length: 50)

        // Reverse navigation

        /// <summary>
        /// Child Activities where [Activity].[BusinessUnitId] point to this entity (FK_Activity_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Activity> Activities { get; set; } // Activity.FK_Activity_BusinessUnit
        /// <summary>
        /// Child AuthApplicationKeys where [AuthApplicationKey].[BusinessUnitId] point to this entity (FK_AuthApplicationKey_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AuthApplicationKey> AuthApplicationKeys { get; set; } // AuthApplicationKey.FK_AuthApplicationKey_BusinessUnit
        /// <summary>
        /// Child BenchMarks where [BenchMark].[BusinessUnitId] point to this entity (FK_BenchMark_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BenchMark> BenchMarks { get; set; } // BenchMark.FK_BenchMark_BusinessUnit
        /// <summary>
        /// Child BusinessUnitEmails where [BusinessUnitEmail].[BusinessUnitId] point to this entity (FK_BusinessUnitEmail_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BusinessUnitEmail> BusinessUnitEmails { get; set; } // BusinessUnitEmail.FK_BusinessUnitEmail_BusinessUnit
        /// <summary>
        /// Child BusinessUnitPhones where [BusinessUnitPhone].[BusinessUnitId] point to this entity (FK_BusinessUnitPhone_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BusinessUnitPhone> BusinessUnitPhones { get; set; } // BusinessUnitPhone.FK_BusinessUnitPhone_BusinessUnit
        /// <summary>
        /// Child BusinessUnitPhoneBinders where [BusinessUnitPhoneBinder].[BusinessUnitId] point to this entity (FK_BusinessUnitPhoneBinder_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BusinessUnitPhoneBinder> BusinessUnitPhoneBinders { get; set; } // BusinessUnitPhoneBinder.FK_BusinessUnitPhoneBinder_BusinessUnit
        /// <summary>
        /// Child CmsPages where [CmsPage].[BusinessUnitId] point to this entity (FK_CmsPage_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CmsPage> CmsPages { get; set; } // CmsPage.FK_CmsPage_BusinessUnit
        /// <summary>
        /// Child CurrentRates where [CurrentRate].[BusinessUnitId] point to this entity (FK_CurrentRate_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CurrentRate> CurrentRates { get; set; } // CurrentRate.FK_CurrentRate_BusinessUnit
        /// <summary>
        /// Child Customers where [Customer].[BusinessUnitId] point to this entity (FK_Customer_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Customer> Customers { get; set; } // Customer.FK_Customer_BusinessUnit
        /// <summary>
        /// Child DefaultLoanParameters where [DefaultLoanParameter].[BusinessUnitId] point to this entity (FK_DefaultLoanParameter_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<DefaultLoanParameter> DefaultLoanParameters { get; set; } // DefaultLoanParameter.FK_DefaultLoanParameter_BusinessUnit
        /// <summary>
        /// Child EmployeeBusinessUnitEmails where [EmployeeBusinessUnitEmail].[BusinessUnitId] point to this entity (FK_EmployeeBusinessUnitEmail_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmployeeBusinessUnitEmail> EmployeeBusinessUnitEmails { get; set; } // EmployeeBusinessUnitEmail.FK_EmployeeBusinessUnitEmail_BusinessUnit
        /// <summary>
        /// Child LoanApplications where [LoanApplication].[BusinessUnitId] point to this entity (FK_LoanApplication_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanApplication> LoanApplications { get; set; } // LoanApplication.FK_LoanApplication_BusinessUnit
        /// <summary>
        /// Child LoanRequests where [LoanRequest].[BusinessUnitId] point to this entity (FK_LoanRequest_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; } // LoanRequest.FK_LoanRequest_BusinessUnit
        /// <summary>
        /// Child LocaleStringResources where [LocaleStringResource].[BusinessUnitId] point to this entity (FK_LocaleStringResource_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LocaleStringResource> LocaleStringResources { get; set; } // LocaleStringResource.FK_LocaleStringResource_BusinessUnit
        /// <summary>
        /// Child Opportunities where [Opportunity].[BusinessUnitId] point to this entity (FK_Opportunity_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Opportunity> Opportunities { get; set; } // Opportunity.FK_Opportunity_BusinessUnit
        /// <summary>
        /// Child PromotionalPrograms where [PromotionalProgram].[BusinessUnitId] point to this entity (FK_PromotionalProgram_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PromotionalProgram> PromotionalPrograms { get; set; } // PromotionalProgram.FK_PromotionalProgram_BusinessUnit
        /// <summary>
        /// Child RateArchives where [RateArchive].[BusinessUnitId] point to this entity (FK_RateArchive_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<RateArchive> RateArchives { get; set; } // RateArchive.FK_RateArchive_BusinessUnit
        /// <summary>
        /// Child RateServiceParameters where [RateServiceParameter].[BusinessUnitId] point to this entity (FK_RateServiceParameter_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<RateServiceParameter> RateServiceParameters { get; set; } // RateServiceParameter.FK_RateServiceParameter_BusinessUnit
        /// <summary>
        /// Child ReviewComments where [ReviewComment].[BusinessUnitId] point to this entity (FK_ReviewComment_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ReviewComment> ReviewComments { get; set; } // ReviewComment.FK_ReviewComment_BusinessUnit
        /// <summary>
        /// Child Settings where [Setting].[BusinessUnitId] point to this entity (FK_Setting_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Setting> Settings { get; set; } // Setting.FK_Setting_BusinessUnit
        /// <summary>
        /// Child UserProfiles where [UserProfile].[BusinessUnitId] point to this entity (FK_UserProfile_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserProfile> UserProfiles { get; set; } // UserProfile.FK_UserProfile_BusinessUnit
        /// <summary>
        /// Child UserResetPasswordKeys where [UserResetPasswordKey].[BusinessUnitId] point to this entity (FK_UserResetPasswordKey_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserResetPasswordKey> UserResetPasswordKeys { get; set; } // UserResetPasswordKey.FK_UserResetPasswordKey_BusinessUnit
        /// <summary>
        /// Child UserResetPasswordLogs where [UserResetPasswordLog].[BusinessUnitId] point to this entity (FK_UserResetPasswordLog_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserResetPasswordLog> UserResetPasswordLogs { get; set; } // UserResetPasswordLog.FK_UserResetPasswordLog_BusinessUnit
        /// <summary>
        /// Child WorkQueues where [WorkQueue].[BusinessUnitId] point to this entity (FK_WorkQueue_BusinessUnit)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<WorkQueue> WorkQueues { get; set; } // WorkQueue.FK_WorkQueue_BusinessUnit

        // Foreign keys

        /// <summary>
        /// Parent EmailAccount pointed by [BusinessUnit].([EmailAccountId]) (FK_BusinessUnit_EmailAccount)
        /// </summary>
        public virtual EmailAccount EmailAccount { get; set; } // FK_BusinessUnit_EmailAccount

        /// <summary>
        /// Parent LeadSource pointed by [BusinessUnit].([DefaultLeadSourceId]) (FK_BusinessUnit_LeadSource)
        /// </summary>
        public virtual LeadSource LeadSource { get; set; } // FK_BusinessUnit_LeadSource
        public string ByteOrganizationCode { get; set; }

        public BusinessUnit()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 26;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            Activities = new System.Collections.Generic.HashSet<Activity>();
            AuthApplicationKeys = new System.Collections.Generic.HashSet<AuthApplicationKey>();
            BenchMarks = new System.Collections.Generic.HashSet<BenchMark>();
            BusinessUnitEmails = new System.Collections.Generic.HashSet<BusinessUnitEmail>();
            BusinessUnitPhones = new System.Collections.Generic.HashSet<BusinessUnitPhone>();
            BusinessUnitPhoneBinders = new System.Collections.Generic.HashSet<BusinessUnitPhoneBinder>();
            CmsPages = new System.Collections.Generic.HashSet<CmsPage>();
            CurrentRates = new System.Collections.Generic.HashSet<CurrentRate>();
            Customers = new System.Collections.Generic.HashSet<Customer>();
            DefaultLoanParameters = new System.Collections.Generic.HashSet<DefaultLoanParameter>();
            EmployeeBusinessUnitEmails = new System.Collections.Generic.HashSet<EmployeeBusinessUnitEmail>();
            LoanApplications = new System.Collections.Generic.HashSet<LoanApplication>();
            LoanRequests = new System.Collections.Generic.HashSet<LoanRequest>();
            LocaleStringResources = new System.Collections.Generic.HashSet<LocaleStringResource>();
            Opportunities = new System.Collections.Generic.HashSet<Opportunity>();
            PromotionalPrograms = new System.Collections.Generic.HashSet<PromotionalProgram>();
            RateArchives = new System.Collections.Generic.HashSet<RateArchive>();
            RateServiceParameters = new System.Collections.Generic.HashSet<RateServiceParameter>();
            ReviewComments = new System.Collections.Generic.HashSet<ReviewComment>();
            Settings = new System.Collections.Generic.HashSet<Setting>();
            UserProfiles = new System.Collections.Generic.HashSet<UserProfile>();
            UserResetPasswordKeys = new System.Collections.Generic.HashSet<UserResetPasswordKey>();
            UserResetPasswordLogs = new System.Collections.Generic.HashSet<UserResetPasswordLog>();
            WorkQueues = new System.Collections.Generic.HashSet<WorkQueue>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
