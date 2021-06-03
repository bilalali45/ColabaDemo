using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
using MilitaryAffiliation = LoanApplication.Model.MilitaryAffiliation;

namespace LoanApplication.Service
{
    public class VaDetailService : ServiceBase<LoanApplicationContext, VaDetail>, IVaDetailService
    {
        private readonly IUnitOfWork<TenantConfigContext> _tenantConfigUow;


        public VaDetailService(IUnitOfWork<LoanApplicationContext> previousUow,
                               IServiceProvider services,
                               IUnitOfWork<TenantConfigContext> tenantConfigUow) : base(previousUow: previousUow,
                                                                                        services: services)
        {
            _tenantConfigUow = tenantConfigUow;
        }


        public async Task<int> AddOrUpdate(TenantModel tenant,
                                           int userId,
                                           VaDetailAddOrUpdate model)
        {
            var borrower = await Uow.Repository<Borrower>().Query(query: borrower => borrower.Id == model.BorrowerId &&
                                                                                     borrower.LoanApplication.UserId == userId &&
                                                                                     borrower.TenantId == tenant.Id)
                                    .Include(navigationPropertyPath: borrower => borrower.VaDetails).Include(x=>x.LoanApplication).SingleOrDefaultAsync();

            if (!borrower.HasValue())
                return 0;

            borrower.IsVaEligible = model.IsVaEligible;
            borrower.TrackingState = TrackingState.Modified;

            foreach (var vaDetail in borrower.VaDetails)
                vaDetail.TrackingState = TrackingState.Deleted;

            //borrower.IsVaEligible = model.IsVaEligible;
            if (model.IsVaEligible)
            {
                foreach (var militaryAffiliationId in model.MilitaryAffiliationIds)
                {
                    var vaDetail = new VaDetail();
                    vaDetail.MilitaryAffiliationId = militaryAffiliationId;

                    if (militaryAffiliationId == (byte)MilitaryAffiliationEnum.Active_Military)
                        vaDetail.ExpirationDateUtc = model.ExpirationDateUtc;

                    if (militaryAffiliationId == (byte)MilitaryAffiliationEnum.Reserves_National_Guard)
                        vaDetail.ReserveEverActivated = model.ReserveEverActivated;

                    vaDetail.BorrowerId = model.BorrowerId;
                    vaDetail.TenantId = tenant.Id;
                    vaDetail.TrackingState = TrackingState.Added;
                    Repository.Attach(item: vaDetail);
                }
            }
            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == borrower.LoanApplication.Id && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(application);
            Repository.ApplyChanges();
            await Uow.SaveChangesAsync();

            return borrower.Id;
        }


        public async Task<BorrowerVaDetailGetModel> GetVaDetails(TenantModel tenant,
                                                                 int userId,
                                                                 int borrowerId)
        {
            var borrower = await Uow.Repository<Borrower>().Query(query: borrower => borrower.Id == borrowerId &&
                                                                                     borrower.LoanApplication.UserId == userId &&
                                                                                     borrower.TenantId == tenant.Id)
                                    .Include(navigationPropertyPath: borrower => borrower.VaDetails).SingleOrDefaultAsync();

            var borrowerVaDetailGetModal = new BorrowerVaDetailGetModel();

            borrowerVaDetailGetModal.IsVaEligible = borrower.IsVaEligible;

            borrowerVaDetailGetModal.VaDetails = null;
            if (borrowerVaDetailGetModal.IsVaEligible == true)
            {
                var vaDetails = borrower.VaDetails.Select(selector: vaDetail => new VaDetailModal
                {
                    MilitaryAffiliationId = vaDetail.MilitaryAffiliationId,
                    ExpirationDateUtc = vaDetail.ExpirationDateUtc,
                    ReserveEverActivated = vaDetail.ReserveEverActivated,
                }).ToList();
                ArrayList affiliationList = new ArrayList();
                foreach (var detail in vaDetails)
                {
                    switch ((MilitaryAffiliationEnum) Convert.ToByte(detail.MilitaryAffiliationId))
                    {
                        case MilitaryAffiliationEnum.Active_Military:
                            affiliationList.Add(new ActiveDutyPersonnel()
                            {
                                MilitaryAffiliationId = detail.MilitaryAffiliationId,
                                ExpirationDateUtc = detail.ExpirationDateUtc
                            });
                            break;

                        case MilitaryAffiliationEnum.Reserves_National_Guard:
                            affiliationList.Add(new ReserveNationalGuard()
                            {
                                MilitaryAffiliationId = detail.MilitaryAffiliationId,
                                ReserveEverActivated = detail.ReserveEverActivated
                            });
                            break;

                        case MilitaryAffiliationEnum.Surviving_Spouse:
                        case MilitaryAffiliationEnum.Veteran:
                            affiliationList.Add(new MilitaryAffiliation()
                            {
                                MilitaryAffiliationId = detail.MilitaryAffiliationId
                            });
                            break;
                    }
                }

                borrowerVaDetailGetModal.VaDetails = affiliationList;
            }

            return borrowerVaDetailGetModal;
        }

        public async Task<int> SetBorrowerVaStatus(TenantModel tenant, int userId, BorrowerVaStatusModel model)
        {
            var borrower = await Uow.Repository<Borrower>().Query(query: borrower => borrower.Id == model.BorrowerId &&
                                                                                     borrower.LoanApplication.UserId == userId &&
                                                                                     borrower.TenantId == tenant.Id)
                                    .Include(navigationPropertyPath: borrower => borrower.VaDetails).Include(x => x.LoanApplication).SingleOrDefaultAsync();

            if (!borrower.HasValue())
                return 0;

            borrower.IsVaEligible = model.IsVaEligible;
            this.Uow.Repository<Borrower>().Update(borrower);

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == borrower.LoanApplication.Id && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(application);
            Repository.ApplyChanges();
            await Uow.SaveChangesAsync();

            return borrower.Id;
        }

        public async Task<bool?> GetBorrowerVaStatus(TenantModel tenant, int userId, int borrowerId)
        {
            var borrower = await Uow.Repository<Borrower>().Query(query: b => b.Id == borrowerId 
                    && b.LoanApplication.UserId == userId
                    && b.TenantId == tenant.Id)
                .SingleOrDefaultAsync();

            return borrower.IsVaEligible;
        }
    }
}