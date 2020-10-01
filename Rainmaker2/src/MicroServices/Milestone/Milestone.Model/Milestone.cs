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
}
