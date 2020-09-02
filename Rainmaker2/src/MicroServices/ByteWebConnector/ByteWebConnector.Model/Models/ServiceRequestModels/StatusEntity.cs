using System;

namespace ByteWebConnector.Model.Models.ServiceRequestModels
{
    public class StatusEntity
    {
        public long FileDataId;
        public DateTime? ApplicationDate { get; set; }
        public DateTime? EstimatedClosingDate { get; set; }
    }
}