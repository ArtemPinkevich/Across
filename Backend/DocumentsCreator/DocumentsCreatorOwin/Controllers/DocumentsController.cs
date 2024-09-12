using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using DocumentsCreatorOwin.Helpers;
using DocumentsCreatorOwin.Services;

namespace DocumentsCreatorOwin.Controllers
{
    public class DocumentsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(int transportationOrderId)
        {
            DocumentsService documentsService = new DocumentsService();

            Stream documentStream = documentsService.CreateDocument(transportationOrderId);
            
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(documentStream);
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "transportation_order.docx";
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");

            return response;
        }
        
        [HttpGet]
        public HttpResponseMessage Get()
        {
            DocumentsService documentsService = new DocumentsService();
            
            //string contentType = "";
            //var file = Path.Combine(Directory.GetCurrentDirectory(), "File", "transportation_order_template.docx") ;
            //new FileExtensionContentTypeProvider().TryGetContentType(file, out contentType);

            OrderDocument document = CreateOrderDocumentFromQuery(Request);
            Stream documentStream = documentsService.CreateDocument(document);
            
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(documentStream);
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "transportation_order.docx";
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");

            return response;
        }

        private OrderDocument CreateOrderDocumentFromQuery(HttpRequestMessage requestMessage)
        {
            OrderDocument document = new OrderDocument();
            if (string.IsNullOrEmpty(requestMessage.RequestUri.Query))
                return document;
            
            var query = requestMessage.GetQueryNameValuePairs().ToList();
            document.Route = query.FirstOrDefault(x => x.Key == "route").Value;
            document.CargoName = query.FirstOrDefault(x => x.Key == "cargoName").Value;
            document.CargoPrice = query.FirstOrDefault(x => x.Key == "cargoPrice").Value;
            document.ClientBank = query.FirstOrDefault(x => x.Key == "bank").Value;
            document.ClientBin = query.FirstOrDefault(x => x.Key == "clientBin").Value;
            document.ClientCompany = query.FirstOrDefault(x => x.Key == "company").Value;
            document.ClientEmail = query.FirstOrDefault(x => x.Key == "email").Value;
            document.DestinationAddress = query.FirstOrDefault(x => x.Key == "dest").Value;
            document.TransportationPrice = query.FirstOrDefault(x => x.Key == "transportPrice").Value;
            document.UnloadingAddress = query.FirstOrDefault(x => x.Key == "unAddress").Value;
            document.ClientBankBin = query.FirstOrDefault(x => x.Key == "bankBin").Value;
            document.ClientCurrentAccount = query.FirstOrDefault(x => x.Key == "account").Value;
            document.ClientLegalAddress = query.FirstOrDefault(x => x.Key == "legalAddress").Value;
            document.ClientNdsSeria = query.FirstOrDefault(x => x.Key == "nds").Value;
            document.ClientPhoneNumber = query.FirstOrDefault(x => x.Key == "phone").Value;
            document.DriverFullName = query.FirstOrDefault(x => x.Key == "driver").Value;
            document.LoadingDateTime = query.FirstOrDefault(x => x.Key == "loadDate").Value;
            document.ManagerFullName = query.FirstOrDefault(x => x.Key == "manager").Value;
            document.NumberOfTrucks = query.FirstOrDefault(x => x.Key == "numberOfTrucks").Value;
            document.UnloadingDateTime = query.FirstOrDefault(x => x.Key == "unloadDate").Value;
            document.CargoReceiverFullName = query.FirstOrDefault(x => x.Key == "receiver").Value;
            document.ClientBankSwiftCode = query.FirstOrDefault(x => x.Key == "bankSwift").Value;
            document.ClientCeoFullName = query.FirstOrDefault(x => x.Key == "ceo").Value;
            document.LoadingPersonFullName = query.FirstOrDefault(x => x.Key == "loadPerson").Value;
            document.UnloadingPersonFullName = query.FirstOrDefault(x => x.Key == "unloadPerson").Value;
            
            return document;
        }
    }
}