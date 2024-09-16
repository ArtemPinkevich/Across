namespace DocumentsCreatorOwin.DAL.Entities
{
    public class TransportationOrder
    {
        public int Id { set; get; }
        
        public string ShipperId { set; get; }
        
        public Cargo Cargo { set; get; }
        
        public int Price { set; get; }
    
        public string LoadDateFrom { set; get; }
    
        public string LoadDateTo { set; get; }
    
        public string LoadingLocalityName { set; get; }
    
        public string LoadingAddress { set; get; }
    
        public string UnloadingLocalityName { set; get; }
    
        public string UnloadingAddress { set; get; }
    }
}