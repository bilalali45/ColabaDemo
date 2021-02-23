using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ByteWebConnector.SDK.Models;
using ByteWebConnector.SDK.Models.Rainmaker;
using ByteWebConnector.SDK.Models;

namespace ByteWebConnector.SDK.Mismo
{
    public interface IMismoConverter
    {
        string ConvertToMismo(LoanApplication loanApplication);
        void SetLiabilitiesToSkip(List<int?> liabilitiesToSkip);
        List<Borrower> SortRainMakerBorrowers(LoanApplication loanApplication);
        ASSETS GetAssets(LoanApplication loanApplication);
        SUBJECT_PROPERTY GetSubjectProperty(LoanApplication loanApplication);
        EXPENSES GetExpenseLiabilities(LoanApplication loanApplication);
        LIABILITIES GetLiabilities(LoanApplication loanApplication);
        List<LOAN> GetLoanInfo(LoanApplication loanApplication);
        List<PARTY> GetBorrowers(LoanApplication loanApplication);
        GOVERNMENT_MONITORING GetGovernmentMonitoring(Borrower rmBorrower);
        List<RESIDENCE> GetBorrowerResidences(Borrower rmBorrower);
        BORROWER_DETAIL GetBorrowerDetail(Borrower rmBorrower);
    }
}
