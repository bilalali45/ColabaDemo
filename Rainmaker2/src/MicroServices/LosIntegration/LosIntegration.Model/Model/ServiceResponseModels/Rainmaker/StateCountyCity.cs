













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // StateCountyCity

    public partial class StateCountyCity 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? StateId { get; set; } // StateId
        public int? CountyId { get; set; } // CountyId
        public int? CityId { get; set; } // CityId
        public string StateName { get; set; } // StateName (length: 100)
        public string Abbreviation { get; set; } // Abbreviation (length: 10)
        public string CountyName { get; set; } // CountyName (length: 100)
        public string CountyType { get; set; } // CountyType (length: 100)
        public string CityName { get; set; } // CityName (length: 200)
        public string DisplayName { get; set; } // DisplayName (length: 300)
        public string SearchKey { get; set; } // SearchKey (length: 200)
        public string Ids { get; set; } // Ids (length: 50)

        public StateCountyCity()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
