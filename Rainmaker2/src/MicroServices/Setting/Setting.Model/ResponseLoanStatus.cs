using System;
using System.Collections.Generic;
using System.Text;

namespace Setting.Model
{
    public class StatusConfigurationModel
    {
        public bool isActive { get; set; }
        public List<LoanStatus> loanStatuses { get; set; }
    }
    public class LoanStatus
    {
        public int id { get; set; }
        public string mcuName { get; set; }
        public int statusId { get; set; }
        public int tenantId { get; set; }
        public int fromStatusId { get; set; }
        public string fromStatus { get; set; }
        public int toStatusId { get; set; }
        public string toStatus { get; set; }
        public short noofDays { get; set; }
        public DateTime recurringTime { get; set; }
        public bool isActive { get; set; }
        public int emailId { get; set; }
        public string fromAddress { get; set; }
        public string ccAddress { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }

}
