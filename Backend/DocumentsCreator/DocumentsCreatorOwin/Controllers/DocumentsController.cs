using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using DocumentsCreatorOwin.Helpers;

namespace DocumentsCreatorOwin.Controllers
{
    public class DocumentsController : ApiController
    {
        public DocumentsController()
        {
            
        }
        
        // GET api/values 
        public HttpResponseMessage Get() 
        { 
            string contentType = "";
            
            FileExtensionContentTypeProvider typeProvider = new FileExtensionContentTypeProvider();

            string path = Directory.GetCurrentDirectory();
            var file = Path.Combine(path, "File", "transportation_order_template.docx") ;
            typeProvider.TryGetContentType(file, out contentType);
            
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new FileStream(file, FileMode.Open, FileAccess.Read));
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "transportation_order_template.docx";
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            return response;
        }
    }
}