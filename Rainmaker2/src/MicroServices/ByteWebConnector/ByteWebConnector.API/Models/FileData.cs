namespace ByteWebConnector.API.Models
{
    public class FileData
    {
        public int LoanID { get; set; }
        public long OrganizationID { get; set; }
        public string FileName { get; set; }
        public int OccupancyType { get; set; }
        public string AgencyCaseNo { get; set; }
        public long FileDataID { get; set; }

    }
}
