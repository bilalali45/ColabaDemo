using RainMaker.Entity.Models;
using RainMaker.Service;
using System.Collections.Generic;

namespace Rainmaker.Service
{
    public interface IBorrowerService : IServiceBase<Borrower>
    {
        


        List<Borrower> GetBorrowerWithDetails(
        string firstName = "",
        string lastName = "",
        string email = "",
        int? loanApplicationId = null,
        string encompassId = "",
        BorrowerService.RelatedEntity? includes = null);
    }
}
