using Newtonsoft.Json;

namespace LosIntegration.Model.Model.ServiceResponseModels.ByteWebConnector
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
