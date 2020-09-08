using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace RainMaker.Common
{
    public class HttpResponse
    {
        public HttpStatusCode StatusCode;
        public string Content;
    }
    public class HttpHelper
    {
        private static HttpClient client = new HttpClient();
        static HttpHelper()
        {
            ServicePointManager.DefaultConnectionLimit = 100;
        }
        public async Task<HttpResponse> GetAsync(string url,Dictionary<string,string> data)
        {
            var querystring = "";
            foreach(KeyValuePair<string,string> pair in data)
            {
                if (string.IsNullOrEmpty(querystring))
                    querystring = "?";
                querystring += string.Format("{0}={1}&",HttpUtility.UrlEncode(pair.Key),HttpUtility.UrlEncode(pair.Value));
            }
            if (querystring.Length > 0)
                querystring = querystring.TrimEnd('&');
            var response = await client.GetAsync(url+querystring).ConfigureAwait(false);
            HttpResponse res = new HttpResponse();
            res.StatusCode = response.StatusCode;
            res.Content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return await Task.FromResult(res).ConfigureAwait(false);
        }
    }
}
