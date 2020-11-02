namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class QtPhoneInfo
    {
        public int Id { get; set; }
        public long? RNo { get; set; }
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int? EmployeeId { get; set; }
        public string NickName { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string Preferred { get; set; }
        public string Company { get; set; }
        public int? Gender { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }
        public int? IsPrimary { get; set; }
        public string Source { get; set; }
    }
}