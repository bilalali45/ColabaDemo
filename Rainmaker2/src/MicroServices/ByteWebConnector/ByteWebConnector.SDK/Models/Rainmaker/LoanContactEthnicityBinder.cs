namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class LoanContactEthnicityBinder
    {
        public int Id { get; set; }
        public int LoanContactId { get; set; }
        public int EthnicityId { get; set; }
        public int? EthnicityDetailId { get; set; }
        public string OtherEthnicity { get; set; }

        public Ethnicity Ethnicity { get; set; }

        public EthnicityDetail EthnicityDetail { get; set; }

        //public LoanContact LoanContact { get; set; }

    }
}