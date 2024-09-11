using System;
using System.IO;
using System.Web.Http;
using System.Web.Mvc;
using DocumentsCreator.Helpers;

namespace DocumentsCreator.Controllers
{
    public class DocumentsController : ApiController
    {
        public ActionResult Get()
        {
            string contentType = "";
            
            FileExtensionContentTypeProvider typeProvider = new FileExtensionContentTypeProvider();

            string path = Directory.GetCurrentDirectory();
            var filename = "";
            typeProvider.TryGetContentType(filename, out contentType);
            
            Stream stream = new MemoryStream();
            return new FileStreamResult(stream, "text");
        }
    }
}