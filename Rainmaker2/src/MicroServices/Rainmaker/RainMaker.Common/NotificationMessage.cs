using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainMaker.Common
{
    public class NotificationMessage
    {
        public string Url { get; set; }
        public int NotifyId { get; set; }
        public string Message { get; set; }
        public int CriticalId { get; set; }
        public string CssClass { get; set; }
        public int? LeadNo { get; set; }
        public string BorrowerName { get; set; }
        public string BorrowerPhone { get; set; }

    }
}
