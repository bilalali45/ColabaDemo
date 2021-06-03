using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Identity.Service.Mobile.Models
{
    public class HasCompletedTimeoutModel
    {
        public bool HasCompleted { get; set; }
        public string PhoneNumber { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? RemainingTimeout { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? LastSendAt { get; set; }
        public int? AttemptsCount { get; set; }
    }
}
