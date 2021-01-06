using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class BreachedPassword
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDateUtc { get; set; }
    }
}