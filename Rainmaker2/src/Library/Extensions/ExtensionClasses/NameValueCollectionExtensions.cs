using System.Collections.Generic;
using System.Collections.Specialized;

namespace Extensions.ExtensionClasses
{
    public static class NameValueCollectionExtensions
    {

        public static Dictionary<string,string> ToDictionary(this NameValueCollection nvc)
        {
            var result = new Dictionary<string, string>();
            foreach (string key in nvc.Keys)
            {
                if(key != null)
                    result.Add(key, nvc[key]);
            }

            return result;

        }
    }
}
