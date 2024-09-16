using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace DocumentsCreatorOwin.Controllers
{
    public class StreamContextResult : IHttpActionResult
    {
        private Task<Stream> _stream;
        private HttpRequestMessage _request;
        
        public StreamContextResult(Task<Stream> stream, HttpRequestMessage requestMessage)
        {
            _stream = stream;
            _request = requestMessage;
        }
        
        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var stream = await _stream;
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.RequestMessage = _request;
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "transportation_order.docx";
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            
            return response;
        }
    }
}