using LosIntegration.Model.Model.ServiceRequestModels.RainMaker;
using LosIntegration.Model.Model.ServiceResponseModels.Rainmaker;
using ServiceCallHelper;

namespace LosIntegration.Service.InternalServices.Rainmaker
{
    public interface ILoanApplicationService
    {
        LoanApplication GetLoanApplicationWithDetails(int loanApplicationId,
                                                      LoanApplicationService.RelatedEntities? includes = null);


        CallResponse<UpdateLoanApplicationRequest> UpdateLoanApplication(UpdateLoanApplicationRequest updateLoanApplicationRequest);
    }
}