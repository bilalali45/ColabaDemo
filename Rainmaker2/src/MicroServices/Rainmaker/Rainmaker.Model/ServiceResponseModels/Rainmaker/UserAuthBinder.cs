namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class UserAuthBinder
    {
        public int UserProfileId { get; set; }
        public int AuthProviderId { get; set; }

        //public AuthProvider AuthProvider { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}