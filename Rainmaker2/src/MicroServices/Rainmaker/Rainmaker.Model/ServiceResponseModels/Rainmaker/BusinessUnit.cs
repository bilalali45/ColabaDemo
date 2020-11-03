using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class BusinessUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? DefaultLeadSourceId { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public int? EmailAccountId { get; set; }
        public int? TimeZoneId { get; set; }
        public string WebUrl { get; set; }
        public string LoanUrl { get; set; }
        public string LoanLoginUrl { get; set; }
        public string TpId { get; set; }
        public string ShortName { get; set; }
        public string AbbreviatedName { get; set; }
        public string ScheduleUrl { get; set; }
        public string Logo { get; set; }

        //public System.Collections.Generic.ICollection<Activity> Activities { get; set; }

        //public System.Collections.Generic.ICollection<AuthApplicationKey> AuthApplicationKeys { get; set; }

        //        public System.Collections.Generic.ICollection<BenchMark> BenchMarks { get; set; }

        //public System.Collections.Generic.ICollection<BusinessUnitEmail> BusinessUnitEmails { get; set; }

        //public System.Collections.Generic.ICollection<BusinessUnitPhone> BusinessUnitPhones { get; set; }

        //public System.Collections.Generic.ICollection<BusinessUnitPhoneBinder> BusinessUnitPhoneBinders { get; set; }

        //public System.Collections.Generic.ICollection<CmsPage> CmsPages { get; set; }

        //public System.Collections.Generic.ICollection<CurrentRate> CurrentRates { get; set; }

        //public System.Collections.Generic.ICollection<Customer> Customers { get; set; }

        //public System.Collections.Generic.ICollection<DefaultLoanParameter> DefaultLoanParameters { get; set; }

        //public System.Collections.Generic.ICollection<EmployeeBusinessUnitEmail> EmployeeBusinessUnitEmails { get; set; }

        //public System.Collections.Generic.ICollection<LoanApplication> LoanApplications { get; set; }

        //public System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; }

        //public System.Collections.Generic.ICollection<LocaleStringResource> LocaleStringResources { get; set; }

        //public System.Collections.Generic.ICollection<Opportunity> Opportunities { get; set; }

        //public System.Collections.Generic.ICollection<PromotionalProgram> PromotionalPrograms { get; set; }

        //public System.Collections.Generic.ICollection<RateArchive> RateArchives { get; set; }

        //public System.Collections.Generic.ICollection<RateServiceParameter> RateServiceParameters { get; set; }

        //public System.Collections.Generic.ICollection<ReviewComment> ReviewComments { get; set; }

        //public System.Collections.Generic.ICollection<Setting> Settings { get; set; }

        //public System.Collections.Generic.ICollection<UserProfile> UserProfiles { get; set; }

        //public System.Collections.Generic.ICollection<UserResetPasswordKey> UserResetPasswordKeys { get; set; }

        //public System.Collections.Generic.ICollection<UserResetPasswordLog> UserResetPasswordLogs { get; set; }

        //        //public System.Collections.Generic.ICollection<WorkQueue> WorkQueues { get; set; }

        //public EmailAccount EmailAccount { get; set; }

        public LeadSource LeadSource { get; set; }
    }
}