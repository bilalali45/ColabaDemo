using System;
using System.Collections.Generic;
using System.Text;

namespace Milestone.Model
{
    public enum MilestoneType
    {
        Timeline=1,
        Special=2
    }
    public class MilestoneForBorrowerDashboard
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }
    public class MilestoneForLoanCenter
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public int MilestoneType { get; set; }
        public bool IsCurrent { get; set; }
    }
    public class MilestoneModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MilestoneIdModel
    {
        public int loanApplicationId { get; set; }
        public int milestoneId { get; set; }
    }
    
    public class MilestoneSettingListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Visible { get; set; }
        public string BorrowerName { get; set; }
        public string McuName { get; set; }
        public string Description { get; set; }
    }
    /*
    public class MilestoneMappingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short LosId { get; set; }
        public List<string> Mapping { get; set; }
    }*/

    public class GlobalMilestoneSettingModel
    {
        public bool ShowMilestone { get; set; }
        public int TenantId { get; set; }
    }
}
