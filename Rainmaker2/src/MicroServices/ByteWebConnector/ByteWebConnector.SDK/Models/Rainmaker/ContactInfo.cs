namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class ContactInfo
    {
        public int Id { get; set; }
        public string Info { get; set; }
        public string Type { get; set; }
        public string InfoType { get; set; }
        public bool? IsPrimary { get; set; }
        public int? ContactId { get; set; }
        public bool IsDeleted { get; set; }
        public int? ValidityId { get; set; }
        public int EntityTypeId { get; set; }
        public int? UseForId { get; set; }

        public Contact Contact { get; set; }
    }
}