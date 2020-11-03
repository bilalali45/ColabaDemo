namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class BusinessUnitEmail
    {
        public int Id { get; set; }
        public int BusinessUnitId { get; set; }
        public int EmailAccountId { get; set; }
        public int TypeId { get; set; }

        public BusinessUnit BusinessUnit { get; set; }

        public EmailAccount EmailAccount { get; set; }
    }
}