using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Extensions.ExtensionClasses;
using Newtonsoft.Json;

namespace ServiceCallHelper
{
    public static class HttpClientExtensions
    {
        public static CallResponse<T> Get<T>(this HttpClient httpClient,
                                             string endPoint,
                                             HttpRequest request,
                                             bool attachBearerTokenFromCurrentRequest)
        {
            return Call.Get<T>(httpClient: httpClient,
                               endPoint: endPoint,
                               request: request,
                               attachBearerTokenFromCurrentRequest: attachBearerTokenFromCurrentRequest);
        }


        public static CallResponse<T> Get<T>(this HttpClient httpClient,
                                             string endPoint,
                                             HttpRequest bearerToken)
        {
            return Call.Get<T>(httpClient: httpClient,
                               endPoint: endPoint,
                               request: bearerToken);
        }


        public static CallResponse<T> Post<T>(this HttpClient httpClient,
                                              string endPoint,
                                              string content,
                                              HttpRequest request,
                                              bool attachBearerTokenFromCurrentRequest = false)
        {
            return Call.Post<T>(httpClient: httpClient,
                                endPoint: endPoint,
                                content: content,
                                request: request,
                                attachBearerTokenFromCurrentRequest: attachBearerTokenFromCurrentRequest );
        }


        public static CallResponse<T> Post<T>(this HttpClient httpClient,
                                              string endPoint,
                                              string content,
                                              string bearerToken = null)
        {
            return Call.Post<T>(httpClient: httpClient,
                                endPoint: endPoint,
                                content: content,
                                bearerToken: bearerToken );
        }



        #region CommonMethods

        public static async Task<CallResponse<TOutput>> GetResponseAsync<TInput, TOutput>(this HttpClient httpClient,
                                                                                          string requestUri,
                                                                                          HttpMethod method,
                                                                                          TInput content,
                                                                                          bool deserializeResponse,
                                                                                          Version version = null,
                                                                                          IEnumerable<KeyValuePair<string, string>> headers = null,
                                                                                          IEnumerable<KeyValuePair<string, object>> properties = null)
        {
            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(uriString: requestUri),
            };

            if (version.HasValue()) request.Version = version;

            if (content.HasValue())
                request.Content = new StringContent(content: content.ToJson(),
                                                    encoding: Encoding.UTF8,
                                                    mediaType: "application/json");

            request.Headers.Accept.Add(item: new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));

            if (headers.HasValue())
                foreach (var header in headers)
                    request.Headers.Add(name: header.Key,
                                        value: header.Value);

            if (properties.HasValue())
                foreach (var keyValuePair in properties)
                    request.Properties.Add(item: keyValuePair);

            var httpResponseMessage = await httpClient.SendAsync(request: request);

            TOutput deserializeObject = default;

            string result = null;

            if (deserializeResponse && httpResponseMessage.IsSuccessStatusCode)
            {
                result = await httpResponseMessage.Content.ReadAsStringAsync();
                deserializeObject = JsonConvert.DeserializeObject<TOutput>(value: result);
            }

            return new CallResponse<TOutput>(httpResponseMessage: httpResponseMessage,
                                             responseObject: deserializeObject,
                                             rawResult: result);
        }

        #endregion

        #region GetHelpers

        public static Task<CallResponse<TOutput>> EasyGetAsync<TOutput>(this HttpClient httpClient,
                                                                        string requestUri,
                                                                        bool attachAuthorizationHeadersFromCurrentRequest = false,
                                                                        bool deserializeResponse = true)
        {
            IEnumerable<KeyValuePair<string, string>> authorizationHeaders = null;

            if (attachAuthorizationHeadersFromCurrentRequest)
            {
                var request = AppContext.Current.Request;
                authorizationHeaders = request.Headers[key: "Authorization"].Select(selector: headerValue => new KeyValuePair<string, string>(key: "Authorization",
                                                                                                                                              value: headerValue));
            }

            return EasyGet<TOutput>(httpClient: httpClient,
                                    requestUri: requestUri,
                                    deserializeResponse: deserializeResponse,
                                    headers: authorizationHeaders);
        }


        public static TOutput EasyGet<TOutput>(this HttpClient httpClient,
                                               out CallResponse<TOutput> callResponse,
                                               string requestUri,
                                               bool attachAuthorizationHeadersFromCurrentRequest = false,
                                               bool deserializeResponse = true)
        {
            callResponse = EasyGetAsync<TOutput>(httpClient: httpClient,
                                                 requestUri: requestUri,
                                                 deserializeResponse: deserializeResponse,
                                                 attachAuthorizationHeadersFromCurrentRequest: attachAuthorizationHeadersFromCurrentRequest).Result;

            return callResponse.ResponseObject;
        }


        public static async Task<CallResponse<TOutput>> EasyGet<TOutput>(this HttpClient httpClient,
                                                                         string requestUri,
                                                                         IEnumerable<KeyValuePair<string, string>> headers = null,
                                                                         bool deserializeResponse = true)
        {
            return await GetResponseAsync<object, TOutput>(httpClient: httpClient,
                                                           requestUri: requestUri,
                                                           method: HttpMethod.Get,
                                                           content: null,
                                                           deserializeResponse: deserializeResponse,
                                                           version: null,
                                                           headers: headers,
                                                           properties: null);
        }

        #endregion

        #region PostHelpers

        public static async Task<CallResponse<TOutput>> EasyPostAsync<TOutput>(this HttpClient httpClient,
                                                                               string requestUri,
                                                                               object content,
                                                                               bool attachAuthorizationHeadersFromCurrentRequest = false,
                                                                               bool deserializeResponse = true)
        {
            IEnumerable<KeyValuePair<string, string>> authorizationHeaders = null;

            if (attachAuthorizationHeadersFromCurrentRequest)
            {
                var request = AppContext.Current.Request;
                authorizationHeaders = request.Headers[key: "Authorization"].Select(selector: headerValue => new KeyValuePair<string, string>(key: "Authorization",
                                                                                                                                              value: headerValue));
            }

            return await EasyPost<TOutput>(httpClient: httpClient,
                                           requestUri: requestUri,
                                           content: content,
                                           deserializeResponse: deserializeResponse,
                                           headers: authorizationHeaders);
        }


        public static TOutput EasyPost<TOutput>(this HttpClient httpClient,
                                                out CallResponse<TOutput> callResponse,
                                                string requestUri,
                                                object content,
                                                bool attachAuthorizationHeadersFromCurrentRequest = false,
                                                bool deserializeResponse = true)
        {
            callResponse = EasyPostAsync<TOutput>(httpClient: httpClient,
                                                  requestUri: requestUri,
                                                  content: content,
                                                  deserializeResponse: deserializeResponse,
                                                  attachAuthorizationHeadersFromCurrentRequest: attachAuthorizationHeadersFromCurrentRequest).Result;

            return callResponse.ResponseObject;
        }


        public static async Task<CallResponse<TOutput>> EasyPost<TOutput>(this HttpClient httpClient,
                                                                          string requestUri,
                                                                          object content,
                                                                          IEnumerable<KeyValuePair<string, string>> headers = null,
                                                                          bool deserializeResponse = true)
        {
            return await GetResponseAsync<object, TOutput>(httpClient: httpClient,
                                                           requestUri: requestUri,
                                                           method: HttpMethod.Post,
                                                           content: content,
                                                           deserializeResponse: deserializeResponse,
                                                           version: null,
                                                           headers: headers,
                                                           properties: null);
        }

        #endregion

    }
} 
