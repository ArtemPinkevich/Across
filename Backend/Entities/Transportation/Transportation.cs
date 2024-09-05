using System.Collections.Generic;

namespace Entities;

public class Transportation : EntityBase
{
    public int TransportationOrderId { set; get; }
    public TransportationOrder TransportationOrder { set; get; }
    
    public string DriverId { set; get; }
    public User Driver { set; get; }
    
    public int TruckId { set; get; }
    public Truck Truck { set; get; }
    
    public List<TransportationStatusRecord> TransportationOrderStatusRecords { set; get; }
    
    public TransportationStatus TransportationStatus { set; get; }
    
    public string Comment { set; get; }
    
    public List<RoutePoint> RoutePoints { set; get; }

    //Тут по необходимости можно добавить другие поля связанные с
    //процессом перевозки груза (время в пути, маршрут, комментарии водителя итд)
    //public string CreatedDateTime { set; get; }
    //public string FinishedDateTime { set; get; }
}