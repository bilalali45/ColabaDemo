using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;

namespace Colaba.Web.Helpers
{
    public class ColabaRewriteRule : IRule
    {
        private static string password = "thisismypassword";
        public void ApplyRule(RewriteContext context)
        {
            string url = context.HttpContext.Request.GetDisplayUrl().ToLower();
            string EncryptUrl(string cookieUrl)
            {
                return Convert.ToBase64String((new AesManaged { Key = Encoding.UTF8.GetBytes(password), Mode = CipherMode.ECB }).CreateEncryptor().TransformFinalBlock(Encoding.UTF8.GetBytes(cookieUrl), 0, Encoding.UTF8.GetBytes(cookieUrl).Length));
            }
            string DecryptUrl(string cookieUrl)
            {
                return Encoding.UTF8.GetString((new AesManaged { Key = Encoding.UTF8.GetBytes(password), Mode = CipherMode.ECB }).CreateDecryptor().TransformFinalBlock(Convert.FromBase64String(cookieUrl), 0, Convert.FromBase64String(cookieUrl).Length));
            }
            void RewriteUrl()
            {
                var uri = new Uri(url);
                string[] urlSegments = uri.Segments;
                int index = Array.FindIndex(urlSegments, a => a.Replace("/", "") == Constants.SPA_PATH);
                context.HttpContext.Request.Path = $"/{string.Join("",urlSegments.Skip(index).ToArray())}";
            }
            void WriteCookie(string newUrl,string path)
            {
                context.HttpContext.Response.Cookies.Append(Constants.COOKIE_NAME,EncryptUrl(newUrl),new CookieOptions() { HttpOnly=true,Path=path,Secure=true,Expires=null,SameSite=SameSiteMode.Strict});
            }
            if(context.HttpContext.Request.Cookies.ContainsKey(Constants.COOKIE_NAME))
            {
                string verifiedUrl = DecryptUrl(context.HttpContext.Request.Cookies[Constants.COOKIE_NAME]).ToLower();
                if(url.StartsWith(verifiedUrl+ Constants.SPA_PATH+"/"))
                {
                    RewriteUrl();
                    context.Result = RuleResult.ContinueRules;
                    return;
                }
            }
            var client = context.HttpContext.RequestServices.GetRequiredService<HttpClient>();
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            using var request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = new Uri($"{configuration["ApiGateway:Url"]}/api/tenantconfig/tenant/GetRedirectUrl");
            request.Headers.Add(Constants.COLABA_WEB_URL_HEADER,url);
            using var response = AsyncHelper.RunSync(()=> client.SendAsync(request));
            if(response.IsSuccessStatusCode)
            {
                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<VerifyModel>(AsyncHelper.RunSync(()=>response.Content.ReadAsStringAsync()));
                if (url.StartsWith(model.Url + Constants.SPA_PATH+"/"))
                {
                    RewriteUrl();
                    WriteCookie(model.Url,model.Path);
                    context.Result = RuleResult.ContinueRules;
                    return;
                }
                else
                {
                    WriteCookie(model.Url, model.Path);
                    context.HttpContext.Response.Redirect(model.Url + Constants.SPA_PATH+"/");
                    context.Result = RuleResult.EndResponse;
                    return;
                }
            }
            context.Result = RuleResult.ContinueRules;
        }
    }
}
