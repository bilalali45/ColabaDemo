namespace ByteWebConnector.Model.Models.ServiceResponseModels.BytePro
{
    public class FileDataResponse
    {
        public int LoanId { get; set; }
        public long OrganizationId { get; set; }
        public string FileName { get; set; }
        public int OccupancyType { get; set; }
        public string AgencyCaseNo { get; set; }
        public long FileDataId { get; set; }
    }
}
