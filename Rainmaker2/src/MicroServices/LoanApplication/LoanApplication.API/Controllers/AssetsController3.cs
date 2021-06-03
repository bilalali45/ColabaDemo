using System.Threading.Tasks;
using Extensions.ExtensionClasses;
using LoanApplication.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.API.Controllers
{
    public partial class AssetsController
    {
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetFromLoanNonRealState([FromQuery] int BorrowerAssetId,
                                                                 int AssetTypeId,
                                                                 int BorrowerId,
                                                                 int LoanApplicationId)
        {
            var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);

            return Ok(value: _assetsService.GetFromLoanNonRealState(tenant: tenant,
                                                                    userId: userId,
                                                                    BorrowerAssetId: BorrowerAssetId,
                                                                    AssetTypeId: AssetTypeId,
                                                                    BorrowerId: BorrowerId,
                                                                    LoanApplicationId: LoanApplicationId));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> AddOrUpdateAssestsNonRealState(AddOrUpdateAssetModelNonRealState model)
        {
            var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);

            var results = await _assetsService.AddOrUpdateAssestsNonRealState(tenant: tenant,
                                                                              userId: userId,
                                                                              model: model);
            if (results < 0) return BadRequest(error: _assetsService.ErrorMessages[key: results]);

            return Ok();
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> AddOrUpdateAssestsRealState(AddOrUpdateAssetModelRealState model)
        {
            var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);

            var results = await _assetsService.AddOrUpdateAssestsRealState(tenant: tenant,
                                                                           userId: userId,
                                                                           model: model);
            if (results < 0) return BadRequest(error: _assetsService.ErrorMessages[key: results]);

            return Ok();
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetFromLoanRealState([FromQuery] int BorrowerAssetId,
                                                              int AssetTypeId,
                                                              int BorrowerId,
                                                              int LoanApplicationId)
        {
            var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);

            return Ok(value: _assetsService.GetFromLoanRealState(tenant: tenant,
                                                                 userId: userId,
                                                                 BorrowerAssetId: BorrowerAssetId,
                                                                 AssetTypeId: AssetTypeId,
                                                                 BorrowerId: BorrowerId,
                                                                 LoanApplicationId: LoanApplicationId));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> AddOrUpdateProceedsfromloan(ProceedFromLoanModel model)
        {
            var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);

            var results = await _assetsService.AddOrUpdateProceedsfromloan(tenant: tenant,
                                                                           userId: userId,
                                                                           model: model);
            if (results < 0) return BadRequest(error: _assetsService.ErrorMessages[key: results]);

            return Ok(value: results);
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> AddOrUpdateProceedsfromloanOther(ProceedFromLoanOtherModel model)
        {
            var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);

            var results = await _assetsService.AddOrUpdateProceedsfromloanOther(tenant: tenant,
                                                                                userId: userId,
                                                                                model: model);
            if (results < 0) return BadRequest(error: _assetsService.ErrorMessages[key: results]);

            return Ok(value: results);
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetProceedsfromloan([FromQuery] int BorrowerAssetId,
                                                             int AssetTypeId,
                                                             int BorrowerId,
                                                             int LoanApplicationId)
        {
            var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);

            return Ok(value: _assetsService.GetProceedsfromloan(tenant: tenant,
                                                                userId: userId,
                                                                BorrowerAssetId: BorrowerAssetId,
                                                                AssetTypeId: AssetTypeId,
                                                                BorrowerId: BorrowerId,
                                                                LoanApplicationId: LoanApplicationId));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetAssetsHomeScreen(int loanApplicationId)
        {
            var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);
            return Ok(value: await _assetsService.GetAllAssetsForHomeScreen(tenant: tenant,
                                                                            userId: userId,
                                                                            loanApplicationId: loanApplicationId));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetOtherAssetsInfo([FromQuery] int otherAssetId) //reviewed
        {
            var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value); //review comment user id was missed
            return Ok(value: await _assetsService.GetOtherAssetInfo(tenant: tenant,
                                                                    userId: userId,
                                                                    assetId: otherAssetId));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> AddOrUpdateOtherAssetsInfo(AddOrUpdateOtherAssetsInfoModel model)
        {
            if (model.AssetTypeId == AssetTypes.TrustAccount ||
                model.AssetTypeId == AssetTypes.BridgeLoanProceeds)
            {
                if (!model.InstitutionName.HasValue())
                    return BadRequest(error: "InstitutionName required.");

                if (!model.AccountNumber.HasValue())
                    return BadRequest(error: "AccountNumber required.");

                if (!model.Value.HasValue())
                    return BadRequest(error: "Value required.");
            }

            if (model.AssetTypeId == AssetTypes.IndividualDevelopmentAccount_Ida ||
                model.AssetTypeId == AssetTypes.CashValueOfLifeInsurance ||
                model.AssetTypeId == AssetTypes.EmployerAssistance ||
                model.AssetTypeId == AssetTypes.RelocationFunds ||
                model.AssetTypeId == AssetTypes.RentCredit ||
                model.AssetTypeId == AssetTypes.LotEquity ||
                model.AssetTypeId == AssetTypes.SweatEquity ||
                model.AssetTypeId == AssetTypes.TradeEquity)
                if (!model.Value.HasValue())
                    return BadRequest(error: "Value required.");

            if (model.AssetTypeId == AssetTypes.Other)
            {
                if (!model.Value.HasValue())
                    return BadRequest(error: "Value required.");

                if (!model.Description.HasValue())
                    return BadRequest(error: "Description required.");
            }

            if (model.Value < 0) return BadRequest(error: "Invalid annual base income");

            var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);
            return Ok(value: await _assetsService.AddOrUpdateOtherAssetInfo(tenant: tenant,
                                                                            userId: userId,
                                                                            model: model));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetBorrowerAssetsForReview([FromQuery] LoanApplicationIdModel model)
        {
            var tenant = (TenantModel)HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);
            var results =
                await this._assetsService.GetBorrowerAssetsForReview(tenant, userId, model.LoanApplicationId);
            if (!string.IsNullOrEmpty(results.ErrorMessage))
            {
                return BadRequest(results.ErrorMessage);
            }

            return Ok(results);
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpDelete(template: "[action]")]
        public async Task<IActionResult> DeleteAsset([FromQuery] AssetDeleteModel model)
        {
            var tenant = (TenantModel)HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);
            var results = await _assetsService.DeleteAsset(tenant, userId, model);
            if (results <= 0)
            {
                var errorMessage = _assetsService.ErrorMessages[results];

                return BadRequest(error: errorMessage);
            }

            return Ok(results);
        }

    }
}