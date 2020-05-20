using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RainMaker.Common.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static void AbandonSession(this HttpContext context)
        {
            foreach (var cookie in context.Request.Cookies.Keys)
            {
                if (cookie == ".AspNetCore.Session")
                    context.Response.Cookies.Delete(cookie);
            }
        }
    }
}
