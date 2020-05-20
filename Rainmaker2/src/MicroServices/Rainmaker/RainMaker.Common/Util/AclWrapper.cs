namespace RainMaker.Common.Util
{
    public class AclWrapper
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
    }
}
