using LoanApplication.Model;
using LoanApplication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.API.Controllers
{
    [Route("api/loanapplication/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {
        private readonly IInfoService _infoService;
        public AddressController(IInfoService infoService)
        {
            _infoService = infoService;
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetZipCodeByStateCountyCityName(string cityName, string stateName, string countyName, string zipCode)
        {
            if (cityName == null)
                cityName = "";
            if (stateName == null)
                stateName = "";
            if (countyName == null)
                countyName = "";
            if (zipCode == null)
                zipCode = "";
            return Ok(_infoService.GetLocationSearchByZipCodeCityState(cityName.Replace("City", "").Trim(), stateName.Replace("State", "").Trim(), countyName.Replace("County", "").Trim(),zipCode.Trim()));
        }
        
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetZipCodeByStateCountyCity(int stateId, int cityId, int countyId)
        {
            return Ok(await _infoService.GetZipCodesByStateCityAndCounty(stateId,cityId,countyId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSearchByStateCountyCity(string searchKey)
        {
            return Ok(_infoService.GetSearchByString(searchKey));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSearchByZipcode(string searchKey="")
        {
            var zipCodeSearch = 0;
            int.TryParse(searchKey, out zipCodeSearch);

            if (zipCodeSearch <= 0)
            {
                return BadRequest(new ErrorModel { Code=400, Message="Invalid zip code"});
            }
            
            return Ok(_infoService.GetSearchByZipcode(zipCodeSearch));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCountry()
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            return Ok(await _infoService.GetAllCountry(tenant));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllState(int? countryId)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            return Ok(await _infoService.GetAllState(tenant,countryId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllOwnershipType()
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            return Ok(_infoService.GetAllOwnershipType(tenant));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTenantState()
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            return Ok(_infoService.GetTenantState(tenant));
        }
    }
}
