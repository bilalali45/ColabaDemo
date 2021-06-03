using System;
using System.Threading.Tasks;
using Extensions.ExtensionClasses;
using LoanApplication.Model;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;

namespace LoanApplication.Service
{
    public class LoanContactService : ServiceBase<LoanApplicationContext, LoanContact>, ILoanContactService
    {
        private readonly IUnitOfWork<TenantConfigContext> _tenantConfigUow;
        private readonly IEncryptionService _encryptionService;

        public LoanContactService(IUnitOfWork<TenantConfigContext> tenantConfigUow,
                                  IUnitOfWork<LoanApplicationContext> previousUow,
                                  IServiceProvider services,IEncryptionService encryptionService) : base(previousUow: previousUow,
                                                                    services: services)
        {
            _tenantConfigUow = tenantConfigUow;
            _encryptionService = encryptionService;
        }


        public async Task<int> UpdateDobSsn(TenantModel tenant,
                                            int userId,
                                            BorrowerDobSsnAddOrUpdate model)
        {
            var borrower = await Uow.Repository<Borrower>().Query(query: borrower1 => borrower1.Id == model.BorrowerId &&
                                                                                      borrower1.LoanApplication.UserId == userId &&
                                                                                      borrower1.TenantId == tenant.Id)
                                    .Include(navigationPropertyPath: borrower1 => borrower1.LoanContact_LoanContactId)
                                    .SingleOrDefaultAsync();

            if (!borrower.HasValue()) return 0;

            //if (borrower.LoanContact.HasValue())
            //{
            //    borrower.LoanContact.TrackingState = TrackingState.Modified;
            //}
            //else
            //{

            //    borrower.LoanContact = new LoanContact
            //                           {
            //                               TrackingState = TrackingState.Added
            //                           };
            //    //borrower.TrackingState = TrackingState.Modified; todo dani check if is needed
            //    Repository.Attach(item: borrower.LoanContact);
            //}

            //borrower.LoanContact.DobUtc = model.DobUtc;
            //borrower.LoanContact.Ssn = model.Ssn;

            if (borrower.LoanContact_LoanContactId.HasValue())
            {
                borrower.LoanContact_LoanContactId.TrackingState = TrackingState.Modified;

                if (model.Ssn.HasValue() && model.Ssn  != "***-**-****")//
                {
                    borrower.LoanContact_LoanContactId.DobUtc = model.DobUtc;
                    borrower.LoanContact_LoanContactId.Ssn = model.Ssn;
                }
                else if (!model.Ssn.HasValue() || model.Ssn== "***-**-****")
                {
                    borrower.LoanContact_LoanContactId.DobUtc = model.DobUtc;

                    var borrowerSsnConfiguration = borrower.OwnTypeId == 1 ? TenantConfigSelection.PrimaryBorrower_SSN : TenantConfigSelection.CoBorrower_SSN;

                    var selection = await Uow.Repository<ConfigSelection>()
                                             .Query(query: configSelection => configSelection.ConfigId == borrowerSsnConfiguration.ToInt() &&
                                                                              configSelection.TenantId == tenant.Id)
                                             .SingleOrDefaultAsync();

                    if ((selection?.SelectionId ?? (byte) TenantConfigSelectionItem.On) == (byte) TenantConfigSelectionItem.On )
                        if (!borrower.LoanContact_LoanContactId.Ssn.HasValue())
                        {
                            return -1;
                        }
                        
                }
            }
            else
            {
                borrower.LoanContact_LoanContactId = new LoanContact
                                       {
                                           DobUtc = model.DobUtc,
                                           Ssn = model.Ssn,
                                           TrackingState = TrackingState.Added
                                       };

                Repository.Attach(item: borrower.LoanContact_LoanContactId);
            }


            (string ssn, string key, string iv) encrypted = _encryptionService.Encrypt(model.Ssn);
            
            borrower.LoanContact_LoanContactId.DobUtc = model.DobUtc;
            borrower.LoanContact_LoanContactId.Ssn = encrypted.ssn;
            
            var LoanContactEncryption = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanContactEncryption>().Query(x => x.LoanContactId == borrower.LoanContact_LoanContactId.Id).SingleOrDefaultAsync();

            if (LoanContactEncryption != null)
            {
                LoanContactEncryption.TrackingState = TrackingState.Modified;
                Uow.Repository<LoanApplicationDb.Entity.Models.LoanContactEncryption>().Update(LoanContactEncryption);
            }
            else
            {
                LoanContactEncryption = new LoanContactEncryption();
                LoanContactEncryption.LoanContactId = borrower.LoanContact_LoanContactId.Id;
             
                LoanContactEncryption.TrackingState = TrackingState.Added;
                Uow.Repository<LoanApplicationDb.Entity.Models.LoanContactEncryption>().Insert(LoanContactEncryption);
            }
            LoanContactEncryption.InitVector = encrypted.iv;
            LoanContactEncryption.EncryptionKey = encrypted.key;

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);
            Repository.ApplyChanges();
            await Uow.SaveChangesAsync();

            return borrower.LoanContact_LoanContactId.Id;
        }


        public async Task<BorrowerDobSsnGet> GetDobSsn(TenantModel tenant,
                                                       int borrowerId,
                                                       int loanApplicationId,
                                                       int userId)
        {
            var borrower = await Uow.Repository<Borrower>().Query(query: borrower1 => borrower1.Id == borrowerId &&
                                                                                      borrower1.LoanApplicationId == loanApplicationId &&
                                                                                      borrower1.LoanApplication.UserId == userId &&
                                                                                      borrower1.TenantId == tenant.Id)
                                    .Include(navigationPropertyPath: borrower1 => borrower1.LoanContact_LoanContactId)
                                    .SingleOrDefaultAsync();

            if (!borrower.HasValue())
                return null;


            return new BorrowerDobSsnGet
                   {
                       DobUtc = borrower.LoanContact_LoanContactId.DobUtc,
                       Ssn =  borrower.LoanContact_LoanContactId.Ssn,
                   };

            // todo dani implement model factory
        }
    }
}