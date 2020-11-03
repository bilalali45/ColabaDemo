













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // GenericAttribute

    public partial class GenericAttribute 
    {
        public int Id { get; set; } // Id (Primary key)
        public int EntityTypeId { get; set; } // EntityTypeId
        public string KeyGroup { get; set; } // KeyGroup (length: 400)
        public string Key { get; set; } // Key (length: 400)
        public string Value { get; set; } // Value

        public GenericAttribute()
        {
            EntityTypeId = 6;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
