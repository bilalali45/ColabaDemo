namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class GenericAttribute
    {
        public int Id { get; set; }
        public int EntityTypeId { get; set; }
        public string KeyGroup { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}