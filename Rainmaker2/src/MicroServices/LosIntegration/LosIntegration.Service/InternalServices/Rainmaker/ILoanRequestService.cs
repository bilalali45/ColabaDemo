using LosIntegration.Model.Model.ServiceResponseModels.Rainmaker;

namespace LosIntegration.Service.InternalServices.Rainmaker
{
    public interface ILoanRequestService
    {
        LoanRequest GetLoanRequestWithDetails(int loanApplicationId,
                                              LoanRequestService.RelatedEntities? includes = null);
    }
}