namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class BranchEmail
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int EmailAccountId { get; set; }
        public int TypeId { get; set; }

        public Branch Branch { get; set; }

        public EmailAccount EmailAccount { get; set; }
    }
}