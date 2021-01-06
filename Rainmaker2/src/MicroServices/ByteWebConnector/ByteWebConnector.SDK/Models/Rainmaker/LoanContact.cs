using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class LoanContact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string Preferred { get; set; }
        public string AlternateFirstName { get; set; }
        public string AlternateMiddleName { get; set; }
        public string AlternateLastName { get; set; }
        public string AlternatePrefix { get; set; }
        public string AlternateSuffix { get; set; }
        public string Company { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public int? PreferredId { get; set; }
        public string Ssn { get; set; }
        public DateTime? DobUtc { get; set; }
        public int? YrsSchool { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? UnmarriedStatusId { get; set; }
        public bool? IsPlanToMarrySoon { get; set; }
        public string OtherMaritalStatus { get; set; }
        public int? RelationFormedStateId { get; set; }
        public int? GenderId { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string WorkPhoneExt { get; set; }
        public string CellPhone { get; set; }
        public string EmailAddress { get; set; }
        public string OtherEmailAddress { get; set; }
        public int? ResidencyTypeId { get; set; }
        public int? ResidencyStateId { get; set; }
        public int? DemographicMediumId { get; set; }
        public bool? SetEthnicityInfoByObservation { get; set; }
        public bool? SetGenderInfoByObservation { get; set; }
        public bool? SetRaceInfoByObservation { get; set; }
        public int? ContactId { get; set; }

        public ICollection<Borrower> Borrowers { get; set; }

        public ICollection<BorrowerResidence> BorrowerResidences { get; set; }

        public ICollection<EmploymentInfo> EmploymentInfoes { get; set; }

        public ICollection<LoanContactEthnicityBinder> LoanContactEthnicityBinders { get; set; }

        public ICollection<LoanContactRaceBinder> LoanContactRaceBinders { get; set; }

        public Contact Contact { get; set; }

        public DemographicMedium DemographicMedium { get; set; }

        public Gender Gender { get; set; }

        public MaritalStatusList MaritalStatusList { get; set; }

        public MaritalStatusType MaritalStatusType { get; set; }

        public ResidencyState ResidencyState { get; set; }

        public ResidencyType ResidencyType { get; set; }
    }
}