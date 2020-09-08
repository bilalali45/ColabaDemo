using Microsoft.AspNetCore.Http;
using RainMaker.Common.Extensions;
using RainMaker.Entity.Models;
using System.Collections.Generic;

namespace RainMaker.Common
{
    public sealed class LeadGenSession
    {
        private const string SessionSingletonName = "User_Session_94FEF882-XXXX-4963-5412-682VAR30Q861";

        private LeadGenSession()
        {
            UrlParameters = new UrlParameters();
        }

        #region ---- Public Properties

        public int OpportunityId { get; set; }
        public int LoanApplicationId { get; set; }
        public int LoanRequestId { get; set; }
        public string ZipCode { get; set; }
        public int? StateId { get; set; }
        public int? CountyId { get; set; }
        public int? CityId { get; set; }
        public bool IsVisitorLeadExist { get; set; }
        public LoanRequest SavedLoanRequest { get; set; }
        public Opportunity SavedOpportunity { get; set; }
        public List<SecondLien> SecondLiens { get; set; }
        public SecondLien DefaultLien { get; set; }
        public bool? CreateNewLoanRequest { get; set; }
        public bool CreateNewLead { get; set; }
        public bool? SecondLienPaidAtClosing { get; set; }
        public bool? WasSmTaken { get; set; }
        public bool? HasSecondLien { get; set; }
        public int LoanPurpose { get; set; }
        public bool HasSecondBorrower { get; set; }

        public UrlParameters UrlParameters { get; set; }

        public int? LoanGoalId { get; set; }

        public string LoanPurposeName { get; set; }

        public bool SkipLoanPurpose{ get; set; }

        public string UtmParameters { get; set; }
        #endregion

        public static LeadGenSession Current(HttpContext _context)
        {
            if (_context != null && _context.Session != null && _context.Session.IsAvailable)
            {
                LeadGenSession app = _context.Session.GetObject<LeadGenSession>(SessionSingletonName);
                if (app == null)
                {
                    app = new LeadGenSession();
                    _context.Session.SetObject(SessionSingletonName, app);
                }
                return app;
            }
            return new LeadGenSession();
        }
        public static void ClearSession(HttpContext _context)
        {
            _context.Session.SetObject(SessionSingletonName, null);
        }

    }
}
