namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class Acl
    {
        public int UserId { get; set; }
        public int EntityRefTypeId { get; set; }
        public int EntityRefId { get; set; }
        public bool EditPermit { get; set; }
        public string EditLogic { get; set; }
        public bool DeletePermit { get; set; }
        public string DeleteLogic { get; set; }
        public bool ViewPermit { get; set; }
        public string ViewLogic { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public System.DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public System.DateTime? CreatedOnUtc { get; set; }






        public EntityType EntityType { get; set; }




        public UserProfile UserProfile { get; set; }

       

        
    }

}

