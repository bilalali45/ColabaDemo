using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class UserResetPasswordLog
    {
        public int Id { get; set; }
        public int? VisitorId { get; set; }
        public int UserId { get; set; }
        public string Password { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int EntityTypeId { get; set; }
        public int? BusinessUnitId { get; set; }
        public int ChangeType { get; set; }
        public int? PasswordFormatId { get; set; }
        public string PasswordSalt { get; set; }

        public BusinessUnit BusinessUnit { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}