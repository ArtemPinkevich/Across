namespace DocumentsCreatorOwin.Services
{
    public class OrderDocument
    {
        public string Route { set; get; }
        public string NumberOfTrucks { set; get; }
        public string DestinationAddress { set; get; }
        public string LoadingDateTime { set; get; }
        public string LoadingPersonFullName { set; get; }
        public string CargoName { set; get; }
        public string CargoPrice { set; get; }
        public string CargoReceiverFullName { set; get; }
        public string UnloadingAddress { set; get; }
        public string UnloadingPersonFullName { set; get; }
        public string UnloadingDateTime { set; get; }
        public string TransportationPrice { set; get; }
        public string DriverFullName { set; get; }
        public string ManagerFullName { set; get; }
        public string ClientCompany { set; get; }
        public string ClientPhoneNumber { set; get; }
        public string ClientEmail { set; get; }
        public string ClientBin { set; get; }
        public string ClientNdsSeria { set; get; }
        public string ClientBank { set; get; }
        public string ClientBankBin { set; get; }
        public string ClientBankSwiftCode { set; get; }
        public string ClientCurrentAccount { set; get; }
        public string ClientLegalAddress { set; get; }
        public string ClientCeoFullName { set; get; }
    }
}