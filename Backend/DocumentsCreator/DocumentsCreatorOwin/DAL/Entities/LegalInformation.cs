namespace DocumentsCreatorOwin.DAL.Entities
{
    public class LegalInformation
    {
        public int Id { set; get; }
        
        public string ShipperId { set; get; }
        public AspNetUser Shipper { set; get; }
    
        public string CompanyName { set; get; }
        public string PhoneNumber { set; get; }
        public string Email { set; get; }
        public string Bin { set; get; }
        public string VatSeria { set; get; }
        public string BankName { set; get; }
        public string BankBin { set; get; }
        public string BankSwiftCode { set; get; }
        public string AccountNumber { set; get; }
        public string LegalAddress { set; get; }
        public string CompanyCeo { set; get; }
    }
}

