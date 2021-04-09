using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LosIntegration.Tests
{
    public class TestMessageHandler : HttpMessageHandler
    {
        private readonly IDictionary<string, HttpResponseMessage> messages;

        public TestMessageHandler(IDictionary<string, HttpResponseMessage> messages)
        {
            this.messages = messages;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);
            if (messages.ContainsKey(request.RequestUri.ToString().ToLower()))
                response = messages[request.RequestUri.ToString().ToLower()] ?? new HttpResponseMessage(HttpStatusCode.NoContent);
            response.RequestMessage = request;
            return Task.FromResult(response);
        }
    }
    public class TestWebReponse : HttpWebResponse
    {
        Stream responseStream;

        /// <summary>Initializes a new instance of <see cref="TestWebReponse"/>
        /// with the response stream to return.</summary>
        public TestWebReponse(Stream responseStream)
        {
            this.responseStream = responseStream;
        }

        /// <summary>See <see cref="WebResponse.GetResponseStream"/>.</summary>
        public override Stream GetResponseStream()
        {
            return responseStream;
        }

    }

    public class HttpWebRequestWrapper : IHttpWebRequestWrapper
    {
        private HttpWebRequest httpWebRequest;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpWebRequestWrapper"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        public HttpWebRequestWrapper(Uri url)
        {
            this.httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
        }

        /// <summary>
        /// Gets the request stream to write data.
        /// </summary>
        /// <returns></returns>
        public Stream GetRequestStream()
        {
            return this.httpWebRequest.GetRequestStream();
        }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        public string Method
        {
            get { return this.httpWebRequest.Method; }
            set { this.httpWebRequest.Method = value; }
        }

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>The type of the content.</value>
        public string ContentType
        {
            get { return this.httpWebRequest.ContentType; }
            set { this.httpWebRequest.ContentType = value; }
        }

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <returns></returns>
        public IHttpWebResponseWrapper GetResponse()
        {
            return new HttpWebResponseWrapper(this.httpWebRequest.GetResponse());
        }
    }

    public class HttpWebResponseWrapper : IHttpWebResponseWrapper
    {
        private WebResponse webResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpWebResponseWrapper"/> class.
        /// </summary>
        /// <param name="webResponse">The web response.</param>
        public HttpWebResponseWrapper(WebResponse webResponse)
        {
            this.webResponse = webResponse;
        }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>The status code.</value>
        public HttpStatusCode StatusCode
        {
            get { return ((HttpWebResponse)webResponse).StatusCode; }
        }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <returns></returns>
        public string GetContent()
        {
            using (StreamReader stream = new StreamReader(webResponse.GetResponseStream()))
            {
                return stream.ReadToEnd();
            }
        }
    }

    public interface IHttpWebRequestWrapper
    {
        string ContentType { get; set; }
        Stream GetRequestStream();
        IHttpWebResponseWrapper GetResponse();
        string Method { get; set; }
    }

    public interface IHttpWebResponseWrapper
    {
        string GetContent();
        HttpStatusCode StatusCode { get; }
    }
    public interface IHttpWebRequestFactory
    {
        HttpWebRequest Create(string uri);
    }
}
