namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class BranchPhoneBinder
    {
        public int BranchId { get; set; }
        public int CompanyPhoneInfoId { get; set; }

        public Branch Branch { get; set; }

        public CompanyPhoneInfo CompanyPhoneInfo { get; set; }
    }
}