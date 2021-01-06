using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ByteWebConnector.SDK.Models.Rainmaker;

namespace ByteWebConnector.SDK.Mismo
{
    public interface IMismoConverter
    {
        string ConvertToMismo(LoanApplication loanApplication);
        void SetLiabilitiesToSkip(List<int?> liabilitiesToSkip);
        List<Borrower> SortRainMakerBorrowers(LoanApplication loanApplication);
    }
}
