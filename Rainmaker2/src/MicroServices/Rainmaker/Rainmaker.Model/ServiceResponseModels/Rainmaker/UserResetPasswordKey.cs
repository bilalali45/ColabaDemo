using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class UserResetPasswordKey
    {
        public int Id { get; set; }
        public int? VisitorId { get; set; }
        public int UserId { get; set; }
        public string PasswordKey { get; set; }
        public DateTime ExpireOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ResetOnUtc { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public int? BusinessUnitId { get; set; }

        public BusinessUnit BusinessUnit { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}