using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Extensions.ExtensionClasses;
using Microsoft.Extensions.Configuration;
using Setting.Model;

namespace Setting.Service
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
        public async Task<List<SettingModel>> GetSettings(IEnumerable<string> authHeader)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "Notification:Url"] + "/api/Notification/Setting/GetSettings"),
                Method = HttpMethod.Get
            };
            request.Headers.Add("Authorization", authHeader);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<SettingModel>>(await response.Content.ReadAsStringAsync());
            else
                return null;
        }

        public async Task<bool> UpdateSettings(int notificationTypeId, short deliveryModeId, short? delayedInterval, IEnumerable<string> authHeader)
        {
            var content = new
            {
                notificationTypeId,
                deliveryModeId,
                delayedInterval
            };
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "Notification:Url"] + "/api/Notification/Setting/UpdateSettings"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: content.ToJson(),
                                                          encoding: Encoding.UTF8,
                                                          mediaType: "application/json")
            };
            request.Headers.Add("Authorization", authHeader);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
    }
}
