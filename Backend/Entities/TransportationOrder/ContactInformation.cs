namespace Entities;

public class ContactInformation : EntityBase
{
    public int TransportationOrderId { set; get; }
    public TransportationOrder TransportationOrder { set; get; }
    public string LoadingTime { set; get; }
    public string LoadingContactPerson { set; get; }
    public string LoadingContactPhone { set; get; }
    public string UnloadingContactPerson { set; get; }
    public string UnloadingContactPhone { set; get; }
}