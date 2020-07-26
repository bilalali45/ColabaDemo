using System;
using RainMaker.Entity.Models;

namespace Rainmaker.Model.Borrower
{
    public class RainmakerBorrower
    {
        public RainmakerBorrower()
        {
            
        }

        public string CellPhone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Suffix { get; set; }
        public string HomePhone { get; set; }
        public string EmailAddress { get; set; }
        public int MaritalStatusId { get; set; }
        public int ResidencyStateId { get; set; }
        public int GenderId { get; set; }
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
        public int EthnicityId { get; set; }
        public int EthnicityDetailId { get; set; }
        public int RaceId { get; set; }
        public int RaceDetailId { get; set; }
        public string FileDataId { get; set; }


        public  void PopulateEntity(RainMaker.Entity.Models.Borrower entity)
        {
            entity.LoanContact.CellPhone = CellPhone;
            entity.LoanContact.FirstName = this.FirstName;
            entity.LoanContact.LastName = this.LastName;
            entity.LoanContact.MiddleName = this.MiddleName;
            entity.LoanContact.Suffix = this.Suffix;
            entity.LoanContact.HomePhone = this.HomePhone;
            entity.LoanContact.MaritalStatusId = this.MaritalStatusId;
            entity.LoanContact.ResidencyStateId = this.ResidencyStateId;
            entity.LoanContact.GenderId = this.GenderId;
            entity.NoOfDependent = this.NoOfDependent;
            entity.DependentAge = this.DependentAge;
          
            foreach (var ethnicityBinder in entity.LoanContact.LoanContactEthnicityBinders)
            {
                ethnicityBinder.EthnicityId = this.EthnicityId;
                ethnicityBinder.EthnicityDetailId = this.EthnicityDetailId;
            }
            foreach (var raceBinder in entity.LoanContact.LoanContactRaceBinders)
            {
                raceBinder.RaceId = this.RaceId;
                raceBinder.RaceDetailId = this.RaceDetailId;
            }








        }
    }
}