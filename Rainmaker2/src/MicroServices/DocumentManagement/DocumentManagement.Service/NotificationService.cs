using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public class NotificationService : INotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public NotificationService(HttpClient _httpClient, IConfiguration _configuration)
        {
            this._httpClient = _httpClient;
            this._configuration = _configuration;
        }

        public async Task<string> DocumentsSubmitted(int loanApplicationId, IEnumerable<string> authHeader)
        {
            var content = new
            {
                notificationType = 1,
                entityId = loanApplicationId,
                customTextJson = String.Empty
            };

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "Notification:Url"] + "/api/Notification/notification/Add"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: content.ToJson(),
                    encoding: Encoding.UTF8,
                    mediaType: "application/json")
            };
            request.Headers.Add("Authorization", authHeader);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else
                return null;
        }
    }
}
