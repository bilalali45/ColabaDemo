













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // CustomerInfoView

    public partial class CustomerInfoView 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? ContactId { get; set; } // ContactId
        public int? UserId { get; set; } // UserId
        public string Prefix { get; set; } // Prefix (Primary key) (length: 10)
        public string FirstName { get; set; } // FirstName (Primary key) (length: 300)
        public string MiddleName { get; set; } // MiddleName (Primary key) (length: 50)
        public string LastName { get; set; } // LastName (Primary key) (length: 300)
        public string Suffix { get; set; } // Suffix (Primary key) (length: 10)
        public string NickName { get; set; } // NickName (Primary key) (length: 50)
        public string Preferred { get; set; } // Preferred (Primary key) (length: 1000)
        public string Phone { get; set; } // Phone (Primary key) (length: 150)
        public string Email { get; set; } // Email (Primary key) (length: 150)
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public bool IsActive { get; set; } // IsActive (Primary key)
        public string AllEmail { get; set; } // AllEmail
        public string AllPhone { get; set; } // AllPhone

        public CustomerInfoView()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
