namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class UserGridSetting
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string GridName { get; set; }
        public string Setting { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}