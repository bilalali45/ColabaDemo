using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace RainMaker.Common.Extensions
{
    public class EnumDisplayClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class EnumExtension
    {
        public static IEnumerable<EnumDisplayClass> ToIEnumerable<TEnum>()
                where TEnum : struct, IComparable, IFormattable, IConvertible
            {
                var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                             where GetEnumDescription(e) != null
                             select new EnumDisplayClass { Id = e.ToInt32(CultureInfo.InvariantCulture), Name = GetEnumDescription(e) };
                return values;
            }
        public static string GetEnumDescription<TEnum>(this TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            if (fi != null)
            {
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if ((attributes.Length > 0))
                    return attributes[0].Description;
            }
            return null;
        }

        public static string GetEnumDisplay<TEnum>(this TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            var attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);

            if ((attributes.Length > 0))
                return attributes[0].Name;
            return null;
        }
             /// <summary>
            /// Get the description attribute for the enum
            /// </summary>
            /// <param name="e"></param>
            /// <returns></returns>
            public static string Description(this Enum e)
            {
                var da = (DescriptionAttribute[])(e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false));

                return da.Length > 0 ? da[0].Description : e.ToString();
            }

        public static object GetValue(this Enum e)
        {
            
            return Convert.ChangeType(e, e.GetTypeCode());

        }
        public static string GetThirdPartyValue(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    ThirdPartyValueAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(ThirdPartyValueAttribute)) as ThirdPartyValueAttribute;
                    if (attr != null)
                    {
                        return attr.Name;
                    }
                }
            }
            return null;
        }
    }

    public class ThirdPartyValueAttribute : System.Attribute
    {
        public string Name { get; set; }
        public ThirdPartyValueAttribute(string name)
        {
            Name = name;
        }
    }

}
