













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // EmailVerification

    public partial class EmailVerification 
    {
        public int Id { get; set; } // Id (Primary key)
        public string EmailAddress { get; set; } // EmailAddress (length: 200)
        public string Code { get; set; } // Code (length: 50)
        public int? TypeId { get; set; } // TypeId
        public System.DateTime? ExpiryDateUtc { get; set; } // ExpiryDateUtc

        public EmailVerification()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
