using System.Net;
using System.Threading.Tasks;

namespace RainMaker.Common
{
    public class WebRequestHelper
    {
        public static async Task<bool> FileExistsAsync(string url)
        {
            HttpWebResponse response = null;
            var request = (HttpWebRequest) System.Net.WebRequest.Create(requestUriString: url);
            request.Method = "HEAD";

            try
            {
                response = (HttpWebResponse) await request.GetResponseAsync();
                return true;
            }
            catch (WebException)
            {
                /* A WebException will be thrown if the status of the response is not `200 OK` */
            }
            finally
            {
                // Don't forget to close your response.
                if (response != null) response.Close();
            }

            return false;
        }
    }
}