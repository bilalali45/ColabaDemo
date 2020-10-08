using System;

namespace ByteWebConnector.Model.Models.ServiceRequestModels
{
    public class StatusEntity
    {
        public long FileDataId { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public DateTime? EstimatedClosingDate { get; set; }
    }
}