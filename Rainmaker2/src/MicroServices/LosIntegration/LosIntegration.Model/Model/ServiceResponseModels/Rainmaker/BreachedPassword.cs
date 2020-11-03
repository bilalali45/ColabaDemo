













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // BreachedPassword

    public partial class BreachedPassword 
    {
        public long Id { get; set; } // Id (Primary key)
        public string Password { get; set; } // Password (length: 200)
        public System.DateTime CreatedDateUtc { get; set; } // CreatedDateUtc

        public BreachedPassword()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
