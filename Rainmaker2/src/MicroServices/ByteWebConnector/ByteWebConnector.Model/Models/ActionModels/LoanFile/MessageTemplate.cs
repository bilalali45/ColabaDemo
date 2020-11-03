













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // MessageTemplate

    public partial class MessageTemplate 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string BccEmailAddresses { get; set; } // BccEmailAddresses (length: 200)
        public string Subject { get; set; } // Subject (length: 1000)
        public string Body { get; set; } // Body
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public int EmailAccountId { get; set; } // EmailAccountId
        public bool IsDeleted { get; set; } // IsDeleted

        public MessageTemplate()
        {
            IsActive = true;
            EntityTypeId = 125;
            IsDeleted = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
