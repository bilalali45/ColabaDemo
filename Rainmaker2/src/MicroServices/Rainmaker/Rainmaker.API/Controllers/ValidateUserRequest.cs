namespace Rainmaker.API.Controllers
{
    public class ValidateUserRequest

    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Employee { get; set; }
    }


    public class GetUserRequest

    {
        public string UserName { get; set; }
        public bool Employee { get; set; }
    }
}