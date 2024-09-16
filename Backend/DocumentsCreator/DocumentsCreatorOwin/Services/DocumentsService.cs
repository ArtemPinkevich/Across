using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Word;
using DbContext = DocumentsCreatorOwin.DAL.DbContext;

namespace DocumentsCreatorOwin.Services
{
    public class DocumentsService
    {
        private const string SQLITE_CONNECTION_STRING = @"Filename=../../../../Across/Across.db";
        
        private const string ROUTE = "ROUTE";
        private const string NUMBER_OF_TRUCKS = "NUMBER_OF_TRUCKS";
        private const string DESTIANTION_ADDRESS = "DESTIANTION_ADDRESS";
        private const string LOADING_DATE_TIME = "LOADING_DATE_TIME";
        private const string LOADING_PERSON_FULL_NAME = "LOADING_PERSON_FULL_NAME";
        private const string CARGO_NAME = "CARGO_NAME";
        private const string CARGO_PRICE = "CARGO_PRICE";
        private const string CARGO_RECEIVER_FULL_NAME = "CARGO_RECEIVER_FULL_NAME";
        private const string UNLOADING_ADDRESS = "UNLOADING_ADDRESS";
        private const string UNLOADING_PERSON_FULL_NAME = "UNLOADING_PERSON_FULL_NAME";
        private const string UNLOADING_DATE_TIME = "UNLOADING_DATE_TIME";
        private const string TRANSPORTATION_PRICE = "TRANSPORTATION_PRICE";
        private const string DRIVER_NAME_SURNAME = "DRIVER_FULL_NAME";
        private const string MANAGER_NAME_SURNAME = "MANAGER_FULL_NAME";
        private const string CLIENT_COMPANY = "CLIENT_COMPANY";
        private const string CLIENT_PHONE_NUMBER = "CLIENT_PHONE_NUMBER";
        private const string CLIENT_EMAIL = "CLIENT_EMAIL";
        private const string CLIENT_BIN = "CLIENT_BIN";
        private const string CLIENT_NDS_SERIA = "CLIENT_NDS_SERIA";
        private const string CLIENT_BANK = "CLIENT_BANK";
        private const string CLIENT_BANK_BIN = "CLIENT_BANK_BIN";
        private const string CLIENT_BANK_SWIFT_CODE = "CLIENT_BANK_SWIFT_CODE";
        private const string CLIENT_CURRENT_ACCOUNT = "CLIENT_CURRENT_ACCOUNT";
        private const string CLIENT_LEGAL_ADDRESS = "CLIENT_LEGAL_ADDRESS";
        private const string CLEINT_CEO_FULL_NAME = "CLEINT_CEO_FULL_NAME";

        private Application _application;
        private Document _document;

        public async Task<Stream> CreateDocument(int transportationOrderId)
        {
            try
            {
                var document = await GetOrderDocument(transportationOrderId);
                string file = CreateWordOrderDocument(document);
                return new FileStream(file, FileMode.Open, FileAccess.Read);
            }
            catch (Exception e)
            {
                _document.Close();
                _application.Quit();
                return null;
            }
        }
        
        public Stream CreateDocument(OrderDocument order)
        {
            try
            {
                string file = CreateWordOrderDocument(order);
                return new FileStream(file, FileMode.Open, FileAccess.Read);
            }
            catch
            {
                _document.Close();
                _application.Quit();
                return null;
            }
        }

        private async Task<OrderDocument> GetOrderDocument(int transportationOrderId)
        {
            OrderDocument document = new OrderDocument();
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();

            var options = optionsBuilder
                .UseSqlite(SQLITE_CONNECTION_STRING)
                .Options;
            
            using (DbContext dbContext = new DbContext(options))
            {
                var order = await dbContext.TransportationOrders
                    .Include(x => x.Cargo)
                    .FirstOrDefaultAsync(x => x.Id == transportationOrderId);

                var transportation =
                    await dbContext.Transportations.FirstOrDefaultAsync(x =>
                        x.TransportationOrderId == transportationOrderId);

                var driver = await dbContext.AspNetUsers
                    .FirstOrDefaultAsync(x => x.Id == transportation.DriverId);

                var legalInfo =
                    await dbContext.LegalInformations.FirstOrDefaultAsync(x => x.ShipperId == order.ShipperId);
                
                document.Route = $"{order.LoadingLocalityName} - {order.UnloadingLocalityName}";
                document.NumberOfTrucks = 1.ToString();
                document.DestinationAddress = order.UnloadingLocalityName;
                document.LoadingDateTime = order.LoadDateFrom;
                document.LoadingPersonFullName = "";
                document.CargoName = order.Cargo.Name;
                document.CargoPrice = order.Cargo.Price.ToString();
                document.CargoReceiverFullName = "";
                document.UnloadingAddress = order.UnloadingLocalityName;
                document.UnloadingPersonFullName = "";
                document.UnloadingDateTime = order.LoadDateTo;
                document.TransportationPrice = order.Price.ToString();
                document.DriverFullName = $"{driver.Name} {driver.Surname} {driver.Patronymic}";
                document.ManagerFullName = "";
                document.ClientCompany = legalInfo.CompanyName;
                document.ClientPhoneNumber = legalInfo.PhoneNumber;
                document.ClientEmail = legalInfo.Email;
                document.ClientBin = legalInfo.Bin;
                document.ClientNdsSeria = legalInfo.VatSeria;
                document.ClientBank = legalInfo.BankName;
                document.ClientBankSwiftCode = legalInfo.BankSwiftCode;
                document.ClientCurrentAccount = legalInfo.AccountNumber;
                document.ClientLegalAddress = legalInfo.LegalAddress;
                document.ClientCeoFullName = legalInfo.CompanyCeo;
            }

            return document;
        }

        private string CreateWordOrderDocument(OrderDocument order)
        {
            _application = new ApplicationClass();
            string templateFile = Path.Combine(Directory.GetCurrentDirectory(), "Files", "transportation_order_template.docx");
            _document = _application.Documents.Open(templateFile);
            _document.Activate();

            Replace(_application, ROUTE, order.Route);
            Replace(_application, NUMBER_OF_TRUCKS, order.NumberOfTrucks);
            Replace(_application, DESTIANTION_ADDRESS, order.DestinationAddress);
            Replace(_application, LOADING_DATE_TIME, order.LoadingDateTime);
            Replace(_application, LOADING_PERSON_FULL_NAME, order.LoadingPersonFullName);
            Replace(_application, CARGO_NAME, order.CargoName);
            Replace(_application, CARGO_PRICE, order.CargoPrice);
            Replace(_application, CARGO_RECEIVER_FULL_NAME, order.CargoReceiverFullName);
            Replace(_application, UNLOADING_ADDRESS, order.UnloadingAddress);
            Replace(_application, UNLOADING_PERSON_FULL_NAME, order.UnloadingPersonFullName);
            Replace(_application, UNLOADING_DATE_TIME, order.UnloadingDateTime);
            Replace(_application, TRANSPORTATION_PRICE, order.TransportationPrice);
            Replace(_application, DRIVER_NAME_SURNAME, order.DriverFullName);
            Replace(_application, MANAGER_NAME_SURNAME, order.ManagerFullName);
            Replace(_application, CLIENT_COMPANY, order.ClientCompany);
            Replace(_application, CLIENT_PHONE_NUMBER, order.ClientPhoneNumber);
            Replace(_application, CLIENT_EMAIL, order.ClientEmail);
            Replace(_application, CLIENT_BIN, order.ClientBin);
            Replace(_application, CLIENT_NDS_SERIA, order.ClientNdsSeria);
            Replace(_application, CLIENT_BANK, order.ClientBank);
            Replace(_application, CLIENT_BANK_BIN, order.ClientBankBin);
            Replace(_application, CLIENT_BANK_SWIFT_CODE, order.ClientBankSwiftCode);
            Replace(_application, CLIENT_CURRENT_ACCOUNT, order.ClientCurrentAccount);
            Replace(_application, CLIENT_LEGAL_ADDRESS, order.ClientLegalAddress);
            Replace(_application, CLEINT_CEO_FULL_NAME, order.ClientCeoFullName);

            string newDocument = Path.Combine(Directory.GetCurrentDirectory(), "Files",
                $"transportation_order_{order.ClientBin}.docx");
            _document.SaveAs2(newDocument);

            _document.Close();
            _application.Quit();
            
            return newDocument;
        }

        private void Replace(Application wordApp, object textToFind, object replaceWithText)
        {
            object matchCase = true;
            object matchwholeWord = true;
            object matchwildCards = false;
            object matchSoundLike = false;
            object nmatchAllforms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiactitics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = -2;
            object wrap = 1;
            wordApp.Selection.Find.Execute(ref textToFind, ref matchCase,
                ref matchwholeWord, ref matchwildCards, ref matchSoundLike,
                ref nmatchAllforms, ref forward,
                ref wrap, ref format, ref replaceWithText,
                ref replace, ref matchKashida,
                ref matchDiactitics, ref matchAlefHamza,
                ref matchControl);
        }
    }
}