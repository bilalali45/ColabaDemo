using MongoDB.Bson.IO;

namespace DocumentManagement.Model
{
    public class ApiResponse
    {
        public string Status { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
         

        public static class ApiResponseStatus
        {
            public static string Success => "Success";
            public static string NotFound => "NotFound";
            public static string Fail => "Fail";
            public static string Error => "Error";
        }
    }
}