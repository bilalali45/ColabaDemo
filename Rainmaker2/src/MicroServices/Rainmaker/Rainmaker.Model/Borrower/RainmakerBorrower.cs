using RainMaker.Common;
using RainMaker.Common.Extensions;
using RainMaker.Entity.Models;
using System.Collections.Generic;
using System.Linq;
using TrackableEntities.Common.Core;

namespace Rainmaker.Model.Borrower
{
    public class RainmakerBorrower
    {
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
        public string FileDataId { get; set; }
        public List<RaceInfoItem> RaceInfo { get; set; }
        public List<EthnicInfoItem> EthnicityInfo { get; set; }
        public string OldFirstName { get; set; }
        public string OldEmailAddress { get; set; }
        public bool IsAddOrUpdate { get; set; }


        public void PopulateEntity(RainMaker.Entity.Models.Borrower entity)
        {
            #region LoanContact

            entity.LoanContact.CellPhone = CellPhone;
            entity.LoanContact.FirstName = FirstName;
            entity.LoanContact.LastName = LastName;
            entity.LoanContact.MiddleName = MiddleName;
            entity.LoanContact.Suffix = Suffix;
            entity.LoanContact.HomePhone = HomePhone;
            entity.LoanContact.MaritalStatusId = MaritalStatusId;
            entity.LoanContact.ResidencyStateId = ResidencyStateId;
            entity.LoanContact.GenderId = GenderIds.Count > 0 ? GenderIds[index: 0] : entity.LoanContact.GenderId;

            #endregion

            #region Borrower

         
            entity.NoOfDependent = NoOfDependent;
            entity.DependentAge = DependentAge;

            #endregion

            #region Ethnicity

            //entity.LoanContact.LoanContactEthnicityBinders delete all
            if (entity.LoanContact.LoanContactEthnicityBinders != null)
            {
                entity.LoanContact.LoanContactEthnicityBinders.ToList().ForEach(loanContactEthnicityBinder =>
                {
                    loanContactEthnicityBinder.TrackingState =
                        TrackingState.Deleted;
                });
            }

            foreach (var ethnicInfoItem in EthnicityInfo)
            {
                var ethnicityBinder = new LoanContactEthnicityBinder();
            
                ethnicityBinder.EthnicityDetailId = ethnicInfoItem.EthnicDetailId;
                ethnicityBinder.EthnicityId = ethnicInfoItem.EthnicId.Value;
                ethnicityBinder.TrackingState = TrackingState.Added;
                entity.LoanContact.LoanContactEthnicityBinders.Add(item: ethnicityBinder);
            }

            #endregion

            #region Race

            //entity.LoanContact.LoanContactRaceBinders delete all
            if (entity.LoanContact.LoanContactRaceBinders != null)
            {
                entity.LoanContact.LoanContactRaceBinders.ToList().ForEach(loanContactRaceBinder =>
                {
                    loanContactRaceBinder.TrackingState =
                        TrackingState.Deleted;
                });
            }

            foreach (var raceInfoItem in RaceInfo)
            {
                var raceBinder = new LoanContactRaceBinder();
              
                raceBinder.RaceDetailId = raceInfoItem.RaceDetailId;
                raceBinder.RaceId = raceInfoItem.RaceId.Value;
                raceBinder.TrackingState = TrackingState.Added;
                entity.LoanContact.LoanContactRaceBinders.Add(item: raceBinder);
            }

            #endregion

            //Are there any outstanding judgments against you?	36
            PrepareBorrowerQuestionResponse(entity: entity,
                                            questionId: 36,
                                            answerText: OutstandingJudgementsIndicator.ToString());

            //Have you been declared bankrupt within the past 7 years?	37
            PrepareBorrowerQuestionResponse(entity: entity,
                                            questionId: 37,
                                            answerText: BankruptcyIndicator.ToString());

            //Have you had property foreclosed upon or given title or deed in lieu thereof in the last 7 years?	38
            PrepareBorrowerQuestionResponse(entity: entity,
                                            questionId: 38,
                                            answerText: PartyToLawsuitIndicator.ToString());

            //Are you a party to a lawsuit?	39
            PrepareBorrowerQuestionResponse(entity: entity,
                                            questionId: 39,
                                            answerText: PartyToLawsuitIndicator.ToString());

            //Have you directly or indirectly been obligated on any loan which resulted in foreclosure, transfer of title in lieu of foreclosure, or judgment?	40
            PrepareBorrowerQuestionResponse(entity: entity,
                                            questionId: 40,
                                            answerText: LoanForeclosureOrJudgementIndicator.ToString());

            //Are you presently delinquent or in default on any Federal debt or any other loan, mortgage, financial obligation, bond or loan guarantee?	41
            PrepareBorrowerQuestionResponse(entity: entity,
                                            questionId: 41,
                                            answerText: PresentlyDelinquentIndicator.ToString());

            //Is any part of the down payment borrowed?	42
            PrepareBorrowerQuestionResponse(entity: entity,
                                            questionId: 42,
                                            answerText: BorrowedDownPaymentIndicator.ToString());



            //Are you a co-maker or endorser on a note?	43
            PrepareBorrowerQuestionResponse(entity: entity,
                                            questionId: 43,
                                            answerText: CoMakerEndorserOfNoteIndicator.ToString());

            //Are you a US citizen?	44
            PrepareBorrowerQuestionResponse(entity: entity,
                                            questionId: 44,
                                            answerText: DeclarationsJIndicator.ToString());




            //Do you intend to occupy the property as your primary residence?	45
            PrepareBorrowerQuestionResponse(entity: entity,
                                            questionId: 45,
                                            answerText: IntentToOccupyIndicator.ToString());

            //Are you obligated to pay alimony, child support, or separate maintenance?	46
            PrepareBorrowerQuestionResponse(entity: entity,
                                            questionId: 46,
                                            answerText: AlimonyChildSupportObligationIndicator.ToString());

            //Have you had an ownership interest in a property in the last three years?	47
            PrepareBorrowerQuestionResponse(entity: entity,
                                            questionId: 47,
                                            answerText: HomeownerPastThreeYearsIndicator.ToString());

            //What type of property did you own? Select the choice that fits best.	49
            PrepareBorrowerQuestionResponse(entity: entity,
                                            questionId: 49,
                                            answerText: PriorPropertyUsageType);

            //How did you hold title to this property?	50
            PrepareBorrowerQuestionResponse(entity: entity,
                                            questionId: 50,
                                            answerText: PriorPropertyTitleType);

            //Are you a permanent resident alien?	54
            PrepareBorrowerQuestionResponse(entity: entity,
                                            questionId: 54,
                                            answerText: DeclarationsKIndicator.ToString());

            #region Declarations

            #endregion
        }






        private void PrepareBorrowerQuestionResponse(RainMaker.Entity.Models.Borrower entity,
                                                     int questionId,
                                                     string answerText)
        {
            var borrowerQuestionResponse =
                entity.BorrowerQuestionResponses.SingleOrDefault(predicate: bqr => bqr.QuestionId == questionId);
            if (borrowerQuestionResponse != null)
            {
                //edit answer
                borrowerQuestionResponse.QuestionResponse.TrackingState = TrackingState.Modified;
            }
            else
            {
                // add answer
                borrowerQuestionResponse = new BorrowerQuestionResponse
                                           {
                                               TrackingState = TrackingState.Added,
                                               QuestionId = questionId
                                           };
                borrowerQuestionResponse.QuestionResponse = new QuestionResponse
                                                            {
                                                                TrackingState = TrackingState.Added
                                                            };
                entity.BorrowerQuestionResponses.Add(item: borrowerQuestionResponse);
            }

            borrowerQuestionResponse.QuestionResponse.AnswerText = answerText;

            switch (questionId)
            {

                case (int) DeclarationQuestionEnum.BorrowedDownPaymentIndicator
                    : //Is any part of the down payment borrowed?
                    Handle42Case();
                    break;
               
                case (int) DeclarationQuestionEnum.DeclarationsJIndicator: //Are you a US citizen?
                    Handle44Case(borrowerQuestionResponse.QuestionResponse,
                                 entity);
                    break;
                case (int) DeclarationQuestionEnum.DeclarationsKIndicator: //Are you a permanent resident alien?
                    Handle54Case(DeclarationsJIndicator.ToString(),
                                 borrowerQuestionResponse.QuestionResponse,
                                 entity);
                    break;
                case (int) DeclarationQuestionEnum.ValidWorkVisa: //Are you holding a valid work visa?

                    Hanlde57Case(DeclarationsJIndicator.ToString(),
                                 DeclarationsKIndicator.ToString(),
                                 borrowerQuestionResponse.QuestionResponse,
                                 entity);
                    break;
                case (int) DeclarationQuestionEnum.AlimonyChildSupportObligationIndicator
                    : //Are you obligated to pay alimony, child support, or separate maintenance?
                    break;
                case (int) DeclarationQuestionEnum.HomeownerPastThreeYearsIndicator
                    : //Have you had an ownership interest in a property in the last three years?
                    Handle47Case(IntentToOccupyIndicator.ToString(),
                                 borrowerQuestionResponse.QuestionResponse);

                    break;

               
                case (int) DeclarationQuestionEnum.PriorPropertyUsageType
                    : //What type of property did you own? Select the choice that fits best.
                    Handle49Case(HomeownerPastThreeYearsIndicator.ToString(),
                                 borrowerQuestionResponse.QuestionResponse);
                    break;
                case (int) DeclarationQuestionEnum.PriorPropertyTitleType
                    : Handle50Case(HomeownerPastThreeYearsIndicator.ToString(),
                                 borrowerQuestionResponse.QuestionResponse);
                    break;
            }


        }


        private void Handle47Case(string question45Answer,
                                  QuestionResponse questionResponse)
        {


            if (question45Answer == QuestionAnswerEnum.No.ToInt().ToString())
            {
                questionResponse.AnswerText = null;
                questionResponse.AnswerText = "";
             
            }
        }


        private void Handle50Case(string question47Response,
                                  QuestionResponse questionResponse)
        {
            if (question47Response == QuestionAnswerEnum.Yes.ToInt().ToString())
            {
                //do nothing
            }
            else
            {
                questionResponse.AnswerText = "";
            }
        }


        private void Handle49Case(string question47Response,
                                  QuestionResponse questionResponse)
        {
            if (question47Response == QuestionAnswerEnum.Yes.ToInt().ToString())
            {
                //do nothing 
            }
            else
            {

                questionResponse.AnswerText = "";
            }
        }

        private void Hanlde57Case(string question44Response,
                                  string question54Response,
                                  QuestionResponse questionResponse,
                                  RainMaker.Entity.Models.Borrower entity)
        {



            if (question44Response == QuestionAnswerEnum.No.ToInt().ToString())
            {
                if (question54Response == QuestionAnswerEnum.No.ToInt().ToString())
                {
                    entity.LoanContact.ResidencyStateId = questionResponse.AnswerText.ToInt();
                    entity.LoanContact.ResidencyTypeId = ResidencyTypeEnum.ValidworkVisa.ToInt();
                    entity.TrackingState = TrackingState.Modified;

                }
                else
                {
                    questionResponse.AnswerText = "0";
                }

            }
            else
            {
                questionResponse.AnswerText = "0";
            }


        }


        private void Handle54Case(string question44Response,
                                  QuestionResponse questionResponse,
                                  RainMaker.Entity.Models.Borrower entity)
        {

            if (question44Response == QuestionAnswerEnum.No.ToInt().ToString())
            {
                if (questionResponse.AnswerText == QuestionAnswerEnum.Yes.ToInt().ToString())
                {

                    entity.LoanContact.ResidencyStateId = ResidencyTypeEnum.PermanentResident.ToInt();
                    entity.LoanContact.ResidencyTypeId = ResidencyTypeEnum.PermanentResident.ToInt();
                    entity.TrackingState = TrackingState.Modified;


                }
            }
            else
            {
                questionResponse.AnswerText = "";
            }
        }


        private void Handle44Case(QuestionResponse questionResponse,
                                  RainMaker.Entity.Models.Borrower entity)
        {
            if (questionResponse.AnswerText == QuestionAnswerEnum.Yes.ToInt().ToString())
            {

                entity.LoanContact.ResidencyStateId = ResidencyTypeEnum.UsCitizen.ToInt();
                entity.LoanContact.ResidencyTypeId = ResidencyTypeEnum.UsCitizen.ToInt();
                entity.TrackingState = TrackingState.Modified;



            }
        }


        private void Handle42Case()
        {
            // implementation will be added later
        }
    }
}