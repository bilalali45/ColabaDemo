using System;
using System.Collections.Generic;
using System.Text;
using LoanApplicationDb.Data;
using Microsoft.EntityFrameworkCore;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;

namespace LoanApplication.Tests.Services
{
    public class UnitTestBase
    {
        protected DbContextOptions<LoanApplicationContext> LoanApplicationContextOptions
        {
            get
            {
                var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
                loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
                DbContextOptions<LoanApplicationContext> dbContextOptions = loanBuilder.Options;
                //using LoanApplicationContext loanApplicationContext = new LoanApplicationContext(loanOptions);
                //loanApplicationContext.Database.EnsureCreated();
                //loanApplicationContext.SaveChanges();

                return dbContextOptions;
            }
        }

        protected IUnitOfWork<T> GetUnitOfWorkInstance<T>(T context) where T : DbContext
        {
            return new UnitOfWork<T>(context, new RepositoryProvider(new RepositoryFactories()));
        }
    }
}
