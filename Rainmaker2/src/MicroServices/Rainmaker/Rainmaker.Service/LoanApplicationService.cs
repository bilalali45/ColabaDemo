using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Collections.Generic;
using System.Text;
using URF.Core.Abstractions;

namespace Rainmaker.Service
{
    public class LoanApplicationService : ServiceBase<RainMakerContext,LoanApplication>, ILoanApplicationService
    {
        public LoanApplicationService(IUnitOfWork<RainMakerContext> previousUow, IServiceProvider services) : base(previousUow,services)
        {
        }
    }
}
