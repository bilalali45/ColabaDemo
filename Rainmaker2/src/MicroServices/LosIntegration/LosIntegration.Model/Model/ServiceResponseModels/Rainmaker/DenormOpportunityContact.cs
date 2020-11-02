













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // DenormOpportunityContact

    public partial class DenormOpportunityContact 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? Duplicate { get; set; } // Duplicate
        public string Prefix { get; set; } // Prefix (length: 10)
        public string FirstName { get; set; } // FirstName (length: 300)
        public string MiddleName { get; set; } // MiddleName (length: 50)
        public string LastName { get; set; } // LastName (length: 300)
        public string Suffix { get; set; } // Suffix (length: 10)
        public string NickName { get; set; } // NickName (length: 50)
        public string Preferred { get; set; } // Preferred (length: 1000)
        public string Email { get; set; } // Email (length: 150)
        public string Phone { get; set; } // Phone (length: 150)
        public string AllName { get; set; } // AllName
        public string AllEmail { get; set; } // AllEmail
        public string AllPhone { get; set; } // AllPhone
        public string XmlNames { get; set; } // XmlNames
        public string EmpFirstName { get; set; } // EmpFirstName (length: 300)
        public string EmpMiddleName { get; set; } // EmpMiddleName (length: 50)
        public string EmpLastName { get; set; } // EmpLastName (length: 300)
        public string EmpNickName { get; set; } // EmpNickName (length: 50)
        public string EmpPreferred { get; set; } // EmpPreferred (length: 1000)

        // Foreign keys

        /// <summary>
        /// Parent Opportunity pointed by [DenormOpportunityContact].([Id]) (FK_DenormOpportunityContact_Opportunity)
        /// </summary>
        public virtual Opportunity Opportunity { get; set; } // FK_DenormOpportunityContact_Opportunity

        public DenormOpportunityContact()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
