using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models.TwoFA
{
    public class Verify2FaModel
    {
        public string Code { get; set; }
        public string RequestSid { get; set; }
    }
}
