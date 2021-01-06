namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class EmployeePhoneBinder
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyPhoneInfoId { get; set; }
        public int TypeId { get; set; }

        public CompanyPhoneInfo CompanyPhoneInfo { get; set; }

        //public Employee Employee { get; set; }

    }
}