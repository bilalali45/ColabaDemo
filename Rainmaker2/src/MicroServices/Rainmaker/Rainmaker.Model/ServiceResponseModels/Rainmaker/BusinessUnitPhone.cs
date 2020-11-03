namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class BusinessUnitPhone
    {
        public int Id { get; set; }
        public int BusinessUnitId { get; set; }
        public int CompanyPhoneInfoId { get; set; }
        public int? TypeId { get; set; }

        public BusinessUnit BusinessUnit { get; set; }

        public CompanyPhoneInfo CompanyPhoneInfo { get; set; }
    }
}