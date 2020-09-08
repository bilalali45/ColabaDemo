using Newtonsoft.Json;
using System;

namespace LosIntegration.API.ExtensionMethods
{
    public static class JsonStringExtension
    {
        public static string ToJsonString(this object model)
        {
            if (model is string) throw new ArgumentException(message: "mode should not be a string");
            return JsonConvert.SerializeObject(value: model);
        }
    }
}