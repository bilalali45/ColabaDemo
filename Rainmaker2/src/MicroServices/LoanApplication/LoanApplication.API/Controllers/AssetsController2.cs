using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanApplication.Model;
using LoanApplication.Service;
using Microsoft.AspNetCore.Authorization;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.API.Controllers
{
    public partial class AssetsController
    {
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAssetCategories()
        {
            return Ok(_assetsService.GetAllAssetCategories(base.Tenant));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public IActionResult GetAssetTypesByCategory(int categoryId)
        {
            if(categoryId <= 0)
            {
                return BadRequest("Invalid asset category id");
            }
            return Ok(_assetsService.GetAssetTypesByCategory(base.Tenant, categoryId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateBorrowerAsset(AddOrUpdateBorrowerAssetModel model)
        {
            var results = await _assetsService.AddOrUpdateBorrowerAsset(base.Tenant, base.UserId, model);
            if (results < 0)
            {
                return BadRequest(_assetsService.ErrorMessages[results]);
            }

            return Ok(results);
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLoanApplicationBorrowersAssets([FromQuery] LoanApplicationIdModel model)
        {
            var results = await _assetsService.GetLoanApplicationBorrowersAssets(base.Tenant, base.UserId, model.LoanApplicationId);
            if (!string.IsNullOrEmpty(results.ErrorMessage))
            {
                return BadRequest(results.ErrorMessage);
            }

            return Ok(results);
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBorrowerAssets([FromQuery] BorrowerAssetDetailGetModel model)
        {
            var results = await _assetsService.GetBorrowerAssets(base.Tenant, base.UserId, model.LoanApplicationId, model.BorrowerId);
            if (!string.IsNullOrEmpty(results.ErrorMessage))
            {
                return BadRequest(results.ErrorMessage);
            }

            return Ok(results);
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBorrowerAssetDetail([FromQuery] BorrowerAssetDetailGetModel model)
        {
            var results = await _assetsService.GetBorrowerAssetDetail(base.Tenant, base.UserId, model);
            if (!string.IsNullOrEmpty(results.ErrorMessage))
            {
                return BadRequest(results.ErrorMessage);
            }

            return Ok(results);
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGiftSourceAssets(int giftSourceId)
        {
            if (giftSourceId <= 0)
            {
                return BadRequest("Invalid gift source id");
            }
            var results = await _assetsService.GetGiftSourceAssets(base.Tenant, giftSourceId);
            

            return Ok(results);
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public IActionResult GetCollateralAssetTypes()
        {
            var results = _assetsService.GetCollateralAssetTypes(base.Tenant);
            return Ok(results);
        }
    }
}
