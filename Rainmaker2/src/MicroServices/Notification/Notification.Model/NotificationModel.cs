﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.Model
{
    public class NotificationModel
    {
        public int NotificationType { get; set; }
        public int EntityId { get; set; }
        public string CustomTextJson { get; set; }
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
}
