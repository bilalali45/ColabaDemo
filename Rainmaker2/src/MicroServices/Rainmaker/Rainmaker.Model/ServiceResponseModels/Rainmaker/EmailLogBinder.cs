namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class EmailLogBinder
    {
        public int EmailLogId { get; set; }
        public int ContactId { get; set; }

        public Contact Contact { get; set; }

        public EmailLog EmailLog { get; set; }
    }
}