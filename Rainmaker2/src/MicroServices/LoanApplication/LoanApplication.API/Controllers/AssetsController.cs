using System.Threading.Tasks;
using LoanApplication.Model;
using LoanApplication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenantConfig.Common;

namespace LoanApplication.API.Controllers
{
    [Route("api/loanapplication/[controller]")]
    public partial class AssetsController : BaseController
    {
        private readonly IAssetsService _assetsService;

        public AssetsController(IAssetsService assetsService)
        {
            _assetsService = assetsService;
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateEarnestMoneyDeposit(EarnestMoneyDepositModel model)
        {
            return Ok(await _assetsService.UpdateEarnestMoneyDeposit(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEarnestMoneyDeposit(int loanApplicationId)
        {
            return Ok(await _assetsService.GetEarnestMoneyDeposit(Tenant, UserId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteEarnestMoneyDeposit(int loanApplicationId)
        {
            await _assetsService.DeleteEarnestMoneyDeposit(Tenant, UserId, loanApplicationId);
            return Ok();
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllGiftSources()
        {
            return Ok(await _assetsService.GetAllGiftSources(Tenant));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRetirementAccount(int loanApplicationId ,int borrowerId ,int borrowerAssetId)
        {
            return Ok(await _assetsService.GetRetirementAccount(Tenant, UserId, loanApplicationId, borrowerId, borrowerAssetId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateRetirementAccount(RetirementAccountModel model)
        {
            return Ok(await _assetsService.UpdateRetirementAccount(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBankAccountType()
        {
            return Ok(await _assetsService.GetBankAccountType(Tenant));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateBankAccount(BorrowerAssetModelRequest model)
        {
            if(!(model.AssetTypeId== (int)AssetTypes.CheckingAccount || model.AssetTypeId== (int)AssetTypes.SavingsAccoount))
                return BadRequest(error: "Asset Type not allowed");

            return Ok(await _assetsService.AddOrUpdateBankAccount(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBankAccount(int borrowerAssetId,int borrowerId , int loanApplicationId)
        {
            return Ok(await _assetsService.GetBankAccount(Tenant, UserId, borrowerAssetId, borrowerId , loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllFinancialAsset()
        {
            return Ok(await _assetsService.GetAllFinancialAsset(Tenant));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateFinancialAsset(BorrowerAssetFinancialModelRequest model)
        {
            if (!(model.AssetTypeId == (int)AssetTypes.MutualFunds || model.AssetTypeId == (int)AssetTypes.Bonds || model.AssetTypeId == (int)AssetTypes.Stocks
                || model.AssetTypeId == (int)AssetTypes.StockOptions || model.AssetTypeId == (int)AssetTypes.Moneymarket || model.AssetTypeId == (int)AssetTypes.CertificateOfDeposit
                ))
                return BadRequest(error: "Asset Type not allowed");

            return Ok(await _assetsService.AddOrUpdateFinancialAsset(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateGiftAsset(GiftAssetModel model)
        {
            if (!(model.AssetTypeId == (int)AssetTypes.CashGift || model.AssetTypeId == (int)AssetTypes.Grant || model.AssetTypeId == (int)AssetTypes.GiftOfEquity)
                )
                return BadRequest(error: "Asset Type not allowed");

            return Ok(await _assetsService.AddOrUpdateGiftAsset(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGiftAsset(int borrowerAssetId, int borrowerId, int loanApplicationId)
        {
            return Ok(await _assetsService.GetGiftAsset(Tenant, UserId, borrowerAssetId, borrowerId, loanApplicationId));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFinancialAsset(int borrowerAssetId, int borrowerId, int loanApplicationId)
        {
            return Ok(await _assetsService.GetFinancialAsset(Tenant, UserId, borrowerAssetId, borrowerId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAssetsTypes(int giftSourceId)
        {
            return Ok(await _assetsService.GetAssetsTypes(Tenant, giftSourceId));
        }

    }
}