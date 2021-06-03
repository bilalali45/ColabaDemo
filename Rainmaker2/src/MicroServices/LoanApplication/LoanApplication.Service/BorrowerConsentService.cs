using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LoanApplication.Model;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using TenantConfig.Common.DistributedCache;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;

namespace LoanApplication.Service
{
    public class BorrowerConsentService : ServiceBase<LoanApplicationContext, BorrowerConsent>, IBorrowerConsentService
    {
        private readonly IDbFunctionService _dbFunctionService;

        public BorrowerConsentService(IUnitOfWork<LoanApplicationContext> previousUow, IServiceProvider services,
            IDbFunctionService dbFunctionService) : base(previousUow,
            services)
        {
            _dbFunctionService = dbFunctionService;
        }


        public async Task<int> AddOrUpdate(TenantModel tenant, BorrowerConsentModel model, string ipAddress, int userId)
        {
            var borrowerInfo = await Uow.Repository<Borrower>()
                .Query(b => b.Id == model.BorrowerId && b.LoanApplicationId == model.LoanApplicationId &&
                            b.LoanApplication.UserId == userId)
                .Include(b => b.LoanContact_LoanContactId)
                .SingleOrDefaultAsync();


            var borrowerConsent = new BorrowerConsent
            {
                ConsentTypeId = model.ConsentTypeId,
                BorrowerId = model.BorrowerId,
                LoanApplicationId = model.LoanApplicationId,
                ConsentText = model.Description,
                Email = borrowerInfo.LoanContact_LoanContactId.EmailAddress,
                FirstName = borrowerInfo.LoanContact_LoanContactId.FirstName,
                MiddleName = borrowerInfo.LoanContact_LoanContactId.MiddleName,
                LastName = borrowerInfo.LoanContact_LoanContactId.LastName,
                CreatedOnUtc = DateTime.UtcNow,
                IsAccepted = model.IsAccepted,
                IpAddress = ipAddress,
                TenantId=tenant.Id
            };

            Uow.Repository<BorrowerConsent>().Insert(borrowerConsent);
            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
                .SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(application);
            var borrowerConsentId = await Uow.SaveChangesAsync();
            return borrowerConsent.Id;
        }

        public async Task<int> AddOrUpdateMultipleConsents(TenantModel tenant, BorrowerMultipleConsentsModel model,
            string ipAddress, int userId)
        {
            try
            {
                var acceptedConsents =
                await GetBorrowerAcceptedConsents(tenant, userId, model.BorrowerId, model.LoanApplicationId);
                if (acceptedConsents.IsAccepted) return -1;

                var borrowerInfo = await Uow.Repository<Borrower>()
                    .Query(b => b.Id == model.BorrowerId && b.LoanApplicationId == model.LoanApplicationId &&
                                b.LoanApplication.UserId == userId)
                    .Include(b => b.LoanContact_LoanContactId)
                    .SingleOrDefaultAsync();

                foreach (var consent in model.BorrowerConsents)
                {
                    var borrowerConsent = new BorrowerConsent
                    {
                        ConsentTypeId = consent.ConsentTypeId,
                        BorrowerId = model.BorrowerId,
                        LoanApplicationId = model.LoanApplicationId,
                        ConsentText = consent.Description,
                        Email = borrowerInfo.LoanContact_LoanContactId.EmailAddress,
                        FirstName = borrowerInfo.LoanContact_LoanContactId.FirstName,
                        MiddleName = borrowerInfo.LoanContact_LoanContactId.MiddleName,
                        LastName = borrowerInfo.LoanContact_LoanContactId.LastName,
                        CreatedOnUtc = DateTime.UtcNow,
                        IsAccepted = model.IsAccepted,
                        IpAddress = ipAddress,
                        TenantId = tenant.Id
                    };
                    borrowerConsent.TrackingState = TrackingState.Added;

                    Uow.Repository<BorrowerConsent>().Insert(borrowerConsent);
                }


                var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                    .Query(x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
                    .SingleAsync();
                application.State = model.State;
                Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(application);
                var borrowerConsentId = await Uow.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }
            return model.BorrowerId;
        }


        public ConsentTypeGetModel GetAllConsentType(TenantModel tenant)
        {
            var modelToReturn = new ConsentTypeGetModel();
            //var results = await base.Uow.DataContext.UdfConsentType(tenant.Id).OrderBy(x => x.DisplayOrder)
            //var results = await _dbFunctionService.UdfConsentType(tenant.Id)
            //    .Select(statusList => new ConsentTypeModel() { Id = statusList.Id, Name = statusList.Name, Description = statusList.Description }).ToListAsync();

            //var tenantFilteredConsents = _dbFunctionService.UdfConsentType(tenant.Id);
            var results = _dbFunctionService.UdfConsentType(tenantId: tenant.Id)
                                            .Select(selector: consentType => new ConsentTypeModel
                                                                             {
                                                                                 Id = consentType.Id,
                                                                                 Name = consentType.Name,
                                                                                 Description = consentType.Description
                                            }).ToList();

            if (results != null)
            {
                //modelToReturn.ConsentHash = Guid.NewGuid().ToString();
                modelToReturn.ConsentHash =
                    ComputeConsentHash(consentList: results.Select(selector: consent => consent.Description).ToList());
                modelToReturn.ConsentList = results;
            }

            return modelToReturn;
        }

        public string ComputeConsentHash(List<string> consentList)
        {
            var rawData = string.Join(",", consentList);
            using (var sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                var builder = new StringBuilder();
                for (var i = 0; i < bytes.Length; i++) builder.Append(bytes[i].ToString("x2"));
                return builder.ToString();
            }
        }

        public async Task<BorrowerAcceptedConsentsModel> GetBorrowerAcceptedConsents(TenantModel tenant, int userId,
            int borrowerId, int loanApplicationId)
        {
            var modelToReturn = new BorrowerAcceptedConsentsModel();
            // Get accepted consents
            var acceptedConsents = await Uow.Repository<BorrowerConsent>()
                .Query(bc => bc.BorrowerId == borrowerId
                             && bc.LoanApplicationId == loanApplicationId
                             && bc.LoanApplication.UserId == userId)
                .ToListAsync();
            //if (acceptedConsents == null || acceptedConsents.Count == 0)
            if (acceptedConsents.Count == 0)
            {
                modelToReturn.IsAccepted = false;
                return modelToReturn;
            }

            modelToReturn.IsAccepted = true;
            // Get ids of accepted consents
            var acceptedConsentIds = acceptedConsents.Select(consent => consent.ConsentTypeId)
                .ToList();


            var tenantConsentModel = GetAllConsentType(tenant);

            tenantConsentModel.ConsentList = tenantConsentModel.ConsentList
                .Where(consent => acceptedConsentIds.Contains(consent.Id))
                .ToList();
            // Ensure we are sending back what was accepted by borrower as there is a chance of change in text when borrower revisit.
            foreach (var acceptedConsent in acceptedConsents)
            {
                var tenantConsentItem = tenantConsentModel.ConsentList
                    .FirstOrDefault(tenantConsent => tenantConsent.Id == acceptedConsent.ConsentTypeId);
                if (tenantConsentItem != null) tenantConsentItem.Description = acceptedConsent.ConsentText;
            }

            modelToReturn.AcceptedConsentList = tenantConsentModel.ConsentList;

            return modelToReturn;
        }

        public async Task<ConsentTypeGetModel> GetBorrowerConsent(TenantModel tenant, int userId, int borrowerId)
        {
            var modelToReturn = new ConsentTypeGetModel();

            var borrower = await Uow.Repository<Borrower>()
                .Query(b => b.Id == borrowerId && b.LoanApplication.UserId == userId)
                .Include(b => b.LoanContact_LoanContactId)
                .SingleAsync();

            var consentTypes = _dbFunctionService.UdfConsentType(tenant.Id)
                .Where(consentType => consentType.OwnTypeId == borrower.OwnTypeId).ToList();

            var results = consentTypes.Select(statusList => new ConsentTypeModel
            {
                Id = statusList.Id,
                Name = statusList.Name,
                Description = statusList.Description
                    .Replace("{borrowerFirstName}",borrower.LoanContact_LoanContactId.FirstName)
                    .Replace("{borrowerLastName}", borrower.LoanContact_LoanContactId.LastName)
            }).ToList();
            

            if (results != null)
            {
                modelToReturn.ConsentHash =
                    ComputeConsentHash(results.Select(consent => consent.Description).ToList());
                modelToReturn.ConsentList = results;
            }

            return modelToReturn;
        }
    }
}