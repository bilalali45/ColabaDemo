using Newtonsoft.Json;

namespace ByteWebConnector.SDK.Models
{
    public class ApiResponse<T>
    {
        public string Status { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(value: this);
        }


       
    }

    public class ApiResponseStatus
    {
        public const string Success = "Success";
        public const string NotFound = "NotFound";
        public const string Fail = "Fail";
        public const string Error = "Error";
    }
}