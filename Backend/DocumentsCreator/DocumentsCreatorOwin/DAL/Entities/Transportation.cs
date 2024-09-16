namespace DocumentsCreatorOwin.DAL.Entities
{
    public class Transportation
    {
        public int Id { set; get; }
        
        public int TransportationOrderId { set; get; }
        public TransportationOrder TransportationOrder { set; get; }

        public string DriverId { set; get; }
        public AspNetUser Driver { set; get; }
    }
}