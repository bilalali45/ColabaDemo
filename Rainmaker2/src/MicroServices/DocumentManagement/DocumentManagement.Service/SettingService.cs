using DocumentManagement.Entity;
using DocumentManagement.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{

    public class SettingService : ISettingService
    {
        private readonly IMongoService mongoService;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public SettingService(IMongoService mongoService, HttpClient _httpClient, IConfiguration _configuration)
        {
            this.mongoService = mongoService;
            this._httpClient = _httpClient;
            this._configuration = _configuration;
        }
        public async Task<Setting> GetSetting()
        {
            IMongoCollection<Setting> collection = mongoService.db.GetCollection<Setting>("Setting");
            using IAsyncCursor<Setting> setting = await collection.FindAsync(FilterDefinition<Setting>.Empty);
            await setting.MoveNextAsync();
            return setting.Current.FirstOrDefault();
        }

        public async Task<bool> InsertEmailReminderLog(EmailReminderLogModel emailReminderLogModel, IEnumerable<string> authHeader)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "Setting:Url"] + "/api/Setting/EmailReminder/InsertEmailReminderLog"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: emailReminderLogModel.ToJson(),
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
