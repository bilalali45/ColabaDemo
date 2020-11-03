













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // LoanContactRaceBinder

    public partial class LoanContactRaceBinder 
    {
        public int Id { get; set; } // Id (Primary key)
        public int LoanContactId { get; set; } // LoanContactId
        public int RaceId { get; set; } // RaceId
        public int? RaceDetailId { get; set; } // RaceDetailId
        public string OtherRace { get; set; } // OtherRace (length: 150)

        // Foreign keys

        /// <summary>
        /// Parent LoanContact pointed by [LoanContactRaceBinder].([LoanContactId]) (FK_LoanContactRaceBinder_LoanContact)
        /// </summary>
        public virtual LoanContact LoanContact { get; set; } // FK_LoanContactRaceBinder_LoanContact

        /// <summary>
        /// Parent Race pointed by [LoanContactRaceBinder].([RaceId]) (FK_LoanContactRaceBinder_Race)
        /// </summary>
        public virtual Race Race { get; set; } // FK_LoanContactRaceBinder_Race

        /// <summary>
        /// Parent RaceDetail pointed by [LoanContactRaceBinder].([RaceDetailId]) (FK_LoanContactRaceBinder_RaceDetail)
        /// </summary>
        public virtual RaceDetail RaceDetail { get; set; } // FK_LoanContactRaceBinder_RaceDetail

        public LoanContactRaceBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
