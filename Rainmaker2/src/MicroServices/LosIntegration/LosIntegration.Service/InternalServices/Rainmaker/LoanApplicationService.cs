using System;
using System.Net.Http;
using Extensions.ExtensionClasses;
using LosIntegration.Model.Model.ServiceRequestModels.RainMaker;
using LosIntegration.Model.Model.ServiceResponseModels.Rainmaker;
using Microsoft.Extensions.Configuration;
using ServiceCallHelper;

namespace LosIntegration.Service.InternalServices.Rainmaker
{
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;


        [Flags]
        public enum RelatedEntities : long
        {
            Borrower = (long)1 << 0,
            PropertyInfo = (long)1 << 1,
            Borrower_LoanContact = (long)1 << 2,
            Borrower_EmploymentInfoes = (long)1 << 3,
            Borrower_EmploymentInfoes_OtherEmploymentIncomes = (long)1 << 4,
            Borrower_OtherIncomes_IncomeType = (long)1 << 5,
            Borrower_BorrowerResidences = (long)1 << 6,
            LoanGoal = (long)1 << 7,
            Borrower_BorrowerResidences_OwnershipType = (long)1 << 8,
            Borrower_BorrowerResidences_LoanAddress = (long)1 << 9,
            Borrower_FamilyRelationType = (long)1 << 10,
            PropertyInfo_PropertyType = (long)1 << 11,
            PropertyInfo_PropertyUsage = (long)1 << 12,
            Borrower_PropertyInfo = (long)1 << 13,
            Borrower_PropertyInfo_AddressInfo = (long)1 << 14,
            Borrower_BorrowerAccount = (long)1 << 15,
            Borrower_BorrowerAccount_AccountType = (long)1 << 16,
            Borrower_EmploymentInfoes_OtherEmploymentIncomes_IncomeType = (long)1 << 17,
            Borrower_EmploymentInfoes_AddressInfo = (long)1 << 18,
            Borrower_BorrowerQuestionResponses = (long)1 << 19,
            Borrower_BorrowerQuestionResponses_Question = (long)1 << 20,
            Borrower_BorrowerQuestionResponses_QuestionResponse = (long)1 << 21,
            Borrower_LoanContact_Gender = (long)1 << 22,
            PropertyInfo_PropertyTaxEscrows = (long)1 << 23,
            Borrower_LoanContact_Ethnicity = (long)1 << 24,
            Borrower_LoanContact_Race = (long)1 << 25,
            Borrower_Bankruptcies = (long)1 << 26,
            Borrower_LoanContact_ResidencyType = (long)1 << 27,
            Borrower_LoanContact_ResidencyState = (long)1 << 28,
            Borrower_BorrowerAssets = (long)1 << 29,
            Borrower_EmploymentInfoes_JobType = (long)1 << 30,
            PropertyInfo_AddressInfo = (long)1 << 31,
            PropertyInfo_MortgageOnProperties = (long)1 << 32,
            Borrower_OwnType = (long)1 << 33,
            LoanPurpose = (long)1 << 34,
            Borrower_Consent_ConsentLog = (long)1 << 35,
            Borrower_Liability = (long)1 << 36,
            Borrower_SupportPayments = (long)1 << 37,
            Borrower_OwnerShipInterests = (long)1 << 38,
            Borrower_VaDetails = (long)1 << 39,
            BusinessUnit = (long)1 << 40,
            Opportunity = (long)1 << 41,
            Opportunity_UserProfile = 1L << 42,
            Opportunity_LoanRequest = 1L << 43,
            LosSyncLog = 1L << 44,
            Opportunity_Employee_UserProfile = 1L << 45,
            Opportunity_Employee_Contact = 1L << 46,
            Opportunity_Employee_CompanyPhoneInfo = 1L << 47,
            Opportunity_Employee_EmailAccount = 1L << 48,
            BusinessUnit_LeadSource = 1L << 49,
        }

        public LoanApplicationService(HttpClient _httpClient, IConfiguration _configuration)
        {
            this._httpClient = _httpClient;
            this._configuration = _configuration;
            _baseUrl = _configuration[key: "ServiceAddress:RainMaker:Url"];
        }

        public LoanApplication GetLoanApplicationWithDetails(int loanApplicationId,
                                                             RelatedEntities? includes = null)
        {

            string url = $"{_baseUrl}/api/RainMaker/LoanApplication/GetLoanApplicationForByte?id={loanApplicationId}&tenantId=1&pending=false";
            if (includes.HasValue())
            {
                url = $"{url}e&includes={includes.Value.ToJson()}";
            }
            _httpClient.EasyGet<LoanApplication>(out var getDocumentsCallResponse,
                                                 requestUri: url,
                                                 
                                                 attachAuthorizationHeadersFromCurrentRequest: true
                                                );

            string rawResult = getDocumentsCallResponse.RawResult;

            if (!getDocumentsCallResponse.HttpResponseMessage.IsSuccessStatusCode)
                throw new Exception(message: "Unable to get loan application from Rainmaker");
            return getDocumentsCallResponse.ResponseObject;
        }


        public CallResponse<UpdateLoanApplicationRequest> UpdateLoanApplication(UpdateLoanApplicationRequest updateLoanApplicationRequest)
        {
            var updateLoanApplicationResponse = 
                _httpClient.EasyPostAsync<UpdateLoanApplicationRequest>(requestUri: $"{_baseUrl}/api/RainMaker/LoanApplication/UpdateLoanApplication",
                                                                        content: updateLoanApplicationRequest,
                                                                        attachAuthorizationHeadersFromCurrentRequest: true);
            return updateLoanApplicationResponse.Result;
        }
    }
}
