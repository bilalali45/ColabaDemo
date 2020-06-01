using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RainMaker.Common;
using RainMaker.Common.Caching;

namespace RainMaker.Service
{
    public class CommonService : ICommonService
    {

        // key for caching
        public const string LocalStringResourceKey = "RainMaker.Resource-{0}-{1}";
        public const string LocalSettingKey = "RainMaker.Setting-{0}";
        public const string LocalSessionKey = "RainMaker.Session-{0}";
        public const string AdsSourceMessagesResourceKey = "RainMaker.AdsMessageResource-{0}-{1}";
        public const string EmployeePhotoResourceKey = "RainMaker.LoPhoto-{0}";
        protected IServiceProvider services;
        protected object lockObject = new object();
        public CommonService(IServiceProvider services)
        {
            this.services = services;
        }
        
        public async Task<Dictionary<string, KeyValuePair<int, string>>> GetAllResourceValueAsync(int languageId = 1,
            int? businessUnit = null)
        {
            ICacheManager cacheManage = new MemoryCacheManager();
            string key = string.Format(LocalStringResourceKey, languageId, businessUnit ?? 0);
            var resource = cacheManage.Get(key, async () =>
            {
                using var scope = services.CreateScope();
                var stringResourceService = scope.ServiceProvider.GetRequiredService<IStringResourceService>();
                var resourceList = await stringResourceService.GetByLanguageAsync(languageId, businessUnit);
                var resourceDictionary = new Dictionary<string, KeyValuePair<int, string>>();
                foreach (var resourceItem in resourceList)
                {
                    if (!resourceDictionary.ContainsKey(resourceItem.ResourceName.Trim().ToLower()))
                    {
                        resourceDictionary.Add(resourceItem.ResourceName.Trim().ToLower(),
                            new KeyValuePair<int, string>(resourceItem.Id, resourceItem.ResourceValue));
                    }
                }
                return resourceDictionary;
            });
            return await resource;
        }

        public void Copy<T>(T from, T to)
        {
            if (from != null)
            {
                var props = typeof(T).GetProperties();

                if (props.Length == 0)
                {
                    return;
                }

                for (int i = 0; i < props.Length; i++)
                {
                    var prop = props[i];

                    object v = prop.GetValue(from);

                    if (!prop.PropertyType.IsValueType && (prop.PropertyType != typeof(string) || prop.PropertyType != typeof(String)))
                        continue;

                    if ((prop.PropertyType == typeof(string) || prop.PropertyType == typeof(String)) && !String.IsNullOrEmpty((string)v))
                        prop.SetValue(to, v);

                    else if (prop.PropertyType == typeof(int) && (int)v != 0)
                        prop.SetValue(to, v);

                    else if (prop.PropertyType == typeof(DateTime) && (DateTime)v != DateTime.MinValue)
                        prop.SetValue(to, v);

                    else if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) && v != null)
                        prop.SetValue(to, v);


                }
            }

        }

        public async Task<Dictionary<string, KeyValuePair<int, string>>> GetAllSettingValueAsync(int? businessUnit = null)
        {
            ICacheManager cacheManage = new MemoryCacheManager();
            string key = string.Format(LocalSettingKey, businessUnit ?? 0);
            var setting = cacheManage.Get(key, async () =>
            {
                using var scope = services.CreateScope();
                var settingService = scope.ServiceProvider.GetRequiredService<ISettingService>();
                var settingList = await settingService.GetAllSettingAsync(businessUnit);
                var settingDictionary = new Dictionary<string, KeyValuePair<int, string>>();
                foreach (var settingItem in settingList)
                {
                    if (!settingDictionary.ContainsKey(settingItem.Name.Trim().ToLower()))
                    {
                        settingDictionary.Add(settingItem.Name.Trim().ToLower(),
                            new KeyValuePair<int, string>(settingItem.Id, settingItem.Value));
                    }
                }
                return settingDictionary;
            });
            return await setting;
        }

        public async Task<string> GetResourceByNameAsync(string key, params string[] parmas)
        {
            return await GetResourceByNameAsync(key, 1, null, parmas);

        }

        public async Task<string> GetResourceByNameAsync(string key, int languageId = 1, int? businessUnit = null, params string[] parmas)
        {
            var resource = await GetAllResourceValueAsync(languageId, businessUnit);
            if (resource.ContainsKey(key.Trim().ToLower()))
            {
                if (parmas != null && parmas.Length > 0)
                    try
                    {
                        return string.Format(resource[key.Trim().ToLower()].Value, parmas);
                    }
                    catch
                    {
                        return resource[key.Trim().ToLower()].Value;
                    }
                else
                    return resource[key.Trim().ToLower()].Value;

            }
            return key;
        }


        public async Task<T> GetSettingValueByKeyAsync<T>(string settingKey, int? businessUnit = null, T defaultValue = default)
        {
            var settings = await GetAllSettingValueAsync(businessUnit);
            //var value = settingKey;
            if (settings.ContainsKey(settingKey.Trim().ToLower()))
            {
                return CommonHelper.To<T>(settings[settingKey.Trim().ToLower()].Value);
            }
            return defaultValue;
        }

        public async Task<T> GetSettingValueByKeyExcludeExceptionsAsync<T>(string settingKey, int? businessUnit = null,
            T defaultValue = default)
        {
            var settings = await GetAllSettingValueAsync(businessUnit);
            //var value = settingKey;
            if (settings.ContainsKey(settingKey.Trim().ToLower()))
            {
                try
                {
                    return CommonHelper.To<T>(settings[settingKey.Trim().ToLower()].Value);
                }
                catch
                {
                    return defaultValue;
                }
            }
            return defaultValue;
        }
        /*
        public async Task<string> GetEmpNameByUserIdAsync(int? userId)
        {
            string employeeName = "";
            if (userId == null || userId == 0)
                return employeeName;

            IEmployeeService empService = new EmployeeService();
            RainMaker.Entity.Models.Employee emp = empService.GetByUserId(userId.Value);

            if (emp != null)
            {
                IContactService contactservice = new ContactService();
                RainMaker.Entity.Models.Contact cont = contactservice.GetByIdWithDetail(emp.ContactId.Value);
                if (cont != null)
                {
                    if (cont.FirstName != null && cont.FirstName.Length > 0)
                        employeeName = cont.FirstName + " ";

                    if (cont.LastName != null && cont.LastName.Length > 0)
                        employeeName += cont.LastName;
                }
            }

            if (employeeName.Length == 0)
            {
                IUserProfileService uservice = new UserProfileService();
                RainMaker.Entity.Models.UserProfile uProfile = uservice.GetByIdWithDetail(userId.Value);
                if (uProfile != null)
                    employeeName = uProfile.UserName;
            }

            return employeeName;
        }

        public string GetEmployeeIdByUserId(int? userId)
        {
            if (userId == null || userId == 0)
                return "0";

            IEmployeeService employeeService = new EmployeeService();
            RainMaker.Entity.Models.Employee emp = employeeService.GetByUserId(userId.Value);

            if (emp != null)
                return emp.Id.ToString();
            else
                return "0";

        }

        public string GetBusinessUnitName(int businessUnitId)
        {
            IBusinessUnitService businessUnitService = new BusinessUnitService();

            var businessUnit = businessUnitService.GetByIdWithDetail(businessUnitId);

            return (businessUnit == null) ? "" : businessUnit.Name;
        }
        */
        public async Task<T> GetSettingFreshValueByKeyAsync<T>(string settingKey, int? businessUnit = null, T defaultValue = default)
        {
            var settings = await GetAllFreshSettingValueAsync(settingKey, businessUnit);

            if (!settings.ContainsKey(settingKey.Trim().ToLower())) return defaultValue;
            try
            {
                return CommonHelper.To<T>(settings[settingKey.Trim().ToLower()].Value);
            }
            catch
            {
                return defaultValue;
            }
        }
        /*
        public string GetAdSourceResourceByName(int adsSourceId, int messageLocationId, int? businessUnitId = null)
        {
            var messageString = string.Empty;

            var resourceName = GetAdsMessageResourceByName(adsSourceId, messageLocationId, businessUnitId);

            if (!string.IsNullOrWhiteSpace(resourceName))
                messageString = GetResourceByName(resourceName, 1, businessUnitId);

            return messageString;
        }
        
        public string GetAdsMessageResourceByName(int adsSourceId, int messageLocationId, int? businessUnitId = null, params string[] parmas)
        {
            var resourceName = string.Empty;
            var resourceKey = string.Format(AdsSourceMessagesResourceKey, adsSourceId, messageLocationId).ToLower();
            var resource = GetAllAdsMessageResourceValue(adsSourceId, messageLocationId, businessUnitId);

            if (resource != null && resource.ContainsKey(resourceKey.Trim().ToLower()))
            {
                if (parmas != null && parmas.Length > 0)
                    try
                    {
                        return string.Format(resource[resourceKey.Trim().ToLower()].Value, parmas);
                    }
                    catch
                    {
                        return resource[resourceKey.Trim().ToLower()].Value;
                    }
                else
                    return resource[resourceKey.Trim().ToLower()].Value;

            }
            return resourceName;
        }

        public Dictionary<string, KeyValuePair<int, string>> GetAllAdsMessageResourceValue(int adsSourceId, int messageLocationId, int? businessUnitId = null, int languageId = 1)
        {
            ICacheManager cacheManage = new MemoryCacheManager();
            string key = string.Format(AdsSourceMessagesResourceKey, adsSourceId, messageLocationId).ToLower();
            var resource = cacheManage.Get(key, () =>
            {
                var adSourceMessageService = new AdsSourceMessageService();
                var resourceItem = adSourceMessageService.GetAdSourceMessage(adsSourceId, messageLocationId);

                var resourceDictionary = new Dictionary<string, KeyValuePair<int, string>>();

                if (resourceItem != null)
                {
                    if (!resourceDictionary.ContainsKey(resourceItem.ResourceName.Trim().ToLower()))
                    {
                        resourceDictionary.Add(key, new KeyValuePair<int, string>(resourceItem.Id, resourceItem.ResourceName));
                        return resourceDictionary;
                    }
                }

                return null;
            });

            return resource;
        }
        */
        public async Task<Dictionary<string, KeyValuePair<int, string>>> GetAllFreshSettingValueAsync(string keyName, int? businessUnit = null)
        {
            using var scope = services.CreateScope();
            var settingService = scope.ServiceProvider.GetRequiredService<ISettingService>();
            var setting = await settingService.GetSettingByKeyAsync(keyName, businessUnit);
            var settings = new Dictionary<string, KeyValuePair<int, string>>();

            if (setting == null) return settings;

            if (!settings.ContainsKey(setting.Name.Trim().ToLower()))
            {
                settings.Add(setting.Name.Trim().ToLower(),
                    new KeyValuePair<int, string>(setting.Id, setting.Value));
            }

            return settings;
        }

        #region Employee Photo Cache
        /*
        public string GetLoPhotoResource(string photoPath, int employeeId, int? businessUnitId = null, int cacheTime = 60)
        {
            var messageString = string.Empty;

            var resourceName = GetLoPhotoResourceByName(photoPath, employeeId, businessUnitId, cacheTime);

            if (!string.IsNullOrWhiteSpace(resourceName))
                messageString = GetResourceByName(resourceName, 1, businessUnitId);

            return messageString;
        }
        
        public string GetLoPhotoResourceByName(string photoPath, int employeeId, int? businessUnitId = null, int cacheTime = 60, params string[] parmas)
        {
            var resourceName = string.Empty;
            var resourceKey = string.Format(EmployeePhotoResourceKey, photoPath, businessUnitId).ToLower();
            var resource = GetAllLoPhotoResources(photoPath, employeeId, businessUnitId, cacheTime);

            if (resource != null && resource.ContainsKey(resourceKey.Trim().ToLower()))
            {
                if (parmas != null && parmas.Length > 0)
                    try
                    {
                        return string.Format(resource[resourceKey.Trim().ToLower()].Value, parmas);
                    }
                    catch
                    {
                        return resource[resourceKey.Trim().ToLower()].Value;
                    }
                else
                    return resource[resourceKey.Trim().ToLower()].Value;

            }
            return resourceName;
        }

        public Dictionary<string, KeyValuePair<int, string>> GetAllLoPhotoResources(string photoPath, int employeeId, int? businessUnitId = null, int cacheTime = 60)
        {
            ICacheManager cacheManage = new MemoryCacheManager();
            string key = string.Format(EmployeePhotoResourceKey, photoPath, businessUnitId).ToLower();
            var resource = cacheManage.Get(key, () =>
            {
                var result = GetLoPhoto(businessUnitId, photoPath);
                var resourceDictionary = new Dictionary<string, KeyValuePair<int, string>>();

                if (!string.IsNullOrWhiteSpace(result))
                {
                    if (!resourceDictionary.ContainsKey(result))
                    {
                        resourceDictionary.Add(key, new KeyValuePair<int, string>(employeeId, result));
                        return resourceDictionary;
                    }
                }

                return null;
            }, cacheTime);

            return resource;
        }

        public string GetLoPhoto(int? businessUnitId, string photoPath)
        {
            var ftp = new FtpHelper();

            var remoteFilePath = CommonService.GetSettingValueByKey<string>(SystemSettingKeys.FtpEmployeePhotoFolder, businessUnitId) + "/" + photoPath;

            var imageData = ftp.DownloadStream(remoteFilePath);
            if (imageData != null)
            {
                byte[] bytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        byte[] buf = new byte[1024];
                        count = imageData.Read(buf, 0, 1024);
                        ms.Write(buf, 0, count);
                    } while (imageData.CanRead && count > 0);
                    bytes = ms.ToArray();
                    ms.Close();
                }

                return Convert.ToBase64String(bytes);
            }
            return string.Empty;

        }
        */
        #endregion
    }
}