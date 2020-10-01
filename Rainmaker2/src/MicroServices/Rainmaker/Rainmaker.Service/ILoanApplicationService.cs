using Rainmaker.Model;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rainmaker.Service
{
    public interface ILoanApplicationService : IServiceBase<LoanApplication>
    {
        Task<LoanSummary> GetLoanSummary(int loanApplicationId,
                                         int userProfileId);


        Task<LoanOfficer> GetLOInfo(int loanApplicationId,
                                    int businessUnitId,
                                    int userProfileId);


        Task<LoanOfficer> GetDbaInfo(int businessUnitId);
        Task<AdminLoanSummary> GetAdminLoanSummary(int loanApplicationId);


        Task<PostModel> PostLoanApplication(int loanApplicationId,
                                            bool isDraft,
                                            int userProfileId,
                                            IOpportunityService opportunityService);
        Task<int> GetMilestoneId(int loanApplicationId);
        Task SetMilestoneId(int loanApplicationId, int milestoneId);

        Task<LoanApplicationModel> GetByLoanApplicationId(int loanId);


        List<LoanApplication> GetLoanApplicationWithDetails(int? id = null,
                                                            string encompassNumber = "",
                                                            LoanApplicationService.RelatedEntities? includes = null);
        Task UpdateLoanInfo(UpdateLoanInfo updateLoanInfo);
        Task<string> GetBanner(int loanApplicationId);
        Task<string> GetFavIcon(int loanApplicationId);
    }
}