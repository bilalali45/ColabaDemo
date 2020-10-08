using Microsoft.AspNetCore.Http;
using System.Net.Http;

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
    }
} 
