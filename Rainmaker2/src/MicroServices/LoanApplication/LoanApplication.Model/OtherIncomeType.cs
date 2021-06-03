using System.Collections.Generic;

namespace LoanApplication.Model
{
    public class IncomeGroupModel
    {
        public int IncomeGroupId { get; set; }
        public string IncomeGroupName { get; set; }
        public string ImageUrl { get; set; }
        public string IncomeGroupDescription { get; set; }
        public int IncomeGroupDisplayOrder { get; set; }

        public List<IncomeTypeModel> IncomeTypes { get; set; }
        public string IncomeGroupDisplayName { get; set; }

        public class IncomeTypeModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            //public string IncomeTypeDescription { get; set; }
            //public int IncomeTypeDisplayOrder { get; set; }
            public string FieldsInfo { get; set; }
        }
    }
}