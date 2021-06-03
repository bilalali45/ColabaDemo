using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Identity.Entity.Models;
using Identity.Model.TwoFA;
using Identity.Service;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using TenantConfig.Common.DistributedCache;
using Twilio;
using Twilio.Clients;
using Twilio.Rest.Verify.V2;
using Twilio.Rest.Verify.V2.Service;
using Twilio.Rest.Voice.V1;
using Identity.Model;
using Identity.Models.TwoFA;

namespace Identity.Service
{
    public class TwilioTwoFactorAuthService : ITwoFactorAuth
    {
        private const string TWILIO_VERIFY_URL = "https://verify.twilio.com/v2";
        private const string DEFAULT_CHANNEL = "sms";

        private readonly IRestClient _client;
        private readonly ILogger<TwilioTwoFactorAuthService> _logger;
        private readonly IOtpTracingService _otpTracingService;
        private readonly ITwoFaHelper _twoFaHelper;
        private readonly IActionContextAccessor _accessor;
        private readonly ITokenService _tokenService;
        private readonly ITenantConfigService _tenantConfigService;

        private string Sid { get; set; }

        public TwilioTwoFactorAuthService(IRestClient client, IKeyStoreService keyStoreService, ILogger<TwilioTwoFactorAuthService> logger, IOtpTracingService otpTracingService, IActionContextAccessor accessor, ITokenService tokenService, ITenantConfigService tenantConfigService, ITwoFaHelper towFaHelper)
        {
            this._client = client;
            this._logger = logger;
            this._otpTracingService = otpTracingService;
            this._accessor = accessor;
            this._tokenService = tokenService;
            this._tenantConfigService = tenantConfigService;
            _twoFaHelper = towFaHelper;
            this._client.BaseUrl = new Uri(TWILIO_VERIFY_URL);
            var twilioAccountSid = keyStoreService.GetTwilioAccountSidAsync().Result;
            var twilioAuthToken = keyStoreService.GetTwilioAuthTokenAsync().Result;
            this._client.Authenticator = new HttpBasicAuthenticator(twilioAccountSid, twilioAuthToken);
        }

        public void SetServiceSid(string sid)
        {
            this.Sid = sid;
        }

        public async Task<TwilioTwoFaResponseModel> Create2FaRequestAsync(string to, string existingVerificationId, int sendDigits = 6)
        {
            try
            {
                var checkCanSend = await this._twoFaHelper.CanSendOtpAsync(to, null);
                if (!checkCanSend.CanSend2Fa)
                {
                    return new TwilioTwoFaResponseModel()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        CanSend2Fa = false,
                        //Next2FaInSeconds = checkCanSend.Next2FaInSeconds,
                        Next2FaInSeconds = this._twoFaHelper.Resend2FaIntervalSeconds,
                        TwoFaRecycleMinutes = this._twoFaHelper.TwoFaRecycleMinutes,
                        OtpValidTill =  checkCanSend.OtpValidity.Value,
                        ErrorMessage = checkCanSend.ErrorMessage
                    };
                }
                var request = new RestRequest($"/Services/{this.Sid}/Verifications", Method.POST);
                request.AlwaysMultipartFormData = true;
                string numberToSend = to;
                if (!to.Trim().StartsWith("+1"))
                {
                    numberToSend = string.Concat("+1", to.Trim());
                }
                request.AddParameter("To", numberToSend);
                request.AddParameter("Channel", DEFAULT_CHANNEL);
                IRestResponse response = await _client.ExecuteAsync(request);
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    TwilioTwoFaResponseModel verifyResponse = JsonConvert.DeserializeObject<TwilioTwoFaResponseModel>(response.Content);
                    request = new RestRequest($"/Services/{this.Sid}/Verifications/{verifyResponse.Sid}", Method.GET);
                    response = await _client.ExecuteAsync(request);
                    verifyResponse = JsonConvert.DeserializeObject<TwilioTwoFaResponseModel>(response.Content);
                    verifyResponse.OtpValidTill = verifyResponse.DateCreated.AddMinutes(this._twoFaHelper.Read2FaConfig<int>("OtpValidity", 10)); // TODO: Sohail
                    verifyResponse.CanSend2Fa = true;
                    //verifyResponse.TwoFaRecycleMinutes = this._twoFaHelper.Read2FaConfig<int>("TwoFaRecycleMinutes", 10);
                    verifyResponse.TwoFaRecycleMinutes = this._twoFaHelper.TwoFaRecycleMinutes;
                    verifyResponse.TwoFaRecycleSeconds = Convert.ToInt32((verifyResponse.OtpValidTill - DateTime.UtcNow).TotalSeconds);
                    var model = await this._twoFaHelper.CanSendOtpAsync(to, existingVerificationId);
                    verifyResponse.Next2FaInSeconds = this._twoFaHelper.Resend2FaIntervalSeconds;// Math.Round((verifyResponse.DateCreated.AddSeconds(this._twoFaHelper.Resend2FaIntervalSeconds) - DateTime.UtcNow).TotalSeconds);
                    return verifyResponse;
                }

                if (response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    if (string.IsNullOrEmpty(existingVerificationId))
                    {
                        var lastLog = await this._otpTracingService.GetLastSendAttemptAsync(to, null);
                        existingVerificationId = lastLog.OtpRequestId;
                    }

                    if (!string.IsNullOrEmpty(existingVerificationId))
                    {
                        var request1 = new RestRequest($"/Services/{this.Sid}/Verifications/{existingVerificationId}", Method.GET);
                        response = await _client.ExecuteAsync(request1);
                        var twoFaResponse = JsonConvert.DeserializeObject<TwilioTwoFaResponseModel>(response.Content);
                        //if (twoResponse.StatusCode != (int) HttpStatusCode.NotFound)
                        //{
                            
                        //}
                        if ((twoFaResponse.Status == "404" || twoFaResponse.StatusCode == (int)HttpStatusCode.NotFound) || (twoFaResponse.StatusCode == (int)HttpStatusCode.TooManyRequests)) 
                        {
                            var temp = await this._otpTracingService.GetLastSendAttemptAsync(to, null);
                            if (temp != null)
                            {
                                twoFaResponse.DateCreated = temp.DateUtc.Value;
                            }
                        }
                        twoFaResponse.StatusCode = (int)HttpStatusCode.TooManyRequests;
                        twoFaResponse.OtpValidTill =
                            twoFaResponse.DateCreated.AddMinutes(
                                this._twoFaHelper.Read2FaConfig<int>("OtpValidity", 10));
                        twoFaResponse.TwoFaRecycleMinutes = this._twoFaHelper.TwoFaRecycleMinutes;
                        //twoFaResponse.TwoFaRecycleSeconds = Convert.ToInt32((twoFaResponse.OtpValidTill - DateTime.UtcNow).TotalSeconds);
                        twoFaResponse.TwoFaRecycleSeconds = this._twoFaHelper.Resend2FaIntervalSeconds;

                        return twoFaResponse;
                    }

                    return new TwilioTwoFaResponseModel()
                    {
                        StatusCode = (int) HttpStatusCode.TooManyRequests
                    };
                }

                throw new Exception("Could not create verification request via twilio.");
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message);
                throw e;
            }
        }

        public async Task<TwilioTwoFaResponseModel> Verify2FaRequestAsync(string code, string requestId)
        {
            TwilioTwoFaResponseModel verifyResponse = new TwilioTwoFaResponseModel()
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
            try
            {
                var request = new RestRequest($"/Services/{this.Sid}/VerificationCheck", Method.POST);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("Code", code);
                request.AddParameter("VerificationSid", requestId);
                IRestResponse response = await _client.ExecuteAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    verifyResponse = JsonConvert.DeserializeObject<TwilioTwoFaResponseModel>(response.Content);
                    verifyResponse.OtpValidTill = verifyResponse.DateCreated.AddMinutes(this._twoFaHelper.Read2FaConfig<int>("OtpValidity", 10)); // TODO: Sohail
                    verifyResponse.TwoFaRecycleMinutes = this._twoFaHelper.TwoFaRecycleMinutes;
                    verifyResponse.Next2FaInSeconds = this._twoFaHelper.Resend2FaIntervalSeconds;
                    if (verifyResponse.Status == "pending")
                    {
                        var viewRequest = new RestRequest($"/Services/{this.Sid}/Verifications/{requestId}", Method.GET);
                        var viewResponse = await _client.ExecuteAsync(viewRequest);
                        var viewResponseModel = JsonConvert.DeserializeObject<TwilioTwoFaResponseModel>(viewResponse.Content);
                        verifyResponse.SendCodeAttempts = viewResponseModel.SendCodeAttempts;
                    }
                    verifyResponse.StatusCode = (int)response.StatusCode;
                }
                else
                {
                    var viewRequest = new RestRequest($"/Services/{this.Sid}/Verifications/{requestId}", Method.GET);
                    var viewResponse = await _client.ExecuteAsync(viewRequest);
                    verifyResponse = JsonConvert.DeserializeObject<TwilioTwoFaResponseModel>(viewResponse.Content);
                    verifyResponse.TwoFaRecycleMinutes = this._twoFaHelper.TwoFaRecycleMinutes;
                    verifyResponse.Next2FaInSeconds = this._twoFaHelper.Resend2FaIntervalSeconds;
                    //verifyResponse.Status = response.StatusCode.ToString();
                    verifyResponse.StatusCode = (int)response.StatusCode;
                }
                
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message);
                throw;
            }

            return verifyResponse;
        }
    }
}
