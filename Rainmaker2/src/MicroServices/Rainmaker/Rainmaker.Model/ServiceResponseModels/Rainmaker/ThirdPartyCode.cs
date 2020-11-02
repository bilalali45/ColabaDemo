namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class ThirdPartyCode
    {
        public int Id { get; set; }
        public int? ThirdPartyId { get; set; }
        public string ElementName { get; set; }
        public string Code { get; set; }
        public int? EntityRefTypeId { get; set; }
        public int? EntityRefId { get; set; }

        public EntityType EntityType { get; set; }
    }
}