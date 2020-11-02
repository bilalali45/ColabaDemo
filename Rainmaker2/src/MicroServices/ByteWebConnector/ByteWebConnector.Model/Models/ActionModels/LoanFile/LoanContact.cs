













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // LoanContact

    public partial class LoanContact 
    {
        public int Id { get; set; } // Id (Primary key)
        public string FirstName { get; set; } // FirstName (length: 300)
        public string MiddleName { get; set; } // MiddleName (length: 50)
        public string LastName { get; set; } // LastName (length: 300)
        public string NickName { get; set; } // NickName (length: 50)
        public string Prefix { get; set; } // Prefix (length: 10)
        public string Suffix { get; set; } // Suffix (length: 10)
        public string Preferred { get; set; } // Preferred (length: 1000)
        public string AlternateFirstName { get; set; } // AlternateFirstName (length: 300)
        public string AlternateMiddleName { get; set; } // AlternateMiddleName (length: 50)
        public string AlternateLastName { get; set; } // AlternateLastName (length: 300)
        public string AlternatePrefix { get; set; } // AlternatePrefix (length: 10)
        public string AlternateSuffix { get; set; } // AlternateSuffix (length: 10)
        public string Company { get; set; } // Company (length: 500)
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDeleted { get; set; } // IsDeleted
        public int? PreferredId { get; set; } // PreferredId
        public string Ssn { get; set; } // Ssn (length: 256)
        public System.DateTime? DobUtc { get; set; } // DobUtc
        public int? YrsSchool { get; set; } // YrsSchool
        public int? MaritalStatusId { get; set; } // MaritalStatusId
        public int? UnmarriedStatusId { get; set; } // UnmarriedStatusId
        public bool? IsPlanToMarrySoon { get; set; } // IsPlanToMarrySoon
        public string OtherMaritalStatus { get; set; } // OtherMaritalStatus (length: 150)
        public int? RelationFormedStateId { get; set; } // RelationFormedStateId
        public int? GenderId { get; set; } // GenderId
        public string HomePhone { get; set; } // HomePhone (length: 50)
        public string WorkPhone { get; set; } // WorkPhone (length: 50)
        public string WorkPhoneExt { get; set; } // WorkPhoneExt (length: 50)
        public string CellPhone { get; set; } // CellPhone (length: 50)
        public string EmailAddress { get; set; } // EmailAddress (length: 150)
        public string OtherEmailAddress { get; set; } // OtherEmailAddress (length: 150)
        public int? ResidencyTypeId { get; set; } // ResidencyTypeId
        public int? ResidencyStateId { get; set; } // ResidencyStateId
        public int? DemographicMediumId { get; set; } // DemographicMediumId
        public bool? SetEthnicityInfoByObservation { get; set; } // SetEthnicityInfoByObservation
        public bool? SetGenderInfoByObservation { get; set; } // SetGenderInfoByObservation
        public bool? SetRaceInfoByObservation { get; set; } // SetRaceInfoByObservation
        public int? ContactId { get; set; } // ContactId

        // Reverse navigation

        /// <summary>
        /// Child Borrowers where [Borrower].[LoanContactId] point to this entity (FK_Borrower_LoanContact)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Borrower> Borrowers { get; set; } // Borrower.FK_Borrower_LoanContact
        /// <summary>
        /// Child BorrowerResidences where [BorrowerResidence].[LandLordContactId] point to this entity (FK_BorrowerResidence_LoanContact)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerResidence> BorrowerResidences { get; set; } // BorrowerResidence.FK_BorrowerResidence_LoanContact
        /// <summary>
        /// Child EmploymentInfoes where [EmploymentInfo].[ContactPersonId] point to this entity (FK_EmploymentInfo_LoanContact)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmploymentInfo> EmploymentInfoes { get; set; } // EmploymentInfo.FK_EmploymentInfo_LoanContact
        /// <summary>
        /// Child LoanContactEthnicityBinders where [LoanContactEthnicityBinder].[LoanContactId] point to this entity (FK_LoanContactEthnicityBinder_LoanContact)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanContactEthnicityBinder> LoanContactEthnicityBinders { get; set; } // LoanContactEthnicityBinder.FK_LoanContactEthnicityBinder_LoanContact
        /// <summary>
        /// Child LoanContactRaceBinders where [LoanContactRaceBinder].[LoanContactId] point to this entity (FK_LoanContactRaceBinder_LoanContact)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanContactRaceBinder> LoanContactRaceBinders { get; set; } // LoanContactRaceBinder.FK_LoanContactRaceBinder_LoanContact

        // Foreign keys

        /// <summary>
        /// Parent Contact pointed by [LoanContact].([ContactId]) (FK_LoanContact_Contact)
        /// </summary>
        public virtual Contact Contact { get; set; } // FK_LoanContact_Contact

        /// <summary>
        /// Parent DemographicMedium pointed by [LoanContact].([DemographicMediumId]) (FK_LoanContact_DemographicMedium)
        /// </summary>
        public virtual DemographicMedium DemographicMedium { get; set; } // FK_LoanContact_DemographicMedium

        /// <summary>
        /// Parent Gender pointed by [LoanContact].([GenderId]) (FK_LoanContact_Gender)
        /// </summary>
        public virtual Gender Gender { get; set; } // FK_LoanContact_Gender

        /// <summary>
        /// Parent MaritalStatusList pointed by [LoanContact].([MaritalStatusId]) (FK_LoanContact_MaritalStatusList)
        /// </summary>
        public virtual MaritalStatusList MaritalStatusList { get; set; } // FK_LoanContact_MaritalStatusList

        /// <summary>
        /// Parent MaritalStatusType pointed by [LoanContact].([UnmarriedStatusId]) (FK_LoanContact_MaritalStatusType)
        /// </summary>
        public virtual MaritalStatusType MaritalStatusType { get; set; } // FK_LoanContact_MaritalStatusType

        /// <summary>
        /// Parent ResidencyState pointed by [LoanContact].([ResidencyStateId]) (FK_LoanContact_ResidencyState)
        /// </summary>
        public virtual ResidencyState ResidencyState { get; set; } // FK_LoanContact_ResidencyState

        /// <summary>
        /// Parent ResidencyType pointed by [LoanContact].([ResidencyTypeId]) (FK_LoanContact_ResidencyType)
        /// </summary>
        public virtual ResidencyType ResidencyType { get; set; } // FK_LoanContact_ResidencyType

        public LoanContact()
        {
            EntityTypeId = 193;
            IsDeleted = false;
            Borrowers = new System.Collections.Generic.HashSet<Borrower>();
            BorrowerResidences = new System.Collections.Generic.HashSet<BorrowerResidence>();
            EmploymentInfoes = new System.Collections.Generic.HashSet<EmploymentInfo>();
            LoanContactEthnicityBinders = new System.Collections.Generic.HashSet<LoanContactEthnicityBinder>();
            LoanContactRaceBinders = new System.Collections.Generic.HashSet<LoanContactRaceBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
