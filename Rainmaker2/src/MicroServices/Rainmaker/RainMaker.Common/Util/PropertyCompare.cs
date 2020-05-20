using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RainMaker.Common.Util
{
    public static class PropertyCompare
    {
        public static List<PropertyInfo> Property = new List<PropertyInfo>();
        public static List<object> Val1 = new List<object>();
        public static List<object> Val2 = new List<object>();
        public static List<PropertyInfo> Excluded;
        public static bool Equal<T>(T x, T y)
        {
            return Compare<T>(x, y);
        }


        static bool Compare<T>(T x, T y)
        {
            bool isEqual = true;

            var props = typeof(T).GetProperties();


            //excluded= typeof(T).GetProperties().Where(prop => prop.PropertyType.IsValueType == false).ToList();
            //excluded = typeof(T).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ExcludeCheck))).ToList();

            if (props.Length == 0)
            {
                return true;
            }

            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];

                if (IsExcluded(prop))
                    continue;

                object v1 = prop.GetValue(x);
                object v2 = prop.GetValue(y);

                if (!object.Equals(v1, v2))
                {
                    isEqual = false;
                    //todo  following return statement will be uncommented and below return statement will be removed.
                    //return isEqual;

                    Property.Add(prop);
                    Val1.Add(v1);
                    Val2.Add(v2);
                }

            }

            return isEqual;
        }

        private static bool IsExcluded(PropertyInfo prop)
        {

            if (Attribute.IsDefined(prop, typeof(ExcludeCheck)))
            {
                return true;
            }

            return false;
        }
    }

}
