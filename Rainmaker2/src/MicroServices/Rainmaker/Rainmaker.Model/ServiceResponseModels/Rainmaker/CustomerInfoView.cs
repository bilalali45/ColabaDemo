using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class CustomerInfoView
    {
        public int Id { get; set; }
        public int? ContactId { get; set; }
        public int? UserId { get; set; }
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string NickName { get; set; }
        public string Preferred { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool IsActive { get; set; }
        public string AllEmail { get; set; }
        public string AllPhone { get; set; }
    }
}