using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace Identity.Models
{

    public static partial class ObjectExtensions
    {
        

        

        public static string ToJson(this object value)
        {
            if (value == null) return null;

            try
            {
                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                string json = JsonConvert.SerializeObject(value, formatting: Formatting.Indented, settings: jsonSerializerSettings);
                return json;
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

    }



}

