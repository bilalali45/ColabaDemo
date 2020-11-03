namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class BranchPhone
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int CompanyPhoneInfoId { get; set; }
        public int? TypeId { get; set; }

        public Branch Branch { get; set; }

        public CompanyPhoneInfo CompanyPhoneInfo { get; set; }
    }
}