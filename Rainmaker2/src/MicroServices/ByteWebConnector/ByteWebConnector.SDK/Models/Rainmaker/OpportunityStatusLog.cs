using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class OpportunityStatusLog
    {
        public int Id { get; set; }
        public int? StatusId { get; set; }
        public int? StatusCauseId { get; set; }
        public DateTime? DatetimeUtc { get; set; }
        public int? OpportunityId { get; set; }
        public bool IsActive { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int EntityTypeId { get; set; }

        public Opportunity Opportunity { get; set; }

        public StatusCause StatusCause { get; set; }

        public StatusList StatusList { get; set; }
    }
}