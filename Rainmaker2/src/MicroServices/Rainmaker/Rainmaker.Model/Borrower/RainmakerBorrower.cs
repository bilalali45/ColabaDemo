using System;
using System.Collections.Generic;
using System.Linq;
using RainMaker.Entity.Models;
using TrackableEntities.Common.Core;

namespace Rainmaker.Model.Borrower
{
    public class RainmakerBorrower
    {
        public RainmakerBorrower()
        {
            
        }

        public int? BorrowerId { get; set; }
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
        public List<int> EthnicityIds { get; set; }
        public List<int> EthnicityDetailIds { get; set; }
        public List<int> RaceIds { get; set; }
        public List<int> RaceDetailIds { get; set; }
        public string FileDataId { get; set; }
        public List<int> EthnicityDetailId { get; set; }
        public Dictionary<int, List<int>> EthnicityDictionary { get; set; }

        public List<RaceInfoItem> RaceInfo { get; set; }


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
            entity.LoanContact.GenderId = this.GenderIds[0];
            entity.LoanContact.TrackingState = TrackingState.Modified;
            entity.NoOfDependent = this.NoOfDependent;
            entity.DependentAge = this.DependentAge;

            //entity.LoanContact.LoanContactEthnicityBinders delete all
            entity.LoanContact.LoanContactEthnicityBinders.Select(loanContactEthnicityBinder =>
            {
                return loanContactEthnicityBinder.TrackingState = TrackingState.Deleted;

            }).ToList();
           
            foreach (var ethnicity in EthnicityDictionary)
            {
                int binderEthnicityId = ethnicity.Key;
                int valueCount = ethnicity.Value.Count;
                if (valueCount > 0)
                {
                    foreach (int detailId in ethnicity.Value)
                    {
                        var ethnicityBinder = new LoanContactEthnicityBinder();
                        //ethnicityBinder.LoanContactId = entity.LoanContactId.Value;
                        ethnicityBinder.EthnicityDetailId = detailId;
                        ethnicityBinder.EthnicityId = binderEthnicityId;
                        ethnicityBinder.TrackingState = TrackingState.Added;
                        entity.LoanContact.LoanContactEthnicityBinders.Add(ethnicityBinder);
                    }
                   
                }
                else
                {
                    var ethnicityBinder = new LoanContactEthnicityBinder();
                    //ethnicityBinder.LoanContactId = entity.LoanContactId.Value;
                    ethnicityBinder.EthnicityDetailId = null;
                    ethnicityBinder.EthnicityId = binderEthnicityId;
                    ethnicityBinder.TrackingState = TrackingState.Added;
                    entity.LoanContact.LoanContactEthnicityBinders.Add(ethnicityBinder);
                }
            }

            //entity.LoanContact.LoanContactRaceBinders delete all
            entity.LoanContact.LoanContactRaceBinders.Select(loanContactRaceBinder =>
            {
                return loanContactRaceBinder.TrackingState = TrackingState.Deleted;
            }).ToList();


            

            foreach (var raceInfoItem in RaceInfo)
            {
                var raceBinder = new LoanContactRaceBinder();
                //raceBinder.LoanContactId = entity.LoanContactId.Value;
                raceBinder.RaceDetailId = raceInfoItem.RaceDetailId;
                raceBinder.RaceId = raceInfoItem.RaceId.Value;
                raceBinder.TrackingState = TrackingState.Added;
                entity.LoanContact.LoanContactRaceBinders.Add(raceBinder);
            }








        }
    }
}