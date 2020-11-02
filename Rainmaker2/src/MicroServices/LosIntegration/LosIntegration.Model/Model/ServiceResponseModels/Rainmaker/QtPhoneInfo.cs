













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // qtPhoneInfo

    public partial class QtPhoneInfo 
    {
        public int Id { get; set; } // Id (Primary key)
        public long? RNo { get; set; } // RNo
        public int ContactId { get; set; } // ContactId
        public string FirstName { get; set; } // FirstName (length: 300)
        public string MiddleName { get; set; } // MiddleName (length: 50)
        public string LastName { get; set; } // LastName (length: 300)
        public string UserName { get; set; } // UserName (length: 256)
        public int? EmployeeId { get; set; } // EmployeeId
        public string NickName { get; set; } // NickName (length: 50)
        public string Prefix { get; set; } // Prefix (length: 10)
        public string Suffix { get; set; } // Suffix (length: 10)
        public string Preferred { get; set; } // Preferred (length: 1000)
        public string Company { get; set; } // Company (length: 500)
        public int? Gender { get; set; } // Gender
        public string Phone { get; set; } // Phone (length: 150)
        public string Type { get; set; } // Type (length: 50)
        public int? IsPrimary { get; set; } // IsPrimary
        public string Source { get; set; } // Source (length: 16)

        public QtPhoneInfo()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
