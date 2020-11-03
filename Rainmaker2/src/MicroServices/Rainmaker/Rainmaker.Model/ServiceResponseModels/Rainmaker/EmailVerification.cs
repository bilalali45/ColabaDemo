using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class EmailVerification
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string Code { get; set; }
        public int? TypeId { get; set; }
        public DateTime? ExpiryDateUtc { get; set; }
    }
}