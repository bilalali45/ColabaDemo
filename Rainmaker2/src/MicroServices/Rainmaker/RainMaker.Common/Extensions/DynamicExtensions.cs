using System.Collections.Generic;
using System.Dynamic;

namespace RainMaker.Common.Extensions
{
    public static class DynamicExtensions
    {
        public static bool IsPropertyExist(dynamic settings, string name)
        {
            if (settings is ExpandoObject)
                return ((IDictionary<string, object>)settings).ContainsKey(name);

            return settings.GetType().GetProperty(name) != null;
        }
    }
}