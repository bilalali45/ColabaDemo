using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class LocaleStringResource
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }
        public string Location { get; set; }
        public int? ResourceForId { get; set; }
        public int EntityTypeId { get; set; }
        public int? BusinessUnitId { get; set; }
        public string Description { get; set; }
        public bool IsDifferentForBusinessUnit { get; set; }

        public ICollection<AdsSourceMessage> AdsSourceMessages { get; set; }

        public ICollection<RateServiceParameter> RateServiceParameters { get; set; }

        public BusinessUnit BusinessUnit { get; set; }

        public Language Language { get; set; }
    }
}