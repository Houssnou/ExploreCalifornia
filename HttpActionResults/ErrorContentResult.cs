using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExploreCalifornia.HttpActionResults
{
    public class ErrorContentResult : IHttpActionResult
    {
        private HttpRequestMessage _request;
        private string _content;
        private string _mediaType;

        public ErrorContentResult(string content, string mediaType, HttpRequestMessage request)
        {
            _content = content;
            _request = request;
            _mediaType = mediaType;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(_content, Encoding.UTF8, _mediaType),
                RequestMessage = _request
            });
        }
    }
}