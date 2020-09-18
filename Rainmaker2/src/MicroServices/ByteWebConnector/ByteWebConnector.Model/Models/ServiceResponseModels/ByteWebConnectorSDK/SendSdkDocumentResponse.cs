using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ByteWebConnector.Model.Models.ServiceResponseModels.ByteWebConnectorSDK
{
    public class SendSdkDocumentResponse
    {
        public string Status { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(value: this);
        }


        public static class SdkDocumentResponseStatus
        {
            public static string Success => "Success";
            public static string NotFound => "NotFound";
            public static string Fail => "Fail";
            public static string Error => "Error";
        }

    }
}
