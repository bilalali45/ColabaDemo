namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class NotificationChange
    {
        public int Id { get; set; }
        public int? NotificationObjectId { get; set; }
        public int? ActorId { get; set; }
        public string Active { get; set; }
        public byte? Status { get; set; }

        public NotificationObject NotificationObject { get; set; }
    }
}