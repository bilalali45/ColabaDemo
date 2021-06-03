using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanApplication.Model;
using LoanApplication.Service;
using LoanApplication.Tests.Helpers;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using TenantConfig.Common.DistributedCache;
using Xunit;

namespace LoanApplication.Tests.Services
{
    public class IncomeServiceTests
    {
        private readonly TenantModel _tenant;

        public IncomeServiceTests()
        {
            _tenant = ObjectHelper.GetTenantModel(1);
        }

        [Fact]
        public async Task GetMilitaryIncome_ShouldReturnRecord()
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(GetMilitaryIncome_ShouldReturnRecord));
            var incomeService = new IncomeService(unitOfWork, null, null,null);

            const int userId = 1;
            const int borrowerId = 1;
            const int incomeInfoId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication { UserId = userId };

            var incomeInfo = new IncomeInfo();
            incomeInfo.Id = incomeInfoId;
            incomeInfo.TenantId = _tenant.Id;
            incomeInfo.BorrowerId = borrowerId;
            incomeInfo.Borrower = new Borrower { Id = borrowerId, LoanApplication = loanApplication };
            incomeInfo.AddressInfo = new AddressInfo();
            incomeInfo.OtherIncomeInfoes = new List<OtherIncomeInfo> { new OtherIncomeInfo { OtherIncomeTypeId = (int)OtherIncomeTypes.MilitaryEntitlements } };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<Borrower>().Insert(incomeInfo.Borrower);
            unitOfWork.Repository<IncomeInfo>().Insert(incomeInfo);
            await unitOfWork.SaveChangesAsync();

            //Act
            MilitaryIncomeModel militaryIncomeModel = await incomeService.GetMilitaryIncome(_tenant, userId, borrowerId, incomeInfoId);

            //Assert
            Assert.NotNull(militaryIncomeModel);
        }

        [Fact]
        public async Task AddOrUpdateMilitaryIncome_ShouldAddRecord()
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateMilitaryIncome_ShouldAddRecord));
            var incomeService = new IncomeService(unitOfWork, null, null,null);

            const int userId = 1;
            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            await unitOfWork.SaveChangesAsync();

            //Act
            var militaryIncomeModel = new MilitaryIncomeModel
            {
                Id = null,
                EmployerName = "Employer Name",
                LoanApplicationId = 1,
                BorrowerId = 1,
                Address = new GenericAddressModel()
            };

            await incomeService.AddOrUpdateMilitaryIncome(_tenant, userId, militaryIncomeModel);

            //Assert
            Assert.Equal(1, unitOfWork.Repository<IncomeInfo>().Query().Count());
        }

        [Fact]
        public async Task AddOrUpdateMilitaryIncome_ShouldUpdateRecord()
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateMilitaryIncome_ShouldUpdateRecord));
            var incomeService = new IncomeService(unitOfWork, null, null,null);

            const int userId = 1;
            const int loanApplicationId = 1;
            const int borrowerId = 1;
            const int incomeInfoId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = loanApplicationId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;

            var borrower = new Borrower { Id = borrowerId, LoanApplication = loanApplication };

            var incomeInfo = new IncomeInfo();
            incomeInfo.Id = incomeInfoId;
            incomeInfo.TenantId = _tenant.Id;
            incomeInfo.Borrower = borrower;
            incomeInfo.AddressInfo = new AddressInfo();
            incomeInfo.OtherIncomeInfoes = new List<OtherIncomeInfo> { new OtherIncomeInfo { OtherIncomeTypeId = (int)OtherIncomeTypes.MilitaryEntitlements } };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<Borrower>().Insert(incomeInfo.Borrower);
            unitOfWork.Repository<IncomeInfo>().Insert(incomeInfo);
            unitOfWork.Repository<OtherIncomeInfo>().Insert(incomeInfo.OtherIncomeInfoes.First());
            unitOfWork.Repository<AddressInfo>().Insert(incomeInfo.AddressInfo);
            await unitOfWork.SaveChangesAsync();

            //Act
            const string updatedEmployerName = "Employer Name - Edit";
            var militaryIncomeModel = new MilitaryIncomeModel
            {
                Id = incomeInfoId,
                EmployerName = updatedEmployerName,
                LoanApplicationId = loanApplicationId,
                BorrowerId = borrowerId,
                Address = new GenericAddressModel()
            };

            await incomeService.AddOrUpdateMilitaryIncome(_tenant, userId, militaryIncomeModel);

            //Assert
            IncomeInfo record = unitOfWork.Repository<IncomeInfo>().Query(x => x.Id == incomeInfo.Id).Single();
            Assert.NotNull(record);
            Assert.Equal(updatedEmployerName, record.EmployerName);
        }
    }
}
