using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanApplication.Model;
using LoanApplication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.API.Controllers
{
    [Route("api/loanapplication/[controller]")]
    public class QuestionController :  BaseController
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSection2PrimaryQuestions(int loanApplicationId, int borrowerId)
        {
            return Ok(await _questionService.GetSection2PrimaryQuestions(Tenant, UserId, loanApplicationId, borrowerId));
        }
        
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSection2SecondaryQuestion(int loanApplicationId, int borrowerId)
        {
            return Ok(await _questionService.GetSection2SecondaryQuestion(Tenant, UserId, loanApplicationId, borrowerId));
        }
      
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSectionOneForPrimaryBorrower(int loanApplicationId, int borrowerId)
        {
            return Ok(await _questionService.GetSectionOneForPrimaryBorrower(Tenant, UserId, borrowerId, loanApplicationId));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSectionOneForSecondaryBorrower(int loanApplicationId, int borrowerId)
        {
            return Ok(await _questionService.GetSectionOneForSecondaryBorrower(Tenant, UserId, borrowerId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateSection2(QuestionRequestModel model)
        {
            return Ok(await _questionService.AddOrUpdateSection2(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateSectionOne(QuestionRequestModel model)
        {
            return Ok(await _questionService.AddOrUpdateSectionOne(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSection3ForPrimaryBorrower(int loanApplicationId, int borrowerId)
        {
            return Ok(await _questionService.GetSection3ForPrimaryBorrower(Tenant, UserId, borrowerId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public IActionResult GetPropertyUsageDropDown()
        {
            return Ok(_questionService.GetAllPropertyUsageDropDown(Tenant));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSection3ForSecondaryBorrower(int loanApplicationId, int borrowerId)
        {
            return Ok(await _questionService.GetSection3ForSecondaryBorrower(Tenant, UserId, borrowerId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public IActionResult GetAllTitleHeldWithDropDown()
        {
            return Ok(_questionService.GetAllTitleHeldWithDropDown(Tenant));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public IActionResult GetAllBankruptcy()
        {
            return Ok(_questionService.GetAllBankruptcy(Tenant));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public IActionResult GetAllLiablilityType()
        {
            return Ok(_questionService.GetAllLiablilityType(Tenant));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateSection3(QuestionRequestModel model)
        {
            return Ok(await _questionService.AddOrUpdateSection3(Tenant, UserId, model));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public IActionResult GetAllRaceList()
        {
            return Ok(_questionService.GetAllRaceList(Tenant));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public IActionResult GetGenderList()
        {
            return Ok(_questionService.GetGenderList(Tenant));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public IActionResult GetAllEthnicityList()
        {
            return Ok(_questionService.GetAllEthnicityList(Tenant));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDemographicInformation(int loanApplicationId, int borrowerId)
        {
            return Ok(await _questionService.GetDemographicInformation(Tenant, UserId, borrowerId, loanApplicationId));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateDemogrhphicInfo(DemographicInfoResponseModel model)
        {
            return Ok(await _questionService.AddOrUpdateDemogrhphicInfo(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDemographicInformationReview(int loanApplicationId)
        {
            return Ok(await _questionService.GetDemographicInformationReview(Tenant, UserId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGovernmentQuestionReview(int loanApplicationId)
        {
            return Ok(await _questionService.GetGovernmentQuestionReview(Tenant, UserId, loanApplicationId));
        }

       
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> CheckPrimaryBorrowerSubjectProperty(int loanApplicationId, int borrowerId)
        {
            return Ok(await _questionService.CheckPrimaryBorrowerSubjectProperty(Tenant, UserId, borrowerId, loanApplicationId));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> CheckSecondaryBorrowerSubjectProperty(int loanApplicationId, int borrowerId)
        {
            return Ok(await _questionService.CheckSecondaryBorrowerSubjectProperty(Tenant, UserId, borrowerId, loanApplicationId));
        }
    }
}
