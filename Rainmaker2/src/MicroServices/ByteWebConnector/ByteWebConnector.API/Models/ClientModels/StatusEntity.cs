using System;

namespace LosIntegration.API.Models.ClientModels
{
    public class StatusEntity
    {
        public long FileDataId;
        public DateTime? ApplicationDate { get; set; }
        public DateTime? EstimatedClosingDate { get; set; }
    }
}