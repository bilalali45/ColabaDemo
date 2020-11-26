using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Notification.Model
{
    public class TenantSettingModel
    {
        public short deliveryModeId { get; set; }
        public short? queueTimeout { get; set; }
    }
    public class NotificationRead
    {
        public List<long> ids { get; set; }
    }
    public class NotificationSeen
    {
        public List<long> ids { get; set; }
    }
    public class NotificationDelete
    {
        public long id { get; set; }
    }
    public class NotificationUndelete
    {
        public long id { get; set; }
    }
    public class NotificationModel
    {
        public int NotificationType { get; set; }
        public int EntityId { get; set; }
        public string CustomTextJson { get; set; }
        public DateTime? DateTime  { get; set; }
        public int? userId { get; set; }
        public int? tenantId { get; set; }
        public long Id { get; set; }
        public List<int> UsersToSendList { get; set; }
    }

    public class LoanSummary
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string LoanPurpose { get; set; }
        public string PropertyType { get; set; }
        public string StateName { get; set; }
        public string CountyName { get; set; }
        public string CityName { get; set; }
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public decimal? LoanAmount { get; set; }
        public string CountryName { get; set; }
        public string UnitNumber { get; set; }
    }

    public class NotificationMediumModel
    {
        public long id { get; set; }
        public JObject payload { get; set; }
        public string status { get; set; }
    }

    public class SettingModel
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int? UserId { get; set; }
        public short DeliveryModeId { get; set; }
        public int NotificationMediumId { get; set; }
        public int NotificationTypeId { get; set; }
        public short? DelayedInterval { get; set; }
        public string NotificationType { get; set; }
    }

    public class UpdateSettingModel
    {
        public int notificationTypeId { get; set; }
        public short deliveryModeId { get; set; }
        public short? delayedInterval { get; set; }
    }
}
