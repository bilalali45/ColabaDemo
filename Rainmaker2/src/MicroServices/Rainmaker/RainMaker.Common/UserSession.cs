using Microsoft.AspNetCore.Http;
using RainMaker.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;


namespace RainMaker.Common
{

    [Serializable]
    public class UserPermissions
    {
        public String PermissionName { set; get; }
        public String RoleName { set; get; }
        public int PermissionId { set; get; }

    }


    [Serializable]
    public sealed class UserSession
    {
        #region private Properties
        private const string SessionSingletonName = "User_Session_94FEF882-151C-4963-9C96-682CBA30E861";
        #endregion

        #region public Properties
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int VisitorId { get; set; }
        public int ActualVisitorId { get; set; }
        public int SessionLogId { get; set; }
        public List<UserPermissions> UserPermissionList { get; set; }
        public bool IsSystemAdmin { get; set; }
        public int EmployeeId { get; set; }
        public int CustomerId { get; set; }
        public string VisitorCode { get; set; }

        private int _businessUnitId;

        private int _brachId;

        public int? LeadCreatedFromId { get; set; }
        public string LeadSourceName { get; set; }
        public int? FirstTimeLeadSourceId { get; set; }    
        public int FirstTimeAdsourceId { get; set; }
        public string Referrer { get; set; }
        public string Error { get; set; }
        public bool IsGoogleAnalyticsUpdated { get; set; }
        public int? LoanApplicationId { get; set; }
        private string _GoogleApISessionId { get; set; }

        public TwilioServicesSetting TwilioServiceSetting { get; set; }

        public class TwilioServicesSetting
        {
            public string AccountSid { get; set; }
            public string AuthToken { get; set; }
            public string Domain { get; set; }
            public string AppSid { get; set; }

        }



        public int BusinessUnitId
        {
            get
            {
                if (_businessUnitId <= 0)
                    _businessUnitId = CommonHelper.To<int>(ConfigurationManager.AppSettings["BusinessUnit"]);
                return _businessUnitId;
            }
            set { _businessUnitId = value; }
        }
        public int BranchId
        {
            get
            {
                if (_brachId <= 0)
                    _brachId = CommonHelper.To<int>(ConfigurationManager.AppSettings["BranchId"]);
                return _brachId;
            }
            set { _brachId = value; }
        }
        public string GoogleApISessionId
        {
            get
            {
                if(string.IsNullOrEmpty(_GoogleApISessionId))
                    _GoogleApISessionId = Guid.NewGuid().ToString("N").Substring(0, 9);
                return _GoogleApISessionId;
            }
        }
        public bool VisitorTypeMarked { get; set; }
        
        public int ApiUserId { get; set; } //used for simulator authentication

        #endregion

        private UserSession()
        {
            TwilioServiceSetting =  new TwilioServicesSetting();
        }

        private static UserSession _currentServiceUser;
        /// <summary>
        /// Set this to false when using from service
        /// </summary>
        public static bool IsWebUser = true;
        public static UserSession Current(HttpContext _context)
        {
            if (_context != null && _context.Session != null && _context.Session.IsAvailable)
            {
                UserSession app = _context.Session.GetObject<UserSession>(SessionSingletonName);
                if (app == null)
                {
                    app = new UserSession();
                    _context.Session.SetObject(SessionSingletonName, app);
                }
                return app;
            }
            return _currentServiceUser ?? (_currentServiceUser = new UserSession());
        }

        public static void ClearSession(HttpContext context)
        {
            context.Session.Clear();
            context.AbandonSession();
        }
    }
}