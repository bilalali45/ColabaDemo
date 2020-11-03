using System;
using System.Collections.Generic;
using System.Text;

namespace LosIntegration.Model.Model.ServiceRequestModels.Milestone
{
    public class LosMilestoneRequest
    {
        //public int tenantId { get; set; }
        //public string loanId { get; set; }
        //public string milestone { get; set; }
        //public short losId { get; set; }
        //public short rainmakerLosId { get; set; }
        //public int rainmakerApplicationId { get; set; }
        public int tenantId { get; set; }
        public string loanId { get; set; }
        public int milestone { get; set; }
        public short losId { get; set; }
        public short rainmakerLosId { get; set; }
    }
}
