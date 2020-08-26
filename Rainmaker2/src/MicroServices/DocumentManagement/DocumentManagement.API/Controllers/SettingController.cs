﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.API.Controllers
{
    [Route("api/documentmanagement/[controller]")]
    [ApiController]
    [Authorize(Roles ="MCU,Customer")]
    public class SettingController : ControllerBase
    {
        private readonly IByteProService byteProService;
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTenantSetting()
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var setting = await byteProService.GetTenantSetting(tenantId);
            return Ok(setting);
        }
    }
}
