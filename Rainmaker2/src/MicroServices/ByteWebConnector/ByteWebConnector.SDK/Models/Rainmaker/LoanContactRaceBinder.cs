namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class LoanContactRaceBinder
    {
        public int Id { get; set; }
        public int LoanContactId { get; set; }
        public int RaceId { get; set; }
        public int? RaceDetailId { get; set; }
        public string OtherRace { get; set; }

        public LoanContact LoanContact { get; set; }

        public Race Race { get; set; }

        public RaceDetail RaceDetail { get; set; }
    }
}