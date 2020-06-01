using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainMaker.Common.Extensions
{
    public static class NameValueCollectionExtensions
    {

        public static Dictionary<string,string> ToDictionary(this NameValueCollection nvc)
        {
            var result = new Dictionary<string, string>();
            foreach (string key in nvc.Keys)
            {
                
               
                    result.Add(key, nvc[key]);
                 
            }

            return result;

        }
    }
}
