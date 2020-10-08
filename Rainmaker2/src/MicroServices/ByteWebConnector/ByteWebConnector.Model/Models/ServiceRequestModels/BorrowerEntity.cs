using System.Collections.Generic;

namespace ByteWebConnector.Model.Models.ServiceRequestModels
{
    public class BorrowerEntity
    {
        public int? BorrowerId { get; set; }
        public long FileDataId { get; set; }
        public string CellPhone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Suffix { get; set; }
        public string HomePhone { get; set; }
        public string EmailAddress { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? ResidencyStateId { get; set; }
        public List<int> GenderIds { get; set; }
        public int? NoOfDependent { get; set; }
        public string DependentAge { get; set; }
        public int OutstandingJudgementsIndicator { get; set; }
        public int BankruptcyIndicator { get; set; }
        public int PartyToLawsuitIndicator { get; set; }
        public int LoanForeclosureOrJudgementIndicator { get; set; }
        public int AlimonyChildSupportObligationIndicator { get; set; }
        public int BorrowedDownPaymentIndicator { get; set; }
        public int CoMakerEndorserOfNoteIndicator { get; set; }
        public int HomeownerPastThreeYearsIndicator { get; set; }
        public int PresentlyDelinquentIndicator { get; set; }
        public int IntentToOccupyIndicator { get; set; }
        public int DeclarationsJIndicator { get; set; }
        public int DeclarationsKIndicator { get; set; }
        public string PriorPropertyUsageType { get; set; }
        public string PriorPropertyTitleType { get; set; }
        public List<RaceInfoItem> RaceInfo { get; set; }
        public List<EthnicInfoItem> EthnicityInfo { get; set; }
        public string OldFirstName { get; set; }
        public string OldEmailAddress { get; set; }
        public bool IsAddOrUpdate { get; set; }
    }
}