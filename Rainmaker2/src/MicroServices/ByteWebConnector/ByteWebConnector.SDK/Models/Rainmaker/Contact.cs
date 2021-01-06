using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string Preferred { get; set; }
        public string Company { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public int? PreferredId { get; set; }
        public string Ssn { get; set; }
        public DateTime? DobUtc { get; set; }
        public int? YrsSchool { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? Gender { get; set; }
        public int? EthnicityId { get; set; }

        public ICollection<ContactAddress> ContactAddresses { get; set; }

        public ICollection<ContactEmailInfo> ContactEmailInfoes { get; set; }

        public ICollection<ContactInfo> ContactInfoes { get; set; }

        public ICollection<ContactPhoneInfo> ContactPhoneInfoes { get; set; }

        //public System.Collections.Generic.ICollection<Customer> Customers { get; set; }

        //public System.Collections.Generic.ICollection<EmailLogBinder> EmailLogBinders { get; set; }

        //public System.Collections.Generic.ICollection<Employee> Employees { get; set; }

        //public System.Collections.Generic.ICollection<FollowUp> FollowUps { get; set; }

        //public System.Collections.Generic.ICollection<LoanContact> LoanContacts { get; set; }

        //public System.Collections.Generic.ICollection<OtpTracing> OtpTracings { get; set; }

        //public System.Collections.Generic.ICollection<Vortex_FollowUp> Vortex_FollowUps { get; set; }

    }
}