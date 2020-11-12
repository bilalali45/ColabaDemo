using System;
using System.Collections.Generic;
using System.Net;
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

        public int loanAplicationId { get; set; }
        public int milestoneId { get; set; }
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
    
    public class MilestoneSettingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Visible { get; set; }
        public string BorrowerName { get; set; }
        public string McuName { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public int TenantId { get; set; }
    }
    
    public class LosModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class MappingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public int? MilestoneId { get; set; }
    }
    public class GlobalMilestoneSettingModel
    {
        public bool ShowMilestone { get; set; }
        public int TenantId { get; set; }
    }

    public class MilestoneMappingModel
    {
        public int Id { get; set; }
        public List<int> Mapping { get; set; }
    }
    public class MilestoneAddMappingModel
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public short LosId { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public int MilestoneId { get; set; }
    }

    public class LosMilestoneModel
    {
        public int tenantId { get; set; }
        public string loanId { get; set; }
        public int milestone { get; set; }
        public short losId { get; set; }
        public short rainmakerLosId { get; set; }
    }

    public class MilestoneloanIdsModel
    {
        public int[] loanApplicationId { get; set; }
    }
    public class BothLosMilestoneModel
    {
        public int? milestoneId { get; set; }
        public int? losMilestoneId { get; set; }
    }
    public class MilestoneForMcuDashboard
    {
        public string milestone { get; set; }
        public string losMilestone { get; set; }
    }
}
