namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class DenormOpportunityContact
    {
        public int Id { get; set; }
        public int? Duplicate { get; set; }
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string NickName { get; set; }
        public string Preferred { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AllName { get; set; }
        public string AllEmail { get; set; }
        public string AllPhone { get; set; }
        public string XmlNames { get; set; }
        public string EmpFirstName { get; set; }
        public string EmpMiddleName { get; set; }
        public string EmpLastName { get; set; }
        public string EmpNickName { get; set; }
        public string EmpPreferred { get; set; }

        public Opportunity Opportunity { get; set; }
    }
}