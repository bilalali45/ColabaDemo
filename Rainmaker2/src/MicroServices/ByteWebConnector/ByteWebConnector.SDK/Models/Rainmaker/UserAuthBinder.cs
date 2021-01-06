namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class UserAuthBinder
    {
        public int UserProfileId { get; set; }
        public int AuthProviderId { get; set; }

        //public AuthProvider AuthProvider { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}