using Microsoft.AspNetCore.Http;
using RainMaker.Common.Extensions;
using System;

namespace RainMaker.Common
{
    [Serializable]
    public class LoanAppSession
    {
        private static string sessionName = "LoanAppSession";
        public int LoanAppId { get; set; }
        public int LoanPurposeId { get; set; }
        public string FirstName { get; set; }
        public string OtherFirstName { get; set; }
        public int LoanPartnerTypeId { get; set; }
        
        public static LoanAppSession Current(HttpContext _context)
        {
            if (_context != null && _context.Session != null && _context.Session.IsAvailable)
            {
                LoanAppSession app = _context.Session.GetObject<LoanAppSession>(sessionName);
                if (app == null)
                {
                    app = new LoanAppSession();
                    _context.Session.SetObject(sessionName, app);
                }
                return app;
            }
            return new LoanAppSession();
        }
        
    }
}
