using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;

namespace TenantConfig.Common
{
    public class ValidateRecaptchaAttribute : ActionFilterAttribute
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ValidateRecaptchaAttribute> _logger;
        private readonly HttpClient _httpClient;
        public ValidateRecaptchaAttribute(IConfiguration configuration, ILogger<ValidateRecaptchaAttribute> logger, HttpClient httpClient)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClient = httpClient;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Headers.ContainsKey(Constants.RECAPTCHA_CODE))
            {
                var code = context.HttpContext.Request.Headers[Constants.RECAPTCHA_CODE][0];
                if (await Verify(code))
                {
                    await base.OnActionExecutionAsync(context, next);
                    return;
                }
            }
            context.Result = new BadRequestObjectResult(new { Message="Unable to verify recaptcha"});
        }

        private async Task<bool> Verify(string code)
        {
            if (_configuration.GetChildren().Any(x => x.Key == "DevToken"))
            {
                var devToken = _configuration["DevToken"];
                if (!string.IsNullOrEmpty(devToken) && devToken == code)
                {
                    return true;
                }
            }
            _logger.LogInformation("checking recaptcha");
            var csResponse = await _httpClient.GetAsync($"{_configuration["KeyStore:Url"]}/api/keystore/keystore?key=RecaptchaKey");
            if(!csResponse.IsSuccessStatusCode)
            {
                _logger.LogError("Unable to find recaptcha key");
                return false;
            }
            var key = await csResponse.Content.ReadAsStringAsync();
            var content = new FormUrlEncodedContent(new[]
            {
                 new KeyValuePair<string, string>("secret", key),
                 new KeyValuePair<string, string>("response", code)
            });
            var res = await _httpClient.PostAsync($"https://www.google.com/recaptcha/api/siteverify",content);
            if(!res.IsSuccessStatusCode)
            {
                _logger.LogError("Google recaptcha api failed");
                return false;
            }
            string JSONres = await res.Content.ReadAsStringAsync();
            _logger.LogInformation(JSONres);

            dynamic JSONdata = JObject.Parse(JSONres);

            if (JSONdata.success != "true")
                return false;

            //if (JSONdata.score < 0.5)
            //    return false;

            if (JSONdata.action != "submit")
                return false;

            return true;
        }
    }
}
