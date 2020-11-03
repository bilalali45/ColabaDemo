using Extensions.ExtensionClasses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ServiceCallHelper
{
    public static class Call
    {
        #region GetHelpers

        public static CallResponse<T> Get<T>(HttpClient httpClient,
                                             string endPoint,
                                             HttpRequest request,
                                             bool attachBearerTokenFromCurrentRequest = false)
        {
            string token = null;
            if (attachBearerTokenFromCurrentRequest)
                token = request
                        .Headers[key: "Authorization"].ToString()
                        .Replace(oldValue: "Bearer ",
                                 newValue: "");

            return Get<T>(httpClient: httpClient,
                          endPoint: endPoint,
                          bearerToken: token);
        }


        public static CallResponse<T> Get<T>(HttpClient httpClient,
                                             string endPoint,
                                             string bearerToken = null)
        {

            if (bearerToken.HasValue())
                httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue(scheme: "Bearer",
                                                    parameter: bearerToken);

            var httpResponseMessage = httpClient.GetAsync(requestUri: endPoint).Result;

            T response = default;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<T>(value: result);
            }

            return new CallResponse<T>(httpResponseMessage: httpResponseMessage,
                                       responseObject: response, rawResult: "");
        }

        #endregion

        #region PostHelpers

        public static CallResponse<T> Post<T>(HttpClient httpClient,
                                              string endPoint,
                                              string content,
                                              HttpRequest request,
                                              bool attachBearerTokenFromCurrentRequest = false)
        {
            string token = null;
            if (attachBearerTokenFromCurrentRequest)
                token = request
                        .Headers[key: "Authorization"].ToString()
                        .Replace(oldValue: "Bearer ",
                                 newValue: "");

            return Post<T>(httpClient: httpClient,
                           endPoint: endPoint,
                           content: content,
                           bearerToken: token);
        }


        public static CallResponse<T> Post<T>(HttpClient httpClient,
                                              string endPoint,
                                              string content,
                                              string bearerToken = null)
        {
            if (bearerToken.HasValue())
                httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue(scheme: "Bearer",
                                                    parameter: bearerToken);

            var httpResponseMessage = httpClient.PostAsync(requestUri:
                                                           endPoint,
                                                           content: new
                                                               StringContent(content: content,
                                                                             encoding: Encoding.UTF8,
                                                                             mediaType: "application/json")).Result;

            T response = default;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<T>(value: result);
            }

            return new CallResponse<T>(httpResponseMessage: httpResponseMessage,
                                       responseObject: response, rawResult:"");
        }

        #endregion
    }

}