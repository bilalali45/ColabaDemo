using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Identity.Model.TwoFA;
using Identity.Service;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Twilio.Clients;
using Twilio.Http;
using Twilio.Rest.Verify.V2;
using Twilio.Rest.Verify.V2.Service;

namespace Identity.Services
{
    public interface ITwilioHandler
    {
        Task<ITwoFaResponseModel> Create2FaRequestAsync(string to);
        Task<ITwoFaResponseModel> Verify2FaRequestAsync(string code, string requestId);
        Task<TwoFaBase> Create2FaServiceForTenant(string userFriendlyName);
        void SetServiceSid(string sid);
    }

    public class TwilioHandler :  ITwilioHandler
    {
        private const string TWILIO_VERIFY_URL = "https://verify.twilio.com/v2";
        private const string DEFAULT_CHANNEL = "sms";

        private readonly IRestClient _client;
        private readonly ILogger<TwilioHandler> _logger;
        private string Sid { get; set; }

        public TwilioHandler(IRestClient client, IKeyStoreService keyStoreService, ILogger<TwilioHandler> logger)
        {
            //this._client = new RestClient(TWILIO_VERIFY_URL);
            this._client = client;
            this._client.BaseUrl = new Uri(TWILIO_VERIFY_URL);
            var twilioAccountSid = keyStoreService.GetTwilioAccountSidAsync().Result;
            var twilioAuthToken = keyStoreService.GetTwilioAuthTokenAsync().Result;
            this._client.Authenticator = new HttpBasicAuthenticator(twilioAccountSid, twilioAuthToken);
        }

        public void SetServiceSid(string sid)
        {
            this.Sid = sid;
        }

        public async Task<ITwoFaResponseModel> Create2FaRequestAsync(string to)
        {
            try
            {
                var request = new RestRequest($"/Services/{this.Sid}/Verifications", Method.POST);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("To", to);
                request.AddParameter("Channel", DEFAULT_CHANNEL);
                IRestResponse response = await _client.ExecuteAsync(request);
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    TwilioTwoFaResponseModel verifyResponse = JsonConvert.DeserializeObject<TwilioTwoFaResponseModel>(response.Content);
                    return verifyResponse;
                }
                else
                {
                    throw new Exception("Could not create verification request on twilio.");
                }
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<ITwoFaResponseModel> Verify2FaRequestAsync(string code, string requestId)
        {
            TwilioTwoFaResponseModel verifyResponse = new TwilioTwoFaResponseModel()
            {
                StatusCode = (int) HttpStatusCode.BadRequest
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
                }
                verifyResponse.StatusCode = (int)response.StatusCode;
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message);
                throw;
            }

            return verifyResponse;
        }

        public async Task<TwoFaBase> Create2FaServiceForTenant(string friendlyName)
        {
            try
            {
                var request = new RestRequest($"/Services/{this.Sid}/Verifications", Method.POST);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("FriendlyName", friendlyName);
                IRestResponse response = await _client.ExecuteAsync(request);
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    TwilioTwoFaResponseModel verifyResponse = JsonConvert.DeserializeObject<TwilioTwoFaResponseModel>(response.Content);
                    return verifyResponse;
                }
                else
                {
                    throw new Exception("Could not create verification service on twilio.");
                }
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message);
                throw;
            }
        }
    }
}
