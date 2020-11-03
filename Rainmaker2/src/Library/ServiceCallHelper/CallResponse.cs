using System.Net.Http;

namespace ServiceCallHelper
{
    public class CallResponse<T>
    {
        public CallResponse(HttpResponseMessage httpResponseMessage,
                            T responseObject,
                            string rawResult)
        {
            HttpResponseMessage = httpResponseMessage;
            ResponseObject = responseObject;
        }


        public HttpResponseMessage HttpResponseMessage { get; }
        public T ResponseObject { get; }
        public string RawResult { get; set; }
    }
}