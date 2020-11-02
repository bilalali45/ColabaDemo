using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class SessionLogDetail
    {
        public int Id { get; set; }
        public DateTime? RequestOnUtc { get; set; }
        public string UrlReferrer { get; set; }
        public string Url { get; set; }
        public int? VisitorId { get; set; }
        public int? SessionLogId { get; set; }
        public string Remarks { get; set; }
        public int EntityTypeId { get; set; }

        public SessionLog SessionLog { get; set; }
    }
}