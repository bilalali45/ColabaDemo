using System;
using System.Collections.Generic;
using System.Text;
using RainMaker.Entity.Models;
using RainMaker.Service;

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
